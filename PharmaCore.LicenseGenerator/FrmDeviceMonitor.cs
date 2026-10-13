using System;using System.Linq;using System.Threading.Tasks;using System.Windows.Forms;using Microsoft.EntityFrameworkCore;using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;using PharmaCore.Infrastructure.Entities;using System.IO;using System.Text;

namespace PharmaCore.ActivationManager
{
    public partial class FrmDeviceMonitor : Form
    {
        private DeviceMonitorRepository? _repo;        
        public FrmDeviceMonitor(){ InitializeComponent(); }
        private async void FrmDeviceMonitor_Load(object sender, EventArgs e){ try { await InitializeAsync(); } catch(Exception ex) { MessageBox.Show($"خطأ في الاتصال بقاعدة البيانات:\n{ex.Message}\n\nاضبط الاتصال من زر ⚙ DB Settings.", "خطأ اتصال", MessageBoxButtons.OK, MessageBoxIcon.Error); } }
        private string _connectionString => Home.ActiveConnectionString
            ?? throw new InvalidOperationException("No server selected.");

        private async Task InitializeAsync()
        {
            var server = Home.ActiveServer;
            if (server != null) Text = $"Device Monitor — Server: {server.ServerName} ({server.Host})";

            _repo = new DeviceMonitorRepository(_connectionString);
            await _repo.EnsureSchemaAsync();
            await LoadOrgsAsync();
            WireEvents();
            await RefreshGridAsync();
        }
        private System.Collections.Generic.List<BranchAuthDbLocal.DeviceInfo> _devices = new();
        private void WireEvents(){ btnRefresh.Click += async (_,__) => await RefreshGridAsync(); btnDeactivate.Click += async (_,__) => await ChangeSelectedAsync(false); btnActivate.Click += async (_,__) => await ChangeSelectedAsync(true); cboOrg.SelectedIndexChanged += async (_,__) => { await LoadBranchesAsync(); await RefreshGridAsync(); }; cboBranch.SelectedIndexChanged += async (_,__) => await RefreshGridAsync(); cboStatusFilter.SelectedIndexChanged += (_,__) => ApplyFilterAndCards(); btnExportCsv.Click += (_,__) => ExportCsv(); }
        private async Task LoadOrgsAsync(){ using var db = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options, new SimpleTenantContext(Guid.Empty)); var orgs = await db.Organizations.IgnoreQueryFilters().AsNoTracking().OrderBy(o=>o.Name).ToListAsync(); cboOrg.DataSource=orgs; cboOrg.DisplayMember="Name"; cboOrg.ValueMember="Id"; cboOrg.SelectedIndex=-1; }
        private async Task LoadBranchesAsync(){ if(cboOrg.SelectedValue is not Guid orgId){ cboBranch.DataSource=null; return; } using var db=new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options,new SimpleTenantContext(orgId)); var branches = await db.Branches.AsNoTracking().OrderBy(b=>b.Name).ToListAsync(); cboBranch.DataSource=branches; cboBranch.DisplayMember="Name"; cboBranch.ValueMember="Id"; cboBranch.SelectedIndex=-1; }
        private async Task RefreshGridAsync(){ if(_repo==null){return;} if(cboOrg.SelectedValue is not Guid orgId){ _devices.Clear(); RebuildCards(); return; } Guid branchId = cboBranch.SelectedValue is Guid g? g: Guid.Empty; _devices = (await _repo.GetDevicesAsync(orgId, branchId)).ToList(); lblStatus.Text=$"Devices: {_devices.Count}"; ApplyFilterAndCards(); }
        private void ApplyFilterAndCards(){ var filter = cboStatusFilter.SelectedItem?.ToString() ?? "All"; var now = DateTime.UtcNow; var list = _devices.Where(d=>
                filter=="All" ||
                (filter=="Active" && d.IsActive && d.LastHeartbeatAt.HasValue && (now - d.LastHeartbeatAt.Value).TotalMinutes < 5) ||
                (filter=="Inactive" && !d.IsActive) ||
                (filter=="Stale" && d.IsActive && (!d.LastHeartbeatAt.HasValue || (now - d.LastHeartbeatAt.Value).TotalMinutes >=5))
            ).ToList(); RebuildCards(list); }
        private void RebuildCards(System.Collections.Generic.IEnumerable<BranchAuthDbLocal.DeviceInfo>? subset = null){ flwCards.SuspendLayout(); flwCards.Controls.Clear(); var arr = subset ?? _devices; foreach(var d in arr){ flwCards.Controls.Add(BuildCard(d)); } flwCards.ResumeLayout(); }
        private Control BuildCard(BranchAuthDbLocal.DeviceInfo d){ var panel = new Panel{ Width=220, Height=140, BorderStyle=BorderStyle.FixedSingle, Margin=new Padding(6)}; var now = DateTime.UtcNow; bool live = d.IsActive && d.LastHeartbeatAt.HasValue && (now - d.LastHeartbeatAt.Value).TotalMinutes <5; var circle = new Panel{ Width=16, Height=16, Left=4, Top=4, BackColor= live? System.Drawing.Color.MediumSeaGreen : System.Drawing.Color.Gray, BorderStyle=BorderStyle.None}; circle.Paint += (_,e)=>{ e.Graphics.FillEllipse(new System.Drawing.SolidBrush(circle.BackColor),0,0,16,16);}; panel.Controls.Add(circle); string uptime = FormatSeconds(d.TotalActiveSeconds); string since = live? $"متصل منذ {(int)(now - d.LastHeartbeatAt!.Value).TotalMinutes} دقيقة" : d.LastHeartbeatAt.HasValue? $"متوقف منذ {(int)(now - d.LastHeartbeatAt.Value).TotalMinutes} دقيقة" : "لم يتصل سابقاً"; var lbl = new Label{ Left=24, Top=4, Width=190, Height=130, Text=$"User: {d.UserName}\nHW: {d.HwidHash.Substring(0,8)}...\nAllowed: {d.AllowedDevices}\nنشط: {d.IsActive}\nUptime: {uptime}\n{since}", AutoSize=false}; panel.Controls.Add(lbl); panel.Tag=d.Id; panel.DoubleClick += async (_,__)=> await ToggleDevice(panel.Tag as Guid?, d.IsActive); return panel; }
        private string FormatSeconds(int sec){ var ts = TimeSpan.FromSeconds(sec); return $"{(int)ts.TotalHours}h {ts.Minutes}m"; }
        private async Task ToggleDevice(Guid? id, bool current){ if(id==null || _repo==null) return; await _repo.SetActiveAsync(id.Value, !current); await RefreshGridAsync(); }
        private async Task ChangeSelectedAsync(bool active){ if(_repo==null) return; if(grd.SelectedRows.Count==0) return; foreach(DataGridViewRow r in grd.SelectedRows){ if(r.Cells["Id"].Value is Guid id){ await _repo.SetActiveAsync(id, active); } } await RefreshGridAsync(); }
        private void ExportCsv(){ if(_devices.Count==0){ MessageBox.Show("لا توجد بيانات"); return; } using var sfd = new SaveFileDialog{ Filter="CSV|*.csv"}; if(sfd.ShowDialog()!= DialogResult.OK) return; var sb = new StringBuilder(); sb.AppendLine("Id,UserName,HwidHash,IsActive,AllowedDevices,TotalActiveSeconds,LastLoginAt,LastHeartbeatAt,LastIp"); foreach(var d in _devices){ sb.AppendLine($"{d.Id},{d.UserName},{d.HwidHash},{d.IsActive},{d.AllowedDevices},{d.TotalActiveSeconds},{d.LastLoginAt},{d.LastHeartbeatAt},{d.LastIp}"); } File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8); MessageBox.Show("تم التصدير"); }
        private void ColorRows(){ foreach(DataGridViewRow r in grd.Rows){ bool isActive = r.Cells["IsActive"].Value is bool b && b; DateTime? lastHb = r.Cells["LastHeartbeatAt"].Value as DateTime?; if(isActive && lastHb.HasValue && (DateTime.UtcNow - lastHb.Value).TotalMinutes < 5){ r.DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew; r.DefaultCellStyle.ForeColor=System.Drawing.Color.DarkGreen; } else if(isActive){ r.DefaultCellStyle.BackColor=System.Drawing.Color.LemonChiffon; r.DefaultCellStyle.ForeColor=System.Drawing.Color.DarkGoldenrod; } else { r.DefaultCellStyle.BackColor=System.Drawing.Color.MistyRose; r.DefaultCellStyle.ForeColor=System.Drawing.Color.DarkRed; } } }
    }
    internal sealed class SimpleTenantContext : ICurrentTenantContext { public Guid OrgId {get;} public Guid? BranchId {get;} public SimpleTenantContext(Guid orgId, Guid? branchId=null){ OrgId=orgId; BranchId=branchId; } }}