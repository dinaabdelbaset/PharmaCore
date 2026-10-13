using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using Npgsql;

namespace PharmaCore.ActivationManager
{
    /// <summary>
    /// Manages initial Employee + EmployeeBranchAccess records.
    /// Uses the SAME hash format as API's EmployeePinHasher (argon2id:v1:...) for compatibility.
    /// </summary>
    internal static class EmployeeAdmin
    {
        // Must match PharmaCore.API.Security.EmployeePinHasher format exactly
        private const string Prefix = "argon2id:v1";
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int MemoryKiB = 64 * 1024;
        private const int Iterations = 3;
        private const int Parallelism = 2;

        public sealed class EmployeeDto
        {
            public Guid Id { get; set; }
            public Guid OrgId { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? JobTitle { get; set; }
            public string Role { get; set; } = "Admin";
            public bool IsActive { get; set; } = true;
        }

        public sealed class EmployeeBranchAccessDto
        {
            public Guid Id { get; set; }
            public Guid EmployeeId { get; set; }
            public Guid BranchId { get; set; }
            public string? BranchName { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public static string HashPin(string pin)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(pin))
            {
                Salt = salt,
                MemorySize = MemoryKiB,
                Iterations = Iterations,
                DegreeOfParallelism = Parallelism
            };
            var hash = argon2.GetBytes(HashSize);
            return $"{Prefix}:m={MemoryKiB},t={Iterations},p={Parallelism}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public static async Task EnsureSchemaAsync(string cs)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            // Employees table (matches EF entity)
            var sql = @"CREATE TABLE IF NOT EXISTS ""Employees""(
                ""Id"" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
                ""OrgId"" uuid NOT NULL,
                ""EmployeeCode"" text NOT NULL,
                ""PINHash"" text NOT NULL DEFAULT '',
                ""FullName"" text NOT NULL DEFAULT '',
                ""JobTitle"" text NULL,
                ""Grade"" text NULL,
                ""TenantId"" uuid NULL,
                ""RegionId"" uuid NULL,
                ""BranchId"" uuid NULL,
                ""Role"" text NOT NULL DEFAULT 'Staff',
                ""IsActive"" boolean NOT NULL DEFAULT true,
                ""IsDeleted"" boolean NOT NULL DEFAULT false,
                ""CreatedAt"" timestamptz NOT NULL DEFAULT now(),
                ""CreatedBy"" text NULL,
                ""UpdatedAt"" timestamptz NULL,
                ""UpdatedBy"" text NULL,
                ""DeletedAt"" timestamptz NULL,
                ""DeletedBy"" text NULL);
                CREATE UNIQUE INDEX IF NOT EXISTS ux_emp_code ON ""Employees""(""OrgId"", ""EmployeeCode"");
                -- EmployeeBranchAccesses
                CREATE TABLE IF NOT EXISTS ""EmployeeBranchAccesses""(
                ""Id"" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
                ""OrgId"" uuid NOT NULL,
                ""EmployeeId"" uuid NOT NULL REFERENCES ""Employees""(""Id"") ON DELETE CASCADE,
                ""BranchId"" uuid NOT NULL,
                ""AccessType"" int NOT NULL DEFAULT 0,
                ""AllowedFrom"" timestamptz NULL,
                ""AllowedTo"" timestamptz NULL,
                ""IsActive"" boolean NOT NULL DEFAULT true,
                ""IsDeleted"" boolean NOT NULL DEFAULT false,
                ""CreatedAt"" timestamptz NOT NULL DEFAULT now(),
                ""CreatedBy"" text NULL,
                ""UpdatedAt"" timestamptz NULL,
                ""UpdatedBy"" text NULL,
                ""DeletedAt"" timestamptz NULL,
                ""DeletedBy"" text NULL);
                CREATE UNIQUE INDEX IF NOT EXISTS ux_emp_branch ON ""EmployeeBranchAccesses""(""OrgId"", ""EmployeeId"", ""BranchId"");";
            await using var cmd = new NpgsqlCommand(sql, con); await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<IReadOnlyList<EmployeeDto>> ListAsync(string cs, Guid orgId)
        {
            var list = new List<EmployeeDto>();
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"SELECT ""Id"", ""OrgId"", ""EmployeeCode"", ""FullName"", ""JobTitle"", ""Role"", ""IsActive"" FROM ""Employees"" WHERE ""OrgId""=@o AND ""IsDeleted""=false ORDER BY ""EmployeeCode""";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("o", orgId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new EmployeeDto
                {
                    Id = rdr.GetGuid(0), OrgId = rdr.GetGuid(1), EmployeeCode = rdr.GetString(2),
                    FullName = rdr.GetString(3), JobTitle = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                    Role = rdr.GetString(5), IsActive = rdr.GetBoolean(6)
                });
            }
            return list;
        }

        public static async Task<Guid> AddAsync(string cs, Guid orgId, string code, string fullName, string pin, string role, string? jobTitle)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var pinHash = HashPin(pin);
            var sql = @"INSERT INTO ""Employees""(""OrgId"", ""EmployeeCode"", ""PINHash"", ""FullName"", ""Role"", ""JobTitle"") VALUES(@o, @c, @p, @n, @r, @j) RETURNING ""Id""";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("o", orgId);
            cmd.Parameters.AddWithValue("c", code);
            cmd.Parameters.AddWithValue("p", pinHash);
            cmd.Parameters.AddWithValue("n", fullName);
            cmd.Parameters.AddWithValue("r", string.IsNullOrWhiteSpace(role) ? "Staff" : role);
            cmd.Parameters.AddWithValue("j", (object?)jobTitle ?? DBNull.Value);
            return (Guid)await cmd.ExecuteScalarAsync()!;
        }

        public static async Task UpdateAsync(string cs, Guid empId, string? fullName = null, string? pin = null, string? role = null, bool? active = null)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"UPDATE ""Employees"" SET
                ""FullName"" = COALESCE(@n, ""FullName""),
                ""PINHash"" = COALESCE(@p, ""PINHash""),
                ""Role"" = COALESCE(@r, ""Role""),
                ""IsActive"" = COALESCE(@a, ""IsActive""),
                ""UpdatedAt"" = now()
                WHERE ""Id"" = @id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", empId);
            cmd.Parameters.AddWithValue("n", (object?)fullName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p", pin != null ? (object)HashPin(pin) : DBNull.Value);
            cmd.Parameters.AddWithValue("r", (object?)role ?? DBNull.Value);
            cmd.Parameters.AddWithValue("a", active.HasValue ? (object)active.Value : DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }

        // Branch Access

        public static async Task<IReadOnlyList<EmployeeBranchAccessDto>> ListBranchAccessAsync(string cs, Guid empId)
        {
            var list = new List<EmployeeBranchAccessDto>();
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"SELECT a.""Id"", a.""EmployeeId"", a.""BranchId"", b.""Name"", a.""IsActive""
                        FROM ""EmployeeBranchAccesses"" a
                        JOIN ""Branches"" b ON b.""Id"" = a.""BranchId""
                        WHERE a.""EmployeeId"" = @e AND a.""IsDeleted"" = false ORDER BY b.""Name""";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("e", empId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new EmployeeBranchAccessDto
                {
                    Id = rdr.GetGuid(0), EmployeeId = rdr.GetGuid(1), BranchId = rdr.GetGuid(2),
                    BranchName = rdr.GetString(3), IsActive = rdr.GetBoolean(4)
                });
            }
            return list;
        }

        public static async Task GrantBranchAccessAsync(string cs, Guid orgId, Guid empId, Guid branchId)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"INSERT INTO ""EmployeeBranchAccesses""(""OrgId"", ""EmployeeId"", ""BranchId"", ""AccessType"", ""IsActive"")
                        VALUES(@o, @e, @b, 0, true)
                        ON CONFLICT (""OrgId"", ""EmployeeId"", ""BranchId"") DO UPDATE SET ""IsActive"" = true, ""IsDeleted"" = false, ""UpdatedAt"" = now()";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("o", orgId);
            cmd.Parameters.AddWithValue("e", empId);
            cmd.Parameters.AddWithValue("b", branchId);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task RevokeBranchAccessAsync(string cs, Guid accessId)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"UPDATE ""EmployeeBranchAccesses"" SET ""IsActive"" = false, ""UpdatedAt"" = now() WHERE ""Id"" = @id";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("id", accessId); await cmd.ExecuteNonQueryAsync();
        }
    }
}
