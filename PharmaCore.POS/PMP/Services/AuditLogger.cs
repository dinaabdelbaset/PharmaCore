using PharmaCore.POS.Services.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    /// <summary>
    /// Centralized audit logger — fire-and-forget logging to the API.
    /// Usage: AuditLogger.Log("Login", "Auth", "Frm_LoginEmployee", "تسجيل دخول ناجح");
    /// </summary>
    internal static class AuditLogger
    {
        private static Guid _currentSessionId;

        /// <summary>Log an event (fire-and-forget).</summary>
        public static void Log(string eventType, string category, string screenName, string description,
            int severity = 0, string? targetTable = null, string? targetId = null)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    using var http = CreateHttpClient();
                    if (http == null) return;

                    await http.PostAsJsonAsync("api/audit-logs", new
                    {
                        eventType,
                        category,
                        screenName,
                        description,
                        severity,
                        targetTable,
                        targetId,
                        employeeId = CurrentEmployeeSession.EmployeeId == Guid.Empty ? (Guid?)null : CurrentEmployeeSession.EmployeeId,
                        employeeCode = NullIfEmpty(CurrentEmployeeSession.EmployeeCode),
                        employeeName = NullIfEmpty(CurrentEmployeeSession.FullName),
                        branchId = CurrentEmployeeSession.BranchId == Guid.Empty ? (Guid?)null : CurrentEmployeeSession.BranchId,
                        ipAddress = (string?)null,
                        deviceName = Environment.MachineName
                    });
                }
                catch { /* never crash the app for logging */ }
            });
        }

        /// <summary>Log an event with field-level changes (fire-and-forget).</summary>
        public static void LogChange(string eventType, string category, string screenName, string description,
            (string fieldName, string? fieldLabel, string? oldValue, string? newValue)[] changes,
            int severity = 0, string? targetTable = null, string? targetId = null)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    using var http = CreateHttpClient();
                    if (http == null) return;

                    var dataChanges = new object[changes.Length];
                    for (int i = 0; i < changes.Length; i++)
                    {
                        dataChanges[i] = new
                        {
                            fieldName = changes[i].fieldName,
                            fieldLabel = changes[i].fieldLabel,
                            oldValue = changes[i].oldValue,
                            newValue = changes[i].newValue
                        };
                    }

                    await http.PostAsJsonAsync("api/audit-logs", new
                    {
                        eventType,
                        category,
                        screenName,
                        description,
                        severity,
                        targetTable,
                        targetId,
                        employeeId = CurrentEmployeeSession.EmployeeId == Guid.Empty ? (Guid?)null : CurrentEmployeeSession.EmployeeId,
                        employeeCode = NullIfEmpty(CurrentEmployeeSession.EmployeeCode),
                        employeeName = NullIfEmpty(CurrentEmployeeSession.FullName),
                        branchId = CurrentEmployeeSession.BranchId == Guid.Empty ? (Guid?)null : CurrentEmployeeSession.BranchId,
                        ipAddress = (string?)null,
                        deviceName = Environment.MachineName,
                        dataChanges
                    });
                }
                catch { /* never crash the app for logging */ }
            });
        }

        /// <summary>Start a login session.</summary>
        public static void StartSession()
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    using var http = CreateHttpClient();
                    if (http == null) return;

                    var resp = await http.PostAsJsonAsync("api/audit-logs/sessions", new
                    {
                        employeeId = CurrentEmployeeSession.EmployeeId,
                        employeeCode = CurrentEmployeeSession.EmployeeCode,
                        employeeName = CurrentEmployeeSession.FullName,
                        branchId = CurrentEmployeeSession.BranchId == Guid.Empty ? (Guid?)null : CurrentEmployeeSession.BranchId,
                        deviceName = Environment.MachineName,
                        ipAddress = (string?)null
                    });

                    if (resp.IsSuccessStatusCode)
                    {
                        var result = await resp.Content.ReadFromJsonAsync<SessionResult>();
                        if (result != null) _currentSessionId = result.Id;
                    }
                }
                catch { }
            });
        }

        /// <summary>End current session.</summary>
        public static void EndSession()
        {
            if (_currentSessionId == Guid.Empty) return;
            var sessionId = _currentSessionId;
            _currentSessionId = Guid.Empty;

            _ = Task.Run(async () =>
            {
                try
                {
                    using var http = CreateHttpClient();
                    if (http == null) return;
                    await http.PutAsync($"api/audit-logs/sessions/{sessionId}/close", null);
                }
                catch { }
            });
        }

        private static HttpClient? CreateHttpClient()
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch { return null; }
            }

            var http = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(15) };
            var token = Properties.Settings.Default.EmployeeToken;
            if (string.IsNullOrWhiteSpace(token))
                token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return http;
        }

        private static string? NullIfEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

        private sealed record SessionResult { public Guid Id { get; init; } }
    }
}