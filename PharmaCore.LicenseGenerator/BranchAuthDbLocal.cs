using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.ActivationManager
{
    internal static class BranchAuthDbLocal
    {
        public sealed class DeviceInfo
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string UserName { get; set; } = string.Empty;
            public string HwidHash { get; set; } = string.Empty;
            public string? LastIp { get; set; }
            public DateTime? LastLoginAt { get; set; }
            public DateTime? LastHeartbeatAt { get; set; }
            public bool IsActive { get; set; }
            public int AllowedDevices { get; set; }
            public int TotalActiveSeconds { get; set; }
        }

        public static async Task EnsureMonitoringSchemaAsync(string cs)
        {
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            // Create base tables if they do not exist (mirrors original schema)
            var create = @"CREATE TABLE IF NOT EXISTS ""BranchUsers""(
                            ""Id"" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
                            ""OrgId"" uuid NOT NULL,
                            ""BranchId"" uuid NOT NULL,
                            ""UserName"" text NOT NULL,
                            ""PasswordHash"" text NOT NULL,
                            ""IsActive"" boolean NOT NULL DEFAULT true,
                            ""AllowedDevices"" int NOT NULL DEFAULT 1,
                            ""CreatedAt"" timestamptz NOT NULL DEFAULT now());
                           CREATE UNIQUE INDEX IF NOT EXISTS ux_branch_user ON ""BranchUsers""(""OrgId"",""BranchId"",lower(""UserName""));
                           CREATE TABLE IF NOT EXISTS ""BranchUserDevices""(
                            ""Id"" uuid PRIMARY KEY DEFAULT gen_random_uuid(),
                            ""BranchUserId"" uuid NOT NULL REFERENCES ""BranchUsers""(""Id"") ON DELETE CASCADE,
                            ""HwidHash"" text NOT NULL,
                            ""HwidSig"" text NULL,
                            ""LastLoginAt"" timestamptz NULL,
                            ""LastIp"" text NULL,
                            ""CreatedAt"" timestamptz NOT NULL DEFAULT now());
                           CREATE UNIQUE INDEX IF NOT EXISTS ux_branch_hwid ON ""BranchUserDevices""(""BranchUserId"",""HwidHash"");";
            await using (var cmdCreate = new NpgsqlCommand(create, con)) { await cmdCreate.ExecuteNonQueryAsync(); }
            // Add monitoring columns if missing
            var alter = @"ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""LastHeartbeatAt"" timestamptz NULL;
ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""TotalActiveSeconds"" int NOT NULL DEFAULT 0;
ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""IsActive"" boolean NOT NULL DEFAULT true;";
            await using var cmdAlter = new NpgsqlCommand(alter, con);
            await cmdAlter.ExecuteNonQueryAsync();
        }

        public static async Task<IReadOnlyList<DeviceInfo>> GetDevicesAsync(string cs, Guid orgId, Guid branchId)
        {
            var list = new List<DeviceInfo>();
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = @"SELECT d.""Id"", u.""Id"", u.""UserName"", d.""HwidHash"", d.""LastIp"", d.""LastLoginAt"", d.""LastHeartbeatAt"", d.""IsActive"", u.""AllowedDevices"", d.""TotalActiveSeconds""
FROM ""BranchUserDevices"" d
JOIN ""BranchUsers"" u ON d.""BranchUserId""=u.""Id""
WHERE u.""OrgId""=@o AND u.""BranchId""=@b
ORDER BY d.""LastLoginAt"" DESC NULLS LAST";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("o", orgId);
            cmd.Parameters.AddWithValue("b", branchId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new DeviceInfo
                {
                    Id = rdr.GetGuid(0),
                    UserId = rdr.GetGuid(1),
                    UserName = rdr.GetString(2),
                    HwidHash = rdr.GetString(3),
                    LastIp = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                    LastLoginAt = rdr.IsDBNull(5) ? null : rdr.GetDateTime(5),
                    LastHeartbeatAt = rdr.IsDBNull(6) ? null : rdr.GetDateTime(6),
                    IsActive = rdr.IsDBNull(7) ? true : rdr.GetBoolean(7),
                    AllowedDevices = rdr.GetInt32(8),
                    TotalActiveSeconds = rdr.GetInt32(9)
                });
            }
            return list;
        }

        public static async Task SetDeviceActiveAsync(string cs, Guid deviceId, bool active)
        {
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = "UPDATE \"BranchUserDevices\" SET \"IsActive\"=@a WHERE \"Id\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("a", active);
            cmd.Parameters.AddWithValue("id", deviceId);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task UpdateHeartbeatAsync(string cs, Guid deviceId, int seconds)
        {
            await using var con = new NpgsqlConnection(cs);
            await con.OpenAsync();
            var sql = "UPDATE \"BranchUserDevices\" SET \"LastHeartbeatAt\"=now(), \"TotalActiveSeconds\"=\"TotalActiveSeconds\"+@s WHERE \"Id\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("s", seconds);
            cmd.Parameters.AddWithValue("id", deviceId);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}