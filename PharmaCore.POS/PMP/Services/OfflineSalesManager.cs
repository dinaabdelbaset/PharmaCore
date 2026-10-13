using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PharmaCore.POS.Services
{
    public static class OfflineSalesManager
    {
        private static readonly string ConfigDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PharmaCore");
        private static readonly string ConfigPath = Path.Combine(ConfigDir, "offline_config.json");
        private static readonly string QueueDir = Path.Combine(ConfigDir, "OfflineSales");
        private static readonly object FileLock = new object();

        private class OfflineConfig
        {
            public int Counter { get; set; }
        }

        private static int GetNextCounter()
        {
            lock (FileLock)
            {
                if (!Directory.Exists(ConfigDir))
                {
                    Directory.CreateDirectory(ConfigDir);
                }

                int counter = 0;
                if (File.Exists(ConfigPath))
                {
                    try
                    {
                        var content = File.ReadAllText(ConfigPath);
                        var config = JsonSerializer.Deserialize<OfflineConfig>(content);
                        if (config != null)
                        {
                            counter = config.Counter;
                        }
                    }
                    catch
                    {
                        counter = 0;
                    }
                }

                counter++;

                var newConfig = new OfflineConfig { Counter = counter };
                var json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);

                return counter;
            }
        }

        public static string GenerateOfflineInvoiceNumber()
        {
            string hostname = Environment.MachineName;
            int counter = GetNextCounter();
            return $"INV-{hostname}-{counter}";
        }

        public static string QueueOfflineSale(object saleReq)
        {
            if (saleReq == null)
            {
                throw new ArgumentNullException(nameof(saleReq));
            }

            // 1. Generate offline invoice number
            string invoiceNumber = GenerateOfflineInvoiceNumber();

            // 2. Serialize and associate details
            var jsonStr = JsonSerializer.Serialize(saleReq);
            var node = JsonNode.Parse(jsonStr);
            if (node is JsonObject obj)
            {
                obj["offlineInvoiceNumber"] = invoiceNumber;
                obj["offlineCreatedAtUtc"] = DateTime.UtcNow.ToString("o");
                obj["OfflineInvoiceNumber"] = invoiceNumber;
                obj["OfflineCreatedAtUtc"] = DateTime.UtcNow.ToString("o");
            }

            string updatedJson = node?.ToJsonString() ?? jsonStr;

            // 3. Encrypt JSON payload using CryptoUtil.EncryptAesGcm
            // We use EnvInfo.MachineSid as password (secure & stable per machine)
            string password = EnvInfo.MachineSid;
            byte[] plainBytes = Encoding.UTF8.GetBytes(updatedJson);
            byte[] blob = CryptoUtil.EncryptAesGcm(plainBytes, password, out byte[] salt, out byte[] nonce);

            // 4. Ensure queue directory exists
            if (!Directory.Exists(QueueDir))
            {
                Directory.CreateDirectory(QueueDir);
            }

            // 5. Write encrypted blob (including salt and nonce)
            string filePath = Path.Combine(QueueDir, $"sale_{Guid.NewGuid()}.dat");
            using (var fs = File.Create(filePath))
            {
                fs.Write(salt, 0, salt.Length);
                fs.Write(nonce, 0, nonce.Length);
                fs.Write(blob, 0, blob.Length);
            }

            return invoiceNumber;
        }
    }
}
