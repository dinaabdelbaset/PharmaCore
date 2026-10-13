using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/branches/{branchId:guid}/employee-access")]
    [Authorize]
    public sealed class EmployeeBranchAccessController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EmployeeBranchAccessController(AppDbContext db)
        {
            _db = db;
        }

        public sealed record AccessDto(
            Guid? Id,
            Guid OrgId,
            Guid EmployeeId,
            Guid BranchId,
            BranchAccessType AccessType,
            DateTime? AllowedFrom,
            DateTime? AllowedTo,
            bool IsActive);

        [HttpGet("/api/employee-access/{employeeId:guid}")]
        public async Task<ActionResult<IEnumerable<EmployeeBranchAccess>>> GetByEmployee(Guid employeeId)
        {
            var items = await _db.EmployeeBranchAccesses
                .AsNoTracking()
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(items);
        }

        // NOTE: should be protected by branch manager role + tenant context.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBranchAccess>>> GetAll(Guid branchId)
        {
            var items = await _db.EmployeeBranchAccesses
                .AsNoTracking()
                .Where(x => x.BranchId == branchId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeBranchAccess>> Create(Guid branchId, [FromBody] AccessDto dto)
        {
            if (dto.BranchId != branchId) return BadRequest("Branch mismatch");

            var entity = new EmployeeBranchAccess
            {
                Id = Guid.NewGuid(),
                OrgId = dto.OrgId,
                EmployeeId = dto.EmployeeId,
                BranchId = branchId,
                AccessType = dto.AccessType,
                AllowedFrom = dto.AllowedFrom,
                AllowedTo = dto.AllowedTo,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };

            _db.EmployeeBranchAccesses.Add(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { branchId }, entity);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid branchId, Guid id, [FromBody] AccessDto dto)
        {
            var existing = await _db.EmployeeBranchAccesses.FirstOrDefaultAsync(x => x.Id == id && x.BranchId == branchId);
            if (existing == null) return NotFound();

            existing.AccessType = dto.AccessType;
            existing.AllowedFrom = dto.AllowedFrom;
            existing.AllowedTo = dto.AllowedTo;
            existing.IsActive = dto.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid branchId, Guid id)
        {
            var existing = await _db.EmployeeBranchAccesses.FirstOrDefaultAsync(x => x.Id == id && x.BranchId == branchId);
            if (existing == null) return NotFound();
            _db.EmployeeBranchAccesses.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
