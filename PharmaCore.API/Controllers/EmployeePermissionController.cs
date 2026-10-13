using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/employee-permissions")]
    [Authorize]
    public class EmployeePermissionController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public EmployeePermissionController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        /// <summary>
        /// Get all permission keys for an employee.
        /// </summary>
        [HttpGet("{employeeId:guid}")]
        public async Task<IActionResult> GetPermissions(Guid employeeId)
        {
            var perms = await _db.EmployeePermissions
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.OrgId == _tenant.OrgId && p.EmployeeId == employeeId && p.IsActive)
                .Select(p => p.Permission)
                .ToListAsync();

            return Ok(perms);
        }

        public record SetPermissionsRequest(Guid? RoleId, string[] Permissions);

        /// <summary>
        /// Replace all permissions for an employee (bulk upsert) and update their Role.
        /// Accepts a SetPermissionsRequest containing the RoleId and permission array.
        /// </summary>
        [HttpPut("{employeeId:guid}")]
        public async Task<IActionResult> SetPermissions(Guid employeeId, [FromBody] SetPermissionsRequest req)
        {
            if (_tenant.OrgId == Guid.Empty)
                return BadRequest("OrgId مطلوب");

            // Verify employee exists
            var employee = await _db.Employees
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(e => e.Id == employeeId && e.OrgId == _tenant.OrgId);

            if (employee == null)
                return NotFound("الموظف غير موجود");

            // Update Employee Role
            employee.RoleId = req.RoleId;
            employee.UpdatedAt = DateTime.UtcNow;

            // Remove existing permissions
            var existing = await _db.EmployeePermissions
                .IgnoreQueryFilters()
                .Where(p => p.OrgId == _tenant.OrgId && p.EmployeeId == employeeId)
                .ToListAsync();

            _db.EmployeePermissions.RemoveRange(existing);

            // Add new permissions
            var newPerms = req.Permissions
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Distinct()
                .Select(p => new EmployeePermission
                {
                    Id = Guid.NewGuid(),
                    OrgId = _tenant.OrgId,
                    EmployeeId = employeeId,
                    Permission = p.Trim().ToLowerInvariant(),
                    IsActive = true
                })
                .ToList();

            _db.EmployeePermissions.AddRange(newPerms);
            await _db.SaveChangesAsync();

            return Ok(new { count = newPerms.Count, permissions = newPerms.Select(p => p.Permission) });
        }

        /// <summary>
        /// Get all valid permission keys supported by the system.
        /// </summary>
        [HttpGet("available")]
        [AllowAnonymous]
        public IActionResult GetAvailablePermissions()
        {
            return Ok(PermissionKeys.All);
        }
    }

    /// <summary>
    /// Central registry of all permission keys used by the system.
    /// </summary>
    public static class PermissionKeys
    {
        // Settings
        public const string SettingsView = "settings.view";
        public const string SettingsEdit = "settings.edit";
        public const string PermissionsManage = "permissions.manage";

        // Products
        public const string ProductsAdd = "products.add";
        public const string ProductsView = "products.view";
        public const string ProductsActivate = "products.activate";
        public const string ProductsBarcode = "products.barcode";
        public const string ProductsRequired = "products.required";
        public const string ProductsExpire = "products.expire";
        public const string ProductsUnit = "products.unit";
        public const string ProductsCompany = "products.company";
        public const string ProductsFinancials = "products.financials";

        // Customers
        public const string CustomersView = "customers.view";
        public const string CustomersMoney = "customers.money";
        public const string CustomersReport = "customers.report";
        public const string CustomersPoints = "customers.points";
        public const string CustomersPointsReport = "customers.points_report";

        // Suppliers
        public const string SuppliersView = "suppliers.view";
        public const string SuppliersMoney = "suppliers.money";
        public const string SuppliersReport = "suppliers.report";

        // Purchases
        public const string PurchasesCreate = "purchases.create";
        public const string PurchasesNa89 = "purchases.na89";
        public const string PurchasesReport = "purchases.report";

        // Sales
        public const string SalesCreate = "sales.create";
        public const string SalesInsurance = "sales.insurance";
        public const string SalesInsuranceCompany = "sales.insurance_company";
        public const string SalesUserReport = "sales.user_report";
        public const string SalesReport = "sales.report";
        public const string SalesInsuranceReport = "sales.insurance_report";
        public const string SalesProfit = "sales.profit";

        // Returns
        public const string ReturnsSale = "returns.sale";
        public const string ReturnsBuy = "returns.buy";
        public const string ReturnsReport = "returns.report";

        // Treasury
        public const string TreasuryAddStock = "treasury.add_stock";
        public const string TreasuryStockDeposit = "treasury.stock_deposit";
        public const string TreasuryBankDeposit = "treasury.bank_deposit";
        public const string TreasuryStockWithdraw = "treasury.stock_withdraw";
        public const string TreasuryBankWithdraw = "treasury.bank_withdraw";
        public const string TreasuryTransfer = "treasury.transfer";
        public const string TreasuryBankStockTransfer = "treasury.bank_stock_transfer";
        public const string TreasuryBalance = "treasury.balance";
        public const string TreasuryReport = "treasury.report";

        // Employees
        public const string EmployeesAdd = "employees.add";
        public const string EmployeesBorrowItems = "employees.borrow_items";
        public const string EmployeesBorrowMoney = "employees.borrow_money";
        public const string EmployeesSalary = "employees.salary";
        public const string EmployeesBorrowItemsReport = "employees.borrow_items_report";
        public const string EmployeesBorrowMoneyReport = "employees.borrow_money_report";
        public const string EmployeesSalaryReport = "employees.salary_report";

        // Accounting
        public const string AccountingType = "accounting.type";
        public const string AccountingEntry = "accounting.entry";
        public const string AccountingReport = "accounting.report";
        public const string AccountingReceipt = "accounting.receipt";
        public const string AccountingPayment = "accounting.payment";
        public const string AccountingVoucherReport = "accounting.voucher_report";
        public const string AccountingTaxReport = "accounting.tax_report";

        // Reports
        public const string ReportsCustomers = "reports.customers";
        public const string ReportsSuppliers = "reports.suppliers";
        public const string ReportsPurchases = "reports.purchases";
        public const string ReportsSales = "reports.sales";
        public const string ReportsReturns = "reports.returns";
        public const string ReportsAccounting = "reports.accounting";
        public const string ReportsShifts = "reports.shifts";

        // Backup
        public const string BackupCreate = "backup.create";
        public const string BackupRestore = "backup.restore";

        // Warehouse
        public const string WarehouseAdd = "warehouse.add";
        public const string WarehouseInventory = "warehouse.inventory";
        public const string WarehouseTransfer = "warehouse.transfer";
        public const string WarehouseOut = "warehouse.out";
        public const string WarehouseTransferReport = "warehouse.transfer_report";
        public const string WarehouseOutReport = "warehouse.out_report";

        // Shifts
        public const string ShiftsOpen = "shifts.open";
        public const string ShiftsClose = "shifts.close";

        /// <summary>All valid permission keys grouped by category.</summary>
        public static readonly Dictionary<string, string[]> All = new()
        {
            ["الإعدادات"] = new[] { SettingsView, SettingsEdit, PermissionsManage },
            ["المنتجات"] = new[] { ProductsAdd, ProductsView, ProductsActivate, ProductsBarcode, ProductsRequired, ProductsExpire, ProductsUnit, ProductsCompany, ProductsFinancials },
            ["العملاء"] = new[] { CustomersView, CustomersMoney, CustomersReport, CustomersPoints, CustomersPointsReport },
            ["الموردين"] = new[] { SuppliersView, SuppliersMoney, SuppliersReport },
            ["المشتريات"] = new[] { PurchasesCreate, PurchasesNa89, PurchasesReport },
            ["المبيعات"] = new[] { SalesCreate, SalesInsurance, SalesInsuranceCompany, SalesUserReport, SalesReport, SalesInsuranceReport, SalesProfit },
            ["المرتجعات"] = new[] { ReturnsSale, ReturnsBuy, ReturnsReport },
            ["الخزينة والبنك"] = new[] { TreasuryAddStock, TreasuryStockDeposit, TreasuryBankDeposit, TreasuryStockWithdraw, TreasuryBankWithdraw, TreasuryTransfer, TreasuryBankStockTransfer, TreasuryBalance, TreasuryReport },
            ["الموظفين"] = new[] { EmployeesAdd, EmployeesBorrowItems, EmployeesBorrowMoney, EmployeesSalary, EmployeesBorrowItemsReport, EmployeesBorrowMoneyReport, EmployeesSalaryReport },
            ["الحسابات"] = new[] { AccountingType, AccountingEntry, AccountingReport, AccountingReceipt, AccountingPayment, AccountingVoucherReport, AccountingTaxReport },
            ["التقارير"] = new[] { ReportsCustomers, ReportsSuppliers, ReportsPurchases, ReportsSales, ReportsReturns, ReportsAccounting, ReportsShifts },
            ["النسخ الاحتياطي"] = new[] { BackupCreate, BackupRestore },
            ["المخازن"] = new[] { WarehouseAdd, WarehouseInventory, WarehouseTransfer, WarehouseOut, WarehouseTransferReport, WarehouseOutReport },
            ["الشيفتات"] = new[] { ShiftsOpen, ShiftsClose },
        };

        /// <summary>Human-readable Arabic labels for each permission key.</summary>
        public static readonly Dictionary<string, string> Labels = new()
        {
            [SettingsView] = "عرض الإعدادات",
            [SettingsEdit] = "تعديل الإعدادات",
            [PermissionsManage] = "إدارة الصلاحيات",
            [ProductsAdd] = "إضافة منتج",
            [ProductsView] = "عرض المنتجات",
            [ProductsActivate] = "تفعيل المنتجات",
            [ProductsBarcode] = "باركود",
            [ProductsRequired] = "المطلوب توفيره",
            [ProductsExpire] = "الصلاحية",
            [ProductsUnit] = "الوحدات",
            [ProductsCompany] = "الشركات",
            [ProductsFinancials] = "البيان المالي",
            [CustomersView] = "عرض العملاء",
            [CustomersMoney] = "مالية العملاء",
            [CustomersReport] = "تقارير العملاء",
            [CustomersPoints] = "النقاط",
            [CustomersPointsReport] = "تقارير النقاط",
            [SuppliersView] = "عرض الموردين",
            [SuppliersMoney] = "مالية الموردين",
            [SuppliersReport] = "تقارير الموردين",
            [PurchasesCreate] = "إنشاء فاتورة شراء",
            [PurchasesNa89] = "الأصناف الناقصة",
            [PurchasesReport] = "تقارير المشتريات",
            [SalesCreate] = "إنشاء فاتورة بيع",
            [SalesInsurance] = "بيع تأمين",
            [SalesInsuranceCompany] = "شركات التأمين",
            [SalesUserReport] = "تقارير مبيعات المستخدمين",
            [SalesReport] = "تقارير المبيعات",
            [SalesInsuranceReport] = "تقارير التأمين",
            [SalesProfit] = "الأرباح",
            [ReturnsSale] = "مرتجع بيع",
            [ReturnsBuy] = "مرتجع شراء",
            [ReturnsReport] = "تقارير المرتجعات",
            [TreasuryAddStock] = "إضافة خزينة",
            [TreasuryStockDeposit] = "إيداع خزينة",
            [TreasuryBankDeposit] = "إيداع بنك",
            [TreasuryStockWithdraw] = "سحب خزينة",
            [TreasuryBankWithdraw] = "سحب بنك",
            [TreasuryTransfer] = "تحويل أموال",
            [TreasuryBankStockTransfer] = "تحويل بنك/خزينة",
            [TreasuryBalance] = "الأرصدة الحالية",
            [TreasuryReport] = "تقارير الخزينة",
            [EmployeesAdd] = "إضافة موظف",
            [EmployeesBorrowItems] = "استعارة أصناف",
            [EmployeesBorrowMoney] = "سلف مالية",
            [EmployeesSalary] = "المرتبات",
            [EmployeesBorrowItemsReport] = "تقارير استعارة أصناف",
            [EmployeesBorrowMoneyReport] = "تقارير السلف",
            [EmployeesSalaryReport] = "تقارير المرتبات",
            [AccountingType] = "أنواع المديونيات",
            [AccountingEntry] = "المديونيات",
            [AccountingReport] = "تقارير المديونيات",
            [AccountingReceipt] = "سند قبض",
            [AccountingPayment] = "سند صرف",
            [AccountingVoucherReport] = "تقارير السندات",
            [AccountingTaxReport] = "تقارير الضرائب",
            [ReportsCustomers] = "تقرير العملاء",
            [ReportsSuppliers] = "تقرير الموردين",
            [ReportsPurchases] = "تقرير المشتريات",
            [ReportsSales] = "تقرير المبيعات",
            [ReportsReturns] = "تقرير المرتجعات",
            [ReportsAccounting] = "تقرير المديونيات",
            [ReportsShifts] = "تقرير الشيفتات",
            [BackupCreate] = "أخذ نسخة احتياطية",
            [BackupRestore] = "استرجاع نسخة احتياطية",
            [WarehouseAdd] = "إضافة مخزن",
            [WarehouseInventory] = "جرد المخزن",
            [WarehouseTransfer] = "تحويل بين مخازن",
            [WarehouseOut] = "إخراج من مخزن",
            [WarehouseTransferReport] = "تقارير تحويل مخازن",
            [WarehouseOutReport] = "تقارير إخراج مخازن",
            [ShiftsOpen] = "فتح شيفت",
            [ShiftsClose] = "إغلاق شيفت",
        };
    }
}
