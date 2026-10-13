using System;
using System.IO;

namespace PharmaCore.POS.Services
{
    /// <summary>
    /// Persists key app settings (ApiBase, ApiToken, etc.) to %AppData% files
    /// so they survive Visual Studio rebuilds (unlike Properties.Settings.Default).
    /// </summary>
    internal static class AppSettingsStore
    {
        private static readonly string Dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore");

        private static string FilePath(string key) => Path.Combine(Dir, $"pharmacore_{key}.txt");

        public static void Set(string key, string value)
        {
            try
            {
                Directory.CreateDirectory(Dir);
                File.WriteAllText(FilePath(key), value ?? string.Empty);
            }
            catch { }
        }

        public static string? Get(string key)
        {
            try
            {
                var path = FilePath(key);
                if (!File.Exists(path)) return null;
                var val = File.ReadAllText(path).Trim();
                return string.IsNullOrWhiteSpace(val) ? null : val;
            }
            catch { return null; }
        }

        public static void Remove(string key)
        {
            try { var p = FilePath(key); if (File.Exists(p)) File.Delete(p); } catch { }
        }
    }
}
