using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PharmaCore.Infrastructure.Identity;
using System.Linq;

namespace PharmaCore.API.Middleware
{
    public class BranchContextMiddleware
    {
        private readonly RequestDelegate _next;

        public BranchContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IBranchProvider branchProvider)
        {
            // Expect header X-Branch-ID
            if (context.Request.Headers.TryGetValue("X-Branch-ID", out var branchVals))
            {
                branchProvider.SetCurrentBranchIdentifier(branchVals.FirstOrDefault() ?? string.Empty);
            }
            await _next(context);
        }
    }
}
