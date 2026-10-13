using System;
using System.Collections.Concurrent;

namespace PharmaCore.POS.Services
{
    internal static class RateLimiter
    {
        private static readonly ConcurrentQueue<DateTime> Attempts = new();
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(1);
        private const int MaxAttempts = 5; // 5 محاولة بالدقيقة
        public static bool Allow()
        {
            var now = DateTime.UtcNow;
            Attempts.Enqueue(now);
            while (Attempts.TryPeek(out var t) && now - t > Window) Attempts.TryDequeue(out _);
            return Attempts.Count <= MaxAttempts;
        }
    }
}