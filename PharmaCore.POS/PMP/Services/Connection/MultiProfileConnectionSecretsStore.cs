using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace PharmaCore.POS.Services.Connection
{
    internal static class MultiProfileConnectionSecretsStore
    {
        private static readonly string _file;
        private static readonly Dictionary<string, ConnectionSecrets> _cache = new(StringComparer.OrdinalIgnoreCase);
        static MultiProfileConnectionSecretsStore(){ var app = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); _file = Path.Combine(app, "pharmacore.multi.conn.sec"); LoadInternal(); }
        private record Envelope(int Ver, string Json);
        private static void LoadInternal(){ if(!File.Exists(_file)) return; try{ var raw = File.ReadAllBytes(_file); var json = Unprotect(raw); var env = JsonSerializer.Deserialize<Envelope>(json); if(env==null) return; var dict = JsonSerializer.Deserialize<Dictionary<string,ConnectionSecrets>>(env.Json); if(dict!=null){ _cache.Clear(); foreach(var kv in dict){ var (ok,_) = SecretsValidator.Validate(kv.Value); if(ok) _cache[kv.Key]=kv.Value; } } } catch { } }
        private static void Persist(){ var env = new Envelope(1, JsonSerializer.Serialize(_cache)); var json = JsonSerializer.Serialize(env); var prot = Protect(json); File.WriteAllBytes(_file, prot); }
        public static IEnumerable<string> Profiles => _cache.Keys;
        public static IReadOnlyDictionary<string, ConnectionSecrets> LoadAll() => new Dictionary<string, ConnectionSecrets>(_cache);
        public static ConnectionSecrets? LoadProfile(string profile){ _cache.TryGetValue(profile, out var s); return s; }
        public static void SaveProfile(string profile, ConnectionSecrets secrets){ var (ok, reason) = SecretsValidator.Validate(secrets); if(!ok) throw new InvalidOperationException(reason); _cache[profile]=secrets; Persist(); ConnectionSecretsStore.Save(secrets); }
        public static void ResetProfile(string profile){ if(_cache.Remove(profile)) Persist(); }
        public static void ResetAll(){ try{ if(File.Exists(_file)){ var old = _file+".old"; try{ File.Copy(_file, old, true);} catch{} File.Delete(_file);} } catch{} _cache.Clear(); Persist(); ConnectionSecretsStore.Reset(); }
        private static byte[] Protect(string payload){ var plain = Encoding.UTF8.GetBytes(payload); return ProtectedData.Protect(plain, EnvInfo.GetEntropy("multi-conn"), DataProtectionScope.CurrentUser); }
        private static string Unprotect(byte[] blob){ var plain = ProtectedData.Unprotect(blob, EnvInfo.GetEntropy("multi-conn"), DataProtectionScope.CurrentUser); return Encoding.UTF8.GetString(plain); }
    }
}
