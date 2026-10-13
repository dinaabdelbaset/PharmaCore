using DevExpress.XtraEditors;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_EmployeeManagement : XtraForm
    {
        private Guid _orgId;
        private Guid _branchId;
        private Guid? _tenantId;
        private Guid? _regionId;
        private List<EmployeeDto> _allEmployees = new();
        private List<RoleItem> _roles = new();
        private Guid _editingId = Guid.Empty;

        public Frm_EmployeeManagement()
        {
            InitializeComponent();
            var meta = Services.Security.ActivationMetadataStorage.Load();
            if (meta != null) { _orgId = meta.OrgId; _branchId = meta.BranchId; _tenantId = meta.TenantId; _regionId = meta.RegionId; }

            grid.SelectionChanged += (_, __) => LoadSelection();
            txtSearch.TextChanged += (_, __) => ApplySearch();
            btnNew.Click += (_, __) => PrepareNew();
            btnSave.Click += async (_, __) => await CreateAsync();
            btnUpdate.Click += async (_, __) => await UpdateAsync();
            btnResetPin.Click += async (_, __) => await ResetPinAsync();
            btnDelete.Click += async (_, __) => await DeleteAsync();
            Shown += async (_, __) => { await LoadRolesAsync(); await RefreshAsync(); };
            PrepareNew();
        }

        // ── DTOs ──────────────────────────────────────────────

        private sealed class EmployeeDto
        {
            [JsonPropertyName("id")] public Guid Id { get; set; }
            [JsonPropertyName("employeeCode")] public string EmployeeCode { get; set; } = "";
            [JsonPropertyName("fullName")] public string FullName { get; set; } = "";
            [JsonPropertyName("jobTitle")] public string? JobTitle { get; set; }
            [JsonPropertyName("grade")] public string? Grade { get; set; }
            [JsonPropertyName("role")] public string Role { get; set; } = "";
            [JsonPropertyName("roleId")] public Guid? RoleId { get; set; }
            [JsonPropertyName("roleDescription")] public string? RoleDescription { get; set; }
            [JsonPropertyName("isActive")] public bool IsActive { get; set; }
            [JsonPropertyName("orgId")] public Guid OrgId { get; set; }
            [JsonPropertyName("branchId")] public Guid? BranchId { get; set; }
        }

        private sealed class RoleItem
        {
            [JsonPropertyName("id")] public Guid Id { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; } = "";
            [JsonPropertyName("description")] public string? Description { get; set; }
            [JsonPropertyName("display")] public string Display { get; set; } = "";
            [JsonPropertyName("sortOrder")] public int SortOrder { get; set; }

            public override string ToString() => Display;
        }

        private sealed record CreateReq(
            string FullName, string? EmployeeCode, string Pin,
            string Role, Guid? RoleId, string? JobTitle, string? Grade,
            Guid OrgId, Guid? TenantId, Guid? RegionId, Guid? BranchId);

        private sealed record UpdateReq(string FullName, string Role, Guid? RoleId,
            string? JobTitle, string? Grade, bool IsActive);
        private sealed record ResetPinReq(string NewPin);

        // ── HTTP Helper ───────────────────────────────────────

        private HttpClient CreateHttpClient()
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch { baseUrl = Properties.Settings.Default.ApiBase?.Trim() ?? "http://localhost:5001/"; }
            }
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            var http = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
            var token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return http;
        }

        // ── Role Loading ──────────────────────────────────────

        private async Task LoadRolesAsync()
        {
            try
            {
                using var http = CreateHttpClient();

                // Seed defaults first (no-op if already seeded)
                try { await http.PostAsync("api/employee-roles/seed-defaults", null); } catch { }

                // Load flat list for dropdown
                var roles = await http.GetFromJsonAsync<List<RoleItem>>("api/employee-roles/flat");
                _roles = roles ?? new();

                // ── SECURITY: Discord/SAP Role Hierarchy Filter ──
                if (!CurrentEmployeeSession.IsSystemRole)
                {
                    // Find the current user's SortOrder based on their role name
                    var myRole = _roles.FirstOrDefault(r => r.Name.Equals(CurrentEmployeeSession.Role, StringComparison.OrdinalIgnoreCase));
                    if (myRole != null)
                    {
                        // Exclude roles that are higher or equal to my rank (Smaller SortOrder = Higher Rank)
                        _roles = _roles.Where(r => r.SortOrder > myRole.SortOrder).ToList();
                    }
                }

                cmbRole.Items.Clear();
                foreach (var r in _roles)
                    cmbRole.Items.Add(r);

                if (cmbRole.Items.Count > 0)
                    cmbRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                SetStatus($"فشل تحميل الرتب: {ex.Message}", true);
            }
        }

        private Guid? GetSelectedRoleId()
        {
            if (cmbRole.SelectedItem is RoleItem role)
                return role.Id;
            return null;
        }

        private string GetSelectedRoleName()
        {
            if (cmbRole.SelectedItem is RoleItem role)
                return role.Name;
            return "Staff";
        }

        private void SelectRoleById(Guid? roleId)
        {
            if (roleId == null) { if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0; return; }
            for (int i = 0; i < cmbRole.Items.Count; i++)
            {
                if (cmbRole.Items[i] is RoleItem r && r.Id == roleId.Value)
                {
                    cmbRole.SelectedIndex = i;
                    return;
                }
            }
            // Fallback: select first
            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
        }

        private void SelectRoleByName(string? roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) { if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0; return; }
            for (int i = 0; i < cmbRole.Items.Count; i++)
            {
                if (cmbRole.Items[i] is RoleItem r && r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                {
                    cmbRole.SelectedIndex = i;
                    return;
                }
            }
            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
        }

        // ── Data Loading ──────────────────────────────────────

        private async Task RefreshAsync()
        {
            try
            {
                using var http = CreateHttpClient();
                var response = await http.GetAsync("api/employees");
                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"API Error: {response.StatusCode}\n{err}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus($"خطأ: {response.StatusCode}", true);
                    return;
                }
                var list = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
                _allEmployees = list ?? new();
                ApplySearch();
                lblCount.Text = $"إجمالي الموظفين: {_allEmployees.Count}";
                SetStatus("تم التحميل", false);
            }
            catch (Exception ex) { SetStatus($"خطأ: {ex.Message}", true); }
        }

        private void ApplySearch()
        {
            var q = txtSearch.Text.Trim().ToLowerInvariant();
            var filtered = string.IsNullOrEmpty(q)
                ? _allEmployees
                : _allEmployees.Where(e =>
                    e.FullName.ToLowerInvariant().Contains(q) ||
                    e.EmployeeCode.ToLowerInvariant().Contains(q) ||
                    e.Role.ToLowerInvariant().Contains(q)).ToList();

            grid.DataSource = null;
            grid.DataSource = filtered;

            if (grid.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (col.Name is "Id" or "OrgId" or "BranchId" or "JobTitle" or "Grade" or "RoleId" or "RoleDescription")
                        col.Visible = false;
                    else if (col.Name == "EmployeeCode") col.HeaderText = "الكود";
                    else if (col.Name == "FullName") col.HeaderText = "الاسم";
                    else if (col.Name == "Role") col.HeaderText = "الرتبة";
                    else if (col.Name == "IsActive") col.HeaderText = "نشط";
                }
            }
        }

        // ── CRUD ──────────────────────────────────────────────

        private async Task CreateAsync()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { SetStatus("ادخل الاسم", true); return; }
            if (string.IsNullOrWhiteSpace(txtPin.Text)) { SetStatus("ادخل الرقم السري", true); return; }
            if (txtPin.Text.Length < 4) { SetStatus("PIN يجب أن يكون 4 أحرف على الأقل", true); return; }
            if (cmbRole.SelectedItem == null) { SetStatus("اختر الرتبة", true); return; }

            try
            {
                using var http = CreateHttpClient();
                var req = new CreateReq(
                    txtName.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtCode.Text) ? null : txtCode.Text.Trim(),
                    txtPin.Text,
                    GetSelectedRoleName(),
                    GetSelectedRoleId(),
                    string.IsNullOrWhiteSpace(txtJob.Text) ? null : txtJob.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtGrade.Text) ? null : txtGrade.Text.Trim(),
                    _orgId, _tenantId, _regionId, _branchId);

                var resp = await http.PostAsJsonAsync("api/employees", req);
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    SetStatus($"فشل الإضافة: {err}", true); return;
                }

                SetStatus("تم الإضافة بنجاح ✓", false);
                PrepareNew();
                await RefreshAsync();
            }
            catch (Exception ex) { SetStatus(ex.Message, true); }
        }

        private async Task UpdateAsync()
        {
            if (_editingId == Guid.Empty) { SetStatus("حدد موظف أولاً", true); return; }
            if (string.IsNullOrWhiteSpace(txtName.Text)) { SetStatus("ادخل الاسم", true); return; }

            try
            {
                using var http = CreateHttpClient();
                var req = new UpdateReq(
                    txtName.Text.Trim(),
                    GetSelectedRoleName(),
                    GetSelectedRoleId(),
                    string.IsNullOrWhiteSpace(txtJob.Text) ? null : txtJob.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtGrade.Text) ? null : txtGrade.Text.Trim(),
                    chkActive.Checked);

                var resp = await http.PutAsJsonAsync($"api/employees/{_editingId}", req);
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    SetStatus($"فشل التعديل: {err}", true); return;
                }

                SetStatus("تم التعديل ✓", false);
                await RefreshAsync();
            }
            catch (Exception ex) { SetStatus(ex.Message, true); }
        }

        private async Task ResetPinAsync()
        {
            if (_editingId == Guid.Empty) { SetStatus("حدد موظف أولاً", true); return; }
            if (string.IsNullOrWhiteSpace(txtPin.Text)) { SetStatus("ادخل PIN جديد", true); return; }
            if (txtPin.Text.Length < 4) { SetStatus("PIN يجب أن يكون 4 أحرف على الأقل", true); return; }

            try
            {
                using var http = CreateHttpClient();
                var resp = await http.PutAsJsonAsync($"api/employees/{_editingId}/pin", new ResetPinReq(txtPin.Text));
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    SetStatus($"فشل تغيير PIN: {err}", true); return;
                }

                txtPin.Clear();
                SetStatus("تم تغيير الرقم السري ✓", false);
            }
            catch (Exception ex) { SetStatus(ex.Message, true); }
        }

        private async Task DeleteAsync()
        {
            if (_editingId == Guid.Empty) { SetStatus("حدد موظف أولاً", true); return; }
            if (MessageBox.Show("هل أنت متأكد من حذف هذا الموظف؟", "تأكيد الحذف",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                using var http = CreateHttpClient();
                var resp = await http.DeleteAsync($"api/employees/{_editingId}");
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    SetStatus($"فشل الحذف: {err}", true); return;
                }

                SetStatus("تم الحذف ✓", false);
                PrepareNew();
                await RefreshAsync();
            }
            catch (Exception ex) { SetStatus(ex.Message, true); }
        }

        // ── UI Helpers ────────────────────────────────────────

        private void PrepareNew()
        {
            _editingId = Guid.Empty;
            txtCode.Clear(); txtCode.ReadOnly = false;
            txtName.Clear(); txtJob.Clear(); txtGrade.Clear();
            txtPin.Clear(); chkActive.Checked = true;
            // Select a sensible default role (e.g. Staff/Cashier)
            SelectRoleByName("Cashier");
            btnSave.Enabled = true; btnUpdate.Enabled = false;
            btnResetPin.Enabled = false; btnDelete.Enabled = false;
            grid.ClearSelection();
        }

        private void LoadSelection()
        {
            if (grid.CurrentRow?.DataBoundItem is EmployeeDto emp)
            {
                _editingId = emp.Id;
                txtCode.Text = emp.EmployeeCode; txtCode.ReadOnly = true;
                txtName.Text = emp.FullName;
                // Select role by RoleId if available, otherwise by name
                if (emp.RoleId.HasValue)
                    SelectRoleById(emp.RoleId);
                else
                    SelectRoleByName(emp.Role);
                txtJob.Text = emp.JobTitle ?? "";
                txtGrade.Text = emp.Grade ?? "";
                chkActive.Checked = emp.IsActive;
                txtPin.Clear();

                btnSave.Enabled = false; btnUpdate.Enabled = true;
                btnResetPin.Enabled = true; btnDelete.Enabled = true;
            }
        }

        private void SetStatus(string text, bool isError)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = isError
                ? System.Drawing.Color.FromArgb(243, 139, 168)
                : System.Drawing.Color.FromArgb(166, 227, 161);
        }
    }
}
