-- ========================================================
-- PharmaCore Products & Batches System — PostgreSQL Migration
-- Run: sudo -u postgres psql -d pharmacore -f /path/to/this.sql
-- ========================================================

-- Product Categories
CREATE TABLE IF NOT EXISTS "ProductCategories" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "Name" VARCHAR(200) NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ProductCategories_OrgId_Name" ON "ProductCategories" ("OrgId", "Name");

-- Active Ingredients
CREATE TABLE IF NOT EXISTS "ActiveIngredients" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "Name" VARCHAR(300) NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ActiveIngredients_OrgId_Name" ON "ActiveIngredients" ("OrgId", "Name");

-- Warehouses
CREATE TABLE IF NOT EXISTS "Warehouses" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "BranchId" UUID REFERENCES "Branches"("Id") ON DELETE SET NULL,
    "Name" VARCHAR(200) NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Warehouses_OrgId_BranchId_Name" ON "Warehouses" ("OrgId", "BranchId", "Name");

-- Product Units (Box, Strip, Tablet, etc.)
CREATE TABLE IF NOT EXISTS "ProductUnits" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "Name" VARCHAR(100) NOT NULL,
    "Abbreviation" VARCHAR(20),
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ProductUnits_OrgId_Name" ON "ProductUnits" ("OrgId", "Name");

-- Products (master data)
CREATE TABLE IF NOT EXISTS "Products" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "ProductCode" VARCHAR(20) NOT NULL,
    "NameEn" VARCHAR(300) NOT NULL,
    "NameAr" VARCHAR(300),
    "SearchName" VARCHAR(300) NOT NULL,
    "CategoryId" UUID REFERENCES "ProductCategories"("Id") ON DELETE SET NULL,
    "ActiveIngredientId" UUID REFERENCES "ActiveIngredients"("Id") ON DELETE SET NULL,
    "UsageType" VARCHAR(200),
    "CompanyName" VARCHAR(200),
    "IsEnglishPrimary" BOOLEAN NOT NULL DEFAULT TRUE,
    "TotalSoldQty" DECIMAL NOT NULL DEFAULT 0,
    "TotalPurchasedQty" DECIMAL NOT NULL DEFAULT 0,
    "TotalReturnedQty" DECIMAL NOT NULL DEFAULT 0,
    "Notes" VARCHAR(1000),
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_Products_OrgId_ProductCode" ON "Products" ("OrgId", "ProductCode");
CREATE INDEX IF NOT EXISTS "IX_Products_OrgId_SearchName" ON "Products" ("OrgId", "SearchName");
CREATE INDEX IF NOT EXISTS "IX_Products_OrgId_CompanyName" ON "Products" ("OrgId", "CompanyName");

-- Product Batches (barcode, pricing, units)
CREATE TABLE IF NOT EXISTS "ProductBatches" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "ProductId" UUID NOT NULL REFERENCES "Products"("Id") ON DELETE CASCADE,
    "Barcode" VARCHAR(100) NOT NULL,
    "ExpiryDate" TIMESTAMPTZ,
    "PharmacyBuyPrice" DECIMAL NOT NULL DEFAULT 0,
    "PublicSalePrice" DECIMAL NOT NULL DEFAULT 0,
    "IsTaxable" BOOLEAN NOT NULL DEFAULT FALSE,
    "TaxPercent" DECIMAL NOT NULL DEFAULT 0,
    "PriceAfterTax" DECIMAL NOT NULL DEFAULT 0,
    "MaxDiscount" DECIMAL NOT NULL DEFAULT 0,
    "MinQtyAlert" DECIMAL NOT NULL DEFAULT 0,
    "MainUnitId" UUID NOT NULL,
    "MainUnitQty" INTEGER NOT NULL DEFAULT 1,
    "SaleUnitId" UUID NOT NULL,
    "PurchaseUnitId" UUID NOT NULL,
    "CreatedByEmployeeId" UUID,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_ProductBatches_OrgId_Barcode" ON "ProductBatches" ("OrgId", "Barcode");
CREATE INDEX IF NOT EXISTS "IX_ProductBatches_OrgId_ProductId" ON "ProductBatches" ("OrgId", "ProductId");
CREATE INDEX IF NOT EXISTS "IX_ProductBatches_ExpiryDate" ON "ProductBatches" ("ExpiryDate");

-- Batch Stock (per branch)
CREATE TABLE IF NOT EXISTS "BatchStocks" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "BatchId" UUID NOT NULL REFERENCES "ProductBatches"("Id") ON DELETE CASCADE,
    "BranchId" UUID NOT NULL REFERENCES "Branches"("Id") ON DELETE RESTRICT,
    "WarehouseId" UUID REFERENCES "Warehouses"("Id") ON DELETE SET NULL,
    "Quantity" DECIMAL NOT NULL DEFAULT 0,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_BatchStocks_OrgId_BatchId_BranchId" ON "BatchStocks" ("OrgId", "BatchId", "BranchId");

-- Batch Units (additional units per batch)
CREATE TABLE IF NOT EXISTS "BatchUnits" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "BatchId" UUID NOT NULL REFERENCES "ProductBatches"("Id") ON DELETE CASCADE,
    "UnitId" UUID NOT NULL,
    "UnitName" VARCHAR(100) NOT NULL,
    "QtyPerMainUnit" INTEGER NOT NULL DEFAULT 1,
    "UnitSalePrice" DECIMAL NOT NULL DEFAULT 0,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_BatchUnits_BatchId_UnitId" ON "BatchUnits" ("BatchId", "UnitId");

-- Promotions
CREATE TABLE IF NOT EXISTS "Promotions" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "Name" VARCHAR(300) NOT NULL,
    "Description" VARCHAR(1000),
    "Type" INTEGER NOT NULL DEFAULT 0,
    "StartDate" TIMESTAMPTZ NOT NULL,
    "EndDate" TIMESTAMPTZ NOT NULL,
    "BranchId" UUID REFERENCES "Branches"("Id") ON DELETE SET NULL,
    "Priority" INTEGER NOT NULL DEFAULT 0,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE INDEX IF NOT EXISTS "IX_Promotions_OrgId_Dates" ON "Promotions" ("OrgId", "StartDate", "EndDate");

-- Promotion Rules
CREATE TABLE IF NOT EXISTS "PromotionRules" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "PromotionId" UUID NOT NULL REFERENCES "Promotions"("Id") ON DELETE CASCADE,
    "ConditionType" INTEGER NOT NULL DEFAULT 0,
    "ConditionProductId" UUID,
    "ConditionCategoryId" UUID,
    "ConditionBrand" VARCHAR(200),
    "BuyQty" INTEGER NOT NULL DEFAULT 1,
    "RewardType" INTEGER NOT NULL DEFAULT 0,
    "RewardProductId" UUID,
    "RewardQty" INTEGER NOT NULL DEFAULT 1,
    "DiscountPercent" DECIMAL,
    "DiscountAmount" DECIMAL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);

-- Seller Bonuses
CREATE TABLE IF NOT EXISTS "SellerBonuses" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "BatchId" UUID NOT NULL REFERENCES "ProductBatches"("Id") ON DELETE CASCADE,
    "BonusPerMainUnit" DECIMAL NOT NULL DEFAULT 0,
    "IsPercentage" BOOLEAN NOT NULL DEFAULT FALSE,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE UNIQUE INDEX IF NOT EXISTS "IX_SellerBonuses_OrgId_BatchId" ON "SellerBonuses" ("OrgId", "BatchId");

-- Employee Bonus Logs
CREATE TABLE IF NOT EXISTS "EmployeeBonusLogs" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "OrgId" UUID NOT NULL REFERENCES "Organizations"("Id") ON DELETE CASCADE,
    "BranchId" UUID,
    "EmployeeId" UUID NOT NULL REFERENCES "Employees"("Id") ON DELETE CASCADE,
    "BatchId" UUID NOT NULL REFERENCES "ProductBatches"("Id") ON DELETE CASCADE,
    "SaleId" UUID,
    "QtySold" DECIMAL NOT NULL DEFAULT 0,
    "UnitId" UUID NOT NULL,
    "BonusAmount" DECIMAL NOT NULL DEFAULT 0,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMPTZ
);
CREATE INDEX IF NOT EXISTS "IX_EmployeeBonusLogs_OrgId_Employee_Date" ON "EmployeeBonusLogs" ("OrgId", "EmployeeId", "CreatedAt");
CREATE INDEX IF NOT EXISTS "IX_EmployeeBonusLogs_OrgId_BatchId" ON "EmployeeBonusLogs" ("OrgId", "BatchId");

-- ========================================================
-- Done! 12 tables created.
-- ========================================================
