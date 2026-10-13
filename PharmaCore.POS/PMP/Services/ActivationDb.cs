using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.POS.Services
{
    internal static class ActivationDb
    {
        public static async Task<(bool ok,string reason,ActivationRecord? rec)> ValidateAsync(string conn,string code,bool includeNames=false)
        {
            if(string.IsNullOrWhiteSpace(code)) return (false,"كود فارغ",null);
            if(!IsFormatValid(code)) return (false,"صيغة الكود غير صحيحة",null);
            try
            {
                await using var con = new NpgsqlConnection(conn);
                await con.OpenAsync();
                string sql;
                if (includeNames)
                {
                    sql = @"SELECT ac.""OrgId"", ac.""TenantId"", ac.""RegionId"", ac.""BranchId"", ac.""MaxDevices"", ac.""UsedDevicesCount"", ac.""ExpiresAt"", ac.""IsActive"", 
                                o.""Name"" AS OrgName, t.""Name"" AS TenantName, r.""Name"" AS RegionName, b.""Name"" AS BranchName
                           FROM ""ActivationCodes"" ac
                           LEFT JOIN ""Organizations"" o ON ac.""OrgId"" = o.""Id""
                           LEFT JOIN ""Tenants"" t ON ac.""TenantId"" = t.""Id""
                           LEFT JOIN ""Regions"" r ON ac.""RegionId"" = r.""Id""
                           LEFT JOIN ""Branches"" b ON ac.""BranchId"" = b.""Id""
                           WHERE ac.""CodePlain"" = @p";
                }
                else
                {
                    sql = @"SELECT ""OrgId"", ""TenantId"", ""RegionId"", ""BranchId"", ""MaxDevices"", ""UsedDevicesCount"", ""ExpiresAt"", ""IsActive"" FROM ""ActivationCodes"" WHERE ""CodePlain""=@p";
                }
                await using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("p", code);
                await using var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                if(!await rdr.ReadAsync()) return (false, "الكود غير موجود", null);
                var rec = new ActivationRecord
                {
                    OrgId = rdr.GetGuid(0),
                    TenantId = rdr.IsDBNull(1)? null : rdr.GetGuid(1),
                    RegionId = rdr.IsDBNull(2)? null : rdr.GetGuid(2),
                    BranchId = rdr.IsDBNull(3)? null : rdr.GetGuid(3),
                    MaxDevices = rdr.GetInt32(4),
                    UsedDevicesCount = rdr.GetInt32(5),
                    ExpiresAt = rdr.IsDBNull(6)? (DateTime?)null : rdr.GetDateTime(6),
                    IsActive = rdr.GetBoolean(7),
                    OrgName = includeNames && !rdr.IsDBNull(8)? rdr.GetString(8):null,
                    TenantName = includeNames && !rdr.IsDBNull(9)? rdr.GetString(9):null,
                    RegionName = includeNames && !rdr.IsDBNull(10)? rdr.GetString(10):null,
                    BranchName = includeNames && !rdr.IsDBNull(11)? rdr.GetString(11):null
                };
                if(!rec.IsActive) return (false, "الكود غير مفعل", rec);
                if(rec.ExpiresAt.HasValue && rec.ExpiresAt.Value.Date < DateTime.UtcNow.Date) return (false, "انتهت الصلاحية", rec);
                if(rec.UsedDevicesCount >= rec.MaxDevices) return (false, "تم استهلاك العدد المسموح للأجهزة", rec);
                return (true, "صالح", rec);
            }
            catch(Exception ex){ return (false, ex.Message, null); }
        }
        public static async Task<(bool ok,string reason)> IncrementUsageAsync(string conn,string code)
        {
            if(!IsFormatValid(code)) return (false,"صيغة الكود غير صحيحة");
            try
            {
                await using var con = new NpgsqlConnection(conn); await con.OpenAsync();
                const string sql = "UPDATE \"ActivationCodes\" SET \"UsedDevicesCount\" = \"UsedDevicesCount\" + 1 WHERE \"CodePlain\"=@c AND \"IsActive\"=true AND (\"ExpiresAt\" IS NULL OR \"ExpiresAt\">=CURRENT_DATE) AND \"UsedDevicesCount\" < \"MaxDevices\"";
                await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("c", code);
                var affected = await cmd.ExecuteNonQueryAsync();
                return affected==1? (true,"تم التحديث") : (false,"شروط التحديث غير مستوفاة أو الكود غير موجود");
            }
            catch(Exception ex){ return (false, ex.Message); }
        }
        public static bool IsFormatValid(string code) => System.Text.RegularExpressions.Regex.IsMatch(code ?? "", "^[A-Za-z0-9]{4}-[A-Za-z0-9]{5}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}$");
    }
    internal sealed class ActivationRecord
    {
        public Guid OrgId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? BranchId { get; set; }
        public int MaxDevices { get; set; }
        public int UsedDevicesCount { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public string? OrgName { get; set; }
        public string? TenantName { get; set; }
        public string? RegionName { get; set; }
        public string? BranchName { get; set; }
    }
}