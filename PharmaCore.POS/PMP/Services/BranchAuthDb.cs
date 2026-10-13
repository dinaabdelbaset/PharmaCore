using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace PharmaCore.POS.Services
{
    internal static class BranchAuthDb
    {
        private static readonly byte[] IpKey = SHA256.HashData(Encoding.UTF8.GetBytes("PharmaCore-IP-Enc-Key-2025"));
        private static readonly byte[] HwidHmacKey = SHA256.HashData(Encoding.UTF8.GetBytes("PharmaCore-HWID-HMAC-Key-2025"));

        public static async Task EnsureSchemaAsync(string cs)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
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
            await using var cmd = new NpgsqlCommand(sql, con); await cmd.ExecuteNonQueryAsync();
            var alter = @"ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""FailedCount"" int NOT NULL DEFAULT 0;
                           ALTER TABLE ""BranchUsers"" ADD COLUMN IF NOT EXISTS ""LockoutUntil"" timestamptz NULL;
                           ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""HwidSig"" text NULL;
                           ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""LastLoginAt"" timestamptz NULL;
                           ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""LastIp"" text NULL;";
            await using var cmdAlter = new NpgsqlCommand(alter, con); await cmdAlter.ExecuteNonQueryAsync();
        }

        public sealed class AuthResult { public bool Success { get; set; } public string Reason { get; set; } = string.Empty; }

        public static async Task<AuthResult> AuthenticateAsync(string cs, Guid orgId, Guid branchId, string branchCode, string user, string pass, string hwid)
        {
            var result = new AuthResult();
            if (orgId == Guid.Empty || branchId == Guid.Empty){ result.Reason = "بيانات الفرع أو المنظمة غير محددة"; return result; }
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            // Verify branch code matches BranchId to avoid mismatched activation
            if(!string.IsNullOrWhiteSpace(branchCode))
            {
                var cmdBr = new NpgsqlCommand("SELECT \"Code\" FROM \"Branches\" WHERE \"Id\"=@bid", con); cmdBr.Parameters.AddWithValue("bid", branchId); var codeDb = await cmdBr.ExecuteScalarAsync() as string; if(codeDb!=null && !codeDb.Equals(branchCode, StringComparison.OrdinalIgnoreCase)){ result.Reason = "كود الفرع غير متطابق"; return result; }
            }
            var cmdUser = new NpgsqlCommand("SELECT \"Id\", \"PasswordHash\", \"IsActive\", \"AllowedDevices\", \"FailedCount\", \"LockoutUntil\" FROM \"BranchUsers\" WHERE \"OrgId\"=@o AND \"BranchId\"=@b AND lower(\"UserName\")=lower(@u)", con);
            cmdUser.Parameters.AddWithValue("o", orgId); cmdUser.Parameters.AddWithValue("b", branchId); cmdUser.Parameters.AddWithValue("u", user);
            await using var rdr = await cmdUser.ExecuteReaderAsync();
            if (!await rdr.ReadAsync()){ result.Reason = "اسم المستخدم غير موجود"; return result; }
            var userId = rdr.GetGuid(0); var storedHash = rdr.GetString(1); var active = rdr.GetBoolean(2); var allowed = rdr.GetInt32(3); var failed = rdr.GetInt32(4); DateTime? lockout = rdr.IsDBNull(5)? null : rdr.GetDateTime(5); rdr.Close();
            if(lockout.HasValue && lockout.Value > DateTime.UtcNow){ result.Reason = "الحساب مقفل مؤقتاً"; return result; }
            if (!active){ result.Reason = "المستخدم غير مفعل"; return result; }
            // Migrate malformed legacy hashes of form "argon2$Isopoh.Cryptography.SecureArray.SecureArray" by re-hashing with incoming password (assumes user provides correct password once)
            if (storedHash.StartsWith("argon2$Isopoh.Cryptography", StringComparison.Ordinal))
            {
                var newHash = HashPassword(pass);
                var upFix = new NpgsqlCommand("UPDATE \"BranchUsers\" SET \"PasswordHash\"=@ph WHERE \"Id\"=@id", con); upFix.Parameters.AddWithValue("ph", newHash); upFix.Parameters.AddWithValue("id", userId); await upFix.ExecuteNonQueryAsync();
                storedHash = newHash; // treat as migrated
            }
            if (!VerifyPassword(pass, storedHash))
            {
                failed++; DateTime? newLockout = null; if(failed >= 5){ newLockout = DateTime.UtcNow.AddMinutes(15); failed = 0; }
                var upFail = new NpgsqlCommand("UPDATE \"BranchUsers\" SET \"FailedCount\"=@f, \"LockoutUntil\"=@l WHERE \"Id\"=@id", con);
                upFail.Parameters.AddWithValue("f", failed); upFail.Parameters.AddWithValue("l", (object?)newLockout ?? DBNull.Value); upFail.Parameters.AddWithValue("id", userId); await upFail.ExecuteNonQueryAsync();
                result.Reason = newLockout!=null? "تم قفل الحساب 15 دقيقة" : "كلمة مرور غير صحيحة"; return result;
            }
            // success reset failed count
            var upReset = new NpgsqlCommand("UPDATE \"BranchUsers\" SET \"FailedCount\"=0, \"LockoutUntil\"=NULL WHERE \"Id\"=@id", con); upReset.Parameters.AddWithValue("id", userId); await upReset.ExecuteNonQueryAsync();
            // Device binding logic
            var hwHashCurrent = HashHwid(hwid); var hwSig = SignHwid(hwid);
            // Count existing devices
            var cmdAllDevices = new NpgsqlCommand("SELECT \"Id\", \"HwidHash\" FROM \"BranchUserDevices\" WHERE \"BranchUserId\"=@id", con); cmdAllDevices.Parameters.AddWithValue("id", userId);
            var existingOtherDevice = false; Guid? existingCurrentDeviceId = null; await using(var rdrDev = await cmdAllDevices.ExecuteReaderAsync())
            {
                while(await rdrDev.ReadAsync())
                {
                    var did = rdrDev.GetGuid(0); var hwStored = rdrDev.GetString(1);
                    if(hwStored.Equals(hwHashCurrent, StringComparison.OrdinalIgnoreCase)) existingCurrentDeviceId = did; else existingOtherDevice = true;
                }
            }
            if(existingCurrentDeviceId==null && existingOtherDevice){ result.Reason = "الحساب مسجل على جهاز آخر"; return result; }
            // Allowed devices check
            var cmdAllowed = new NpgsqlCommand("SELECT \"AllowedDevices\" FROM \"BranchUsers\" WHERE \"Id\"=@id", con); cmdAllowed.Parameters.AddWithValue("id", userId); var allowedDevices = (int)await cmdAllowed.ExecuteScalarAsync();
            if(existingCurrentDeviceId==null && existingOtherDevice && allowedDevices<=1){ result.Reason = "الحساب مسجل على جهاز آخر"; return result; }
            if(existingCurrentDeviceId==null && !existingOtherDevice && allowedDevices<=0){ result.Reason = "تجاوز عدد الأجهزة المسموح"; return result; }
            var now = DateTime.UtcNow; var ipPlain = GetLocalIp(); var ipEnc = ipPlain!=null? EncryptIp(ipPlain): null;
            if(existingCurrentDeviceId==null)
            {
                var cmdIns = new NpgsqlCommand("INSERT INTO \"BranchUserDevices\"(\"BranchUserId\",\"HwidHash\",\"HwidSig\",\"LastLoginAt\",\"LastIp\") VALUES(@id,@h,@s,@t,@ip)", con);
                cmdIns.Parameters.AddWithValue("id", userId); cmdIns.Parameters.AddWithValue("h", hwHashCurrent); cmdIns.Parameters.AddWithValue("s", (object?)hwSig ?? DBNull.Value); cmdIns.Parameters.AddWithValue("t", now); cmdIns.Parameters.AddWithValue("ip", (object?)ipEnc ?? DBNull.Value); await cmdIns.ExecuteNonQueryAsync();
            }
            else
            {
                var cmdUpd = new NpgsqlCommand("UPDATE \"BranchUserDevices\" SET \"HwidSig\"=@s, \"LastLoginAt\"=@t, \"LastIp\"=@ip WHERE \"Id\"=@did", con);
                cmdUpd.Parameters.AddWithValue("s", (object?)hwSig ?? DBNull.Value); cmdUpd.Parameters.AddWithValue("t", now); cmdUpd.Parameters.AddWithValue("ip", (object?)ipEnc ?? DBNull.Value); cmdUpd.Parameters.AddWithValue("did", existingCurrentDeviceId.Value); await cmdUpd.ExecuteNonQueryAsync();
            }
            if (IsLegacyHash(storedHash))
            {
                var newHash = HashPassword(pass); var up = new NpgsqlCommand("UPDATE \"BranchUsers\" SET \"PasswordHash\"=@ph WHERE \"Id\"=@id", con); up.Parameters.AddWithValue("ph", newHash); up.Parameters.AddWithValue("id", userId); await up.ExecuteNonQueryAsync();
            }
            result.Success = true; result.Reason = "نجاح"; return result;
        }

        private static string GetLocalIp()
        {
            try { return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)?.ToString(); } catch { return null; }
        }

        // Replace Argon2 hash with PBKDF2 strong parameters
        public static string HashPassword(string password)
        {
            return Security.Argon2Hasher.Hash(password);
        }

        private static bool VerifyPassword(string input, string stored)
        {
            try
            {
                if (stored.StartsWith("$argon2", StringComparison.Ordinal))
                {
                    return Security.Argon2Hasher.Verify(input, stored);
                }
                if (stored.StartsWith("argon2$"))
                {
                    // legacy malformed prefix, remove then attempt
                    var raw = stored.Substring("argon2$".Length);
                    if (!raw.StartsWith("$argon2")) return false;
                    return Security.Argon2Hasher.Verify(input, raw);
                }
                if (IsLegacyHash(stored))
                {
                    var parts = stored.Split(':'); var legacySalt = parts[0]; var legacyHash = parts[1]; using var sha = SHA256.Create(); var calc = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(legacySalt + input))); return string.Equals(calc, legacyHash, StringComparison.OrdinalIgnoreCase);
                }
                if (stored.StartsWith("pbkdf2:"))
                {
                    var seg = stored.Split(':'); if (seg.Length != 4) return false;
                    int iterations = int.Parse(seg[1]); var salt = Convert.FromBase64String(seg[2]); var keyStored = Convert.FromBase64String(seg[3]);
                    using var pbkdf2 = new Rfc2898DeriveBytes(input, salt, iterations, HashAlgorithmName.SHA512); var keyCalc = pbkdf2.GetBytes(keyStored.Length);
                    return CryptographicOperations.FixedTimeEquals(keyStored, keyCalc);
                }
                return false;
            }
            catch { return false; }
        }

        private static bool IsLegacyHash(string h) => h.Count(c => c == ':') == 1 && !h.StartsWith("pbkdf2:");

        public static string HashHwid(string hwid){ using var sha=SHA256.Create(); return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(hwid))).Substring(0,48); }
        private static string SignHwid(string hwid){ using var h = new HMACSHA256(HwidHmacKey); return Convert.ToHexString(h.ComputeHash(Encoding.UTF8.GetBytes(hwid))).Substring(0,32); }

        private static string? EncryptIp(string ip)
        {
            try
            {
                using var aes = Aes.Create(); aes.Key = IpKey; aes.GenerateIV(); aes.Mode = CipherMode.CBC; aes.Padding = PaddingMode.PKCS7;
                using var enc = aes.CreateEncryptor(); var plain = Encoding.UTF8.GetBytes(ip); var cipher = enc.TransformFinalBlock(plain,0,plain.Length);
                var blob = new byte[aes.IV.Length + cipher.Length]; Buffer.BlockCopy(aes.IV,0,blob,0,aes.IV.Length); Buffer.BlockCopy(cipher,0,blob,aes.IV.Length,cipher.Length);
                return Convert.ToBase64String(blob);
            }
            catch { return null; }
        }

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
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var alter = @"ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""LastHeartbeatAt"" timestamptz NULL; ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""TotalActiveSeconds"" int NOT NULL DEFAULT 0; ALTER TABLE ""BranchUserDevices"" ADD COLUMN IF NOT EXISTS ""IsActive"" boolean NOT NULL DEFAULT true;";
            await using var cmd = new NpgsqlCommand(alter, con); await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<IReadOnlyList<DeviceInfo>> GetDevicesAsync(string cs, Guid orgId, Guid branchId)
        {
            var list = new System.Collections.Generic.List<DeviceInfo>();
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = @"SELECT d.""Id"", u.""Id"", u.""UserName"", d.""HwidHash"", d.""LastIp"", d.""LastLoginAt"", d.""LastHeartbeatAt"", d.""IsActive"", u.""AllowedDevices"", d.""TotalActiveSeconds"" FROM ""BranchUserDevices"" d JOIN ""BranchUsers"" u ON d.""BranchUserId""=u.""Id"" WHERE u.""OrgId""=@o AND u.""BranchId""=@b ORDER BY d.""LastLoginAt"" DESC NULLS LAST";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("o", orgId); cmd.Parameters.AddWithValue("b", branchId);
            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                list.Add(new DeviceInfo
                {
                    Id = rdr.GetGuid(0),
                    UserId = rdr.GetGuid(1),
                    UserName = rdr.GetString(2),
                    HwidHash = rdr.GetString(3),
                    LastIp = rdr.IsDBNull(4)? null : rdr.GetString(4),
                    LastLoginAt = rdr.IsDBNull(5)? null : rdr.GetDateTime(5),
                    LastHeartbeatAt = rdr.IsDBNull(6)? null : rdr.GetDateTime(6),
                    IsActive = rdr.IsDBNull(7)? true : rdr.GetBoolean(7),
                    AllowedDevices = rdr.GetInt32(8),
                    TotalActiveSeconds = rdr.GetInt32(9)
                });
            }
            return list;
        }

        public static async Task UpdateHeartbeatAsync(string cs, Guid deviceId, int sessionSeconds)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "UPDATE \"BranchUserDevices\" SET \"LastHeartbeatAt\"=now(), \"TotalActiveSeconds\"=\"TotalActiveSeconds\"+@s WHERE \"Id\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("s", sessionSeconds); cmd.Parameters.AddWithValue("id", deviceId); await cmd.ExecuteNonQueryAsync();
        }

        public static async Task SetDeviceActiveAsync(string cs, Guid deviceId, bool active)
        {
            await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
            var sql = "UPDATE \"BranchUserDevices\" SET \"IsActive\"=@a WHERE \"Id\"=@id";
            await using var cmd = new NpgsqlCommand(sql, con); cmd.Parameters.AddWithValue("a", active); cmd.Parameters.AddWithValue("id", deviceId); await cmd.ExecuteNonQueryAsync();
        }
    }
}