using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.API.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using PharmaCore.API.Security;
using PharmaCore.Infrastructure.Interceptors;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authentication - JWT (kept)
var jwtSection = configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key") ?? "ReplaceWithSecureKey";
var issuer = jwtSection.GetValue<string>("Issuer") ?? "pharmacore";
var audience = jwtSection.GetValue<string>("Audience") ?? "pharmacore_clients";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true
    };
});

// Authorization (basic)
builder.Services.AddAuthorization();

// DbContext (AppDbContext)
var connectionString = configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured in appsettings.json");

builder.Services.AddScoped<ICurrentTenantContext, ClaimsTenantContext>();
builder.Services.AddScoped<AuditSaveChangesInterceptor>();
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    options.UseNpgsql(connectionString);
    options.AddInterceptors(sp.GetRequiredService<AuditSaveChangesInterceptor>());
});

// Health checks (basic + DB)
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("db")
    .AddCheck("self", () => HealthCheckResult.Healthy());

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmployeeLoginService, EmployeeLoginService>();

var app = builder.Build();

// Migrate database if possible
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try { db.Database.Migrate(); } catch { /* ignore migrate errors at startup */ }

    // Self-healing schema booster for branch details
    try
    {
        db.Database.ExecuteSqlRaw(@"
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""NameAr"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Address"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""City"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Phones"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Email"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""ManagerName"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""ManagerPhone"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""ManagerEmail"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Latitude"" double precision NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Longitude"" double precision NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Description"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""DescriptionAr"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""OpeningHours"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""EstablishedDate"" text NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""TotalArea"" double precision NULL;
            ALTER TABLE ""Branches"" ADD COLUMN IF NOT EXISTS ""Status"" text NOT NULL DEFAULT 'ACTIVE';
        ");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error running Branch details schema booster: " + ex.Message);
    }

    // Self-healing schema booster for organization subscription settings
    try
    {
        db.Database.ExecuteSqlRaw(@"
            ALTER TABLE ""Organizations"" ADD COLUMN IF NOT EXISTS ""MaxTenants"" integer NOT NULL DEFAULT 0;
            ALTER TABLE ""Organizations"" ADD COLUMN IF NOT EXISTS ""MaxRegions"" integer NOT NULL DEFAULT 0;
            ALTER TABLE ""Organizations"" ADD COLUMN IF NOT EXISTS ""MaxBranches"" integer NOT NULL DEFAULT 0;
            ALTER TABLE ""Organizations"" ADD COLUMN IF NOT EXISTS ""MaxDevices"" integer NOT NULL DEFAULT 0;
            ALTER TABLE ""Organizations"" ADD COLUMN IF NOT EXISTS ""ActiveAddons"" text NULL;
        ");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error running Organization subscription settings schema booster: " + ex.Message);
    }

    // Auto-seed default role hierarchy for all orgs
    try
    {
        var orgIds = await db.Organizations.IgnoreQueryFilters()
            .Where(o => o.IsActive)
            .Select(o => o.Id)
            .ToListAsync();
        foreach (var orgId in orgIds)
            await PharmaCore.Infrastructure.Data.RoleHierarchySeeder.SeedAsync(db, orgId);
    }
    catch { /* ignore seed errors at startup */ }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Removed to prevent 302 redirects converting POST to GET when behind Nginx or direct HTTP.
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();

// Custom DB health check avoiding missing AddDbContextCheck extension
internal sealed class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _db;
    public DatabaseHealthCheck(AppDbContext db) => _db = db;
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _db.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
