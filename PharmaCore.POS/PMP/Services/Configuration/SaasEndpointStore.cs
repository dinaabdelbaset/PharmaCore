using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services.Configuration
{
    /// <summary>
    /// Stores SaaS endpoint configuration per (OrgId, TenantId).
    /// RootDomain and TenantSubdomain are stored (encrypted) and API base is derived as https://{tenant}.{root}/
    /// </summary>
    internal static class SaasEndpointStore
    {
        private static readonly string RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore", "SaasProfiles");
        private static readonly string FilePath = Path.Combine(RootDir, "org-tenant-endpoints.json.dpapi");
        private static byte[] Entropy => SHA256.HashData(Encoding.UTF8.GetBytes($"{Environment.UserName}|saas-endpoints|PharmaCore"));

        private sealed record Entry(string RootDomain, string TenantSubdomain);

        public static void Set(Guid orgId, Guid tenantId, string rootDomain, string tenantSubdomain)
        {
            if (orgId == Guid.Empty || tenantId == Guid.Empty) return;
            var map = LoadAll();
            map[Key(orgId, tenantId)] = new Entry(rootDomain.Trim(), tenantSubdomain.Trim());
            SaveAll(map);
        }

        public static (string RootDomain, string TenantSubdomain)? Get(Guid orgId, Guid tenantId)
        {
            if (orgId == Guid.Empty || tenantId == Guid.Empty) return null;
            var map = LoadAll();
            return map.TryGetValue(Key(orgId, tenantId), out var e) ? (e.RootDomain, e.TenantSubdomain) : null;
        }

        private static string Key(Guid orgId, Guid tenantId) => $"{orgId:N}:{tenantId:N}";

        private static Dictionary<string, Entry> LoadAll()
        {
            try
            {
                if (!File.Exists(FilePath)) return new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
                var blob = File.ReadAllBytes(FilePath);
                var plain = ProtectedData.Unprotect(blob, Entropy, DataProtectionScope.CurrentUser);
                var map = JsonSerializer.Deserialize<Dictionary<string, Entry>>(plain);
                return map ?? new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                return new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static void SaveAll(Dictionary<string, Entry> map)
        {
            Directory.CreateDirectory(RootDir);
            var json = JsonSerializer.Serialize(map);
            var plain = Encoding.UTF8.GetBytes(json);
            var blob = ProtectedData.Protect(plain, Entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(FilePath, blob);
        }
    }
}
