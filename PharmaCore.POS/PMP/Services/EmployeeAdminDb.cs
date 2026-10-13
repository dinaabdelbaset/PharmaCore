using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services
{
    internal static class EmployeeAdminDb
    {
        public sealed class EmployeeRow
        {
            public Guid Id { get; set; }
            public string EmployeeCode { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? JobTitle { get; set; }
            public string? Grade { get; set; }
            public Guid OrgId { get; set; }
            public Guid? TenantId { get; set; }
            public Guid? RegionId { get; set; }
            public Guid? BranchId { get; set; }
            public string Role { get; set; } = string.Empty;
            public bool IsActive { get; set; }
        }

        public static async Task<string> GenerateNextEmployeeCodeAsync(string cs, Guid orgId)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT MAX(\"EmployeeCode\") FROM \"Employees\" WHERE \"OrgId\"=@o", con); cmd.Parameters.AddWithValue("o", orgId);
            var latest = await cmd.ExecuteScalarAsync() as string; if(string.IsNullOrWhiteSpace(latest)) return "E0001";
            // extract numeric tail
            int i = latest.Length - 1; while(i>=0 && char.IsDigit(latest[i])) i--; var prefix = latest.Substring(0,i+1); var numberPart = latest.Substring(i+1);
            if(!int.TryParse(numberPart, out var num)) return latest + "1"; num++; return prefix + num.ToString(new string('0', numberPart.Length));
        }

        public static string HashPin(string pin)
        {
            return Security.Argon2Hasher.Hash(pin); // Argon2 strong hashing
        }

        public static async Task<Guid?> CreateAsync(string cs, Guid orgId, Guid? tenantId, Guid? regionId, Guid? branchId, string employeeCode, string fullName, string pinPlain, string role, string? jobTitle, string? grade, bool isActive)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "INSERT INTO \"Employees\"(\"Id\",\"OrgId\",\"TenantId\",\"RegionId\",\"BranchId\",\"EmployeeCode\",\"PINHash\",\"FullName\",\"Role\",\"JobTitle\",\"Grade\",\"IsActive\") VALUES(@id,@o,@t,@r,@b,@code,@ph,@fn,@role,@jt,@gr,@act)";
            var id = Guid.NewGuid(); var hash = HashPin(pinPlain);
            var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id); cmd.Parameters.AddWithValue("o", orgId); cmd.Parameters.AddWithValue("t", (object?)tenantId ?? DBNull.Value); cmd.Parameters.AddWithValue("r", (object?)regionId ?? DBNull.Value); cmd.Parameters.AddWithValue("b", (object?)branchId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("code", employeeCode); cmd.Parameters.AddWithValue("ph", hash); cmd.Parameters.AddWithValue("fn", fullName); cmd.Parameters.AddWithValue("role", role); cmd.Parameters.AddWithValue("jt", (object?)jobTitle ?? DBNull.Value); cmd.Parameters.AddWithValue("gr", (object?)grade ?? DBNull.Value); cmd.Parameters.AddWithValue("act", isActive);
            var rows = await cmd.ExecuteNonQueryAsync(); return rows==1? id : null;
        }

        public static async Task<IReadOnlyList<EmployeeRow>> LoadAllAsync(string cs, Guid orgId)
        {
            var list = new List<EmployeeRow>();
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "SELECT \"Id\",\"EmployeeCode\",\"FullName\",\"JobTitle\",\"Grade\",\"OrgId\",\"TenantId\",\"RegionId\",\"BranchId\",\"Role\",\"IsActive\" FROM \"Employees\" WHERE \"OrgId\"=@o ORDER BY \"EmployeeCode\"";
            var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("o", orgId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while(await rdr.ReadAsync())
            {
                list.Add(new EmployeeRow
                {
                    Id = rdr.GetGuid(0),
                    EmployeeCode = rdr.GetString(1),
                    FullName = rdr.GetString(2),
                    JobTitle = rdr.IsDBNull(3)? null : rdr.GetString(3),
                    Grade = rdr.IsDBNull(4)? null : rdr.GetString(4),
                    OrgId = rdr.GetGuid(5),
                    TenantId = rdr.IsDBNull(6)? null : rdr.GetGuid(6),
                    RegionId = rdr.IsDBNull(7)? null : rdr.GetGuid(7),
                    BranchId = rdr.IsDBNull(8)? null : rdr.GetGuid(8),
                    Role = rdr.IsDBNull(9)? string.Empty : rdr.GetString(9),
                    IsActive = rdr.IsDBNull(10)? true : rdr.GetBoolean(10)
                });
            }
            return list;
        }

        public static async Task<bool> UpdateAsync(string cs, Guid id, string fullName, string? jobTitle, string? grade, string role, bool isActive)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "UPDATE \"Employees\" SET \"FullName\"=@fn, \"JobTitle\"=@jt, \"Grade\"=@gr, \"Role\"=@role, \"IsActive\"=@act, \"UpdatedAt\"=now() WHERE \"Id\"=@id";
            var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("fn", fullName); cmd.Parameters.AddWithValue("jt", (object?)jobTitle ?? DBNull.Value); cmd.Parameters.AddWithValue("gr", (object?)grade ?? DBNull.Value); cmd.Parameters.AddWithValue("role", role); cmd.Parameters.AddWithValue("act", isActive); cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync()==1;
        }

        public static async Task<bool> ResetPinAsync(string cs, Guid id, string newPin)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "UPDATE \"Employees\" SET \"PINHash\"=@ph, \"UpdatedAt\"=now() WHERE \"Id\"=@id"; var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("ph", HashPin(newPin)); cmd.Parameters.AddWithValue("id", id);
            return await cmd.ExecuteNonQueryAsync()==1;
        }

        public static async Task<bool> DeleteAsync(string cs, Guid id)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "DELETE FROM \"Employees\" WHERE \"Id\"=@id"; var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("id", id); return await cmd.ExecuteNonQueryAsync()==1;
        }
        public static async Task<bool> AnyEmployeesExistAsync()
        {
            var csProvider = new ConnectionStringProvider();
            var cs = csProvider.GetConnectionString();
            if (string.IsNullOrWhiteSpace(cs)) return true; // can't check → assume exist
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var cmd = new NpgsqlCommand("SELECT EXISTS(SELECT 1 FROM \"Employees\" LIMIT 1)", con);
            var result = await cmd.ExecuteScalarAsync();
            return result is true;
        }
    }
}
