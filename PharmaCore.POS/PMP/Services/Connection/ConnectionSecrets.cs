using System;

namespace PharmaCore.POS.Services.Connection
{
    internal sealed class ConnectionSecrets
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 5432;
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool SslRequire { get; set; } = true;
        public bool TrustServerCert { get; set; } = true;
    }

    internal static class SecretsValidator
    {
        public static (bool ok, string reason) Validate(ConnectionSecrets s)
        {
            if (string.IsNullOrWhiteSpace(s.Host)) return (false, "اسم المضيف فارغ");
            if (!HostLooksValid(s.Host)) return (false, "اسم المضيف غير صالح");
            if (s.Port < 1 || s.Port > 65535) return (false, "رقم المنفذ خارج النطاق");
            if (string.IsNullOrWhiteSpace(s.Database)) return (false, "اسم قاعدة البيانات مطلوب");
            if (string.IsNullOrWhiteSpace(s.Username)) return (false, "اسم المستخدم مطلوب");
            if (s.Username.Contains(" ")) return (false, "اسم المستخدم لا يحتوي مسافات");
            if (string.IsNullOrWhiteSpace(s.Password)) return (false, "كلمة المرور مطلوبة");
            return (true, "سليم");
        }
        private static bool HostLooksValid(string host)
        {
            // بسيط: IP أو اسم دومين بدون مسافات
            if (host.Length>255) return false;
            if (System.Net.IPAddress.TryParse(host, out _)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(host, "^[A-Za-z0-9.-]+$");
        }
    }
}
