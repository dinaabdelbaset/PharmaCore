using System;

namespace PharmaCore.POS.Services.Configuration
{
    internal static class ApiBaseResolver
    {
        private static bool ProductionMode => string.Equals(
            Properties.Settings.Default.ApiAuth?.Trim(), "Production",
            StringComparison.OrdinalIgnoreCase);

        public static string GetRequiredSaasBaseUrlForCurrentOrgTenant()
        {
            var meta = Services.Security.ActivationMetadataStorage.Load();
            if (meta == null || meta.OrgId == Guid.Empty || meta.TenantId == Guid.Empty)
                throw new InvalidOperationException("بيانات التفعيل غير مكتملة (Org/Tenant)");

            var e = SaasEndpointStore.Get(meta.OrgId, meta.TenantId);
            if (e == null)
                throw new InvalidOperationException("لم يتم ضبط الدومين/التينانت لهذا الحساب. افتح إعدادات الشبكة.");

            var url = TenantEndpointResolver.BuildHttpsBaseUrl(e.Value.TenantSubdomain, e.Value.RootDomain);
            if (ProductionMode)
                TenantEndpointResolver.ValidateProductionUrlOrThrow(url);
            return url;
        }

        public static string GetRequiredBaseUrlForCurrentOrg()
        {
            var meta = Services.Security.ActivationMetadataStorage.Load();
            if (meta != null && meta.OrgId != Guid.Empty)
            {
                var perOrg = ApiEndpointProfilesStore.GetOrgApiBase(meta.OrgId);
                if (!string.IsNullOrWhiteSpace(perOrg))
                    return PersistAndReturnNormalized(perOrg);
            }

            return GetRequiredBaseUrl();
        }

        /// <summary>
        /// Returns validated API base URL.
        /// Migration: if `ApiBase` is empty but legacy `API` has a value, it is copied into `ApiBase`.
        /// No settings are deleted.
        /// </summary>
        public static string GetRequiredBaseUrl()
        {
            var baseUrl = ReadCandidateUrl();
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new InvalidOperationException("API base URL غير مضبوط");

            var normalized = PersistAndReturnNormalized(baseUrl);
            if (ProductionMode)
                TenantEndpointResolver.ValidateProductionUrlOrThrow(normalized);
            return normalized;
        }

        public static bool TryGetBaseUrl(out string baseUrl)
        {
            try
            {
                baseUrl = GetRequiredBaseUrl();
                return true;
            }
            catch
            {
                baseUrl = string.Empty;
                return false;
            }
        }

        private static string PersistAndReturnNormalized(string raw)
        {
            var baseUrl = Normalize(raw);

            // Persist normalized value into ApiBase for consistency.
            var old = Properties.Settings.Default.ApiBase?.Trim();
            if (!string.Equals(old, baseUrl, StringComparison.Ordinal))
            {
                Properties.Settings.Default.ApiBase = baseUrl;
                Properties.Settings.Default.Save();
            }

            return baseUrl;
        }

        private static string? ReadCandidateUrl()
        {
            var apiBase = Properties.Settings.Default.ApiBase?.Trim();
            if (!string.IsNullOrWhiteSpace(apiBase)) return apiBase;

            // Persistent AppData fallback (survives VS rebuilds)
            var persisted = AppSettingsStore.Get("ApiBase");
            if (!string.IsNullOrWhiteSpace(persisted)) return persisted;

            // Migration path: legacy `API`.
            var legacy = Properties.Settings.Default.API?.Trim();
            if (!string.IsNullOrWhiteSpace(legacy))
            {
                Properties.Settings.Default.ApiBase = legacy;
                Properties.Settings.Default.Save();
                return legacy;
            }

            return null;
        }

        private static string Normalize(string raw)
        {
            raw = raw.Trim();

            // In SaaS production, URL is expected to be full https://... (no implicit IP/host).
            if (!raw.Contains("://", StringComparison.Ordinal))
                raw = "https://" + raw;

            if (!raw.EndsWith("/", StringComparison.Ordinal))
                raw += "/";

            // Validate URI.
            if (!Uri.TryCreate(raw, UriKind.Absolute, out var uri))
                throw new InvalidOperationException("API base URL غير صالح");

            // Disallow non-http(s)
            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                throw new InvalidOperationException("API base URL يجب أن يبدأ بـ http أو https");

            return uri.ToString();
        }
    }
}
