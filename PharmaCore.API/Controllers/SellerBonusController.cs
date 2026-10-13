using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController, Route("api/seller-bonuses"), Authorize]
    public class SellerBonusController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SellerBonusController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        public record BonusReportDto(Guid EmployeeId, string EmployeeName, decimal TotalBonus, int TransactionCount);

        // ── Get bonus config for a batch ─────────────────
        [HttpGet]
        public async Task<IActionResult> GetByBatch([FromQuery] Guid batchId)
        {
            var bonus = await _db.SellerBonuses.FirstOrDefaultAsync(b => b.BatchId == batchId && b.OrgId == OrgId);
            if (bonus == null) return Ok(new { exists = false });
            return Ok(new { exists = true, bonus.Id, bonus.BonusPerMainUnit, bonus.IsPercentage });
        }

        // ── Set bonus for a batch ────────────────────────
        [HttpPost]
        public async Task<IActionResult> Set([FromBody] SetBonusReq req)
        {
            var orgId = OrgId;
            var existing = await _db.SellerBonuses.FirstOrDefaultAsync(b => b.BatchId == req.BatchId && b.OrgId == orgId);

            if (existing != null)
            {
                existing.BonusPerMainUnit = req.BonusPerMainUnit;
                existing.IsPercentage = req.IsPercentage;
                existing.IsActive = req.BonusPerMainUnit > 0;
            }
            else if (req.BonusPerMainUnit > 0)
            {
                _db.SellerBonuses.Add(new SellerBonus
                {
                    Id = Guid.NewGuid(), OrgId = orgId,
                    BatchId = req.BatchId, BonusPerMainUnit = req.BonusPerMainUnit,
                    IsPercentage = req.IsPercentage, IsActive = true
                });
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Employee Bonus Report ────────────────────────
        [HttpGet("report")]
        public async Task<IActionResult> Report([FromQuery] Guid? employeeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var orgId = OrgId;
            var q = _db.EmployeeBonusLogs.Where(l => l.OrgId == orgId);

            if (employeeId.HasValue) q = q.Where(l => l.EmployeeId == employeeId.Value);
            if (from.HasValue) q = q.Where(l => l.CreatedAt >= from.Value);
            if (to.HasValue) q = q.Where(l => l.CreatedAt <= to.Value);

            var report = await q.GroupBy(l => new { l.EmployeeId })
                .Select(g => new BonusReportDto(
                    g.Key.EmployeeId,
                    g.First().Employee!.FullName,
                    g.Sum(l => l.BonusAmount),
                    g.Count()
                )).ToListAsync();

            return Ok(report);
        }

        // ── Employee Bonus Detail ────────────────────────
        [HttpGet("detail")]
        public async Task<IActionResult> Detail([FromQuery] Guid employeeId, [FromQuery] DateTime? from, [FromQuery] DateTime? to,
            [FromQuery] int page = 1, [FromQuery] int size = 50)
        {
            var orgId = OrgId;
            var q = _db.EmployeeBonusLogs
                .Include(l => l.Batch).ThenInclude(b => b!.Product)
                .Where(l => l.OrgId == orgId && l.EmployeeId == employeeId);

            if (from.HasValue) q = q.Where(l => l.CreatedAt >= from.Value);
            if (to.HasValue) q = q.Where(l => l.CreatedAt <= to.Value);

            var total = await q.CountAsync();
            var items = await q.OrderByDescending(l => l.CreatedAt)
                .Skip((page - 1) * size).Take(size)
                .Select(l => new
                {
                    l.Id, l.CreatedAt, l.QtySold, l.BonusAmount,
                    ProductName = l.Batch!.Product!.NameEn,
                    l.Batch.Barcode
                }).ToListAsync();

            return Ok(new { total, page, size, items });
        }

        public record SetBonusReq(Guid BatchId, decimal BonusPerMainUnit, bool IsPercentage);
    }
}
