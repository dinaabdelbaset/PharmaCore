using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PharmaCore.Infrastructure.Identity
{
    public class HttpTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpTenantProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetCurrentTenantIdentifier()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null) return null;

            // Example: tenant identifier from header "X-Tenant-ID"
            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var values))
            {
                return values.FirstOrDefault();
            }

            // Fallback to host or claim
            return context.Request.Host.Host;
        }
    }
}
