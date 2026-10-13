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
    [ApiController, Route("api/purchases"), Authorize]
    public class PurchasesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PurchasesController(AppDbContext db) => _db = db;

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // ── DTOs ──────────────────────────────────────────
        public record PurchaseListDto(Guid Id, string InvoiceNumber, DateTime Date, string SupplierName, decimal Total, bool IsPaid, string? BranchName);
        
        public record PurchaseLineDetailDto(Guid Id, Guid ProductId, string ProductName, string Barcode, decimal Quantity, decimal UnitPrice, decimal LineTotal, DateTime? ExpiryDate);
        public record PurchaseDetailDto(Guid Id, string InvoiceNumber, DateTime Date, Guid SupplierId, string SupplierName, decimal Total, bool IsPaid, string? Notes, string? BranchName, List<PurchaseLineDetailDto> Lines);

        public record CreatePurchaseReq(
            Guid BranchId,
            Guid SupplierId,
            string InvoiceNumber,
            DateTime Date,
            string? Notes,
            bool IsPaid,
            List<CreatePurchaseLineReq> Lines
        );

        public record CreatePurchaseLineReq(
            Guid ProductId,
            string Barcode,
            decimal Quantity,
            decimal UnitPrice,
            DateTime? ExpiryDate
        );

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var orgId = OrgId;
            var q = _db.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Branch)
                .Where(p => p.OrgId == orgId && p.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(p => p.InvoiceNumber.ToLower().Contains(s) || 
                                 p.Supplier!.Name.ToLower().Contains(s));
            }

            var items = await q.OrderByDescending(p => p.Date)
                .Select(p => new PurchaseListDto(
                    p.Id,
                    p.InvoiceNumber,
                    p.Date,
                    p.Supplier != null ? p.Supplier.Name : "Unknown",
                    p.Total,
                    p.IsPaid,
                    p.Branch != null ? p.Branch.Name : "Headquarters"
                ))
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var p = await _db.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Branch)
                .Include(p => p.Lines!)
                    .ThenInclude(l => l.Product)
                .FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId && x.IsActive);

            if (p == null) return NotFound();

            var lineDtos = (p.Lines ?? new List<PurchaseLine>()).Select(l => new PurchaseLineDetailDto(
                l.Id,
                l.ProductId,
                l.Product != null ? l.Product.NameEn : "Unknown",
                l.Barcode,
                l.Quantity,
                l.UnitPrice,
                l.LineTotal,
                l.ExpiryDate
            )).ToList();

            var dto = new PurchaseDetailDto(
                p.Id,
                p.InvoiceNumber,
                p.Date,
                p.SupplierId,
                p.Supplier != null ? p.Supplier.Name : "Unknown",
                p.Total,
                p.IsPaid,
                p.Notes,
                p.Branch != null ? p.Branch.Name : "Headquarters",
                lineDtos
            );

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseReq req)
        {
            var orgId = OrgId;

            // 1. Fetch Supplier
            var supplier = await _db.Suppliers.FirstOrDefaultAsync(s => s.Id == req.SupplierId && s.OrgId == orgId);
            if (supplier == null) return BadRequest("Supplier not found.");

            // 2. Fetch default unit for fallback batch creation
            var defaultUnit = await _db.ProductUnits.FirstOrDefaultAsync(u => u.OrgId == orgId && u.IsActive);
            if (defaultUnit == null)
            {
                // Create a default unit if none exists
                defaultUnit = new ProductUnit
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    Name = "Box",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.ProductUnits.Add(defaultUnit);
                await _db.SaveChangesAsync();
            }

            var purchaseId = Guid.NewGuid();
            decimal invoiceTotal = 0;
            var purchaseLines = new List<PurchaseLine>();

            foreach (var line in req.Lines)
            {
                var lineTotal = line.Quantity * line.UnitPrice;
                invoiceTotal += lineTotal;

                // A. Find or Create ProductBatch for Product & Barcode
                var batch = await _db.ProductBatches.FirstOrDefaultAsync(b => b.ProductId == line.ProductId && b.Barcode == line.Barcode && b.OrgId == orgId);
                if (batch == null)
                {
                    batch = new ProductBatch
                    {
                        Id = Guid.NewGuid(),
                        OrgId = orgId,
                        ProductId = line.ProductId,
                        Barcode = line.Barcode,
                        PharmacyBuyPrice = line.UnitPrice,
                        PublicSalePrice = line.UnitPrice * 1.30m, // 30% default markup
                        ExpiryDate = line.ExpiryDate ?? DateTime.UtcNow.AddYears(2),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        MinQtyAlert = 10,
                        MainUnitId = defaultUnit.Id,
                        MainUnitQty = 1,
                        SaleUnitId = defaultUnit.Id,
                        PurchaseUnitId = defaultUnit.Id,
                        PriceAfterTax = line.UnitPrice * 1.30m
                    };
                    _db.ProductBatches.Add(batch);
                }
                else
                {
                    // Update batch buy price with latest purchase price
                    batch.PharmacyBuyPrice = line.UnitPrice;
                    batch.UpdatedAt = DateTime.UtcNow;
                }

                // B. Increment Branch Stock for this Batch
                var stock = await _db.BatchStocks.FirstOrDefaultAsync(s => s.BranchId == req.BranchId && s.BatchId == batch.Id && s.OrgId == orgId);
                if (stock == null)
                {
                    stock = new BatchStock
                    {
                        Id = Guid.NewGuid(),
                        OrgId = orgId,
                        BranchId = req.BranchId,
                        BatchId = batch.Id,
                        Quantity = line.Quantity,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    _db.BatchStocks.Add(stock);
                }
                else
                {
                    stock.Quantity += line.Quantity;
                    stock.UpdatedAt = DateTime.UtcNow;
                }

                // C. Record Purchase Line
                purchaseLines.Add(new PurchaseLine
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    PurchaseId = purchaseId,
                    ProductId = line.ProductId,
                    BatchId = batch.Id,
                    Barcode = line.Barcode,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    LineTotal = lineTotal,
                    ExpiryDate = line.ExpiryDate,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // 3. Update Supplier Balance if not paid immediately
            if (!req.IsPaid)
            {
                supplier.Balance += invoiceTotal;
                supplier.UpdatedAt = DateTime.UtcNow;
            }

            // 4. Create Purchase Invoice
            var purchase = new Purchase
            {
                Id = purchaseId,
                OrgId = orgId,
                BranchId = req.BranchId,
                SupplierId = req.SupplierId,
                InvoiceNumber = req.InvoiceNumber,
                Date = req.Date,
                Total = invoiceTotal,
                Notes = req.Notes,
                IsPaid = req.IsPaid,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                Lines = purchaseLines
            };

            _db.Purchases.Add(purchase);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = purchase.Id }, new PurchaseListDto(
                purchase.Id,
                purchase.InvoiceNumber,
                purchase.Date,
                supplier.Name,
                purchase.Total,
                purchase.IsPaid,
                purchase.BranchId.HasValue ? "Active Branch" : "Headquarters"
            ));
        }
    }
}
