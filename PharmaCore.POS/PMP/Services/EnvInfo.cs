using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace PharmaCore.POS.Services
{
    internal static class EnvInfo
    {
        public static string MachineSid
        {
            get
            {
                try
                {
                    var sid = WindowsIdentity.GetCurrent()?.User?.Value;
                    if (!string.IsNullOrWhiteSpace(sid)) return sid!;
                }
                catch { }
                return Environment.MachineName;
            }
        }
        public static byte[] GetEntropy(string purpose)
        {
            var baseStr = $"{MachineSid}|{Environment.MachineName}|{purpose}";
            return System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(baseStr));
        }
    }
}