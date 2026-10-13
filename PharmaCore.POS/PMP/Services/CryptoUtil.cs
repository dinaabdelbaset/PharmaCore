using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services
{
    internal static class CryptoUtil
    {
        public static byte[] EncryptAesGcm(byte[] plain, string password, out byte[] salt, out byte[] nonce)
        {
            salt = RandomNumberGenerator.GetBytes(16);
            nonce = RandomNumberGenerator.GetBytes(12);
            using var kdf = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var key = kdf.GetBytes(32);
            var cipher = new byte[plain.Length];
            var tag = new byte[16];
            using var gcm = new AesGcm(key);
            gcm.Encrypt(nonce, plain, cipher, tag);
            using var ms = new MemoryStream();
            ms.Write(tag,0,tag.Length); ms.Write(cipher,0,cipher.Length);
            return ms.ToArray();
        }
        public static byte[] DecryptAesGcm(byte[] blob, string password, byte[] salt, byte[] nonce)
        {
            using var kdf = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            var key = kdf.GetBytes(32);
            var tag = new byte[16];
            Buffer.BlockCopy(blob,0,tag,0,16);
            var cipher = new byte[blob.Length-16];
            Buffer.BlockCopy(blob,16,cipher,0,cipher.Length);
            var plain = new byte[cipher.Length];
            using var gcm = new AesGcm(key);
            gcm.Decrypt(nonce, cipher, tag, plain);
            return plain;
        }
    }
}