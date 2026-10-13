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
    [ApiController, Route("api/promotions/collections"), Authorize]
    public class PromotionCollectionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PromotionCollectionsController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // ── DTOs ──────────────────────────────
        public record CollectionDto(Guid Id, string Name, List<CollectionItemDto> Items);
        public record CollectionItemDto(Guid Id, Guid ProductId, string ProductName, string ProductCode);
        public record CreateCollectionReq(string Name, List<Guid> ProductIds);

        // ── List ────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orgId = OrgId;
            var items = await _db.PromotionCollections
                .Include(c => c.Items)
                .Where(c => c.OrgId == orgId && c.IsActive)
                .Select(c => new { c.Id, c.Name, ItemCount = c.Items!.Count })
                .ToListAsync();
            return Ok(items);
        }

        // ── Get One ─────────────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var c = await _db.PromotionCollections
                .Include(x => x.Items!).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId && x.IsActive);
            
            if (c == null) return NotFound();

            return Ok(new CollectionDto(
                c.Id, c.Name,
                c.Items!.Select(i => new CollectionItemDto(i.Id, i.ProductId, i.Product!.NameEn, i.Product.ProductCode)).ToList()
            ));
        }

        // ── Create ──────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCollectionReq req)
        {
            var orgId = OrgId;
            var collection = new PromotionCollection
            {
                Id = Guid.NewGuid(), OrgId = orgId,
                Name = req.Name.Trim(),
                IsActive = true,
                Items = req.ProductIds.Distinct().Select(pid => new PromotionCollectionItem
                {
                    Id = Guid.NewGuid(), OrgId = orgId, ProductId = pid, IsActive = true
                }).ToList()
            };

            _db.PromotionCollections.Add(collection);
            await _db.SaveChangesAsync();
            return Ok(new { collection.Id });
        }

        // ── Update ──────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateCollectionReq req)
        {
            var orgId = OrgId;
            var collection = await _db.PromotionCollections
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id && c.OrgId == orgId && c.IsActive);
                
            if (collection == null) return NotFound();

            collection.Name = req.Name.Trim();
            
            // Rebuild items
            _db.PromotionCollectionItems.RemoveRange(collection.Items ?? new List<PromotionCollectionItem>());
            
            collection.Items = req.ProductIds.Distinct().Select(pid => new PromotionCollectionItem
            {
                Id = Guid.NewGuid(), OrgId = orgId, CollectionId = id, ProductId = pid, IsActive = true
            }).ToList();

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Delete ──────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var collection = await _db.PromotionCollections.FirstOrDefaultAsync(c => c.Id == id && c.OrgId == OrgId);
            if (collection == null) return NotFound();
            collection.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
