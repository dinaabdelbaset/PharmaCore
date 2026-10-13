using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services.Security
{
    internal static class AuditLogger
    {
        private static readonly ConcurrentQueue<string> _queue = new();
        private static readonly string _file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pharmacore.audit.log");
        public static void LogAttempt(string subject, bool success, string reason)
        {
            var line = $"{DateTime.UtcNow:o}\t{subject}\t{(success?"OK":"FAIL")}\t{reason}";
            _queue.Enqueue(line);
            FlushIfNeeded();
        }
        private static void FlushIfNeeded(){ if(_queue.Count<10) return; Flush(); }
        public static void Flush(){ try{ using var sw = new StreamWriter(_file, append:true, Encoding.UTF8); while(_queue.TryDequeue(out var l)) sw.WriteLine(l); } catch { } }
    }

    internal sealed class AuditSender
    {
        private readonly string _endpoint;
        private readonly System.Net.Http.HttpClient _http = new();
        private bool _started;
        public AuditSender(string endpoint){ _endpoint = endpoint; }
        public void Start(){ if(_started || string.IsNullOrWhiteSpace(_endpoint)) return; _started=true; Task.Run(Loop); }
        private async Task Loop(){ while(_started){ try{ AuditLogger.Flush(); var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pharmacore.audit.log"); if(File.Exists(file)){ var lines = File.ReadAllLines(file); if(lines.Length>0){ var content = new StringContent(string.Join("\n", lines), Encoding.UTF8, "text/plain"); await _http.PostAsync(_endpoint, content); File.Delete(file); } } } catch { } await Task.Delay(5000); } }
        public void Stop(){ _started=false; }
    }
}
