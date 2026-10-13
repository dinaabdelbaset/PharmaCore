using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/treasury")]
    [Authorize]
    public class TreasuryController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TreasuryController(AppDbContext db)
        {
            _db = db;
        }

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException("Organization ID claim not found."));

        // GET: api/treasury/balances
        [HttpGet("balances")]
        public async Task<IActionResult> GetBalances([FromQuery] Guid? branchId)
        {
            var orgId = OrgId;
            var query = _db.CashRegisterEntries.Where(e => e.OrgId == orgId && e.IsActive);

            if (branchId.HasValue)
            {
                query = query.Where(e => e.BranchId == branchId.Value);
            }

            var totalCash = await query.Where(e => !e.IsBank).SumAsync(e => e.Amount);
            var totalBank = await query.Where(e => e.IsBank).SumAsync(e => e.Amount);

            // Fetch net cash flow data for the last 30 days grouped by date
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30).Date;
            var recentEntries = await query
                .Where(e => e.Date >= thirtyDaysAgo)
                .Select(e => new { e.Date, e.Amount, e.IsBank })
                .ToListAsync();

            var flowChart = recentEntries
                .GroupBy(e => e.Date.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    date = g.Key.ToString("yyyy-MM-dd"),
                    netFlow = g.Sum(e => e.Amount),
                    cashFlow = g.Where(e => !e.IsBank).Sum(e => e.Amount),
                    bankFlow = g.Where(e => e.IsBank).Sum(e => e.Amount)
                })
                .ToList();

            return Ok(new
            {
                totalCash,
                totalBank,
                netFlowChart = flowChart
            });
        }

        // GET: api/treasury/entries
        [HttpGet("entries")]
        public async Task<IActionResult> GetEntries(
            [FromQuery] string? searchTerm,
            [FromQuery] Guid? branchId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? type, // e.g. "عمليات مبيعات", "Deposit", "Withdrawal"
            [FromQuery] string? paymentMethod, // "cash" or "bank"
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            var orgId = OrgId;
            var query = _db.CashRegisterEntries
                .Include(e => e.Branch)
                .Where(e => e.OrgId == orgId && e.IsActive);

            if (branchId.HasValue)
            {
                query = query.Where(e => e.BranchId == branchId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(e => e.Date >= fromDate.Value.ToUniversalTime());
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.Date <= toDate.Value.ToUniversalTime());
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearch = searchTerm.ToLower();
                query = query.Where(e => (e.Description != null && e.Description.ToLower().Contains(lowerSearch)) 
                                      || e.EmployeeName.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrWhiteSpace(type) && type != "all")
            {
                query = query.Where(e => e.EntryType == type);
            }

            if (!string.IsNullOrWhiteSpace(paymentMethod) && paymentMethod != "all")
            {
                var isBank = paymentMethod.ToLower() == "bank";
                query = query.Where(e => e.IsBank == isBank);
            }

            var total = await query.CountAsync();

            var list = await query
                .OrderByDescending(e => e.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new
                {
                    e.Id,
                    e.BranchId,
                    branchName = e.Branch != null ? e.Branch.Name : "Main Office",
                    e.Amount,
                    e.EntryType,
                    e.Description,
                    e.EmployeeName,
                    e.IsBank,
                    e.Date
                })
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                items = list
            });
        }

        public record CreateEntryReq(
            decimal Amount,
            bool IsBank,
            string EntryType, // "Deposit" or "Withdrawal"
            string? Description,
            Guid? BranchId,
            DateTime? Date
        );

        // POST: api/treasury/entries
        [HttpPost("entries")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryReq req)
        {
            var orgId = OrgId;
            var employeeName = User.FindFirst(ClaimTypes.Name)?.Value ?? User.Identity?.Name ?? "System";

            if (req.Amount <= 0)
            {
                return BadRequest("المبلغ يجب أن يكون أكبر من الصفر");
            }

            var finalAmount = req.Amount;
            if (req.EntryType.Equals("Withdrawal", StringComparison.OrdinalIgnoreCase) || req.EntryType.Equals("سحب", StringComparison.OrdinalIgnoreCase))
            {
                finalAmount = -req.Amount;
            }

            var entry = new CashRegisterEntry
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = req.BranchId,
                Amount = finalAmount,
                EntryType = req.EntryType,
                Description = req.Description,
                EmployeeName = employeeName,
                IsBank = req.IsBank,
                Date = req.Date?.ToUniversalTime() ?? DateTime.UtcNow,
                IsActive = true
            };

            _db.CashRegisterEntries.Add(entry);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                entry.Id,
                entry.Amount,
                entry.IsBank,
                entry.EntryType,
                entry.Description,
                entry.EmployeeName,
                entry.Date
            });
        }

        public record CreateAdvanceReq(
            Guid EmployeeId,
            string EmployeeName,
            decimal Amount,
            bool IsBank,
            DateTime Date,
            string Description,
            Guid OrgId,
            Guid? BranchId
        );

        [HttpPost("advance")]
        public async Task<IActionResult> CreateAdvance([FromBody] CreateAdvanceReq req)
        {
            var entry = new CashRegisterEntry
            {
                Id = Guid.NewGuid(),
                OrgId = req.OrgId,
                BranchId = req.BranchId,
                Amount = -req.Amount, // Advances are withdrawals
                EntryType = "Advance",
                Description = req.Description,
                EmployeeName = req.EmployeeName,
                IsBank = req.IsBank,
                Date = req.Date.ToUniversalTime(),
                IsActive = true
            };

            _db.CashRegisterEntries.Add(entry);
            await _db.SaveChangesAsync();
            return Ok(entry);
        }

        public record CreateSalaryReq(
            Guid EmployeeId,
            string EmployeeName,
            decimal Amount,
            string EntryType,
            bool IsBank,
            DateTime Date,
            string Description,
            Guid OrgId,
            Guid? BranchId,
            decimal GrossSalary,
            decimal Deductions,
            decimal Bonus,
            int Month,
            int Year
        );

        [HttpPost("salary")]
        public async Task<IActionResult> CreateSalary([FromBody] CreateSalaryReq req)
        {
            var entry = new CashRegisterEntry
            {
                Id = Guid.NewGuid(),
                OrgId = req.OrgId,
                BranchId = req.BranchId,
                Amount = -req.Amount, // Salary payouts are withdrawals
                EntryType = "Salary",
                Description = req.Description,
                EmployeeName = req.EmployeeName,
                IsBank = req.IsBank,
                Date = req.Date.ToUniversalTime(),
                IsActive = true
            };

            _db.CashRegisterEntries.Add(entry);
            await _db.SaveChangesAsync();
            return Ok(entry);
        }

        // GET: api/treasury/expense
        [HttpGet("expense")]
        public async Task<IActionResult> GetExpenses()
        {
            var orgId = OrgId;
            var list = await _db.CashRegisterEntries
                .Include(e => e.Branch)
                .Where(e => e.OrgId == orgId && e.IsActive && e.EntryType == "Expense")
                .OrderByDescending(e => e.Date)
                .Select(e => new
                {
                    e.Id,
                    e.BranchId,
                    branchName = e.Branch != null ? e.Branch.Name : "Main Office",
                    amount = Math.Abs(e.Amount),
                    e.EntryType,
                    description = e.Description,
                    e.EmployeeName,
                    e.IsBank,
                    e.Date
                })
                .ToListAsync();

            return Ok(list);
        }

        public record CreateExpenseReq(
            decimal Amount,
            bool IsBank,
            string Category,
            string? Description,
            Guid? BranchId,
            DateTime Date
        );

        // POST: api/treasury/expense
        [HttpPost("expense")]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseReq req)
        {
            var orgId = OrgId;
            var employeeName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? User.Identity?.Name ?? "System";

            if (req.Amount <= 0)
            {
                return BadRequest("المبلغ يجب أن يكون أكبر من الصفر");
            }

            var entry = new CashRegisterEntry
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = req.BranchId,
                Amount = -req.Amount,
                EntryType = "Expense",
                Description = $"[{req.Category}] {req.Description ?? ""}",
                EmployeeName = employeeName,
                IsBank = req.IsBank,
                Date = req.Date.ToUniversalTime(),
                IsActive = true
            };

            _db.CashRegisterEntries.Add(entry);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                entry.Id,
                entry.BranchId,
                branchName = "",
                amount = req.Amount,
                entry.EntryType,
                description = entry.Description,
                entry.EmployeeName,
                entry.IsBank,
                entry.Date
            });
        }
    }
}
