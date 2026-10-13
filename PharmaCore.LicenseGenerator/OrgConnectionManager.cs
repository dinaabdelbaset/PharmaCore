using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.ActivationManager
{
    /// <summary>
    /// Manages per-organization connection strings stored in a central control database.
    /// The control DB uses a "OrgConnectionStrings" table mapping OrgId → ConnectionString.
    /// All other projects resolve connection strings via this lookup.
    /// </summary>
    internal static class OrgConnectionManager
    {
        // In-memory cache (populated on load)
        private static readonly ConcurrentDictionary<Guid, string> _cache = new();

        public sealed class OrgConnectionDto
        {
            public Guid OrgId { get; set; }
            public string? OrgName { get; set; }
            public string ConnectionString { get; set; } = string.Empty;
            public string? Description { get; set; }
            public bool IsActive { get; set; } = true;
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }

        /// <summary>
        /// Creates the OrgConnectionStrings table in the control database if it doesn't exist.
        /// </summary>
        public static async Task EnsureSchemaAsync(string controlCs)
        {
            await using var con = new NpgsqlConnection(controlCs); await con.OpenAsync();
            var sql = @"CREATE TABLE IF NOT EXISTS ""OrgConnectionStrings""(
                ""OrgId"" uuid PRIMARY KEY,
                ""ConnectionString"" text NOT NULL,
                ""Description"" text NULL,
                ""IsActive"" boolean NOT NULL DEFAULT true,
                ""CreatedAt"" timestamptz NOT NULL DEFAULT now(),
                ""UpdatedAt"" timestamptz NULL);";
            await using var cmd = new NpgsqlCommand(sql, con); await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Lists all org → connection string mappings with org names.
        /// </summary>
        public static async Task<IReadOnlyList<OrgConnectionDto>> ListAsync(string controlCs)
        {
            var list = new List<OrgConnectionDto>();
            await using var con = new NpgsqlConnection(controlCs); await con.OpenAsync();
            var sql = @"SELECT oc.""OrgId"", o.""Name"", oc.""ConnectionString"", oc.""Description"", oc.""IsActive"", oc.""CreatedAt"", oc.""UpdatedAt""
                        FROM ""OrgConnectionStrings"" oc
                        LEFT JOIN ""Organizations"" o ON o.""Id"" = oc.""OrgId""
                        ORDER BY o.""Name"" NULLS LAST";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                var dto = new OrgConnectionDto
                {
                    OrgId = rdr.GetGuid(0),
                    OrgName = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                    ConnectionString = rdr.GetString(2),
                    Description = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                    IsActive = rdr.GetBoolean(4),
                    CreatedAt = rdr.GetDateTime(5),
                    UpdatedAt = rdr.IsDBNull(6) ? null : rdr.GetDateTime(6)
                };
                list.Add(dto);
                _cache[dto.OrgId] = dto.ConnectionString;
            }
            return list;
        }

        /// <summary>
        /// Get connection string for a specific org. Uses cache first, then DB.
        /// </summary>
        public static async Task<string?> GetAsync(string controlCs, Guid orgId)
        {
            if (_cache.TryGetValue(orgId, out var cached)) return cached;
            await using var con = new NpgsqlConnection(controlCs); await con.OpenAsync();
            var sql = @"SELECT ""ConnectionString"" FROM ""OrgConnectionStrings"" WHERE ""OrgId"" = @o AND ""IsActive"" = true";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("o", orgId);
            var result = await cmd.ExecuteScalarAsync() as string;
            if (result != null) _cache[orgId] = result;
            return result;
        }

        /// <summary>
        /// Upsert org → connection string mapping.
        /// </summary>
        public static async Task UpsertAsync(string controlCs, Guid orgId, string connectionString, string? description = null)
        {
            await using var con = new NpgsqlConnection(controlCs); await con.OpenAsync();
            var sql = @"INSERT INTO ""OrgConnectionStrings""(""OrgId"", ""ConnectionString"", ""Description"")
                        VALUES(@o, @cs, @d)
                        ON CONFLICT (""OrgId"") DO UPDATE SET ""ConnectionString"" = @cs, ""Description"" = COALESCE(@d, ""OrgConnectionStrings"".""Description""), ""UpdatedAt"" = now()";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("o", orgId);
            cmd.Parameters.AddWithValue("cs", connectionString);
            cmd.Parameters.AddWithValue("d", (object?)description ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
            _cache[orgId] = connectionString;
        }

        /// <summary>
        /// Remove mapping.
        /// </summary>
        public static async Task RemoveAsync(string controlCs, Guid orgId)
        {
            _cache.TryRemove(orgId, out _);
            await using var con = new NpgsqlConnection(controlCs); await con.OpenAsync();
            var sql = @"DELETE FROM ""OrgConnectionStrings"" WHERE ""OrgId"" = @o";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("o", orgId); await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Preload all active mappings into cache.
        /// </summary>
        public static async Task PreloadCacheAsync(string controlCs)
        {
            await ListAsync(controlCs);
        }

        /// <summary>
        /// Clear the in-memory cache.
        /// </summary>
        public static void ClearCache() => _cache.Clear();
    }
}
