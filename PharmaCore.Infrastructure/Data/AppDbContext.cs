using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PharmaCore.Domain.Entities;

namespace PharmaCore.Infrastructure.Data
{
    public interface ICurrentTenantContext
    {
        Guid OrgId { get; }
        Guid? BranchId { get; }
    }

    public sealed class AppDbContext : DbContext
    {
        private readonly ICurrentTenantContext _tenant;
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentTenantContext tenantContext) : base(options) { _tenant = tenantContext; }

        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<BranchAppAccount> BranchAppAccounts => Set<BranchAppAccount>();
        public DbSet<BranchAppSession> BranchAppSessions => Set<BranchAppSession>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<EmployeeBranchAccess> EmployeeBranchAccesses => Set<EmployeeBranchAccess>();
        public DbSet<EmployeePermission> EmployeePermissions => Set<EmployeePermission>();
        public DbSet<ActivationCode> ActivationCodes => Set<ActivationCode>();
        public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
        public DbSet<AuditDataChange> AuditDataChanges => Set<AuditDataChange>();
        public DbSet<AuditSession> AuditSessions => Set<AuditSession>();
        public DbSet<ReportLayout> ReportLayouts => Set<ReportLayout>();
        public DbSet<PharmaCoreMessage> PharmaCoreMessages => Set<PharmaCoreMessage>();

        // ── Products & Batches ──
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<ActiveIngredient> ActiveIngredients => Set<ActiveIngredient>();
        public DbSet<ProductCompany> ProductCompanies => Set<ProductCompany>();
        public DbSet<ProductUsage> ProductUsages => Set<ProductUsage>();
        public DbSet<Warehouse> Warehouses => Set<Warehouse>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductBatch> ProductBatches => Set<ProductBatch>();
        public DbSet<BatchStock> BatchStocks => Set<BatchStock>();
        public DbSet<BatchUnit> BatchUnits => Set<BatchUnit>();
        public DbSet<ProductUnit> ProductUnits => Set<ProductUnit>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<PromotionRule> PromotionRules => Set<PromotionRule>();
        public DbSet<SellerBonus> SellerBonuses => Set<SellerBonus>();
        public DbSet<EmployeeBonusLog> EmployeeBonusLogs => Set<EmployeeBonusLog>();
        public DbSet<PromotionCollection> PromotionCollections => Set<PromotionCollection>();
        public DbSet<PromotionCollectionItem> PromotionCollectionItems => Set<PromotionCollectionItem>();
        public DbSet<StandardItem> StandardItems => Set<StandardItem>();

        // ── Global Catalog (Drug Eye) ──
        public DbSet<DrugEyeActiveIngredient> DrugEyeActiveIngredients => Set<DrugEyeActiveIngredient>();
        public DbSet<DrugEyeUsage> DrugEyeUsages => Set<DrugEyeUsage>();
        public DbSet<DrugEyeDrug> DrugEyeDrugs => Set<DrugEyeDrug>();

        // ── Sales & Customers ──
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleLine> SaleLines => Set<SaleLine>();
        public DbSet<CashRegisterEntry> CashRegisterEntries => Set<CashRegisterEntry>();

        // ── Suppliers & Purchases ──
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<PurchaseLine> PurchaseLines => Set<PurchaseLine>();


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // ── RBAC hierarchy configuration
            mb.Entity<EmployeeRole>(e =>
            {
                e.HasOne(x => x.ParentRole)
                 .WithMany(x => x.ChildRoles)
                 .HasForeignKey(x => x.ParentRoleId)
                 .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete of tree

                e.HasOne(x => x.Organization)
                 .WithMany()
                 .HasForeignKey(x => x.OrgId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<RolePermission>(e =>
            {
                e.HasOne(x => x.Role)
                 .WithMany(x => x.Permissions)
                 .HasForeignKey(x => x.RoleId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Organization)
                 .WithMany()
                 .HasForeignKey(x => x.OrgId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Employee>(e =>
            {
                e.HasOne(x => x.EmployeeRole)
                 .WithMany(x => x.Employees)
                 .HasForeignKey(x => x.RoleId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            mb.Entity<ActivationCode>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.CodePlain)
                    .IsRequired()
                    .HasMaxLength(32);

                e.Property(x => x.CodeHash)
                    .HasMaxLength(128);

                e.Property(x => x.MaxDevices)
                    .HasDefaultValue(1);

                e.Property(x => x.UsedDevicesCount)
                    .HasDefaultValue(0);

                e.Property(x => x.IsActive)
                    .HasDefaultValue(true);

                e.HasIndex(x => x.CodePlain).IsUnique();

                e.HasOne(x => x.Organization)
                    .WithMany()
                    .HasForeignKey(x => x.OrgId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Tenant)
                    .WithMany()
                    .HasForeignKey(x => x.TenantId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(x => x.Region)
                    .WithMany()
                    .HasForeignKey(x => x.RegionId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(x => x.Branch)
                    .WithMany()
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // لو عايز تضمن إن الفلتر يطبَّق عليه برضو:
            mb.Entity<ActivationCode>().HasQueryFilter(a =>
                a.OrgId == _tenant.OrgId && a.IsActive);


            base.OnModelCreating(mb);
            mb.Entity<Organization>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).IsRequired().HasMaxLength(50);
                e.HasIndex(x => x.Code).IsUnique();
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                e.Property(x => x.IsActive).HasDefaultValue(true);
            });
            mb.Entity<Tenant>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).IsRequired().HasMaxLength(30);
                e.Property(x => x.Name).IsRequired().HasMaxLength(150);
                e.HasIndex(x => new { x.OrgId, x.Code }).IsUnique();
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasOne(x => x.Organization).WithMany(o => o.Tenants).HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Region>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).IsRequired().HasMaxLength(30);
                e.Property(x => x.Name).IsRequired().HasMaxLength(150);
                e.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasOne(x => x.Tenant).WithMany(t => t.Regions).HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Branch>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).IsRequired().HasMaxLength(40);
                e.Property(x => x.Name).IsRequired().HasMaxLength(150);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Code }).IsUnique();
                e.HasOne(x => x.Region).WithMany(r => r.Branches).HasForeignKey(x => x.RegionId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Organization).WithMany(o => o.Branches).HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.UserCode).IsRequired().HasMaxLength(60);
                e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(200);
                e.Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
                e.Property(x => x.Role).IsRequired().HasMaxLength(50);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.UserCode }).IsUnique();
                e.HasOne(x => x.Organization).WithMany(o => o.Users).HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany(b => b.Users).HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<Device>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.MachineId).IsRequired().HasMaxLength(120);
                e.Property(x => x.DeviceToken).IsRequired().HasMaxLength(200);
                e.Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
                e.Property(x => x.ActivatedAt).IsRequired();
                e.Property(x => x.DeactivatedAt);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.MachineId }).IsUnique();
                e.HasIndex(x => new { x.OrgId, x.DeviceToken }).IsUnique();
                e.HasOne(x => x.Organization).WithMany(o => o.Devices).HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany(b => b.Devices).HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<BranchAppAccount>(e =>
            {
                // Map to existing "BranchUsers" table created by ActivationManager
                e.ToTable("BranchUsers");
                e.HasKey(x => x.Id);
                e.Property(x => x.Username).HasColumnName("UserName").IsRequired().HasMaxLength(80);
                e.Property(x => x.PasswordHash).IsRequired();
                e.Property(x => x.MaxConcurrentSessions).HasColumnName("AllowedDevices").HasDefaultValue(1);
                e.Property(x => x.FailedCount).HasDefaultValue(0);
                e.Property(x => x.LockoutUntil);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.BranchId, x.Username }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany(b => b.BranchAppAccounts).HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<BranchAppSession>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.LoginAt).IsRequired();
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.BranchAppAccountId, x.IsActive });
                e.HasOne(x => x.BranchAppAccount).WithMany(a => a.Sessions).HasForeignKey(x => x.BranchAppAccountId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Device).WithMany(d => d.Sessions).HasForeignKey(x => x.DeviceId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Employee>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EmployeeCode).IsRequired().HasMaxLength(60);
                e.Property(x => x.PINHash).IsRequired().HasMaxLength(200);
                e.Property(x => x.FullName).IsRequired().HasMaxLength(150);
                e.Property(x => x.Role).IsRequired().HasMaxLength(60);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.EmployeeCode }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Tenant).WithMany().HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Region).WithMany().HasForeignKey(x => x.RegionId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<EmployeeBranchAccess>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.AccessType).IsRequired();
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.EmployeeId, x.BranchId }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Employee).WithMany(emp => emp.BranchAccesses).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany(b => b.EmployeeBranchAccesses).HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<EmployeePermission>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Permission).IsRequired().HasMaxLength(100);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.EmployeeId, x.Permission }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Employee).WithMany(emp => emp.Permissions).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Organization>().HasQueryFilter(o => o.Id == _tenant.OrgId && o.IsActive);
            mb.Entity<Tenant>().HasQueryFilter(t => t.OrgId == _tenant.OrgId && t.IsActive);
            mb.Entity<Region>().HasQueryFilter(r => r.Tenant != null && r.Tenant.OrgId == _tenant.OrgId && r.IsActive);
            mb.Entity<Branch>().HasQueryFilter(b => b.OrgId == _tenant.OrgId && b.IsActive && (_tenant.BranchId == null || b.Id == _tenant.BranchId));
            mb.Entity<User>().HasQueryFilter(u => u.OrgId == _tenant.OrgId && u.IsActive && (_tenant.BranchId == null || u.BranchId == null || u.BranchId == _tenant.BranchId));
            mb.Entity<Device>().HasQueryFilter(d => d.OrgId == _tenant.OrgId && d.IsActive && (_tenant.BranchId == null || d.BranchId == null || d.BranchId == _tenant.BranchId));
            mb.Entity<BranchAppAccount>().HasQueryFilter(a => a.OrgId == _tenant.OrgId && a.IsActive);
            mb.Entity<BranchAppSession>().HasQueryFilter(s => s.BranchAppAccount != null && s.BranchAppAccount.OrgId == _tenant.OrgId && s.IsActive);
            mb.Entity<Employee>().HasQueryFilter(e => e.OrgId == _tenant.OrgId && e.IsActive);
            mb.Entity<EmployeeBranchAccess>().HasQueryFilter(a => a.OrgId == _tenant.OrgId && a.IsActive);
            mb.Entity<EmployeePermission>().HasQueryFilter(p => p.OrgId == _tenant.OrgId && p.IsActive);
            mb.Entity<EmployeeRole>().HasQueryFilter(r => r.OrgId == _tenant.OrgId && r.IsActive);
            mb.Entity<RolePermission>().HasQueryFilter(rp => rp.OrgId == _tenant.OrgId && rp.IsActive);

            mb.Entity<ReportLayout>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.ReportName).IsRequired().HasMaxLength(150);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.ReportName }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<ReportLayout>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);

            // ── Products & Batches config ─────────────────────────
            mb.Entity<ProductCategory>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Name }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<ActiveIngredient>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(300);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Name }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Warehouse>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.BranchId, x.Name }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<ProductUnit>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Abbreviation).HasMaxLength(20);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Name }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Product>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.ProductCode).IsRequired().HasMaxLength(20);
                e.Property(x => x.NameEn).IsRequired().HasMaxLength(300);
                e.Property(x => x.NameAr).HasMaxLength(300);
                e.Property(x => x.SearchName).IsRequired().HasMaxLength(300);
                e.Property(x => x.UsageType).HasMaxLength(200);
                e.Property(x => x.CompanyName).HasMaxLength(200);
                e.Property(x => x.Notes).HasMaxLength(1000);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.ProductCode }).IsUnique();
                e.HasIndex(x => new { x.OrgId, x.SearchName });
                e.HasIndex(x => new { x.OrgId, x.CompanyName });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.ActiveIngredientNav).WithMany().HasForeignKey(x => x.ActiveIngredientId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<ProductBatch>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Barcode).IsRequired().HasMaxLength(100);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Barcode }).IsUnique();
                e.HasIndex(x => new { x.OrgId, x.ProductId });
                e.HasIndex(x => x.ExpiryDate);
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Product).WithMany(p => p.Batches).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<BatchStock>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.BatchId, x.BranchId }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Batch).WithMany(b => b.Stocks).HasForeignKey(x => x.BatchId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.WarehouseNav).WithMany().HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<BatchUnit>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.UnitName).IsRequired().HasMaxLength(100);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.BatchId, x.UnitId }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Batch).WithMany(b => b.Units).HasForeignKey(x => x.BatchId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<Promotion>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(300);
                e.Property(x => x.Description).HasMaxLength(1000);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.StartDate, x.EndDate });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<PromotionRule>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.ConditionBrand).HasMaxLength(200);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Promotion).WithMany(p => p.Rules).HasForeignKey(x => x.PromotionId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<SellerBonus>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.BatchId }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Batch).WithOne(b => b.SellerBonus).HasForeignKey<SellerBonus>(x => x.BatchId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<EmployeeBonusLog>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.EmployeeId, x.CreatedAt });
                e.HasIndex(x => new { x.OrgId, x.BatchId });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId).OnDelete(DeleteBehavior.Cascade);
            });

            // Query filters for product entities
            mb.Entity<ProductCategory>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<ActiveIngredient>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<Warehouse>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<ProductUnit>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<Product>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<ProductBatch>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<BatchStock>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<BatchUnit>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<Promotion>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<PromotionRule>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<SellerBonus>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<EmployeeBonusLog>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);

            // ── Sales & Customers config ─────────────────────────
            mb.Entity<Customer>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.CustomerCode).IsRequired().HasMaxLength(50);
                e.Property(x => x.Name).IsRequired().HasMaxLength(200);
                e.Property(x => x.Phone).HasMaxLength(30);
                e.Property(x => x.Address).HasMaxLength(500);
                e.Property(x => x.Notes).HasMaxLength(1000);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.CustomerCode }).IsUnique();
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<Sale>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(30);
                e.Property(x => x.SaleType).IsRequired().HasMaxLength(50);
                e.Property(x => x.MovementType).IsRequired().HasMaxLength(30).HasDefaultValue("بيع");
                e.Property(x => x.SellerName).HasMaxLength(150);
                e.Property(x => x.SellerCode).HasMaxLength(60);
                e.Property(x => x.CustomerName).HasMaxLength(200);
                e.Property(x => x.CustomerCode).HasMaxLength(50);
                e.Property(x => x.CustomerPhone).HasMaxLength(30);
                e.Property(x => x.Notes).HasMaxLength(1000);
                e.Property(x => x.InsuranceCompanyName).HasMaxLength(200);
                e.Property(x => x.InsuranceCode).HasMaxLength(100);
                e.Property(x => x.ApprovalCode).HasMaxLength(100);
                e.Property(x => x.InsuranceMobile).HasMaxLength(30);
                e.Property(x => x.DeliveryAddress).HasMaxLength(500);
                e.Property(x => x.EmployeePayType).HasMaxLength(50);
                e.Property(x => x.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Completed");
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.InvoiceNumber }).IsUnique();
                e.HasIndex(x => new { x.OrgId, x.Date });
                e.HasIndex(x => new { x.OrgId, x.SaleType });
                e.HasIndex(x => new { x.OrgId, x.CustomerId });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.SellerEmployee).WithMany().HasForeignKey(x => x.SellerEmployeeId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<SaleLine>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Barcode).IsRequired().HasMaxLength(100);
                e.Property(x => x.ProductName).IsRequired().HasMaxLength(300);
                e.Property(x => x.UnitName).IsRequired().HasMaxLength(100);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.SaleId });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Sale).WithMany(s => s.Lines).HasForeignKey(x => x.SaleId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId).OnDelete(DeleteBehavior.Restrict);
            });
            mb.Entity<CashRegisterEntry>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EntryType).IsRequired().HasMaxLength(50);
                e.Property(x => x.Description).HasMaxLength(500);
                e.Property(x => x.EmployeeName).IsRequired().HasMaxLength(150);
                e.Property(x => x.IsActive).HasDefaultValue(true);
                e.HasIndex(x => new { x.OrgId, x.Date });
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Sale).WithMany().HasForeignKey(x => x.SaleId).OnDelete(DeleteBehavior.SetNull);
            });
            mb.Entity<Customer>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<Sale>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<SaleLine>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<CashRegisterEntry>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);

            mb.Entity<Supplier>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<Purchase>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Supplier).WithMany(s => s.Purchases).HasForeignKey(x => x.SupplierId).OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<PurchaseLine>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Organization).WithMany().HasForeignKey(x => x.OrgId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Purchase).WithMany(p => p.Lines).HasForeignKey(x => x.PurchaseId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId).OnDelete(DeleteBehavior.SetNull);
            });

            mb.Entity<Supplier>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<Purchase>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);
            mb.Entity<PurchaseLine>().HasQueryFilter(x => x.OrgId == _tenant.OrgId && x.IsActive);

            // ── Audit tables ─────────────────────────────────────
            mb.Entity<AuditEvent>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EventType).IsRequired().HasMaxLength(50);
                e.Property(x => x.Category).IsRequired().HasMaxLength(50);
                e.Property(x => x.ScreenName).HasMaxLength(100);
                e.Property(x => x.TargetTable).HasMaxLength(100);
                e.Property(x => x.TargetId).HasMaxLength(100);
                e.Property(x => x.Description).IsRequired().HasMaxLength(1000);
                e.Property(x => x.EmployeeCode).HasMaxLength(60);
                e.Property(x => x.EmployeeName).HasMaxLength(150);
                e.Property(x => x.IpAddress).HasMaxLength(50);
                e.Property(x => x.DeviceName).HasMaxLength(150);
                e.Property(x => x.Severity).HasDefaultValue(0);
                e.Property(x => x.CreatedAt).IsRequired();
                e.HasIndex(x => new { x.OrgId, x.CreatedAt });
                e.HasIndex(x => new { x.OrgId, x.Category });
                e.HasIndex(x => new { x.OrgId, x.EventType });
                e.HasIndex(x => new { x.OrgId, x.ScreenName });
                e.HasIndex(x => new { x.OrgId, x.EmployeeId });
            });
            mb.Entity<AuditDataChange>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.FieldName).IsRequired().HasMaxLength(100);
                e.Property(x => x.FieldLabel).HasMaxLength(100);
                e.Property(x => x.OldValue).HasMaxLength(2000);
                e.Property(x => x.NewValue).HasMaxLength(2000);
                e.HasIndex(x => x.AuditEventId);
                e.HasOne(x => x.AuditEvent).WithMany(a => a.DataChanges).HasForeignKey(x => x.AuditEventId).OnDelete(DeleteBehavior.Cascade);
            });
            mb.Entity<AuditSession>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.EmployeeCode).IsRequired().HasMaxLength(60);
                e.Property(x => x.EmployeeName).IsRequired().HasMaxLength(150);
                e.Property(x => x.DeviceName).HasMaxLength(150);
                e.Property(x => x.IpAddress).HasMaxLength(50);
                e.Property(x => x.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Active");
                e.Property(x => x.LoginAt).IsRequired();
                e.HasIndex(x => new { x.OrgId, x.LoginAt });
                e.HasIndex(x => new { x.OrgId, x.EmployeeId });
            });
            // No query filters on audit tables — they should be queryable across all data
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyAudit();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken ct = default)
        {
            ApplyAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, ct);
        }
        private void ApplyAudit()
        {
            var now = DateTime.UtcNow;
            foreach (var e in ChangeTracker.Entries<BaseEntity>())
            {
                if (e.State == EntityState.Added) e.Entity.CreatedAt = now;
                else if (e.State == EntityState.Modified) e.Entity.UpdatedAt = now;
            }
        }
    }
}
