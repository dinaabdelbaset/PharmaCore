using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    public static class ApiProbe
    {
        private static HttpClient CreateClient(string baseUrl, bool skipCert, int timeoutSec)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new InvalidOperationException("API base is empty.");

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var handler = new HttpClientHandler();
            if (skipCert)
                handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;

            var http = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(timeoutSec <= 0 ? 15 : timeoutSec)
            };

            var token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return http;
        }

        public static async Task<bool> TestAsync(string baseUrl, bool skipCert, int timeoutSec)
        {
            try
            {
                using (var http = CreateClient(baseUrl, skipCert, timeoutSec))
                {
                    var res = await http.GetAsync("/health");
                    return res.IsSuccessStatusCode;
                }
            }
            catch { return false; }
        }

        public static async Task<bool> LoginAsync(string baseUrl, bool skipCert, int timeoutSec, string user, string pass)
        {
            using (var http = CreateClient(baseUrl, skipCert, timeoutSec))
            {
                var body = new { username = user, password = pass };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var res = await http.PostAsync("/api/auth/login", content);
                if (!res.IsSuccessStatusCode) return false;

                var json = await res.Content.ReadAsStringAsync();
                dynamic dto = JsonConvert.DeserializeObject(json);
                string token = (string)dto.token;

                Properties.Settings.Default.ApiToken = token;
                Properties.Settings.Default.Save();
                return true;
            }
        }
    }
}
