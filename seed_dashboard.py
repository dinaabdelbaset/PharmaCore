import psycopg2
import uuid
import random
from datetime import datetime, timedelta

def main():
    print("=========================================================")
    print("      PHARMACORE HIGH-FIDELITY DASHBOARD DATA SEEDER     ")
    print("=========================================================")
    
    conn_str = "dbname='PharmaCoreDB' user='postgres' host='localhost' port='5432' password='12345' sslmode='disable'"
    db_name = "Local (Port 5432)"

    print(f"\nConnecting to {db_name}...")
    try:
        conn = psycopg2.connect(conn_str)
        cur = conn.cursor()
        print("[OK] Connected successfully!")
    except Exception as e:
        print(f"[ERROR] Failed to connect: {e}")
        return

    try:
        # 1. Fetch or Create Organization
        cur.execute('SELECT "OrgId" FROM "Users" WHERE "UserCode" = \'tareqmahmoud25@gmail.com\' LIMIT 1;')
        user_org = cur.fetchone()
        if user_org:
            org_id = user_org[0]
            print(f"[OK] Found Admin User OrgId: {org_id}")
        else:
            cur.execute('SELECT "Id", "Name" FROM "Organizations" LIMIT 1;')
            org = cur.fetchone()
            if org:
                org_id, org_name = org
                print(f"[OK] Found existing Organization")
            else:
                org_id = str(uuid.uuid4())
                org_name = "صيدليات العزبي الرئيسية"
                cur.execute(
                    'INSERT INTO "Organizations" ("Id", "Code", "Name", "IsActive", "CreatedAt") VALUES (%s, %s, %s, %s, %s);',
                    (org_id, 'EL_EZZABY_ORG', org_name, True, datetime.utcnow())
                )
                print(f"[OK] Created new Organization")

        # 2. Fetch or Create Tenant
        cur.execute('SELECT "Id" FROM "Tenants" WHERE "OrgId" = %s LIMIT 1;', (org_id,))
        tenant = cur.fetchone()
        if tenant:
            tenant_id = tenant[0]
            print(f"[OK] Found existing Tenant")
        else:
            tenant_id = str(uuid.uuid4())
            cur.execute(
                'INSERT INTO "Tenants" ("Id", "OrgId", "Code", "Name", "IsActive", "CreatedAt") VALUES (%s, %s, %s, %s, %s, %s);',
                (tenant_id, org_id, 'TENANT_MAIN', 'المقر الرئيسي', True, datetime.utcnow())
            )
            print(f"[OK] Created new Tenant")

        # 3. Fetch or Create Region
        cur.execute('SELECT "Id" FROM "Regions" WHERE "TenantId" = %s LIMIT 1;', (tenant_id,))
        region = cur.fetchone()
        if region:
            region_id = region[0]
            print(f"[OK] Found existing Region")
        else:
            region_id = str(uuid.uuid4())
            cur.execute(
                'INSERT INTO "Regions" ("Id", "TenantId", "Code", "Name", "IsActive", "CreatedAt") VALUES (%s, %s, %s, %s, %s, %s);',
                (region_id, tenant_id, 'REG_CAIRO', 'منطقة القاهرة', True, datetime.utcnow())
            )
            print(f"[OK] Created new Region")

        # 4. Fetch or Create Branches
        cur.execute('SELECT "Id", "Name" FROM "Branches" WHERE "OrgId" = %s;', (org_id,))
        branches = cur.fetchall()
        if len(branches) >= 3:
            print(f"[OK] Found existing branches: {len(branches)}")
        else:
            branch_data = [
                ('BR_CAIRO', 'فرع القاهرة - وسط البلد', 'Cairo Main Branch'),
                ('BR_ALEX', 'فرع الإسكندرية - سموحة', 'Alexandria Branch'),
                ('BR_GIZA', 'فرع الجيزة - شارع الهرم', 'Giza Branch')
            ]
            branches = []
            for code, name_ar, name_en in branch_data:
                b_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "Branches" ("Id", "OrgId", "RegionId", "Code", "Name", "NameAr", "IsActive", "CreatedAt", "Status") VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (b_id, org_id, region_id, code, name_en, name_ar, True, datetime.utcnow(), 'ACTIVE')
                )
                branches.append((b_id, name_en))
            print(f"[OK] Seeded new branches: {len(branches)}")

        # 5. Fetch or Create Products, Batches & Units
        products_to_seed = [
            ('Panadol Extra 500mg', 'بنادول اكسترا 500 مجم', '622100300124', 15.00, 22.00, 'Tablet', 'GlaxoSmithKline', 'Tablet'),
            ('Augmentin 1g Tablets', 'أوجمنتين 1 جرام أقراص', '622300104921', 45.00, 89.00, 'Tablet', 'GlaxoSmithKline', 'Tablet'),
            ('Cataflam 50mg Tablets', 'كاتافلام 50 مجم أقراص', '622201004521', 18.00, 36.00, 'Tablet', 'Novartis', 'Tablet'),
            ('Concor 5mg Tablets', 'كونكور 5 مجم أقراص', '622104005912', 25.00, 42.50, 'Tablet', 'Merck', 'Tablet'),
            ('Otrivin Pediatric Spray', 'أوتريفين أطفال بخاخ', '622104005988', 12.00, 20.00, 'Spray', 'Novartis', 'Spray'),
            ('Ventolin Inhaler 100mcg', 'فنتولين بخاخ 100 ميكروجرام', '622400508930', 35.00, 55.00, 'Inhaler', 'GlaxoSmithKline', 'Inhaler'),
            ('C-Retard 500mg Capsules', 'سي ريتارد 500 مجم كبسول', '622400508212', 10.00, 16.50, 'Capsule', 'Eva Pharma', 'Capsule')
        ]

        seeded_products = []
        for name_en, name_ar, barcode, buy_price, sell_price, unit_name, company_name, usage_type in products_to_seed:
            # Get or Create ProductUnit
            cur.execute('SELECT "Id" FROM "ProductUnits" WHERE "Name" = %s AND "OrgId" = %s LIMIT 1;', (unit_name, org_id))
            unit_row = cur.fetchone()
            if unit_row:
                unit_id = unit_row[0]
            else:
                unit_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "ProductUnits" ("Id", "OrgId", "Name", "IsActive", "CreatedAt") VALUES (%s, %s, %s, %s, %s);',
                    (unit_id, org_id, unit_name, True, datetime.utcnow())
                )

            # Get or Create Product
            cur.execute(
                'SELECT p."Id" FROM "Products" p JOIN "ProductBatches" b ON p."Id" = b."ProductId" '
                'WHERE b."Barcode" = %s AND p."OrgId" = %s LIMIT 1;',
                (barcode, org_id)
            )
            prod = cur.fetchone()
            if prod:
                p_id = prod[0]
                cur.execute(
                    'UPDATE "Products" SET "CompanyName" = %s, "UsageType" = %s WHERE "Id" = %s;',
                    (company_name, usage_type, p_id)
                )
            else:
                p_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "Products" ("Id", "OrgId", "ProductCode", "NameEn", "NameAr", "SearchName", "IsActive", "CreatedAt", "IsEnglishPrimary", "TotalSoldQty", "TotalPurchasedQty", "TotalReturnedQty", "CompanyName", "UsageType") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (p_id, org_id, 'PROD_' + barcode[-6:], name_en, name_ar, name_en.lower(), True, datetime.utcnow(), True, 0.0, 0.0, 0.0, company_name, usage_type)
                )
            
            # Ensure ProductBatch exists
            cur.execute('SELECT "Id" FROM "ProductBatches" WHERE "ProductId" = %s AND "Barcode" = %s;', (p_id, barcode))
            batch = cur.fetchone()
            if batch:
                b_id = batch[0]
            else:
                b_id = str(uuid.uuid4())
                is_close_expiry = random.choice([True, False, False])
                days_to_expiry = random.randint(10, 80) if is_close_expiry else random.randint(300, 700)
                expiry_date = datetime.utcnow() + timedelta(days=days_to_expiry)
                
                cur.execute(
                    'INSERT INTO "ProductBatches" ('
                    '"Id", "OrgId", "ProductId", "Barcode", "PharmacyBuyPrice", "PublicSalePrice", '
                    '"ExpiryDate", "IsActive", "CreatedAt", "MinQtyAlert", "MainUnitId", "MainUnitQty", '
                    '"SaleUnitId", "PurchaseUnitId", "IsTaxable", "TaxPercent", "PriceAfterTax", "MaxDiscount"'
                    ') VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (b_id, org_id, p_id, barcode, buy_price, sell_price, expiry_date, True, datetime.utcnow(), 15, unit_id, 1, unit_id, unit_id, False, 0.0, sell_price, 0.0)
                )
            
            seeded_products.append((p_id, b_id, name_en, barcode, sell_price, buy_price, unit_id, unit_name))
            
        print(f"[OK] Seeded/Verified products: {len(seeded_products)}")
        conn.commit()

        # 6. Seed Batch Stocks
        print("Seeding stock quantities and alerts...")
        for b_id, branch_name in branches:
            for p_id, batch_id, name_en, barcode, sell_price, buy_price, unit_id, unit in seeded_products:
                cur.execute('SELECT "Id" FROM "BatchStocks" WHERE "BranchId" = %s AND "BatchId" = %s;', (b_id, batch_id))
                stock = cur.fetchone()
                if not stock:
                    is_low_stock = random.choice([True, False, False]) and "Main" not in branch_name
                    qty = random.randint(1, 4) if is_low_stock else random.randint(40, 150)
                    
                    cur.execute(
                        'INSERT INTO "BatchStocks" ("Id", "BranchId", "BatchId", "Quantity", "IsActive", "CreatedAt", "OrgId") '
                        'VALUES (%s, %s, %s, %s, %s, %s, %s);',
                        (str(uuid.uuid4()), b_id, batch_id, qty, True, datetime.utcnow(), org_id)
                    )
        print("[OK] Seeded stock levels.")

        # 7. Seed Cashier Sessions & Accounts
        print("Seeding cashier sessions...")
        cur.execute('SELECT "Id" FROM "Users" WHERE "OrgId" = %s LIMIT 1;', (org_id,))
        user = cur.fetchone()
        if user:
            user_id = user[0]
        else:
            user_id = str(uuid.uuid4())
            cur.execute(
                'INSERT INTO "Users" ("Id", "OrgId", "UserCode", "DisplayName", "PasswordHash", "Role", "IsActive", "CreatedAt") '
                'VALUES (%s, %s, %s, %s, %s, %s, %s, %s);',
                (user_id, org_id, 'tareqmahmoud25@gmail.com', 'طارق محمود', 
                 '$argon2id$v=19$m=65536,t=3,p=1$lXX8hGpoOK+ffnTOOj+roQ$Mcav0J/fk4Nma+o3Me2GWevJlduGYhMPkx8uNJBFaB0',
                 'Admin', True, datetime.utcnow())
            )
            print("[OK] Created default Admin user")

        cashiers = [
            ('emp_ahmed', 'أحمد حسن', 'ahmed@pharmacore.com'),
            ('emp_sara', 'سارة محمد', 'sara@pharmacore.com'),
            ('emp_khaled', 'خالد علي', 'khaled@pharmacore.com')
        ]
        
        seeded_cashiers = []
        for code, name, email in cashiers:
            cur.execute('SELECT "Id" FROM "Users" WHERE "UserCode" = %s;', (email,))
            emp_user = cur.fetchone()
            if emp_user:
                emp_user_id = emp_user[0]
            else:
                emp_user_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "Users" ("Id", "OrgId", "UserCode", "DisplayName", "PasswordHash", "Role", "IsActive", "CreatedAt") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s);',
                    (emp_user_id, org_id, email, name, 
                     '$argon2id$v=19$m=65536,t=3,p=1$bk5rT+qhsamV57b9ZjDNyA$APHYObz9UqRRvhk79ntrOB4gVowBg2Owk5Cqk693z/s',
                     'Employee', True, datetime.utcnow())
                )
            
            cur.execute('SELECT "Id" FROM "Employees" WHERE "EmployeeCode" = %s AND "OrgId" = %s;', (email, org_id))
            employee = cur.fetchone()
            if employee:
                emp_id = employee[0]
            else:
                emp_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "Employees" ("Id", "OrgId", "EmployeeCode", "FullName", "IsActive", "CreatedAt", "PINHash", "JobTitle", "Role") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (emp_id, org_id, email, name, True, datetime.utcnow(), '1234', 'كاشير', 'Employee')
                )
            seeded_cashiers.append((emp_id, name, email))

        for b_id, _ in branches:
            cur.execute('SELECT "Id" FROM "BranchUsers" WHERE "BranchId" = %s AND "UserName" = %s;', (b_id, f"ACC_{b_id[:8]}"))
            acc_row = cur.fetchone()
            if acc_row:
                acc_id = acc_row[0]
            else:
                acc_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "BranchUsers" ("Id", "OrgId", "BranchId", "UserName", "PasswordHash", "AllowedDevices", "IsActive", "CreatedAt", "FailedCount") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (acc_id, org_id, b_id, f"ACC_{b_id[:8]}", 'some_hash', 5, True, datetime.utcnow(), 0)
                )
            
            cur.execute('SELECT "Id" FROM "Devices" WHERE "BranchId" = %s AND "MachineId" = %s;', (b_id, f"MAC-{b_id[:4]}"))
            dev_row = cur.fetchone()
            if dev_row:
                dev_id = dev_row[0]
            else:
                dev_id = str(uuid.uuid4())
                cur.execute(
                    'INSERT INTO "Devices" ("Id", "OrgId", "BranchId", "MachineId", "DeviceToken", "DisplayName", "ActivatedAt", "IsActive", "CreatedAt") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (dev_id, org_id, b_id, f"MAC-{b_id[:4]}", f"TOK-{b_id[:4]}", f"Device for {b_id[:4]}", datetime.utcnow() - timedelta(days=90), True, datetime.utcnow())
                )
            
            cur.execute('SELECT "Id" FROM "BranchAppSessions" WHERE "BranchAppAccountId" = %s AND "DeviceId" = %s;', (acc_id, dev_id))
            sess_row = cur.fetchone()
            if not sess_row:
                cur.execute(
                    'INSERT INTO "BranchAppSessions" ("Id", "BranchAppAccountId", "DeviceId", "IsActive", "LoginAt", "CreatedAt") '
                    'VALUES (%s, %s, %s, %s, %s, %s);',
                    (str(uuid.uuid4()), acc_id, dev_id, True, datetime.utcnow() - timedelta(hours=3), datetime.utcnow())
                )
        print("[OK] Seeded cashier sessions.")

        # 8. Seed Cash Register Entries (Deposits & Expenses)
        print("Seeding treasury entries and expenses...")
        expense_categories = ['Rent', 'Utilities', 'Salaries', 'Supplies', 'Maintenance', 'Marketing']
        
        for b_id, branch_name in branches:
            cur.execute(
                'INSERT INTO "CashRegisterEntries" ("Id", "OrgId", "BranchId", "Amount", "EntryType", "Description", "EmployeeName", "IsBank", "Date", "IsActive", "CreatedAt") '
                'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
                (str(uuid.uuid4()), org_id, b_id, 100000.00, 'Deposit', 'رصيد افتتاحي للخزينة', 'طارق محمود', False, datetime.utcnow() - timedelta(days=90), True, datetime.utcnow())
            )
            cur.execute(
                'INSERT INTO "CashRegisterEntries" ("Id", "OrgId", "BranchId", "Amount", "EntryType", "Description", "EmployeeName", "IsBank", "Date", "IsActive", "CreatedAt") '
                'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
                (str(uuid.uuid4()), org_id, b_id, 250000.00, 'Deposit', 'رصيد افتتاحي للبنك', 'طارق محمود', True, datetime.utcnow() - timedelta(days=90), True, datetime.utcnow())
            )
            
            for days_ago in [5, 12, 20, 35, 50, 70]:
                category = random.choice(expense_categories)
                amount = random.randint(100, 3500)
                is_bank = random.choice([True, False])
                date = datetime.utcnow() - timedelta(days=days_ago)
                cur.execute(
                    'INSERT INTO "CashRegisterEntries" ("Id", "OrgId", "BranchId", "Amount", "EntryType", "Description", "EmployeeName", "IsBank", "Date", "IsActive", "CreatedAt") '
                    'VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
                    (str(uuid.uuid4()), org_id, b_id, -amount, 'Expense', f"[{category}]", 'أحمد حسن', is_bank, date, True, datetime.utcnow())
                )

        # 9. Seed Sales Transactions (10 days of sales to build charts)
        print("Generating 10 days of historical sales transactions...")
        start_date = datetime.utcnow() - timedelta(days=10)
        current_date = start_date
        
        invoice_seq = 1000
        sales_to_insert = []
        sale_lines_to_insert = []
        cash_entries_to_insert = []

        while current_date <= datetime.utcnow():
            for b_id, b_name in branches:
                days_diff = (current_date - start_date).days
                month_index = days_diff // 30
                invoices_count = random.randint(2, 6) + month_index
                
                for _ in range(invoices_count):
                    sale_id = str(uuid.uuid4())
                    invoice_seq += 1
                    inv_num = f"INV-{current_date.strftime('%Y%m')}-{invoice_seq}"
                    
                    emp_id, emp_name, emp_email = random.choice(seeded_cashiers)
                    lines_count = random.randint(1, 3)
                    subtotal = 0
                    
                    for line_num in range(1, lines_count + 1):
                        p_id, batch_id, name_en, barcode, sell_price, buy_price, unit_id, unit = random.choice(seeded_products)
                        qty = random.randint(1, 5)
                        line_total = sell_price * qty
                        subtotal += line_total
                        
                        sale_lines_to_insert.append((
                            str(uuid.uuid4()), org_id, sale_id, line_num, p_id, batch_id,
                            barcode, name_en, unit_id, unit, qty, qty + 10,
                            buy_price, 0.0, sell_price, 0.0, line_total, line_total, buy_price,
                            current_date, True, current_date
                        ))

                    discount_percent = random.choice([0.0, 0.0, 0.0, 5.0, 10.0])
                    discount_amount = subtotal * (discount_percent / 100)
                    total = subtotal - discount_amount
                    
                    paid_cash = total if random.choice([True, False]) else 0.0
                    paid_visa = total - paid_cash
                    
                    sales_to_insert.append((
                        sale_id, org_id, b_id, inv_num, current_date,
                        'Walk-In Customer', 'CUST_001', 'Walk-in', 'Sale',
                        emp_id, emp_name, emp_email, False,
                        subtotal, discount_percent, discount_amount, 0.0, total,
                        paid_cash, paid_visa, 0.0, 0.0, 'daily sales',
                        'Completed', True, current_date, 0.0
                    ))
                    
                    cash_entries_to_insert.append((
                        str(uuid.uuid4()), org_id, b_id, sale_id, total,
                        'sales', f"payment for {inv_num}",
                        emp_name, paid_visa > 0, current_date, True, current_date
                    ))

            current_date += timedelta(days=1)

        print(f"Inserting {len(sales_to_insert)} sales records...")
        cur.executemany(
            'INSERT INTO "Sales" ('
            '"Id", "OrgId", "BranchId", "InvoiceNumber", "Date", '
            '"CustomerName", "CustomerCode", "SaleType", "MovementType", '
            '"SellerEmployeeId", "SellerName", "SellerCode", "UseAlternateSeller", '
            '"Subtotal", "DiscountPercent", "DiscountAmount", "TaxTotal", "Total", '
            '"PaidCash", "PaidVisa", "PaidFromBalance", "Remaining", "Notes", '
            '"Status", "IsActive", "CreatedAt", "InsurancePercent"'
            ') VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
            sales_to_insert
        )

        print(f"Inserting {len(sale_lines_to_insert)} sales lines...")
        cur.executemany(
            'INSERT INTO "SaleLines" ('
            '"Id", "OrgId", "SaleId", "LineNumber", "ProductId", "BatchId", '
            '"Barcode", "ProductName", "UnitId", "UnitName", "Quantity", "StockQtyAtSale", '
            '"PriceBeforeTax", "TaxValue", "UnitPrice", "Discount", "LineTotal", "LineTotalAfterDiscount", "BuyPrice", '
            '"ExpiryDate", "IsActive", "CreatedAt"'
            ') VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
            sale_lines_to_insert
        )

        print(f"Inserting {len(cash_entries_to_insert)} cash ledger entries...")
        cur.executemany(
            'INSERT INTO "CashRegisterEntries" ('
            '"Id", "OrgId", "BranchId", "SaleId", "Amount", '
            '"EntryType", "Description", "EmployeeName", "IsBank", "Date", "IsActive", "CreatedAt"'
            ') VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s);',
            cash_entries_to_insert
        )

        conn.commit()
        print("\n=========================================================")
        print("[OK] SUCCESS: DATABASE SUCCESSFULLY SEEDED WITH LIVE DATA!")
        print("=========================================================")
        
        cur.execute('SELECT COUNT(*) FROM "Sales" WHERE "OrgId" = %s;', (org_id,))
        sc = cur.fetchone()[0]
        cur.execute('SELECT COUNT(*) FROM "CashRegisterEntries" WHERE "OrgId" = %s;', (org_id,))
        ce = cur.fetchone()[0]
        print(f"Total Sales generated: {sc}")
        print(f"Total Cash Register Entries generated: {ce}")

    except Exception as ex:
        conn.rollback()
        print(f"[ERROR] Error: {ex}")
    finally:
        cur.close()
        conn.close()

if __name__ == "__main__":
    main()
