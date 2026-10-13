using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services.Configuration
{
    /// <summary>
    /// Stores API base URLs per organization to support multi-org deployments.
    /// Values are protected per Windows user.
    /// </summary>
    internal static class ApiEndpointProfilesStore
    {
        private static readonly string RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore", "ApiProfiles");
        private static readonly string FilePath = Path.Combine(RootDir, "org-api.json.dpapi");

        private static byte[] Entropy => SHA256.HashData(Encoding.UTF8.GetBytes($"{Environment.UserName}|api-profiles|PharmaCore"));

        public static void SetOrgApiBase(Guid orgId, string apiBase)
        {
            if (orgId == Guid.Empty) return;
            if (string.IsNullOrWhiteSpace(apiBase)) return;

            var map = LoadAll();
            map[orgId.ToString("N")] = apiBase.Trim();
            SaveAll(map);
        }

        public static string? GetOrgApiBase(Guid orgId)
        {
            if (orgId == Guid.Empty) return null;
            var map = LoadAll();
            return map.TryGetValue(orgId.ToString("N"), out var v) ? v : null;
        }

        private static Dictionary<string, string> LoadAll()
        {
            try
            {
                if (!File.Exists(FilePath)) return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var blob = File.ReadAllBytes(FilePath);
                var plain = ProtectedData.Unprotect(blob, Entropy, DataProtectionScope.CurrentUser);
                var map = JsonSerializer.Deserialize<Dictionary<string, string>>(plain);
                return map ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static void SaveAll(Dictionary<string, string> map)
        {
            Directory.CreateDirectory(RootDir);
            var json = JsonSerializer.Serialize(map);
            var plain = Encoding.UTF8.GetBytes(json);
            var blob = ProtectedData.Protect(plain, Entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(FilePath, blob);
        }
    }
}
