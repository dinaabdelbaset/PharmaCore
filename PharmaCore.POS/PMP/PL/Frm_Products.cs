using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;
using PharmaCore.POS.Helpers;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Products : Form
    {
        // ── State ───────────────────────────────────────
        private Guid? _productId;
        private Guid? _batchId;
        private List<ProductLightDto> _allProductsList = new();

        // ── Singleton ───────────────────────────────────
        private static Frm_Products _inst;
        static void closed(object s, FormClosedEventArgs e) => _inst = null;
        public static Frm_Products GetFrmProducts
        {
            get { if (_inst == null) { _inst = new(); _inst.FormClosed += closed; } return _inst; }
        }

        public Frm_Products()
        {
            InitializeComponent();
            btnDetails.Click += btnDetails_Click;
            btnSetName.Click += btnSetName_Click;
            btnSetUnit.Click += btnSetUnit_Click;
            btnHouses.Click += btnHouses_Click;
            txtBarcode.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };
            SetupGrids();
        }

        private async void btnDetails_Click(object sender, EventArgs e)
        {
            if (!CurrentEmployeeSession.HasPermission("products.activate") && !CurrentEmployeeSession.HasPermission("products.add"))
            {
                MessageBox.Show("عفواً، لا تملك صلاحية الوصول لهذه الشاشة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var frm = new Frm_ProductActive();
            frm.ShowDialog();
            await ReloadProductDetailsAsync();
        }

        private void btnNewProduct_Click(object sender, EventArgs e) => PrepareNew();

        private void btnSetName_Click(object sender, EventArgs e)
        {
            if (!CurrentEmployeeSession.HasPermission("products.add"))
            {
                MessageBox.Show("عفواً، لا تملك صلاحية الوصول لهذه الشاشة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var frm = new Frm_ItemName();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(frm.SelectedNameEn)) txtNameEn.Text = frm.SelectedNameEn;
                if (!string.IsNullOrWhiteSpace(frm.SelectedNameAr)) txtNameAr.Text = frm.SelectedNameAr;
                if (!string.IsNullOrWhiteSpace(frm.SelectedCompany)) cmbCompany.Text = frm.SelectedCompany;
                if (!string.IsNullOrWhiteSpace(frm.SelectedUsage)) cmbUsage.Text = frm.SelectedUsage;
                if (!string.IsNullOrWhiteSpace(frm.SelectedActiveIngredient)) cmbIngredient.Text = frm.SelectedActiveIngredient;
            }
        }

        private async void btnHouses_Click(object sender, EventArgs e)
        {
            if (!CurrentEmployeeSession.HasPermission("warehouse.add"))
            {
                MessageBox.Show("عفواً، لا تملك صلاحية الوصول لهذه الشاشة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var frm = new Frm_Store())
            {
                frm.ShowDialog();
            }
            try
            {
                using var http = Http();
                var whs = await http.GetFromJsonAsync<List<LookupItem>>("api/warehouses") ?? new();
                SetupContainsAutoComplete(cmbWarehouse, whs);
                if (cmbWarehouse.Items.Count > 0) cmbWarehouse.SelectedIndex = 0; else cmbWarehouse.SelectedIndex = -1;
            }
            catch { }
        }

        private void SetupGrids()
        {
            dgvUnits.Columns.Clear();
            dgvBranchStock.Columns.Clear();
            dgvBatches.Columns.Clear();

            // Units grid columns
            dgvUnits.Columns.Add("colUnitName", "الوحدة");
            dgvUnits.Columns.Add("colUnitQty", "العدد");
            dgvUnits.Columns.Add("colUnitPrice", "السعر");
            var delCol = new DataGridViewButtonColumn
            {
                Name = "colUnitDel",
                HeaderText = "حذف",
                Text = "✕",
                UseColumnTextForButtonValue = true
            };
            dgvUnits.Columns.Add(delCol);

            // Branch stock grid columns
            dgvBranchStock.Columns.Add("colRegion", "المنطقة");
            dgvBranchStock.Columns.Add("colBranch", "الفرع");
            dgvBranchStock.Columns.Add("colStockQty", "الكمية");

            // Batches grid columns
            dgvBatches.Columns.Add("colBatchId", "ID");
            dgvBatches.Columns["colBatchId"].Visible = false;
            dgvBatches.Columns.Add("colBarcode", "الباركود");
            dgvBatches.Columns.Add("colExpiry", "الانتهاء");
            dgvBatches.Columns.Add("colBatchQty", "الكمية");
            dgvBatches.Columns.Add("colBatchBuyPrice", "سعر الشراء");
            dgvBatches.Columns.Add("colBatchSalePrice", "سعر البيع");
            dgvBatches.Columns.Add("colBatchTax", "الضريبة%");
            dgvBatches.Columns.Add("colBatchAfterTax", "بعد الضريبة");
            dgvBatches.Columns.Add("colBatchWarehouse", "المخزن");
            dgvBatches.Columns.Add("colBatchBonus", "البونص");
        }

        // ── HTTP ────────────────────────────────────────
        private HttpClient Http()
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch { baseUrl = Properties.Settings.Default.ApiBase?.Trim() ?? "http://localhost:5001/"; }
            }
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            var h = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            var c = new HttpClient(h) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
            var t = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(t))
                c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", t);
            return c;
        }

        private FormScaler _scaler;

        private async void Frm_Products_Load(object sender, EventArgs e)
        {
            try
            {
                _scaler = new FormScaler(this, pnlTopBar);

                using var http = Http();

                var cats = await http.GetFromJsonAsync<List<LookupItem>>("api/categories") ?? new();
                var whs = await http.GetFromJsonAsync<List<LookupItem>>("api/warehouses") ?? new();

                SetupContainsAutoComplete(cmbCategory, cats);
                SetupContainsAutoComplete(cmbWarehouse, whs);

                cmbCategory.SelectedIndex = -1;
                if (cmbWarehouse.Items.Count > 0) cmbWarehouse.SelectedIndex = 0; else cmbWarehouse.SelectedIndex = -1;

                await ReloadProductDetailsAsync(http);
                await ReloadUnitsAsync();

                lstProducts.DisplayMember = "DisplayLabel";
                lstProducts.ValueMember = "Id";
                await LoadListAsync();

                PrepareNew();
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async Task ReloadProductDetailsAsync(HttpClient http = null)
        {
            var client = http ?? Http();
            try
            {
                var ings = await client.GetFromJsonAsync<List<LookupItem>>("api/active-ingredients") ?? new();
                var usages = await client.GetFromJsonAsync<List<LookupItem>>("api/usages") ?? new();
                var comps = await client.GetFromJsonAsync<List<LookupItem>>("api/companies") ?? new();

                SetupContainsAutoComplete(cmbIngredient, ings);
                SetupContainsAutoComplete(cmbUsage, usages);
                SetupContainsAutoComplete(cmbCompany, comps);

                cmbIngredient.SelectedIndex = -1;
                cmbUsage.SelectedIndex = -1;
                cmbCompany.SelectedIndex = -1;
            }
            catch { }
            finally
            {
                if (http == null) client.Dispose();
            }
        }

        private void SetupContainsAutoComplete(ComboBox cmb, List<LookupItem> sourceList)
        {
            cmb.AutoCompleteMode = AutoCompleteMode.None;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "Id";
            cmb.DataSource = sourceList.ToList();
            
            cmb.TextUpdate -= Cmb_TextUpdate_Shared;
            cmb.TextUpdate += Cmb_TextUpdate_Shared;
            cmb.Tag = sourceList;
        }

        private void Cmb_TextUpdate_Shared(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cmb || cmb.Tag is not List<LookupItem> originalList) return;

            string typedText = cmb.Text;
            int cursorPosition = cmb.SelectionStart;

            if (string.IsNullOrWhiteSpace(typedText))
            {
                cmb.DataSource = originalList.ToList();
                cmb.DroppedDown = false;
            }
            else
            {
                var filtered = originalList.Where(x => x.Name != null && x.Name.Contains(typedText, StringComparison.OrdinalIgnoreCase)).ToList();
                cmb.DataSource = filtered;
                cmb.DroppedDown = true;
                Cursor.Current = Cursors.Default;
            }

            cmb.Text = typedText;
            cmb.SelectionStart = cursorPosition;
            cmb.SelectionLength = 0;
        }

        // ══════════════════════════════════════════════
        // PREPARE NEW
        // ══════════════════════════════════════════════
        private void PrepareNew()
        {
            _productId = null; _batchId = null;
            txtProductCode.Text = "(جديد)"; txtNameEn.Text = ""; txtNameAr.Text = "";
            cmbCompany.SelectedIndex = -1; txtNotes.Text = "";
            cmbCategory.SelectedIndex = -1; cmbIngredient.SelectedIndex = -1; cmbUsage.SelectedIndex = -1;
            chkEnglishPrimary.Checked = true;

            ClearBatchFields();
            dgvBatches.Rows.Clear();
            lstPromos.Items.Clear();
            dgvBranchStock.Rows.Clear();
            lblStatSold.Text = "إجمالي المبيعات: 0";
            lblStatPurchased.Text = "إجمالي المشتريات: 0";
            lblStatReturned.Text = "إجمالي المرتجعات: 0";
            txtTotalQty.Text = "0";

            btnAddProduct.Enabled = true; btnAddProduct.BackColor = System.Drawing.Color.FromArgb(0, 140, 100);
            btnSaveProduct.Enabled = false; btnSaveProduct.BackColor = System.Drawing.Color.Gray;
            btnDeleteProduct.Enabled = false; btnDeleteProduct.BackColor = System.Drawing.Color.Gray;
        }

        private void ClearBatchFields()
        {
            _batchId = null;
            txtBarcode.Text = ""; dtpExpiry.Value = DateTime.Now;
            nudBuyPrice.Value = 0; nudSalePrice.Value = 0; nudTaxPercent.Value = 0;
            chkTax.Checked = false; txtPriceAfterTax.Text = "0.00";
            nudMaxDiscount.Value = 0; nudQty.Value = 0; nudMinAlert.Value = 0;
            chkBonus.Checked = false; nudBonusValue.Value = 0; chkBonusPercent.Checked = false;
            dgvUnits.Rows.Clear();
            if (cmbWarehouse.Items.Count > 0) cmbWarehouse.SelectedIndex = 0; else cmbWarehouse.SelectedIndex = -1;

            btnAddBatch.Enabled = true; btnSaveBatch.Enabled = false;
        }

        // ══════════════════════════════════════════════
        // SHOW PRODUCT
        // ══════════════════════════════════════════════
        private async System.Threading.Tasks.Task ShowProductAsync(Guid id)
        {
            try
            {
                using var http = Http();
                var p = await http.GetFromJsonAsync<ProductDetailDto>($"api/products/{id}");
                if (p == null) { MessageBox.Show("لم يتم العثور على المنتج"); return; }

                _productId = p.Id;
                txtProductCode.Text = p.ProductCode;
                txtNameEn.Text = p.NameEn; txtNameAr.Text = p.NameAr ?? "";
                if (!string.IsNullOrEmpty(p.CompanyName)) cmbCompany.Text = p.CompanyName; else cmbCompany.SelectedIndex = -1;
                txtNotes.Text = p.Notes ?? "";
                chkEnglishPrimary.Checked = p.IsEnglishPrimary;

                if (p.CategoryId.HasValue) cmbCategory.SelectedValue = p.CategoryId.Value; else cmbCategory.SelectedIndex = -1;
                if (p.ActiveIngredientId.HasValue) cmbIngredient.SelectedValue = p.ActiveIngredientId.Value; else cmbIngredient.SelectedIndex = -1;
                if (!string.IsNullOrEmpty(p.UsageType)) cmbUsage.SelectedItem = p.UsageType; else cmbUsage.SelectedIndex = -1;

                // Stats
                lblStatSold.Text = $"إجمالي المبيعات: {p.TotalSoldQty:F0}";
                lblStatPurchased.Text = $"إجمالي المشتريات: {p.TotalPurchasedQty:F0}";
                lblStatReturned.Text = $"إجمالي المرتجعات: {p.TotalReturnedQty:F0}";

                // Batches → current branch only
                dgvBatches.Rows.Clear();
                decimal total = 0;
                foreach (var b in p.Batches ?? new())
                {
                    var branchStock = b.Stocks?.FirstOrDefault(); // current branch stock
                    decimal qty = branchStock?.Quantity ?? 0;
                    total += qty;

                    int r = dgvBatches.Rows.Add();
                    dgvBatches.Rows[r].Cells["colBatchId"].Value = b.Id.ToString();
                    dgvBatches.Rows[r].Cells["colBarcode"].Value = b.Barcode;
                    dgvBatches.Rows[r].Cells["colExpiry"].Value = b.ExpiryDate?.ToString("MM/yyyy") ?? "";
                    dgvBatches.Rows[r].Cells["colBatchQty"].Value = qty.ToString("F2");
                    dgvBatches.Rows[r].Cells["colBatchBuyPrice"].Value = b.PharmacyBuyPrice.ToString("F2");
                    dgvBatches.Rows[r].Cells["colBatchSalePrice"].Value = b.PublicSalePrice.ToString("F2");
                    dgvBatches.Rows[r].Cells["colBatchTax"].Value = b.TaxPercent.ToString("F0") + "%";
                    dgvBatches.Rows[r].Cells["colBatchAfterTax"].Value = b.PriceAfterTax.ToString("F2");
                    dgvBatches.Rows[r].Cells["colBatchWarehouse"].Value = branchStock?.WarehouseName ?? "";
                    dgvBatches.Rows[r].Cells["colBatchBonus"].Value = b.BonusAmount?.ToString("F2") ?? "-";
                }
                txtTotalQty.Text = total.ToString("F2");

                // Load active promotions for this product
                await LoadPromotionsAsync(http, p.Id);

                ClearBatchFields();
                btnAddProduct.Enabled = false; btnAddProduct.BackColor = System.Drawing.Color.Gray;
                btnSaveProduct.Enabled = true; btnSaveProduct.BackColor = System.Drawing.Color.FromArgb(0, 100, 180);
                btnDeleteProduct.Enabled = true; btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async System.Threading.Tasks.Task LoadPromotionsAsync(HttpClient http, Guid productId)
        {
            try
            {
                lstPromos.Items.Clear();
                var promos = await http.GetFromJsonAsync<List<PromoDto>>($"api/products/{productId}/promotions") ?? new();
                foreach (var pr in promos)
                {
                    string typeLabel = pr.Type switch
                    {
                        0 => "🟢 اشتري واحصل مجاناً",
                        1 => "🔵 خصم على الكمية",
                        2 => "🟡 عرض باقة",
                        3 => "🟠 عرض براند",
                        _ => "⚪ عرض"
                    };
                    lstPromos.Items.Add($"{typeLabel} — {pr.Name}");
                }
                if (lstPromos.Items.Count == 0)
                    lstPromos.Items.Add("لا توجد عروض نشطة");
            }
            catch { lstPromos.Items.Add("خطأ في تحميل العروض"); }
        }

        // ══════════════════════════════════════════════
        // NAVIGATION & SEARCH
        // ══════════════════════════════════════════════
        private async System.Threading.Tasks.Task LoadListAsync()
        {
            try
            {
                using var http = Http();
                var list = await http.GetFromJsonAsync<List<ProductLightDto>>("api/products/light");
                if (list != null)
                {
                    _allProductsList = list;
                    FilterList("");
                }
            }
            catch { }
        }

        private void FilterList(string query)
        {
            lstProducts.BeginUpdate();
            if (string.IsNullOrWhiteSpace(query))
            {
                lstProducts.DataSource = _allProductsList.Take(100).ToList();
            }
            else
            {
                var parts = query.Split(new[] { '*', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.ToLower()).ToArray();
                lstProducts.DataSource = _allProductsList.Where(p =>
                {
                    bool match = true;
                    foreach (var part in parts)
                    {
                        if (!((p.SearchName?.ToLower().Contains(part) == true) ||
                              (p.ProductCode?.ToLower().Contains(part) == true) ||
                              (p.NameEn?.ToLower().Contains(part) == true) ||
                              (p.NameAr?.ToLower().Contains(part) == true)))
                        {
                            match = false;
                            break;
                        }
                    }
                    return match;
                }).Take(100).ToList();
            }
            lstProducts.EndUpdate();
        }

        private void txtSearch_TextChanged(object s, EventArgs e)
        {
            FilterList(txtSearch.Text);
        }

        private async void lstProducts_SelectedIndexChanged(object s, EventArgs e)
        {
            if (lstProducts.SelectedValue is Guid id)
            {
                await ShowProductAsync(id);
            }
        }

        // ══════════════════════════════════════════════
        // PRODUCT CRUD
        // ══════════════════════════════════════════════
        private async void btnAddProduct_Click(object s, EventArgs e)
        {
            if (_productId.HasValue) { MessageBox.Show("هذا المنتج مسجل بالفعل، يرجى الضغط على زر حفظ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtNameEn.Text)) { MessageBox.Show("ادخل اسم المنتج أولاً"); return; }
            try
            {
                using var http = Http();
                var req = new
                {
                    NameEn = txtNameEn.Text.Trim(),
                    NameAr = string.IsNullOrWhiteSpace(txtNameAr.Text) ? (string)null : txtNameAr.Text.Trim(),
                    CategoryId = cmbCategory.SelectedValue is Guid c ? (Guid?)c : null,
                    ActiveIngredientId = cmbIngredient.SelectedValue is Guid a ? (Guid?)a : null,
                    UsageType = cmbUsage.Text?.ToString(),
                    CompanyName = string.IsNullOrWhiteSpace(cmbCompany.Text) ? null : cmbCompany.Text.Trim(),
                    IsEnglishPrimary = chkEnglishPrimary.Checked,
                    Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim()
                };
                var resp = await http.PostAsJsonAsync("api/products", req);
                if (!resp.IsSuccessStatusCode) { MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}"); return; }
                var d = await resp.Content.ReadFromJsonAsync<JsonElement>();
                _productId = d.GetProperty("id").GetGuid();
                txtProductCode.Text = d.GetProperty("productCode").GetString();
                MessageBox.Show("تم إضافة المنتج بنجاح ✅");
                btnAddProduct.Enabled = false; btnAddProduct.BackColor = System.Drawing.Color.Gray;
                btnSaveProduct.Enabled = true; btnSaveProduct.BackColor = System.Drawing.Color.FromArgb(0, 100, 180);
                btnDeleteProduct.Enabled = true; btnDeleteProduct.BackColor = System.Drawing.Color.FromArgb(180, 40, 40);

                var newItem = new ProductLightDto
                {
                    Id = _productId.Value,
                    ProductCode = txtProductCode.Text,
                    NameEn = txtNameEn.Text.Trim(),
                    NameAr = string.IsNullOrWhiteSpace(txtNameAr.Text) ? null : txtNameAr.Text.Trim(),
                    SearchName = txtNameEn.Text.Trim()
                };
                _allProductsList.Add(newItem);
                FilterList(txtSearch.Text);
                lstProducts.SelectedItem = newItem;
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async void btnSaveProduct_Click(object s, EventArgs e)
        {
            if (!_productId.HasValue) { MessageBox.Show("هذا منتج جديد، يرجى الضغط على زر إضافة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtNameEn.Text)) { MessageBox.Show("ادخل اسم المنتج أولاً"); return; }
            try
            {
                using var http = Http();
                var req = new
                {
                    NameEn = txtNameEn.Text.Trim(),
                    NameAr = string.IsNullOrWhiteSpace(txtNameAr.Text) ? (string)null : txtNameAr.Text.Trim(),
                    CategoryId = cmbCategory.SelectedValue is Guid c ? (Guid?)c : null,
                    ActiveIngredientId = cmbIngredient.SelectedValue is Guid a ? (Guid?)a : null,
                    UsageType = cmbUsage.Text?.ToString(),
                    CompanyName = string.IsNullOrWhiteSpace(cmbCompany.Text) ? null : cmbCompany.Text.Trim(),
                    IsEnglishPrimary = chkEnglishPrimary.Checked,
                    Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim()
                };
                var resp = await http.PutAsJsonAsync($"api/products/{_productId}", req);
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("تم حفظ التعديل ✅");
                    var item = _allProductsList.FirstOrDefault(p => p.Id == _productId.Value);
                    if (item != null)
                    {
                        item.NameEn = txtNameEn.Text.Trim();
                        item.NameAr = string.IsNullOrWhiteSpace(txtNameAr.Text) ? null : txtNameAr.Text.Trim();
                        item.SearchName = txtNameEn.Text.Trim();
                        FilterList(txtSearch.Text);
                        lstProducts.SelectedItem = item;
                    }
                }
                else MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}");
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async void btnDeleteProduct_Click(object s, EventArgs e)
        {
            if (!_productId.HasValue) return;
            if (MessageBox.Show("هل أنت متأكد من حذف المنتج وجميع باتشاته؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                using var http = Http();
                await http.DeleteAsync($"api/products/{_productId}");
                MessageBox.Show("تم حذف المنتج ✅");
                var item = _allProductsList.FirstOrDefault(p => p.Id == _productId.Value);
                if (item != null)
                {
                    _allProductsList.Remove(item);
                    FilterList(txtSearch.Text);
                }
                PrepareNew();
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        // ══════════════════════════════════════════════
        // BATCH ADD / EDIT
        // ══════════════════════════════════════════════
        private object BuildBatchPayload()
        {
            var units = new List<object>();
            for (int i = 0; i < dgvUnits.Rows.Count; i++)
            {
                units.Add(new
                {
                    UnitId = Guid.Empty,
                    UnitName = dgvUnits.Rows[i].Cells["colUnitName"].Value?.ToString() ?? "",
                    QtyPerMainUnit = Convert.ToInt32(dgvUnits.Rows[i].Cells["colUnitQty"].Value ?? 1),
                    UnitSalePrice = Convert.ToDecimal(dgvUnits.Rows[i].Cells["colUnitPrice"].Value ?? 0)
                });
            }
            return new
            {
                Barcode = txtBarcode.Text.Trim(),
                ExpiryDate = dtpExpiry.Value,
                Quantity = nudQty.Value,
                WarehouseId = cmbWarehouse.SelectedValue is Guid w ? (Guid?)w : null,
                PharmacyBuyPrice = nudBuyPrice.Value,
                PublicSalePrice = nudSalePrice.Value,
                IsTaxable = chkTax.Checked,
                TaxPercent = nudTaxPercent.Value,
                MaxDiscount = nudMaxDiscount.Value,
                MinQtyAlert = nudMinAlert.Value,
                MainUnitId = (Guid)(cmbMainUnit.SelectedValue ?? Guid.Empty),
                MainUnitQty = 1,
                SaleUnitId = (Guid)(cmbSaleUnit.SelectedValue ?? Guid.Empty),
                PurchaseUnitId = (Guid)(cmbBuyUnit.SelectedValue ?? Guid.Empty),
                BonusPerMainUnit = chkBonus.Checked ? (decimal?)nudBonusValue.Value : null,
                BonusIsPercentage = chkBonus.Checked ? (bool?)chkBonusPercent.Checked : null,
                AdditionalUnits = units
            };
        }

        private async void btnAddBatch_Click(object s, EventArgs e)
        {
            if (!_productId.HasValue) { MessageBox.Show("أضف المنتج أولاً"); return; }
            if (string.IsNullOrWhiteSpace(txtBarcode.Text)) { MessageBox.Show("ادخل الباركود أولاً"); return; }
            if (nudSalePrice.Value <= 0) { MessageBox.Show("سعر البيع لا يمكن أن يكون صفر"); return; }
            if (cmbMainUnit.SelectedValue == null) { MessageBox.Show("اختر الوحدة الكبرى"); return; }
            try
            {
                using var http = Http();
                var resp = await http.PostAsJsonAsync($"api/products/{_productId}/batches", BuildBatchPayload());
                if (!resp.IsSuccessStatusCode) { MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}"); return; }
                MessageBox.Show("تمت إضافة الباتشة ✅");
                await ShowProductAsync(_productId.Value);
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async void btnSaveBatch_Click(object s, EventArgs e)
        {
            if (!_batchId.HasValue) return;
            if (nudSalePrice.Value <= 0) { MessageBox.Show("سعر البيع لا يمكن أن يكون صفر"); return; }
            try
            {
                using var http = Http();
                var resp = await http.PutAsJsonAsync($"api/batches/{_batchId}", BuildBatchPayload());
                if (resp.IsSuccessStatusCode) { MessageBox.Show("تم حفظ التعديل ✅"); await ShowProductAsync(_productId!.Value); }
                else MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}");
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        // ══════════════════════════════════════════════
        // BOTTOM GRID ACTIONS
        // ══════════════════════════════════════════════
        private async void btnEditBatchFromGrid_Click(object s, EventArgs e)
        {
            if (dgvBatches.CurrentRow == null) return;
            var barcode = dgvBatches.CurrentRow.Cells["colBarcode"].Value?.ToString();
            if (string.IsNullOrEmpty(barcode)) return;
            try
            {
                using var http = Http();
                var batch = await http.GetFromJsonAsync<BatchDetailDto>($"api/batches/by-barcode/{barcode}");
                if (batch == null) return;

                _batchId = batch.BatchId;
                txtBarcode.Text = batch.Barcode;
                nudBuyPrice.Value = 0; // will load from detailed view
                nudSalePrice.Value = batch.PublicSalePrice;
                txtPriceAfterTax.Text = batch.PriceAfterTax.ToString("F2");
                nudMaxDiscount.Value = batch.MaxDiscount;
                chkTax.Checked = batch.IsTaxable; nudTaxPercent.Value = batch.TaxPercent;
                nudQty.Value = batch.StockInBranch;

                if (batch.ExpiryDate.HasValue) dtpExpiry.Value = batch.ExpiryDate.Value;

                if (batch.BonusPerMainUnit.HasValue && batch.BonusPerMainUnit.Value > 0)
                {
                    chkBonus.Checked = true; nudBonusValue.Value = batch.BonusPerMainUnit.Value;
                    chkBonusPercent.Checked = batch.BonusIsPercentage ?? false;
                }
                else { chkBonus.Checked = false; }

                dgvUnits.Rows.Clear();
                foreach (var u in batch.Units ?? new())
                {
                    int r = dgvUnits.Rows.Add();
                    dgvUnits.Rows[r].Cells["colUnitName"].Value = u.UnitName;
                    dgvUnits.Rows[r].Cells["colUnitQty"].Value = u.QtyPerMainUnit;
                    dgvUnits.Rows[r].Cells["colUnitPrice"].Value = u.UnitSalePrice.ToString("F2");
                }

                // Toggle buttons
                btnAddBatch.Enabled = false; btnSaveBatch.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private async void btnDeleteBatch_Click(object s, EventArgs e)
        {
            if (dgvBatches.CurrentRow == null) return;
            var id = dgvBatches.CurrentRow.Cells["colBatchId"].Value?.ToString();
            if (!Guid.TryParse(id, out var batchId)) return;
            if (MessageBox.Show("حذف هذه الباتشة؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                using var http = Http();
                await http.DeleteAsync($"api/batches/{batchId}");
                MessageBox.Show("تم حذف الباتشة ✅");
                if (_productId.HasValue) await ShowProductAsync(_productId.Value);
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        private void SetupBarcodeData(DevExpress.XtraReports.UI.XtraReport rpt, int qty = 1)
        {
            var dt = new System.Data.DataTable("Barcod");
            dt.Columns.Add("اسم المنتج AR", typeof(string));
            dt.Columns.Add("اسم المنتج EN", typeof(string));
            dt.Columns.Add("اسم المنتج", typeof(string)); // Legacy support
            dt.Columns.Add("المجموعة", typeof(string));
            dt.Columns.Add("المادة الفعالة", typeof(string));
            dt.Columns.Add("اسم الشركة", typeof(string));
            dt.Columns.Add("اسم الصيدلية", typeof(string));
            dt.Columns.Add("رقم الهاتف", typeof(string));
            dt.Columns.Add("سعر البيع", typeof(decimal));
            dt.Columns.Add("تاريخ الصلاحية", typeof(DateTime));
            dt.Columns.Add("رقم الباركود", typeof(string));

            string nameAr = txtNameAr.Text;
            string nameEn = txtNameEn.Text;
            string grp = cmbCategory.Text;
            string active = cmbIngredient.Text;
            string comp = cmbCompany.Text;
            string pharmacy = AppSettingsStore.Get("InvoiceName") ?? "";
            string p1 = AppSettingsStore.Get("InvoicePhone1") ?? "";

            string barcode = "";
            decimal pr = 0;
            string expStr = "";
            DateTime exp = DateTime.Now;

            if (dgvBatches.CurrentRow != null)
            {
                barcode = dgvBatches.CurrentRow.Cells["colBarcode"].Value?.ToString() ?? "";
                decimal.TryParse(dgvBatches.CurrentRow.Cells["colBatchAfterTax"].Value?.ToString(), out pr);
                expStr = dgvBatches.CurrentRow.Cells["colExpiry"].Value?.ToString() ?? "";
                try { var parts = expStr.Split('/'); if (parts.Length == 2) exp = new DateTime(int.Parse(parts[1]), int.Parse(parts[0]), 1); } catch { }
            }

            for (int i = 0; i < qty; i++)
            {
                dt.Rows.Add(nameAr, nameEn, nameEn, grp, active, comp, pharmacy, p1, pr, exp, barcode);
            }

            rpt.DataSource = dt;
            rpt.DataMember = ""; 
            rpt.ExportOptions.PrintPreview.DefaultFileName = $"{nameEn}_{barcode}";

            // Parameters
            if (rpt.Parameters["ProductName"] == null) rpt.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter { Name = "ProductName", Type = typeof(string), Value = nameEn, Description = "اسم المنتج" });
            if (rpt.Parameters["Barcode"] == null) rpt.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter { Name = "Barcode", Type = typeof(string), Value = barcode, Description = "رقم الباركود" });
            if (rpt.Parameters["Price"] == null) rpt.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter { Name = "Price", Type = typeof(string), Value = pr.ToString("F2"), Description = "السعر" });

            rpt.Parameters["ProductName"].Value = nameEn;
            rpt.Parameters["Barcode"].Value = barcode;
            rpt.Parameters["Price"].Value = pr.ToString("F2");

            // Automatically translate and repair the inner canvas to work with Arabic 
            Services.ReportSchemaProvider.FormatReportForSmartArabic(rpt);
        }

        private async void btnPrintBarcode_Click(object s, EventArgs e)
        {
            if (dgvBatches.CurrentRow == null) return;
            string productName = txtNameEn.Text;
            string barcode = dgvBatches.CurrentRow.Cells["colBarcode"].Value?.ToString() ?? "";
            decimal.TryParse(dgvBatches.CurrentRow.Cells["colBatchAfterTax"].Value?.ToString(), out var pr);
            string price = pr.ToString("F2");
            string expiryStr = dgvBatches.CurrentRow.Cells["colExpiry"].Value?.ToString() ?? "";

            string reportName = "RptCrystalReport1";

            string printerName = AppSettingsStore.Get("PrinterName2") ?? Properties.Settings.Default.PrinterName2;
            
            Form prompt = new Form()
            {
                Width = 250, Height = 170, FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "عداد الطباعة", StartPosition = FormStartPosition.CenterScreen,
                RightToLeft = RightToLeft.Yes, RightToLeftLayout = true,
                MinimizeBox = false, MaximizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 10, Width = 190, Text = "كم عدد ملصقات الباركود؟" };
            NumericUpDown num = new NumericUpDown() { Left = 20, Top = 40, Width = 190, Minimum = 1, Maximum = 5000, Value = 1 };
            Button confirmation = new Button() { Text = "إرسال للطابعة", Left = 20, Width = 190, Top = 80, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(num); prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() != DialogResult.OK) return;
            int qty = (int)num.Value;

            var rpt = new RE.RptCrystalReport1();
            PharmaCore.POS.Services.ReportManager.LoadCustomLayoutIfExists(rpt, reportName);

            // Bind Data & Params AFTER loading layout to avoid wiping
            SetupBarcodeData(rpt, qty);
            rpt.RequestParameters = false;
            rpt.ShowPrintMarginsWarning = false;

            if (!string.IsNullOrWhiteSpace(printerName))
                rpt.PrinterName = printerName;

            var printTool = new DevExpress.XtraReports.UI.ReportPrintTool(rpt);
            printTool.Print();
        }

        private void dgvBatches_CellClick(object s, DataGridViewCellEventArgs e)
        {
            // Row selected — enable bottom buttons
        }

        // ══════════════════════════════════════════════
        // TAX CALC
        // ══════════════════════════════════════════════
        private void CalcTax()
        {
            decimal sale = nudSalePrice.Value;
            decimal tax = chkTax.Checked ? (sale / 100m) * nudTaxPercent.Value : 0;
            txtPriceAfterTax.Text = (sale + tax).ToString("F2");
        }
        private void nudSalePrice_ValueChanged(object s, EventArgs e) => CalcTax();
        private void nudTaxPercent_ValueChanged(object s, EventArgs e) => CalcTax();
        private void chkTax_CheckedChanged(object s, EventArgs e) { nudTaxPercent.Value = chkTax.Checked ? 14 : 0; CalcTax(); }

        // ══════════════════════════════════════════════
        // UNITS
        // ══════════════════════════════════════════════
        private void btnAddUnit_Click(object s, EventArgs e)
        {
            if (cmbSubUnit.SelectedValue == null || nudSubQty.Value <= 0) { MessageBox.Show("اختر الوحدة وعددها"); return; }
            if (cmbSubUnit.SelectedValue.Equals(cmbMainUnit.SelectedValue)) { MessageBox.Show("الوحدة الفرعية لا يمكن أن تكون نفس الوحدة الكبرى"); return; }

            string name = cmbSubUnit.Text;
            for (int i = 0; i < dgvUnits.Rows.Count; i++)
                if (dgvUnits.Rows[i].Cells["colUnitName"].Value?.ToString() == name) { MessageBox.Show("تمت إضافتها من قبل"); return; }

            // Auto-calc price = PriceAfterTax / subQty
            decimal afterTax = 0; decimal.TryParse(txtPriceAfterTax.Text, out afterTax);
            decimal unitPrice = nudSubQty.Value > 0 ? Math.Round(afterTax / nudSubQty.Value, 2) : 0;

            int r = dgvUnits.Rows.Add();
            dgvUnits.Rows[r].Cells["colUnitName"].Value = name;
            dgvUnits.Rows[r].Cells["colUnitQty"].Value = (int)nudSubQty.Value;
            dgvUnits.Rows[r].Cells["colUnitPrice"].Value = unitPrice.ToString("F2");
        }

        private void dgvUnits_CellContentClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUnits.Columns[e.ColumnIndex].Name == "colUnitDel")
                dgvUnits.Rows.RemoveAt(e.RowIndex);
        }

        private void dgvUnits_CellDoubleClick(object s, DataGridViewCellEventArgs e)
        {
            // Allow editing price cell only
            if (e.RowIndex >= 0 && dgvUnits.Columns[e.ColumnIndex].Name == "colUnitPrice")
            {
                dgvUnits.Columns["colUnitPrice"].ReadOnly = false;
                dgvUnits.BeginEdit(true);
            }
        }

        // ══════════════════════════════════════════════
        // BARCODE GEN
        // ══════════════════════════════════════════════
        private async void btnGenBarcode_Click(object s, EventArgs e)
        {
            try
            {
                using var http = Http();
                var r = await http.GetFromJsonAsync<JsonElement>($"api/batches/generate-barcode?length={nudBarcodeLength.Value}");
                txtBarcode.Text = r.GetProperty("barcode").GetString();
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }


        // ══════════════════════════════════════════════
        // PROMOTIONS MANAGEMENT
        // ══════════════════════════════════════════════
        private async void btnManagePromos_Click(object s, EventArgs e)
        {
            using (Frm_ManagePromos frm = new Frm_ManagePromos(_productId))
            {
                frm.ShowDialog();
            }
            // Refresh
            if (_productId.HasValue)
            {
                try { using var http = Http(); await LoadPromotionsAsync(http, _productId.Value); }
                catch { }
            }
        }

        // ══════════════════════════════════════════════
        // BRANCH STOCK (on-demand)
        // ══════════════════════════════════════════════
        private async void btnLoadStock_Click(object s, EventArgs e)
        {
            if (!_productId.HasValue) { MessageBox.Show("اختر منتج أولاً"); return; }
            try
            {
                dgvBranchStock.Rows.Clear();
                using var http = Http();
                var p = await http.GetFromJsonAsync<ProductDetailDto>($"api/products/{_productId}");
                if (p?.Batches == null) return;

                // Aggregate stock by branch across all batches
                var branchTotals = new Dictionary<string, (string Region, string Branch, decimal Qty)>();
                foreach (var batch in p.Batches)
                {
                    foreach (var stk in batch.Stocks ?? new())
                    {
                        var key = stk.BranchId.ToString();
                        if (branchTotals.ContainsKey(key))
                        {
                            var existing = branchTotals[key];
                            branchTotals[key] = (existing.Region, existing.Branch, existing.Qty + stk.Quantity);
                        }
                        else
                        {
                            branchTotals[key] = (stk.RegionName ?? "-", stk.BranchName ?? "-", stk.Quantity);
                        }
                    }
                }

                foreach (var kv in branchTotals.Values)
                {
                    int r = dgvBranchStock.Rows.Add();
                    dgvBranchStock.Rows[r].Cells["colRegion"].Value = kv.Region;
                    dgvBranchStock.Rows[r].Cells["colBranch"].Value = kv.Branch;
                    dgvBranchStock.Rows[r].Cells["colStockQty"].Value = kv.Qty.ToString("F2");
                }

                if (dgvBranchStock.Rows.Count == 0) MessageBox.Show("لا يوجد مخزون مسجل");
            }
            catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}"); }
        }

        // ══════════════════════════════════════════════
        // F1 SEARCH
        // ══════════════════════════════════════════════
        private async void Frm_Products_KeyDown(object s, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Properties.Settings.Default.Open_Form = 17;
                Properties.Settings.Default.Save();
                new Frm_ItemSales().ShowDialog();
                if (Properties.Settings.Default.Open_Form == 1)
                {
                    var sid = Properties.Settings.Default.sales_ID.ToString();
                    if (!string.IsNullOrEmpty(sid) && sid != "0")
                    {
                        try
                        {
                            using var http = Http();
                            var resp = await http.GetAsync($"api/products/by-code/{sid}");
                            if (resp.IsSuccessStatusCode)
                            {
                                var p = await resp.Content.ReadFromJsonAsync<ProductDetailDto>();
                                if (p != null) await ShowProductAsync(p.Id);
                            }
                        }
                        catch { }
                    }
                }
            }
            else if (e.KeyCode == Keys.F5) { PrepareNew(); }
        }

        // ══════════════════════════════════════════════
        // DTOs
        // ══════════════════════════════════════════════
        record LookupItem { public Guid Id { get; set; } public string Name { get; set; } = ""; }
        record UnitItem { public Guid Id { get; set; } public string Name { get; set; } = ""; }
        record ProductLightDto
        {
            public Guid Id { get; set; }
            public string ProductCode { get; set; } = "";
            public string? NameEn { get; set; }
            public string? NameAr { get; set; }
            public string? SearchName { get; set; }
            public string DisplayLabel => $"[{ProductCode}] {NameEn ?? NameAr}";
        }

        record ProductDetailDto
        {
            public Guid Id { get; set; }
            public string ProductCode { get; set; } = "";
            public string NameEn { get; set; } = "";
            public string? NameAr { get; set; }
            public Guid? CategoryId { get; set; }
            public Guid? ActiveIngredientId { get; set; }
            public string? UsageType { get; set; }
            public string? CompanyName { get; set; }
            public bool IsEnglishPrimary { get; set; }
            public string? Notes { get; set; }
            public decimal TotalSoldQty { get; set; }
            public decimal TotalPurchasedQty { get; set; }
            public decimal TotalReturnedQty { get; set; }
            public List<BatchDto> Batches { get; set; } = new();
        }

        record BatchDto
        {
            public Guid Id { get; set; }
            public string Barcode { get; set; } = "";
            public DateTime? ExpiryDate { get; set; }
            public decimal PharmacyBuyPrice { get; set; }
            public decimal PublicSalePrice { get; set; }
            public bool IsTaxable { get; set; }
            public decimal TaxPercent { get; set; }
            public decimal PriceAfterTax { get; set; }
            public decimal MaxDiscount { get; set; }
            public decimal? BonusAmount { get; set; }
            public List<StockDto> Stocks { get; set; } = new();
            public List<UnitDto> Units { get; set; } = new();
        }

        record StockDto
        {
            public Guid BranchId { get; set; }
            public string? BranchName { get; set; }
            public string? RegionName { get; set; }
            public string? WarehouseName { get; set; }
            public decimal Quantity { get; set; }
        }

        record UnitDto { public string UnitName { get; set; } = ""; public int QtyPerMainUnit { get; set; } public decimal UnitSalePrice { get; set; } }

        record BatchDetailDto
        {
            public Guid ProductId { get; set; }
            public Guid BatchId { get; set; }
            public string Barcode { get; set; } = "";
            public DateTime? ExpiryDate { get; set; }
            public decimal PublicSalePrice { get; set; }
            public decimal PriceAfterTax { get; set; }
            public decimal MaxDiscount { get; set; }
            public bool IsTaxable { get; set; }
            public decimal TaxPercent { get; set; }
            public decimal StockInBranch { get; set; }
            public decimal? BonusPerMainUnit { get; set; }
            public bool? BonusIsPercentage { get; set; }
            public List<UnitDto> Units { get; set; } = new();
        }

        record PromoDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public int Type { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public bool IsActive { get; set; }
        }

        private async void btnSetUnit_Click(object sender, EventArgs e)
        {
            if (!CurrentEmployeeSession.HasPermission("products.unit"))
            {
                MessageBox.Show("عفواً، لا تملك صلاحية الوصول لهذه الشاشة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var frm = new Frm_Unit();
            frm.ShowDialog();
            await ReloadUnitsAsync();
        }

        public async Task ReloadUnitsAsync()
        {
            try
            {
                using var http = Http();
                var units = await http.GetFromJsonAsync<List<UnitItem>>("api/units") ?? new();
                cmbMainUnit.DataSource = new List<UnitItem>(units); cmbMainUnit.DisplayMember = "Name"; cmbMainUnit.ValueMember = "Id";
                cmbSaleUnit.DataSource = new List<UnitItem>(units); cmbSaleUnit.DisplayMember = "Name"; cmbSaleUnit.ValueMember = "Id";
                cmbBuyUnit.DataSource = new List<UnitItem>(units); cmbBuyUnit.DisplayMember = "Name"; cmbBuyUnit.ValueMember = "Id";
                cmbSubUnit.DataSource = new List<UnitItem>(units); cmbSubUnit.DisplayMember = "Name"; cmbSubUnit.ValueMember = "Id";
            }
            catch { }
        }
    }
}
