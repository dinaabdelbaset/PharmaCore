using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
// Avoid Region clash
using DataOrganization = PharmaCore.Infrastructure.Data.Organization;
using DataTenant = PharmaCore.Infrastructure.Data.Tenant;
using DataRegion = PharmaCore.Infrastructure.Data.Region;
using DataBranch = PharmaCore.Infrastructure.Data.Branch;

namespace PharmaCore.ActivationManager
{
    public partial class MasterDataManager : Form
    {
        private string _connectionString => ControlDbConnection.Get();

        public MasterDataManager()
        {
            InitializeComponent();
            Load += MasterDataManager_Load;

            grdOrgs.CellClick += (_, __) => LoadOrgSelection();
            grdTenants.CellClick += (_, __) => LoadTenantSelection();
            grdRegions.CellClick += (_, __) => LoadRegionSelection();
            grdBranches.CellClick += (_, __) => LoadBranchSelection();

            cboTenantOrg.SelectedIndexChanged += async (_, __) => await LoadTenantsAsync();
            cboRegionTenant.SelectedIndexChanged += async (_, __) => await LoadRegionsAsync();
            cboBranchRegion.SelectedIndexChanged += async (_, __) => await LoadBranchesAsync();

            btnOrgAdd.Click += async (_, __) => await AddOrgAsync();
            btnOrgUpdate.Click += async (_, __) => await UpdateOrgAsync();
            btnOrgClear.Click += (_, __) => ClearOrgInputs();

            btnTenantAdd.Click += async (_, __) => await AddTenantAsync();
            btnTenantUpdate.Click += async (_, __) => await UpdateTenantAsync();
            btnTenantClear.Click += (_, __) => ClearTenantInputs();

            btnRegionAdd.Click += async (_, __) => await AddRegionAsync();
            btnRegionUpdate.Click += async (_, __) => await UpdateRegionAsync();
            btnRegionClear.Click += (_, __) => ClearRegionInputs();

            btnBranchAdd.Click += async (_, __) => await AddBranchAsync();
            btnBranchUpdate.Click += async (_, __) => await UpdateBranchAsync();
            btnBranchClear.Click += (_, __) => ClearBranchInputs();
        }

        private AppDbContext CreateDb(Guid orgId) =>
            new(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options,
                new MasterTenantContext(orgId));

        private AppDbContext CreateDbGlobal() =>
            new(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(_connectionString).Options,
                new MasterTenantContext(Guid.Empty));

        private async void MasterDataManager_Load(object? sender, EventArgs e)
        {
            await LoadOrganizationsAsync();
            await LoadOrgComboForTenantsAsync();
        }

        private async Task LoadOrganizationsAsync()
        {
            try
            {
                using var db = CreateDbGlobal();
                // Ignore filter to list all Orgs if needed:
                var data = await db.Organizations
                    .IgnoreQueryFilters()
                    .OrderBy(o => o.Name)
                    .Select(o => new { o.Id, o.Code, o.Name, o.IsActive })
                    .ToListAsync();
                grdOrgs.DataSource = data;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task LoadOrgComboForTenantsAsync()
        {
            try
            {
                using var db = CreateDbGlobal();
                var orgs = await db.Organizations
                    .IgnoreQueryFilters()
                    .Where(o => o.IsActive)
                    .OrderBy(o => o.Name)
                    .ToListAsync();
                cboTenantOrg.DataSource = orgs;
                cboTenantOrg.DisplayMember = "Name";
                cboTenantOrg.ValueMember = "Id";
                cboTenantOrg.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task LoadTenantsAsync()
        {
            grdTenants.DataSource = null;
            cboRegionTenant.DataSource = null;
            grdRegions.DataSource = null;
            cboBranchRegion.DataSource = null;
            grdBranches.DataSource = null;
            if (cboTenantOrg.SelectedValue is not Guid orgId) return;

            try
            {
                using var db = CreateDb(orgId);
                var tenants = await db.Tenants
                    .IgnoreQueryFilters()
                    .Where(t => t.OrgId == orgId && t.IsActive)
                    .OrderBy(t => t.Name)
                    .Select(t => new { t.Id, t.Code, t.Name, t.IsActive })
                    .ToListAsync();
                grdTenants.DataSource = tenants;

                cboRegionTenant.DataSource = tenants;
                cboRegionTenant.DisplayMember = "Name";
                cboRegionTenant.ValueMember = "Id";
                cboRegionTenant.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task LoadRegionsAsync()
        {
            grdRegions.DataSource = null;
            cboBranchRegion.DataSource = null;
            grdBranches.DataSource = null;

            if (cboRegionTenant.SelectedValue is not Guid tenantId) return;
            if (cboTenantOrg.SelectedValue is not Guid orgId) return;

            try
            {
                using var db = CreateDb(orgId);
                var regions = await db.Regions
                    .IgnoreQueryFilters()
                    .Where(r => r.TenantId == tenantId && r.IsActive)
                    .OrderBy(r => r.Name)
                    .Select(r => new { r.Id, r.Code, r.Name, r.IsActive })
                    .ToListAsync();
                grdRegions.DataSource = regions;

                cboBranchRegion.DataSource = regions;
                cboBranchRegion.DisplayMember = "Name";
                cboBranchRegion.ValueMember = "Id";
                cboBranchRegion.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task LoadBranchesAsync()
        {
            grdBranches.DataSource = null;
            if (cboBranchRegion.SelectedValue is not Guid regionId) return;
            if (cboTenantOrg.SelectedValue is not Guid orgId) return;

            try
            {
                using var db = CreateDb(orgId);
                var branches = await db.Branches
                    .IgnoreQueryFilters()
                    .Where(b => b.RegionId == regionId && b.IsActive)
                    .OrderBy(b => b.Name)
                    .Select(b => new { b.Id, b.Code, b.Name, b.IsActive })
                    .ToListAsync();
                grdBranches.DataSource = branches;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadOrgSelection()
        {
            if (grdOrgs.CurrentRow == null) return;
            txtOrgCode.Text = grdOrgs.CurrentRow.Cells["Code"].Value?.ToString();
            txtOrgName.Text = grdOrgs.CurrentRow.Cells["Name"].Value?.ToString();
            chkOrgActive.Checked = grdOrgs.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadTenantSelection()
        {
            if (grdTenants.CurrentRow == null) return;
            txtTenantCode.Text = grdTenants.CurrentRow.Cells["Code"].Value?.ToString();
            txtTenantName.Text = grdTenants.CurrentRow.Cells["Name"].Value?.ToString();
            chkTenantActive.Checked = grdTenants.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadRegionSelection()
        {
            if (grdRegions.CurrentRow == null) return;
            txtRegionCode.Text = grdRegions.CurrentRow.Cells["Code"].Value?.ToString();
            txtRegionName.Text = grdRegions.CurrentRow.Cells["Name"].Value?.ToString();
            chkRegionActive.Checked = grdRegions.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private void LoadBranchSelection()
        {
            if (grdBranches.CurrentRow == null) return;
            txtBranchCode.Text = grdBranches.CurrentRow.Cells["Code"].Value?.ToString();
            txtBranchName.Text = grdBranches.CurrentRow.Cells["Name"].Value?.ToString();
            chkBranchActive.Checked = grdBranches.CurrentRow.Cells["IsActive"].Value is bool b && b;
        }

        private async Task AddOrgAsync()
        {
            try
            {
                using var db = CreateDbGlobal();
                var org = new DataOrganization
                {
                    Code = txtOrgCode.Text.Trim(),
                    Name = txtOrgName.Text.Trim(),
                    IsActive = chkOrgActive.Checked
                };
                db.Organizations.Add(org);
                await db.SaveChangesAsync();
                await LoadOrganizationsAsync();
                ClearOrgInputs();
                await LoadOrgComboForTenantsAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateOrgAsync()
        {
            if (grdOrgs.CurrentRow == null) return;
            try
            {
                var id = (Guid)grdOrgs.CurrentRow.Cells["Id"].Value;
                using var db = CreateDbGlobal();
                var org = await db.Organizations.IgnoreQueryFilters().FirstOrDefaultAsync(o => o.Id == id);
                if (org == null) return;
                org.Code = txtOrgCode.Text.Trim();
                org.Name = txtOrgName.Text.Trim();
                org.IsActive = chkOrgActive.Checked;
                await db.SaveChangesAsync();
                await LoadOrganizationsAsync();
                await LoadOrgComboForTenantsAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ClearOrgInputs()
        {
            txtOrgCode.Clear();
            txtOrgName.Clear();
            chkOrgActive.Checked = true;
        }

        private async Task AddTenantAsync()
        {
            if (cboTenantOrg.SelectedValue is not Guid orgId) { MessageBox.Show("Select Organization first"); return; }
            try
            {
                using var db = CreateDb(orgId);
                var tenant = new DataTenant
                {
                    OrgId = orgId,
                    Code = txtTenantCode.Text.Trim(),
                    Name = txtTenantName.Text.Trim(),
                    IsActive = chkTenantActive.Checked
                };
                db.Tenants.Add(tenant);
                await db.SaveChangesAsync();
                await LoadTenantsAsync();
                ClearTenantInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateTenantAsync()
        {
            if (grdTenants.CurrentRow == null || cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                var id = (Guid)grdTenants.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var tenant = await db.Tenants.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == id);
                if (tenant == null) return;
                tenant.Code = txtTenantCode.Text.Trim();
                tenant.Name = txtTenantName.Text.Trim();
                tenant.IsActive = chkTenantActive.Checked;
                await db.SaveChangesAsync();
                await LoadTenantsAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ClearTenantInputs()
        {
            txtTenantCode.Clear();
            txtTenantName.Clear();
            chkTenantActive.Checked = true;
        }

        private async Task AddRegionAsync()
        {
            if (cboRegionTenant.SelectedValue is not Guid tenantId || cboTenantOrg.SelectedValue is not Guid orgId)
            { MessageBox.Show("Select Tenant"); return; }

            try
            {
                using var db = CreateDb(orgId);
                var region = new DataRegion
                {
                    TenantId = tenantId,
                    Code = txtRegionCode.Text.Trim(),
                    Name = txtRegionName.Text.Trim(),
                    IsActive = chkRegionActive.Checked
                };
                db.Regions.Add(region);
                await db.SaveChangesAsync();
                await LoadRegionsAsync();
                ClearRegionInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateRegionAsync()
        {
            if (grdRegions.CurrentRow == null || cboRegionTenant.SelectedValue is not Guid tenantId || cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                var id = (Guid)grdRegions.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var region = await db.Regions.IgnoreQueryFilters().FirstOrDefaultAsync(r => r.Id == id);
                if (region == null) return;
                region.Code = txtRegionCode.Text.Trim();
                region.Name = txtRegionName.Text.Trim();
                region.IsActive = chkRegionActive.Checked;
                await db.SaveChangesAsync();
                await LoadRegionsAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ClearRegionInputs()
        {
            txtRegionCode.Clear();
            txtRegionName.Clear();
            chkRegionActive.Checked = true;
        }

        private async Task AddBranchAsync()
        {
            if (cboBranchRegion.SelectedValue is not Guid regionId || cboTenantOrg.SelectedValue is not Guid orgId)
            { MessageBox.Show("Select Region"); return; }

            try
            {
                using var db = CreateDb(orgId);
                var branch = new DataBranch
                {
                    OrgId = orgId,
                    RegionId = regionId,
                    Code = txtBranchCode.Text.Trim(),
                    Name = txtBranchName.Text.Trim(),
                    IsActive = chkBranchActive.Checked
                };
                db.Branches.Add(branch);
                await db.SaveChangesAsync();
                await LoadBranchesAsync();
                ClearBranchInputs();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async Task UpdateBranchAsync()
        {
            if (grdBranches.CurrentRow == null || cboBranchRegion.SelectedValue is not Guid regionId || cboTenantOrg.SelectedValue is not Guid orgId) return;
            try
            {
                var id = (Guid)grdBranches.CurrentRow.Cells["Id"].Value;
                using var db = CreateDb(orgId);
                var branch = await db.Branches.IgnoreQueryFilters().FirstOrDefaultAsync(b => b.Id == id);
                if (branch == null) return;
                branch.Code = txtBranchCode.Text.Trim();
                branch.Name = txtBranchName.Text.Trim();
                branch.IsActive = chkBranchActive.Checked;
                await db.SaveChangesAsync();
                await LoadBranchesAsync();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ClearBranchInputs()
        {
            txtBranchCode.Clear();
            txtBranchName.Clear();
            chkBranchActive.Checked = true;
        }
    }
}