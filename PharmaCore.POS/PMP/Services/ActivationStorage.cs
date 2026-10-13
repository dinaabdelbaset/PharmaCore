using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services
{
    internal static class ActivationStorage
    {
        private const string FileName = "pharmacore_activation.bin";
        private static readonly string PathFull = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), FileName);
        private static readonly string BackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".cache", "pharmacore_activation.bak");
        private static readonly byte[] AppKey = SHA256.HashData(Encoding.UTF8.GetBytes("PharmaCore-Activation-AppKey-2025"));
        private const int SaltSize = 16;
        private sealed class Payload { public int Ver {get;set;}=1; public string Code { get; set; } = string.Empty; public string SaltBase64 { get; set; } = string.Empty; public string HmacBase64 { get; set; } = string.Empty; }

        public static void Save(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return;
            Directory.CreateDirectory(Path.GetDirectoryName(BackupPath)!);
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hmac = ComputeHmac(code.Trim(), salt);
            var payload = new Payload { Ver=1, Code = code.Trim(), SaltBase64 = Convert.ToBase64String(salt), HmacBase64 = Convert.ToBase64String(hmac) };
            var json = JsonSerializer.Serialize(payload);
            var prot = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), EnvInfo.GetEntropy("activation"), DataProtectionScope.CurrentUser);
            File.WriteAllBytes(PathFull, prot);
            try { File.WriteAllBytes(BackupPath, prot); } catch { }
        }

        public static string? Load()
        {
            if (!File.Exists(PathFull) && !File.Exists(BackupPath)) return null;
            foreach (var p in new[]{PathFull, BackupPath})
            {
                try
                {
                    if (!File.Exists(p)) continue;
                    var prot = File.ReadAllBytes(p);
                    var jsonBytes = ProtectedData.Unprotect(prot, EnvInfo.GetEntropy("activation"), DataProtectionScope.CurrentUser);
                    var payload = JsonSerializer.Deserialize<Payload>(jsonBytes);
                    if (payload == null) continue;
                    var salt = Convert.FromBase64String(payload.SaltBase64);
                    var expected = ComputeHmac(payload.Code, salt);
                    if (!CryptographicOperations.FixedTimeEquals(expected, Convert.FromBase64String(payload.HmacBase64))) continue;
                    return payload.Code;
                }
                catch { }
            }
            return null;
        }

        public static void Clear()
        {
            try { if (File.Exists(PathFull)) File.Delete(PathFull); } catch { }
            try { if (File.Exists(BackupPath)) File.Delete(BackupPath); } catch { }
        }

        private static byte[] ComputeHmac(string code, byte[] salt)
        {
            using var h = new HMACSHA256(AppKey);
            var data = new byte[salt.Length + Encoding.UTF8.GetByteCount(code)];
            Buffer.BlockCopy(salt,0,data,0,salt.Length);
            Encoding.UTF8.GetBytes(code,0,code.Length,data,salt.Length);
            return h.ComputeHash(data);
        }
    }
}