using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/audit-logs")]
    [Authorize]
    public sealed class AuditLogController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public AuditLogController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        // ── DTOs ──────────────────────────────────────────────

        public sealed record CreateAuditEventRequest(
            string EventType, string Category, string? ScreenName,
            string Description, int Severity = 0,
            string? TargetTable = null, string? TargetId = null,
            Guid? EmployeeId = null, string? EmployeeCode = null, string? EmployeeName = null,
            Guid? BranchId = null, string? IpAddress = null, string? DeviceName = null,
            FieldChangeDto[]? DataChanges = null);

        public sealed record FieldChangeDto(string FieldName, string? FieldLabel, string? OldValue, string? NewValue);

        public sealed record CreateSessionRequest(
            Guid EmployeeId, string EmployeeCode, string EmployeeName,
            Guid? BranchId = null, string? DeviceName = null, string? IpAddress = null);

        public sealed record AuditEventDto(
            Guid Id, Guid OrgId, Guid? BranchId,
            Guid? EmployeeId, string? EmployeeCode, string? EmployeeName,
            string EventType, string Category, string? ScreenName,
            string? TargetTable, string? TargetId, string Description,
            int Severity, string? IpAddress, string? DeviceName,
            DateTime CreatedAt, FieldChangeDto[]? DataChanges);

        public sealed record AuditSessionDto(
            Guid Id, Guid OrgId, Guid? BranchId,
            Guid EmployeeId, string EmployeeCode, string EmployeeName,
            DateTime LoginAt, DateTime? LogoutAt,
            string? DeviceName, string? IpAddress, string Status);

        public sealed record PagedResult<T>(T[] Items, int TotalCount, int Page, int PageSize);

        // ── POST api/audit-logs — create event ───────────────

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateAuditEventRequest req)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var ev = new AuditEvent
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = req.BranchId ?? _tenant.BranchId,
                EmployeeId = req.EmployeeId,
                EmployeeCode = req.EmployeeCode,
                EmployeeName = req.EmployeeName,
                EventType = req.EventType,
                Category = req.Category,
                ScreenName = req.ScreenName,
                TargetTable = req.TargetTable,
                TargetId = req.TargetId,
                Description = req.Description,
                Severity = req.Severity,
                IpAddress = req.IpAddress,
                DeviceName = req.DeviceName,
                CreatedAt = DateTime.UtcNow
            };

            _db.AuditEvents.Add(ev);

            if (req.DataChanges is { Length: > 0 })
            {
                foreach (var dc in req.DataChanges)
                {
                    _db.AuditDataChanges.Add(new AuditDataChange
                    {
                        Id = Guid.NewGuid(),
                        AuditEventId = ev.Id,
                        FieldName = dc.FieldName,
                        FieldLabel = dc.FieldLabel,
                        OldValue = dc.OldValue,
                        NewValue = dc.NewValue
                    });
                }
            }

            await _db.SaveChangesAsync();
            return Ok(new { id = ev.Id });
        }

        // ── GET api/audit-logs — list with filters ───────────

        [HttpGet]
        public async Task<ActionResult<PagedResult<AuditEventDto>>> GetEvents(
            [FromQuery] int page = 1,
            [FromQuery] int size = 50,
            [FromQuery] string? category = null,
            [FromQuery] string? eventType = null,
            [FromQuery] string? screenName = null,
            [FromQuery] Guid? employeeId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] int? severity = null,
            [FromQuery] string? search = null)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var q = _db.AuditEvents
                .Where(e => e.OrgId == orgId)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(category))
                q = q.Where(e => e.Category == category);
            if (!string.IsNullOrWhiteSpace(eventType))
                q = q.Where(e => e.EventType == eventType);
            if (!string.IsNullOrWhiteSpace(screenName))
                q = q.Where(e => e.ScreenName == screenName);
            if (employeeId.HasValue)
                q = q.Where(e => e.EmployeeId == employeeId.Value);
            if (from.HasValue)
                q = q.Where(e => e.CreatedAt >= from.Value);
            if (to.HasValue)
                q = q.Where(e => e.CreatedAt <= to.Value);
            if (severity.HasValue)
                q = q.Where(e => e.Severity == severity.Value);
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(e => e.Description.Contains(search) || (e.EmployeeName != null && e.EmployeeName.Contains(search)));

            var total = await q.CountAsync();

            var items = await q
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(e => new AuditEventDto(
                    e.Id, e.OrgId, e.BranchId,
                    e.EmployeeId, e.EmployeeCode, e.EmployeeName,
                    e.EventType, e.Category, e.ScreenName,
                    e.TargetTable, e.TargetId, e.Description,
                    e.Severity, e.IpAddress, e.DeviceName,
                    e.CreatedAt, null))
                .ToArrayAsync();

            return Ok(new PagedResult<AuditEventDto>(items, total, page, size));
        }

        // ── GET api/audit-logs/{id}/changes — detail changes ─

        [HttpGet("{id:guid}/changes")]
        public async Task<IActionResult> GetEventChanges(Guid id)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var ev = await _db.AuditEvents.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.OrgId == orgId);
            if (ev == null) return NotFound();

            var changes = await _db.AuditDataChanges.AsNoTracking()
                .Where(c => c.AuditEventId == id)
                .Select(c => new FieldChangeDto(c.FieldName, c.FieldLabel, c.OldValue, c.NewValue))
                .ToArrayAsync();

            return Ok(changes);
        }

        // ── GET api/audit-logs/stats ─────────────────────────

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var q = _db.AuditEvents.Where(e => e.OrgId == orgId).AsNoTracking();
            if (from.HasValue) q = q.Where(e => e.CreatedAt >= from.Value);
            if (to.HasValue) q = q.Where(e => e.CreatedAt <= to.Value);

            var byCategory = await q.GroupBy(e => e.Category).Select(g => new { Category = g.Key, Count = g.Count() }).ToListAsync();
            var byEventType = await q.GroupBy(e => e.EventType).Select(g => new { EventType = g.Key, Count = g.Count() }).ToListAsync();
            var total = await q.CountAsync();

            return Ok(new { total, byCategory, byEventType });
        }

        // ── Sessions ─────────────────────────────────────────

        [HttpPost("sessions")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequest req)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var session = new AuditSession
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = req.BranchId ?? _tenant.BranchId,
                EmployeeId = req.EmployeeId,
                EmployeeCode = req.EmployeeCode,
                EmployeeName = req.EmployeeName,
                LoginAt = DateTime.UtcNow,
                DeviceName = req.DeviceName,
                IpAddress = req.IpAddress,
                Status = "Active"
            };

            _db.AuditSessions.Add(session);
            await _db.SaveChangesAsync();
            return Ok(new { id = session.Id });
        }

        [HttpPut("sessions/{id:guid}/close")]
        public async Task<IActionResult> CloseSession(Guid id)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var session = await _db.AuditSessions.FirstOrDefaultAsync(s => s.Id == id && s.OrgId == orgId);
            if (session == null) return NotFound();

            session.LogoutAt = DateTime.UtcNow;
            session.Status = "Closed";
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("sessions")]
        public async Task<ActionResult<PagedResult<AuditSessionDto>>> GetSessions(
            [FromQuery] int page = 1,
            [FromQuery] int size = 50,
            [FromQuery] Guid? employeeId = null,
            [FromQuery] string? status = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var orgId = GetOrgId();
            if (orgId == Guid.Empty) return Unauthorized();

            var q = _db.AuditSessions.Where(s => s.OrgId == orgId).AsNoTracking();

            if (employeeId.HasValue) q = q.Where(s => s.EmployeeId == employeeId.Value);
            if (!string.IsNullOrWhiteSpace(status)) q = q.Where(s => s.Status == status);
            if (from.HasValue) q = q.Where(s => s.LoginAt >= from.Value);
            if (to.HasValue) q = q.Where(s => s.LoginAt <= to.Value);

            var total = await q.CountAsync();

            var items = await q
                .OrderByDescending(s => s.LoginAt)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(s => new AuditSessionDto(
                    s.Id, s.OrgId, s.BranchId,
                    s.EmployeeId, s.EmployeeCode, s.EmployeeName,
                    s.LoginAt, s.LogoutAt,
                    s.DeviceName, s.IpAddress, s.Status))
                .ToArrayAsync();

            return Ok(new PagedResult<AuditSessionDto>(items, total, page, size));
        }

        // ── Helper ───────────────────────────────────────────

        private Guid GetOrgId()
        {
            if (_tenant.OrgId != Guid.Empty) return _tenant.OrgId;
            var claim = User.FindFirst("org_id");
            return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
        }
    }
}
