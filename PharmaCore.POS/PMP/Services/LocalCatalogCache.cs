using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    public static class LocalCatalogCache
    {
        private static readonly string CacheDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PharmaCore");
        private static readonly string CachePath = Path.Combine(CacheDir, "CatalogCache.dat");
        private static readonly object FileLock = new object();

        public static Task SyncCatalogAsync() => RefreshCatalogAsync();

        public static async Task RefreshCatalogAsync()
        {
            try
            {
                // Check online status first
                if (!await ConnectionManager.PingApiAsync())
                {
                    return;
                }

                using (var http = ConnectionManager.GetHttpClient())
                {
                    var res = await http.GetAsync("api/batches/sync-catalog");
                    if (res.IsSuccessStatusCode)
                    {
                        var json = await res.Content.ReadAsStringAsync();
                        if (string.IsNullOrWhiteSpace(json)) return;

                        // Encrypt JSON payload using CryptoUtil.EncryptAesGcm
                        string password = EnvInfo.MachineSid;
                        byte[] plainBytes = Encoding.UTF8.GetBytes(json);
                        byte[] blob = CryptoUtil.EncryptAesGcm(plainBytes, password, out byte[] salt, out byte[] nonce);

                        lock (FileLock)
                        {
                            if (!Directory.Exists(CacheDir))
                            {
                                Directory.CreateDirectory(CacheDir);
                            }
                            
                            using (var fs = File.Create(CachePath))
                            {
                                fs.Write(salt, 0, salt.Length);
                                fs.Write(nonce, 0, nonce.Length);
                                fs.Write(blob, 0, blob.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LocalCatalogCache sync failed: {ex.Message}");
            }
        }

        public static JToken LookupBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

            lock (FileLock)
            {
                if (!File.Exists(CachePath)) return null;

                try
                {
                    byte[] data = File.ReadAllBytes(CachePath);
                    if (data.Length < 28) return null;

                    // Extract salt, nonce, and blob
                    var salt = new byte[16];
                    var nonce = new byte[12];
                    Buffer.BlockCopy(data, 0, salt, 0, 16);
                    Buffer.BlockCopy(data, 16, nonce, 0, 12);
                    
                    var blob = new byte[data.Length - 28];
                    Buffer.BlockCopy(data, 28, blob, 0, blob.Length);

                    // Decrypt content using EnvInfo.MachineSid
                    string password = EnvInfo.MachineSid;
                    byte[] plainBytes = CryptoUtil.DecryptAesGcm(blob, password, salt, nonce);
                    if (plainBytes == null) return null;

                    var json = Encoding.UTF8.GetString(plainBytes);
                    var array = JsonConvert.DeserializeObject<JArray>(json);
                    if (array == null) return null;

                    var element = array.FirstOrDefault(x =>
                    {
                        var b = x["barcode"]?.ToString() ?? x["Barcode"]?.ToString();
                        return string.Equals(b, barcode, StringComparison.OrdinalIgnoreCase);
                    });

                    if (element != null)
                    {
                        // Map properties to support case-insensitive casing and Frm_Sales expectations
                        var mapped = new JObject();
                        foreach (var prop in ((JObject)element).Properties())
                        {
                            var name = prop.Name;
                            var camelName = char.ToLowerInvariant(name[0]) + name.Substring(1);
                            mapped[name] = prop.Value;
                            mapped[camelName] = prop.Value;
                        }

                        if (mapped["stockQty"] != null)
                        {
                            mapped["stockInBranch"] = mapped["stockQty"];
                        }
                        if (mapped["stockInBranch"] == null)
                        {
                            mapped["stockInBranch"] = 0;
                        }

                        // Process nested units to make properties accessible with either casing
                        var units = mapped["units"] as JArray;
                        if (units != null)
                        {
                            var mappedUnits = new JArray();
                            var prodUnitsArr = new JArray(); // for combo_Enter product.units (id, name)

                            foreach (JObject u in units)
                            {
                                var mappedU = new JObject();
                                foreach (var prop in u.Properties())
                                {
                                    var name = prop.Name;
                                    var camelName = char.ToLowerInvariant(name[0]) + name.Substring(1);
                                    mappedU[name] = prop.Value;
                                    mappedU[camelName] = prop.Value;
                                }
                                mappedUnits.Add(mappedU);

                                var uId = mappedU["unitId"] ?? mappedU["UnitId"];
                                var uName = mappedU["unitName"] ?? mappedU["UnitName"];
                                if (uId != null && uName != null)
                                {
                                    var prodUnitObj = new JObject();
                                    prodUnitObj["id"] = uId;
                                    prodUnitObj["name"] = uName;
                                    prodUnitsArr.Add(prodUnitObj);
                                }
                            }
                            mapped["units"] = mappedUnits;

                            var productObj = new JObject();
                            productObj["units"] = prodUnitsArr;
                            mapped["product"] = productObj;
                        }

                        return mapped;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"LocalCatalogCache LookupBarcode error: {ex.Message}");
                }
            }

            return null;
        }
    }
}
