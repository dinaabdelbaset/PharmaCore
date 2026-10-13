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
    [ApiController, Route("api/products"), Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // ── DTOs ──────────────────────────────────────────
        public record ProductListDto(Guid Id, string ProductCode, string NameEn, string? NameAr,
            string SearchName, string? CategoryName, string? ActiveIngredient,
            string? CompanyName, string? UsageType, int BatchCount, decimal TotalStock);

        public record ProductDetailDto(Guid Id, string ProductCode, string NameEn, string? NameAr,
            Guid? CategoryId, string? CategoryName, Guid? ActiveIngredientId, string? ActiveIngredient,
            string? UsageType, string? CompanyName, bool IsEnglishPrimary, string? Notes,
            decimal TotalSoldQty, decimal TotalPurchasedQty, decimal TotalReturnedQty,
            List<BatchDto> Batches);

        public record BatchDto(Guid Id, string Barcode, DateTime? ExpiryDate,
            decimal PharmacyBuyPrice, decimal PublicSalePrice, bool IsTaxable,
            decimal TaxPercent, decimal PriceAfterTax, decimal MaxDiscount, decimal MinQtyAlert,
            Guid MainUnitId, string? MainUnitName, int MainUnitQty,
            Guid SaleUnitId, string? SaleUnitName, Guid PurchaseUnitId, string? PurchaseUnitName,
            decimal TotalStock, List<BatchStockDto> Stocks, List<BatchUnitDto> Units,
            SellerBonusDto? SellerBonus);

        public record BatchStockDto(Guid Id, Guid BranchId, string? BranchName, Guid? WarehouseId, string? WarehouseName, decimal Quantity);
        public record BatchUnitDto(Guid Id, Guid UnitId, string UnitName, int QtyPerMainUnit, decimal UnitSalePrice);
        public record SellerBonusDto(Guid Id, decimal BonusPerMainUnit, bool IsPercentage);

        public record CreateProductReq(string NameEn, string? NameAr, Guid? CategoryId,
            Guid? ActiveIngredientId, string? UsageType, string? CompanyName,
            bool IsEnglishPrimary, string? Notes);

        public record UpdateProductReq(string NameEn, string? NameAr, Guid? CategoryId,
            Guid? ActiveIngredientId, string? UsageType, string? CompanyName,
            bool IsEnglishPrimary, string? Notes);

        // ── List Products ────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int size = 50)
        {
            var orgId = OrgId;
            var q = _db.Products.Where(p => p.OrgId == orgId && p.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(p => p.SearchName.ToLower().Contains(s) ||
                                 p.ProductCode.ToLower().Contains(s) ||
                                 (p.CompanyName != null && p.CompanyName.ToLower().Contains(s)));
            }

            var total = await q.CountAsync();
            var items = await q.OrderBy(p => p.ProductCode)
                .Skip((page - 1) * size).Take(size)
                .Select(p => new ProductListDto(
                    p.Id, p.ProductCode, p.NameEn, p.NameAr, p.SearchName,
                    p.Category != null ? p.Category.Name : null,
                    p.ActiveIngredientNav != null ? p.ActiveIngredientNav.Name : null,
                    p.CompanyName, p.UsageType,
                    p.Batches != null ? p.Batches.Count(b => b.IsActive) : 0,
                    p.Batches != null ? p.Batches.Where(b => b.IsActive)
                        .SelectMany(b => b.Stocks!.Where(s => s.IsActive))
                        .Sum(s => s.Quantity) : 0
                )).ToListAsync();

            return Ok(new { total, page, size, items });
        }

        [HttpGet("light")]
        public async Task<IActionResult> GetLightList()
        {
            var orgId = OrgId;
            var items = await _db.Products
                .Where(p => p.OrgId == orgId && p.IsActive)
                .Select(p => new
                {
                    p.Id,
                    p.ProductCode,
                    p.NameEn,
                    p.NameAr,
                    p.SearchName
                }).ToListAsync();
            return Ok(items);
        }

        // ── Get Product Detail ───────────────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var p = await _db.Products
                .Include(x => x.Category)
                .Include(x => x.ActiveIngredientNav)
                .Include(x => x.Batches!)
                    .ThenInclude(b => b.Stocks!)
                .Include(x => x.Batches!)
                    .ThenInclude(b => b.Units!)
                .Include(x => x.Batches!)
                    .ThenInclude(b => b.SellerBonus)
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);

            if (p == null) return NotFound();

            // Resolve unit names
            var unitIds = new HashSet<Guid>();
            var branchIds = new HashSet<Guid>();
            var warehouseIds = new HashSet<Guid>();
            foreach (var b in p.Batches ?? Enumerable.Empty<ProductBatch>())
            {
                unitIds.Add(b.MainUnitId); unitIds.Add(b.SaleUnitId); unitIds.Add(b.PurchaseUnitId);
                foreach (var u in b.Units ?? Enumerable.Empty<BatchUnit>()) unitIds.Add(u.UnitId);
                foreach (var s in b.Stocks ?? Enumerable.Empty<BatchStock>())
                {
                    branchIds.Add(s.BranchId);
                    if (s.WarehouseId.HasValue) warehouseIds.Add(s.WarehouseId.Value);
                }
            }
            var units = await _db.ProductUnits.IgnoreQueryFilters()
                .Where(u => unitIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id, u => u.Name);
            var branches = await _db.Branches.IgnoreQueryFilters()
                .Where(b => branchIds.Contains(b.Id)).ToDictionaryAsync(b => b.Id, b => b.Name);
            var warehouses = await _db.Warehouses.IgnoreQueryFilters()
                .Where(w => warehouseIds.Contains(w.Id)).ToDictionaryAsync(w => w.Id, w => w.Name);

            var dto = new ProductDetailDto(
                p.Id, p.ProductCode, p.NameEn, p.NameAr,
                p.CategoryId, p.Category?.Name, p.ActiveIngredientId, p.ActiveIngredientNav?.Name,
                p.UsageType, p.CompanyName, p.IsEnglishPrimary, p.Notes,
                p.TotalSoldQty, p.TotalPurchasedQty, p.TotalReturnedQty,
                (p.Batches ?? new List<ProductBatch>()).Select(b => new BatchDto(
                    b.Id, b.Barcode, b.ExpiryDate,
                    b.PharmacyBuyPrice, b.PublicSalePrice, b.IsTaxable,
                    b.TaxPercent, b.PriceAfterTax, b.MaxDiscount, b.MinQtyAlert,
                    b.MainUnitId, units.GetValueOrDefault(b.MainUnitId), b.MainUnitQty,
                    b.SaleUnitId, units.GetValueOrDefault(b.SaleUnitId),
                    b.PurchaseUnitId, units.GetValueOrDefault(b.PurchaseUnitId),
                    (b.Stocks ?? new List<BatchStock>()).Sum(s => s.Quantity),
                    (b.Stocks ?? new List<BatchStock>()).Select(s => new BatchStockDto(
                        s.Id, s.BranchId, branches.GetValueOrDefault(s.BranchId),
                        s.WarehouseId, s.WarehouseId.HasValue ? warehouses.GetValueOrDefault(s.WarehouseId.Value) : null,
                        s.Quantity)).ToList(),
                    (b.Units ?? new List<BatchUnit>()).Select(u => new BatchUnitDto(
                        u.Id, u.UnitId, u.UnitName, u.QtyPerMainUnit, u.UnitSalePrice)).ToList(),
                    b.SellerBonus != null && b.SellerBonus.IsActive
                        ? new SellerBonusDto(b.SellerBonus.Id, b.SellerBonus.BonusPerMainUnit, b.SellerBonus.IsPercentage)
                        : null
                )).ToList()
            );

            return Ok(dto);
        }

        // ── Search by name (F1 popup) ────────────────────
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return Ok(Array.Empty<object>());
            var orgId = OrgId;
            var s = q.Trim().ToLower();
            var items = await _db.Products.Where(p => p.OrgId == orgId && p.IsActive &&
                    (p.SearchName.ToLower().Contains(s) || p.NameEn.ToLower().Contains(s) ||
                     (p.NameAr != null && p.NameAr.ToLower().Contains(s)) ||
                     p.ProductCode.ToLower().Contains(s)))
                .OrderBy(p => p.SearchName)
                .Take(30)
                .Select(p => new { p.Id, p.ProductCode, p.NameEn, p.NameAr, p.CompanyName })
                .ToListAsync();
            return Ok(items);
        }

        // ── Create Product ───────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductReq req)
        {
            var orgId = OrgId;

            // Auto product code
            var maxCodeList = await _db.Products.IgnoreQueryFilters()
                .Where(p => p.OrgId == orgId && p.ProductCode.StartsWith("P-"))
                .Select(p => p.ProductCode)
                .ToListAsync();

            int nextNum = 1;
            if (maxCodeList.Any())
            {
                nextNum = maxCodeList.Max(c => int.TryParse(c.Substring(2), out var n) ? n : 0) + 1;
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                ProductCode = $"P-{nextNum}",
                NameEn = req.NameEn.Trim(),
                NameAr = req.NameAr?.Trim(),
                SearchName = req.IsEnglishPrimary ? req.NameEn.Trim() : (req.NameAr?.Trim() ?? req.NameEn.Trim()),
                CategoryId = req.CategoryId,
                ActiveIngredientId = req.ActiveIngredientId,
                UsageType = req.UsageType?.Trim(),
                CompanyName = req.CompanyName?.Trim(),
                IsEnglishPrimary = req.IsEnglishPrimary,
                Notes = req.Notes?.Trim(),
                IsActive = true
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok(new { product.Id, product.ProductCode });
        }

        // ── Update Product ───────────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductReq req)
        {
            var orgId = OrgId;
            var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (p == null) return NotFound();

            p.NameEn = req.NameEn.Trim();
            p.NameAr = req.NameAr?.Trim();
            p.SearchName = req.IsEnglishPrimary ? req.NameEn.Trim() : (req.NameAr?.Trim() ?? req.NameEn.Trim());
            p.CategoryId = req.CategoryId;
            p.ActiveIngredientId = req.ActiveIngredientId;
            p.UsageType = req.UsageType?.Trim();
            p.CompanyName = req.CompanyName?.Trim();
            p.IsEnglishPrimary = req.IsEnglishPrimary;
            p.Notes = req.Notes?.Trim();

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Delete Product ───────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orgId = OrgId;
            var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (p == null) return NotFound();
            p.IsActive = false;

            // Soft delete all batches
            var batches = await _db.ProductBatches.Where(b => b.ProductId == id && b.OrgId == orgId).ToListAsync();
            foreach (var b in batches) b.IsActive = false;

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Get by product code ──────────────────────────
        [HttpGet("by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var orgId = OrgId;
            var p = await _db.Products.FirstOrDefaultAsync(x => x.ProductCode == code && x.OrgId == orgId);
            if (p == null) return NotFound();
            return await GetById(p.Id);
        }
        // ── Get Promotions for Product ───────────────────
        [HttpGet("{id}/promotions")]
        public async Task<IActionResult> GetPromotions(Guid id)
        {
            var orgId = OrgId;
            var now = DateTime.UtcNow;

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id && p.OrgId == orgId && p.IsActive);
            if (product == null) return NotFound();

            var promos = await _db.Promotions
                .Include(p => p.Rules)
                .Where(p => p.OrgId == orgId && p.StartDate <= now && p.EndDate >= now && p.IsActive)
                .OrderByDescending(p => p.Priority)
                .ToListAsync();

            var collectionIds = promos.SelectMany(p => p.Rules ?? new List<PromotionRule>())
                .Where(r => r.ConditionType == PromotionConditionType.CustomCollection && r.ConditionCollectionId.HasValue)
                .Select(r => r.ConditionCollectionId!.Value)
                .Distinct().ToList();

            var collectionItems = await _db.PromotionCollectionItems
                .Where(c => c.OrgId == orgId && c.ProductId == id && collectionIds.Contains(c.CollectionId))
                .Select(c => c.CollectionId)
                .ToListAsync();

            var activePromos = new List<object>();

            foreach (var promo in promos)
            {
                foreach (var rule in promo.Rules ?? new List<PromotionRule>())
                {
                    bool matches = rule.ConditionType switch
                    {
                        PromotionConditionType.SpecificProduct => rule.ConditionProductId == id,
                        PromotionConditionType.SpecificCategory => rule.ConditionCategoryId != null && product.CategoryId == rule.ConditionCategoryId,
                        PromotionConditionType.SpecificBrand => !string.IsNullOrEmpty(rule.ConditionBrand) && string.Equals(product.CompanyName, rule.ConditionBrand, StringComparison.OrdinalIgnoreCase),
                        PromotionConditionType.AnyProduct => true,
                        PromotionConditionType.CustomCollection => rule.ConditionCollectionId.HasValue && collectionItems.Contains(rule.ConditionCollectionId.Value),
                        _ => false
                    };
                    if (matches)
                    {
                        activePromos.Add(new { Id = promo.Id, Name = promo.Name, Type = (int)promo.Type });
                        break; // don't add same promo twice
                    }
                }
            }

            return Ok(activePromos);
        }

        // ── Get Suppliers associated with Product ────────
        [HttpGet("{id}/suppliers")]
        public async Task<IActionResult> GetSuppliersForProduct(Guid id)
        {
            var orgId = OrgId;
            
            // 1. Get from purchase history (real suppliers who supplied this product in invoices)
            var history = await _db.PurchaseLines
                .Include(pl => pl.Purchase)
                .ThenInclude(p => p!.Supplier)
                .Where(pl => pl.ProductId == id && pl.OrgId == orgId && pl.Purchase != null && pl.Purchase.Supplier != null)
                .Select(pl => new
                {
                    SupplierId = pl.Purchase.SupplierId,
                    SupplierCode = pl.Purchase.Supplier.Code,
                    SupplierName = pl.Purchase.Supplier.Name,
                    PurchaseDate = pl.Purchase.Date,
                    BuyPrice = pl.UnitPrice,
                    InvoiceNumber = pl.Purchase.InvoiceNumber
                })
                .OrderByDescending(x => x.PurchaseDate)
                .ToListAsync();

            // 2. Get general active suppliers to suggest
            var allSuppliers = await _db.Suppliers
                .Where(s => s.OrgId == orgId && s.IsActive)
                .Select(s => new
                {
                    SupplierId = s.Id,
                    SupplierCode = s.Code,
                    SupplierName = s.Name,
                    Phone = s.Phone
                })
                .ToListAsync();

            return Ok(new
            {
                history = history,
                allSuppliers = allSuppliers
            });
        }
    }
}
