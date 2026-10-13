CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Organizations" (
        "Id" uuid NOT NULL,
        "Code" character varying(50) NOT NULL,
        "Name" character varying(200) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Organizations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Tenants" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "Code" character varying(30) NOT NULL,
        "Name" character varying(150) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Tenants" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Tenants_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Regions" (
        "Id" uuid NOT NULL,
        "TenantId" uuid NOT NULL,
        "Code" character varying(30) NOT NULL,
        "Name" character varying(150) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Regions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Regions_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Branches" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "RegionId" uuid NOT NULL,
        "Code" character varying(40) NOT NULL,
        "Name" character varying(150) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Branches" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Branches_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Branches_Regions_RegionId" FOREIGN KEY ("RegionId") REFERENCES "Regions" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "ActivationCodes" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "TenantId" uuid,
        "RegionId" uuid,
        "BranchId" uuid,
        "CodePlain" character varying(32) NOT NULL,
        "CodeHash" character varying(128),
        "MaxDevices" integer NOT NULL DEFAULT 1,
        "UsedDevicesCount" integer NOT NULL DEFAULT 0,
        "ExpiresAt" timestamp with time zone,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_ActivationCodes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_ActivationCodes_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_ActivationCodes_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_ActivationCodes_Regions_RegionId" FOREIGN KEY ("RegionId") REFERENCES "Regions" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_ActivationCodes_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("Id") ON DELETE SET NULL
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "BranchAppAccounts" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid NOT NULL,
        "Username" character varying(80) NOT NULL,
        "PasswordHash" character varying(200) NOT NULL,
        "MaxConcurrentSessions" integer NOT NULL DEFAULT 1,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_BranchAppAccounts" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_BranchAppAccounts_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_BranchAppAccounts_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Devices" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "MachineId" character varying(120) NOT NULL,
        "DeviceToken" character varying(200) NOT NULL,
        "DisplayName" character varying(150) NOT NULL,
        "ActivatedAt" timestamp with time zone NOT NULL,
        "DeactivatedAt" timestamp with time zone,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Devices" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Devices_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Devices_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Employees" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "EmployeeCode" character varying(60) NOT NULL,
        "PINHash" character varying(200) NOT NULL,
        "FullName" character varying(150) NOT NULL,
        "JobTitle" text,
        "Grade" text,
        "TenantId" uuid,
        "RegionId" uuid,
        "BranchId" uuid,
        "Role" character varying(60) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Employees" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Employees_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Employees_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Employees_Regions_RegionId" FOREIGN KEY ("RegionId") REFERENCES "Regions" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Employees_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("Id") ON DELETE SET NULL
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "Users" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "UserCode" character varying(60) NOT NULL,
        "PasswordHash" character varying(200) NOT NULL,
        "DisplayName" character varying(150) NOT NULL,
        "Role" character varying(50) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Users_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE SET NULL,
        CONSTRAINT "FK_Users_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "BranchAppSessions" (
        "Id" uuid NOT NULL,
        "BranchAppAccountId" uuid NOT NULL,
        "DeviceId" uuid NOT NULL,
        "LoginAt" timestamp with time zone NOT NULL,
        "LogoutAt" timestamp with time zone,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_BranchAppSessions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_BranchAppSessions_BranchAppAccounts_BranchAppAccountId" FOREIGN KEY ("BranchAppAccountId") REFERENCES "BranchAppAccounts" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_BranchAppSessions_Devices_DeviceId" FOREIGN KEY ("DeviceId") REFERENCES "Devices" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE TABLE "EmployeeBranchAccesses" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "BranchId" uuid NOT NULL,
        "AccessType" integer NOT NULL,
        "AllowedFrom" timestamp with time zone,
        "AllowedTo" timestamp with time zone,
        "CreatedByUserId" uuid,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_EmployeeBranchAccesses" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeeBranchAccesses_Branches_BranchId" FOREIGN KEY ("BranchId") REFERENCES "Branches" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_EmployeeBranchAccesses_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_EmployeeBranchAccesses_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_ActivationCodes_BranchId" ON "ActivationCodes" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_ActivationCodes_CodePlain" ON "ActivationCodes" ("CodePlain");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_ActivationCodes_OrgId" ON "ActivationCodes" ("OrgId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_ActivationCodes_RegionId" ON "ActivationCodes" ("RegionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_ActivationCodes_TenantId" ON "ActivationCodes" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_BranchAppAccounts_BranchId_Username" ON "BranchAppAccounts" ("BranchId", "Username");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_BranchAppAccounts_OrgId" ON "BranchAppAccounts" ("OrgId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_BranchAppSessions_BranchAppAccountId_IsActive" ON "BranchAppSessions" ("BranchAppAccountId", "IsActive");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_BranchAppSessions_DeviceId" ON "BranchAppSessions" ("DeviceId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Branches_OrgId_Code" ON "Branches" ("OrgId", "Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Branches_RegionId" ON "Branches" ("RegionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Devices_BranchId" ON "Devices" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Devices_OrgId_DeviceToken" ON "Devices" ("OrgId", "DeviceToken");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Devices_OrgId_MachineId" ON "Devices" ("OrgId", "MachineId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_EmployeeBranchAccesses_BranchId" ON "EmployeeBranchAccesses" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_EmployeeBranchAccesses_EmployeeId" ON "EmployeeBranchAccesses" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_EmployeeBranchAccesses_OrgId_EmployeeId_BranchId" ON "EmployeeBranchAccesses" ("OrgId", "EmployeeId", "BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Employees_BranchId" ON "Employees" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Employees_OrgId_EmployeeCode" ON "Employees" ("OrgId", "EmployeeCode");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Employees_RegionId" ON "Employees" ("RegionId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Employees_TenantId" ON "Employees" ("TenantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Organizations_Code" ON "Organizations" ("Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Regions_TenantId_Code" ON "Regions" ("TenantId", "Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Tenants_OrgId_Code" ON "Tenants" ("OrgId", "Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE INDEX "IX_Users_BranchId" ON "Users" ("BranchId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    CREATE UNIQUE INDEX "IX_Users_OrgId_UserCode" ON "Users" ("OrgId", "UserCode");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251120194831_AddActivationCodes') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251120194831_AddActivationCodes', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260312204636_AddEmployeePermissions') THEN
    CREATE TABLE "EmployeePermissions" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "EmployeeId" uuid NOT NULL,
        "Permission" character varying(100) NOT NULL,
        "IsActive" boolean NOT NULL DEFAULT TRUE,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_EmployeePermissions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeePermissions_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_EmployeePermissions_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260312204636_AddEmployeePermissions') THEN
    CREATE INDEX "IX_EmployeePermissions_EmployeeId" ON "EmployeePermissions" ("EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260312204636_AddEmployeePermissions') THEN
    CREATE UNIQUE INDEX "IX_EmployeePermissions_OrgId_EmployeeId_Permission" ON "EmployeePermissions" ("OrgId", "EmployeeId", "Permission");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260312204636_AddEmployeePermissions') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260312204636_AddEmployeePermissions', '8.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    ALTER TABLE "Employees" ADD "RoleId" uuid;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE TABLE "AuditEvents" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "EmployeeId" uuid,
        "EmployeeCode" character varying(60),
        "EmployeeName" character varying(150),
        "EventType" character varying(50) NOT NULL,
        "Category" character varying(50) NOT NULL,
        "ScreenName" character varying(100),
        "TargetTable" character varying(100),
        "TargetId" character varying(100),
        "Description" character varying(1000) NOT NULL,
        "Severity" integer NOT NULL DEFAULT 0,
        "IpAddress" character varying(50),
        "DeviceName" character varying(150),
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_AuditEvents" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE TABLE "AuditSessions" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "BranchId" uuid,
        "EmployeeId" uuid NOT NULL,
        "EmployeeCode" character varying(60) NOT NULL,
        "EmployeeName" character varying(150) NOT NULL,
        "LoginAt" timestamp with time zone NOT NULL,
        "LogoutAt" timestamp with time zone,
        "DeviceName" character varying(150),
        "IpAddress" character varying(50),
        "Status" character varying(20) NOT NULL DEFAULT 'Active',
        CONSTRAINT "PK_AuditSessions" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE TABLE "EmployeeRoles" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "Name" text NOT NULL,
        "ParentRoleId" uuid,
        "IsSystemRole" boolean NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_EmployeeRoles" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_EmployeeRoles_EmployeeRoles_ParentRoleId" FOREIGN KEY ("ParentRoleId") REFERENCES "EmployeeRoles" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_EmployeeRoles_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE TABLE "AuditDataChanges" (
        "Id" uuid NOT NULL,
        "AuditEventId" uuid NOT NULL,
        "FieldName" character varying(100) NOT NULL,
        "FieldLabel" character varying(100),
        "OldValue" character varying(2000),
        "NewValue" character varying(2000),
        CONSTRAINT "PK_AuditDataChanges" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_AuditDataChanges_AuditEvents_AuditEventId" FOREIGN KEY ("AuditEventId") REFERENCES "AuditEvents" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE TABLE "RolePermissions" (
        "Id" uuid NOT NULL,
        "OrgId" uuid NOT NULL,
        "RoleId" uuid NOT NULL,
        "Permission" text NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "UpdatedAt" timestamp with time zone,
        CONSTRAINT "PK_RolePermissions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_RolePermissions_EmployeeRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "EmployeeRoles" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_RolePermissions_Organizations_OrgId" FOREIGN KEY ("OrgId") REFERENCES "Organizations" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_Employees_RoleId" ON "Employees" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditDataChanges_AuditEventId" ON "AuditDataChanges" ("AuditEventId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditEvents_OrgId_Category" ON "AuditEvents" ("OrgId", "Category");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditEvents_OrgId_CreatedAt" ON "AuditEvents" ("OrgId", "CreatedAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditEvents_OrgId_EmployeeId" ON "AuditEvents" ("OrgId", "EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditEvents_OrgId_EventType" ON "AuditEvents" ("OrgId", "EventType");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditEvents_OrgId_ScreenName" ON "AuditEvents" ("OrgId", "ScreenName");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditSessions_OrgId_EmployeeId" ON "AuditSessions" ("OrgId", "EmployeeId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_AuditSessions_OrgId_LoginAt" ON "AuditSessions" ("OrgId", "LoginAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_EmployeeRoles_OrgId" ON "EmployeeRoles" ("OrgId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_EmployeeRoles_ParentRoleId" ON "EmployeeRoles" ("ParentRoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_RolePermissions_OrgId" ON "RolePermissions" ("OrgId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    CREATE INDEX "IX_RolePermissions_RoleId" ON "RolePermissions" ("RoleId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    ALTER TABLE "Employees" ADD CONSTRAINT "FK_Employees_EmployeeRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "EmployeeRoles" ("Id") ON DELETE SET NULL;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260313130610_AddHierarchicalRBAC') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260313130610_AddHierarchicalRBAC', '8.0.5');
    END IF;
END $EF$;
COMMIT;

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

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321095018_AddConditionCollectionId') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260321095018_AddConditionCollectionId', '8.0.5');
    END IF;
END $EF$;
COMMIT;

