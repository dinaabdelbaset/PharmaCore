using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.ActivationManager
{
    internal static class BranchUserAdmin
    {
        public class BranchUserDto
        {
            public Guid Id { get; set; }
            public Guid OrgId { get; set; }
            public Guid BranchId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string? Role { get; set; }
            public bool IsActive { get; set; }
            public int AllowedDevices { get; set; }
            public int FailedCount { get; set; }
            public DateTime? LockoutUntil { get; set; }
        }

        public class BranchUserViewDto : BranchUserDto
        {
            public string? OrgName { get; set; }
            public string? TenantName { get; set; }
            public string? RegionName { get; set; }
            public string? BranchName { get; set; }
            public int DeviceCount { get; set; }
        }

        private static string HashPassword(string password)
        {
            return Security.Argon2Hasher.Hash(password);
        }

        public static async Task EnsureSchemaAsync(string cs)
        {
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = @"CREATE TABLE IF NOT EXISTS ""BranchUsers""(
                        ""Id"" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
                        ""OrgId"" uuid NOT NULL,
                        ""BranchId"" uuid NOT NULL,
                        ""UserName"" text NOT NULL,
                        ""PasswordHash"" text NOT NULL,
                        ""IsActive"" boolean NOT NULL DEFAULT true,
                        ""AllowedDevices"" int NOT NULL DEFAULT 1,
                        ""FailedCount"" int NOT NULL DEFAULT 0,
                        ""LockoutUntil"" timestamptz NULL,
                        ""CreatedAt"" timestamptz NOT NULL DEFAULT now());
                        CREATE UNIQUE INDEX IF NOT EXISTS ux_branch_user ON ""BranchUsers""(""OrgId"",""BranchId"", lower(""UserName""));";
            await using var cmd = new NpgsqlCommand(sql, con);
            await cmd.ExecuteNonQueryAsync();
            // Ensure new columns exist on previously created tables
            var alterCols = @"ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""FailedCount"" int NOT NULL DEFAULT 0;
                               ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""LockoutUntil"" timestamptz NULL;
                               ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""PasswordPlain"" text NULL;
                               ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""UpdatedAt"" timestamptz NULL;
                               ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""Role"" text NULL;";
            await using (var cmdAlter = new NpgsqlCommand(alterCols, con)) { await cmdAlter.ExecuteNonQueryAsync(); }
        }

        public static async Task<IReadOnlyList<BranchUserViewDto>> ListViewAsync(string cs, Guid orgId, Guid tenantId, Guid regionId, Guid branchId)
        {
            var list = new List<BranchUserViewDto>();
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = @"SELECT u.""Id"", u.""OrgId"", u.""BranchId"", u.""UserName"", u.""IsActive"", u.""AllowedDevices"", u.""FailedCount"", u.""LockoutUntil"",
                               o.""Name"" as OrgName, t.""Name"" as TenantName, r.""Name"" as RegionName, b.""Name"" as BranchName,
                               COALESCE(d.cnt,0) as DeviceCount, u.""Role""
                        FROM ""BranchUsers"" u
                        JOIN ""Branches"" b ON b.""Id"" = u.""BranchId""
                        JOIN ""Regions"" r ON r.""Id"" = b.""RegionId""
                        JOIN ""Tenants"" t ON t.""Id"" = r.""TenantId""
                        JOIN ""Organizations"" o ON o.""Id"" = u.""OrgId""
                        LEFT JOIN (SELECT ""BranchUserId"", COUNT(*) cnt FROM ""BranchUserDevices"" GROUP BY ""BranchUserId"") d ON d.""BranchUserId"" = u.""Id""
                        WHERE 1=1";
            await using var cmd = new NpgsqlCommand(); cmd.Connection = con;
            if (orgId != Guid.Empty) { sql += " AND u.\"OrgId\"=@o"; cmd.Parameters.AddWithValue("o", orgId); }
            if (tenantId != Guid.Empty) { sql += " AND t.\"Id\"=@t"; cmd.Parameters.AddWithValue("t", tenantId); }
            if (regionId != Guid.Empty) { sql += " AND r.\"Id\"=@r"; cmd.Parameters.AddWithValue("r", regionId); }
            if (branchId != Guid.Empty) { sql += " AND u.\"BranchId\"=@b"; cmd.Parameters.AddWithValue("b", branchId); }
            sql += " ORDER BY lower(u.\"UserName\")"; cmd.CommandText = sql;
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new BranchUserViewDto
                {
                    Id = rdr.GetGuid(0), OrgId = rdr.GetGuid(1), BranchId = rdr.GetGuid(2), UserName = rdr.GetString(3), IsActive = rdr.GetBoolean(4), AllowedDevices = rdr.GetInt32(5), FailedCount = rdr.GetInt32(6), LockoutUntil = rdr.IsDBNull(7)? null : rdr.GetDateTime(7),
                    OrgName = rdr.GetString(8), TenantName = rdr.GetString(9), RegionName = rdr.GetString(10), BranchName = rdr.GetString(11), DeviceCount = rdr.GetInt32(12),
                    Role = rdr.IsDBNull(13) ? null : rdr.GetString(13)
                });
            }
            return list;
        }

        public static async Task<Guid> AddAsync(string cs, Guid orgId, Guid branchId, string user, string pass, int allowed, string? role = null)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var newId = Guid.NewGuid();
            var sql = "INSERT INTO \"BranchUsers\"(\"Id\",\"OrgId\",\"BranchId\",\"UserName\",\"PasswordHash\",\"AllowedDevices\",\"PasswordPlain\",\"CreatedAt\",\"Role\") VALUES(@id,@o,@b,@u,@p,@al,@pp,@ca,@rl) RETURNING \"Id\"";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", newId);
            cmd.Parameters.AddWithValue("o", orgId); cmd.Parameters.AddWithValue("b", branchId); cmd.Parameters.AddWithValue("u", user); cmd.Parameters.AddWithValue("p", HashPassword(pass)); cmd.Parameters.AddWithValue("al", allowed); cmd.Parameters.AddWithValue("pp", pass); cmd.Parameters.AddWithValue("ca", DateTime.UtcNow); cmd.Parameters.AddWithValue("rl", (object?)role ?? DBNull.Value);
            return (Guid)await cmd.ExecuteScalarAsync()!;
        }

        public static async Task UpdateAsync(string cs, Guid id, string? user = null, string? pass = null, int? allowed = null, bool? active = null, string? role = null)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "UPDATE \"BranchUsers\" SET \"UserName\"=COALESCE(@u,\"UserName\"), \"PasswordHash\"=COALESCE(@p,\"PasswordHash\"), \"AllowedDevices\"=COALESCE(@al,\"AllowedDevices\"), \"IsActive\"=COALESCE(@ac,\"IsActive\"), \"PasswordPlain\"=COALESCE(@pp,\"PasswordPlain\"), \"Role\"=@rl WHERE \"Id\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id); cmd.Parameters.AddWithValue("u", (object?)user ?? DBNull.Value); cmd.Parameters.AddWithValue("p", pass == null ? (object)DBNull.Value : HashPassword(pass)); cmd.Parameters.AddWithValue("al", allowed.HasValue ? allowed.Value : (object)DBNull.Value); cmd.Parameters.AddWithValue("ac", active.HasValue ? active.Value : (object)DBNull.Value); cmd.Parameters.AddWithValue("pp", pass == null ? (object)DBNull.Value : pass); cmd.Parameters.AddWithValue("rl", (object?)role ?? DBNull.Value); await cmd.ExecuteNonQueryAsync();
        }

        public static async Task ResetDevicesAsync(string cs, Guid userId)
        {
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = "DELETE FROM \"BranchUserDevices\" WHERE \"BranchUserId\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", userId);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<string?> GetPlainPasswordAsync(string cs, Guid userId)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT \"PasswordPlain\" FROM \"BranchUsers\" WHERE \"Id\"=@id", con); cmd.Parameters.AddWithValue("id", userId);
            var r = await cmd.ExecuteScalarAsync(); return r as string;
        }

        /// <summary>Lists available roles from EmployeeRoles table for a given org.</summary>
        public static async Task<List<string>> ListRolesAsync(string cs, Guid orgId)
        {
            var roles = new List<string>();
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "SELECT \"Name\" FROM \"EmployeeRoles\" WHERE \"OrgId\"=@o AND \"IsActive\"=true ORDER BY \"SortOrder\"";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("o", orgId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync()) roles.Add(rdr.GetString(0));
            return roles;
        }
    }
}