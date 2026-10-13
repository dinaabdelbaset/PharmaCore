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
    /// <summary>Generic CRUD for simple lookup tables: Categories, Active Ingredients, Warehouses, Units.</summary>

    [ApiController, Route("api/categories"), Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.ProductCategories
                .Where(x => x.OrgId == OrgId)
                .OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name })
                .ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NameReq req)
        {
            var orgId = OrgId;
            var exists = await _db.ProductCategories.AnyAsync(x => x.OrgId == orgId && x.Name == req.Name.Trim());
            if (exists) return BadRequest("المجموعة موجودة بالفعل");
            var e = new ProductCategory { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), IsActive = true };
            _db.ProductCategories.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NameReq req)
        {
            var e = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    [ApiController, Route("api/active-ingredients"), Authorize]
    public class ActiveIngredientsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ActiveIngredientsController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.ActiveIngredients
                .Where(x => x.OrgId == OrgId).OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NameReq req)
        {
            var orgId = OrgId;
            var exists = await _db.ActiveIngredients.AnyAsync(x => x.OrgId == orgId && x.Name == req.Name.Trim());
            if (exists) return BadRequest("المادة الفعالة موجودة بالفعل");
            var e = new ActiveIngredient { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), IsActive = true };
            _db.ActiveIngredients.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NameReq req)
        {
            var e = await _db.ActiveIngredients.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.ActiveIngredients.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    [ApiController, Route("api/warehouses"), Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public WarehousesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? branchId)
        {
            var q = _db.Warehouses.Where(x => x.OrgId == OrgId);
            if (branchId.HasValue) q = q.Where(x => x.BranchId == branchId || x.BranchId == null);
            var items = await q.OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name, x.BranchId }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarehouseReq req)
        {
            var orgId = OrgId;
            var e = new Warehouse { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), BranchId = req.BranchId, IsActive = true };
            _db.Warehouses.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WarehouseReq req)
        {
            var e = await _db.Warehouses.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            e.BranchId = req.BranchId;
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.Warehouses.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    [ApiController, Route("api/units"), Authorize]
    public class UnitsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UnitsController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.ProductUnits
                .Where(x => x.OrgId == OrgId).OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name, x.Abbreviation }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnitCreateReq req)
        {
            var orgId = OrgId;
            var exists = await _db.ProductUnits.AnyAsync(x => x.OrgId == orgId && x.Name == req.Name.Trim());
            if (exists) return BadRequest("الوحدة موجودة بالفعل");
            var e = new ProductUnit { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), Abbreviation = req.Abbreviation?.Trim(), IsActive = true };
            _db.ProductUnits.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name, e.Abbreviation });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UnitCreateReq req)
        {
            var e = await _db.ProductUnits.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            e.Abbreviation = req.Abbreviation?.Trim();
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.ProductUnits.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    [ApiController, Route("api/companies"), Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CompaniesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.ProductCompanies
                .Where(x => x.OrgId == OrgId).OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NameReq req)
        {
            var orgId = OrgId;
            var exists = await _db.ProductCompanies.AnyAsync(x => x.OrgId == orgId && x.Name == req.Name.Trim());
            if (exists) return BadRequest("الشركة موجودة بالفعل");
            var e = new ProductCompany { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), IsActive = true };
            _db.ProductCompanies.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NameReq req)
        {
            var e = await _db.ProductCompanies.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.ProductCompanies.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    [ApiController, Route("api/usages"), Authorize]
    public class UsagesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UsagesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _db.ProductUsages
                .Where(x => x.OrgId == OrgId).OrderBy(x => x.Name)
                .Select(x => new { x.Id, x.Name }).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NameReq req)
        {
            var orgId = OrgId;
            var exists = await _db.ProductUsages.AnyAsync(x => x.OrgId == orgId && x.Name == req.Name.Trim());
            if (exists) return BadRequest("الاستخدام موجود بالفعل");
            var e = new ProductUsage { Id = Guid.NewGuid(), OrgId = orgId, Name = req.Name.Trim(), IsActive = true };
            _db.ProductUsages.Add(e);
            await _db.SaveChangesAsync();
            return Ok(new { e.Id, e.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NameReq req)
        {
            var e = await _db.ProductUsages.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.Name = req.Name.Trim();
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var e = await _db.ProductUsages.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == OrgId);
            if (e == null) return NotFound();
            e.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }

    // ── Shared DTOs ──────────────────────────────────
    public record NameReq(string Name);
    public record WarehouseReq(string Name, Guid? BranchId);
    public record UnitCreateReq(string Name, string? Abbreviation);
}
