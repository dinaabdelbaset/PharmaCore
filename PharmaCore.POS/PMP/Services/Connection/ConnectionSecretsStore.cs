using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services.Connection
{
    internal static class ConnectionSecretsStore
    {
        private const string AppKeySeed = "PharmaCore-ConnKey-2025";
        private static readonly byte[] AppKey = SHA256.HashData(Encoding.UTF8.GetBytes(AppKeySeed));
        private static readonly string _mainPath;
        private static readonly string _bakPath;
        private static readonly string _oldPath;
        static ConnectionSecretsStore()
        {
            var app = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _mainPath = Path.Combine(app, "pharmacore.conn");
            _bakPath = Path.Combine(local, ".cache", "pharmacore.conn.bak");
            Directory.CreateDirectory(Path.GetDirectoryName(_bakPath)!);
            _oldPath = Path.Combine(app, "pharmacore.conn.old");
        }
        private record Envelope(int Ver, string Json, string Salt, string Hmac);
        public static void Save(ConnectionSecrets s)
        {
            var (ok, reason) = SecretsValidator.Validate(s); if (!ok) throw new InvalidOperationException(reason);
            var json = JsonSerializer.Serialize(s);
            var salt = RandomNumberGenerator.GetBytes(16);
            var hmacBytes = ComputeHmac(salt, json);
            var env = new Envelope(1, json, Convert.ToBase64String(salt), Convert.ToBase64String(hmacBytes));
            var payload = JsonSerializer.Serialize(env);
            var protectedBytes = Protect(payload, "conn");
            File.WriteAllBytes(_mainPath, protectedBytes);
            File.WriteAllBytes(_bakPath, protectedBytes);
        }
        public static ConnectionSecrets? Load()
        {
            foreach (var path in new[] { _mainPath, _bakPath, _oldPath })
            {
                if (!File.Exists(path)) continue;
                try
                {
                    var raw = File.ReadAllBytes(path);
                    var json = Unprotect(raw, "conn");
                    var env = JsonSerializer.Deserialize<Envelope>(json);
                    if (env == null) continue;
                    var salt = Convert.FromBase64String(env.Salt);
                    var expected = ComputeHmac(salt, env.Json);
                    var actual = Convert.FromBase64String(env.Hmac);
                    if (!FixedTimeEquals(expected, actual)) continue;
                    var secrets = JsonSerializer.Deserialize<ConnectionSecrets>(env.Json);
                    if (secrets == null) continue;
                    var (ok, _) = SecretsValidator.Validate(secrets); if (!ok) continue;
                    if (path != _mainPath) Save(secrets); // auto-restore
                    return secrets;
                }
                catch { }
            }
            return null;
        }
        public static void Reset(){ TryDelete(_mainPath); TryDelete(_bakPath); TryDelete(_oldPath); }
        private static byte[] ComputeHmac(byte[] salt, string json){ using var h = new HMACSHA256(AppKey); h.TransformBlock(salt, 0, salt.Length, null, 0); var data = Encoding.UTF8.GetBytes(json); h.TransformFinalBlock(data, 0, data.Length); return h.Hash!; }
        private static bool FixedTimeEquals(byte[] a, byte[] b){ if (a.Length != b.Length) return false; int diff = 0; for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i]; return diff == 0; }
        private static byte[] Protect(string payload, string purpose){ var plain = Encoding.UTF8.GetBytes(payload); return ProtectedData.Protect(plain, EnvInfo.GetEntropy(purpose), DataProtectionScope.CurrentUser); }
        private static string Unprotect(byte[] blob, string purpose){ var plain = ProtectedData.Unprotect(blob, EnvInfo.GetEntropy(purpose), DataProtectionScope.CurrentUser); return Encoding.UTF8.GetString(plain); }
        private static void TryDelete(string path){ try { if (File.Exists(path)) File.Delete(path); } catch { } }
    }
}
