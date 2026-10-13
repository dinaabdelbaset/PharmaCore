using System;
using System.Collections.Generic;

namespace PharmaCore.POS.Services
{
    internal static class CurrentEmployeeSession
    {
        public static Guid EmployeeId { get; private set; }
        public static string EmployeeCode { get; private set; } = string.Empty;
        public static string FullName { get; private set; } = string.Empty;
        public static string Role { get; private set; } = string.Empty;
        public static Guid OrgId { get; private set; }
        public static Guid BranchId { get; private set; }
        public static Guid? TenantId { get; private set; }
        public static Guid? RegionId { get; private set; }
        public static bool IsSystemRole { get; private set; }
        public static DateTime LoginAtUtc { get; private set; }
        public static HashSet<string> Permissions { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

        public static void Set(EmployeeAuthDb.Result r)
        {
            EmployeeId = r.EmployeeId;
            EmployeeCode = r.EmployeeCode;
            FullName = r.FullName;
            Role = r.Role;
            IsSystemRole = r.IsSystemRole;
            OrgId = r.OrgId;
            BranchId = r.BranchId ?? Guid.Empty;
            TenantId = r.TenantId;
            RegionId = r.RegionId;
            LoginAtUtc = DateTime.UtcNow;
            Permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Failsafe: if the user is somehow 'Staff' but they are the ONLY user in the system, they are the owner.
            if (!IsSystemRole)
            {
                try
                {
                    var count = GetEmployeeCountSync();
                    if (count == 1) IsSystemRole = true;
                }
                catch { } // Ignore DB errors, just fallback
            }
        }

        private static int GetEmployeeCountSync()
        {
            try
            {
                var csProvider = new ConnectionStringProvider();
                var cs = csProvider.GetConnectionString();
                if (string.IsNullOrWhiteSpace(cs)) return -1;
                using var con = new Npgsql.NpgsqlConnection(cs);
                con.Open();
                using var cmd = new Npgsql.NpgsqlCommand("SELECT COUNT(*) FROM \"Employees\"", con);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return -1; }
        }

        public static void SetPermissions(string[]? perms)
        {
            Permissions = perms != null
                ? new HashSet<string>(perms, StringComparer.OrdinalIgnoreCase)
                : new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public static bool HasPermission(string permission)
        {
            // Admin/Manager always has full access
            var r = Role?.Trim().ToLowerInvariant() ?? "";
            if (IsSystemRole || r == "admin" || r == "manager" || r == "مدير" || r == "owner" || r == "اونر" || r == "مالك")
                return true;

            return Permissions.Contains(permission);
        }

        public static void Clear()
        {
            EmployeeId = Guid.Empty; EmployeeCode = string.Empty; FullName = string.Empty; Role = string.Empty; OrgId = Guid.Empty; BranchId = Guid.Empty; TenantId = null; RegionId = null; LoginAtUtc = default; IsSystemRole = false;
            Permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
