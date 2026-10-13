using Isopoh.Cryptography.Argon2;

namespace PharmaCore.ActivationManager.Security
{
    internal static class Argon2Hasher
    {
        /// <summary>
        /// Hash password using standard Isopoh Argon2id format ("$argon2id$...").
        /// This MUST match the format expected by PharmaCore.API's BranchPasswordHasher.Verify.
        /// </summary>
        public static string Hash(string password)
        {
            // Produces standard "$argon2id$v=19$..." string
            return Argon2.Hash(password);
        }
    }
}
