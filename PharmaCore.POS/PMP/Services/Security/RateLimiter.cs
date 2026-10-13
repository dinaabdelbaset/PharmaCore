using System;
using System.Collections.Concurrent;

namespace PharmaCore.POS.Services.Security
{
    internal static class RateLimiter
    {
        private static readonly ConcurrentDictionary<string,(int count, DateTime windowStart)> _map = new();
        public static bool Allow(string key, int max, TimeSpan window)
        {
            var now = DateTime.UtcNow;
            var entry = _map.GetOrAdd(key, _ => (0, now));
            if (now - entry.windowStart > window) entry = (0, now);
            entry = (entry.count + 1, entry.windowStart);
            _map[key] = entry;
            return entry.count <= max;
        }
    }
}
