using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.ActivationManager
{
    /// <summary>
    /// Central place for the control (master) database connection string.
    /// All ActivationManager screens read from here instead of hardcoding.
    /// The value is stored encrypted on disk so it persists across app restarts.
    /// </summary>
    internal static class ControlDbConnection
    {
        private static readonly string _filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "pharmacore_control_db.dat");

        private static readonly byte[] _entropy =
            SHA256.HashData(Encoding.UTF8.GetBytes(
                Environment.UserName + "|control-db|PharmaCore-Admin-2025"));

        private static string? _cached;

        /// <summary>
        /// Get the control DB connection string. Returns cached value or loads from disk.
        /// Validates format before returning. Throws if not configured or corrupt.
        /// </summary>
        public static string Get()
        {
            if (!string.IsNullOrWhiteSpace(_cached) && IsValidFormat(_cached))
                return _cached;

            var loaded = LoadFromDisk();
            if (!string.IsNullOrWhiteSpace(loaded))
            {
                if (IsValidFormat(loaded))
                {
                    _cached = loaded;
                    return _cached;
                }
                // Corrupt file — delete it so IsConfigured returns false
                Reset();
            }

            throw new InvalidOperationException(
                "لم يتم ضبط اتصال قاعدة البيانات بعد.\n" +
                "يرجى ضبط الاتصال من زر ⚙ DB Settings.");
        }

        /// <summary>
        /// Try to get connection string without throwing.
        /// </summary>
        public static bool TryGet(out string cs)
        {
            try { cs = Get(); return true; }
            catch { cs = string.Empty; return false; }
        }

        /// <summary>
        /// Save new connection string (persists to encrypted file + cache).
        /// </summary>
        public static void Set(string connectionString)
        {
            if (!IsValidFormat(connectionString))
                throw new ArgumentException("صيغة الاتصال غير صحيحة");
            _cached = connectionString;
            SaveToDisk(connectionString);
        }

        /// <summary>
        /// Check if connection string is explicitly configured AND valid.
        /// </summary>
        public static bool IsConfigured
        {
            get
            {
                if (!File.Exists(_filePath)) return false;
                var loaded = LoadFromDisk();
                return !string.IsNullOrWhiteSpace(loaded) && IsValidFormat(loaded);
            }
        }

        /// <summary>
        /// Delete stored config and clear cache (forces re-setup).
        /// </summary>
        public static void Reset()
        {
            _cached = null;
            try { if (File.Exists(_filePath)) File.Delete(_filePath); } catch { }
        }

        /// <summary>
        /// Validate connection string format using Npgsql parser.
        /// </summary>
        private static bool IsValidFormat(string cs)
        {
            try
            {
                _ = new Npgsql.NpgsqlConnectionStringBuilder(cs);
                return true;
            }
            catch { return false; }
        }

        private static void SaveToDisk(string cs)
        {
            try
            {
                var json = JsonSerializer.Serialize(new { Cs = cs });
                var plain = Encoding.UTF8.GetBytes(json);
                var prot = ProtectedData.Protect(plain, _entropy, DataProtectionScope.CurrentUser);
                File.WriteAllBytes(_filePath, prot);
            }
            catch { /* fallback: still has cached value */ }
        }

        private static string? LoadFromDisk()
        {
            try
            {
                if (!File.Exists(_filePath)) return null;
                var prot = File.ReadAllBytes(_filePath);
                var plain = ProtectedData.Unprotect(prot, _entropy, DataProtectionScope.CurrentUser);
                var doc = JsonDocument.Parse(plain);
                return doc.RootElement.GetProperty("Cs").GetString();
            }
            catch { return null; }
        }
    }
}
