using System;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services.Security
{
    internal static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 120_000;
        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var pbkdf = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf.GetBytes(KeySize);
            return $"PBKDF2$sha256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
        }
        public static bool Verify(string password, string encoded)
        {
            try
            {
                var parts = encoded.Split('$');
                if (parts.Length != 5) return false;
                if (parts[0] != "PBKDF2" || parts[1] != "sha256") return false;
                var iters = int.Parse(parts[2]);
                var salt = Convert.FromBase64String(parts[3]);
                var expected = Convert.FromBase64String(parts[4]);
                using var pbkdf = new Rfc2898DeriveBytes(password, salt, iters, HashAlgorithmName.SHA256);
                var actual = pbkdf.GetBytes(expected.Length);
                return FixedTimeEquals(expected, actual);
            }
            catch { return false; }
        }
        private static bool FixedTimeEquals(byte[] a, byte[] b){ if(a.Length!=b.Length) return false; int diff=0; for(int i=0;i<a.Length;i++) diff |= a[i]^b[i]; return diff==0; }
    }
}
