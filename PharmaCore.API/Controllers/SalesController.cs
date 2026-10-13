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
    [ApiController, Route("api/sales"), Authorize]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SalesController(AppDbContext db) => _db = db;
        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());
        private Guid? BranchId
        {
            get
            {
                var v = User.FindFirst("branch_id")?.Value;
                return string.IsNullOrEmpty(v) ? null : Guid.Parse(v);
            }
        }

        // ══════════════════════════════════════════════════════════
        //  DTOs
        // ══════════════════════════════════════════════════════════
        public record CreateSaleReq(
            string SaleType,          // نقدي/مسجل/آجل/رصيد/موظف/توصيل/تأمين
            string MovementType,      // بيع
            // ── Customer info ──
            Guid? CustomerId,
            string? CustomerName,
            string? CustomerCode,
            string? CustomerPhone,
            string? DeliveryAddress,
            // ── Seller info ──
            bool UseAlternateSeller,
            Guid? SellerEmployeeId,
            string? SellerName,
            string? SellerCode,
            // ── Financials ──
            decimal Subtotal,
            decimal DiscountPercent,
            decimal DiscountAmount,
            decimal TaxTotal,
            decimal Total,
            decimal PaidCash,
            decimal PaidVisa,
            decimal PaidFromBalance,
            decimal Remaining,
            // ── Insurance ──
            string? InsuranceCompanyName,
            string? InsuranceCode,
            string? ApprovalCode,
            decimal InsurancePercent,
            string? InsuranceMobile,
            // ── Employee sale ──
            Guid? EmployeeCustomerId,
            string? EmployeePayType,
            // ── Other ──
            string? Notes,
            DateTime? ReminderDate,
            string Status,            // Completed / Temp
            List<SaleLineReq> Lines,
            string? InvoiceNumber = null,
            DateTime? Date = null);

        public record SaleLineReq(
            int LineNumber,
            Guid ProductId,
            Guid BatchId,
            string Barcode,
            string ProductName,
            Guid UnitId,
            string UnitName,
            decimal Quantity,
            decimal StockQtyAtSale,
            decimal PriceBeforeTax,
            decimal TaxValue,
            decimal UnitPrice,
            decimal Discount,
            decimal LineTotal,
            decimal LineTotalAfterDiscount,
            decimal BuyPrice,
            DateTime? ExpiryDate);

        public record SaleResultDto(
            Guid Id, string InvoiceNumber, DateTime Date, string SaleType,
            string? CustomerName, decimal Subtotal, decimal Total,
            decimal PaidCash, decimal PaidVisa, decimal Remaining, string Status,
            List<SaleLineDto> Lines);

        public record SaleLineDto(
            Guid Id, int LineNumber, string Barcode, string ProductName,
            string UnitName, decimal Quantity, decimal UnitPrice,
            decimal Discount, decimal LineTotal, decimal LineTotalAfterDiscount,
            decimal TaxValue, decimal PriceBeforeTax, decimal BuyPrice,
            DateTime? ExpiryDate, decimal StockQtyAtSale);

        // ══════════════════════════════════════════════════════════
        //  POST — Create Sale (the big transactional endpoint)
        // ══════════════════════════════════════════════════════════
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleReq req)
        {
            var orgId = OrgId;
            var branchId = BranchId;

            if (req.Lines == null || req.Lines.Count == 0)
                return BadRequest("الفاتورة لا تحتوي على أصناف");

            string invoiceNumber;
            if (!string.IsNullOrWhiteSpace(req.InvoiceNumber))
            {
                var existingSale = await _db.Sales
                    .FirstOrDefaultAsync(s => s.InvoiceNumber == req.InvoiceNumber && s.OrgId == orgId);
                if (existingSale != null)
                {
                    return Ok(new { existingSale.Id, existingSale.InvoiceNumber, existingSale.Date, existingSale.Status });
                }
                invoiceNumber = req.InvoiceNumber;
            }
            else
            {
                // ── Generate invoice number ──
                var maxNum = await _db.Sales.IgnoreQueryFilters()
                    .Where(s => s.OrgId == orgId)
                    .OrderByDescending(s => s.InvoiceNumber)
                    .Select(s => s.InvoiceNumber)
                    .FirstOrDefaultAsync();

                long nextNum = 1;
                if (maxNum != null && long.TryParse(maxNum, out var n)) nextNum = n + 1;
                invoiceNumber = nextNum.ToString();
            }

            var saleDate = req.Date?.ToUniversalTime() ?? DateTime.UtcNow;

            // ── Create Sale header ──
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = branchId,
                InvoiceNumber = invoiceNumber,
                Date = saleDate,
                SaleType = req.SaleType,
                MovementType = req.MovementType,
                CustomerId = req.CustomerId,
                CustomerName = req.CustomerName,
                CustomerCode = req.CustomerCode,
                CustomerPhone = req.CustomerPhone,
                DeliveryAddress = req.DeliveryAddress,
                UseAlternateSeller = req.UseAlternateSeller,
                SellerEmployeeId = req.SellerEmployeeId,
                SellerName = req.SellerName,
                SellerCode = req.SellerCode,
                Subtotal = req.Subtotal,
                DiscountPercent = req.DiscountPercent,
                DiscountAmount = req.DiscountAmount,
                TaxTotal = req.TaxTotal,
                Total = req.Total,
                PaidCash = req.PaidCash,
                PaidVisa = req.PaidVisa,
                PaidFromBalance = req.PaidFromBalance,
                Remaining = req.Remaining,
                Notes = req.Notes,
                InsuranceCompanyName = req.InsuranceCompanyName,
                InsuranceCode = req.InsuranceCode,
                ApprovalCode = req.ApprovalCode,
                InsurancePercent = req.InsurancePercent,
                InsuranceMobile = req.InsuranceMobile,
                EmployeeCustomerId = req.EmployeeCustomerId,
                EmployeePayType = req.EmployeePayType,
                ReminderDate = req.ReminderDate,
                Status = req.Status ?? "Completed",
                IsActive = true
            };
            _db.Sales.Add(sale);

            // ── Create Sale Lines + deduct stock ──
            foreach (var line in req.Lines)
            {
                _db.SaleLines.Add(new SaleLine
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    SaleId = sale.Id,
                    LineNumber = line.LineNumber,
                    ProductId = line.ProductId,
                    BatchId = line.BatchId,
                    Barcode = line.Barcode,
                    ProductName = line.ProductName,
                    UnitId = line.UnitId,
                    UnitName = line.UnitName,
                    Quantity = line.Quantity,
                    StockQtyAtSale = line.StockQtyAtSale,
                    PriceBeforeTax = line.PriceBeforeTax,
                    TaxValue = line.TaxValue,
                    UnitPrice = line.UnitPrice,
                    Discount = line.Discount,
                    LineTotal = line.LineTotal,
                    LineTotalAfterDiscount = line.LineTotalAfterDiscount,
                    BuyPrice = line.BuyPrice,
                    ExpiryDate = line.ExpiryDate,
                    IsActive = true
                });

                // ── Deduct stock (only for Completed sales) ──
                if (sale.Status == "Completed" && branchId.HasValue)
                {
                    // Calculate quantity in main units
                    var batchUnit = await _db.BatchUnits
                        .FirstOrDefaultAsync(u => u.BatchId == line.BatchId && u.UnitId == line.UnitId);

                    decimal qtyInMain = line.Quantity;
                    if (batchUnit != null && batchUnit.QtyPerMainUnit > 0)
                        qtyInMain = line.Quantity / batchUnit.QtyPerMainUnit;

                    var stock = await _db.BatchStocks
                        .FirstOrDefaultAsync(s => s.BatchId == line.BatchId && s.BranchId == branchId.Value);

                    if (stock != null)
                    {
                        stock.Quantity -= qtyInMain;
                    }

                    // Update product sold qty counter
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == line.ProductId);
                    if (product != null)
                    {
                        product.TotalSoldQty += qtyInMain;
                    }
                }
            }

            // ── Update customer data (for registered/deferred/points) ──
            if (sale.Status == "Completed" && req.CustomerId.HasValue)
            {
                var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == req.CustomerId.Value);
                if (customer != null)
                {
                    customer.VisitCount++;
                    customer.LastVisitDate = sale.Date;

                    if (req.SaleType == "عميل مسجل" || req.SaleType == "عميل آجل")
                    {
                        customer.TotalPoints += req.Total;
                        customer.PointsBalance += req.Total;
                    }
                    else if (req.SaleType == "سحب من رصيد العميل")
                    {
                        customer.MoneyBalance -= req.PaidFromBalance;
                        var cashPoints = req.PaidCash + req.PaidVisa;
                        if (cashPoints > 0)
                        {
                            customer.TotalPoints += cashPoints;
                            customer.PointsBalance += cashPoints;
                        }
                    }
                }
            }

            // ── Cash register / bank entry ──
            if (sale.Status == "Completed")
            {
                var isDeferred = req.SaleType == "عميل آجل" || req.EmployeePayType == "آجل";
                var isSalaryDeduct = req.EmployeePayType == "من الراتب";

                if (req.PaidVisa > 0)
                {
                    // Visa payment → bank
                    _db.CashRegisterEntries.Add(new CashRegisterEntry
                    {
                        Id = Guid.NewGuid(), OrgId = orgId, BranchId = branchId,
                        SaleId = sale.Id, Amount = req.PaidVisa,
                        EntryType = "عمليات مبيعات", IsBank = true,
                        EmployeeName = sale.SellerName ?? "", Date = sale.Date, IsActive = true
                    });
                }

                if (isDeferred || isSalaryDeduct)
                {
                    // Deferred → record 0 in cash register
                    _db.CashRegisterEntries.Add(new CashRegisterEntry
                    {
                        Id = Guid.NewGuid(), OrgId = orgId, BranchId = branchId,
                        SaleId = sale.Id, Amount = 0,
                        EntryType = isDeferred ? "مبيعات آجل" : "سحب من راتب موظف",
                        Description = req.Total.ToString("F2"),
                        IsBank = false,
                        EmployeeName = sale.SellerName ?? "", Date = sale.Date, IsActive = true
                    });

                    // If anything was paid cash even in deferred
                    if (req.PaidCash > 0)
                    {
                        _db.CashRegisterEntries.Add(new CashRegisterEntry
                        {
                            Id = Guid.NewGuid(), OrgId = orgId, BranchId = branchId,
                            SaleId = sale.Id, Amount = req.PaidCash,
                            EntryType = "دفع قيمة من آجل",
                            IsBank = false,
                            EmployeeName = sale.SellerName ?? "", Date = sale.Date, IsActive = true
                        });
                    }
                }
                else if (!isDeferred && !isSalaryDeduct && req.PaidVisa <= 0)
                {
                    // Normal cash sale → cash register
                    _db.CashRegisterEntries.Add(new CashRegisterEntry
                    {
                        Id = Guid.NewGuid(), OrgId = orgId, BranchId = branchId,
                        SaleId = sale.Id, Amount = req.Total,
                        EntryType = "عمليات مبيعات",
                        IsBank = false,
                        EmployeeName = sale.SellerName ?? "", Date = sale.Date, IsActive = true
                    });
                }
            }

            // ── Save everything in one transaction ──
            await _db.SaveChangesAsync();

            return Ok(new { sale.Id, sale.InvoiceNumber, sale.Date, sale.Status });
        }

        // ══════════════════════════════════════════════════════════
        //  GET — Retrieve sale by ID (for printing / viewing)
        // ══════════════════════════════════════════════════════════
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orgId = OrgId;
            var sale = await _db.Sales
                .Include(s => s.Lines!.OrderBy(l => l.LineNumber))
                .FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId);

            if (sale == null) return NotFound();

            return Ok(new SaleResultDto(
                sale.Id, sale.InvoiceNumber, sale.Date, sale.SaleType,
                sale.CustomerName, sale.Subtotal, sale.Total,
                sale.PaidCash, sale.PaidVisa, sale.Remaining, sale.Status,
                (sale.Lines ?? new List<SaleLine>()).Select(l => new SaleLineDto(
                    l.Id, l.LineNumber, l.Barcode, l.ProductName,
                    l.UnitName, l.Quantity, l.UnitPrice,
                    l.Discount, l.LineTotal, l.LineTotalAfterDiscount,
                    l.TaxValue, l.PriceBeforeTax, l.BuyPrice,
                    l.ExpiryDate, l.StockQtyAtSale)).ToList()));
        }

        // ══════════════════════════════════════════════════════════
        //  GET — List recent sales (for history / reprint)
        // ══════════════════════════════════════════════════════════
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? saleType,
            [FromQuery] string? movementType,
            [FromQuery] string? sellerCode,
            [FromQuery] string? customerCode,
            [FromQuery] string? phone,
            [FromQuery] string? invoiceNumber,
            [FromQuery] string? barcode,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            var orgId = OrgId;
            var query = _db.Sales.AsQueryable().Where(s => s.OrgId == orgId);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(s => s.Status == status);
            if (from.HasValue)
                query = query.Where(s => s.Date >= from.Value);
            if (to.HasValue)
                query = query.Where(s => s.Date <= to.Value);

            if (!string.IsNullOrWhiteSpace(saleType) && saleType != "الكل")
                query = query.Where(s => s.SaleType == saleType);
            if (!string.IsNullOrWhiteSpace(movementType) && movementType != "الكل")
                query = query.Where(s => s.MovementType == movementType);
            if (!string.IsNullOrWhiteSpace(sellerCode))
                query = query.Where(s => s.SellerCode == sellerCode);
            if (!string.IsNullOrWhiteSpace(barcode))
                query = query.Where(s => s.Lines.Any(l => l.Barcode == barcode));
            if (!string.IsNullOrWhiteSpace(customerCode))
                query = query.Where(s => s.CustomerCode == customerCode);
            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(s => s.CustomerPhone == phone);
            if (!string.IsNullOrWhiteSpace(invoiceNumber))
                query = query.Where(s => s.InvoiceNumber == invoiceNumber);

            var total = await query.CountAsync();

            var list = await query
                .OrderByDescending(s => s.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new
                {
                    s.Id,
                    s.InvoiceNumber,
                    s.Date,
                    s.SaleType,
                    s.MovementType,
                    s.CustomerName,
                    s.CustomerCode,
                    s.CustomerPhone,
                    s.SellerName,
                    s.SellerCode,
                    s.Subtotal,
                    s.Total,
                    s.PaidCash,
                    s.PaidVisa,
                    s.PaidFromBalance,
                    s.Remaining,
                    s.Notes,
                    s.DeliveryAddress,
                    s.ReminderDate,
                    s.Status,
                    s.InsuranceCompanyName,
                    s.InsuranceCode,
                    s.ApprovalCode,
                    s.InsurancePercent,
                    s.InsuranceMobile,
                    LineCount = s.Lines != null ? s.Lines.Count : 0
                })
                .ToListAsync();

            return Ok(new { total, page, pageSize, items = list });
        }

        // ══════════════════════════════════════════════════════════
        //  DELETE — Void a sale (soft delete, reverse stock)
        // ══════════════════════════════════════════════════════════
        [HttpDelete("{id}")]
        public async Task<IActionResult> VoidSale(Guid id)
        {
            var orgId = OrgId;
            var branchId = BranchId;

            var sale = await _db.Sales
                .Include(s => s.Lines)
                .FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId);

            if (sale == null) return NotFound();
            if (sale.Status == "Voided") return BadRequest("الفاتورة ملغاة بالفعل");

            // Reverse stock deductions
            if (sale.Status == "Completed" && branchId.HasValue && sale.Lines != null)
            {
                foreach (var line in sale.Lines)
                {
                    var batchUnit = await _db.BatchUnits
                        .FirstOrDefaultAsync(u => u.BatchId == line.BatchId && u.UnitId == line.UnitId);

                    decimal qtyInMain = line.Quantity;
                    if (batchUnit != null && batchUnit.QtyPerMainUnit > 0)
                        qtyInMain = line.Quantity / batchUnit.QtyPerMainUnit;

                    var stock = await _db.BatchStocks
                        .FirstOrDefaultAsync(s => s.BatchId == line.BatchId && s.BranchId == branchId.Value);
                    if (stock != null) stock.Quantity += qtyInMain;

                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == line.ProductId);
                    if (product != null) product.TotalSoldQty -= qtyInMain;
                }
            }

            sale.Status = "Voided";
            await _db.SaveChangesAsync();
            return Ok();
        }

        // ══════════════════════════════════════════════════════════
        //  DELETE — Remove a Temp Sale (physical delete, used when resuming a temp sale)
        // ══════════════════════════════════════════════════════════
        [HttpDelete("temp/{id}")]
        public async Task<IActionResult> DeleteTempSale(Guid id)
        {
            var orgId = OrgId;

            var sale = await _db.Sales
                .Include(s => s.Lines)
                .FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId);

            if (sale == null) return NotFound();
            
            // Only allow deleting Temp sales to prevent accidental data loss
            if (sale.Status != "Temp") return BadRequest("لا يمكن مسح إلا الفواتير المعلقة فقط");

            _db.Sales.Remove(sale);
            await _db.SaveChangesAsync();
            
            return Ok();
        }
    }
}
