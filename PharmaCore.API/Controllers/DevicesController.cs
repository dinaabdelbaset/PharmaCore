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
    [Route("api/devices")]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public DevicesController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        private Guid ResolveOrgId()
        {
            var orgId = _tenant.OrgId;
            if (orgId == Guid.Empty)
            {
                var orgIdClaim = User.FindFirst("org_id")?.Value;
                if (Guid.TryParse(orgIdClaim, out var parsedOrgId))
                {
                    orgId = parsedOrgId;
                }
            }
            return orgId;
        }

        public record DeviceDto(
            Guid Id,
            string MachineId,
            string DeviceId,
            string DisplayName,
            string DeviceName,
            Guid? BranchId,
            string? BranchName,
            bool IsActive,
            DateTime ActivatedAt,
            DateTime? DeactivatedAt,
            DateTime? LastLogin,
            string? LastIp
        );

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var orgId = ResolveOrgId();
            if (orgId == Guid.Empty)
            {
                return Unauthorized("Organization identity not found.");
            }

            var devices = await _db.Devices
                .IgnoreQueryFilters()
                .Include(d => d.Branch)
                .Include(d => d.Sessions)
                .Where(d => d.OrgId == orgId)
                .ToListAsync();

            var list = devices.Select(d =>
            {
                var lastSession = d.Sessions?
                    .OrderByDescending(s => s.LoginAt)
                    .FirstOrDefault();

                return new DeviceDto(
                    Id: d.Id,
                    MachineId: d.MachineId,
                    DeviceId: d.MachineId,
                    DisplayName: d.DisplayName,
                    DeviceName: d.DisplayName,
                    BranchId: d.BranchId,
                    BranchName: d.Branch?.Name,
                    IsActive: d.IsActive,
                    ActivatedAt: d.ActivatedAt,
                    DeactivatedAt: d.DeactivatedAt,
                    LastLogin: lastSession?.LoginAt,
                    LastIp: lastSession?.IpAddress
                );
            }).ToList();

            return Ok(list);
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var orgId = ResolveOrgId();
            if (orgId == Guid.Empty)
            {
                return Unauthorized("Organization identity not found.");
            }

            var device = await _db.Devices
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.Id == id && d.OrgId == orgId);

            if (device == null)
            {
                return NotFound("الجهاز غير موجود");
            }

            device.IsActive = !device.IsActive;
            if (!device.IsActive)
            {
                device.DeactivatedAt = DateTime.UtcNow;
            }
            else
            {
                device.DeactivatedAt = null;
            }

            device.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var orgId = ResolveOrgId();
            if (orgId == Guid.Empty)
            {
                return Unauthorized("Organization identity not found.");
            }

            var device = await _db.Devices
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.Id == id && d.OrgId == orgId);

            if (device == null)
            {
                return NotFound("الجهاز غير موجود");
            }

            device.IsActive = false;
            device.DeactivatedAt = DateTime.UtcNow;
            device.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
