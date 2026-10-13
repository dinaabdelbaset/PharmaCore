using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ManagerSetup : DevExpress.XtraEditors.XtraForm
    {
        private Guid _orgId;
        private Guid _branchId;
        private Guid? _tenantId;
        private Guid? _regionId;

        public bool ManagerCreated { get; private set; }

        public Frm_ManagerSetup()
        {
            InitializeComponent();
            var meta = Services.Security.ActivationMetadataStorage.Load();
            if (meta != null)
            {
                _orgId = meta.OrgId;
                _branchId = meta.BranchId;
                _tenantId = meta.TenantId;
                _regionId = meta.RegionId;
            }
            btnRegister.Click += BtnRegister_Click;
            txtConfirmPin.KeyPress += (s, e) => { if (e.KeyChar == '\r') BtnRegister_Click(s, e); };
        }

        private void UpdateCodePreview()
        {
            int digits = (int)nudDigits.Value;
            string first = "1" + new string('0', digits - 1);
            string second = (int.Parse(first) + 1).ToString();
            lblCodePreview.Text = $"مثال: أول كود = {first} ← ثم {second} ← وهكذا (من 2 إلى 7 خانات)";
        }

        private sealed record BootstrapRequest(
            string FullName, string Pin, string? EmployeeCode,
            Guid OrgId, Guid BranchId, Guid? TenantId, Guid? RegionId);

        private sealed record BootstrapResponse
        {
            public Guid Id { get; init; }
            public string EmployeeCode { get; init; } = string.Empty;
            public string FullName { get; init; } = string.Empty;
            public string Role { get; init; } = string.Empty;
        }

        private async void BtnRegister_Click(object? sender, EventArgs e)
        {
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168); // red

            // Validate
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                lblStatus.Text = "يرجى إدخال الاسم بالكامل";
                txtFullName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(txtPin.Text))
            {
                lblStatus.Text = "يرجى إدخال الرقم السري";
                txtPin.Focus(); return;
            }
            if (txtPin.Text.Length < 4)
            {
                lblStatus.Text = "الرقم السري يجب أن يكون 4 أحرف على الأقل";
                txtPin.Focus(); return;
            }
            if (txtPin.Text != txtConfirmPin.Text)
            {
                lblStatus.Text = "الرقم السري غير متطابق";
                txtConfirmPin.Focus(); return;
            }

            btnRegister.Enabled = false;
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(166, 173, 200);
            lblStatus.Text = "جارٍ التسجيل...";

            try
            {
                string baseUrl;
                try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
                catch
                {
                    try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                    catch (Exception ex2) { lblStatus.Text = ex2.Message; btnRegister.Enabled = true; return; }
                }

                var _sslHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
                using var http = new HttpClient(_sslHandler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(30) };
                var branchToken = Properties.Settings.Default.ApiToken;
                if (!string.IsNullOrWhiteSpace(branchToken))
                    http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", branchToken);

                int digits = (int)nudDigits.Value;
                string firstCode = "1" + new string('0', digits - 1);

                var req = new BootstrapRequest(
                    txtFullName.Text.Trim(),
                    txtPin.Text,
                    firstCode,
                    _orgId, _branchId, _tenantId, _regionId);

                var resp = await http.PostAsJsonAsync("api/employees/bootstrap", req);

                if (!resp.IsSuccessStatusCode)
                {
                    var errorBody = await resp.Content.ReadAsStringAsync();
                    lblStatus.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168);
                    lblStatus.Text = $"فشل: {resp.StatusCode} — {errorBody}";
                    btnRegister.Enabled = true;
                    return;
                }

                var dto = await resp.Content.ReadFromJsonAsync<BootstrapResponse>();

                lblStatus.ForeColor = System.Drawing.Color.FromArgb(166, 227, 161); // green
                lblStatus.Text = $"تم تسجيل المدير: {dto?.EmployeeCode ?? "OK"} ✓";

                ManagerCreated = true;
                await Task.Delay(1500);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168);
                lblStatus.Text = ex.Message;
                btnRegister.Enabled = true;
            }
        }
    }
}
