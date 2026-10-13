using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.API.Security;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/report-layouts")]
    public class ReportLayoutsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public ReportLayoutsController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        [HttpGet("{reportName}")]
        public async Task<IActionResult> GetLayout(string reportName)
        {
            var layout = await _db.ReportLayouts.FirstOrDefaultAsync(x => x.ReportName == reportName);
            if (layout == null)
            {
                return NotFound();
            }

            return Ok(new { reportName = layout.ReportName, layoutXml = layout.LayoutXml });
        }

        [HttpPost]
        public async Task<IActionResult> SaveLayout([FromBody] ReportLayoutDto request)
        {
            if (string.IsNullOrWhiteSpace(request.ReportName) || string.IsNullOrWhiteSpace(request.LayoutXml))
            {
                return BadRequest("ReportName and LayoutXml are required.");
            }

            var layout = await _db.ReportLayouts.FirstOrDefaultAsync(x => x.ReportName == request.ReportName);
            if (layout == null)
            {
                layout = new ReportLayout
                {
                    OrgId = _tenant.OrgId,
                    ReportName = request.ReportName,
                    LayoutXml = request.LayoutXml,
                    IsActive = true
                };
                _db.ReportLayouts.Add(layout);
            }
            else
            {
                layout.LayoutXml = request.LayoutXml;
            }

            await _db.SaveChangesAsync();
            return Ok(new { success = true, reportName = layout.ReportName });
        }

        [HttpDelete("{reportName}")]
        public async Task<IActionResult> DeleteLayout(string reportName)
        {
            var layout = await _db.ReportLayouts.FirstOrDefaultAsync(x => x.ReportName == reportName);
            if (layout != null)
            {
                _db.ReportLayouts.Remove(layout);
                await _db.SaveChangesAsync();
            }
            return Ok(new { success = true });
        }
    }

    public class ReportLayoutDto
    {
        public string ReportName { get; set; } = string.Empty;
        public string LayoutXml { get; set; } = string.Empty;
    }
}
