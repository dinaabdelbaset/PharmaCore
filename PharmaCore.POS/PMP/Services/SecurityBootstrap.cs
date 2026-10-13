using System;
using System.Configuration;
namespace PharmaCore.POS.Services
{
    internal static class SecurityBootstrap
    {
        private static bool _done;
        public static void Init()
        {
            if (_done) return; _done = true;
            var endpoint = ConfigurationManager.AppSettings["AuditEndpoint"] ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(endpoint)) AuditSender.Start(endpoint);
        }
    }
}