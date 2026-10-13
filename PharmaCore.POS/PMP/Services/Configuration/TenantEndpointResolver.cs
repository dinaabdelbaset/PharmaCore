using System;
using System.Text.RegularExpressions;

namespace PharmaCore.POS.Services.Configuration
{
    internal static class TenantEndpointResolver
    {
        private static readonly Regex SubdomainRegex = new("^[a-z0-9]([a-z0-9-]{0,61}[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RootDomainRegex = new(@"^[a-z0-9.-]+\.[a-z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string BuildHttpsBaseUrl(string tenantSubdomain, string rootDomain)
        {
            tenantSubdomain = (tenantSubdomain ?? string.Empty).Trim().ToLowerInvariant();
            rootDomain = (rootDomain ?? string.Empty).Trim().ToLowerInvariant();

            if (!SubdomainRegex.IsMatch(tenantSubdomain))
                throw new InvalidOperationException("Tenant subdomain غير صالح");
            if (!RootDomainRegex.IsMatch(rootDomain))
                throw new InvalidOperationException("Root domain غير صالح");

            var host = $"{tenantSubdomain}.{rootDomain}";
            return $"https://{host}/";
        }

        public static void ValidateProductionUrlOrThrow(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidOperationException("API base URL غير صالح");

            if (!string.Equals(uri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("يجب استخدام HTTPS فقط");

            if (!uri.IsDefaultPort)
                throw new InvalidOperationException("لا يسمح باستخدام Ports في وضع الإنتاج");

            var host = uri.Host?.ToLowerInvariant() ?? "";
            if (host == "localhost" || host.EndsWith(".local", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("لا يسمح باستخدام localhost");

            // Disallow raw IPs.
            if (System.Net.IPAddress.TryParse(host, out _))
                throw new InvalidOperationException("لا يسمح باستخدام IP مباشرة في وضع الإنتاج");
        }
    }
}
