using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PharmaCore.POS.Services
{
    public static class OfflineSalesSyncWorker
    {
        private static readonly object _syncLock = new object();
        private static bool _running = false;
        private static bool _isSyncing = false;
        private const string DefaultPassword = "PharmaCore-OfflineSales-Secret-Key-2026";

        public static void Start()
        {
            lock (_syncLock)
            {
                if (_running) return;
                _running = true;
            }

            Task.Run(async () => await SyncLoopAsync());
        }

        private static async Task SyncLoopAsync()
        {
            while (_running)
            {
                try
                {
                    await SyncPendingSalesAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"OfflineSalesSyncWorker loop error: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        private static async Task SyncPendingSalesAsync()
        {
            // Thread safety / re-entrancy prevention
            lock (_syncLock)
            {
                if (_isSyncing) return;
                _isSyncing = true;
            }

            try
            {
                // 1. Check if API is online
                bool isOnline = await ConnectionManager.PingApiAsync();
                if (!isOnline) return;

                // Sync catalog cache when we are online
                await LocalCatalogCache.SyncCatalogAsync();

                // 2. Scan directory
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string salesDir = Path.Combine(localAppData, "PharmaCore", "OfflineSales");

                if (!Directory.Exists(salesDir))
                {
                    try
                    {
                        Directory.CreateDirectory(salesDir);
                    }
                    catch
                    {
                        return; // directory cannot be created or accessed
                    }
                }

                var files = Directory.GetFiles(salesDir, "*.dat");
                foreach (var file in files)
                {
                    if (IsFileLocked(file)) continue;

                    byte[] data;
                    try
                    {
                        data = File.ReadAllBytes(file);
                    }
                    catch (IOException)
                    {
                        continue; // file locked or inaccessible
                    }

                    if (data.Length < 28)
                    {
                        // File is corrupted (too small)
                        MarkFileAsCorrupt(file);
                        continue;
                    }

                    // Extract salt, nonce, and blob
                    var salt = new byte[16];
                    var nonce = new byte[12];
                    Buffer.BlockCopy(data, 0, salt, 0, 16);
                    Buffer.BlockCopy(data, 16, nonce, 0, 12);
                    
                    var blob = new byte[data.Length - 28];
                    Buffer.BlockCopy(data, 28, blob, 0, blob.Length);

                    // Decrypt content
                    byte[] plainBytes = null;
                    try
                    {
                        // 1. Try decrypting with EnvInfo.MachineSid (since OfflineSalesManager uses it to encrypt)
                        var machineSid = EnvInfo.MachineSid;
                        if (!string.IsNullOrWhiteSpace(machineSid))
                        {
                            try
                            {
                                plainBytes = CryptoUtil.DecryptAesGcm(blob, machineSid, salt, nonce);
                            }
                            catch (CryptographicException)
                            {
                            }
                        }

                        // 2. Try decrypting with Activation Code as fallback
                        if (plainBytes == null)
                        {
                            var activationCode = ActivationStorage.Load();
                            if (!string.IsNullOrWhiteSpace(activationCode))
                            {
                                try
                                {
                                    plainBytes = CryptoUtil.DecryptAesGcm(blob, activationCode, salt, nonce);
                                }
                                catch (CryptographicException)
                                {
                                }
                            }
                        }

                        // 3. Try decrypting with default password as final fallback
                        if (plainBytes == null)
                        {
                            plainBytes = CryptoUtil.DecryptAesGcm(blob, DefaultPassword, salt, nonce);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Decryption failed for {file}: {ex.Message}");
                        MarkFileAsCorrupt(file);
                        continue;
                    }

                    if (plainBytes == null)
                    {
                        MarkFileAsCorrupt(file);
                        continue;
                    }

                    // Validate JSON and POST
                    string jsonStr;
                    try
                    {
                        jsonStr = Encoding.UTF8.GetString(plainBytes);
                        JObject.Parse(jsonStr); // test parse
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Invalid JSON in {file}: {ex.Message}");
                        MarkFileAsCorrupt(file);
                        continue;
                    }

                    try
                    {
                        using (var http = ConnectionManager.GetHttpClient())
                        {
                            using (var content = new StringContent(jsonStr, Encoding.UTF8, "application/json"))
                            {
                                var res = await http.PostAsync("api/sales", content);
                                
                                if (res.StatusCode == System.Net.HttpStatusCode.OK ||
                                    res.StatusCode == System.Net.HttpStatusCode.Created ||
                                    res.StatusCode == System.Net.HttpStatusCode.Conflict)
                                {
                                    TryDeleteFile(file);
                                }
                                else
                                {
                                    var responseBody = await res.Content.ReadAsStringAsync();
                                    if (responseBody.Contains("already exists") || 
                                        responseBody.Contains("duplicate") || 
                                        responseBody.Contains("exists") || 
                                        responseBody.Contains("synced"))
                                    {
                                        TryDeleteFile(file);
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Sync POST failed for {file} with status {res.StatusCode}");
                                    }
                                }
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        // Network error, retry next loop
                        System.Diagnostics.Debug.WriteLine($"Network error during sync of {file}: {ex.Message}");
                    }
                    catch (TaskCanceledException ex)
                    {
                        // Timeout, retry next loop
                        System.Diagnostics.Debug.WriteLine($"Timeout during sync of {file}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Unexpected error during sync of {file}: {ex.Message}");
                    }
                }
            }
            finally
            {
                lock (_syncLock)
                {
                    _isSyncing = false;
                }
            }
        }

        private static bool IsFileLocked(string filePath)
        {
            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        private static void TryDeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to delete synced file {path}: {ex.Message}");
            }
        }

        private static void MarkFileAsCorrupt(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string dir = Path.GetDirectoryName(path) ?? "";
                    string name = Path.GetFileNameWithoutExtension(path);
                    string newPath = Path.Combine(dir, $"{name}.err");

                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Move(path, newPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to mark file as corrupt {path}: {ex.Message}");
            }
        }
    }
}
