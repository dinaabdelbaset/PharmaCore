using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PharmaCore.POS.Services.Connection;

namespace PharmaCore.POS.Services.Security
{
    internal static class SettingsTransferService
    {
        private record ExportEnvelope(int Ver,string Salt,string Nonce,string Cipher,string Tag);
        private record ImportDto(System.Collections.Generic.Dictionary<string,ConnectionSecrets?>? Profiles,string? ActivationCode);
        private const int Iterations = 120_000;
        public static void Export(string filePath, string password)
        {
            var all = MultiProfileConnectionSecretsStore.LoadAll();
            var act = ActivationStorage.Load();
            var blob = JsonSerializer.Serialize(new { Profiles = all, ActivationCode = act });
            var salt = RandomNumberGenerator.GetBytes(16); var nonce = RandomNumberGenerator.GetBytes(12);
            using var kdf = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256); var key = kdf.GetBytes(32);
            var plain = Encoding.UTF8.GetBytes(blob); var cipher = new byte[plain.Length]; var tag = new byte[16]; using var gcm = new AesGcm(key); gcm.Encrypt(nonce, plain, cipher, tag);
            var env = new ExportEnvelope(1, Convert.ToBase64String(salt), Convert.ToBase64String(nonce), Convert.ToBase64String(cipher), Convert.ToBase64String(tag));
            File.WriteAllText(filePath, JsonSerializer.Serialize(env), Encoding.UTF8);
        }
        public static bool Import(string filePath, string password)
        {
            try
            {
                var text = File.ReadAllText(filePath, Encoding.UTF8);
                var env = JsonSerializer.Deserialize<ExportEnvelope>(text) ?? throw new InvalidOperationException("ملف تالف");
                var salt = Convert.FromBase64String(env.Salt); var nonce = Convert.FromBase64String(env.Nonce); var cipher = Convert.FromBase64String(env.Cipher); var tag = Convert.FromBase64String(env.Tag);
                using var kdf = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256); var key = kdf.GetBytes(32);
                var plain = new byte[cipher.Length]; using var gcm = new AesGcm(key); gcm.Decrypt(nonce, cipher, tag, plain); var json = Encoding.UTF8.GetString(plain);
                var dto = JsonSerializer.Deserialize<ImportDto>(json) ?? throw new InvalidOperationException("بيانات غير صالحة");
                MultiProfileConnectionSecretsStore.ResetAll();
                if(dto.Profiles != null){ foreach(var kv in dto.Profiles){ if(kv.Value!=null) MultiProfileConnectionSecretsStore.SaveProfile(kv.Key, kv.Value); } }
                if(!string.IsNullOrWhiteSpace(dto.ActivationCode)) ActivationStorage.Save(dto.ActivationCode);
                return true;
            }
            catch { return false; }
        }
    }
}
