using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using DataOrganization=PharmaCore.Domain.Entities.Organization;
using DataTenant=PharmaCore.Domain.Entities.Tenant;
using DataRegion=PharmaCore.Domain.Entities.Region;
using DataBranch=PharmaCore.Domain.Entities.Branch;


namespace PharmaCore.ActivationManager
{


    public partial class MasterDataManager : Form
    {
        // Use the currently selected server from Home screen
        private string _connectionString => Home.ActiveConnectionString 
            ?? throw new InvalidOperationException("No server selected. Please select a server from the Home screen.");

        private readonly BindingSource _bsOrgs = new();
        private readonly BindingSource _bsTenants = new();
        private readonly BindingSource _bsRegions = new();
        private readonly BindingSource _bsBranches = new();

        private record ItemVM(Guid Id, string Code, string Name, bool IsActive);

        private bool ActiveOnly => chkFilterActive.Checked;

        private readonly Dictionary<string,bool> _columnSortDirections = new(); // track ascending state per column

        public MasterDataManager()
        {
            InitializeComponent();
            grdOrgs.AutoGenerateColumns = true; grdOrgs.DataSource = _bsOrgs;
            grdTenants.AutoGenerateColumns = true; grdTenants.DataSource = _bsTenants;
            grdRegions.AutoGenerateColumns = true; grdRegions.DataSource = _bsRegions;
            grdBranches.AutoGenerateColumns = true; grdBranches.DataSource = _bsBranches;

            // Sorting events
            grdOrgs.ColumnHeaderMouseClick += (_, e) => SortGrid(_bsOrgs, grdOrgs, e.ColumnIndex);
            grdTenants.ColumnHeaderMouseClick += (_, e) => SortGrid(_bsTenants, grdTenants, e.ColumnIndex);
            grdRegions.ColumnHeaderMouseClick += (_, e) => SortGrid(_bsRegions, grdRegions, e.ColumnIndex);
            grdBranches.ColumnHeaderMouseClick += (_, e) => SortGrid(_bsBranches, grdBranches, e.ColumnIndex);

            grdOrgs.DataBindingComplete += (_, __) => SetColumnsSortable(grdOrgs);
            grdTenants.DataBindingComplete += (_, __) => SetColumnsSortable(grdTenants);
            grdRegions.DataBindingComplete += (_, __) => SetColumnsSortable(grdRegions);
            grdBranches.DataBindingComplete += (_, __) => SetColumnsSortable(grdBranches);

            Load += MasterDataManager_Load;

            // Grid selection
            grdOrgs.CellClick += (_, __) => LoadOrgSelection();
            grdTenants.CellClick += (_, __) => LoadTenantSelection();
            grdRegions.CellClick += (_, __) => LoadRegionSelection();
            grdBranches.CellClick += (_, __) => LoadBranchSelection();

            // Cascading combos
            cboTenantOrg.SelectedIndexChanged += async (_, __) => await LoadTenantsAsync();
            cboRegionTenant.SelectedIndexChanged += async (_, __) => await LoadRegionsAsync();
            cboBranchRegion.SelectedIndexChanged += async (_, __) => await LoadBranchesAsync();

            // Org buttons
            btnOrgAdd.Click += async (_, __) => await AddOrgAsync();
            btnOrgUpdate.Click += async (_, __) => await UpdateOrgAsync();
            btnOrgClear.Click += (_, __) => ClearOrgInputs();

            // Tenant buttons
            btnTenantAdd.Click += async (_, __) => await AddTenantAsync();
            btnTenantUpdate.Click += async (_, __) => await UpdateTenantAsync();
            btnTenantClear.Click += (_, __) => ClearTenantInputs();

            // Region buttons
            btnRegionAdd.Click += async (_, __) => await AddRegionAsync();
            btnRegionUpdate.Click += async (_, __) => await UpdateRegionAsync();
            btnRegionClear.Click += (_, __) => ClearRegionInputs();

            // Branch buttons
            btnBranchAdd.Click += async (_, __) => await AddBranchAsync();
            btnBranchUpdate.Click += async (_, __) => await UpdateBranchAsync();
            btnBranchClear.Click += (_, __) => ClearBranchInputs();

            // Filter checkbox
            chkFilterActive.CheckedChanged += async (_, __) => await ReloadAllAfterFilterAsync();
        }

        private AppDbContext CreateDb(Guid orgId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            return new AppDbContext(options, new MasterTenantContext(orgId));
        }

        private AppDbContext CreateDbGlobal()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            // مبدئياً بنمرر Guid.Empty – لو حبيت بعدين تعدّل الفلاتر العالمية
            return new AppDbContext(options, new MasterTenantContext(Guid.Empty));
        }

        private async void MasterDataManager_Load(object? sender, EventArgs e)
        {
            // Show which server we're managing
            var server = Home.ActiveServer;
            if (server != null)
                Text = $"Master Data — Server: {server.ServerName} ({server.Host})";

            await LoadOrganizationsAsync();
            await LoadOrgComboForTenantsAsync();
        }

        private static bool IsBlank(string? v) => string.IsNullOrWhiteSpace(v);
        private static string Norm(string v) => v.Trim().ToUpperInvariant();
        private void SetBusy(bool busy)
        {
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;

            foreach (var btn in new[]
            {
                btnOrgAdd, btnOrgUpdate, btnOrgClear,
                btnTenantAdd, btnTenantUpdate, btnTenantClear,
                btnRegionAdd, btnRegionUpdate, btnRegionClear,
                btnBranchAdd, btnBranchUpdate, btnBranchClear
            })
                btn.Enabled = !busy;
        }

        private async Task ReloadAllAfterFilterAsync()
        {
            // reload root + dependent selections preserving current selection ids if possible
            await LoadOrganizationsAsync();
            await LoadOrgComboForTenantsAsync();
            await LoadTenantsAsync();
            await LoadRegionsAsync();
            await LoadBranchesAsync();
        }

        // Validation helpers (re-added)
        private bool ValidateOrgInputs()
        {
            if (IsBlank(txtOrgCode.Text) || IsBlank(txtOrgName.Text))
            { MessageBox.Show("Org Code & Name required"); return false; }
            return true;
        }
        private bool ValidateTenantInputs()
        {
            if (IsBlank(txtTenantCode.Text) || IsBlank(txtTenantName.Text))
            { MessageBox.Show("Tenant Code & Name required"); return false; }
            if (cboTenantOrg.SelectedValue is not Guid)
            { MessageBox.Show("Select Organization"); return false; }
            return true;
        }
        private bool ValidateRegionInputs()
        {
            if (IsBlank(txtRegionCode.Text) || IsBlank(txtRegionName.Text))
            { MessageBox.Show("Region Code & Name required"); return false; }
            if (cboRegionTenant.SelectedValue is not Guid || cboTenantOrg.SelectedValue is not Guid)
            { MessageBox.Show("Select Tenant & Org"); return false; }
            return true;
        }
        private bool ValidateBranchInputs()
        {
            if (IsBlank(txtBranchCode.Text) || IsBlank(txtBranchName.Text))
            { MessageBox.Show("Branch Code & Name required"); return false; }
            if (cboBranchRegion.SelectedValue is not Guid || cboTenantOrg.SelectedValue is not Guid)
            { MessageBox.Show("Select Region & Org"); return false; }
            return true;
        }

        // Uniqueness helpers
        private async Task<bool> OrgCodeExistsAsync(AppDbContext db, string code) =>
            await db.Organizations.AnyAsync(o => o.Code == code); // global unique

        private async Task<bool> TenantCodeExistsAsync(AppDbContext db, Guid orgId, string code) =>
            await db.Tenants.AnyAsync(t => t.OrgId == orgId && t.Code == code);

        private async Task<bool> RegionCodeExistsAsync(AppDbContext db, Guid tenantId, string code) =>
            await db.Regions.AnyAsync(r => r.TenantId == tenantId && r.Code == code);

        private async Task<bool> BranchCodeExistsAsync(AppDbContext db, Guid orgId, string code) =>
            await db.Branches.AnyAsync(b => b.OrgId == orgId && b.Code == code);


        #region Load lists

        private async Task LoadOrganizationsAsync()
        {
            try
            {
                using var db = CreateDbGlobal();
                var q = db.Organizations.IgnoreQueryFilters().AsNoTracking();
                if (ActiveOnly) q = q.Where(o => o.IsActive);
                var list = await q.OrderBy(o => o.Name).Select(o => new ItemVM(o.Id, o.Code, o.Name, o.IsActive)).ToListAsync();

                _bsOrgs.DataSource = new BindingList<ItemVM>(list);
                _bsOrgs.ResetBindings(false);
                grdOrgs.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadOrgComboForTenantsAsync()
        {
            try
            {
                using var db = CreateDbGlobal();
                var q = db.Organizations.IgnoreQueryFilters().AsNoTracking();
                if (ActiveOnly) q = q.Where(o => o.IsActive);
                var orgs = await q.OrderBy(o => o.Name).ToListAsync();

                cboTenantOrg.DataSource = orgs;
                cboTenantOrg.DisplayMember = "Name";
                cboTenantOrg.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cboTenantOrg.SelectedIndex = -1;
            }
        }

        private async Task LoadTenantsAsync()
        {
            _bsTenants.DataSource = null; cboRegionTenant.DataSource = null; _bsRegions.DataSource = null; cboBranchRegion.DataSource = null; _bsBranches.DataSource = null;
            if (cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                using var db = CreateDb(orgId);
                var q = db.Tenants.IgnoreQueryFilters().AsNoTracking().Where(t=>t.OrgId==orgId); if (ActiveOnly) q = q.Where(t=>t.IsActive);
                var tenants = await q.OrderBy(t=>t.Name).Select(t=>new ItemVM(t.Id,t.Code,t.Name,t.IsActive)).ToListAsync();
                _bsTenants.DataSource = new BindingList<ItemVM>(tenants); _bsTenants.ResetBindings(false); grdTenants.Refresh();
                var comboQ = db.Tenants.IgnoreQueryFilters().AsNoTracking().Where(t=>t.OrgId==orgId); if (ActiveOnly) comboQ = comboQ.Where(t=>t.IsActive);
                cboRegionTenant.DataSource = await comboQ.OrderBy(t=>t.Name).ToListAsync(); cboRegionTenant.DisplayMember="Name"; cboRegionTenant.ValueMember="Id"; cboRegionTenant.SelectedIndex=-1;
            }
            catch (Exception ex){ MessageBox.Show(ex.Message);} }
        private async Task LoadRegionsAsync()
        {
            _bsRegions.DataSource = null; cboBranchRegion.DataSource = null; _bsBranches.DataSource = null;
            if (cboRegionTenant.SelectedValue is not Guid tenantId) return; if (cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                using var db = CreateDb(orgId);
                var q = db.Regions.IgnoreQueryFilters().AsNoTracking().Where(r=>r.TenantId==tenantId); if (ActiveOnly) q = q.Where(r=>r.IsActive);
                var regions = await q.OrderBy(r=>r.Name).Select(r=>new ItemVM(r.Id,r.Code,r.Name,r.IsActive)).ToListAsync();

                _bsRegions.DataSource = new BindingList<ItemVM>(regions); _bsRegions.ResetBindings(false); grdRegions.Refresh();
                var comboQ = db.Regions.IgnoreQueryFilters().AsNoTracking().Where(r=>r.TenantId==tenantId); if (ActiveOnly) comboQ = comboQ.Where(r=>r.IsActive);
                cboBranchRegion.DataSource = await comboQ.OrderBy(r=>r.Name).ToListAsync(); cboBranchRegion.DisplayMember="Name"; cboBranchRegion.ValueMember="Id"; cboBranchRegion.SelectedIndex=-1;
            }
            catch (Exception ex){ MessageBox.Show(ex.Message);} }
        private async Task LoadBranchesAsync()
        {
            _bsBranches.DataSource = null; if (cboBranchRegion.SelectedValue is not Guid regionId) return; if (cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                using var db = CreateDb(orgId);
                var q = db.Branches.IgnoreQueryFilters().AsNoTracking().Where(b=>b.RegionId==regionId); if (ActiveOnly) q = q.Where(b=>b.IsActive);
                var branches = await q.OrderBy(b=>b.Name).Select(b=>new ItemVM(b.Id,b.Code,b.Name,b.IsActive)).ToListAsync();

                _bsBranches.DataSource = new BindingList<ItemVM>(branches); _bsBranches.ResetBindings(false); grdBranches.Refresh();
            }
            catch (Exception ex){ MessageBox.Show(ex.Message);} }
        #endregion

        #region grid selection → inputs

        private void LoadOrgSelection()
        {
            if (grdOrgs.CurrentRow == null)
                return;

            txtOrgCode.Text = grdOrgs.CurrentRow.Cells["Code"].Value?.ToString();
            txtOrgName.Text = grdOrgs.CurrentRow.Cells["Name"].Value?.ToString();
            chkOrgActive.Checked =
                grdOrgs.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadTenantSelection()
        {
            if (grdTenants.CurrentRow == null)
                return;

            txtTenantCode.Text = grdTenants.CurrentRow.Cells["Code"].Value?.ToString();
            txtTenantName.Text = grdTenants.CurrentRow.Cells["Name"].Value?.ToString();
            chkTenantActive.Checked =
                grdTenants.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadRegionSelection()
        {
            if (grdRegions.CurrentRow == null)
                return;

            txtRegionCode.Text = grdRegions.CurrentRow.Cells["Code"].Value?.ToString();
            txtRegionName.Text = grdRegions.CurrentRow.Cells["Name"].Value?.ToString();
            chkRegionActive.Checked =
                grdRegions.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadBranchSelection()
        {
            if (grdBranches.CurrentRow == null)
                return;

            txtBranchCode.Text = grdBranches.CurrentRow.Cells["Code"].Value?.ToString();
            txtBranchName.Text = grdBranches.CurrentRow.Cells["Name"].Value?.ToString();
            chkBranchActive.Checked =
                grdBranches.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        #endregion

        #region CRUD – Organization

        private async Task AddOrgAsync()
        {
            if (!ValidateOrgInputs())
                return;

            SetBusy(true);

            try
            {
                using var db = CreateDbGlobal();

                var code = Norm(txtOrgCode.Text);
                if (await OrgCodeExistsAsync(db, code))
                {
                    MessageBox.Show("Organization code exists");
                    return;
                }

                var org = new DataOrganization
                {
                    Code = code,
                    Name = txtOrgName.Text.Trim(),
                    IsActive = chkOrgActive.Checked
                };

                db.Organizations.Add(org);
                await db.SaveChangesAsync();

                await LoadOrganizationsAsync();
                await LoadOrgComboForTenantsAsync();
                ClearOrgInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task UpdateOrgAsync()
        {
            if (grdOrgs.CurrentRow == null || !ValidateOrgInputs()) return;
            SetBusy(true);
            try
            {
                var id = (Guid)grdOrgs.CurrentRow.Cells["Id"].Value;
                using var db = CreateDbGlobal();
                var org = await db.Organizations.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == id); // added IgnoreQueryFilters
                if (org == null) return;
                var newCode = Norm(txtOrgCode.Text);
                if (org.Code != newCode && await OrgCodeExistsAsync(db, newCode)) { MessageBox.Show("Organization code exists"); return; }
                org.Code = newCode; org.Name = txtOrgName.Text.Trim(); org.IsActive = chkOrgActive.Checked;
                await db.SaveChangesAsync();
                await LoadOrganizationsAsync(); await LoadOrgComboForTenantsAsync(); ClearOrgInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { SetBusy(false); }
        }

        private void ClearOrgInputs()
        {
            txtOrgCode.Clear();
            txtOrgName.Clear();
            chkOrgActive.Checked = false;
            grdOrgs.ClearSelection();
        }

        #endregion

        #region CRUD – Tenant

        private async Task AddTenantAsync()
        {
            if (!ValidateTenantInputs())
                return;

            SetBusy(true);

            var orgId = (Guid)cboTenantOrg.SelectedValue;

            try
            {
                using var db = CreateDb(orgId);

                var code = Norm(txtTenantCode.Text);
                if (await TenantCodeExistsAsync(db, orgId, code))
                {
                    MessageBox.Show("Tenant code exists");
                    return;
                }

                var tenant = new DataTenant
                {
                    OrgId = orgId,
                    Code = code,
                    Name = txtTenantName.Text.Trim(),
                    IsActive = chkTenantActive.Checked
                };

                db.Tenants.Add(tenant);
                await db.SaveChangesAsync();

                await LoadTenantsAsync();
                ClearTenantInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task UpdateTenantAsync()
        {
            if (grdTenants.CurrentRow == null ||
                cboTenantOrg.SelectedValue is not Guid orgId ||
                !ValidateTenantInputs())
                return;

            SetBusy(true);

            try
            {
                var id = (Guid)grdTenants.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var tenant = await db.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == id); // added IgnoreQueryFilters
                if (tenant == null) return;
                var newCode = Norm(txtTenantCode.Text);
                if (tenant.Code != newCode && await TenantCodeExistsAsync(db, orgId, newCode)) { MessageBox.Show("Tenant code exists"); return; }
                tenant.Code = newCode; tenant.Name = txtTenantName.Text.Trim(); tenant.IsActive = chkTenantActive.Checked;
                await db.SaveChangesAsync(); await LoadTenantsAsync(); ClearTenantInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { SetBusy(false); }
        }

        private void ClearTenantInputs()
        {
            txtTenantCode.Clear();
            txtTenantName.Clear();
            chkTenantActive.Checked = false;
            grdTenants.ClearSelection();
            cboRegionTenant.SelectedIndex = -1;
        }

        #endregion

        #region CRUD – Region

        private async Task AddRegionAsync()
        {
            if (!ValidateRegionInputs())
                return;

            SetBusy(true);

            var tenantId = (Guid)cboRegionTenant.SelectedValue;
            var orgId = (Guid)cboTenantOrg.SelectedValue;

            try
            {
                using var db = CreateDb(orgId);

                var code = Norm(txtRegionCode.Text);
                if (await RegionCodeExistsAsync(db, tenantId, code))
                {
                    MessageBox.Show("Region code exists");
                    return;
                }

                var region = new DataRegion
                {
                    TenantId = tenantId,
                    Code = code,
                    Name = txtRegionName.Text.Trim(),
                    IsActive = chkRegionActive.Checked
                };

                db.Regions.Add(region);
                await db.SaveChangesAsync();

                await LoadRegionsAsync();
                ClearRegionInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task UpdateRegionAsync()
        {
            if (grdRegions.CurrentRow == null ||
                cboRegionTenant.SelectedValue is not Guid tenantId ||
                cboTenantOrg.SelectedValue is not Guid orgId ||
                !ValidateRegionInputs())
                return;

            SetBusy(true);

            try
            {
                var id = (Guid)grdRegions.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var region = await db.Regions.IgnoreQueryFilters().FirstOrDefaultAsync(r => r.Id == id); // added IgnoreQueryFilters
                if (region == null) return;
                var newCode = Norm(txtRegionCode.Text);
                if (region.Code != newCode && await RegionCodeExistsAsync(db, tenantId, newCode)) { MessageBox.Show("Region code exists"); return; }
                region.Code = newCode; region.Name = txtRegionName.Text.Trim(); region.IsActive = chkRegionActive.Checked;
                await db.SaveChangesAsync(); await LoadRegionsAsync(); ClearRegionInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { SetBusy(false); }
        }

        private void ClearRegionInputs()
        {
            txtRegionCode.Clear();
            txtRegionName.Clear();
            chkRegionActive.Checked = false;
            grdRegions.ClearSelection();
            cboBranchRegion.SelectedIndex = -1;
        }

        #endregion

        #region CRUD – Branch

        private async Task AddBranchAsync()
        {
            if (!ValidateBranchInputs())
                return;

            SetBusy(true);

            var regionId = (Guid)cboBranchRegion.SelectedValue;
            var orgId = (Guid)cboTenantOrg.SelectedValue;

            try
            {
                using var db = CreateDb(orgId);

                var code = Norm(txtBranchCode.Text);
                if (await BranchCodeExistsAsync(db, orgId, code))
                {
                    MessageBox.Show("Branch code exists");
                    return;
                }

                var branch = new DataBranch
                {
                    OrgId = orgId,
                    RegionId = regionId,
                    Code = code,
                    Name = txtBranchName.Text.Trim(),
                    IsActive = chkBranchActive.Checked
                };

                db.Branches.Add(branch);
                await db.SaveChangesAsync();

                await LoadBranchesAsync();
                ClearBranchInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async Task UpdateBranchAsync()
        {
            if (grdBranches.CurrentRow == null ||
                cboBranchRegion.SelectedValue is not Guid regionId ||
                cboTenantOrg.SelectedValue is not Guid orgId ||
                !ValidateBranchInputs())
                return;

            SetBusy(true);

            try
            {
                var id = (Guid)grdBranches.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var branch = await db.Branches.IgnoreQueryFilters().FirstOrDefaultAsync(b => b.Id == id); // added IgnoreQueryFilters
                if (branch == null) return;
                var newCode = Norm(txtBranchCode.Text);
                if (branch.Code != newCode && await BranchCodeExistsAsync(db, orgId, newCode)) { MessageBox.Show("Branch code exists"); return; }
                branch.Code = newCode; branch.Name = txtBranchName.Text.Trim(); branch.IsActive = chkBranchActive.Checked;
                await db.SaveChangesAsync(); await LoadBranchesAsync(); ClearBranchInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { SetBusy(false); }
        }

        private void ClearBranchInputs()
        {
            txtBranchCode.Clear();
            txtBranchName.Clear();
            chkBranchActive.Checked = false;
            grdBranches.ClearSelection();
        }

        #endregion

        private void SetColumnsSortable(DataGridView grid)
        { foreach (DataGridViewColumn c in grid.Columns) c.SortMode = DataGridViewColumnSortMode.Programmatic; }

        private void SortGrid(BindingSource source, DataGridView grid, int colIndex)
        {
            if (source.DataSource is not BindingList<ItemVM> list) return;
            var column = grid.Columns[colIndex];
            var name = column.DataPropertyName == string.Empty ? column.Name : column.DataPropertyName;
            var ascending = !_columnSortDirections.TryGetValue(name, out var current) || !current;
            IEnumerable<ItemVM> items = list;
            items = name switch
            {
                nameof(ItemVM.Id) => ascending ? items.OrderBy(i=>i.Id) : items.OrderByDescending(i=>i.Id),
                nameof(ItemVM.Code) => ascending ? items.OrderBy(i=>i.Code) : items.OrderByDescending(i=>i.Code),
                nameof(ItemVM.Name) => ascending ? items.OrderBy(i=>i.Name) : items.OrderByDescending(i=>i.Name),
                nameof(ItemVM.IsActive) => ascending ? items.OrderBy(i=>i.IsActive) : items.OrderByDescending(i=>i.IsActive),
                _ => items
            };
            source.DataSource = new BindingList<ItemVM>(items.ToList());
            source.ResetBindings(false);
            _columnSortDirections[name] = ascending;
        }

    }
}
