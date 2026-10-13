using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController, Route("api/customers"), Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CustomersController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());
        private Guid? BranchId
        {
            get
            {
                var v = User.FindFirst("branch_id")?.Value;
                return string.IsNullOrEmpty(v) ? null : Guid.Parse(v);
            }
        }

        // ── DTOs ──────────────────────────────────────
        public record CustomerDto(
            Guid Id, string CustomerCode, string Name, string? Phone,
            string? Address, string? Notes,
            decimal TotalPoints, decimal PointsBalance, decimal MoneyBalance,
            int VisitCount, DateTime? LastVisitDate);

        public record CreateCustomerReq(
            string CustomerCode, string Name, string? Phone,
            string? Address, string? Notes);

        public record UpdateCustomerReq(
            string Name, string? Phone, string? Address, string? Notes);

        // ── GET all ──────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? q)
        {
            var orgId = OrgId;
            var query = _db.Customers.Where(c => c.OrgId == orgId);

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(c =>
                    c.CustomerCode.Contains(q) ||
                    c.Name.Contains(q) ||
                    (c.Phone != null && c.Phone.Contains(q)));
            }

            var list = await query.OrderBy(c => c.Name)
                .Select(c => new CustomerDto(
                    c.Id, c.CustomerCode, c.Name, c.Phone,
                    c.Address, c.Notes,
                    c.TotalPoints, c.PointsBalance, c.MoneyBalance,
                    c.VisitCount, c.LastVisitDate))
                .Take(100)
                .ToListAsync();

            return Ok(list);
        }

        // ── GET by code ──────────────────────────────
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var orgId = OrgId;
            var c = await _db.Customers.FirstOrDefaultAsync(x => x.OrgId == orgId && x.CustomerCode == code);
            if (c == null) return NotFound("العميل غير موجود");

            return Ok(new CustomerDto(
                c.Id, c.CustomerCode, c.Name, c.Phone,
                c.Address, c.Notes,
                c.TotalPoints, c.PointsBalance, c.MoneyBalance,
                c.VisitCount, c.LastVisitDate));
        }

        // ── GET by id ────────────────────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var c = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (c == null) return NotFound();

            return Ok(new CustomerDto(
                c.Id, c.CustomerCode, c.Name, c.Phone,
                c.Address, c.Notes,
                c.TotalPoints, c.PointsBalance, c.MoneyBalance,
                c.VisitCount, c.LastVisitDate));
        }

        // ── POST create ──────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerReq req)
        {
            var orgId = OrgId;
            var exists = await _db.Customers.AnyAsync(c => c.OrgId == orgId && c.CustomerCode == req.CustomerCode);
            if (exists) return BadRequest("كود العميل مستخدم بالفعل");

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = BranchId,
                CustomerCode = req.CustomerCode.Trim(),
                Name = req.Name.Trim(),
                Phone = req.Phone?.Trim(),
                Address = req.Address?.Trim(),
                Notes = req.Notes?.Trim(),
                IsActive = true
            };
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return Ok(new CustomerDto(
                customer.Id, customer.CustomerCode, customer.Name, customer.Phone,
                customer.Address, customer.Notes,
                customer.TotalPoints, customer.PointsBalance, customer.MoneyBalance,
                customer.VisitCount, customer.LastVisitDate));
        }

        // ── PUT update ───────────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerReq req)
        {
            var orgId = OrgId;
            var c = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (c == null) return NotFound();

            c.Name = req.Name.Trim();
            c.Phone = req.Phone?.Trim();
            c.Address = req.Address?.Trim();
            c.Notes = req.Notes?.Trim();

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── DELETE (soft) ────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orgId = OrgId;
            var c = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (c == null) return NotFound();
            c.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
