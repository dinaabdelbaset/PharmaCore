START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313132249_FixRBACQueryFilters') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260313132249_FixRBACQueryFilters', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318144001_AddStandardItems') THEN
    CREATE TABLE "StandardItems" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "NameEn" text NOT NULL,
        "NameAr" text,
        "ActiveIngredient" text,
        "Usage" text,
        "Company" text,
        "SearchIndex" text NOT NULL,
        "OrganizationId" uuid,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_StandardItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_StandardItems_Organizations_OrganizationId" FOREIGN KEY ("OrganizationId") REFERENCES "Organizations" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318144001_AddStandardItems') THEN
    CREATE INDEX "IX_StandardItems_OrganizationId" ON "StandardItems" ("OrganizationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318144001_AddStandardItems') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260318144001_AddStandardItems', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318155338_AddProductDetailsLookups') THEN
    CREATE TABLE "ProductCompanies" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "Name" text NOT NULL,
        "OrganizationId" uuid,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_ProductCompanies" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ProductCompanies_Organizations_OrganizationId" FOREIGN KEY ("OrganizationId") REFERENCES "Organizations" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318155338_AddProductDetailsLookups') THEN
    CREATE TABLE "ProductUsages" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "Name" text NOT NULL,
        "OrganizationId" uuid,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_ProductUsages" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ProductUsages_Organizations_OrganizationId" FOREIGN KEY ("OrganizationId") REFERENCES "Organizations" ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318155338_AddProductDetailsLookups') THEN
    CREATE INDEX "IX_ProductCompanies_OrganizationId" ON "ProductCompanies" ("OrganizationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318155338_AddProductDetailsLookups') THEN
    CREATE INDEX "IX_ProductUsages_OrganizationId" ON "ProductUsages" ("OrganizationId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318155338_AddProductDetailsLookups') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260318155338_AddProductDetailsLookups', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE TABLE "Customers" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "CustomerCode" character varying(50) NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Phone" character varying(30),
        "Address" character varying(500),
        "Notes" character varying(1000),
        "TotalPoints" numeric NOT NULL,
        "PointsBalance" numeric NOT NULL,
        "MoneyBalance" numeric NOT NULL,
        "VisitCount" integer NOT NULL,
        "LastVisitDate" timestamp with time zone,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Customers" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Customers_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Customers_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE TABLE "Sales" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "InvoiceNumber" character varying(30) NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "CustomerId" uuid,
        "CustomerName" character varying(200),
        "CustomerCode" character varying(50),
        "CustomerPhone" character varying(30),
        "SaleType" character varying(50) NOT NULL,
        "MovementType" character varying(30) NOT NULL DEFAULT 'بيع',
        "SellerEmployeeId" uuid,
        "SellerName" character varying(150),
        "SellerCode" character varying(60),
        "UseAlternateSeller" boolean NOT NULL,
        "Subtotal" numeric NOT NULL,
        "DiscountPercent" numeric NOT NULL,
        "DiscountAmount" numeric NOT NULL,
        "TaxTotal" numeric NOT NULL,
        "Total" numeric NOT NULL,
        "PaidCash" numeric NOT NULL,
        "PaidVisa" numeric NOT NULL,
        "PaidFromBalance" numeric NOT NULL,
        "Remaining" numeric NOT NULL,
        "Notes" character varying(1000),
        "InsuranceCompanyId" uuid,
        "InsuranceCompanyName" character varying(200),
        "InsuranceCode" character varying(100),
        "ApprovalCode" character varying(100),
        "InsurancePercent" numeric NOT NULL,
        "InsuranceMobile" character varying(30),
        "DeliveryAddress" character varying(500),
        "ReminderDate" timestamp with time zone,
        "EmployeeCustomerId" uuid,
        "EmployeePayType" character varying(50),
        "Status" character varying(20) NOT NULL DEFAULT 'Completed',
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Sales" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Sales_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Sales_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Sales_Employees_SellerEmployeeId" FOREIGN KEY ("SellerEmployeeId") REFERENCES "Employees" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Sales_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE TABLE "CashRegisterEntries" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "SaleId" uuid,
        "Amount" numeric NOT NULL,
        "EntryType" character varying(50) NOT NULL,
        "Description" character varying(500),
        "EmployeeName" character varying(150) NOT NULL,
        "IsBank" boolean NOT NULL,
        "Date" timestamp with time zone NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_CashRegisterEntries" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_CashRegisterEntries_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_CashRegisterEntries_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_CashRegisterEntries_Sales_SaleId" FOREIGN KEY ("SaleId") REFERENCES "Sales" ("Id") ON DELETE SET NULL
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE TABLE "SaleLines" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "SaleId" uuid NOT NULL,
        "LineNumber" integer NOT NULL,
        "ProductId" uuid NOT NULL,
        "BatchId" uuid NOT NULL,
        "Barcode" character varying(100) NOT NULL,
        "ProductName" character varying(300) NOT NULL,
        "UnitId" uuid NOT NULL,
        "UnitName" character varying(100) NOT NULL,
        "Quantity" numeric NOT NULL,
        "StockQtyAtSale" numeric NOT NULL,
        "PriceBeforeTax" numeric NOT NULL,
        "TaxValue" numeric NOT NULL,
        "UnitPrice" numeric NOT NULL,
        "Discount" numeric NOT NULL,
        "LineTotal" numeric NOT NULL,
        "LineTotalAfterDiscount" numeric NOT NULL,
        "BuyPrice" numeric NOT NULL,
        "ExpiryDate" timestamp with time zone,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_SaleLines" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_SaleLines_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_SaleLines_ProductBatches_BatchId" FOREIGN KEY ("BatchId") REFERENCES "ProductBatches" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_SaleLines_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_SaleLines_Sales_SaleId" FOREIGN KEY ("SaleId") REFERENCES "Sales" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_CashRegisterEntries_BranchId" ON "CashRegisterEntries" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_CashRegisterEntries_OrgId_Date" ON "CashRegisterEntries" ("OrgId", "Date");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_CashRegisterEntries_SaleId" ON "CashRegisterEntries" ("SaleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Customers_BranchId" ON "Customers" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE UNIQUE INDEX "IX_Customers_OrgId_CustomerCode" ON "Customers" ("OrgId", "CustomerCode");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_SaleLines_BatchId" ON "SaleLines" ("BatchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_SaleLines_OrgId_SaleId" ON "SaleLines" ("OrgId", "SaleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_SaleLines_ProductId" ON "SaleLines" ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_SaleLines_SaleId" ON "SaleLines" ("SaleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_BranchId" ON "Sales" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_CustomerId" ON "Sales" ("CustomerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_OrgId_CustomerId" ON "Sales" ("OrgId", "CustomerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_OrgId_Date" ON "Sales" ("OrgId", "Date");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE UNIQUE INDEX "IX_Sales_OrgId_InvoiceNumber" ON "Sales" ("OrgId", "InvoiceNumber");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_OrgId_SaleType" ON "Sales" ("OrgId", "SaleType");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    CREATE INDEX "IX_Sales_SellerEmployeeId" ON "Sales" ("SellerEmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260318172232_AddSalesSystem') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260318172232_AddSalesSystem', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321083300_AddAuditLogs') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260321083300_AddAuditLogs', '8.0.5');
    END IF;
END $EF$;
COMMIT;

