using Isopoh.Cryptography.Argon2;

namespace PharmaCore.API.Security
{
    internal static class BranchPasswordHasher
    {
        // Keep compatible with existing BranchUsers hashes (Isopoh Argon2 format).
        public static string Hash(string password) => Argon2.Hash(password);

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash)) return false;
            try
            {
                // Stored hashes are usually "$argon2id$...".
                return Argon2.Verify(storedHash, password);
            }
            catch
            {
                return false;
            }
        }
    }
}
