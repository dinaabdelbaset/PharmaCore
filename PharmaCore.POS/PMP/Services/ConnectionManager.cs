using PharmaCore.POS.Enums;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    public static class ConnectionManager
    {
        public static ConnectionMode CurrentMode => ConnectionMode.Api; // forced to API mode after SQL removal

        public static HttpClient GetHttpClient()
        {
            var baseUrl = Properties.Settings.Default.ApiBase?.Trim();
            var skipCert = Properties.Settings.Default.ApiSkipCert;
            var timeout = Properties.Settings.Default.ApiTimeoutSec;
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new InvalidOperationException("API base URL is missing.");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var handler = new HttpClientHandler(); if (skipCert) handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            var http = new HttpClient(handler){ BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(timeout <= 0 ? 15 : timeout) };
            var token = Properties.Settings.Default.ApiToken; if (!string.IsNullOrWhiteSpace(token)) http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return http;
        }
        public static async Task<bool> PingApiAsync(){ try { using var http = GetHttpClient(); var res = await http.GetAsync("/health"); return true; } catch { return false; } }
        public static async Task<bool> TestAsync(){ try { using var http = GetHttpClient(); var res = await http.GetAsync("/health"); if (res.IsSuccessStatusCode) return true; res = await http.GetAsync("/swagger/index.html"); return res.IsSuccessStatusCode; } catch { return false; } }
        // Retain optional encryption helpers for future secure storage of API secrets
        public static void SaveEncryptedSqlPassword(string plain){ /* deprecated */ Properties.Settings.Default.LogPassword=""; Properties.Settings.Default.Save(); }
        public static string ReadEncryptedSqlPassword() => "";
    }
}
