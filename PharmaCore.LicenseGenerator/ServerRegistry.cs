using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.ActivationManager
{
    /// <summary>
    /// Central Server Registry — manages connections to all PharmaCore database servers.
    /// Stores server credentials in the control database, provides health checking,
    /// and acts as the single source of truth for multi-server operations.
    /// </summary>
    public static class ServerRegistry
    {
        // ───────── Data Model ─────────

        public sealed class ServerEntry
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string ServerName { get; set; } = string.Empty;
            public string Host { get; set; } = string.Empty;
            public int Port { get; set; } = 5432;
            public string DatabaseName { get; set; } = string.Empty;
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string SslMode { get; set; } = "Prefer";
            public string? Description { get; set; }
            public bool IsActive { get; set; } = true;
            public DateTime? LastHealthCheck { get; set; }
            public string? LastHealthStatus { get; set; }  // "Online", "Offline", "Slow", "Error"
            public string? ServerVersion { get; set; }
            public int? LatencyMs { get; set; }

            // Aggregated stats (populated on demand)
            public int OrgCount { get; set; }
            public int BranchCount { get; set; }
            public int DeviceCount { get; set; }
            public int ActiveCodeCount { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }

            public string BuildConnectionString()
            {
                var b = new NpgsqlConnectionStringBuilder
                {
                    Host = Host,
                    Port = Port,
                    Database = DatabaseName,
                    Username = Username,
                    Password = Password,
                    Timeout = 10,
                    CommandTimeout = 30
                };
                if (!string.IsNullOrEmpty(SslMode) && SslMode != "Disable")
                {
                    b.SslMode = Enum.TryParse<Npgsql.SslMode>(SslMode, true, out var mode) ? mode : Npgsql.SslMode.Prefer;
                    b.TrustServerCertificate = true;
                }
                return b.ConnectionString;
            }

            public string DisplayLabel => $"{ServerName}  ({Host}:{Port}/{DatabaseName})";

            public override string ToString() => DisplayLabel;
        }

        public sealed class HealthResult
        {
            public bool IsOnline { get; set; }
            public string Status { get; set; } = "Unknown";
            public int LatencyMs { get; set; }
            public string? ServerVersion { get; set; }
            public string? ErrorMessage { get; set; }
        }

        // ───────── Schema ─────────

        private const string CreateTableSql = @"
CREATE TABLE IF NOT EXISTS ""ServerRegistry"" (
    ""Id""               uuid PRIMARY KEY,
    ""ServerName""        varchar(200) NOT NULL,
    ""Host""              varchar(200) NOT NULL,
    ""Port""              int NOT NULL DEFAULT 5432,
    ""DatabaseName""      varchar(200) NOT NULL,
    ""Username""          varchar(100) NOT NULL,
    ""Password""          varchar(500) NOT NULL,
    ""SslMode""           varchar(20) NOT NULL DEFAULT 'Prefer',
    ""Description""       varchar(1000),
    ""IsActive""          boolean NOT NULL DEFAULT true,
    ""LastHealthCheck""   timestamptz,
    ""LastHealthStatus""  varchar(20),
    ""ServerVersion""     varchar(50),
    ""LatencyMs""         int,
    ""CreatedAt""         timestamptz NOT NULL DEFAULT now(),
    ""UpdatedAt""         timestamptz
);
CREATE UNIQUE INDEX IF NOT EXISTS ""IX_ServerRegistry_Name"" ON ""ServerRegistry""(""ServerName"");
CREATE INDEX IF NOT EXISTS ""IX_ServerRegistry_Host"" ON ""ServerRegistry""(""Host"", ""Port"", ""DatabaseName"");
";

        public static async Task EnsureSchemaAsync(string controlCs)
        {
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();
            await using var cmd = new NpgsqlCommand(CreateTableSql, con);
            await cmd.ExecuteNonQueryAsync();
        }

        // ───────── CRUD ─────────

        public static async Task<List<ServerEntry>> ListAsync(string controlCs, bool includeInactive = false)
        {
            await EnsureSchemaAsync(controlCs);
            var list = new List<ServerEntry>();
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();

            var filter = includeInactive ? "" : @"WHERE ""IsActive"" = true";
            var sql = $@"SELECT ""Id"",""ServerName"",""Host"",""Port"",""DatabaseName"",""Username"",""Password"",
                ""SslMode"",""Description"",""IsActive"",""LastHealthCheck"",""LastHealthStatus"",
                ""ServerVersion"",""LatencyMs"",""CreatedAt"",""UpdatedAt""
                FROM ""ServerRegistry"" {filter} ORDER BY ""ServerName""";

            await using var cmd = new NpgsqlCommand(sql, con);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new ServerEntry
                {
                    Id = rdr.GetGuid(0),
                    ServerName = rdr.GetString(1),
                    Host = rdr.GetString(2),
                    Port = rdr.GetInt32(3),
                    DatabaseName = rdr.GetString(4),
                    Username = rdr.GetString(5),
                    Password = rdr.GetString(6),
                    SslMode = rdr.GetString(7),
                    Description = rdr.IsDBNull(8) ? null : rdr.GetString(8),
                    IsActive = rdr.GetBoolean(9),
                    LastHealthCheck = rdr.IsDBNull(10) ? null : rdr.GetDateTime(10),
                    LastHealthStatus = rdr.IsDBNull(11) ? null : rdr.GetString(11),
                    ServerVersion = rdr.IsDBNull(12) ? null : rdr.GetString(12),
                    LatencyMs = rdr.IsDBNull(13) ? null : rdr.GetInt32(13),
                    CreatedAt = rdr.GetDateTime(14),
                    UpdatedAt = rdr.IsDBNull(15) ? null : rdr.GetDateTime(15)
                });
            }
            return list;
        }

        public static async Task<ServerEntry?> GetByIdAsync(string controlCs, Guid id)
        {
            var all = await ListAsync(controlCs, includeInactive: true);
            return all.FirstOrDefault(s => s.Id == id);
        }

        public static async Task AddAsync(string controlCs, ServerEntry entry)
        {
            await EnsureSchemaAsync(controlCs);
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();
            var sql = @"INSERT INTO ""ServerRegistry""(""Id"",""ServerName"",""Host"",""Port"",""DatabaseName"",
                ""Username"",""Password"",""SslMode"",""Description"",""IsActive"",""CreatedAt"")
                VALUES(@id,@name,@host,@port,@db,@user,@pass,@ssl,@desc,@active,@created)";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", entry.Id);
            cmd.Parameters.AddWithValue("name", entry.ServerName);
            cmd.Parameters.AddWithValue("host", entry.Host);
            cmd.Parameters.AddWithValue("port", entry.Port);
            cmd.Parameters.AddWithValue("db", entry.DatabaseName);
            cmd.Parameters.AddWithValue("user", entry.Username);
            cmd.Parameters.AddWithValue("pass", entry.Password);
            cmd.Parameters.AddWithValue("ssl", entry.SslMode);
            cmd.Parameters.AddWithValue("desc", (object?)entry.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("active", entry.IsActive);
            cmd.Parameters.AddWithValue("created", entry.CreatedAt);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task UpdateAsync(string controlCs, ServerEntry entry)
        {
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();
            var sql = @"UPDATE ""ServerRegistry"" SET
                ""ServerName""=@name,""Host""=@host,""Port""=@port,""DatabaseName""=@db,
                ""Username""=@user,""Password""=@pass,""SslMode""=@ssl,""Description""=@desc,
                ""IsActive""=@active,""UpdatedAt""=@upd WHERE ""Id""=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", entry.Id);
            cmd.Parameters.AddWithValue("name", entry.ServerName);
            cmd.Parameters.AddWithValue("host", entry.Host);
            cmd.Parameters.AddWithValue("port", entry.Port);
            cmd.Parameters.AddWithValue("db", entry.DatabaseName);
            cmd.Parameters.AddWithValue("user", entry.Username);
            cmd.Parameters.AddWithValue("pass", entry.Password);
            cmd.Parameters.AddWithValue("ssl", entry.SslMode);
            cmd.Parameters.AddWithValue("desc", (object?)entry.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("active", entry.IsActive);
            cmd.Parameters.AddWithValue("upd", DateTime.UtcNow);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task DeleteAsync(string controlCs, Guid id)
        {
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();
            var sql = @"DELETE FROM ""ServerRegistry"" WHERE ""Id""=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        // ───────── Health Check ─────────

        public static async Task<HealthResult> CheckHealthAsync(ServerEntry server, int timeoutMs = 5000)
        {
            var result = new HealthResult();
            var sw = Stopwatch.StartNew();
            try
            {
                var cs = server.BuildConnectionString();
                var builder = new NpgsqlConnectionStringBuilder(cs) { Timeout = timeoutMs / 1000 };
                await using var con = new NpgsqlConnection(builder.ConnectionString);

                using var cts = new CancellationTokenSource(timeoutMs);
                await con.OpenAsync(cts.Token);
                sw.Stop();

                result.IsOnline = true;
                result.LatencyMs = (int)sw.ElapsedMilliseconds;
                result.ServerVersion = con.ServerVersion;
                result.Status = result.LatencyMs > 2000 ? "Slow" : "Online";
            }
            catch (OperationCanceledException)
            {
                sw.Stop();
                result.IsOnline = false;
                result.Status = "Timeout";
                result.LatencyMs = (int)sw.ElapsedMilliseconds;
                result.ErrorMessage = "Connection timeout";
            }
            catch (Exception ex)
            {
                sw.Stop();
                result.IsOnline = false;
                result.Status = "Offline";
                result.LatencyMs = (int)sw.ElapsedMilliseconds;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Health check a server and update its status in the control DB.
        /// </summary>
        public static async Task<HealthResult> CheckAndUpdateHealthAsync(string controlCs, ServerEntry server)
        {
            var result = await CheckHealthAsync(server);

            // Update status in the registry
            await using var con = new NpgsqlConnection(controlCs);
            await con.OpenAsync();
            var sql = @"UPDATE ""ServerRegistry"" SET
                ""LastHealthCheck""=@t, ""LastHealthStatus""=@s, ""ServerVersion""=@v, ""LatencyMs""=@l
                WHERE ""Id""=@id";
            await using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", server.Id);
            cmd.Parameters.AddWithValue("t", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("s", result.Status);
            cmd.Parameters.AddWithValue("v", (object?)result.ServerVersion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("l", result.LatencyMs);
            await cmd.ExecuteNonQueryAsync();

            // Update the in-memory entry too
            server.LastHealthCheck = DateTime.UtcNow;
            server.LastHealthStatus = result.Status;
            server.ServerVersion = result.ServerVersion;
            server.LatencyMs = result.LatencyMs;

            return result;
        }

        /// <summary>
        /// Ping all servers in parallel and update their health status.
        /// </summary>
        public static async Task<Dictionary<Guid, HealthResult>> PingAllAsync(string controlCs)
        {
            var servers = await ListAsync(controlCs);
            var results = new Dictionary<Guid, HealthResult>();
            var tasks = servers.Select(async s =>
            {
                var r = await CheckAndUpdateHealthAsync(controlCs, s);
                lock (results) results[s.Id] = r;
            });
            await Task.WhenAll(tasks);
            return results;
        }

        // ───────── Stats (Aggregate from remote server) ─────────

        /// <summary>
        /// Fetch aggregate stats from a specific server (org count, branch count, etc.)
        /// </summary>
        public static async Task<ServerEntry> FetchStatsAsync(ServerEntry server)
        {
            try
            {
                var cs = server.BuildConnectionString();
                await using var con = new NpgsqlConnection(cs);
                await con.OpenAsync();

                // Count orgs
                await using (var cmd = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""Organizations"" WHERE ""IsActive""=true", con))
                    server.OrgCount = Convert.ToInt32(await cmd.ExecuteScalarAsync() ?? 0);

                // Count branches
                await using (var cmd = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""Branches"" WHERE ""IsActive""=true", con))
                    server.BranchCount = Convert.ToInt32(await cmd.ExecuteScalarAsync() ?? 0);

                // Count devices
                try
                {
                    await using var cmd = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""Devices"" WHERE ""IsActive""=true", con);
                    server.DeviceCount = Convert.ToInt32(await cmd.ExecuteScalarAsync() ?? 0);
                }
                catch { server.DeviceCount = 0; }

                // Count active activation codes
                try
                {
                    await using var cmd = new NpgsqlCommand(@"SELECT COUNT(*) FROM ""ActivationCodes"" WHERE ""IsActive""=true", con);
                    server.ActiveCodeCount = Convert.ToInt32(await cmd.ExecuteScalarAsync() ?? 0);
                }
                catch { server.ActiveCodeCount = 0; }
            }
            catch { /* stats are optional — don't break if server is down */ }
            return server;
        }
    }
}
