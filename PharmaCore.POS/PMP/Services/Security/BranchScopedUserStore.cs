using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace PharmaCore.POS.Services.Security
{
    internal static class BranchScopedUserStore
    {
        private static readonly string RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore", "BranchUsers");

        private static byte[] Entropy(Guid orgId, Guid branchId)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes($"{Environment.UserName}|branch-user|{orgId}|{branchId}|PharmaCore"));
        }

        private static string FilePath(Guid orgId, Guid branchId)
        {
            Directory.CreateDirectory(RootDir);
            return Path.Combine(RootDir, $"{orgId:N}_{branchId:N}.user");
        }

        public static void SaveLastUsername(Guid orgId, Guid branchId, string username)
        {
            if (orgId == Guid.Empty || branchId == Guid.Empty) return;
            if (string.IsNullOrWhiteSpace(username)) return;

            var payload = JsonSerializer.Serialize(new Payload { UserName = username.Trim() });
            var plain = Encoding.UTF8.GetBytes(payload);
            var protectedBytes = ProtectedData.Protect(plain, Entropy(orgId, branchId), DataProtectionScope.CurrentUser);
            File.WriteAllBytes(FilePath(orgId, branchId), protectedBytes);
        }

        public static string? LoadLastUsername(Guid orgId, Guid branchId)
        {
            try
            {
                if (orgId == Guid.Empty || branchId == Guid.Empty) return null;
                var path = FilePath(orgId, branchId);
                if (!File.Exists(path)) return null;

                var blob = File.ReadAllBytes(path);
                var plain = ProtectedData.Unprotect(blob, Entropy(orgId, branchId), DataProtectionScope.CurrentUser);
                var p = JsonSerializer.Deserialize<Payload>(plain);
                return string.IsNullOrWhiteSpace(p?.UserName) ? null : p!.UserName!.Trim();
            }
            catch
            {
                return null;
            }
        }

        private sealed class Payload
        {
            public string? UserName { get; set; }
        }
    }
}
