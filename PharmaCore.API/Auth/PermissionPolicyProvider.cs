using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PharmaCore.API.Auth
{
    // Optional: custom policy provider if dynamic policies required. For now, we will use static registration in Program.cs
    public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }
    }
}
