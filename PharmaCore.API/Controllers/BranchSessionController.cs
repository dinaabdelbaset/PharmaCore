using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.API.Security;
using PharmaCore.API.Services;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System.Security.Claims;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/branch-sessions")]
    public sealed class BranchSessionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;

        public BranchSessionController(AppDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public sealed record LoginRequest(Guid OrgId, Guid BranchId, string BranchCode, string UserName, string Password, string HardwareId);

        public sealed record LoginResponse(bool Ok, string Error, string? Token, Guid? BranchUserId, Guid? BranchUserDeviceId);

        // First step login: binds account to a device and returns a token containing org_id + branch_id.
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        {
            if (req.OrgId == Guid.Empty || req.BranchId == Guid.Empty)
                return Ok(new LoginResponse(false, "Org/Branch غير صحيح", null, null, null));

            var branch = await _db.Branches.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(b => b.Id == req.BranchId);
            if (branch == null || !branch.OrgId.Equals(req.OrgId))
                return Ok(new LoginResponse(false, "Branch غير موجود", null, null, null));

            if (!string.IsNullOrWhiteSpace(req.BranchCode) && !branch.Code.Equals(req.BranchCode, StringComparison.OrdinalIgnoreCase))
                return Ok(new LoginResponse(false, "كود الفرع غير متطابق", null, null, null));

            var account = await _db.BranchAppAccounts.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.OrgId == req.OrgId && a.BranchId == req.BranchId && a.Username.ToLower() == req.UserName.ToLower());
            if (account == null)
                return Ok(new LoginResponse(false, "اسم المستخدم غير موجود", null, null, null));

            if (!account.IsActive)
                return Ok(new LoginResponse(false, "الحساب غير مفعل", null, account.Id, null));

            // TODO: unify hashing format for BranchAppAccount.PasswordHash (currently string). Assume Argon2 string handled elsewhere.
            if (!BranchPasswordHasher.Verify(req.Password, account.PasswordHash))
                return Ok(new LoginResponse(false, "كلمة مرور غير صحيحة", null, account.Id, null));

            // Device: match by MachineId from Devices table (activated devices are already bound to branch)
            var device = await _db.Devices.IgnoreQueryFilters().FirstOrDefaultAsync(d => d.OrgId == req.OrgId && d.BranchId == req.BranchId && d.MachineId == req.HardwareId);
            if (device == null)
                return Ok(new LoginResponse(false, "الجهاز غير مفعل", null, account.Id, null));

            // ── Device-based concurrency ──────────────────────────────────
            // AllowedDevices = max UNIQUE physical machines this account can use.
            // If this device already has an active session → refresh it (always allow).
            // If it's a new device → check if the limit is reached.

            var existingSession = await _db.BranchAppSessions
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.BranchAppAccountId == account.Id
                                       && s.DeviceId == device.Id
                                       && s.IsActive);

            if (existingSession != null)
            {
                // Same device re-login → refresh the existing session
                existingSession.LoginAt = DateTime.UtcNow;
                existingSession.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                var claims2 = new List<Claim>
                {
                    new Claim("org_id", req.OrgId.ToString()),
                    new Claim("branch_id", req.BranchId.ToString()),
                    new Claim("branch_app_account_id", account.Id.ToString()),
                    new Claim("branch_app_session_id", existingSession.Id.ToString()),
                    new Claim(ClaimTypes.Name, req.UserName)
                };
                var token2 = _tokenService.GenerateToken(claims2, 12 * 60);
                return Ok(new LoginResponse(true, string.Empty, token2, account.Id, existingSession.Id));
            }

            // New device → count how many UNIQUE devices already have active sessions
            var uniqueDeviceCount = await _db.BranchAppSessions
                .IgnoreQueryFilters()
                .Where(s => s.BranchAppAccountId == account.Id && s.IsActive)
                .Select(s => s.DeviceId)
                .Distinct()
                .CountAsync();

            if (uniqueDeviceCount >= account.MaxConcurrentSessions)
                return Ok(new LoginResponse(false, "فشل الدخول: تجاوز عدد الأجهزة المسموح", null, account.Id, null));

            var session = new BranchAppSession
            {
                Id = Guid.NewGuid(),
                BranchAppAccountId = account.Id,
                DeviceId = device.Id,
                LoginAt = DateTime.UtcNow,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _db.BranchAppSessions.Add(session);
            await _db.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim("org_id", req.OrgId.ToString()),
                new Claim("branch_id", req.BranchId.ToString()),
                new Claim("branch_app_account_id", account.Id.ToString()),
                new Claim("branch_app_session_id", session.Id.ToString()),
                new Claim(ClaimTypes.Name, req.UserName)
            };

            var token = _tokenService.GenerateToken(claims, 12 * 60);
            return Ok(new LoginResponse(true, string.Empty, token, account.Id, session.Id));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var sid = User.FindFirstValue("branch_app_session_id");
            if (!Guid.TryParse(sid, out var sessionId)) return NoContent();

            var s = await _db.BranchAppSessions.FirstOrDefaultAsync(x => x.Id == sessionId);
            if (s == null) return NoContent();
            if (!s.IsActive) return NoContent();
            s.IsActive = false;
            s.LogoutAt = DateTime.UtcNow;
            s.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // ── Device Registration (called once during POS activation) ──────────
        public sealed record RegisterDeviceRequest(string ActivationCode, string MachineId, string MachineName);
        public sealed record RegisterDeviceResponse(bool Ok, string Error, string? DeviceToken, Guid? DeviceId, Guid? OrgId, Guid? BranchId);

        [HttpPost("register-device")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterDeviceResponse>> RegisterDevice([FromBody] RegisterDeviceRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.ActivationCode) || string.IsNullOrWhiteSpace(req.MachineId))
                return Ok(new RegisterDeviceResponse(false, "بيانات ناقصة", null, null, null, null));

            // Validate activation code directly (no tenant filter)
            var code = await _db.ActivationCodes
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CodePlain == req.ActivationCode.Trim());

            if (code == null)
                return Ok(new RegisterDeviceResponse(false, "كود التفعيل غير موجود", null, null, null, null));

            if (!code.IsActive)
                return Ok(new RegisterDeviceResponse(false, "كود التفعيل غير مفعل", null, null, null, null));

            if (code.ExpiresAt.HasValue && code.ExpiresAt.Value.Date < DateTime.UtcNow.Date)
                return Ok(new RegisterDeviceResponse(false, "انتهت صلاحية الكود", null, null, null, null));

            // Check if device already registered for this org
            var existing = await _db.Devices
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.OrgId == code.OrgId && d.MachineId == req.MachineId);

            if (existing != null)
            {
                // Update branch binding + re-activate if needed
                existing.BranchId = code.BranchId;
                existing.IsActive = true;
                existing.DeactivatedAt = null;
                existing.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return Ok(new RegisterDeviceResponse(true, string.Empty, existing.DeviceToken, existing.Id, code.OrgId, code.BranchId));
            }

            // Check usage limit
            if (code.UsedDevicesCount >= code.MaxDevices)
                return Ok(new RegisterDeviceResponse(false, "تم استهلاك العدد المسموح للأجهزة", null, null, null, null));

            // Create new device
            var deviceToken = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            var device = new Device
            {
                Id = Guid.NewGuid(),
                OrgId = code.OrgId,
                BranchId = code.BranchId,
                MachineId = req.MachineId,
                DeviceToken = deviceToken,
                DisplayName = string.IsNullOrWhiteSpace(req.MachineName) ? req.MachineId[..Math.Min(12, req.MachineId.Length)] : req.MachineName,
                ActivatedAt = DateTime.UtcNow,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _db.Devices.Add(device);
            await _db.SaveChangesAsync();

            return Ok(new RegisterDeviceResponse(true, string.Empty, deviceToken, device.Id, code.OrgId, code.BranchId));
        }
    }
}
