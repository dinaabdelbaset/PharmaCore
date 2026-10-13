// DevExpress (اختياري)
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Net.Http;
using System.Windows.Forms;
using PharmaCore.POS.Enums;
using PharmaCore.POS.PL;
using PharmaCore.POS.Services;

namespace PharmaCore.POS
{
    internal static class Program
    {
        // services
        private static IApiClient ApiClient = null!;
        private static IAuthService AuthService = null!;
        private static IProductsService ProductsService = null!;

        private static async System.Threading.Tasks.Task<bool> ValidateStoredActivationAsync()
        {
            var code = ActivationStorage.Load();
            var provider = new ConnectionStringProvider();
            var cs = provider.GetConnectionString();
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(cs)) return false;
            // Try DB validation; if unreachable, trust local data
            try
            {
                var res = await ActivationDb.ValidateAsync(cs, code);
                return res.ok;
            }
            catch
            {
                // DB unreachable — trust locally stored activation
                return true;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.SetHighDpiMode(HighDpiMode.SystemAware); } catch { }
            PharmaCore.POS.Helpers.ThemeHelper.InitializeGlobalTheme();
            Application.ThreadException += (s,e)=> ShowGlobalError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s,e)=> { if(e.ExceptionObject is Exception ex) ShowGlobalError(ex); };

            SecurityBootstrap.Init();

            // Fix DevExpress NonTrustedTypeDeserializationException for Legacy/Custom Reports
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(System.Data.DataTable));
            DevExpress.Utils.DeserializationSettings.RegisterTrustedClass(typeof(System.Data.DataSet));
            
            var provider = new PharmaCore.POS.Services.ConnectionStringProvider();
            PharmaCore.POS.Services.DatabaseMigrator.RunAsync(provider.GetConnectionString()).GetAwaiter().GetResult();

            var metaPresent = Services.Security.ActivationMetadataStorage.Exists();
            bool activationValid = metaPresent && ValidateStoredActivationAsync().GetAwaiter().GetResult();
            if(!activationValid)
            {
                // Show activation screen — loop until valid or user explicitly quits
                while (!activationValid)
                {
                    using var s = new Frm_ServerSetting();
                    var result = s.ShowDialog();

                    // If btnLogin_Click succeeded (DialogResult.OK) — trust it and proceed
                    if (result == DialogResult.OK) break;

                    // Otherwise re-check in case user saved without clicking Login
                    metaPresent = Services.Security.ActivationMetadataStorage.Exists();
                    activationValid = metaPresent && ValidateStoredActivationAsync().GetAwaiter().GetResult();
                    if (activationValid) break;

                    // User closed without valid activation — ask if they want to retry or quit
                    var retry = XtraMessageBox.Show("لم يتم التفعيل بعد. هل تريد المحاولة مرة أخرى؟", "تنبيه",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (retry != DialogResult.Yes) return; // exit app
                }
            }

            InitializeServices();
            OfflineSalesSyncWorker.Start();
            Application.Run(new Frm_Login(AuthService, ProductsService));
        }

        private static void InitializeServices()
        {
            string baseUrl;
            try
            {
                baseUrl = Services.Configuration.ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant();
            }
            catch
            {
                try { baseUrl = Services.Configuration.ApiBaseResolver.GetRequiredBaseUrlForCurrentOrg(); }
                catch
                {
                    var saved = Properties.Settings.Default.ApiBase?.Trim();
                    if (string.IsNullOrWhiteSpace(saved))
                        throw new InvalidOperationException("API base URL is not configured.");
                    baseUrl = saved;
                }
            }
            var http = new HttpClient { BaseAddress = new Uri(baseUrl) };
            ApiClient = new ApiClient(http);
            AuthService = new AuthService(ApiClient);
            ProductsService = new ProductsService(ApiClient);
        }

        private static void ShowGlobalError(Exception ex)
        {
            try { XtraMessageBox.Show($"حدث خطأ غير متوقع:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch { MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
