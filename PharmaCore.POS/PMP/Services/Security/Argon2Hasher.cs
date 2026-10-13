using System;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;

namespace PharmaCore.POS.Services.Security
{
    internal static class Argon2Hasher
    {
        // Parameters chosen for desktop app (can adjust memory/time cost depending on environment)
        private const int SaltSize = 16; // bytes
        private const int MemoryKb = 64 * 1024; // 64MB
        private const int Iterations = 3; // time cost
        private const int DegreeOfParallelism = 2; // lanes
        private const int HashLength = 32; // output bytes

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = Iterations,
                MemoryCost = MemoryKb,
                Lanes = DegreeOfParallelism,
                Threads = DegreeOfParallelism,
                Password = Encoding.UTF8.GetBytes(password),
                Salt = salt,
                HashLength = HashLength
            };
            using var argon2 = new Argon2(config);
            using var hash = argon2.Hash();
            // SecureArray<byte> -> byte[] via Buffer property
            var raw = hash.Buffer;
            return config.EncodeString(raw);
        }

        public static bool Verify(string password, string encoded)
        {
            try { return Argon2.Verify(encoded, password); } catch { return false; }
        }
    }
}
