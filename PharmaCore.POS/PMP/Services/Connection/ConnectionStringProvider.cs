using System;
using System.Linq;
using System.Threading;

namespace PharmaCore.POS.Services.Connection
{
    internal sealed class ConnectionStringProvider
    {
        private static readonly Lazy<ConnectionStringProvider> _lazy = new(() => new ConnectionStringProvider());
        public static ConnectionStringProvider Instance => _lazy.Value;
        private string? _currentProfile;
        private string? _cached;
        private readonly ReaderWriterLockSlim _lock = new();
        public void SetProfile(string profile)
        {
            _lock.EnterWriteLock();
            try
            {
                _currentProfile = profile;
                _cached = null; // invalidate
            }
            finally { _lock.ExitWriteLock(); }
        }
        public void SetAndPersist(ConnectionSecrets secrets)
        {
            MultiProfileConnectionSecretsStore.SaveProfile(_currentProfile ?? "Default", secrets);
            _cached = null;
        }
        public string? GetConnectionString()
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (_cached != null) return _cached;
                var profile = _currentProfile ?? MultiProfileConnectionSecretsStore.Profiles.FirstOrDefault();
                if (profile == null) return null;
                var secrets = MultiProfileConnectionSecretsStore.LoadProfile(profile);
                if (secrets == null) return null;
                var ssl = secrets.SslRequire ? "SSL Mode=Require;" : "SSL Mode=Disable;";
                if (secrets.TrustServerCert) ssl += "Trust Server Certificate=true;";
                var cs = $"Host={secrets.Host};Port={secrets.Port};Database={secrets.Database};Username={secrets.Username};Password={secrets.Password};{ssl}";
                _lock.EnterWriteLock();
                try { _cached = cs; }
                finally { _lock.ExitWriteLock(); }
                return cs;
            }
            finally { _lock.ExitUpgradeableReadLock(); }
        }
    }
}
