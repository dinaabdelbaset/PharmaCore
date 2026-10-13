using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.API.Security;
using InfraEmployee = PharmaCore.Domain.Entities.Employee; // alias to avoid conflict with Domain.Entities.Employee

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/legacy-employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EmployeesController(AppDbContext db){_db = db;}

        public sealed class EmployeeUpsertDto
        {
            public Guid? Id { get; set; }
            public Guid OrgId { get; set; }
            public Guid? TenantId { get; set; }
            public Guid? RegionId { get; set; }
            public Guid? BranchId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? JobTitle { get; set; }
            public string? Grade { get; set; }
            public string Role { get; set; } = "Staff";
            public Guid? RoleId { get; set; }
            public bool IsActive { get; set; } = true;

            // Optional: if provided, will be hashed into PINHash
            public string? Pin { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employees = await _db.Employees
                .AsNoTracking()
                .Include(e => e.EmployeeRole)
                .OrderBy(e => e.FullName)
                .Select(e => new
                {
                    e.Id, e.OrgId, e.EmployeeCode, e.FullName,
                    e.JobTitle, e.Grade, e.BranchId, e.RoleId,
                    Role = e.EmployeeRole != null ? e.EmployeeRole.Name : e.Role,
                    RoleDescription = e.EmployeeRole != null ? e.EmployeeRole.Description : null,
                    e.IsActive
                })
                .ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InfraEmployee>> Get(Guid id)
        {
            var entity = await _db.Employees.FindAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<InfraEmployee>> Create([FromBody] EmployeeUpsertDto model)
        {
            var entity = new InfraEmployee
            {
                Id = Guid.NewGuid(),
                OrgId = model.OrgId,
                TenantId = model.TenantId,
                RegionId = model.RegionId,
                BranchId = model.BranchId,
                EmployeeCode = model.EmployeeCode,
                FullName = model.FullName,
                JobTitle = model.JobTitle,
                Grade = model.Grade,
                Role = model.Role,
                RoleId = model.RoleId,
                IsActive = model.IsActive,
                CreatedAt = DateTime.UtcNow,
                PINHash = string.IsNullOrWhiteSpace(model.Pin) ? string.Empty : EmployeePinHasher.Hash(model.Pin)
            };

            _db.Employees.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeUpsertDto model)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();

            existing.FullName = model.FullName;
            existing.JobTitle = model.JobTitle;
            existing.Grade = model.Grade;
            existing.BranchId = model.BranchId;
            existing.RegionId = model.RegionId;
            existing.TenantId = model.TenantId;
            existing.Role = model.Role;
            existing.RoleId = model.RoleId;
            existing.IsActive = model.IsActive;

            if (!string.IsNullOrWhiteSpace(model.Pin))
                existing.PINHash = EmployeePinHasher.Hash(model.Pin);

            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Employees.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}