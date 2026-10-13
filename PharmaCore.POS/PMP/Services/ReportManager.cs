using DevExpress.XtraReports.UI;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PharmaCore.POS.Services
{
    public static class ReportManager
    {
        private static readonly string ReportsDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "PharmaCore", 
            "Reports");

        static ReportManager()
        {
            if (!Directory.Exists(ReportsDirectory))
            {
                Directory.CreateDirectory(ReportsDirectory);
            }
        }

        public static string GetReportFilePath(string reportName)
        {
            return Path.Combine(ReportsDirectory, $"{reportName}.repx");
        }

        public static void LoadCustomLayoutIfExists(XtraReport report, string reportName)
        {
            string filePath = GetReportFilePath(reportName);
            if (File.Exists(filePath))
            {
                try
                {
                    report.LoadLayoutFromXml(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load custom report layout for {reportName}: {ex.Message}");
                }
            }
        }

        public static async Task ResetToDefaultAsync(string reportName)
        {
            string filePath = GetReportFilePath(reportName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                using var http = ConnectionManager.GetHttpClient();
                await http.DeleteAsync($"api/report-layouts/{reportName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete layout from API: {ex.Message}");
            }
        }

        public static async Task SyncLayoutFromApiAsync(string reportName)
        {
            try
            {
                using var http = ConnectionManager.GetHttpClient();
                var res = await http.GetAsync($"api/report-layouts/{reportName}");
                if (res.IsSuccessStatusCode)
                {
                    var json = await res.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(json);
                    string layoutXml = obj["layoutXml"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(layoutXml))
                    {
                        File.WriteAllText(GetReportFilePath(reportName), layoutXml, Encoding.UTF8);
                    }
                }
                else if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // If not found in API, ensure local doesn't have an orphaned copy
                    string filePath = GetReportFilePath(reportName);
                    if (File.Exists(filePath)) File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to sync layout from API: {ex.Message}");
            }
        }

        public static async Task UploadLayoutToApiAsync(string reportName)
        {
            try
            {
                string filePath = GetReportFilePath(reportName);
                if (!File.Exists(filePath)) return;

                string layoutXml = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

                using var http = ConnectionManager.GetHttpClient();
                var payload = new
                {
                    reportName = reportName,
                    layoutXml = layoutXml
                };

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var res = await http.PostAsync("api/report-layouts", content);
                res.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload layout to API: {ex.Message}");
            }
        }
        
        public static bool HasCustomLayout(string reportName)
        {
            return File.Exists(GetReportFilePath(reportName));
        }

        public static async Task SyncAllLayoutsAsync()
        {
            // Sync specific known layouts. Since there is no "GET ALL" endpoint list yet,
            // we will sync the widely used ones explicitly on login.
            string[] reports = { "RptOrderSales1", "RptOrderSales2", "RptOrderSalesA41", "RptCrystalReport1", "RptSanadKabd1", "RptSanadSarf1" };
            foreach (var r in reports)
            {
                await SyncLayoutFromApiAsync(r);
            }
        }
    }
}
