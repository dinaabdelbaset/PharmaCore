using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController, Route("api/employees/performance-stats"), Authorize]
    public class EmployeePerformanceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EmployeePerformanceController(AppDbContext db) => _db = db;

        private Guid OrgId
        {
            get
            {
                var claim = User.FindFirst("org_id");
                return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPerformanceStats()
        {
            var orgId = OrgId;
            if (orgId == Guid.Empty) return Unauthorized();

            // Query completed sales from database grouped by cashier details
            var stats = await _db.Sales
                .Where(s => s.OrgId == orgId && s.Status == "Completed")
                .GroupBy(s => new { s.SellerEmployeeId, s.SellerName, s.SellerCode })
                .Select(g => new
                {
                    employeeId = g.Key.SellerEmployeeId,
                    employeeName = g.Key.SellerName ?? "كاشير غير محدد",
                    employeeCode = g.Key.SellerCode ?? "N/A",
                    totalRevenue = g.Sum(s => s.Total),
                    salesCount = g.Count(),
                    averageTicketValue = g.Count() > 0 ? g.Sum(s => s.Total) / g.Count() : 0
                })
                .OrderByDescending(x => x.totalRevenue)
                .ToListAsync();

            return Ok(stats);
        }
    }
}
