using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Login : DevExpress.XtraEditors.XtraForm
    {
        Thread t;
        private readonly IAuthService? _authService;
        private readonly IProductsService? _productsService;
        private readonly IConnectionStringProvider _connProvider = new ConnectionStringProvider();

        private sealed record BranchSessionLoginRequest(Guid OrgId, Guid BranchId, string BranchCode, string UserName, string Password, string HardwareId);
        private sealed record BranchSessionLoginResponse(bool Ok, string Error, string? Token, Guid? BranchUserId, Guid? BranchUserDeviceId);

        public Frm_Login() : this(null, null) { }

        public Frm_Login(IAuthService? authService, IProductsService? productsService)
        {
            InitializeComponent();
            _authService = authService;
            _productsService = productsService;
            // عند الضغط على اسم المستخدم المقصود فقط محاولة تحميل المحفوظ (لا تحديث اللابل)
            labUserName.Click += (_, __) => { var saved = PharmaCore.POS.Properties.Settings.Default.USERNAME; if(!string.IsNullOrWhiteSpace(saved)) txtUser.Text = saved.Trim(); txtUser.Focus(); };
            btnLogin.Click += btnLogin_Click; // ensure wired
            // تمت إزالة حدث TextChanged الذي كان يغير اللابل
        }

        private string GetActiveConnectionString() => _connProvider.GetConnectionString() ?? Services.Connection.ConnectionStringProvider.Instance.GetConnectionString();

        private string GetHardwareId()
        {
            try
            {
                string cpu = QueryWMI("Win32_Processor", "ProcessorId");
                string disk = QueryWMI("Win32_DiskDrive", "SerialNumber");
                string machine = Environment.MachineName;
                string raw = cpu + "|" + disk + "|" + machine;
                using var sha = SHA256.Create();
                return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(raw)));
            }
            catch { return Environment.MachineName; }
        }

        private string QueryWMI(string cls, string prop)
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT " + prop + " FROM " + cls);
                foreach (var o in searcher.Get())
                {
                    var v = o?[prop]?.ToString(); if (!string.IsNullOrWhiteSpace(v)) return v.Trim();
                }
            }
            catch { }
            return "";
        }

        private async void Frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                var meta = Services.Security.ActivationMetadataStorage.Load();
                if (meta == null)
                {
                    Hide(); using (var s = new Frm_ServerSetting()) { if (s.ShowDialog() != DialogResult.OK) { Close(); return; } }
                    meta = Services.Security.ActivationMetadataStorage.Load(); if (meta == null) { XtraMessageBox.Show("البيانات غير مكتملة", "خطأ"); Close(); return; }
                }

                PopulateEnvironmentLabels();
                txtPassword.Clear(); txtPassword.Focus();

                // IMPORTANT: username must be scoped per org+branch to avoid mixing branch devices/users.
                var branchScopedUser = Services.Security.BranchScopedUserStore.LoadLastUsername(_orgId, _branchId);

                var conn = GetActiveConnectionString();
                if (string.IsNullOrWhiteSpace(conn))
                {
                    Hide(); using (var s = new Frm_ServerSetting()) { if (s.ShowDialog() != DialogResult.OK) { Close(); return; } }
                    Show();
                }

                if (!string.IsNullOrWhiteSpace(branchScopedUser))
                {
                    txtUser.Text = branchScopedUser.Trim();
                    labBrUser.Text = branchScopedUser.Trim(); // show only if exists for this org+branch
                }
                else
                {
                    txtUser.Text = string.Empty;
                    labBrUser.Text = string.Empty; // يبقى فارغ إلى أن يتم أول دخول ناجح لهذا الفرع على هذا الجهاز
                }
            }
            catch (Exception) { }
        }

        private Guid _orgId = Guid.Empty; private Guid _branchId = Guid.Empty; private Guid _tenantId = Guid.Empty; private Guid _regionId = Guid.Empty;
        private void PopulateEnvironmentLabels()
        {
            labMacName.Text = Environment.MachineName;
            labMacID.Text = GetHardwareId().Substring(0, 12);
            var meta = Services.Security.ActivationMetadataStorage.Load();
            if(meta!=null)
            {
                _orgId = meta.OrgId; _branchId = meta.BranchId; _tenantId = meta.TenantId; _regionId = meta.RegionId;
                labOrgName.Text = meta.OrgName ?? "Org"; labBrName.Text = meta.BranchName ?? "Branch"; labBrCode.Text = meta.BranchCode ?? "---";
            }
            else
            {
                var code = ActivationStorage.Load(); labBrCode.Text = code?.Split('-').LastOrDefault() ?? "---"; labOrgName.Text = "Org"; labBrName.Text = "BranchUser";
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e) { if (e.KeyChar == 13) btnLogin_Click(null, null); }
        private bool _loginInProgress;
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (_loginInProgress) return; _loginInProgress = true;
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!Services.Security.RateLimiter.Allow("login", 5, TimeSpan.FromMinutes(1))) { XtraMessageBox.Show("محاولات كثيرة، حاول لاحقاً", "تنبيه"); return; }
                var username = txtUser.Text.Trim(); var password = txtPassword.Text.Trim(); var hwid = GetHardwareId();
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) { XtraMessageBox.Show("ادخل اسم المستخدم وكلمة المرور", "خطأ"); return; }

                // Resolve API URL
                string baseUrl;
                try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
                catch
                {
                    try { baseUrl = ApiBaseResolver.GetRequiredBaseUrlForCurrentOrg(); }
                    catch
                    {
                        var saved = Properties.Settings.Default.ApiBase?.Trim();
                        if (string.IsNullOrWhiteSpace(saved)) { XtraMessageBox.Show("لم يتم ضبط عنوان السيرفر.\nافتح إعدادات الشبكة.", "خطأ"); return; }
                        baseUrl = saved;
                    }
                }

                // Ensure baseUrl ends with /
                if (!baseUrl.EndsWith("/")) baseUrl += "/";

                var branchCode = labBrCode.Text.Trim();
                var request = new BranchSessionLoginRequest(_orgId, _branchId, branchCode, username, password, hwid);
                BranchSessionLoginResponse? res = null;

                // Always skip SSL validation — server cert is for domain, POS connects via IP/proxy
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

                using var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(15) };
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = await httpClient.PostAsJsonAsync("api/branch-sessions/login", request);
                    if (response.IsSuccessStatusCode)
                    {
                        res = await response.Content.ReadFromJsonAsync<BranchSessionLoginResponse>();
                        if (res == null) throw new Exception("فشل غير متوقع من السيرفر");
                    }
                    else
                    {
                        XtraMessageBox.Show($"فشل في الاتصال بالسيرفر: {response.StatusCode}\n\nURL: {baseUrl}api/branch-sessions/login", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (TaskCanceledException)
                {
                    XtraMessageBox.Show("انتهت مهلة الاتصال بالسيرفر.\n\nتأكد أن السيرفر (API) شغال على:\n" + baseUrl, "خطأ اتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (HttpRequestException ex)
                {
                    XtraMessageBox.Show("فشل الاتصال بالسيرفر:\n" + ex.Message + "\n\nتأكد أن API شغال على:\n" + baseUrl, "خطأ اتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Services.Security.AuditLogger.LogAttempt(username, res.Ok, res.Error);
                if (!res.Ok) { XtraMessageBox.Show("فشل الدخول: " + res.Error, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                PharmaCore.POS.Properties.Settings.Default.ApiToken = res.Token ?? string.Empty;
                PharmaCore.POS.Properties.Settings.Default.USERNAME = username;
                PharmaCore.POS.Properties.Settings.Default.Save();
                Services.Security.BranchScopedUserStore.SaveLastUsername(_orgId, _branchId, username);

                labBrUser.Text = username;
                
                // Sync centralized reporting layouts in the background seamlessly
                _ = System.Threading.Tasks.Task.Run(async () => await ReportManager.SyncAllLayoutsAsync());

                Hide();
                try { using var emp = new Frm_LoginEmployee(); emp.ShowDialog(); } catch { }
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("خطأ غير متوقع:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _loginInProgress = false; Cursor = Cursors.Default; }
        }

        private bool trial(){ int num = Properties.Settings.Default.trial; int thissum = num + 1; Properties.Settings.Default.trial = thissum; if (thissum >= 30){ MessageBox.Show("تم إنتهاء الفترة التجريبية", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Stop); return false; } else { int baky = 30 - thissum; MessageBox.Show("باقي على نهاية الفترة التجريبية عدد" + baky + "مرة/ات تسجيل دخول", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); } return true; }
        private void btnExit_Click(object sender, EventArgs e)=> Application.Exit();
        private void simpleButton2_Click(object sender, EventArgs e){ simpleButton1.BringToFront(); txtPassword.UseSystemPasswordChar = true; }
        private void simpleButton1_Click(object sender, EventArgs e){ simpleButton2.BringToFront(); txtPassword.UseSystemPasswordChar = false; }
        private void txtPassword_TextChanged(object sender, EventArgs e){ txtPassword.UseSystemPasswordChar = true; }
        private void cbxUserName_SelectedIndexChanged(object sender, EventArgs e){ txtPassword.Focus(); }
        private void btnServerSetting_Click(object sender, EventArgs e){ if (MessageBox.Show("الدخول على هذه الشاشة و التغيير فيها يؤدي إلى عطل البرنامج ، هل تريد الدخول على اي حال؟ ", "تنبيه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK){ Properties.Settings.Default.Open_Form = 14; Properties.Settings.Default.Save(); Frm_DataBase frm = new Frm_DataBase(); frm.ShowDialog(); } }

        private bool UsingApiOrPostgres() => !string.IsNullOrWhiteSpace(Properties.Settings.Default.ApiBase) || System.IO.File.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"pharmacore.conn"));
    }
}