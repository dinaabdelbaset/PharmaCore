using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System.Security.Claims;

namespace PharmaCore.API.Security
{
    internal sealed class ClaimsTenantContext : ICurrentTenantContext
    {
        private readonly IHttpContextAccessor _http;

        public ClaimsTenantContext(IHttpContextAccessor http) => _http = http;

        public Guid OrgId
        {
            get
            {
                var v = _http.HttpContext?.User?.FindFirstValue("org_id");
                return Guid.TryParse(v, out var id) ? id : Guid.Empty;
            }
        }

        public Guid? BranchId
        {
            get
            {
                var v = _http.HttpContext?.User?.FindFirstValue("branch_id");
                return Guid.TryParse(v, out var id) ? id : null;
            }
        }
    }
}
