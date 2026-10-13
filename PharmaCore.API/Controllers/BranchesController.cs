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
    [ApiController]
    [Route("api/branches")]
    [Authorize]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BranchesController(AppDbContext db)
        {
            _db = db;
        }

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException("Organization ID claim not found."));

        // ── DTOs ──────────────────────────────────────
        public record BranchManagerDto(string Id, string Name, string Phone, string? Email);

        public record BranchDto(
            Guid Id,
            string Code,
            string Name,
            string? NameAr,
            string? Address,
            string? City,
            List<string> Phones,
            string? Email,
            BranchManagerDto? Manager,
            double? Latitude,
            double? Longitude,
            string? Description,
            string? DescriptionAr,
            string? OpeningHours,
            string? EstablishedDate,
            double? TotalArea,
            string Status,
            int EmployeeCount,
            DateTime CreatedAt,
            DateTime? UpdatedAt
        );

        public record BranchManagerReq(string Name, string Phone, string? Email);

        public record CreateBranchReq(
            string Code,
            string Name,
            string? NameAr,
            string? Address,
            string? City,
            List<string>? Phones,
            string? Email,
            BranchManagerReq? Manager,
            double? Latitude,
            double? Longitude,
            string? Description,
            string? DescriptionAr,
            string? OpeningHours,
            string? EstablishedDate,
            double? TotalArea,
            string Status
        );

        // ── GET all ──────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orgId = OrgId;
            var branches = await _db.Branches
                .IgnoreQueryFilters() // Fetch active and inactive branches
                .Where(b => b.OrgId == orgId && b.IsActive)
                .ToListAsync();

            var branchIds = branches.Select(b => b.Id).ToList();
            var employeeCounts = await _db.Employees
                .Where(e => e.BranchId.HasValue && branchIds.Contains(e.BranchId.Value))
                .GroupBy(e => e.BranchId!.Value)
                .Select(g => new { BranchId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.BranchId, x => x.Count);

            var list = branches.Select(b =>
            {
                var phoneList = !string.IsNullOrEmpty(b.Phones)
                    ? b.Phones.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>();

                BranchManagerDto? manager = null;
                if (!string.IsNullOrEmpty(b.ManagerName))
                {
                    manager = new BranchManagerDto(
                        Id: b.Id.ToString(),
                        Name: b.ManagerName,
                        Phone: b.ManagerPhone ?? "",
                        Email: b.ManagerEmail
                    );
                }

                employeeCounts.TryGetValue(b.Id, out var empCount);

                return new BranchDto(
                    Id: b.Id,
                    Code: b.Code,
                    Name: b.Name,
                    NameAr: b.NameAr,
                    Address: b.Address,
                    City: b.City ?? "",
                    Phones: phoneList,
                    Email: b.Email,
                    Manager: manager,
                    Latitude: b.Latitude,
                    Longitude: b.Longitude,
                    Description: b.Description,
                    DescriptionAr: b.DescriptionAr,
                    OpeningHours: b.OpeningHours,
                    EstablishedDate: b.EstablishedDate,
                    TotalArea: b.TotalArea,
                    Status: b.Status,
                    EmployeeCount: empCount,
                    CreatedAt: b.CreatedAt,
                    UpdatedAt: b.UpdatedAt
                );
            }).ToList();

            return Ok(list);
        }

        // ── GET by id ────────────────────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var b = await _db.Branches
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId && x.IsActive);

            if (b == null) return NotFound();

            var phoneList = !string.IsNullOrEmpty(b.Phones)
                ? b.Phones.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>();

            BranchManagerDto? manager = null;
            if (!string.IsNullOrEmpty(b.ManagerName))
            {
                manager = new BranchManagerDto(
                    Id: b.Id.ToString(),
                    Name: b.ManagerName,
                    Phone: b.ManagerPhone ?? "",
                    Email: b.ManagerEmail
                );
            }

            var empCount = await _db.Employees.CountAsync(e => e.BranchId == b.Id);

            return Ok(new BranchDto(
                Id: b.Id,
                Code: b.Code,
                Name: b.Name,
                NameAr: b.NameAr,
                Address: b.Address,
                City: b.City ?? "",
                Phones: phoneList,
                Email: b.Email,
                Manager: manager,
                Latitude: b.Latitude,
                Longitude: b.Longitude,
                Description: b.Description,
                DescriptionAr: b.DescriptionAr,
                OpeningHours: b.OpeningHours,
                EstablishedDate: b.EstablishedDate,
                TotalArea: b.TotalArea,
                Status: b.Status,
                EmployeeCount: empCount,
                CreatedAt: b.CreatedAt,
                UpdatedAt: b.UpdatedAt
            ));
        }

        // ── POST create ──────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchReq req)
        {
            var orgId = OrgId;

            // Check if code is unique
            var exists = await _db.Branches.IgnoreQueryFilters()
                .AnyAsync(b => b.OrgId == orgId && b.Code == req.Code.Trim() && b.IsActive);
            if (exists) return BadRequest("كود الفرع مستخدم بالفعل");

            // Self-healing: Resolve or create a region and tenant for constraints
            var regionId = await _db.Regions
                .Where(r => r.Tenant != null && r.Tenant.OrgId == orgId && r.IsActive)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (regionId == Guid.Empty)
            {
                var tenant = await _db.Tenants.FirstOrDefaultAsync(t => t.OrgId == orgId && t.IsActive);
                if (tenant == null)
                {
                    tenant = new Tenant
                    {
                        Id = Guid.NewGuid(),
                        OrgId = orgId,
                        Code = "DEFAULT",
                        Name = "Default Tenant",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    _db.Tenants.Add(tenant);
                    await _db.SaveChangesAsync();
                }

                var region = new Region
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenant.Id,
                    Code = "DEFAULT",
                    Name = "Default Region",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Regions.Add(region);
                await _db.SaveChangesAsync();
                regionId = region.Id;
            }

            var phoneString = req.Phones != null ? string.Join(";", req.Phones.Where(p => !string.IsNullOrWhiteSpace(p))) : null;

            var branch = new Branch
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                RegionId = regionId,
                Code = req.Code.Trim(),
                Name = req.Name.Trim(),
                NameAr = req.NameAr?.Trim(),
                Address = req.Address?.Trim(),
                City = req.City?.Trim(),
                Phones = phoneString,
                Email = req.Email?.Trim(),
                ManagerName = req.Manager?.Name?.Trim(),
                ManagerPhone = req.Manager?.Phone?.Trim(),
                ManagerEmail = req.Manager?.Email?.Trim(),
                Latitude = req.Latitude,
                Longitude = req.Longitude,
                Description = req.Description?.Trim(),
                DescriptionAr = req.DescriptionAr?.Trim(),
                OpeningHours = req.OpeningHours?.Trim(),
                EstablishedDate = req.EstablishedDate?.Trim(),
                TotalArea = req.TotalArea,
                Status = string.IsNullOrWhiteSpace(req.Status) ? "ACTIVE" : req.Status.ToUpper().Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Branches.Add(branch);
            await _db.SaveChangesAsync();

            var phoneList = !string.IsNullOrEmpty(branch.Phones)
                ? branch.Phones.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>();

            BranchManagerDto? manager = null;
            if (!string.IsNullOrEmpty(branch.ManagerName))
            {
                manager = new BranchManagerDto(
                    Id: branch.Id.ToString(),
                    Name: branch.ManagerName,
                    Phone: branch.ManagerPhone ?? "",
                    Email: branch.ManagerEmail
                );
            }

            return Ok(new BranchDto(
                Id: branch.Id,
                Code: branch.Code,
                Name: branch.Name,
                NameAr: branch.NameAr,
                Address: branch.Address,
                City: branch.City ?? "",
                Phones: phoneList,
                Email: branch.Email,
                Manager: manager,
                Latitude: branch.Latitude,
                Longitude: branch.Longitude,
                Description: branch.Description,
                DescriptionAr: branch.DescriptionAr,
                OpeningHours: branch.OpeningHours,
                EstablishedDate: branch.EstablishedDate,
                TotalArea: branch.TotalArea,
                Status: branch.Status,
                EmployeeCount: 0,
                CreatedAt: branch.CreatedAt,
                UpdatedAt: branch.UpdatedAt
            ));
        }

        // ── PUT update ───────────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateBranchReq req)
        {
            var orgId = OrgId;
            var b = await _db.Branches
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId && x.IsActive);

            if (b == null) return NotFound();

            // Validate code is not taken by another branch
            var exists = await _db.Branches.IgnoreQueryFilters()
                .AnyAsync(x => x.Id != id && x.OrgId == orgId && x.Code == req.Code.Trim() && x.IsActive);
            if (exists) return BadRequest("كود الفرع مستخدم بالفعل من قبل فرع آخر");

            var phoneString = req.Phones != null ? string.Join(";", req.Phones.Where(p => !string.IsNullOrWhiteSpace(p))) : null;

            b.Code = req.Code.Trim();
            b.Name = req.Name.Trim();
            b.NameAr = req.NameAr?.Trim();
            b.Address = req.Address?.Trim();
            b.City = req.City?.Trim();
            b.Phones = phoneString;
            b.Email = req.Email?.Trim();
            b.ManagerName = req.Manager?.Name?.Trim();
            b.ManagerPhone = req.Manager?.Phone?.Trim();
            b.ManagerEmail = req.Manager?.Email?.Trim();
            b.Latitude = req.Latitude;
            b.Longitude = req.Longitude;
            b.Description = req.Description?.Trim();
            b.DescriptionAr = req.DescriptionAr?.Trim();
            b.OpeningHours = req.OpeningHours?.Trim();
            b.EstablishedDate = req.EstablishedDate?.Trim();
            b.TotalArea = req.TotalArea;
            b.Status = string.IsNullOrWhiteSpace(req.Status) ? "ACTIVE" : req.Status.ToUpper().Trim();
            b.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── DELETE (soft) ────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orgId = OrgId;
            var b = await _db.Branches
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId && x.IsActive);

            if (b == null) return NotFound();

            b.IsActive = false;
            b.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
