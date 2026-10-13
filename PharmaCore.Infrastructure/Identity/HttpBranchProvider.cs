using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PharmaCore.Infrastructure.Identity
{
    public class HttpBranchProvider : IBranchProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private string? _overrideBranchId;

        public HttpBranchProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetCurrentBranchIdentifier()
        {
            if (!string.IsNullOrEmpty(_overrideBranchId)) return _overrideBranchId;
            var context = _contextAccessor.HttpContext;
            if (context == null) return null;
            if (context.Request.Headers.TryGetValue("X-Branch-ID", out var values))
            {
                return values.FirstOrDefault();
            }
            return null;
        }

        public void SetCurrentBranchIdentifier(string id)
        {
            _overrideBranchId = id;
        }
    }
}
