using System;
using System.IO;
using System.Text;

namespace PharmaCore.POS.Services
{
    internal interface IConnectionStringProvider
    {
        string GetConnectionString();
        void SetAndPersist(ConnectionSecrets secrets);
        void SetProfile(string profile);
    }

    internal sealed class ConnectionStringProvider : IConnectionStringProvider
    {
        private static readonly string ProfileFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore_profile.txt");

        private string? _cached;
        private string _profile;

        public ConnectionStringProvider()
        {
            // Load last-used profile from persistent file, fallback to Properties, then "Production"
            _profile = LoadPersistedProfile()
                ?? Properties.Settings.Default.ApiAuth?.Trim()
                ?? "Production";
            if (string.IsNullOrWhiteSpace(_profile)) _profile = "Production";
        }

        public void SetProfile(string profile)
        {
            _profile = string.IsNullOrWhiteSpace(profile) ? "Production" : profile;
            _cached = null;
            SavePersistedProfile(_profile);
        }

        public string GetConnectionString()
        {
            if (_cached != null) return _cached;
            var s = MultiProfileConnectionSecretsStore.LoadProfile(_profile) ?? ConnectionSecretsStore.Load();
            if (s == null) return string.Empty;
            var ssl = s.SslRequire ? "SSL Mode=Require;" : string.Empty;
            var trust = s.TrustServerCert ? "Trust Server Certificate=true;" : string.Empty;
            _cached = $"Host={s.Host};Port={s.Port};Database={s.Database};Username={s.Username};Password={s.Password};{ssl}{trust}";
            return _cached;
        }

        public void SetAndPersist(ConnectionSecrets secrets)
        {
            MultiProfileConnectionSecretsStore.SaveProfile(_profile, secrets);
            _cached = null;
        }

        private static string? LoadPersistedProfile()
        {
            try { if (File.Exists(ProfileFilePath)) { var p = File.ReadAllText(ProfileFilePath).Trim(); return string.IsNullOrWhiteSpace(p) ? null : p; } } catch { }
            return null;
        }

        private static void SavePersistedProfile(string profile)
        {
            try { File.WriteAllText(ProfileFilePath, profile); } catch { }
        }
    }
}