using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Npgsql;

namespace PharmaCore.POS.Services.Connection
{
    internal static class ConnectionDiagnostics
    {
        public static async System.Threading.Tasks.Task<string> RunAsync(ConnectionSecrets secrets)
        {
            var sb = new StringBuilder();
            sb.AppendLine("تشخيص الإتصال:");
            try
            {
                var sw = Stopwatch.StartNew();
                var ips = await Dns.GetHostAddressesAsync(secrets.Host);
                sw.Stop();
                sb.AppendLine($"- حل DNS: {ips.Length} عنوان خلال {sw.ElapsedMilliseconds}ms");
            }
            catch (Exception ex) { sb.AppendLine("- فشل حل DNS: " + ex.Message); }
            try
            {
                using var client = new TcpClient();
                var sw = Stopwatch.StartNew();
                var connectTask = client.ConnectAsync(secrets.Host, secrets.Port);
                var timeoutTask = System.Threading.Tasks.Task.Delay(4000);
                var completed = await System.Threading.Tasks.Task.WhenAny(connectTask, timeoutTask);
                if (completed == timeoutTask) sb.AppendLine("- فشل فتح المنفذ (مهلة)");
                else { sw.Stop(); sb.AppendLine($"- فتح منفذ TCP تم خلال {sw.ElapsedMilliseconds}ms"); }
            }
            catch (Exception ex) { sb.AppendLine("- فشل فتح المنفذ: " + ex.Message); }
            try
            {
                var cs = $"Host={secrets.Host};Port={secrets.Port};Database={secrets.Database};Username={secrets.Username};Password={secrets.Password};SSL Mode={(secrets.SslRequire?"Require":"Disable")};Trust Server Certificate={secrets.TrustServerCert}";
                await using var con = new NpgsqlConnection(cs);
                var sw = Stopwatch.StartNew();
                await con.OpenAsync();
                sw.Stop();
                sb.AppendLine($"- فتح اتصال PostgreSQL خلال {sw.ElapsedMilliseconds}ms");
                await using var cmd = new NpgsqlCommand("select version();", con);
                var ver = (string?)await cmd.ExecuteScalarAsync();
                sb.AppendLine("- نسخة PostgreSQL: " + ver);
            }
            catch (PostgresException pex)
            {
                sb.AppendLine("- خطأ PostgreSQL: " + PgErrorClassifier.Classify(pex));
            }
            catch (Exception ex)
            {
                sb.AppendLine("- خطأ عام: " + ex.Message);
            }
            return sb.ToString();
        }
    }

    internal static class PgErrorClassifier
    {
        public static string Classify(PostgresException ex)
        {
            return ex.SqlState switch
            {
                "28P01" => "فشل المصادقة: اسم مستخدم أو كلمة مرور غير صحيحة",
                "3D000" => "قاعدة البيانات غير موجودة",
                "28000" => "رفض الدخول - تأكد من الصلاحيات",
                _ => TranslateMessage(ex.MessageText)
            };
        }
        private static string TranslateMessage(string msg)
        {
            if (msg.Contains("no pg_hba.conf entry", StringComparison.OrdinalIgnoreCase)) return "لا يوجد تصريح في pg_hba.conf لهذا الجهاز - أضف IP والسطر المناسب";
            if (msg.Contains("timeout", StringComparison.OrdinalIgnoreCase)) return "انتهت مهلة الاتصال - تحقق من الشبكة والجدار الناري";
            if (msg.Contains("SSL", StringComparison.OrdinalIgnoreCase)) return "مشكلة في طبقة SSL - تأكد من الإعدادات والشهادة";
            return msg;
        }
    }
}
