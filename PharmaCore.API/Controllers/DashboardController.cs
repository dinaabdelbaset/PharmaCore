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
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException("Organization ID claim not found."));

        // ── DTOs ──────────────────────────────────────
        public record KpiMetricDto(decimal Value, double ChangePercent);
        public record MonthlyRevenueDto(string Month, decimal Revenue, int Orders);
        public record BranchPerformanceDto(string Id, string Name, decimal Revenue, int Orders);
        
        public record ExpiryRiskDto(
            string Barcode,
            string ProductName,
            string BranchName,
            decimal StockQuantity,
            DateTime ExpiryDate,
            int DaysRemaining
        );

        public record StockRiskDto(
            string Barcode,
            string ProductName,
            string BranchName,
            decimal StockQuantity,
            decimal MinQtyAlert
        );

        public record TopProductDto(string Name, decimal Quantity, decimal Revenue);
        public record BranchBalanceDto(string BranchId, string BranchName, decimal CashBalance, decimal BankBalance, decimal TotalBalance);

        public record PharmacyHealthDto(
            string InventoryStatus,
            int LowStockCount,
            int ExpiredCount,
            double SalesTrendPercent,
            int StagnantCount,
            int OverdueDebtsCount
        );

        public record DashboardStatsDto(
            KpiMetricDto TotalRevenue,
            KpiMetricDto SalesCount,
            int ActiveCashiers,
            List<MonthlyRevenueDto> RevenueChart,
            List<BranchPerformanceDto> BranchPerformance,
            List<ExpiryRiskDto> ExpiryRisks,
            List<StockRiskDto> StockRisks,
            List<TopProductDto> TopProducts,
            decimal TotalExpenses,
            List<BranchBalanceDto> BranchBalances,
            PharmacyHealthDto PharmacyHealth
        );

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var orgId = OrgId;

            // 1. Calculate dates
            var utcNow = DateTime.UtcNow;
            var thisMonthStart = new DateTime(utcNow.Year, utcNow.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddTicks(-1);

            // 2. Fetch sales for this month and last month
            var thisMonthSales = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Date >= thisMonthStart)
                .Select(s => new { s.Total })
                .ToListAsync();

            var lastMonthSales = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Date >= lastMonthStart && s.Date <= lastMonthEnd)
                .Select(s => new { s.Total })
                .ToListAsync();

            decimal thisMonthRevenue = thisMonthSales.Sum(s => s.Total);
            decimal lastMonthRevenue = lastMonthSales.Sum(s => s.Total);

            int thisMonthOrders = thisMonthSales.Count;
            int lastMonthOrders = lastMonthSales.Count;

            double revenueChangePercent = lastMonthRevenue > 0
                ? (double)((thisMonthRevenue - lastMonthRevenue) / lastMonthRevenue) * 100
                : 0;

            double ordersChangePercent = lastMonthOrders > 0
                ? (double)(thisMonthOrders - lastMonthOrders) / lastMonthOrders * 100
                : 0;

            // 3. Count active cashiers (unique devices with active sessions in last 24h)
            var activeLimit = utcNow.AddDays(-1);
            var activeCashiersCount = await _db.BranchAppSessions
                .Where(s => s.BranchAppAccount != null && s.BranchAppAccount.OrgId == orgId && s.IsActive && s.LogoutAt == null && s.LoginAt >= activeLimit)
                .Select(s => s.DeviceId)
                .Distinct()
                .CountAsync();

            // 4. Monthly revenues for the last 6 months
            var sixMonthsAgo = thisMonthStart.AddMonths(-5);
            var salesHistory = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Date >= sixMonthsAgo)
                .Select(s => new { s.Total, s.Date })
                .ToListAsync();

            var chartData = Enumerable.Range(0, 6)
                .Select(i => sixMonthsAgo.AddMonths(i))
                .Select(m => {
                    var monthSales = salesHistory.Where(s => s.Date.Year == m.Year && s.Date.Month == m.Month);
                    return new MonthlyRevenueDto(
                        Month: m.ToString("MMM yyyy"),
                        Revenue: monthSales.Sum(s => s.Total),
                        Orders: monthSales.Count()
                    );
                })
                .ToList();

            // 5. Branch rankings
            var rawBranchPerf = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.BranchId != null)
                .GroupBy(s => new { s.BranchId, BranchName = s.Branch != null ? s.Branch.Name : "Unknown" })
                .Select(g => new
                {
                    BranchId = g.Key.BranchId,
                    BranchName = g.Key.BranchName,
                    Revenue = g.Sum(s => s.Total),
                    Orders = g.Count()
                })
                .OrderByDescending(b => b.Revenue)
                .Take(10)
                .ToListAsync();

            var branchPerformance = rawBranchPerf
                .Select(b => new BranchPerformanceDto(
                    b.BranchId.ToString(),
                    b.BranchName ?? "Unknown",
                    b.Revenue,
                    b.Orders
                ))
                .ToList();

            // 6. AI Expiry Risk Alerts: Batches expiring within 90 days
            var expiryDateLimit = utcNow.AddDays(90);
            var rawExpiryRisks = await _db.BatchStocks
                .Include(s => s.Batch)
                    .ThenInclude(b => b.Product)
                .Include(s => s.Branch)
                .Where(s => s.OrgId == orgId && s.IsActive && s.Batch != null && s.Batch.ExpiryDate.HasValue 
                       && s.Batch.ExpiryDate.Value <= expiryDateLimit && s.Batch.ExpiryDate.Value >= utcNow && s.Quantity > 0)
                .OrderBy(s => s.Batch.ExpiryDate)
                .Select(s => new
                {
                    Barcode = s.Batch.Barcode,
                    ProductName = s.Batch.Product != null ? s.Batch.Product.NameEn : "Unknown Product",
                    BranchName = s.Branch != null ? s.Branch.Name : "Main Warehouse",
                    Quantity = s.Quantity,
                    ExpiryDate = s.Batch.ExpiryDate.Value
                })
                .Take(15)
                .ToListAsync();

            var expiryRisks = rawExpiryRisks
                .Select(r => new ExpiryRiskDto(
                    r.Barcode,
                    r.ProductName,
                    r.BranchName,
                    r.Quantity,
                    r.ExpiryDate,
                    (int)(r.ExpiryDate - utcNow).TotalDays
                ))
                .ToList();

            // 7. AI Stock Risk Alerts: Batches with low stock (quantity <= MinQtyAlert or <= 10 if alert not set)
            var rawStockRisks = await _db.BatchStocks
                .Include(s => s.Batch)
                    .ThenInclude(b => b.Product)
                .Include(s => s.Branch)
                .Where(s => s.OrgId == orgId && s.IsActive && s.Batch != null && s.Quantity <= (s.Batch.MinQtyAlert > 0 ? s.Batch.MinQtyAlert : 10))
                .OrderBy(s => s.Quantity)
                .Select(s => new
                {
                    Barcode = s.Batch.Barcode,
                    ProductName = s.Batch.Product != null ? s.Batch.Product.NameEn : "Unknown Product",
                    BranchName = s.Branch != null ? s.Branch.Name : "Main Warehouse",
                    Quantity = s.Quantity,
                    MinQtyAlert = s.Batch.MinQtyAlert
                })
                .Take(15)
                .ToListAsync();

            var stockRisks = rawStockRisks
                .Select(r => new StockRiskDto(
                    r.Barcode,
                    r.ProductName,
                    r.BranchName,
                    r.Quantity,
                    r.MinQtyAlert > 0 ? r.MinQtyAlert : 10
                ))
                .ToList();


            // 8. Top Selling Products this month
            var rawTopProducts = await _db.SaleLines
                .Include(l => l.Sale)
                .Where(l => l.OrgId == orgId && l.IsActive && l.Sale.Date >= thisMonthStart)
                .GroupBy(l => l.ProductName)
                .Select(g => new
                {
                    Name = g.Key,
                    Quantity = g.Sum(l => l.Quantity),
                    Revenue = g.Sum(l => l.LineTotalAfterDiscount)
                })
                .OrderByDescending(p => p.Revenue)
                .Take(5)
                .ToListAsync();

            var topProducts = rawTopProducts
                .Select(p => new TopProductDto(p.Name, p.Quantity, p.Revenue))
                .ToList();

            // 9. Total Expenses this month
            var totalExpenses = await _db.CashRegisterEntries
                .Where(e => e.OrgId == orgId && e.IsActive && e.EntryType == "Expense" && e.Date >= thisMonthStart)
                .SumAsync(e => (decimal?)Math.Abs(e.Amount)) ?? 0;

            // 10. Branch Balances (Treasury)
            var rawBranchBalances = await _db.CashRegisterEntries
                .Include(e => e.Branch)
                .Where(e => e.OrgId == orgId && e.IsActive)
                .ToListAsync();

            var branchBalances = rawBranchBalances
                .GroupBy(e => new { BranchId = e.BranchId?.ToString() ?? "MainOffice", BranchName = e.Branch != null ? e.Branch.Name : "Main Office" })
                .Select(g => new BranchBalanceDto(
                    g.Key.BranchId,
                    g.Key.BranchName,
                    g.Where(e => !e.IsBank).Sum(e => e.Amount),
                    g.Where(e => e.IsBank).Sum(e => e.Amount),
                    g.Sum(e => e.Amount)
                ))
                .ToList();

            // 11. Calculate Pharmacy Health statistics
            int lowStockCount = stockRisks.Count;
            int expiredCount = await _db.BatchStocks
                .Include(s => s.Batch)
                .Where(s => s.OrgId == orgId && s.IsActive && s.Batch != null && s.Batch.ExpiryDate.HasValue && s.Batch.ExpiryDate.Value < utcNow && s.Quantity > 0)
                .CountAsync();

            var todayStart = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);
            var yesterdayStart = todayStart.AddDays(-1);
            var todaySalesTotal = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Date >= todayStart)
                .SumAsync(s => (decimal?)s.Total) ?? 0;
            var yesterdaySalesTotal = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Date >= yesterdayStart && s.Date < todayStart)
                .SumAsync(s => (decimal?)s.Total) ?? 0;
            double healthSalesTrendPercent = yesterdaySalesTotal > 0
                ? (double)((todaySalesTotal - yesterdaySalesTotal) / yesterdaySalesTotal) * 100
                : 0;

            var cutoffDate = utcNow.AddDays(-90);
            var soldProductNames = await _db.SaleLines
                .Include(l => l.Sale)
                .Where(l => l.OrgId == orgId && l.IsActive && l.Sale.Date >= cutoffDate)
                .Select(l => l.ProductName)
                .Distinct()
                .ToListAsync();

            var inStockProducts = await _db.BatchStocks
                .Include(s => s.Batch)
                    .ThenInclude(b => b.Product)
                .Where(s => s.OrgId == orgId && s.IsActive && s.Batch != null && s.Quantity > 0 && s.Batch.Product != null)
                .Select(s => s.Batch.Product.NameEn)
                .Distinct()
                .ToListAsync();
            int stagnantCount = inStockProducts.Count(name => !soldProductNames.Contains(name));

            int overdueDebtsCount = await _db.Sales
                .Where(s => s.OrgId == orgId && s.IsActive && s.Remaining > 0)
                .Select(s => s.CustomerId)
                .Distinct()
                .CountAsync();

            string inventoryStatus = lowStockCount == 0 ? "جيد" : "يحتاج انتباه";

            var pharmacyHealth = new PharmacyHealthDto(
                InventoryStatus: inventoryStatus,
                LowStockCount: lowStockCount,
                ExpiredCount: expiredCount,
                SalesTrendPercent: healthSalesTrendPercent,
                StagnantCount: stagnantCount,
                OverdueDebtsCount: overdueDebtsCount
            );

            return Ok(new DashboardStatsDto(
                TotalRevenue: new KpiMetricDto(thisMonthRevenue, revenueChangePercent),
                SalesCount: new KpiMetricDto(thisMonthOrders, ordersChangePercent),
                ActiveCashiers: activeCashiersCount,
                RevenueChart: chartData,
                BranchPerformance: branchPerformance,
                ExpiryRisks: expiryRisks,
                StockRisks: stockRisks,
                TopProducts: topProducts,
                TotalExpenses: totalExpenses,
                BranchBalances: branchBalances,
                PharmacyHealth: pharmacyHealth
            ));
        }
    }
}
