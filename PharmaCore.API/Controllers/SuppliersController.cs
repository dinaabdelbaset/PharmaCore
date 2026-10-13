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
    [ApiController, Route("api/suppliers"), Authorize]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SuppliersController(AppDbContext db) => _db = db;

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        public record SupplierDto(Guid Id, string Code, string Name, string? Phone, string? Email, string? Address, string? TaxNumber, decimal Balance);
        public record CreateSupplierReq(string Name, string? Phone, string? Email, string? Address, string? TaxNumber);
        public record UpdateSupplierReq(string Name, string? Phone, string? Email, string? Address, string? TaxNumber, decimal Balance);

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var orgId = OrgId;
            var q = _db.Suppliers.Where(x => x.OrgId == orgId && x.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(x => x.Name.ToLower().Contains(s) || x.Code.ToLower().Contains(s));
            }

            var items = await q.OrderBy(x => x.Code)
                .Select(x => new SupplierDto(x.Id, x.Code, x.Name, x.Phone, x.Email, x.Address, x.TaxNumber, x.Balance))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var x = await _db.Suppliers.FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId && s.IsActive);
            if (x == null) return NotFound();

            return Ok(new SupplierDto(x.Id, x.Code, x.Name, x.Phone, x.Email, x.Address, x.TaxNumber, x.Balance));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierReq req)
        {
            var orgId = OrgId;

            // Generate supplier code
            var maxNum = await _db.Suppliers.IgnoreQueryFilters()
                .Where(s => s.OrgId == orgId && s.Code.StartsWith("SUP-"))
                .CountAsync();
            var code = $"SUP-{(maxNum + 1):D3}";

            var x = new Supplier
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                Code = code,
                Name = req.Name,
                Phone = req.Phone,
                Email = req.Email,
                Address = req.Address,
                TaxNumber = req.TaxNumber,
                Balance = 0.00m,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Suppliers.Add(x);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = x.Id }, new SupplierDto(x.Id, x.Code, x.Name, x.Phone, x.Email, x.Address, x.TaxNumber, x.Balance));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierReq req)
        {
            var orgId = OrgId;
            var x = await _db.Suppliers.FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId && s.IsActive);
            if (x == null) return NotFound();

            x.Name = req.Name;
            x.Phone = req.Phone;
            x.Email = req.Email;
            x.Address = req.Address;
            x.TaxNumber = req.TaxNumber;
            x.Balance = req.Balance;
            x.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok(new SupplierDto(x.Id, x.Code, x.Name, x.Phone, x.Email, x.Address, x.TaxNumber, x.Balance));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orgId = OrgId;
            var x = await _db.Suppliers.FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId && s.IsActive);
            if (x == null) return NotFound();

            x.IsActive = false;
            x.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
