using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.API.Security;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System.Security.Claims;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/employee-roles")]
    [Authorize]
    public class EmployeeRolesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public EmployeeRolesController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        public record RoleResponse(Guid Id, string Name, string? Description, Guid? ParentRoleId, bool IsSystemRole, int SortOrder);

        [HttpGet]
        public async Task<ActionResult<List<RoleResponse>>> GetRoles()
        {
            var roles = await _db.EmployeeRoles
                .Where(r => r.OrgId == _tenant.OrgId && r.IsActive)
                .OrderBy(r => r.SortOrder)
                .Select(r => new RoleResponse(r.Id, r.Name, r.Description, r.ParentRoleId, r.IsSystemRole, r.SortOrder))
                .ToListAsync();

            return Ok(roles);
        }

        /// <summary>Seeds the default role hierarchy if none exist for this org.</summary>
        [HttpPost("seed-defaults")]
        public async Task<ActionResult> SeedDefaults()
        {
            var seeded = await RoleHierarchySeeder.SeedAsync(_db, _tenant.OrgId);
            return Ok(new { seeded, message = seeded ? "تم إنشاء الرتب الافتراضية" : "الرتب موجودة بالفعل" });
        }

        /// <summary>Returns all roles as a flat list suitable for dropdowns.</summary>
        [HttpGet("flat")]
        public async Task<ActionResult> GetFlatList()
        {
            var roles = await _db.EmployeeRoles
                .Where(r => r.OrgId == _tenant.OrgId && r.IsActive)
                .OrderBy(r => r.SortOrder)
                .Select(r => new { r.Id, r.Name, r.Description, r.ParentRoleId, r.SortOrder })
                .ToListAsync();

            // Build indented display names for dropdown
            var lookup = roles.ToDictionary(r => r.Id);
            var result = roles.Select(r =>
            {
                int depth = 0;
                var current = r;
                while (current.ParentRoleId.HasValue && depth < 10)
                {
                    if (lookup.TryGetValue(current.ParentRoleId.Value, out var parent))
                    { current = parent; depth++; }
                    else break;
                }
                var indent = new string('─', depth * 2);
                var display = depth > 0 ? $"{indent} {r.Description ?? r.Name}" : (r.Description ?? r.Name);
                return new { r.Id, r.Name, r.Description, Display = display, r.SortOrder };
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}/permissions")]
        public async Task<ActionResult<List<string>>> GetRolePermissions(Guid id)
        {
            var perms = await _db.RolePermissions
                .Where(p => p.OrgId == _tenant.OrgId && p.RoleId == id && p.IsActive)
                .Select(p => p.Permission)
                .ToListAsync();

            return Ok(perms);
        }

        public record CreateRoleRequest(string Name, Guid? ParentRoleId, List<string> Permissions);

        [HttpPost]
        public async Task<ActionResult<RoleResponse>> CreateRole([FromBody] CreateRoleRequest req)
        {
            var isOwnerOrAdmin = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value is "Admin" or "Manager" or "مدير" or "admin" or "manager";
            
            // For now, only top admins can create roles freely. 
            // In a strict hierarchy, you would verify the creator's role and ensure they aren't creating a role above themselves.
            if (!isOwnerOrAdmin)
                return Forbid();

            var role = new EmployeeRole
            {
                Id = Guid.NewGuid(),
                OrgId = _tenant.OrgId,
                Name = req.Name,
                ParentRoleId = req.ParentRoleId,
                IsSystemRole = false,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.EmployeeRoles.Add(role);

            if (req.Permissions != null && req.Permissions.Any())
            {
                foreach (var p in req.Permissions)
                {
                    _db.RolePermissions.Add(new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        OrgId = _tenant.OrgId,
                        RoleId = role.Id,
                        Permission = p,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    });
                }
            }

            await _db.SaveChangesAsync();

            return Ok(new RoleResponse(role.Id, role.Name, null, role.ParentRoleId, role.IsSystemRole, 999));
        }

        public record UpdateRoleRequest(string Name, Guid? ParentRoleId, List<string> Permissions);

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleRequest req)
        {
            var role = await _db.EmployeeRoles.FirstOrDefaultAsync(r => r.Id == id && r.OrgId == _tenant.OrgId && r.IsActive);
            if (role == null) return NotFound();

            if (role.IsSystemRole)
                return BadRequest("لا يمكن تعديل الرتبة الأساسية للنظام (المالك).");

            role.Name = req.Name;
            
            // Prevent circular dependency (A -> B -> A)
            if (req.ParentRoleId == id)
                return BadRequest("لا يمكن أن تكون الرتبة هي المديرة لنفسها.");

            if (req.ParentRoleId.HasValue)
            {
                // Simple circular check: Walk up the tree to make sure ‘id’ isn't an ancestor of ‘ParentRoleId’
                var currentParent = req.ParentRoleId.Value;
                int maxDepth = 50;
                while (currentParent != Guid.Empty && maxDepth > 0)
                {
                    if (currentParent == id) return BadRequest("لا يمكن ربط الرتبة برتبة أخرى تقع تحتها.");
                    
                    var parentNode = await _db.EmployeeRoles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == currentParent);
                    if (parentNode?.ParentRoleId == null) break;
                    currentParent = parentNode.ParentRoleId.Value;
                    maxDepth--;
                }
            }

            role.ParentRoleId = req.ParentRoleId;
            role.UpdatedAt = DateTime.UtcNow;

            // Update permissions
            var existingPerms = await _db.RolePermissions.Where(p => p.RoleId == id).ToListAsync();
            _db.RolePermissions.RemoveRange(existingPerms); // soft delete might be better, but hard delete is fine for mapping table

            if (req.Permissions != null && req.Permissions.Any())
            {
                foreach (var p in req.Permissions)
                {
                    _db.RolePermissions.Add(new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        OrgId = _tenant.OrgId,
                        RoleId = role.Id,
                        Permission = p,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    });
                }
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(Guid id)
        {
            var role = await _db.EmployeeRoles.FirstOrDefaultAsync(r => r.Id == id && r.OrgId == _tenant.OrgId);
            if (role == null) return NotFound();

            if (role.IsSystemRole)
                return BadRequest("لا يمكن حذف الرتبة الأساسية للنظام.");

            var hasChildren = await _db.EmployeeRoles.AnyAsync(r => r.ParentRoleId == id && r.IsActive);
            if (hasChildren)
                return BadRequest("لا يمكن حذف الرتبة لأن هناك رتب أخرى تابعة لها.");

            var hasEmployees = await _db.Employees.AnyAsync(e => e.RoleId == id && e.IsActive);
            if (hasEmployees)
                return BadRequest("لا يمكن حذف الرتبة لوجود موظفين مسجلين عليها.");

            role.IsActive = false;
            role.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
