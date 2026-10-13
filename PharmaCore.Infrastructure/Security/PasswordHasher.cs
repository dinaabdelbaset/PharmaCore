using System;
using System.Security.Cryptography;

namespace PharmaCore.Infrastructure.Security
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var derived = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
            var result = new byte[1 + salt.Length + derived.Length];
            result[0] = 0x01; // version
            Buffer.BlockCopy(salt, 0, result, 1, salt.Length);
            Buffer.BlockCopy(derived, 0, result, 1 + salt.Length, derived.Length);
            return Convert.ToBase64String(result);
        }

        public static bool Verify(string hash, string password)
        {
            try
            {
                var data = Convert.FromBase64String(hash);
                if (data[0] != 0x01) return false;
                var salt = new byte[16];
                Buffer.BlockCopy(data, 1, salt, 0, salt.Length);
                var stored = new byte[32];
                Buffer.BlockCopy(data, 1 + salt.Length, stored, 0, stored.Length);
                var derived = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100_000, HashAlgorithmName.SHA256, 32);
                return CryptographicOperations.FixedTimeEquals(stored, derived);
            }
            catch
            {
                return false;
            }
        }
    }
}
