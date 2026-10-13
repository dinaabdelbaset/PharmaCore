using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.Infrastructure.Entities;

namespace PharmaCore.LicenseGenerator
{
    public partial class FrmActivationManager : Form
    {
        private string _connectionString => PharmaCore.ActivationManager.Home.ActiveConnectionString
            ?? throw new InvalidOperationException("No server selected.");

        private bool _ascending = true; // toggle for grid sorting
        private const int DefaultTake = 500;
        private bool _showAll = false;
        private string? _lastCodePlain;
        private bool _loadingCodes; // re-entrancy guard

        private DateTime MakeUtc(DateTime dt) => DateTime.SpecifyKind(dt, DateTimeKind.Utc);

        public FrmActivationManager()
        {
            InitializeComponent();
            Load += FrmActivationManager_Load;
            cboOrg.SelectedIndexChanged += async (s, e) => { _showAll = false; await LoadTenantsAsync(); await LoadCodesAsync(); UpdateGenerateButtonState(); };
            cboTenant.SelectedIndexChanged += async (s, e) => { _showAll = false; await LoadRegionsAsync(); await LoadCodesAsync(); UpdateGenerateButtonState(); };
            cboRegion.SelectedIndexChanged += async (s, e) => { _showAll = false; await LoadBranchesAsync(); await LoadCodesAsync(); UpdateGenerateButtonState(); };
            cboBranch.SelectedIndexChanged += async (_, __) => { _showAll = false; await LoadCodesAsync(); UpdateGenerateButtonState(); };
            btnGenerate.Click += async (_, __) => await GenerateActivationCodeAsync();
            grdCodes.ColumnHeaderMouseClick += (_, e) => SortCodes(e.ColumnIndex);
            grdCodes.CellContentClick += GrdCodes_CellContentClick;
            grdCodes.DataBindingComplete += (_, __) => ColorExpiredRows();
            btnShowAll.Click += async (_, __) => { _showAll = true; await LoadCodesAsync(); SelectLastCodeRow(); };
            lblLastCode.Click += (_, __) => SelectLastCodeRow();
        }

        private void FrmActivationManager_Load(object? sender, EventArgs e)
        { _ = InitializeAsync(); }
        private async Task InitializeAsync()
        {
            var server = PharmaCore.ActivationManager.Home.ActiveServer;
            if (server != null) Text = $"Activation Manager — Server: {server.ServerName} ({server.Host})";
            await LoadLastCodePersistentAsync(); await LoadOrgsAsync(); await LoadCodesAsync(); PrepareGrid(); UpdateGenerateButtonState();
        }

        private AppDbContext CreateDb(Guid orgId, Guid? branchId=null)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options;
            return new AppDbContext(options, new SimpleTenantContext(orgId, branchId));
        }

        private void UpdateGenerateButtonState()
        {
            btnGenerate.Enabled = (cboOrg.SelectedValue is Guid)
                               && (cboTenant.SelectedValue is Guid)
                               && (cboRegion.SelectedValue is Guid)
                               && (cboBranch.SelectedValue is Guid)
                               && numMaxDevices.Value > 0;
        }

        #region Load Combos
        private async Task LoadOrgsAsync()
        {
            using var db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options, new SimpleTenantContext(Guid.Empty));
            var orgs = await db.Organizations.IgnoreQueryFilters().AsNoTracking().OrderBy(o=>o.Name).ToListAsync();
            cboOrg.DataSource = orgs; cboOrg.DisplayMember="Name"; cboOrg.ValueMember="Id"; cboOrg.SelectedIndex=-1;
            // reset and disable dependents
            cboTenant.DataSource=null; cboRegion.DataSource=null; cboBranch.DataSource=null;
            cboTenant.Enabled = false; cboRegion.Enabled = false; cboBranch.Enabled = false;
        }
        private async Task LoadTenantsAsync()
        {
            cboTenant.DataSource=null; cboRegion.DataSource=null; cboBranch.DataSource=null;
            cboRegion.Enabled=false; cboBranch.Enabled=false;
            if(cboOrg.SelectedValue is not Guid orgId){ cboTenant.Enabled=false; return; }
            using var db = CreateDb(orgId);
            var tenants = await db.Tenants.AsNoTracking()
                .OrderBy(t=>t.Name).ToListAsync(); // scoped by org via context
            cboTenant.DataSource=tenants; cboTenant.DisplayMember="Name"; cboTenant.ValueMember="Id"; cboTenant.SelectedIndex=-1;
            cboTenant.Enabled = true;
        }
        private async Task LoadRegionsAsync()
        {
            cboRegion.DataSource=null; cboBranch.DataSource=null; cboBranch.Enabled=false;
            if(cboTenant.SelectedValue is not Guid tenantId){ cboRegion.Enabled=false; return; }
            if(cboOrg.SelectedValue is not Guid orgId){ cboRegion.Enabled=false; return; }
            using var db = CreateDb(orgId);
            var regions = await db.Regions.AsNoTracking()
                .Where(r=>r.TenantId==tenantId)
                .OrderBy(r=>r.Name).ToListAsync();
            cboRegion.DataSource=regions; cboRegion.DisplayMember="Name"; cboRegion.ValueMember="Id"; cboRegion.SelectedIndex=-1;
            cboRegion.Enabled=true;
        }
        private async Task LoadBranchesAsync()
        {
            cboBranch.DataSource=null;
            if(cboRegion.SelectedValue is not Guid regionId){ cboBranch.Enabled=false; return; }
            if(cboOrg.SelectedValue is not Guid orgId){ cboBranch.Enabled=false; return; }
            using var db = CreateDb(orgId);
            var branches = await db.Branches.AsNoTracking()
                .Where(b=>b.RegionId==regionId)
                .OrderBy(b=>b.Name).ToListAsync();
            cboBranch.DataSource=branches; cboBranch.DisplayMember="Name"; cboBranch.ValueMember="Id"; cboBranch.SelectedIndex=-1;
            cboBranch.Enabled=true;
        }
        #endregion

        #region Generate Code
        private async Task<bool> EnsureChainConsistencyAsync(Guid orgId, Guid tenantId, Guid regionId, Guid branchId)
        {
            using var db = CreateDb(orgId, branchId);
            var tenantOk = await db.Tenants.AsNoTracking().AnyAsync(t=>t.Id==tenantId); // org scoping via context
            if(!tenantOk) return false;
            var regionOk = await db.Regions.AsNoTracking().AnyAsync(r=>r.Id==regionId && r.TenantId==tenantId);
            if(!regionOk) return false;
            var branchOk = await db.Branches.AsNoTracking().AnyAsync(b=>b.Id==branchId && b.RegionId==regionId);
            return branchOk;
        }

        private async Task GenerateActivationCodeAsync()
        {
            // existing validation ...
            if(cboOrg.SelectedValue is not Guid orgId){MessageBox.Show("اختر Organization","تنبيه");return;}
            if(cboTenant.SelectedValue is not Guid tenantId){MessageBox.Show("اختر Tenant","تنبيه");return;}
            if(cboRegion.SelectedValue is not Guid regionId){MessageBox.Show("اختر Region","تنبيه");return;}
            if(cboBranch.SelectedValue is not Guid branchId){MessageBox.Show("اختر Branch","تنبيه");return;}
            if(numMaxDevices.Value <= 0){MessageBox.Show("عدد الأجهزة يجب أن يكون أكبر من صفر","تنبيه");return;}
            if(!await EnsureChainConsistencyAsync(orgId, tenantId, regionId, branchId))
            { MessageBox.Show("اختيارات غير متوافقة مع البيانات. تأكد من التدرج الصحيح","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error); return; }
            // Generate unique code (retry few times if accidental collision)
            string plain; int tries=0; const int maxTries=5; bool exists;
            do { plain = GeneratePlainCode(); using var dupDb = CreateDb(orgId, branchId); exists = await dupDb.ActivationCodes.AnyAsync(c => c.CodePlain == plain); tries++; } while(exists && tries < maxTries);
            if(exists){ MessageBox.Show("تعذر توليد كود فريد، حاول مرة أخرى","تنبيه"); return; }
            var hash = ComputeSha256(plain);
            using var db = CreateDb(orgId, branchId);
            var entity = new ActivationCode
            {
                OrgId = orgId, TenantId = tenantId, RegionId = regionId, BranchId = branchId,
                CodePlain = plain, CodeHash = hash, MaxDevices = (int)numMaxDevices.Value,
                UsedDevicesCount = 0, ExpiresAt = MakeUtc(dtpExpiry.Value.Date), CreatedAt = DateTime.UtcNow
            };
            db.ActivationCodes.Add(entity); await db.SaveChangesAsync();
            _lastCodePlain = plain; lblLastCode.Text = $"Last Code: {plain}";
            await LoadCodesAsync();
        }

        private string GeneratePlainCode()
        {
            // pattern: 4-5-4-4 random upper letters+digits
            string seg(int len){const string chars="ABCDEFGHJKLMNPQRSTUVWXYZ23456789";using var rng=RandomNumberGenerator.Create();var bytes=new byte[len];rng.GetBytes(bytes);var sb=new StringBuilder();foreach(var b in bytes) sb.Append(chars[b%chars.Length]);return sb.ToString();}
            return $"{seg(4)}-{seg(5)}-{seg(4)}-{seg(4)}";
        }
        private string ComputeSha256(string input){using var sha=SHA256.Create();var bytes=sha.ComputeHash(Encoding.UTF8.GetBytes(input));return Convert.ToHexString(bytes);}        
        #endregion

        #region Grid Display
        private void PrepareGrid()
        { if(grdCodes.Columns.Count==0) return; }

        private async Task LoadCodesAsync()
        {
            if (_loadingCodes) return;
            _loadingCodes = true;
            try
            {
            if(_showAll)
            {
                using var globalDb = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options, new SimpleTenantContext(Guid.Empty));
                var listAll = await globalDb.ActivationCodes.IgnoreQueryFilters().AsNoTracking()
                    .OrderByDescending(c=>c.CreatedAt)
                    .Select(c=> new ActivationGridRow
                    {
                        Code = c.CodePlain,
                        Org = c.Organization!=null?c.Organization.Name:"",
                        Tenant = c.Tenant!=null?c.Tenant.Name:"",
                        Region = c.Region!=null?c.Region.Name:"",
                        Branch = c.Branch!=null?c.Branch.Name:"",
                        MaxDevices = c.MaxDevices,
                        UsedDevices = c.UsedDevicesCount,
                        Remaining = c.MaxDevices - c.UsedDevicesCount,
                        ExpiresAt = c.ExpiresAt,
                        CreatedAt = c.CreatedAt
                    }).ToListAsync();
                grdCodes.DataSource = listAll; ConfigureGridColumns(); ColorExpiredRows(); return;
            }
            if(cboOrg.SelectedValue is not Guid orgId){ grdCodes.DataSource=null; return; }
            using var db = CreateDb(orgId);
            Guid? tenantId = cboTenant.SelectedValue as Guid?;
            Guid? regionId = cboRegion.SelectedValue as Guid?;
            Guid? branchId = cboBranch.SelectedValue as Guid?;
            var query = db.ActivationCodes.Include(c=>c.Organization).Include(c=>c.Tenant).Include(c=>c.Region).Include(c=>c.Branch).AsNoTracking();
            if(tenantId.HasValue) query = query.Where(c=>c.TenantId==tenantId);
            if(regionId.HasValue) query = query.Where(c=>c.RegionId==regionId);
            if(branchId.HasValue) query = query.Where(c=>c.BranchId==branchId);
            var list = await query.OrderByDescending(c=>c.CreatedAt).Take(DefaultTake)
                .Select(c=> new ActivationGridRow
                {
                    Code = c.CodePlain,
                    Org = c.Organization!=null?c.Organization.Name:"",
                    Tenant = c.Tenant!=null?c.Tenant.Name:"",
                    Region = c.Region!=null?c.Region.Name:"",
                    Branch = c.Branch!=null?c.Branch.Name:"",
                    MaxDevices = c.MaxDevices,
                    UsedDevices = c.UsedDevicesCount,
                    Remaining = c.MaxDevices - c.UsedDevicesCount,
                    ExpiresAt = c.ExpiresAt,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();
            grdCodes.DataSource = list; ConfigureGridColumns(); ColorExpiredRows();
            }
            finally { _loadingCodes = false; }
        }
        private void ColorExpiredRows()
        {
            var todayUtc = DateTime.UtcNow.Date;
            foreach (DataGridViewRow row in grdCodes.Rows)
            {
                if (row.Cells["ExpiresAt"].Value is DateTime dt && dt.Kind != DateTimeKind.Utc)
                {
                    dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                }
                if (row.Cells["ExpiresAt"].Value is DateTime dtu && dtu.Date < todayUtc)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (row.Cells["MaxDevices"].Value is int mx && mx > 0 && row.Cells["UsedDevices"].Value is int used)
                {
                    double pct = used * 100.0 / mx;
                    if (pct >= 100)
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (pct >= 80)
                    {
                        row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                    }
                }
            }
        }
        private void ConfigureGridColumns()
        {
            if (grdCodes.Columns["Copy"] == null)
            { var copyCol = new DataGridViewButtonColumn { Name = "Copy", HeaderText = "Copy", Text = "Copy", UseColumnTextForButtonValue = true, Width = 60 }; grdCodes.Columns.Insert(0, copyCol); }
            if (grdCodes.Columns["Deactivate"] == null)
            { var deactCol = new DataGridViewButtonColumn { Name = "Deactivate", HeaderText = "Deactivate", Text = "Off", UseColumnTextForButtonValue = true, Width = 70 }; grdCodes.Columns.Insert(1, deactCol); }
            if (grdCodes.Columns["Delete"] == null)
            { var delCol = new DataGridViewButtonColumn { Name = "Delete", HeaderText = "Delete", Text = "Del", UseColumnTextForButtonValue = true, Width = 60 }; grdCodes.Columns.Insert(2, delCol); }
            foreach (DataGridViewColumn col in grdCodes.Columns) { col.SortMode = DataGridViewColumnSortMode.Programmatic; }
        }
        private async void GrdCodes_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; var name = grdCodes.Columns[e.ColumnIndex].Name;
            var code = grdCodes.Rows[e.RowIndex].Cells["Code"].Value?.ToString();
            if (name == "Copy")
            {
                if (!string.IsNullOrWhiteSpace(code)) { Clipboard.SetText(code); MessageBox.Show("تم النسخ"); }
            }
            else if (name == "Deactivate")
            {
                if (string.IsNullOrWhiteSpace(code)) return;
                if (MessageBox.Show("تعطيل هذا الكود؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cboOrg.SelectedValue is not Guid orgId) return; using var db = CreateDb(orgId);
                    var entity = await db.ActivationCodes.FirstOrDefaultAsync(c => c.CodePlain == code);
                    if (entity != null)
                    { entity.MaxDevices = entity.UsedDevicesCount; entity.ExpiresAt = DateTime.UtcNow.Date.AddDays(-1); await db.SaveChangesAsync(); await LoadCodesAsync(); }
                }
            }
            else if (name == "Delete")
            {
                if (string.IsNullOrWhiteSpace(code)) return;
                if (MessageBox.Show("حذف هذا الكود؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (cboOrg.SelectedValue is not Guid orgId) return; using var db = CreateDb(orgId);
                    var entity = await db.ActivationCodes.FirstOrDefaultAsync(c => c.CodePlain == code);
                    if (entity != null)
                    { db.ActivationCodes.Remove(entity); await db.SaveChangesAsync(); await LoadCodesAsync(); }
                }
            }
        }

        private async Task LoadLastCodePersistentAsync()
        {
            // Global context, ignore filters to get absolute last code
            using var db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options, new SimpleTenantContext(Guid.Empty));
            var last = await db.ActivationCodes.IgnoreQueryFilters().AsNoTracking().OrderByDescending(c=>c.CreatedAt).Select(c=>c.CodePlain).FirstOrDefaultAsync();
            if (!string.IsNullOrWhiteSpace(last)) { _lastCodePlain = last; lblLastCode.Text = $"Last Code: {last}"; }
        }

        private void SelectLastCodeRow()
        {
            if (string.IsNullOrWhiteSpace(_lastCodePlain) || grdCodes.DataSource == null) return;
            foreach (DataGridViewRow row in grdCodes.Rows)
            {
                if (row.Cells["Code"].Value?.ToString() == _lastCodePlain)
                {
                    row.Selected = true;
                    grdCodes.CurrentCell = row.Cells["Code"]; // focus
                    grdCodes.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void SortCodes(int colIndex)
        {
            if (grdCodes.DataSource is not System.Collections.Generic.List<ActivationGridRow> data) return; var col = grdCodes.Columns[colIndex];
            Func<ActivationGridRow, object> key = col.Name switch
            {
                "Copy" => r => r.Code,
                "Deactivate" => r => r.Code,
                "Code" => r => r.Code,
                "Org" => r => r.Org,
                "Tenant" => r => r.Tenant,
                "Region" => r => r.Region,
                "Branch" => r => r.Branch,
                "MaxDevices" => r => r.MaxDevices,
                "UsedDevices" => r => r.UsedDevices,
                "Remaining" => r => r.Remaining,
                "ExpiresAt" => r => r.ExpiresAt ?? DateTime.MaxValue,
                "CreatedAt" => r => r.CreatedAt,
                _ => r => r.Code
            }; _ascending = !_ascending; var sorted = _ascending ? data.OrderBy(key).ToList() : data.OrderByDescending(key).ToList(); grdCodes.DataSource = sorted; ColorExpiredRows();
        }
        private record ActivationGridRow
        {
            public string Code { get; set; } = string.Empty; public string Org { get; set; } = string.Empty; public string Tenant { get; set; } = string.Empty; public string Region { get; set; } = string.Empty; public string Branch { get; set; } = string.Empty;
            public int MaxDevices { get; set; } public int UsedDevices { get; set; } public int Remaining { get; set; } public DateTime? ExpiresAt { get; set; } public DateTime CreatedAt { get; set; }
        }
        #endregion
    }
}
