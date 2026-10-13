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
    [ApiController, Route("api/batches"), Authorize]
    public class BatchesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BatchesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());
        private Guid? BranchId
        {
            get
            {
                var v = User.FindFirst("branch_id")?.Value;
                return string.IsNullOrEmpty(v) ? null : Guid.Parse(v);
            }
        }

        // ── DTOs ──────────────────────────────────────────
        public record CreateBatchReq(
            string Barcode, DateTime? ExpiryDate, decimal Quantity,
            Guid? WarehouseId, decimal PharmacyBuyPrice, decimal PublicSalePrice,
            bool IsTaxable, decimal TaxPercent, decimal MaxDiscount, decimal MinQtyAlert,
            Guid MainUnitId, int MainUnitQty, Guid SaleUnitId, Guid PurchaseUnitId,
            decimal? BonusPerMainUnit, bool? BonusIsPercentage,
            List<UnitReq>? AdditionalUnits);

        public record UpdateBatchReq(
            string Barcode, DateTime? ExpiryDate,
            Guid? WarehouseId, decimal PharmacyBuyPrice, decimal PublicSalePrice,
            bool IsTaxable, decimal TaxPercent, decimal MaxDiscount, decimal MinQtyAlert,
            Guid MainUnitId, int MainUnitQty, Guid SaleUnitId, Guid PurchaseUnitId,
            decimal? BonusPerMainUnit, bool? BonusIsPercentage,
            List<UnitReq>? AdditionalUnits);

        public record UnitReq(Guid UnitId, string UnitName, int QtyPerMainUnit, decimal UnitSalePrice);
        public record StockAdjustReq(decimal Quantity, Guid? WarehouseId);

        public record BarcodeLookupResult(
            Guid ProductId, string ProductCode, string ProductName, string? CompanyName,
            Guid BatchId, string Barcode, DateTime? ExpiryDate,
            decimal PublicSalePrice, decimal PriceAfterTax, decimal MaxDiscount,
            bool IsTaxable, decimal TaxPercent,
            Guid MainUnitId, string? MainUnitName, int MainUnitQty,
            decimal StockInBranch,
            decimal? BonusPerMainUnit, bool? BonusIsPercentage,
            List<ProductsController.BatchUnitDto> Units);

        public record SyncCatalogResult(
            Guid ProductId, string ProductName, string ProductCode, string? CompanyName,
            Guid BatchId, string Barcode, DateTime? ExpiryDate,
            decimal PublicSalePrice, decimal PriceAfterTax, decimal MaxDiscount,
            bool IsTaxable, decimal TaxPercent,
            Guid MainUnitId, string? MainUnitName, int MainUnitQty,
            decimal StockQty,
            List<SyncCatalogUnit> Units);

        public record SyncCatalogUnit(
            Guid Id, Guid UnitId, string UnitName, int QtyPerMainUnit, decimal UnitSalePrice);

        // ── Add Batch to Product ─────────────────────────
        [HttpPost("/api/products/{productId}/batches")]
        public async Task<IActionResult> Create(Guid productId, [FromBody] CreateBatchReq req)
        {
            var orgId = OrgId;
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId && p.OrgId == orgId);
            if (product == null) return NotFound("المنتج غير موجود");

            // Check barcode uniqueness
            var exists = await _db.ProductBatches.AnyAsync(b => b.Barcode == req.Barcode && b.OrgId == orgId);
            if (exists) return BadRequest("الباركود مستخدم بالفعل");

            var priceAfterTax = req.IsTaxable
                ? req.PublicSalePrice + (req.PublicSalePrice * req.TaxPercent / 100)
                : req.PublicSalePrice;

            var batch = new ProductBatch
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                ProductId = productId,
                Barcode = req.Barcode.Trim(),
                ExpiryDate = req.ExpiryDate?.ToUniversalTime(),
                PharmacyBuyPrice = req.PharmacyBuyPrice,
                PublicSalePrice = req.PublicSalePrice,
                IsTaxable = req.IsTaxable,
                TaxPercent = req.TaxPercent,
                PriceAfterTax = priceAfterTax,
                MaxDiscount = req.MaxDiscount,
                MinQtyAlert = req.MinQtyAlert,
                MainUnitId = req.MainUnitId,
                MainUnitQty = req.MainUnitQty > 0 ? req.MainUnitQty : 1,
                SaleUnitId = req.SaleUnitId,
                PurchaseUnitId = req.PurchaseUnitId,
                IsActive = true
            };
            _db.ProductBatches.Add(batch);

            // Stock for current branch
            var branchId = BranchId;
            if (branchId.HasValue && req.Quantity > 0)
            {
                _db.BatchStocks.Add(new BatchStock
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    BatchId = batch.Id,
                    BranchId = branchId.Value,
                    WarehouseId = req.WarehouseId,
                    Quantity = req.Quantity,
                    IsActive = true
                });
            }

            // Additional units
            if (req.AdditionalUnits != null)
            {
                foreach (var u in req.AdditionalUnits)
                {
                    _db.BatchUnits.Add(new BatchUnit
                    {
                        Id = Guid.NewGuid(),
                        OrgId = orgId,
                        BatchId = batch.Id,
                        UnitId = u.UnitId,
                        UnitName = u.UnitName,
                        QtyPerMainUnit = u.QtyPerMainUnit,
                        UnitSalePrice = u.UnitSalePrice,
                        IsActive = true
                    });
                }
            }

            // Seller bonus
            if (req.BonusPerMainUnit.HasValue && req.BonusPerMainUnit.Value > 0)
            {
                _db.SellerBonuses.Add(new SellerBonus
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    BatchId = batch.Id,
                    BonusPerMainUnit = req.BonusPerMainUnit.Value,
                    IsPercentage = req.BonusIsPercentage ?? false,
                    IsActive = true
                });
            }

            await _db.SaveChangesAsync();
            return Ok(new { batch.Id, batch.Barcode });
        }

        // ── Update Batch ─────────────────────────────────
        [HttpPut("{batchId}")]
        public async Task<IActionResult> Update(Guid batchId, [FromBody] UpdateBatchReq req)
        {
            var orgId = OrgId;
            var batch = await _db.ProductBatches.FirstOrDefaultAsync(b => b.Id == batchId && b.OrgId == orgId);
            if (batch == null) return NotFound();

            // Check barcode uniqueness (if changed)
            if (batch.Barcode != req.Barcode.Trim())
            {
                var exists = await _db.ProductBatches.AnyAsync(b => b.Barcode == req.Barcode.Trim() && b.OrgId == orgId && b.Id != batchId);
                if (exists) return BadRequest("الباركود مستخدم بالفعل");
            }

            batch.Barcode = req.Barcode.Trim();
            batch.ExpiryDate = req.ExpiryDate?.ToUniversalTime();
            batch.PharmacyBuyPrice = req.PharmacyBuyPrice;
            batch.PublicSalePrice = req.PublicSalePrice;
            batch.IsTaxable = req.IsTaxable;
            batch.TaxPercent = req.TaxPercent;
            batch.PriceAfterTax = req.IsTaxable
                ? req.PublicSalePrice + (req.PublicSalePrice * req.TaxPercent / 100)
                : req.PublicSalePrice;
            batch.MaxDiscount = req.MaxDiscount;
            batch.MinQtyAlert = req.MinQtyAlert;
            batch.MainUnitId = req.MainUnitId;
            batch.MainUnitQty = req.MainUnitQty > 0 ? req.MainUnitQty : 1;
            batch.SaleUnitId = req.SaleUnitId;
            batch.PurchaseUnitId = req.PurchaseUnitId;

            // Update warehouse on current branch stock
            if (req.WarehouseId.HasValue && BranchId.HasValue)
            {
                var stock = await _db.BatchStocks.FirstOrDefaultAsync(s => s.BatchId == batchId && s.BranchId == BranchId.Value);
                if (stock != null) stock.WarehouseId = req.WarehouseId;
            }

            // Replace additional units
            var oldUnits = await _db.BatchUnits.Where(u => u.BatchId == batchId).ToListAsync();
            _db.BatchUnits.RemoveRange(oldUnits);
            if (req.AdditionalUnits != null)
            {
                foreach (var u in req.AdditionalUnits)
                {
                    _db.BatchUnits.Add(new BatchUnit
                    {
                        Id = Guid.NewGuid(), OrgId = orgId, BatchId = batchId,
                        UnitId = u.UnitId, UnitName = u.UnitName,
                        QtyPerMainUnit = u.QtyPerMainUnit, UnitSalePrice = u.UnitSalePrice,
                        IsActive = true
                    });
                }
            }

            // Update seller bonus
            var bonus = await _db.SellerBonuses.FirstOrDefaultAsync(b => b.BatchId == batchId);
            if (req.BonusPerMainUnit.HasValue && req.BonusPerMainUnit.Value > 0)
            {
                if (bonus != null)
                {
                    bonus.BonusPerMainUnit = req.BonusPerMainUnit.Value;
                    bonus.IsPercentage = req.BonusIsPercentage ?? false;
                }
                else
                {
                    _db.SellerBonuses.Add(new SellerBonus
                    {
                        Id = Guid.NewGuid(), OrgId = orgId, BatchId = batchId,
                        BonusPerMainUnit = req.BonusPerMainUnit.Value,
                        IsPercentage = req.BonusIsPercentage ?? false,
                        IsActive = true
                    });
                }
            }
            else if (bonus != null)
            {
                bonus.IsActive = false;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Delete Batch ─────────────────────────────────
        [HttpDelete("{batchId}")]
        public async Task<IActionResult> Delete(Guid batchId)
        {
            var orgId = OrgId;
            var batch = await _db.ProductBatches.FirstOrDefaultAsync(b => b.Id == batchId && b.OrgId == orgId);
            if (batch == null) return NotFound();
            batch.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Sync Catalog ─────────────────────────────────
        [HttpGet("sync-catalog")]
        public async Task<IActionResult> SyncCatalog()
        {
            var orgId = OrgId;
            var branchId = BranchId;

            var result = await _db.ProductBatches
                .Where(b => b.OrgId == orgId && b.IsActive && b.Product!.IsActive)
                .Select(b => new SyncCatalogResult(
                    b.ProductId,
                    b.Product!.IsEnglishPrimary ? b.Product.NameEn : (b.Product.NameAr ?? b.Product.NameEn),
                    b.Product.ProductCode,
                    b.Product.CompanyName,
                    b.Id,
                    b.Barcode,
                    b.ExpiryDate,
                    b.PublicSalePrice,
                    b.PriceAfterTax,
                    b.MaxDiscount,
                    b.IsTaxable,
                    b.TaxPercent,
                    b.MainUnitId,
                    _db.ProductUnits.Where(u => u.Id == b.MainUnitId).Select(u => u.Name).FirstOrDefault(),
                    b.MainUnitQty,
                    b.Stocks!.Where(s => s.IsActive && (!branchId.HasValue || s.BranchId == branchId.Value)).Sum(s => s.Quantity),
                    b.Units!.Where(u => u.IsActive).Select(u => new SyncCatalogUnit(
                        u.Id,
                        u.UnitId,
                        u.UnitName,
                        u.QtyPerMainUnit,
                        u.UnitSalePrice
                    )).ToList()
                ))
                .ToListAsync();

            return Ok(result);
        }

        // ── Barcode Lookup (Cashier) ─────────────────────
        [HttpGet("by-barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var orgId = OrgId;
            var branchId = BranchId;

            var batch = await _db.ProductBatches
                .Include(b => b.Product)
                .Include(b => b.Units!.Where(u => u.IsActive))
                .Include(b => b.SellerBonus)
                .FirstOrDefaultAsync(b => b.Barcode == barcode && b.OrgId == orgId);

            if (batch == null) return NotFound("الباركود غير موجود");

            decimal stock = 0;
            if (branchId.HasValue)
            {
                stock = await _db.BatchStocks
                    .Where(s => s.BatchId == batch.Id && s.BranchId == branchId.Value)
                    .SumAsync(s => s.Quantity);
            }

            var mainUnit = await _db.ProductUnits.FindAsync(batch.MainUnitId);

            var result = new BarcodeLookupResult(
                batch.ProductId, batch.Product!.ProductCode,
                batch.Product.IsEnglishPrimary ? batch.Product.NameEn : (batch.Product.NameAr ?? batch.Product.NameEn),
                batch.Product.CompanyName,
                batch.Id, batch.Barcode, batch.ExpiryDate,
                batch.PublicSalePrice, batch.PriceAfterTax, batch.MaxDiscount,
                batch.IsTaxable, batch.TaxPercent,
                batch.MainUnitId, mainUnit?.Name, batch.MainUnitQty,
                stock,
                batch.SellerBonus?.IsActive == true ? batch.SellerBonus.BonusPerMainUnit : null,
                batch.SellerBonus?.IsActive == true ? batch.SellerBonus.IsPercentage : null,
                (batch.Units ?? new List<BatchUnit>()).Select(u =>
                    new ProductsController.BatchUnitDto(u.Id, u.UnitId, u.UnitName, u.QtyPerMainUnit, u.UnitSalePrice)).ToList()
            );

            return Ok(result);
        }

        // ── Search Batches (for Frm_ItemSales) ───────────────
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? q,
            [FromQuery] string field = "name")   // name / barcode / ingredient / nameAr
        {
            var orgId = OrgId;
            var branchId = BranchId;

            if (string.IsNullOrWhiteSpace(q))
                return Ok(new List<object>());

            var query = _db.Products
                .Include(p => p.Batches)
                .Include(p => p.ActiveIngredientNav)
                .Where(p => p.OrgId == orgId);

            // Wildcard support: "pan*adv" → ["pan","adv"]
            var words = q.Split('*', StringSplitOptions.RemoveEmptyEntries)
                         .Select(w => w.Trim())
                         .Where(w => w.Length > 0)
                         .ToList();

            foreach (var word in words)
            {
                var w = word; // closure capture
                switch (field.ToLower())
                {
                    case "barcode":
                        query = query.Where(p => p.Batches.Any(b => b.Barcode != null && b.Barcode.Contains(w)));
                        break;
                    case "namear":
                        query = query.Where(p => p.NameAr != null && p.NameAr.Contains(w));
                        break;
                    case "ingredient":
                        query = query.Where(p => p.ActiveIngredientNav != null && p.ActiveIngredientNav.Name != null && p.ActiveIngredientNav.Name.Contains(w));
                        break;
                    case "all":
                        var wLower = w.ToLower();
                        query = query.Where(p => 
                            (p.NameEn != null && p.NameEn.ToLower().Contains(wLower)) || 
                            (p.NameAr != null && p.NameAr.ToLower().Contains(wLower)) ||
                            (p.ActiveIngredientNav != null && p.ActiveIngredientNav.Name != null && p.ActiveIngredientNav.Name.ToLower().Contains(wLower)) ||
                            p.Batches.Any(b => b.Barcode != null && b.Barcode.ToLower().Contains(wLower)));
                        break;
                    default: // name (English)
                        var wl = w.ToLower();
                        query = query.Where(p => p.NameEn != null && p.NameEn.ToLower().Contains(wl));
                        break;
                }
            }

            var productsList = await query.Take(100).ToListAsync();
            
            // Build flat DTO list
            var results = new List<dynamic>();
            foreach (var p in productsList)
            {
                var batches = p.Batches.ToList();
                if (batches.Any())
                {
                    foreach(var b in batches)
                    {
                        results.Add(new {
                            ProductId = p.Id,
                            ProductName = p.IsEnglishPrimary ? p.NameEn : (p.NameAr ?? p.NameEn),
                            ProductNameAr = p.NameAr,
                            ExpiryDate = (DateTime?)b.ExpiryDate,
                            SalePrice = b.PublicSalePrice,
                            Barcode = b.Barcode,
                            BuyPrice = b.PharmacyBuyPrice,
                            MinQtyAlert = b.MinQtyAlert,
                            CompanyName = p.CompanyName,
                            ActiveIngredient = p.ActiveIngredientNav?.Name,
                            BatchId = (Guid?)b.Id
                        });
                    }
                }
                else
                {
                    results.Add(new {
                        ProductId = p.Id,
                        ProductName = p.IsEnglishPrimary ? p.NameEn : (p.NameAr ?? p.NameEn),
                        ProductNameAr = p.NameAr,
                        ExpiryDate = (DateTime?)null,
                        SalePrice = 0m,
                        Barcode = "",
                        BuyPrice = 0m,
                        MinQtyAlert = 0m,
                        CompanyName = p.CompanyName,
                        ActiveIngredient = p.ActiveIngredientNav?.Name,
                        BatchId = (Guid?)null
                    });
                }
            }

            // Get stock quantities for the branch
            if (branchId.HasValue && results.Count > 0)
            {
                var batchIds = results.Where(r => r.BatchId != null).Select(r => (Guid)r.BatchId).ToList();
                var stocks = new Dictionary<Guid, decimal>();
                
                if (batchIds.Any())
                {
                    stocks = await _db.BatchStocks
                        .Where(s => batchIds.Contains(s.BatchId) && s.BranchId == branchId.Value)
                        .ToDictionaryAsync(s => s.BatchId, s => s.Quantity);
                }

                var withStock = results.Select(r => new
                {
                    r.ProductId,
                    r.ProductName,
                    r.ProductNameAr,
                    r.ExpiryDate,
                    StockQty = r.BatchId != null && stocks.ContainsKey((Guid)r.BatchId) ? stocks[(Guid)r.BatchId] : 0m,
                    r.SalePrice,
                    r.Barcode,
                    r.BuyPrice,
                    r.MinQtyAlert,
                    r.CompanyName,
                    r.ActiveIngredient,
                    r.BatchId
                }).ToList();

                return Ok(withStock);
            }

            return Ok(results);
        }

        // ── Stock Adjust ─────────────────────────────────
        [HttpPost("{batchId}/stock/adjust")]
        public async Task<IActionResult> AdjustStock(Guid batchId, [FromBody] StockAdjustReq req)
        {
            var orgId = OrgId;
            var branchId = BranchId;
            if (!branchId.HasValue) return BadRequest("Branch required");

            var stock = await _db.BatchStocks.FirstOrDefaultAsync(s =>
                s.BatchId == batchId && s.BranchId == branchId.Value && s.OrgId == orgId);

            if (stock != null)
            {
                stock.Quantity = req.Quantity;
                if (req.WarehouseId.HasValue) stock.WarehouseId = req.WarehouseId;
            }
            else
            {
                _db.BatchStocks.Add(new BatchStock
                {
                    Id = Guid.NewGuid(), OrgId = orgId,
                    BatchId = batchId, BranchId = branchId.Value,
                    WarehouseId = req.WarehouseId,
                    Quantity = req.Quantity, IsActive = true
                });
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        // ── Generate unique barcode ──────────────────────
        [HttpGet("generate-barcode")]
        public async Task<IActionResult> GenerateBarcode([FromQuery] int length = 6)
        {
            var orgId = OrgId;

            if (length < 1) length = 1;
            if (length > 9) length = 9;

            long min = length == 1 ? 1 : (long)Math.Pow(10, length - 1);
            long max = (long)Math.Pow(10, length) - 1;

            if (length <= 2)
            {
                var used = await _db.ProductBatches.IgnoreQueryFilters()
                    .Where(b => b.OrgId == orgId && b.Barcode.Length == length)
                    .Select(b => b.Barcode)
                    .ToListAsync();

                var usedSet = new HashSet<string>(used);
                var available = new List<long>();
                for (long i = min; i <= max; i++)
                {
                    if (!usedSet.Contains(i.ToString())) available.Add(i);
                }

                if (available.Count == 0) return BadRequest("لا يوجد رقم باركود متاح بعدد الخانات المحدده");

                long val = available[Random.Shared.Next(available.Count)];
                return Ok(new { barcode = val.ToString() });
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    long val = Random.Shared.NextInt64(min, max + 1);
                    string strVal = val.ToString();

                    bool exists = await _db.ProductBatches.IgnoreQueryFilters()
                        .AnyAsync(b => b.OrgId == orgId && b.Barcode == strVal);

                    if (!exists) return Ok(new { barcode = strVal });
                }

                var used = await _db.ProductBatches.IgnoreQueryFilters()
                    .Where(b => b.OrgId == orgId && b.Barcode.Length == length)
                    .Select(b => b.Barcode)
                    .ToListAsync();

                var usedSet = new HashSet<string>(used);

                for (long i = min; i <= max; i++)
                {
                    if (!usedSet.Contains(i.ToString())) return Ok(new { barcode = i.ToString() });
                }

                return BadRequest("لا يوجد رقم باركود متاح بعدد الخانات المحدده");
            }
        }
    }
}
