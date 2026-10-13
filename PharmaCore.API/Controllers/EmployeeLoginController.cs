using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.API.Services;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/employee-login")]
    public sealed class EmployeeLoginController : ControllerBase
    {
        private readonly IEmployeeLoginService _svc;
        private readonly PharmaCore.Infrastructure.Data.AppDbContext _db;

        public EmployeeLoginController(IEmployeeLoginService svc, PharmaCore.Infrastructure.Data.AppDbContext db)
        {
            _svc = svc;
            _db = db;
        }

        public sealed record LoginRequest(string EmployeeCode, string Pin, Guid BranchId, Guid WorkstationSessionId);

        public sealed record LoginResponse(
            bool Ok,
            string Error,
            string? Token,
            Guid? EmployeeId,
            string? EmployeeCode,
            string? FullName,
            Guid? BranchId,
            string? Role,
            bool IsSystemRole,
            string[]? Permissions);

        // Employee login must happen after branch/device session login.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        {
            var (ok, error, token, permissions, _, employeeId, employeeCode, fullName, branchId, _, _, _, _) =
                await _svc.LoginAsync(req.EmployeeCode, req.Pin, req.BranchId, req.WorkstationSessionId);

            string? role = null;
            bool isSystemRole = false;
            if (ok && employeeId.HasValue)
            {
                var emp = await _db.Employees
                    .Include(e => e.EmployeeRole)
                    .FirstOrDefaultAsync(e => e.Id == employeeId.Value);
                
                role = emp?.EmployeeRole?.Name ?? emp?.Role;
                isSystemRole = emp?.EmployeeRole?.IsSystemRole ?? false;

                // Fallback for older existing admin accounts or if IsSystemRole is true
                if (!isSystemRole && emp != null)
                {
                    var r = role?.Trim().ToLowerInvariant() ?? "";
                    if (r == "admin" || r == "manager" || r == "مدير" || r == "owner" || r == "اونر" || r == "مالك")
                    {
                        isSystemRole = true;
                    }
                    else
                    {
                        // Failsafe: If they are the only user in their org, they are the owner
                        var count = await _db.Employees.IgnoreQueryFilters().CountAsync(x => x.OrgId == emp.OrgId);
                        if (count == 1)
                        {
                            isSystemRole = true;
                        }
                    }
                }
            }

            return Ok(new LoginResponse(
                ok,
                error,
                token,
                employeeId,
                employeeCode,
                fullName,
                branchId,
                role,
                isSystemRole,
                permissions));
        }
    }
}
