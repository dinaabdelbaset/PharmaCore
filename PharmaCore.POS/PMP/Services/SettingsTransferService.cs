using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services
{
    internal static class SettingsTransferService
    {
        private sealed class ExportModel{ public ConnectionSecrets? Secrets {get;set;} public string? ActivationCode {get;set;} }
        public static void Export(string path, string password)
        {
            var model = new ExportModel{ Secrets = ConnectionSecretsStore.Load(), ActivationCode = ActivationStorage.Load() };
            var json = JsonSerializer.Serialize(model);
            var blob = CryptoUtil.EncryptAesGcm(Encoding.UTF8.GetBytes(json), password, out var salt, out var nonce);
            using var fs = File.Create(path);
            fs.Write(salt,0,salt.Length); fs.Write(nonce,0,nonce.Length); fs.Write(blob,0,blob.Length);
        }
        public static bool Import(string path, string password)
        {
            var data = File.ReadAllBytes(path);
            var salt = new byte[16]; var nonce = new byte[12];
            Buffer.BlockCopy(data,0,salt,0,16); Buffer.BlockCopy(data,16,nonce,0,12);
            var blob = new byte[data.Length-28]; Buffer.BlockCopy(data,28,blob,0,blob.Length);
            var json = Encoding.UTF8.GetString(CryptoUtil.DecryptAesGcm(blob, password, salt, nonce));
            var model = JsonSerializer.Deserialize<ExportModel>(json); if(model==null) return false;
            if(model.Secrets!=null) ConnectionSecretsStore.Save(model.Secrets);
            if(!string.IsNullOrWhiteSpace(model.ActivationCode)) ActivationStorage.Save(model.ActivationCode);
            return true;
        }
    }
}