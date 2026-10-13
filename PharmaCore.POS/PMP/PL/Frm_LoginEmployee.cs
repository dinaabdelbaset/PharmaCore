using DevExpress.XtraEditors;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace PharmaCore.POS.PL;

public partial class Frm_LoginEmployee : XtraForm
{
    private Guid _orgId = Guid.Empty;
    private Guid _branchId = Guid.Empty;

    public Frm_LoginEmployee()
    {
        InitializeComponent();
        btnLogin.Click += btnLogin_Click;
        btnExit.Click += (_, __) => Close();
        simpleButton1.Click += (_, __) => { simpleButton2.BringToFront(); txtPassword.UseSystemPasswordChar = false; };
        simpleButton2.Click += (_, __) => { simpleButton1.BringToFront(); txtPassword.UseSystemPasswordChar = true; };
        txtPassword.UseSystemPasswordChar = true;
        txtPassword.KeyPress += (s, e) => { if (e.KeyChar == '\r') btnLogin_Click(s, e); };
    }

    private async void Frm_LoginEmployee_Load(object sender, EventArgs e)
    {
        var meta = Services.Security.ActivationMetadataStorage.Load();
        if (meta != null) { _orgId = meta.OrgId; _branchId = meta.BranchId; }

        // Check if any employees exist — if not, show first-time manager setup
        await CheckFirstTimeSetupAsync();
    }

    private sealed record ExistsResponse { public bool Exists { get; init; } }

    private async Task CheckFirstTimeSetupAsync()
    {
        bool employeesExist = true; // default: assume they exist (safe fallback)

        // ── Try API check first ──
        try
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch { throw; }
            }
            var _handler1 = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            using var http = new HttpClient(_handler1) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(10) };
            var branchToken = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(branchToken))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", branchToken);

            var response = await http.GetAsync("api/employees/exists");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                // Simple JSON check (avoid deserialization issues)
                employeesExist = json.Contains("true", StringComparison.OrdinalIgnoreCase);
            }
        }
        catch
        {
            // API not reachable → try local DB check
            try
            {
                employeesExist = await Services.EmployeeAdminDb.AnyEmployeesExistAsync();
            }
            catch { /* local DB also failed → skip check */ }
        }

        if (!employeesExist)
        {
            using var setup = new Frm_ManagerSetup();
            if (setup.ShowDialog(this) != DialogResult.OK || !setup.ManagerCreated)
            {
                Close();
                return;
            }
        }
    }

    private sealed record EmployeeLoginRequest(string EmployeeCode, string Pin, Guid BranchId, Guid WorkstationSessionId);

    private sealed record EmployeeLoginResponse
    {
        public bool Ok { get; init; }
        public string Error { get; init; } = string.Empty;
        public string? Token { get; init; }
        public Guid? EmployeeId { get; init; }
        public string? EmployeeCode { get; init; }
        public string? FullName { get; init; }
        public Guid? BranchId { get; init; }
        public string? Role { get; init; }
        public bool IsSystemRole { get; init; }
        public string[]? Permissions { get; init; }
    }

    private async void btnLogin_Click(object? sender, EventArgs e)
    {
        var code = txtUser.Text.Trim();
        var pin = txtPassword.Text.Trim();

        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(pin))
        {
            XtraMessageBox.Show("ادخل كود الموظف وكلمة السر", "تنبيه");
            return;
        }

        // POS is a thin client: authenticate via API
        try { Services.Security.BranchContext.ValidateOrThrow(); }
        catch (Exception ex) { XtraMessageBox.Show(ex.Message, "تفعيل"); return; }

        string baseUrl;
        try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
        catch
        {
            try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
            catch (Exception ex2) { XtraMessageBox.Show(ex2.Message, "خطأ"); return; }
        }

        try
        {
            var _handler2 = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            using var http = new HttpClient(_handler2) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
            var branchToken = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(branchToken))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", branchToken);

            var workstationSessionId = Guid.NewGuid(); // placeholder until API returns real session id
            var req = new EmployeeLoginRequest(code, pin, _branchId, workstationSessionId);

            var resp = await http.PostAsJsonAsync("api/employee-login", req);
            if (!resp.IsSuccessStatusCode)
            {
                XtraMessageBox.Show("فشل الاتصال بالـAPI", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dto = await resp.Content.ReadFromJsonAsync<EmployeeLoginResponse>();
            if (dto == null || !dto.Ok)
            {
                // Log failed login attempt (before session is set)
                AuditLogger.Log("LoginFailed", "Auth", "Frm_LoginEmployee",
                    $"فشل تسجيل دخول — الكود: {code}, السبب: {dto?.Error ?? "فشل غير معروف"}", severity: 1);
                XtraMessageBox.Show(dto?.Error ?? "فشل تسجيل الدخول", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Store employee token and session
            Properties.Settings.Default.EmployeeToken = dto.Token ?? string.Empty;
            Properties.Settings.Default.Save();

            // Populate global employee session context
            CurrentEmployeeSession.Set(new EmployeeAuthDb.Result
            {
                Ok = true,
                EmployeeId = dto.EmployeeId ?? Guid.Empty,
                EmployeeCode = dto.EmployeeCode ?? code,
                FullName = dto.FullName ?? string.Empty,
                Role = dto.Role ?? "Staff",
                IsSystemRole = dto.IsSystemRole,
                OrgId = _orgId,
                BranchId = _branchId,
            });

            // Populate permissions from API response
            CurrentEmployeeSession.SetPermissions(dto.Permissions);

            // ── BlackBox: Log successful login + start session ──
            AuditLogger.Log("Login", "Auth", "Frm_LoginEmployee",
                $"تسجيل دخول ناجح — {dto.FullName} ({dto.EmployeeCode})");
            AuditLogger.StartSession();

            Hide();
            try { using var main = new Form1(); main.ShowDialog(); }
            finally { Close(); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message, "خطأ");
        }
    }
}
