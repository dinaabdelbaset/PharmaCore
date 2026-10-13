using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using Npgsql; // added for branch code query

namespace PharmaCore.POS.PL
{
    public partial class Frm_ServerSetting : XtraForm
    {
        // SaaS: root domain + per-tenant subdomain
        private const string RootDomainSettingKey = "SaaSRootDomain";
        private const string DeviceTokenFileName = "pharmacore_device_token.txt";
        private static readonly string DeviceTokenFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DeviceTokenFileName);
        private HttpClient? _http;
        private Label? _statusLabel; // added at runtime
        private readonly IConnectionStringProvider _csProvider = new ConnectionStringProvider();

        public Frm_ServerSetting()
        {
            InitializeComponent();
            InjectStatusLabel();
            LoadApiSettings();
            txtBranchId.KeyPress += (s, e) => {
                if (char.IsControl(e.KeyChar)) return;
                if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-') e.Handled = true;
            };
            txtBranchId.Properties.ContextMenuStrip = new ContextMenuStrip();
            txtBranchId.Properties.ContextMenuStrip.Items.Add("Paste", null, (_, __) => { try { string clip = string.Empty; if (InvokeRequired) Invoke(() => clip = Clipboard.GetText().Trim()); else clip = Clipboard.GetText().Trim(); if (ActivationDb.IsFormatValid(clip)) txtBranchId.Text = clip; } catch { } });
            btnExport.Click += (_, __) => { using var sfd = new SaveFileDialog{ Filter = "PharmaCore Settings|*.phcfgx" }; if(sfd.ShowDialog()==DialogResult.OK){ var pwd = PromptPassword(); if(!string.IsNullOrEmpty(pwd)){ SettingsTransferService.Export(sfd.FileName, pwd); XtraMessageBox.Show("تم التصدير"); } } };
            btnImport.Click += (_, __) => { using var ofd = new OpenFileDialog{ Filter = "PharmaCore Settings|*.phcfgx" }; if(ofd.ShowDialog()==DialogResult.OK){ var pwd = PromptPassword(); if(!string.IsNullOrEmpty(pwd) && SettingsTransferService.Import(ofd.FileName, pwd)){ XtraMessageBox.Show("تم الاستيراد"); } } };
            btnDiag.Click += async (_, __) => { var secrets = ConnectionSecretsStore.Load(); if(secrets==null){ XtraMessageBox.Show("لا توجد إعدادات"); return; } var rep = await ConnectionDiagnostics.RunAsync(secrets); XtraMessageBox.Show($"DNS: {rep.Dns}\nLatency(ms): {rep.LatencyMs}\nPG: {rep.PgVersion}\nNote: {rep.Note}", "Diagnostics"); };
            cboProfile.SelectedIndexChanged += (_, __) => { var profile = cboProfile.SelectedItem?.ToString() ?? "Production"; _csProvider.SetProfile(profile); Properties.Settings.Default.ApiAuth = profile; Properties.Settings.Default.Save(); AppSettingsStore.Set("ApiAuth", profile); };
            btnReset.Click += (_, __) => {
                if (XtraMessageBox.Show("سيتم مسح البيانات من الشاشة فقط.\nاذا ضغطت حفظ بعد كدا سيتم مسح بيانات الجهاز نهائياً.", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    txtApiBase.Text = string.Empty;
                    txtBranchId.Text = string.Empty;
                    cboProfile.SelectedIndex = 0;
                    chkSkipCert.Checked = false;
                    numTimeout.Value = 15;
                    _statusLabel!.Text = "⚠ تم مسح البيانات — اضغط حفظ للتأكيد";
                    _statusLabel.ForeColor = System.Drawing.Color.OrangeRed;
                }
            };
            AuditSender.Start("https://your-api-endpoint.example.com/audit");
        }

        private string PromptPassword()
        {
            using var f = new Form{ Width=300, Height=120, Text="Password" }; var tb = new TextBox{ Left=10, Top=10, Width=260, UseSystemPasswordChar=true }; var ok = new Button{ Text="OK", Left=110, Top=40, DialogResult=DialogResult.OK }; f.Controls.Add(tb); f.Controls.Add(ok); f.AcceptButton = ok; return f.ShowDialog()==DialogResult.OK? tb.Text : string.Empty;
        }

        private void InjectStatusLabel()
        {
            if (_statusLabel != null) return;
            _statusLabel = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Tahoma", 10F),
                ForeColor = System.Drawing.Color.DarkSlateGray,
                Location = new System.Drawing.Point(30, 245),
                Text = "" // will be set in LoadApiSettings
            };
            // xtraTabPage7 was removed; add label directly to the form
            Controls.Add(_statusLabel);
        }

        private void Frm_ServerSetting_Load(object sender, EventArgs e)
        {
            // legacy SQL discovery kept minimal
        }

        #region API Activation
        private void LoadApiSettings()
        {
            try
            {
                // Helper: pick first non-empty value
                static string FirstNonEmpty(params string?[] vals)
                {
                    foreach (var v in vals) if (!string.IsNullOrWhiteSpace(v)) return v!;
                    return string.Empty;
                }

                // ── Load ApiBase ──
                string apiBase = string.Empty;
                var meta = Services.Security.ActivationMetadataStorage.Load();
                if (meta != null && meta.OrgId != Guid.Empty && meta.TenantId != Guid.Empty)
                {
                    var e = SaasEndpointStore.Get(meta.OrgId, meta.TenantId);
                    if (e != null)
                        apiBase = TenantEndpointResolver.BuildHttpsBaseUrl(e.Value.TenantSubdomain, e.Value.RootDomain);
                }
                if (string.IsNullOrWhiteSpace(apiBase))
                    apiBase = FirstNonEmpty(Properties.Settings.Default.ApiBase, AppSettingsStore.Get("ApiBase"));
                txtApiBase.Text = apiBase;

                // ── Load Activation Code ──
                var savedCode = ActivationStorage.Load();
                if (!string.IsNullOrWhiteSpace(savedCode)) txtBranchId.Text = savedCode;

                // ── Load Profile ──
                var savedProfile = FirstNonEmpty(Properties.Settings.Default.ApiAuth, AppSettingsStore.Get("ApiAuth"));
                if (!string.IsNullOrWhiteSpace(savedProfile) && cboProfile.Items.Contains(savedProfile))
                    cboProfile.SelectedItem = savedProfile;

                numTimeout.Value = Math.Max(1, Properties.Settings.Default.ApiTimeoutSec == 0 ? 15 : Properties.Settings.Default.ApiTimeoutSec);
                chkSkipCert.Checked = Properties.Settings.Default.ApiSkipCert;
                _statusLabel!.Text = HasDeviceToken() ? "✅ مفعل" : "⚠ غير مفعل";
                _statusLabel.ForeColor = HasDeviceToken() ? System.Drawing.Color.Green : System.Drawing.Color.OrangeRed;
            }
            catch { }
        }

        private bool HasDeviceToken()
        {
            if (!File.Exists(DeviceTokenFilePath)) return false;
            var token = File.ReadAllText(DeviceTokenFilePath).Trim();
            return token.Length > 20;
        }

        private string? GetDeviceToken() => HasDeviceToken() ? File.ReadAllText(DeviceTokenFilePath).Trim() : null;

        private HttpClient GetOrCreateClient()
        {
            if (_http != null) return _http;
            var handler = new HttpClientHandler();
            if (chkSkipCert.Checked)
                handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            _http = new HttpClient(handler)
            {
                BaseAddress = new Uri(NormalizeBaseUrl(txtApiBase.Text)),
                Timeout = TimeSpan.FromSeconds((int)numTimeout.Value)
            };
            var token = GetDeviceToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Add("X-Device-Token", token);
            }
            return _http;
        }

        private static string NormalizeBaseUrl(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "http://localhost:5000/";
            raw = raw.Trim();
            if (!raw.EndsWith("/")) raw += "/";
            return raw;
        }

        private async System.Threading.Tasks.Task<bool> ActivateAsync()
        {
            var baseUrl = NormalizeBaseUrl(txtApiBase.Text);
            var activationCode = txtBranchId.Text.Trim(); // reuse txtBranchId control for activation code
            if (string.IsNullOrEmpty(baseUrl)) { MessageBox.Show("أدخل رابط الـ API"); return false; }
            if (string.IsNullOrEmpty(activationCode)) { MessageBox.Show("أدخل كود التفعيل"); return false; }

            var client = GetOrCreateClient();
            var payload = new { activationCode, machineId = Environment.MachineName, appVersion = Application.ProductVersion };
            try
            {
                var json = JsonSerializer.Serialize(payload);
                var resp = await client.PostAsync("api/devices/activate", new StringContent(json, Encoding.UTF8, "application/json"));
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("فشل التفعيل: " + resp.StatusCode);
                    return false;
                }
                var body = await resp.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DeviceActivationResponse>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result == null || string.IsNullOrWhiteSpace(result.DeviceToken))
                {
                    MessageBox.Show("استجابة غير صالحة");
                    return false;
                }
                File.WriteAllText(DeviceTokenFilePath, result.DeviceToken);
                Properties.Settings.Default.ApiBase = baseUrl;
                AppSettingsStore.Set("ApiBase", baseUrl);
                Properties.Settings.Default.ApiSkipCert = chkSkipCert.Checked;
                Properties.Settings.Default.ApiTimeoutSec = (int)numTimeout.Value;
                Properties.Settings.Default.Save();
                // Persist per-org endpoint if activation metadata exists.
                var meta = Services.Security.ActivationMetadataStorage.Load();
                if (meta != null && meta.OrgId != Guid.Empty)
                    ApiEndpointProfilesStore.SetOrgApiBase(meta.OrgId, baseUrl);

                _statusLabel!.Text = "✅ Device Activated";
                MessageBox.Show("تم التفعيل بنجاح");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ اتصال");
                return false;
            }
        }

        // Build real PostgreSQL connection string from the API Base textbox.
        // If user pastes a full connection string (contains 'Host='), use it directly.
        private string MasterConnection => _csProvider.GetConnectionString();

        private string BuildConnectionString()
        {
            // Read UI into secrets and persist
            var raw = txtApiBase.Text.Trim();
            string host; int port = 5432; string db = "PharmaCoreDB"; string user = "postgres"; string pass = "12345";
            if (raw.Contains("Host="))
            {
                // Full connection string pasted
                host = GetBetween(raw, "Host=", ";") ?? raw; int.TryParse(GetBetween(raw, "Port=", ";") ?? "5432", out port);
                db = GetBetween(raw, "Database=", ";") ?? db; user = GetBetween(raw, "Username=", ";") ?? user; pass = GetBetween(raw, "Password=", ";") ?? pass;
            }
            else if (raw.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || raw.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                // API URL like http://localhost:5001/ — extract just the hostname
                try { var uri = new Uri(raw); host = uri.Host; } catch { host = "localhost"; }
                // DB port is always 5432, NOT the API port
                port = 5432;
            }
            else
            {
                if (raw.Contains(':')){ var parts = raw.Split(':'); host=parts[0]; if(parts.Length>1 && int.TryParse(parts[1], out var p)) port=p; } else host=raw;
            }

            // --- FLEXIBLE LOCAL / REMOTE CONNECTION ---
            // Dynamically select DB credentials based on the API target.
            // When API is localhost → local dev DB (no SSL, postgres user)
            // When API is remote   → production DB (SSL, pharmacore user)
            if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || host.Equals("127.0.0.1"))
            {
                db = "PharmaCoreDB";
                user = "postgres";
                pass = "12345";
            }

            bool sslRequired = !(host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || host.Equals("127.0.0.1"));

            var secrets = new ConnectionSecrets{ Host=host, Port=port, Database=db, Username=user, Password=pass, SslRequire=sslRequired, TrustServerCert=true };
            _csProvider.SetAndPersist(secrets);
            return _csProvider.GetConnectionString();
        }
        private static string? GetBetween(string s,string start,string end)
        {
            var i = s.IndexOf(start,StringComparison.OrdinalIgnoreCase); if(i<0) return null; i += start.Length; var j = s.IndexOf(end, i, StringComparison.OrdinalIgnoreCase); if(j<0) j = s.Length; return s.Substring(i, j-i);
        }

        private string DiagnoseConnectionStringProblems(string cs, string code)
        {
            if (string.IsNullOrWhiteSpace(cs)) return "سلسلة الاتصال غير محددة";
            if (!cs.Contains("Host=")) return "صيغة سلسلة الاتصال غير صحيحة";
            if (string.IsNullOrWhiteSpace(code)) return "أدخل كود التفعيل";
            if (!ActivationDb.IsFormatValid(code)) return "صيغة الكود غير صحيحة";
            return string.Empty; // ok
        }

        private async void btnTestApi_Click(object sender, EventArgs e)
        {
            if (!RateLimiter.Allow()) { XtraMessageBox.Show("محاولات كثيرة، حاول لاحقاً", "تنبيه"); return; }
            // In SaaS mode we don't rely on IP/ports. The value should be a tenant URL like https://{tenant}.{domain}/
            // Keep global ApiBase in sync for legacy code paths, but validate as production URL.
            try
            {
                var url = txtApiBase.Text.Trim();
                if (!url.EndsWith("/")) url += "/";
                var isProduction = cboProfile.SelectedItem?.ToString() == "Production";
                if (isProduction) TenantEndpointResolver.ValidateProductionUrlOrThrow(url);
                Properties.Settings.Default.ApiBase = url;
                AppSettingsStore.Set("ApiBase", url);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "خطأ");
                return;
            }

             var code = txtBranchId.Text.Trim();
             BuildConnectionString(); // always rebuild from current URL
             var cs = MasterConnection;
             var prelim = DiagnoseConnectionStringProblems(cs, code);
             if (!string.IsNullOrEmpty(prelim)) { XtraMessageBox.Show(prelim, "تنبيه"); AuditLogger.Log("LoginFailed", "Auth", "Frm_ServerSetting", $"فشل التحقق: {prelim}", severity: 1); return; }
             var res = await ActivationDb.ValidateAsync(cs, code, includeNames:true);
             AuditLogger.Log(res.ok ? "Login" : "LoginFailed", "Auth", "Frm_ServerSetting", $"اختبار تفعيل: {(res.ok ? "ناجح" : res.reason)}", severity: res.ok ? 0 : 1);
             if (!res.ok) { XtraMessageBox.Show("فشل: " + res.reason, "اختبار", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

             // fetch real Branch.Code
             string? branchCode = null;
             if(res.rec?.BranchId != null)
             {
                 try{ await using var con = new NpgsqlConnection(cs); await con.OpenAsync(); await using var cmd = new NpgsqlCommand("SELECT \"Code\" FROM \"Branches\" WHERE \"Id\"=@id", con); cmd.Parameters.AddWithValue("id", res.rec.BranchId); var r = await cmd.ExecuteScalarAsync(); branchCode = r as string; } catch { }
             }

             _statusLabel!.Text = $"✅ صالح | Org:{res.rec!.OrgName} Tenant:{res.rec.TenantName} Region:{res.rec.RegionName} Branch:{res.rec.BranchName}";
             PharmaCore.POS.Services.Security.ActivationMetadataStorage.Save(new PharmaCore.POS.Services.Security.ActivationMetadata{ OrgName = res.rec.OrgName, BranchName = res.rec.BranchName, BranchCode = branchCode, OrgId = res.rec.OrgId, BranchId = res.rec.BranchId ?? Guid.Empty, TenantId = res.rec.TenantId ?? Guid.Empty, RegionId = res.rec.RegionId ?? Guid.Empty });

            // If we now know org+tenant, store SaaS endpoint by parsing the host into tenantSub + rootDomain.
            try
            {
                var uri = new Uri(Properties.Settings.Default.ApiBase);
                var hostParts = uri.Host.Split('.', 2);
                if (hostParts.Length == 2)
                {
                    SaasEndpointStore.Set(res.rec.OrgId, res.rec.TenantId ?? Guid.Empty, hostParts[1], hostParts[0]);
                }
            }
            catch { }

             XtraMessageBox.Show(_statusLabel.Text, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!RateLimiter.Allow()) { XtraMessageBox.Show("محاولات كثيرة، حاول لاحقاً", "تنبيه"); return; }
            var code = txtBranchId.Text.Trim();
            BuildConnectionString(); // always rebuild from current URL
            var cs = MasterConnection;
            var prelim = DiagnoseConnectionStringProblems(cs, code);
            if (!string.IsNullOrEmpty(prelim)) { XtraMessageBox.Show(prelim, "تنبيه"); AuditLogger.Log("LoginFailed", "Auth", "Frm_ServerSetting", $"فشل التحقق: {prelim}", severity: 1); return; }
            var res = await ActivationDb.ValidateAsync(cs, code, includeNames:true);
            AuditLogger.Log(res.ok ? "Login" : "LoginFailed", "Auth", "Frm_ServerSetting", $"محاولة دخول: {(res.ok ? "ناجح" : res.reason)}", severity: res.ok ? 0 : 1);
            if (!res.ok) { XtraMessageBox.Show("لا يمكن الدخول: " + res.reason, "دخول", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            var inc = await ActivationDb.IncrementUsageAsync(cs, code);
            if (!inc.ok) { AuditLogger.Log("LoginFailed", "Auth", "Frm_ServerSetting", $"فشل تحديث عدد الأجهزة: {inc.reason}", severity: 1); XtraMessageBox.Show("تعذر تحديث عدد الأجهزة: "+inc.reason, "خطأ"); return; }
            ActivationStorage.Save(code);
            // fetch real Branch.Code
            string? branchCode = null;
            if(res.rec?.BranchId != null)
            {
                try{ await using var con = new NpgsqlConnection(cs); await con.OpenAsync(); await using var cmd = new NpgsqlCommand("SELECT \"Code\" FROM \"Branches\" WHERE \"Id\"=@id", con); cmd.Parameters.AddWithValue("id", res.rec.BranchId); var r = await cmd.ExecuteScalarAsync(); branchCode = r as string; } catch { }
            }
            PharmaCore.POS.Services.Security.ActivationMetadataStorage.Save(new PharmaCore.POS.Services.Security.ActivationMetadata{ OrgName = res.rec!.OrgName, BranchName = res.rec.BranchName, BranchCode = branchCode, OrgId = res.rec.OrgId, BranchId = res.rec.BranchId ?? Guid.Empty, TenantId = res.rec.TenantId ?? Guid.Empty, RegionId = res.rec.RegionId ?? Guid.Empty });

            // Register device in API's Devices table
            await RegisterDeviceViaApiAsync(code);

            AuditLogger.Log("Login", "Auth", "Frm_ServerSetting", "تسجيل دخول ناجح من إعدادات السيرفر");
            DialogResult = DialogResult.OK; Close();
        }

        private async System.Threading.Tasks.Task RegisterDeviceViaApiAsync(string activationCode)
        {
            try
            {
                string baseUrl;
                try { baseUrl = Services.Configuration.ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
                catch { baseUrl = NormalizeBaseUrl(txtApiBase.Text); }

                var _sslHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
                using var http = new HttpClient(_sslHandler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(15) };
                var hwid = GetHardwareId();
                var payload = new { ActivationCode = activationCode, MachineId = hwid, MachineName = Environment.MachineName };
                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await http.PostAsync("api/branch-sessions/register-device", content);
                // Device registration is best-effort during activation
                if (resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<RegisterDeviceApiResponse>(body, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (result?.DeviceToken != null)
                    {
                        File.WriteAllText(DeviceTokenFilePath, result.DeviceToken);
                        _statusLabel!.Text = "✅ Device Registered + Activated";
                    }
                }
            }
            catch { /* best-effort */ }
        }

        private string GetHardwareId()
        {
            try
            {
                string cpu = QueryWMI("Win32_Processor", "ProcessorId");
                string disk = QueryWMI("Win32_DiskDrive", "SerialNumber");
                string machine = Environment.MachineName;
                string raw = cpu + "|" + disk + "|" + machine;
                using var sha = System.Security.Cryptography.SHA256.Create();
                return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(raw)));
            }
            catch { return Environment.MachineName; }
        }

        private string QueryWMI(string cls, string prop)
        {
            try
            {
                using var searcher = new System.Management.ManagementObjectSearcher("SELECT " + prop + " FROM " + cls);
                foreach (var o in searcher.Get())
                {
                    var v = o?[prop]?.ToString(); if (!string.IsNullOrWhiteSpace(v)) return v.Trim();
                }
            }
            catch { }
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save all settings to device
            var code = txtBranchId.Text.Trim();
            var apiBase = txtApiBase.Text.Trim();
            var profile = cboProfile.SelectedItem?.ToString() ?? "Production";

            // If fields are empty after Reset, this effectively wipes the device
            if (string.IsNullOrWhiteSpace(apiBase) && string.IsNullOrWhiteSpace(code))
            {
                if (XtraMessageBox.Show("البيانات فارغة — سيتم مسح إعدادات الجهاز نهائياً. متأكد؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                MultiProfileConnectionSecretsStore.ResetAll();
                ActivationStorage.Clear();
                Properties.Settings.Default.ApiBase = string.Empty;
                Properties.Settings.Default.ApiToken = string.Empty;
                Properties.Settings.Default.EmployeeToken = string.Empty;
                Properties.Settings.Default.ApiAuth = string.Empty;
                Properties.Settings.Default.Save();
                _statusLabel!.Text = "⚠ تم مسح الجهاز — يحتاج تفعيل جديد";
                _statusLabel.ForeColor = System.Drawing.Color.OrangeRed;
                XtraMessageBox.Show("تم مسح جميع الإعدادات");
                return;
            }

            BuildConnectionString();
            ActivationStorage.Save(code);
            if (!apiBase.EndsWith("/")) apiBase += "/";
            Properties.Settings.Default.ApiBase = apiBase;
            AppSettingsStore.Set("ApiBase", apiBase);
            Properties.Settings.Default.ApiAuth = profile;
            Properties.Settings.Default.ApiTimeoutSec = (int)numTimeout.Value;
            Properties.Settings.Default.ApiSkipCert = chkSkipCert.Checked;
            Properties.Settings.Default.Save();
            _statusLabel!.Text = "✅ تم حفظ الإعدادات";
            _statusLabel.ForeColor = System.Drawing.Color.Green;
            XtraMessageBox.Show("تم حفظ الإعدادات بنجاح");
        }
        #endregion

        #region Legacy SQL placeholders

        #endregion

        private void txtBranchId_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-') e.Handled = true;
        }
    }

    internal sealed class DeviceActivationResponse
    {
        public string DeviceToken { get; set; } = string.Empty;
        public DateTime ActivatedAt { get; set; }
        public string? DeviceId { get; set; }
    }

    internal sealed class RegisterDeviceApiResponse
    {
        public bool Ok { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? DeviceToken { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? OrgId { get; set; }
        public Guid? BranchId { get; set; }
    }
}
