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
    [ApiController, Route("api/promotions"), Authorize]
    public class PromotionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PromotionsController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // ── DTOs ──────────────────────────────────────────
        public record PromotionDto(Guid Id, string Name, string? Description,
            PromotionType Type, DateTime StartDate, DateTime EndDate,
            Guid? BranchId, string? BranchName, int Priority, bool IsActive,
            List<RuleDto> Rules);

        public record RuleDto(Guid Id, PromotionConditionType ConditionType,
            Guid? ConditionProductId, string? ConditionProductName,
            Guid? ConditionCategoryId, string? ConditionCategoryName,
            Guid? ConditionCollectionId, string? ConditionCollectionName,
            string? ConditionBrand, int BuyQty,
            PromotionRewardType RewardType, Guid? RewardProductId, string? RewardProductName,
            int RewardQty, decimal? DiscountPercent, decimal? DiscountAmount);

        public record CreatePromotionReq(string Name, string? Description,
            PromotionType Type, DateTime StartDate, DateTime EndDate,
            Guid? BranchId, int Priority, List<CreateRuleReq> Rules);

        public record CreateRuleReq(PromotionConditionType ConditionType,
            Guid? ConditionProductId, Guid? ConditionCategoryId, Guid? ConditionCollectionId, string? ConditionBrand,
            int BuyQty, PromotionRewardType RewardType, Guid? RewardProductId,
            int RewardQty, decimal? DiscountPercent, decimal? DiscountAmount);

        // ── List ─────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool activeOnly = true)
        {
            var orgId = OrgId;
            var now = DateTime.UtcNow;
            
            var q = _db.Promotions
                .Include(p => p.Rules)
                .Include(p => p.Branch)
                .Where(p => p.OrgId == orgId);

            if (activeOnly) q = q.Where(p => p.StartDate <= now && p.EndDate >= now);

            var entities = await q.OrderByDescending(p => p.Priority).ToListAsync();

            var items = entities.Select(p => new PromotionDto(
                p.Id, p.Name, p.Description, p.Type, p.StartDate, p.EndDate,
                p.BranchId, p.Branch?.Name, p.Priority, p.IsActive,
                (p.Rules ?? new List<PromotionRule>()).Select(r => new RuleDto(
                    r.Id, r.ConditionType,
                    r.ConditionProductId, null, r.ConditionCategoryId, null,
                    r.ConditionCollectionId, null,
                    r.ConditionBrand, r.BuyQty, r.RewardType,
                    r.RewardProductId, null, r.RewardQty,
                    r.DiscountPercent, r.DiscountAmount
                )).ToList()
            )).ToList();

            return Ok(items);
        }

        // ── Create ───────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionReq req)
        {
            var orgId = OrgId;
            var promo = new Promotion
            {
                Id = Guid.NewGuid(), OrgId = orgId,
                Name = req.Name.Trim(), Description = req.Description?.Trim(),
                Type = req.Type, StartDate = req.StartDate, EndDate = req.EndDate,
                BranchId = req.BranchId, Priority = req.Priority, IsActive = true
            };
            _db.Promotions.Add(promo);

            foreach (var r in req.Rules ?? new List<CreateRuleReq>())
            {
                _db.PromotionRules.Add(new PromotionRule
                {
                    Id = Guid.NewGuid(), OrgId = orgId, PromotionId = promo.Id,
                    ConditionType = r.ConditionType, ConditionProductId = r.ConditionProductId,
                    ConditionCategoryId = r.ConditionCategoryId, ConditionCollectionId = r.ConditionCollectionId, ConditionBrand = r.ConditionBrand,
                    BuyQty = r.BuyQty, RewardType = r.RewardType, RewardProductId = r.RewardProductId,
                    RewardQty = r.RewardQty, DiscountPercent = r.DiscountPercent,
                    DiscountAmount = r.DiscountAmount, IsActive = true
                });
            }

            await _db.SaveChangesAsync();
            return Ok(new { promo.Id });
        }

        // ── Update ───────────────────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreatePromotionReq req)
        {
            var orgId = OrgId;
            var promo = await _db.Promotions.FirstOrDefaultAsync(p => p.Id == id && p.OrgId == orgId);
            if (promo == null) return NotFound();

            promo.Name = req.Name.Trim();
            promo.Description = req.Description?.Trim();
            promo.Type = req.Type;
            promo.StartDate = req.StartDate;
            promo.EndDate = req.EndDate;
            promo.BranchId = req.BranchId;
            promo.Priority = req.Priority;

            // Replace rules
            var old = await _db.PromotionRules.Where(r => r.PromotionId == id).ToListAsync();
            _db.PromotionRules.RemoveRange(old);
            foreach (var r in req.Rules ?? new List<CreateRuleReq>())
            {
                _db.PromotionRules.Add(new PromotionRule
                {
                    Id = Guid.NewGuid(), OrgId = orgId, PromotionId = id,
                    ConditionType = r.ConditionType, ConditionProductId = r.ConditionProductId,
                    ConditionCategoryId = r.ConditionCategoryId, ConditionCollectionId = r.ConditionCollectionId, ConditionBrand = r.ConditionBrand,
                    BuyQty = r.BuyQty, RewardType = r.RewardType, RewardProductId = r.RewardProductId,
                    RewardQty = r.RewardQty, DiscountPercent = r.DiscountPercent,
                    DiscountAmount = r.DiscountAmount, IsActive = true
                });
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Delete ───────────────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var promo = await _db.Promotions.FirstOrDefaultAsync(p => p.Id == id && p.OrgId == OrgId);
            if (promo == null) return NotFound();
            promo.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Check Cart for Promotions ────────────────────
        [HttpPost("check-cart")]
        public async Task<IActionResult> CheckCart([FromBody] List<CartItem> items)
        {
            var orgId = OrgId;
            var branchId = User.FindFirst("branch_id")?.Value;
            var now = DateTime.UtcNow;

            // Get active promotions for this branch
            var promos = await _db.Promotions
                .Include(p => p.Rules)
                .Where(p => p.OrgId == orgId && p.StartDate <= now && p.EndDate >= now &&
                            (p.BranchId == null || (branchId != null && p.BranchId == Guid.Parse(branchId))))
                .OrderByDescending(p => p.Priority)
                .ToListAsync();

            // Get product info for cart items
            var batchIds = items.Select(i => i.BatchId).ToList();
            var batches = await _db.ProductBatches.Include(b => b.Product)
                .Where(b => batchIds.Contains(b.Id) && b.OrgId == orgId)
                .ToListAsync();

            var collectionIds = promos.SelectMany(p => p.Rules ?? new List<PromotionRule>())
                .Where(r => r.ConditionType == PromotionConditionType.CustomCollection && r.ConditionCollectionId.HasValue)
                .Select(r => r.ConditionCollectionId!.Value)
                .Distinct().ToList();
            var collectionItems = await _db.PromotionCollectionItems
                .Where(c => c.OrgId == orgId && collectionIds.Contains(c.CollectionId))
                .ToListAsync();

            var results = new List<PromotionMatch>();

            foreach (var promo in promos)
            {
                foreach (var rule in promo.Rules ?? new List<PromotionRule>())
                {
                    foreach (var item in items)
                    {
                        var batch = batches.FirstOrDefault(b => b.Id == item.BatchId);
                        if (batch == null) continue;

                        bool matches = rule.ConditionType switch
                        {
                            PromotionConditionType.SpecificProduct =>
                                rule.ConditionProductId == batch.ProductId,
                            PromotionConditionType.SpecificCategory =>
                                rule.ConditionCategoryId != null && batch.Product?.CategoryId == rule.ConditionCategoryId,
                            PromotionConditionType.SpecificBrand =>
                                !string.IsNullOrEmpty(rule.ConditionBrand) &&
                                string.Equals(batch.Product?.CompanyName, rule.ConditionBrand, StringComparison.OrdinalIgnoreCase),
                            PromotionConditionType.AnyProduct => true,
                            PromotionConditionType.CustomCollection =>
                                rule.ConditionCollectionId != null &&
                                collectionItems.Any(c => c.CollectionId == rule.ConditionCollectionId && c.ProductId == batch.ProductId),
                            _ => false
                        };

                        if (matches && item.Qty >= rule.BuyQty)
                        {
                            results.Add(new PromotionMatch(
                                promo.Id, promo.Name, promo.Type,
                                item.BatchId, batch.Product?.NameEn ?? "",
                                rule.RewardType, rule.RewardQty,
                                rule.DiscountPercent, rule.DiscountAmount
                            ));
                        }
                    }
                }
            }

            return Ok(results);
        }

        public record CartItem(Guid BatchId, int Qty);
        public record PromotionMatch(Guid PromotionId, string PromotionName, PromotionType Type,
            Guid BatchId, string ProductName,
            PromotionRewardType RewardType, int RewardQty,
            decimal? DiscountPercent, decimal? DiscountAmount);
    }
}
