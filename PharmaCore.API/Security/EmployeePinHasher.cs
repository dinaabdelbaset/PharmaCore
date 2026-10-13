using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.API.Security
{
    internal static class EmployeePinHasher
    {
        // Format: argon2id:v1:m=<memKiB>,t=<iters>,p=<lanes>:<saltB64>:<hashB64>
        private const string Prefix = "argon2id:v1";

        // Tuned for server-side verification. Adjust via config later if needed.
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int MemoryKiB = 64 * 1024; // 64 MiB
        private const int Iterations = 3;
        private const int Parallelism = 2;

        public static string Hash(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin)) throw new ArgumentException("PIN is empty", nameof(pin));

            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Compute(pin, salt, MemoryKiB, Iterations, Parallelism);

            return $"{Prefix}:m={MemoryKiB},t={Iterations},p={Parallelism}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string pin, string stored)
        {
            if (string.IsNullOrWhiteSpace(pin) || string.IsNullOrWhiteSpace(stored)) return false;

            if (!stored.StartsWith(Prefix + ":", StringComparison.Ordinal))
                return false;

            try
            {
                // argon2id:v1:m=...,t=...,p=...:<salt>:<hash>
                var parts = stored.Split(':');
                if (parts.Length != 5) return false;

                var paramPart = parts[2];
                var saltB64 = parts[3];
                var hashB64 = parts[4];

                var mem = MemoryKiB;
                var iters = Iterations;
                var lanes = Parallelism;

                foreach (var kv in paramPart.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    var seg = kv.Split('=', 2);
                    if (seg.Length != 2) continue;
                    if (seg[0] == "m") mem = int.Parse(seg[1]);
                    else if (seg[0] == "t") iters = int.Parse(seg[1]);
                    else if (seg[0] == "p") lanes = int.Parse(seg[1]);
                }

                var salt = Convert.FromBase64String(saltB64);
                var storedHash = Convert.FromBase64String(hashB64);
                var computed = Compute(pin, salt, mem, iters, lanes, storedHash.Length);
                return CryptographicOperations.FixedTimeEquals(storedHash, computed);
            }
            catch
            {
                return false;
            }
        }

        private static byte[] Compute(string pin, byte[] salt, int memKiB, int iters, int lanes, int hashSize = HashSize)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(pin))
            {
                Salt = salt,
                MemorySize = memKiB,
                Iterations = iters,
                DegreeOfParallelism = lanes
            };
            return argon2.GetBytes(hashSize);
        }
    }
}
