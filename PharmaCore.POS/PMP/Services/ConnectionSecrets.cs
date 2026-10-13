using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PharmaCore.POS.Services
{
    internal sealed class ConnectionSecrets
    {
        public string Host { get; set; } = "";
        public int Port { get; set; } = 5432;
        public string Database { get; set; } = "pharmacore";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool SslRequire { get; set; } = true;
        public bool TrustServerCert { get; set; } = true;
    }

    internal static class SecretsValidator
    {
        private static readonly Regex HostRx = new("^([a-zA-Z0-9.-]+|\\d{1,3}(?:\\.\\d{1,3}){3})$");
        public static (bool ok,string reason) Validate(ConnectionSecrets s)
        {
            if (string.IsNullOrWhiteSpace(s.Host) || !HostRx.IsMatch(s.Host)) return (false, "Host غير صالح");
            if (s.Port < 1 || s.Port > 65535) return (false, "Port غير صالح");
            if (string.IsNullOrWhiteSpace(s.Database)) return (false, "Database فارغ");
            if (string.IsNullOrWhiteSpace(s.Username) || s.Username.Contains(' ')) return (false, "Username غير صالح");
            if (string.IsNullOrWhiteSpace(s.Password)) return (false, "Password فارغ");
            return (true, "OK");
        }
    }

    internal static class ConnectionSecretsStore
    {
        private static readonly string PathFull = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore.conn");
        private static readonly string BackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".cache", "pharmacore.conn.bak");
        private static readonly string OldPath = PathFull + ".old"; // optional manual backup name
        private static readonly byte[] AppKey = SHA256.HashData(Encoding.UTF8.GetBytes("PharmaCore-ConnKey-2025"));
        private const int SaltSize = 16;
        private sealed class Payload { public int Ver {get;set;}=1; public string Json { get; set; } = ""; public string Salt { get; set; } = ""; public string Hmac { get; set; } = ""; }
        public static void Save(ConnectionSecrets s)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(BackupPath)!);
            var (ok,reason) = SecretsValidator.Validate(s); if(!ok) throw new InvalidOperationException(reason);
            var jsonPlain = JsonSerializer.Serialize(s);
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hmac = ComputeHmac(jsonPlain, salt);
            var payload = new Payload { Ver=1, Json = jsonPlain, Salt = Convert.ToBase64String(salt), Hmac = Convert.ToBase64String(hmac) };
            var json = JsonSerializer.Serialize(payload);
            var prot = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), EnvInfo.GetEntropy("conn"), DataProtectionScope.CurrentUser);
            File.WriteAllBytes(PathFull, prot);
            try { File.WriteAllBytes(BackupPath, prot); } catch { }
        }
        public static ConnectionSecrets? Load()
        {
            foreach (var p in new[]{PathFull, BackupPath, OldPath})
            {
                try
                {
                    if (!File.Exists(p)) continue;
                    var prot = File.ReadAllBytes(p);
                    var jsonBytes = ProtectedData.Unprotect(prot, EnvInfo.GetEntropy("conn"), DataProtectionScope.CurrentUser);
                    var payload = JsonSerializer.Deserialize<Payload>(jsonBytes);
                    if (payload == null) continue;
                    var salt = Convert.FromBase64String(payload.Salt);
                    var expected = ComputeHmac(payload.Json, salt);
                    if (!CryptographicOperations.FixedTimeEquals(expected, Convert.FromBase64String(payload.Hmac))) continue;
                    var secrets = JsonSerializer.Deserialize<ConnectionSecrets>(payload.Json);
                    if (secrets == null) continue;
                    var v = SecretsValidator.Validate(secrets); if(!v.ok) continue;
                    // auto-restore if loaded from fallback
                    if (p != PathFull)
                    {
                        try { Save(secrets); } catch { }
                    }
                    return secrets;
                }
                catch { }
            }
            return null;
        }
        public static void Reset()
        {
            try { if (File.Exists(PathFull)) File.Delete(PathFull); } catch { }
            try { if (File.Exists(BackupPath)) File.Delete(BackupPath); } catch { }
            try { if (File.Exists(OldPath)) File.Delete(OldPath); } catch { }
        }
        private static byte[] ComputeHmac(string data, byte[] salt)
        {
            using var h = new HMACSHA256(AppKey);
            var db = Encoding.UTF8.GetBytes(data);
            var buf = new byte[salt.Length + db.Length];
            Buffer.BlockCopy(salt,0,buf,0,salt.Length);
            Buffer.BlockCopy(db,0,buf,salt.Length,db.Length);
            return h.ComputeHash(buf);
        }
    }
}