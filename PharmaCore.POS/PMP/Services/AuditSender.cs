using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    internal static class AuditSender
    {
        private static readonly ConcurrentQueue<string> Queue = new();
        private static readonly CancellationTokenSource Cts = new();
        private static bool _started; private static string _endpoint="";
        public static void Start(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint)) return;
            if (_started && _endpoint == endpoint) return;
            _endpoint = endpoint;
            if (!_started)
            {
                _started = true;
                Task.Run(async () => await Worker(Cts.Token));
            }
        }
        public static void Enqueue(string line){ if(!_started) return; Queue.Enqueue(line); }
        private static async Task Worker(CancellationToken token)
        {
            using var http = new HttpClient{ Timeout = TimeSpan.FromSeconds(5) };
            while(!token.IsCancellationRequested)
            {
                if (Queue.TryDequeue(out var line))
                {
                    try { await http.PostAsync(_endpoint, new StringContent(line, Encoding.UTF8, "text/plain"), token); }
                    catch { }
                }
                else { await Task.Delay(1000, token); }
            }
        }
    }
}