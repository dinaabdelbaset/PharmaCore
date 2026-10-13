using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.API.Security;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public sealed class EmployeeManagementController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public EmployeeManagementController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        // ── DTOs ──────────────────────────────────────────────

        public sealed record EmployeeDto(
            Guid Id, string EmployeeCode, string FullName,
            string? JobTitle, string? Grade, string Role,
            bool IsActive, Guid OrgId, Guid? BranchId, Guid? RoleId);

        public sealed record CreateEmployeeRequest(
            string FullName, string? EmployeeCode, string Pin,
            string Role, Guid? RoleId, string? JobTitle, string? Grade,
            Guid? TenantId, Guid? RegionId, Guid? BranchId);

        public sealed record UpdateEmployeeRequest(
            string FullName, string Role, Guid? RoleId, string? JobTitle, string? Grade, bool IsActive, Guid? BranchId = null, Guid? RegionId = null);

        public sealed record ResetPinRequest(string NewPin);

        public sealed record BootstrapRequest(
            string FullName, string Pin, string? EmployeeCode,
            Guid OrgId, Guid BranchId, Guid? TenantId, Guid? RegionId);

        // ── Helpers ───────────────────────────────────────────

        private Guid GetOrgId()
        {
            // Try tenant context first (from JWT claims)
            if (_tenant.OrgId != Guid.Empty) return _tenant.OrgId;
            // Fallback: read from "org_id" claim directly
            var claim = User.FindFirst("org_id");
            return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
        }

        private Guid GetCurrentEmployeeId()
        {
            var claim = User.FindFirst("employee_id");
            return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
        }

        private async Task<bool> CanAssignRoleAsync(Guid currentEmployeeId, Guid? targetRoleId)
        {
            if (targetRoleId == null) return true; // Default fallback roles like Staff

            // Portal Administrator bypass: if the logged-in JWT user has the Admin role, they can assign any employee role.
            var portalRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value 
                ?? User.FindFirst("role")?.Value;
            if (portalRole != null && portalRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var currentUser = await _db.Employees
                .Include(e => e.EmployeeRole)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == currentEmployeeId);
                
            if (currentUser == null) return false;

            // System Owner can assign any role
            if (currentUser.EmployeeRole?.IsSystemRole == true) return true;
            
            // Legacy owner logic
            var r = currentUser.Role?.Trim().ToLowerInvariant() ?? "";
            if (r == "admin" || r == "manager" || r == "مدير" || r == "owner" || r == "اونر" || r == "مالك") return true;

            int currentUserSortOrder = currentUser.EmployeeRole?.SortOrder ?? 9999;

            var targetRole = await _db.EmployeeRoles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == targetRoleId);

            if (targetRole == null) return false;

            // Smaller SortOrder = Higher Rank
            // Discord/SAP Rule: You can only assign roles that are strictly BELOW you in rank (LARGER SortOrder).
            // Example: Order 3 cannot assign Order 3 or Order 2 or Order 1.
            return targetRole.SortOrder > currentUserSortOrder;
        }

        private async Task<string> GenerateNextCodeAsync(Guid orgId)
        {
            // Get the highest existing employee code (numeric)
            var latest = await _db.Employees
                .IgnoreQueryFilters()
                .Where(e => e.OrgId == orgId)
                .OrderByDescending(e => e.EmployeeCode)
                .Select(e => e.EmployeeCode)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(latest)) return "1000"; // fallback

            // Parse as number and increment
            if (long.TryParse(latest, out var num))
                return (num + 1).ToString();

            // Non-numeric fallback: append "1"
            return latest + "1";
        }

        // ── GET api/employees/exists ──────────────────────────

        [HttpGet("exists")]
        public async Task<ActionResult<object>> Exists()
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty)
                return Ok(new { exists = false, error = "OrgId not available" });

            var any = await _db.Employees
                .IgnoreQueryFilters()
                .AnyAsync(e => e.OrgId == orgId);

            return Ok(new { exists = any });
        }

        // ── GET api/employees/by-code/{code} ─────────────────

        [HttpGet("by-code/{code}")]
        public async Task<ActionResult<EmployeeDto>> GetByCode(string code)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var emp = await _db.Employees
                .IgnoreQueryFilters()
                .Where(e => e.OrgId == orgId && e.EmployeeCode == code.Trim())
                .Select(e => new EmployeeDto(
                    e.Id, e.EmployeeCode, e.FullName,
                    e.JobTitle, e.Grade, e.Role,
                    e.IsActive, e.OrgId, e.BranchId, e.RoleId))
                .FirstOrDefaultAsync();

            if (emp == null) return NotFound(new { error = $"لا يوجد موظف بالكود: {code}" });
            return Ok(emp);
        }

        // ── GET api/employees ─────────────────────────────────

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            // Auto-heal: ensure the original 'Manager' account is properly linked to the 'Owner' role
            var firstUser = await _db.Employees.IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.OrgId == orgId && e.Role == "Manager" && e.RoleId == null);
            if (firstUser != null)
            {
                var ownerRole = await _db.EmployeeRoles.IgnoreQueryFilters()
                    .FirstOrDefaultAsync(r => r.OrgId == orgId && r.Name == "Owner");
                if (ownerRole != null)
                {
                    firstUser.RoleId = ownerRole.Id;
                    firstUser.Role = "Owner";
                    await _db.SaveChangesAsync();
                }
            }

            var list = await _db.Employees
                .IgnoreQueryFilters()
                .Where(e => e.OrgId == orgId)
                .OrderBy(e => e.EmployeeCode)
                .Select(e => new EmployeeDto(
                    e.Id, e.EmployeeCode, e.FullName,
                    e.JobTitle, e.Grade, e.Role,
                    e.IsActive, e.OrgId, e.BranchId, e.RoleId))
                .ToListAsync();

            return Ok(list);
        }

        // ── POST api/employees ────────────────────────────────

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create([FromBody] CreateEmployeeRequest req)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            if (string.IsNullOrWhiteSpace(req.FullName) || string.IsNullOrWhiteSpace(req.Pin))
                return BadRequest(new { error = "الاسم والرقم السري مطلوبين" });

            var code = string.IsNullOrWhiteSpace(req.EmployeeCode)
                ? await GenerateNextCodeAsync(orgId)
                : req.EmployeeCode.Trim();

            // Check duplicate code
            var exists = await _db.Employees.IgnoreQueryFilters()
                .AnyAsync(e => e.OrgId == orgId && e.EmployeeCode == code);
            if (exists) return Conflict(new { error = $"كود الموظف '{code}' مستخدم بالفعل" });

            // Security: Hierarchical Role Assignment Guard
            if (!await CanAssignRoleAsync(GetCurrentEmployeeId(), req.RoleId))
            {
                return StatusCode(403, new { error = "غير مصرح لك بتعيين موظف بهذه الرتبة. الرتبة أعلى أو مساوية لرتبتك." });
            }

            var emp = new Employee
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                TenantId = req.TenantId,
                RegionId = req.RegionId,
                BranchId = req.BranchId ?? _tenant.BranchId,
                EmployeeCode = code,
                PINHash = EmployeePinHasher.Hash(req.Pin),
                FullName = req.FullName.Trim(),
                Role = string.IsNullOrWhiteSpace(req.Role) ? "Staff" : req.Role.Trim(),
                RoleId = req.RoleId,
                JobTitle = req.JobTitle?.Trim(),
                Grade = req.Grade?.Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Employees.Add(emp);

            // Auto-create branch access
            var branchId = req.BranchId ?? _tenant.BranchId;
            if (branchId.HasValue)
            {
                _db.EmployeeBranchAccesses.Add(new EmployeeBranchAccess
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    EmployeeId = emp.Id,
                    BranchId = branchId.Value,
                    AccessType = BranchAccessType.Always,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            await _db.SaveChangesAsync();

            return Ok(new EmployeeDto(
                emp.Id, emp.EmployeeCode, emp.FullName,
                emp.JobTitle, emp.Grade, emp.Role,
                emp.IsActive, emp.OrgId, emp.BranchId, emp.RoleId));
        }

        // ── POST api/employees/bootstrap ──────────────────────

        [HttpPost("bootstrap")]
        public async Task<ActionResult<EmployeeDto>> Bootstrap([FromBody] BootstrapRequest req)
        {
            var orgId = req.OrgId;
            if (orgId == Guid.Empty)
                orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            if (string.IsNullOrWhiteSpace(req.FullName) || string.IsNullOrWhiteSpace(req.Pin))
                return BadRequest(new { error = "الاسم والرقم السري مطلوبين" });

            // Only allow if NO employees exist for this org
            var any = await _db.Employees.IgnoreQueryFilters()
                .AnyAsync(e => e.OrgId == orgId);
            if (any)
                return Conflict(new { error = "يوجد موظفين بالفعل — لا يمكن تسجيل مدير جديد" });

            var code = string.IsNullOrWhiteSpace(req.EmployeeCode)
                ? "E0001"
                : req.EmployeeCode.Trim();

            var ownerRole = await _db.EmployeeRoles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.OrgId == orgId && r.Name == "Owner");

            var emp = new Employee
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                TenantId = req.TenantId,
                RegionId = req.RegionId,
                BranchId = req.BranchId,
                EmployeeCode = code,
                PINHash = EmployeePinHasher.Hash(req.Pin),
                FullName = req.FullName.Trim(),
                Role = "Owner",
                RoleId = ownerRole?.Id,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Employees.Add(emp);

            // Auto-grant permanent branch access
            _db.EmployeeBranchAccesses.Add(new EmployeeBranchAccess
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                EmployeeId = emp.Id,
                BranchId = req.BranchId,
                AccessType = BranchAccessType.Always,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();

            return Ok(new EmployeeDto(
                emp.Id, emp.EmployeeCode, emp.FullName,
                null, null, emp.Role,
                emp.IsActive, emp.OrgId, emp.BranchId, emp.RoleId));
        }

        // ── PUT api/employees/{id} ────────────────────────────

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeRequest req)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var emp = await _db.Employees.IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == id && e.OrgId == orgId);
            if (emp == null) return NotFound(new { error = "الموظف غير موجود" });

            // Security: Hierarchical Role Assignment Guard
            if (req.RoleId != emp.RoleId) // Only check if they are trying to change the role
            {
                if (!await CanAssignRoleAsync(GetCurrentEmployeeId(), req.RoleId))
                {
                    return StatusCode(403, new { error = "غير مصرح لك بترقية/تعديل رتبة هذا الموظف إلى رتبة أعلى أو مساوية لرتبتك." });
                }
            }

            emp.FullName = req.FullName.Trim();
            emp.Role = req.Role.Trim();
            emp.RoleId = req.RoleId;
            emp.JobTitle = req.JobTitle?.Trim();
            emp.Grade = req.Grade?.Trim();
            emp.IsActive = req.IsActive;
            if (req.BranchId.HasValue) emp.BranchId = req.BranchId.Value;
            if (req.RegionId.HasValue) emp.RegionId = req.RegionId.Value;
            emp.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(new { message = "تم التعديل" });
        }

        // ── PUT api/employees/{id}/pin ────────────────────────

        [HttpPut("{id:guid}/pin")]
        public async Task<IActionResult> ResetPin(Guid id, [FromBody] ResetPinRequest req)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            if (string.IsNullOrWhiteSpace(req.NewPin))
                return BadRequest(new { error = "الرقم السري مطلوب" });

            var emp = await _db.Employees.IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == id && e.OrgId == orgId);
            if (emp == null) return NotFound(new { error = "الموظف غير موجود" });

            emp.PINHash = EmployeePinHasher.Hash(req.NewPin);
            emp.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(new { message = "تم تغيير الرقم السري" });
        }

        // ── DELETE api/employees/{id} ─────────────────────────

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var emp = await _db.Employees.IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == id && e.OrgId == orgId);
            if (emp == null) return NotFound(new { error = "الموظف غير موجود" });

            // Soft delete
            emp.IsActive = false;
            emp.UpdatedAt = DateTime.UtcNow;

            // Also deactivate branch accesses
            var accesses = await _db.EmployeeBranchAccesses.IgnoreQueryFilters()
                .Where(a => a.EmployeeId == id && a.OrgId == orgId)
                .ToListAsync();
            foreach (var a in accesses) a.IsActive = false;

            await _db.SaveChangesAsync();
            return Ok(new { message = "تم حذف الموظف" });
        }

        public record VerifyPinRequest(string EmployeeCode, string Pin);

        [HttpPost("verify-pin")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPin([FromBody] VerifyPinRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.EmployeeCode) || string.IsNullOrWhiteSpace(req.Pin))
            {
                return BadRequest(new { error = "كود الموظف والرقم السري مطلوبان" });
            }

            var emp = await _db.Employees
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.EmployeeCode == req.EmployeeCode && e.IsActive);

            if (emp == null || string.IsNullOrEmpty(emp.PINHash) || !EmployeePinHasher.Verify(req.Pin, emp.PINHash))
            {
                return BadRequest(new { error = "كود الموظف أو الرقم السري غير صحيح" });
            }

            // Find current active session
            var activeSession = await _db.AuditSessions
                .FirstOrDefaultAsync(s => s.EmployeeId == emp.Id && s.Status == "Active");

            return Ok(new
            {
                id = emp.Id,
                fullName = emp.FullName,
                employeeCode = emp.EmployeeCode,
                role = emp.Role,
                roleId = emp.RoleId,
                branchId = emp.BranchId,
                orgId = emp.OrgId,
                hasActiveSession = activeSession != null,
                activeSessionId = activeSession?.Id
            });
        }
    }
}
