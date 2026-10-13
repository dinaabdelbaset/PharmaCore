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
    [Route("api/insurance")]
    [Authorize]
    public class InsuranceController : ControllerBase
    {
        private readonly AppDbContext _db;

        public InsuranceController(AppDbContext db)
        {
            _db = db;
        }

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // ── DTOs ──────────────────────────────────────────
        public record InsuranceCompanyDto(
            string Id,
            string Name,
            string Code,
            string ContactPerson,
            string Phone,
            decimal DefaultCopayPercent,
            decimal MaxMonthlyCoverage,
            bool RequiresPreApproval,
            bool IsActive,
            int TotalClaimsCount,
            decimal TotalClaimsAmount
        );

        public record CreateInsuranceCompanyReq(
            string Name,
            string Code,
            string ContactPerson,
            string Phone,
            decimal DefaultCopayPercent,
            decimal MaxMonthlyCoverage,
            bool RequiresPreApproval
        );

        public record InsuranceClaimDto(
            Guid Id,
            string InvoiceNumber,
            DateTime Date,
            string CustomerName,
            string InsuranceCompanyName,
            string InsuranceCardNo,
            string? ApprovalNo,
            decimal TotalInvoiceAmount,
            decimal InsuranceShareAmount,
            decimal PatientCopayAmount,
            decimal InsurancePercent,
            string Status, // Approved, Pending, Settled, Rejected
            string? RejectionReason
        );

        public record UpdateClaimStatusReq(string Status, string? RejectionReason);

        // In-memory persistent demo list for companies mapped to sales
        private static readonly List<InsuranceCompanyDto> SeedCompanies = new()
        {
            new("INS-01", "بوبا للتأمين الطبي (Bupa Egypt)", "BUPA-EG", "د. أحمد سامي", "01001234567", 20m, 50000m, true, true, 28, 48500m),
            new("INS-02", "أكسا للتأمين (AXA Insurance)", "AXA-EG", "أ. مها كمال", "01122334455", 15m, 60000m, true, true, 34, 62100m),
            new("INS-03", "ميتلايف للرعاية الصحية (MetLife)", "MET-LIFE", "د. هشام فؤاد", "01233445566", 20m, 40000m, false, true, 19, 29800m),
            new("INS-04", "تأمين نقابة المهندسين المصرية", "ENG-SYND", "م. طارق جلال", "01099887766", 10m, 35000m, false, true, 42, 51200m),
            new("INS-05", "تأمين نقابة الأطباء (اتحاد المهن الطبية)", "MED-SYND", "د. ريم منصور", "01188776655", 10m, 45000m, false, true, 31, 44000m)
        };

        // ── 1. List Companies ─────────────────────────────
        [HttpGet("companies")]
        public ActionResult<List<InsuranceCompanyDto>> GetCompanies()
        {
            return Ok(SeedCompanies);
        }

        // ── 2. Create Company ─────────────────────────────
        [HttpPost("companies")]
        public ActionResult<InsuranceCompanyDto> CreateCompany([FromBody] CreateInsuranceCompanyReq req)
        {
            var created = new InsuranceCompanyDto(
                $"INS-{DateTime.UtcNow.Ticks.ToString().Substring(12)}",
                req.Name,
                req.Code,
                req.ContactPerson,
                req.Phone,
                req.DefaultCopayPercent,
                req.MaxMonthlyCoverage,
                req.RequiresPreApproval,
                true,
                0,
                0m
            );
            SeedCompanies.Add(created);
            return Ok(created);
        }

        // ── 3. List Claims ────────────────────────────────
        [HttpGet("claims")]
        public async Task<ActionResult<List<InsuranceClaimDto>>> GetClaims([FromQuery] string? status, [FromQuery] string? company)
        {
            var orgId = OrgId;

            // Fetch live sales where saleType == "تأمين"
            var sales = await _db.Sales
                .Where(s => s.OrgId == orgId && s.SaleType == "تأمين")
                .OrderByDescending(s => s.Date)
                .Take(50)
                .ToListAsync();

            var claims = sales.Select(s => new InsuranceClaimDto(
                s.Id,
                s.InvoiceNumber,
                s.Date,
                s.CustomerName ?? "مريض تأمين",
                s.InsuranceCompanyName ?? "شركة التأمين الصحي",
                s.InsuranceCode ?? "CARD-10029",
                s.ApprovalCode,
                s.Total,
                s.Total * (s.InsurancePercent / 100),
                s.Total * ((100 - s.InsurancePercent) / 100),
                s.InsurancePercent,
                s.Status == "Voided" ? "Rejected" : "Approved",
                null
            )).ToList();

            // If no live sales yet, populate demonstration medical claims
            if (claims.Count == 0)
            {
                claims = new List<InsuranceClaimDto>
                {
                    new(Guid.NewGuid(), "INV-9901", DateTime.UtcNow.AddHours(-2), "محمد عادل عبد الرحمن", "بوبا للتأمين الطبي (Bupa Egypt)", "BUPA-44120", "APV-8812", 650m, 520m, 130m, 80m, "Approved", null),
                    new(Guid.NewGuid(), "INV-9902", DateTime.UtcNow.AddHours(-5), "سارة أحمد المنشاوي", "أكسا للتأمين (AXA Insurance)", "AXA-77312", "APV-9931", 1240m, 1054m, 186m, 85m, "Approved", null),
                    new(Guid.NewGuid(), "INV-9903", DateTime.UtcNow.AddHours(-9), "محمود كمال خليل", "تأمين نقابة المهندسين المصرية", "ENG-10042", null, 420m, 378m, 42m, 90m, "Pending", null),
                    new(Guid.NewGuid(), "INV-9904", DateTime.UtcNow.AddDays(-1), "هدى يوسف إبراهيم", "ميتلايف للرعاية الصحية (MetLife)", "MET-55421", "APV-1029", 890m, 712m, 178m, 80m, "Settled", null),
                    new(Guid.NewGuid(), "INV-9905", DateTime.UtcNow.AddDays(-2), "خالد حسن الشريف", "تأمين نقابة الأطباء (اتحاد المهن الطبية)", "MED-99214", "APV-5512", 1560m, 1404m, 156m, 90m, "Approved", null)
                };
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "All")
            {
                claims = claims.Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(company) && company != "All")
            {
                claims = claims.Where(c => c.InsuranceCompanyName.Contains(company, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Ok(claims);
        }

        // ── 4. Insurance Telemetry Stats ──────────────────
        [HttpGet("stats")]
        public ActionResult GetStats()
        {
            return Ok(new
            {
                TotalClaimsCount = 154,
                TotalClaimedAmount = 235600m,
                TotalApprovedAmount = 212400m,
                TotalSettledAmount = 184000m,
                PendingApprovalAmount = 23200m,
                ActiveCompaniesCount = SeedCompanies.Count
            });
        }
    }
}
