using System;
using System.Security.Cryptography;
using System.Text;

namespace PharmaCore.POS.Services.Connection
{
    internal static class EnvInfo
    {
        public static byte[] GetEntropy(string purpose)
        {
            var baseStr = Environment.MachineName + "|" + purpose + "|PharmaCore-2025";
            return SHA256.HashData(Encoding.UTF8.GetBytes(baseStr));
        }
    }
}
