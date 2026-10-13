using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;



namespace PharmaCore.Infrastructure.Data
{
    internal sealed class DesignTimeTenantContext : ICurrentTenantContext
    {
        public Guid OrgId { get; } = Guid.Empty; // design-time placeholder
        public Guid? BranchId { get; } = null;
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cs = Environment.GetEnvironmentVariable("PHARMACORE_DB") 
                     ?? "Host=localhost;Port=5432;Database=PharmaCoreDB;Username=postgres;Password=12345;Trust Server Certificate=true";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(cs);
            return new AppDbContext(optionsBuilder.Options, new DesignTimeTenantContext());
        }
    }
}
