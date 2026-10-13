using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Npgsql;

namespace PharmaCore.POS.Services
{
    internal static class ConnectionDiagnostics
    {
        public sealed class Report { public string Host=""; public int Port; public string Dns=""; public long LatencyMs; public string PgVersion=""; public string Note=""; }
        public static async Task<Report> RunAsync(ConnectionSecrets s)
        {
            var rep = new Report{ Host=s.Host, Port=s.Port };
            var sw = Stopwatch.StartNew();
            try { var addrs = await Dns.GetHostAddressesAsync(s.Host); rep.Dns = string.Join(",", addrs.Select(a=>a.ToString())); } catch (Exception ex){ rep.Dns = ex.Message; }
            try { using var tcp = new TcpClient(); var t = tcp.ConnectAsync(s.Host, s.Port); var ok = await Task.WhenAny(t, Task.Delay(5000))==t; rep.LatencyMs = ok? sw.ElapsedMilliseconds : -1; } catch { rep.LatencyMs = -1; }
            try {
                var cs = $"Host={s.Host};Port={s.Port};Database={s.Database};Username={s.Username};Password={s.Password};SSL Mode={(s.SslRequire?"Require":"Disable")};Trust Server Certificate={(s.TrustServerCert?"true":"false")}";

                await using var con = new NpgsqlConnection(cs); await con.OpenAsync();
                await using var cmd = new NpgsqlCommand("SELECT version()", con); rep.PgVersion = (string?)await cmd.ExecuteScalarAsync() ?? "";
            } catch (Exception ex){ rep.Note = ex.Message; }
            return rep;
        }
    }
}