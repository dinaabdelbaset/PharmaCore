using System;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services.Security
{
    internal sealed class ActivationMetadata
    {
        public string? OrgName { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public Guid OrgId { get; set; }
        public Guid BranchId { get; set; }
        public Guid TenantId { get; set; }
        public Guid RegionId { get; set; }
    }

    internal static class ActivationMetadataStorage
    {
        private static readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore.activation.meta");
        private static readonly byte[] _entropy = SHA256.HashData(Encoding.UTF8.GetBytes(Environment.UserName+"|activation-meta|PharmaCore"));
        public static void Save(ActivationMetadata meta)
        {
            var json = JsonSerializer.Serialize(meta);
            var plain = Encoding.UTF8.GetBytes(json);
            var protectedBytes = ProtectedData.Protect(plain, _entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_path, protectedBytes);
        }
        public static ActivationMetadata? Load()
        {
            try
            {
                if(!File.Exists(_path)) return null;
                var blob = File.ReadAllBytes(_path);
                var plain = ProtectedData.Unprotect(blob, _entropy, DataProtectionScope.CurrentUser);
                return JsonSerializer.Deserialize<ActivationMetadata>(plain);
            }
            catch { return null; }
        }
        public static bool Exists() => File.Exists(_path);
    }
}
