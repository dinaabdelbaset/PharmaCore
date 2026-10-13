using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;

namespace PharmaCore.POS.Services
{
    internal static class MultiProfileConnectionSecretsStore
    {
        private static readonly string PathFull = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pharmacore.multi.conn.sec");
        private static Dictionary<string, ConnectionSecrets> _cache = new(StringComparer.OrdinalIgnoreCase);
        private static void PersistEncrypted()
        {
            try
            {
                var json = JsonSerializer.Serialize(_cache);
                var prot = ProtectedData.Protect(System.Text.Encoding.UTF8.GetBytes(json), EnvInfo.GetEntropy("multi-conn"), DataProtectionScope.CurrentUser);
                File.WriteAllBytes(PathFull, prot);
            }
            catch { }
        }
        public static IReadOnlyDictionary<string, ConnectionSecrets> LoadAll()
        {
            if (_cache.Count > 0) return _cache;
            if (!File.Exists(PathFull)) return _cache;
            try
            {
                var prot = File.ReadAllBytes(PathFull);
                var jsonBytes = ProtectedData.Unprotect(prot, EnvInfo.GetEntropy("multi-conn"), DataProtectionScope.CurrentUser);
                _cache = JsonSerializer.Deserialize<Dictionary<string, ConnectionSecrets>>(jsonBytes) ?? new();
            }
            catch { }
            return _cache;
        }
        public static ConnectionSecrets? LoadProfile(string profile)
        {
            LoadAll(); return _cache.TryGetValue(profile, out var v) ? v : null;
        }
        public static void SaveProfile(string profile, ConnectionSecrets secrets)
        {
            LoadAll(); _cache[profile] = secrets; PersistEncrypted(); ConnectionSecretsStore.Save(secrets);
        }
        public static void ResetProfile(string profile)
        {
            LoadAll(); if (_cache.Remove(profile)) PersistEncrypted();
        }
        public static void ResetAll(){
            try { if (File.Exists(PathFull)) File.Copy(PathFull, PathFull + ".old", true); } catch { }
            _cache.Clear(); PersistEncrypted(); ConnectionSecretsStore.Reset(); }
    }
}