using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services.Security
{
    internal static class ActivationStorage
    {
        private static readonly string _roam = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore.activation");
        private static readonly string _local = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pharmacore.activation.bak");
        private const string KdfSeed = "PharmaCore-Activation-2025";

        public static void Save(string code)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var key = SHA256.HashData(Encoding.UTF8.GetBytes(Environment.MachineName + KdfSeed));
            using var h1 = new HMACSHA256(key);
            using var h2 = new HMACSHA256(h1.ComputeHash(salt));
            var payload = Encoding.UTF8.GetBytes(code);
            var mac1 = h1.ComputeHash(payload);
            var mac2 = h2.ComputeHash(mac1);
            var blob = new byte[salt.Length + mac1.Length + mac2.Length + payload.Length];
            Buffer.BlockCopy(salt,0,blob,0,salt.Length);
            Buffer.BlockCopy(mac1,0,blob,salt.Length,mac1.Length);
            Buffer.BlockCopy(mac2,0,blob,salt.Length+mac1.Length,mac2.Length);
            Buffer.BlockCopy(payload,0,blob,salt.Length+mac1.Length+mac2.Length,payload.Length);
            var prot = ProtectedData.Protect(blob, GetEntropy(), DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_roam, prot);
            File.WriteAllBytes(_local, prot);
        }
        public static string? Load()
        {
            foreach(var path in new[]{_roam,_local})
            {
                if(!File.Exists(path)) continue;
                try
                {
                    var raw = File.ReadAllBytes(path);
                    var plain = ProtectedData.Unprotect(raw, GetEntropy(), DataProtectionScope.CurrentUser);
                    if(plain.Length < 16+32+32+1) continue;
                    var salt = new byte[16]; Buffer.BlockCopy(plain,0,salt,0,16);
                    var mac1 = new byte[32]; Buffer.BlockCopy(plain,16,mac1,0,32);
                    var mac2 = new byte[32]; Buffer.BlockCopy(plain,48,mac2,0,32);
                    var payload = new byte[plain.Length-16-32-32]; Buffer.BlockCopy(plain,80,payload,0,payload.Length);
                    var key = SHA256.HashData(Encoding.UTF8.GetBytes(Environment.MachineName + KdfSeed));
                    using var h1 = new HMACSHA256(key);
                    using var h2 = new HMACSHA256(h1.ComputeHash(salt));
                    var check1 = h1.ComputeHash(payload);
                    var check2 = h2.ComputeHash(check1);
                    if(!FixedTimeEquals(mac1, check1) || !FixedTimeEquals(mac2, check2)) continue;
                    var code = Encoding.UTF8.GetString(payload);
                    if(path!=_roam) Save(code); // auto-restore
                    return code;
                }
                catch { }
            }
            return null;
        }
        public static void Reset(){ TryDelete(_roam); TryDelete(_local); }
        private static bool FixedTimeEquals(byte[] a, byte[] b){ if(a.Length!=b.Length) return false; int diff=0; for(int i=0;i<a.Length;i++) diff |= a[i]^b[i]; return diff==0; }
        private static byte[] GetEntropy(){ return SHA256.HashData(Encoding.UTF8.GetBytes(Environment.UserName+"|activation|PharmaCore")); }
        private static void TryDelete(string p){ try{ if(File.Exists(p)) File.Delete(p);} catch{} }
    }
}
