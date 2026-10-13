using System;
using System.Threading.Tasks;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services
{
    public class EmployeeAuthDb
    {
        public sealed class Result
        {
            public bool Ok { get; set; }
            public string Reason { get; set; } = string.Empty;
            public Guid EmployeeId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public Guid OrgId { get; set; }
            public Guid? TenantId { get; set; }
            public Guid? RegionId { get; set; }
            public Guid? BranchId { get; set; }
            public string Role { get; set; } = "";
            public bool IsSystemRole { get; set; }
        }

        public static async Task<Result> AuthenticateAsync(string cs, Guid orgId, Guid branchId, string employeeCode, string pin)
        {
            var res = new Result();
            if (orgId == Guid.Empty || branchId == Guid.Empty){ res.Reason = "بيانات الفرع/المنظمة غير صحيحة"; return res; }
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT e.\"Id\", e.\"PINHash\", e.\"FullName\", e.\"IsActive\", e.\"TenantId\", e.\"RegionId\", e.\"BranchId\", COALESCE(r.\"Name\", e.\"Role\") AS \"RoleName\", r.\"IsSystemRole\" FROM \"Employees\" e LEFT JOIN \"EmployeeRoles\" r ON e.\"RoleId\" = r.\"Id\" WHERE e.\"OrgId\"=@o AND lower(e.\"EmployeeCode\")=lower(@c)", con);
            cmd.Parameters.AddWithValue("o", orgId);
            cmd.Parameters.AddWithValue("c", employeeCode);
            await using var rd = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.SingleRow);
            if(!await rd.ReadAsync()){ res.Reason = "الموظف غير موجود"; return res; }
            var empId = rd.GetGuid(0); var hash = rd.IsDBNull(1)? null : rd.GetString(1); var full = rd.IsDBNull(2)? string.Empty : rd.GetString(2); var active = !rd.IsDBNull(3) && rd.GetBoolean(3);
            var tenantId = rd.IsDBNull(4)? (Guid?)null : rd.GetGuid(4);
            var regionId = rd.IsDBNull(5)? (Guid?)null : rd.GetGuid(5);
            var empBranchId = rd.IsDBNull(6)? (Guid?)null : rd.GetGuid(6);
            var role = rd.IsDBNull(7)? string.Empty : rd.GetString(7);
            var isSystemRole = !rd.IsDBNull(8) && rd.GetBoolean(8);
            if (!isSystemRole)
            {
                var r = role?.Trim().ToLowerInvariant() ?? "";
                if (r == "admin" || r == "manager" || r == "مدير" || r == "owner" || r == "اونر" || r == "مالك")
                {
                    isSystemRole = true;
                }
            }
            rd.Close();
            if(!active){ res.Reason = "حساب الموظف غير مفعل"; return res; }
            if(string.IsNullOrWhiteSpace(hash) || !VerifyPin(pin, hash)){ res.Reason = "الرقم السري غير صحيح"; return res; }
            // check branch access
            var cmdAcc = new NpgsqlCommand("SELECT COUNT(1) FROM \"EmployeeBranchAccesses\" WHERE \"EmployeeId\"=@e AND \"BranchId\"=@b AND \"IsActive\"=true", con);
            cmdAcc.Parameters.AddWithValue("e", empId); cmdAcc.Parameters.AddWithValue("b", branchId);
            var allowed = Convert.ToInt64(await cmdAcc.ExecuteScalarAsync()) > 0;
            if(!allowed){ res.Reason = "غير مسموح بهذا الفرع"; return res; }
            res.Ok = true; res.EmployeeId = empId; res.EmployeeCode = employeeCode; res.FullName = full; res.OrgId = orgId; res.TenantId = tenantId; res.RegionId = regionId; res.BranchId = empBranchId ?? branchId; res.Role = role; res.IsSystemRole = isSystemRole; res.Reason = "نجاح"; return res;
        }

        // We reuse Argon2Hasher for PIN hashing
        private static bool VerifyPin(string pin, string stored)
        {
            try
            {
                if (stored.StartsWith("$argon2", StringComparison.Ordinal))
                    return Security.Argon2Hasher.Verify(pin, stored);
                if (stored.StartsWith("pbkdf2:"))
                {
                    var seg = stored.Split(':'); if (seg.Length != 4) return false;
                    int iterations = int.Parse(seg[1]); var salt = Convert.FromBase64String(seg[2]); var keyStored = Convert.FromBase64String(seg[3]);
                    using var pbkdf2 = new Rfc2898DeriveBytes(pin, salt, iterations, HashAlgorithmName.SHA512); var keyCalc = pbkdf2.GetBytes(keyStored.Length);
                    return CryptographicOperations.FixedTimeEquals(keyStored, keyCalc);
                }
                return false;
            }
            catch { return false; }
        }
    }
}
