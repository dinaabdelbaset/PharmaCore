using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.API.Security;
using PharmaCore.API.Services;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public sealed class RegisterRequestDto
        {
            public string OrganizationName { get; set; } = string.Empty;
            public string AdminName { get; set; } = string.Empty;
            public string AdminEmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public sealed class LoginRequestDto
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public sealed class SetupRequestDto
        {
            public int Tenants { get; set; }
            public int Regions { get; set; }
            public int Branches { get; set; }
            public int Devices { get; set; }
            public Dictionary<string, bool> Addons { get; set; } = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request payload.");
            }

            if (string.IsNullOrWhiteSpace(req.OrganizationName) ||
                string.IsNullOrWhiteSpace(req.AdminName) ||
                string.IsNullOrWhiteSpace(req.AdminEmail) ||
                string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest("OrganizationName, AdminName, AdminEmail, and Password are required.");
            }

            var emailNormalized = req.AdminEmail.Trim().ToLower();

            // Validate that Email is not already used in the Users table (query case-insensitive, trimmed).
            var emailExists = await _db.Users.IgnoreQueryFilters()
                .AnyAsync(u => u.UserCode.ToLower() == emailNormalized);

            if (emailExists)
            {
                return BadRequest("Email already registered");
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // Create a new Organization
                var organization = new Organization
                {
                    Id = Guid.NewGuid(),
                    Code = "org_" + Guid.NewGuid().ToString().Substring(0, 8),
                    Name = req.OrganizationName.Trim(),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Organizations.Add(organization);

                // Create a default Tenant
                var tenant = new Tenant
                {
                    Id = Guid.NewGuid(),
                    OrgId = organization.Id,
                    Code = "DEFAULT",
                    Name = "Default Tenant",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Tenants.Add(tenant);

                // Create a default Region
                var region = new Region
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenant.Id,
                    Code = "DEFAULT",
                    Name = "Default Region",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Regions.Add(region);

                // Create a default Branch
                var branch = new Branch
                {
                    Id = Guid.NewGuid(),
                    OrgId = organization.Id,
                    RegionId = region.Id,
                    Code = "MAIN",
                    Name = "Main Branch",
                    NameAr = "الفرع الرئيسي",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Status = "ACTIVE"
                };
                _db.Branches.Add(branch);

                // Create a new User
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    OrgId = organization.Id,
                    BranchId = branch.Id,
                    UserCode = emailNormalized,
                    DisplayName = req.AdminName.Trim(),
                    PasswordHash = BranchPasswordHasher.Hash(req.Password),
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                _db.Users.Add(user);

                await _db.SaveChangesAsync();

                // Seed default role hierarchy for the new organization
                try
                {
                    await RoleHierarchySeeder.SeedAsync(_db, organization.Id);
                }
                catch
                {
                    // Ignore seeder issues to ensure registration succeeds
                }

                await transaction.CommitAsync();

                // Generate claims and token
                var claims = new[]
                {
                    new Claim("org_id", organization.Id.ToString()),
                    new Claim("branch_id", branch.Id.ToString()),
                    new Claim("user_id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("role", "Admin"),
                    new Claim(ClaimTypes.Name, user.UserCode),
                    new Claim("unique_name", user.UserCode)
                };

                var token = _tokenService.GenerateToken(claims);

                return Ok(new
                {
                    Token = token,
                    User = new
                    {
                        Id = user.Id,
                        Email = user.UserCode,
                        Name = user.DisplayName,
                        Role = user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred during registration: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request payload.");
            }

            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest("Email and Password are required.");
            }

            var emailNormalized = req.Email.Trim().ToLower();

            // Retrieve user by UserCode (email) from Users table (case-insensitive)
            var user = await _db.Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.UserCode.ToLower() == emailNormalized);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            // Verify password
            if (!BranchPasswordHasher.Verify(req.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }

            // Generate claims and token
            var claims = new List<Claim>
            {
                new Claim("org_id", user.OrgId.ToString()),
                new Claim("user_id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("role", user.Role),
                new Claim(ClaimTypes.Name, user.UserCode),
                new Claim("unique_name", user.UserCode)
            };

            if (user.BranchId.HasValue)
            {
                claims.Add(new Claim("branch_id", user.BranchId.Value.ToString()));
            }

            var token = _tokenService.GenerateToken(claims);

            var org = await _db.Organizations.IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == user.OrgId);
            bool setupCompleted = org != null && org.MaxTenants > 0;

            return Ok(new
            {
                Token = token,
                User = new
                {
                    Id = user.Id,
                    Email = user.UserCode,
                    Name = user.DisplayName,
                    Role = user.Role
                },
                SetupCompleted = setupCompleted
            });
        }

        [Authorize]
        [HttpPost("setup")]
        public async Task<IActionResult> Setup([FromBody] SetupRequestDto req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request payload.");
            }

            var orgIdClaim = User.FindFirst("org_id")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var orgId))
            {
                return Unauthorized("Organization ID claim not found or invalid.");
            }

            var organization = await _db.Organizations.IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.Id == orgId);

            if (organization == null)
            {
                return NotFound("Organization not found.");
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // Update organization's subscription/scale limits
                organization.MaxTenants = req.Tenants;
                organization.MaxRegions = req.Regions;
                organization.MaxBranches = req.Branches;
                organization.MaxDevices = req.Devices;
                organization.ActiveAddons = System.Text.Json.JsonSerializer.Serialize(req.Addons ?? new Dictionary<string, bool>());
                organization.UpdatedAt = DateTime.UtcNow;

                // Load existing Tenants, Regions, Branches ignoring query filters to get complete dataset
                var existingTenants = await _db.Tenants.IgnoreQueryFilters()
                    .Where(t => t.OrgId == orgId && t.IsActive)
                    .ToListAsync();

                // ── Provision Tenants ──
                int tenantsToCreate = req.Tenants - existingTenants.Count;
                if (tenantsToCreate > 0)
                {
                    for (int i = 1; tenantsToCreate > 0; i++)
                    {
                        var code = $"TENANT_{i}";
                        if (existingTenants.Any(t => t.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        var newTenant = new Tenant
                        {
                            Id = Guid.NewGuid(),
                            OrgId = orgId,
                            Code = code,
                            Name = $"Tenant {i}",
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        _db.Tenants.Add(newTenant);
                        existingTenants.Add(newTenant);
                        tenantsToCreate--;
                    }
                }

                // Load existing Regions for these tenants
                var tenantIds = existingTenants.Select(t => t.Id).ToList();
                var existingRegions = await _db.Regions.IgnoreQueryFilters()
                    .Where(r => tenantIds.Contains(r.TenantId) && r.IsActive)
                    .ToListAsync();

                // ── Provision Regions ──
                int regionsToCreate = req.Regions - existingRegions.Count;
                if (regionsToCreate > 0 && existingTenants.Any())
                {
                    for (int i = 1; regionsToCreate > 0; i++)
                    {
                        // Distribute logically: pick tenant with fewest regions
                        var targetTenant = existingTenants
                            .OrderBy(t => existingRegions.Count(r => r.TenantId == t.Id))
                            .First();

                        var code = $"REG_{i}";
                        if (existingRegions.Any(r => r.TenantId == targetTenant.Id && r.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        var newRegion = new Region
                        {
                            Id = Guid.NewGuid(),
                            TenantId = targetTenant.Id,
                            Code = code,
                            Name = $"Region {i}",
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        _db.Regions.Add(newRegion);
                        existingRegions.Add(newRegion);
                        regionsToCreate--;
                    }
                }

                // Load existing Branches
                var existingBranches = await _db.Branches.IgnoreQueryFilters()
                    .Where(b => b.OrgId == orgId && b.IsActive)
                    .ToListAsync();

                // ── Provision Branches ──
                int branchesToCreate = req.Branches - existingBranches.Count;
                if (branchesToCreate > 0 && existingRegions.Any())
                {
                    for (int i = 1; branchesToCreate > 0; i++)
                    {
                        // Distribute logically: pick region with fewest branches
                        var targetRegion = existingRegions
                            .OrderBy(r => existingBranches.Count(b => b.RegionId == r.Id))
                            .First();

                        var code = $"BR_{i}";
                        if (existingBranches.Any(b => b.OrgId == orgId && b.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        var newBranch = new Branch
                        {
                            Id = Guid.NewGuid(),
                            OrgId = orgId,
                            RegionId = targetRegion.Id,
                            Code = code,
                            Name = $"Branch {i}",
                            NameAr = $"فرع {i}",
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            Status = "ACTIVE"
                        };
                        _db.Branches.Add(newBranch);
                        existingBranches.Add(newBranch);
                        branchesToCreate--;
                    }
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { Message = "Setup completed successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"An error occurred during setup: {ex.Message}");
            }
        }
    }
}
