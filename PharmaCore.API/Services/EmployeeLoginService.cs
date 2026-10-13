using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.API.Security;
using System.Security.Claims;

namespace PharmaCore.API.Services
{
    public interface IEmployeeLoginService
    {
        Task<(bool ok, string error, string? token, string[] permissions, Guid[] branchIds, Guid? employeeId, string? employeeCode, string? fullName, Guid? branchId, string? branchName, Guid? workstationSessionId, DateTime? loginTime, string? message)> LoginAsync(string employeeCode, string pin, Guid branchId, Guid workstationSessionId);
    }

    public class EmployeeLoginService : IEmployeeLoginService
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly ICurrentTenantContext _tenant;

        public EmployeeLoginService(AppDbContext db, ITokenService tokenService, ICurrentTenantContext tenant)
        {
            _db = db;
            _tokenService = tokenService;
            _tenant = tenant;
        }

        public async Task<(bool ok, string error, string? token, string[] permissions, Guid[] branchIds, Guid? employeeId, string? employeeCode, string? fullName, Guid? branchId, string? branchName, Guid? workstationSessionId, DateTime? loginTime, string? message)> LoginAsync(string employeeCode, string pin, Guid branchId, Guid workstationSessionId)
        {
            if (_tenant.OrgId == Guid.Empty || _tenant.BranchId == null)
                return (false, "Branch session غير صالح", null, Array.Empty<string>(), Array.Empty<Guid>(), null, null, null, null, null, null, null, null);

            if (_tenant.BranchId.Value != branchId)
                return (false, "Branch mismatch", null, Array.Empty<string>(), Array.Empty<Guid>(), null, null, null, null, null, null, null, null);

            if (string.IsNullOrWhiteSpace(employeeCode) || string.IsNullOrWhiteSpace(pin))
                return (false, "Missing credentials", null, Array.Empty<string>(), Array.Empty<Guid>(), null, null, null, null, null, null, null, null);

            var emp = await _db.Employees
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.OrgId == _tenant.OrgId && e.EmployeeCode == employeeCode);

            if (emp == null)
                return (false, "الموظف غير موجود", null, Array.Empty<string>(), Array.Empty<Guid>(), null, null, null, null, null, null, null, null);

            if (!emp.IsActive)
                return (false, "حساب الموظف غير مفعل", null, Array.Empty<string>(), Array.Empty<Guid>(), emp.Id, emp.EmployeeCode, emp.FullName, null, null, null, null, null);

            if (!EmployeePinHasher.Verify(pin, emp.PINHash))
                return (false, "الرقم السري غير صحيح", null, Array.Empty<string>(), Array.Empty<Guid>(), emp.Id, emp.EmployeeCode, emp.FullName, null, null, null, null, null);

            // Branch allowlist (wait list)
            var now = DateTime.UtcNow;
            var allowed = await _db.EmployeeBranchAccesses
                .IgnoreQueryFilters()
                .AsNoTracking()
                .AnyAsync(a => a.EmployeeId == emp.Id
                              && a.BranchId == branchId
                              && a.IsActive
                              && (a.AccessType == BranchAccessType.Always
                                  || (a.AccessType == BranchAccessType.TimeWindow
                                      && (a.AllowedFrom == null || a.AllowedFrom <= now)
                                      && (a.AllowedTo == null || a.AllowedTo >= now))));

            if (!allowed)
                return (false, "غير مسموح بهذا الفرع", null, Array.Empty<string>(), Array.Empty<Guid>(), emp.Id, emp.EmployeeCode, emp.FullName, branchId, null, null, null, null);

            // Load exceptional permissions from DB (EmployeePermissions)
            var empPermissions = await _db.EmployeePermissions
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.OrgId == _tenant.OrgId && p.EmployeeId == emp.Id && p.IsActive)
                .Select(p => p.Permission)
                .ToListAsync();

            // Load role-based permissions if the employee has a RoleId
            var rolePermissions = new List<string>();
            bool isSystemOwner = false;
            string actualRoleName = string.IsNullOrWhiteSpace(emp.Role) ? "Staff" : emp.Role;

            if (emp.RoleId.HasValue)
            {
                var roleEntity = await _db.EmployeeRoles
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == emp.RoleId.Value);

                if (roleEntity != null)
                {
                    actualRoleName = roleEntity.Name; // Use the new role name
                    isSystemOwner = roleEntity.IsSystemRole;
                    
                    var rPerms = await _db.RolePermissions
                        .IgnoreQueryFilters()
                        .AsNoTracking()
                        .Where(p => p.OrgId == _tenant.OrgId && p.RoleId == emp.RoleId.Value && p.IsActive)
                        .Select(p => p.Permission)
                        .ToListAsync();

                    rolePermissions.AddRange(rPerms);
                }
            }

            // Override claims with actual determined role name
            var claims = new List<Claim>
            {
                new Claim("org_id", _tenant.OrgId.ToString()),
                new Claim("employee_id", emp.Id.ToString()),
                new Claim("employee_code", emp.EmployeeCode),
                new Claim("branch_id", branchId.ToString()),
                new Claim(ClaimTypes.Name, emp.FullName),
                new Claim(ClaimTypes.Role, actualRoleName)
            };

            // Fallback for older existing admin accounts or if IsSystemRole is true
            var isLegacyAdmin = actualRoleName is "Admin" or "Manager" or "مدير" or "admin" or "manager" ||
                                emp.Role is "Admin" or "Manager" or "مدير" or "admin" or "manager";

            if (isSystemOwner || isLegacyAdmin)
            {
                var allKeys = PharmaCore.API.Controllers.PermissionKeys.All
                    .SelectMany(kv => kv.Value)
                    .Distinct()
                    .ToList();
                rolePermissions.AddRange(allKeys);
            }

            // Merge both exceptional permissions and role permissions into one array
            var finalPermissions = empPermissions.Concat(rolePermissions).Distinct().ToArray();

            var token = _tokenService.GenerateToken(claims, 480);
            var loginTime = DateTime.UtcNow;

            return (true, string.Empty, token, finalPermissions, new[] { branchId }, emp.Id, emp.EmployeeCode, emp.FullName, branchId, null, workstationSessionId, loginTime, "Login OK");
        }
    }
}