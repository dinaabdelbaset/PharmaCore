using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmaCore.Domain.Entities;

namespace PharmaCore.Infrastructure.Data
{
    /// <summary>
    /// Seeds the full pharmacy organizational role hierarchy for an organization.
    /// Only runs if no EmployeeRoles exist for the given org.
    /// Permission keys MUST match the actual POS permission keys in Frm_Permission.
    /// </summary>
    public static class RoleHierarchySeeder
    {
        // ══════════════════════════════════════════════════════════════
        //  All permission keys — MUST match Frm_Permission._groups
        // ══════════════════════════════════════════════════════════════
        private static readonly string[] AllPermissions = new[]
        {
            // الإعدادات
            "settings.view", "settings.edit", "permissions.manage",
            // المنتجات
            "products.add", "products.view", "products.activate",
            "products.barcode", "products.required", "products.expire",
            "products.unit", "products.company", "products.financials",
            // العملاء
            "customers.view", "customers.money", "customers.report",
            "customers.points", "customers.points_report",
            // الموردين
            "suppliers.view", "suppliers.money", "suppliers.report",
            // المشتريات
            "purchases.create", "purchases.na89", "purchases.report",
            // المبيعات
            "sales.create", "sales.insurance", "sales.insurance_company",
            "sales.user_report", "sales.report", "sales.insurance_report", "sales.profit",
            // المرتجعات
            "returns.sale", "returns.buy", "returns.report",
            // الخزينة والبنك
            "treasury.add_stock", "treasury.stock_deposit", "treasury.bank_deposit",
            "treasury.stock_withdraw", "treasury.bank_withdraw",
            "treasury.transfer", "treasury.bank_stock_transfer",
            "treasury.balance", "treasury.report",
            // الموظفين
            "employees.add", "employees.borrow_items", "employees.borrow_money",
            "employees.salary", "employees.borrow_items_report",
            "employees.borrow_money_report", "employees.salary_report",
            // الحسابات
            "accounting.type", "accounting.entry", "accounting.report",
            "accounting.receipt", "accounting.payment",
            "accounting.voucher_report", "accounting.tax_report",
            // التقارير
            "reports.customers", "reports.suppliers", "reports.purchases",
            "reports.sales", "reports.returns", "reports.accounting", "reports.shifts",
            // النسخ الاحتياطي
            "backup.create", "backup.restore",
            // المخازن
            "warehouse.add", "warehouse.inventory", "warehouse.transfer",
            "warehouse.out", "warehouse.transfer_report", "warehouse.out_report",
            // الشيفتات
            "shifts.open", "shifts.close"
        };

        private static readonly string[] FullPermissions = AllPermissions;

        private static readonly string[] DirectorPermissions = AllPermissions
            .Where(p => p != "backup.restore").ToArray();

        private static readonly string[] ManagerPermissions = new[]
        {
            "settings.view",
            "products.add", "products.view", "products.activate", "products.barcode",
            "products.required", "products.expire", "products.unit", "products.company", "products.financials",
            "customers.view", "customers.money", "customers.report", "customers.points", "customers.points_report",
            "suppliers.view", "suppliers.money", "suppliers.report",
            "purchases.create", "purchases.na89", "purchases.report",
            "sales.create", "sales.insurance", "sales.insurance_company",
            "sales.user_report", "sales.report", "sales.insurance_report", "sales.profit",
            "returns.sale", "returns.buy", "returns.report",
            "treasury.add_stock", "treasury.stock_deposit", "treasury.bank_deposit",
            "treasury.stock_withdraw", "treasury.bank_withdraw",
            "treasury.transfer", "treasury.bank_stock_transfer", "treasury.balance", "treasury.report",
            "employees.add", "employees.borrow_items", "employees.borrow_money",
            "employees.salary", "employees.borrow_items_report", "employees.borrow_money_report", "employees.salary_report",
            "accounting.type", "accounting.entry", "accounting.report",
            "accounting.receipt", "accounting.payment", "accounting.voucher_report", "accounting.tax_report",
            "reports.customers", "reports.suppliers", "reports.purchases",
            "reports.sales", "reports.returns", "reports.accounting", "reports.shifts",
            "warehouse.add", "warehouse.inventory", "warehouse.transfer",
            "warehouse.out", "warehouse.transfer_report", "warehouse.out_report",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] BranchManagerPermissions = new[]
        {
            "settings.view",
            "products.view", "products.activate", "products.barcode",
            "products.required", "products.expire", "products.financials",
            "customers.view", "customers.money", "customers.report", "customers.points",
            "suppliers.view", "suppliers.money",
            "purchases.create", "purchases.na89", "purchases.report",
            "sales.create", "sales.insurance", "sales.user_report", "sales.report", "sales.profit",
            "returns.sale", "returns.buy", "returns.report",
            "treasury.stock_deposit", "treasury.stock_withdraw",
            "treasury.transfer", "treasury.balance", "treasury.report",
            "employees.add", "employees.borrow_items", "employees.borrow_money",
            "employees.salary", "employees.borrow_items_report", "employees.borrow_money_report", "employees.salary_report",
            "reports.customers", "reports.suppliers", "reports.purchases",
            "reports.sales", "reports.returns", "reports.shifts",
            "warehouse.inventory", "warehouse.transfer",
            "warehouse.transfer_report",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] SupervisorPermissions = new[]
        {
            "products.view", "products.activate", "products.barcode", "products.expire",
            "customers.view", "customers.money", "customers.report",
            "sales.create", "sales.report", "sales.user_report",
            "returns.sale",
            "purchases.create", "purchases.na89",
            "treasury.stock_deposit", "treasury.balance",
            "employees.add",
            "reports.sales", "reports.shifts",
            "warehouse.inventory",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] PharmacistPermissions = new[]
        {
            "products.view", "products.barcode", "products.expire", "products.required",
            "customers.view", "customers.report",
            "sales.create", "sales.report",
            "returns.sale",
            "reports.sales",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] CashierPermissions = new[]
        {
            "products.view", "products.barcode",
            "customers.view",
            "sales.create", "sales.report",
            "returns.sale",
            "treasury.stock_deposit", "treasury.balance",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] WarehouseManagerPermissions = new[]
        {
            "products.view", "products.barcode", "products.required",
            "warehouse.add", "warehouse.inventory", "warehouse.transfer",
            "warehouse.out", "warehouse.transfer_report", "warehouse.out_report",
            "purchases.create", "purchases.na89", "purchases.report",
            "suppliers.view", "suppliers.report",
            "employees.add",
            "reports.purchases", "reports.suppliers"
        };

        private static readonly string[] WarehouseStaffPermissions = new[]
        {
            "products.view", "products.barcode",
            "warehouse.inventory", "warehouse.transfer",
            "warehouse.transfer_report",
            "purchases.na89"
        };

        private static readonly string[] FinancePermissions = new[]
        {
            "accounting.type", "accounting.entry", "accounting.report",
            "accounting.receipt", "accounting.payment",
            "accounting.voucher_report", "accounting.tax_report",
            "treasury.add_stock", "treasury.stock_deposit", "treasury.bank_deposit",
            "treasury.stock_withdraw", "treasury.bank_withdraw",
            "treasury.transfer", "treasury.bank_stock_transfer",
            "treasury.balance", "treasury.report",
            "reports.sales", "reports.purchases", "reports.accounting",
            "customers.view", "customers.money",
            "suppliers.view", "suppliers.money"
        };

        private static readonly string[] HRPermissions = new[]
        {
            "employees.add", "employees.borrow_items", "employees.borrow_money",
            "employees.salary", "employees.borrow_items_report",
            "employees.borrow_money_report", "employees.salary_report",
            "reports.shifts"
        };

        private static readonly string[] ITPermissions = new[]
        {
            "settings.view", "settings.edit", "permissions.manage",
            "backup.create", "backup.restore",
            "employees.add"
        };

        private static readonly string[] QualityPermissions = new[]
        {
            "products.view", "products.expire", "products.required",
            "warehouse.inventory",
            "reports.purchases"
        };

        private static readonly string[] PurchasingPermissions = new[]
        {
            "purchases.create", "purchases.na89", "purchases.report",
            "suppliers.view", "suppliers.money", "suppliers.report",
            "products.view", "products.required", "products.barcode",
            "warehouse.transfer",
            "reports.purchases", "reports.suppliers"
        };

        private static readonly string[] SalesAssistantPermissions = new[]
        {
            "products.view", "products.barcode",
            "customers.view",
            "sales.create",
            "shifts.open", "shifts.close"
        };

        private static readonly string[] DeliveryPermissions = new[]
        {
            "sales.report",
            "customers.view"
        };

        private static readonly string[] TraineePermissions = new[]
        {
            "products.view",
            "sales.create",
            "customers.view"
        };

        /// <summary>
        /// Seeds role hierarchy if none exist for the given org.
        /// Returns true if seeded, false if roles already existed.
        /// </summary>
        public static async Task<bool> SeedAsync(AppDbContext db, Guid orgId)
        {
            var hasRoles = await db.EmployeeRoles
                .IgnoreQueryFilters()
                .AnyAsync(r => r.OrgId == orgId && r.IsActive);

            if (hasRoles) return false;

            int order = 0;
            var now = DateTime.UtcNow;

            EmployeeRole MakeRole(string name, string desc, Guid? parentId, bool isSystem, string[] perms)
            {
                var role = new EmployeeRole
                {
                    Id = Guid.NewGuid(),
                    OrgId = orgId,
                    Name = name,
                    Description = desc,
                    SortOrder = order++,
                    ParentRoleId = parentId,
                    IsSystemRole = isSystem,
                    CreatedAt = now,
                    IsActive = true
                };
                db.EmployeeRoles.Add(role);

                foreach (var p in perms)
                {
                    db.RolePermissions.Add(new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        OrgId = orgId,
                        RoleId = role.Id,
                        Permission = p,
                        CreatedAt = now,
                        IsActive = true
                    });
                }
                return role;
            }

            // ═══════════════════════════════════════════════════
            //  Build the complete hierarchy
            // ═══════════════════════════════════════════════════

            var owner = MakeRole("Owner", "المالك", null, true, FullPermissions);
            var ceo = MakeRole("CEO", "المدير التنفيذي", owner.Id, false, FullPermissions);

            // ── Operations ──
            var opDir = MakeRole("Operations Director", "مدير العمليات", ceo.Id, false, DirectorPermissions);
            var regMgr = MakeRole("Regional Manager", "مدير إقليم", opDir.Id, false, ManagerPermissions);
            var areaMgr = MakeRole("Area Manager", "مدير منطقة", regMgr.Id, false, ManagerPermissions);
            var brMgr = MakeRole("Branch Manager", "مدير فرع", areaMgr.Id, false, BranchManagerPermissions);
            var asstBrMgr = MakeRole("Assistant Branch Manager", "مساعد مدير الفرع", brMgr.Id, false, BranchManagerPermissions);

            MakeRole("Shift Supervisor", "مشرف وردية", asstBrMgr.Id, false, SupervisorPermissions);

            var seniorPharm = MakeRole("Senior Pharmacist", "صيدلي أول", asstBrMgr.Id, false, PharmacistPermissions);
            MakeRole("Pharmacist", "صيدلي", seniorPharm.Id, false, PharmacistPermissions);
            MakeRole("Trainee Pharmacist", "صيدلي متدرب", seniorPharm.Id, false, TraineePermissions);

            MakeRole("Quality Officer", "مسؤول جودة", asstBrMgr.Id, false, QualityPermissions);

            MakeRole("Cashier", "كاشير", brMgr.Id, false, CashierPermissions);
            MakeRole("Sales Assistant", "مساعد بيع", brMgr.Id, false, SalesAssistantPermissions);
            MakeRole("Delivery", "مندوب توصيل", brMgr.Id, false, DeliveryPermissions);

            MakeRole("Purchasing Manager", "مدير مشتريات", opDir.Id, false, PurchasingPermissions);

            // ── Warehouse ──
            var whMgr = MakeRole("Warehouse Manager", "مدير المخزن المركزي", ceo.Id, false, WarehouseManagerPermissions);
            MakeRole("Inventory Controller", "مسؤول الجرد", whMgr.Id, false, WarehouseStaffPermissions);
            MakeRole("Store Keeper", "أمين مخزن", whMgr.Id, false, WarehouseStaffPermissions);
            MakeRole("Warehouse Worker", "عامل مخزن", whMgr.Id, false, new[] { "warehouse.inventory", "products.view" });

            // ── Finance ──
            var finDir = MakeRole("Finance Director", "المدير المالي", ceo.Id, false, FinancePermissions);
            var finMgr = MakeRole("Finance Manager", "مدير الحسابات", finDir.Id, false, FinancePermissions);
            MakeRole("Accountant", "محاسب", finMgr.Id, false, new[] {
                "accounting.type", "accounting.entry", "accounting.report",
                "accounting.receipt", "accounting.payment", "accounting.voucher_report",
                "treasury.balance", "treasury.report",
                "reports.accounting"
            });
            MakeRole("Treasury", "الخزينة", finMgr.Id, false, new[] {
                "treasury.stock_deposit", "treasury.bank_deposit",
                "treasury.stock_withdraw", "treasury.bank_withdraw",
                "treasury.transfer", "treasury.balance", "treasury.report"
            });

            // ── HR ──
            var hrDir = MakeRole("HR Director", "مدير الموارد البشرية", ceo.Id, false, HRPermissions);
            var hrMgr = MakeRole("HR Manager", "مدير شؤون الموظفين", hrDir.Id, false, HRPermissions);
            MakeRole("HR Officer", "موظف موارد بشرية", hrMgr.Id, false, new[] {
                "employees.add", "employees.salary", "employees.salary_report"
            });
            MakeRole("Recruiter", "مسؤول توظيف", hrMgr.Id, false, new[] { "employees.add" });

            // ── IT ──
            var itDir = MakeRole("IT Director", "مدير التقنية", ceo.Id, false, ITPermissions);
            MakeRole("System Admin", "مسؤول النظام", itDir.Id, false, ITPermissions);
            MakeRole("IT Support", "دعم تقني", itDir.Id, false, new[] { "settings.view", "backup.create" });
            MakeRole("POS Support", "دعم نقاط البيع", itDir.Id, false, new[] { "settings.view" });

            await db.SaveChangesAsync();
            return true;
        }
    }
}
