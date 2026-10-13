using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ManagePromos : Form
    {
        // ── Promo State
        private List<PromoListDto> _promos = new();
        private List<CollectionListDto> _collections = new();
        private Guid? _currentPromoId = null;
        private Guid? _currentCollectionId = null;

        // ── Selected Product Context from Frm_Products
        private Guid? _contextProductId;

        public Frm_ManagePromos(Guid? contextProductId = null)
        {
            InitializeComponent();
            _contextProductId = contextProductId;
            
            // Event Bindings
            this.Load += Frm_ManagePromos_Load;
            _lstPromos.SelectedIndexChanged += async (s, e) => await LoadPromoDetails();
            _lstCollections.SelectedIndexChanged += async (s, e) => await LoadCollectionDetails();
            _dgvRules.CellContentClick += DgvRules_CellContentClick;
            _dgvColItems.CellContentClick += DgvColItems_CellContentClick;

            _btnSavePromo.Click += BtnSavePromo_Click;
            _btnNewPromo.Click += (s, e) => ResetPromo();
            _btnSaveCol.Click += BtnSaveCol_Click;
            _btnNewCol.Click += (s, e) => ResetCollection();
            btnAddItems.Click += BtnSearchForCollection_Click;
        }

        private HttpClient Http()
        {
            string baseUrl;
            try { baseUrl = PharmaCore.POS.Services.Configuration.ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = PharmaCore.POS.Services.Configuration.ApiBaseResolver.GetRequiredBaseUrl(); }
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

        // ── LOAD ───────────────────────────────────────────
        private async void Frm_ManagePromos_Load(object sender, EventArgs e)
        {
            ResetPromo();
            ResetCollection();
            await RefreshLists();
        }

        private async Task RefreshLists()
        {
            try
            {
                using var http = Http();
                var p = await http.GetFromJsonAsync<List<PromoListDto>>("api/promotions?activeOnly=false");
                _promos = p ?? new();
                _lstPromos.DataSource = null;
                _lstPromos.DataSource = _promos;
                _lstPromos.DisplayMember = "Name";
                _lstPromos.ValueMember = "Id";

                var c = await http.GetFromJsonAsync<List<CollectionListDto>>("api/promotions/collections");
                _collections = c ?? new();
                _lstCollections.DataSource = null;
                _lstCollections.DataSource = _collections;
                _lstCollections.DisplayMember = "Name";
                _lstCollections.ValueMember = "Id";
            }
            catch (Exception ex) { MessageBox.Show("خطأ في التحميل: " + ex.Message); }
        }

        // ── API DTOs ──────────────────────────────────────
        record PromoListDto(Guid Id, string Name, string? Description, int Type, DateTime StartDate, DateTime EndDate, bool IsActive, List<RuleDto> Rules);
        record RuleDto(Guid Id, int ConditionType, Guid? ConditionProductId, string? ConditionProductName, Guid? ConditionCategoryId, string? ConditionCategoryName, Guid? ConditionCollectionId, string? ConditionCollectionName, string? ConditionBrand, int BuyQty, int RewardType, Guid? RewardProductId, string? RewardProductName, int RewardQty, decimal? DiscountPercent, decimal? DiscountAmount);
        record CreatePromoReq(string Name, string? Description, int Type, DateTime StartDate, DateTime EndDate, Guid? BranchId, int Priority, List<CreateRuleReq> Rules);
        record CreateRuleReq(int ConditionType, Guid? ConditionProductId, Guid? ConditionCategoryId, Guid? ConditionCollectionId, string? ConditionBrand, int BuyQty, int RewardType, Guid? RewardProductId, int RewardQty, decimal? DiscountPercent, decimal? DiscountAmount);

        record CollectionListDto(Guid Id, string Name, int ItemCount);
        record CollectionDetailDto(Guid Id, string Name, List<CollectionItemDto> Items);
        record CollectionItemDto(Guid Id, Guid ProductId, string ProductName, string ProductCode);
        record CreateColReq(string Name, List<Guid> ProductIds);

        // ── BEHAVIOR: PROMOS ───────────────────────────────
        private void ResetPromo()
        {
            _currentPromoId = null;
            _txtPromoName.Text = ""; _txtPromoDesc.Text = "";
            _dtpStart.Value = DateTime.Today; _dtpEnd.Value = DateTime.Today.AddMonths(1);
            if (_cmbPromoType.Items.Count > 0) _cmbPromoType.SelectedIndex = 0;
            _chkActive.Checked = true;
            _dgvRules.Rows.Clear();
        }

        private async Task LoadPromoDetails()
        {
            if (_lstPromos.SelectedItem is not PromoListDto p) return;
            ResetPromo();
            _currentPromoId = p.Id;
            _txtPromoName.Text = p.Name; _txtPromoDesc.Text = p.Description;
            _dtpStart.Value = p.StartDate; _dtpEnd.Value = p.EndDate;
            if (p.Type < _cmbPromoType.Items.Count) _cmbPromoType.SelectedIndex = p.Type; 
            _chkActive.Checked = p.IsActive;

            foreach (var r in p.Rules ?? new())
            {
                int row = _dgvRules.Rows.Add();
                var rr = _dgvRules.Rows[row];
                rr.Cells["colCondition"].Value = r.ConditionType switch { 0 => "منتج محدد", 1 => "قسم محدد", 2 => "براند محدد", 3 => "أي منتج", 4 => "مجموعة مخصصة", _ => "أي منتج" };
                
                string condVal = r.ConditionProductName ?? r.ConditionCategoryName ?? r.ConditionCollectionName ?? r.ConditionBrand ?? "";
                Guid? cid = r.ConditionProductId ?? r.ConditionCategoryId ?? r.ConditionCollectionId;
                
                rr.Cells["colCondValName"].Value = condVal;
                rr.Cells["colCondValId"].Value = cid;
                rr.Cells["colBuyQty"].Value = r.BuyQty;
                
                rr.Cells["colReward"].Value = r.RewardType switch { 0 => "بونص مجاني", 1 => "خصم نسبة", 2 => "خصم مبلغ ثابت", _ => "بونص مجاني" };
                rr.Cells["colRewardQtyOrVal"].Value = r.RewardType == 0 ? r.RewardQty.ToString() : (r.RewardType == 1 ? r.DiscountPercent?.ToString() : r.DiscountAmount?.ToString());
                rr.Cells["colRewardProdName"].Value = r.RewardProductName;
                rr.Cells["colRewardProdId"].Value = r.RewardProductId;
            }
        }

        private async void BtnSavePromo_Click(object s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtPromoName.Text)) { MessageBox.Show("الرجاء إدخال اسم العرض"); return; }
            var rules = new List<CreateRuleReq>();
            for (int i=0; i<_dgvRules.Rows.Count; i++)
            {
                var row = _dgvRules.Rows[i];
                if (row.IsNewRow) continue;
                string condStr = row.Cells["colCondition"].Value?.ToString() ?? "أي منتج";
                string rewStr = row.Cells["colReward"].Value?.ToString() ?? "خصم نسبة";
                int cType = condStr switch { "منتج محدد" => 0, "قسم محدد" => 1, "براند محدد" => 2, "مجموعة مخصصة" => 4, _ => 3 };
                int rType = rewStr switch { "بونص مجاني" => 0, "خصم نسبة" => 1, "خصم مبلغ ثابت" => 2, _ => 1 };
                
                Guid? cId = Guid.TryParse(row.Cells["colCondValId"].Value?.ToString(), out var g) ? g : null;
                string? brand = cType == 2 ? row.Cells["colCondValName"].Value?.ToString() : null;
                int.TryParse(row.Cells["colBuyQty"].Value?.ToString(), out int bQty);
                
                Guid? rId = Guid.TryParse(row.Cells["colRewardProdId"].Value?.ToString(), out var rg) ? rg : null;
                decimal.TryParse(row.Cells["colRewardQtyOrVal"].Value?.ToString(), out decimal rVal);
                
                rules.Add(new CreateRuleReq(
                    ConditionType: cType,
                    ConditionProductId: cType == 0 ? cId : null,
                    ConditionCategoryId: cType == 1 ? cId : null,
                    ConditionCollectionId: cType == 4 ? cId : null,
                    ConditionBrand: brand,
                    BuyQty: bQty,
                    RewardType: rType,
                    RewardProductId: rId,
                    RewardQty: rType == 0 ? (int)rVal : 0,
                    DiscountPercent: rType == 1 ? rVal : null,
                    DiscountAmount: rType == 2 ? rVal : null
                ));
            }

            var req = new CreatePromoReq(_txtPromoName.Text, _txtPromoDesc.Text, _cmbPromoType.SelectedIndex, _dtpStart.Value, _dtpEnd.Value, null, 1, rules);
            try
            {
                using var http = Http();
                HttpResponseMessage resp;
                if (_currentPromoId.HasValue) resp = await http.PutAsJsonAsync($"api/promotions/{_currentPromoId}", req);
                else resp = await http.PostAsJsonAsync("api/promotions", req);

                if (resp.IsSuccessStatusCode) { MessageBox.Show("تم حفظ العرض بنجاح"); await RefreshLists(); }
                else MessageBox.Show("خطأ: " + await resp.Content.ReadAsStringAsync());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DgvRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == _dgvRules.NewRowIndex) return;
            string colName = _dgvRules.Columns[e.ColumnIndex].Name;
            
            if (colName == "colPickCond")
            {
                string cond = _dgvRules.Rows[e.RowIndex].Cells["colCondition"].Value?.ToString() ?? "";
                if (cond == "أي منتج") { MessageBox.Show("العرض يطبق على كل المنتجات"); return; }
                if (cond == "براند محدد") { 
                    string brand = Microsoft.VisualBasic.Interaction.InputBox("ادخل اسم الشركة / البراند", "بحث");
                    if (!string.IsNullOrEmpty(brand)) _dgvRules.Rows[e.RowIndex].Cells["colCondValName"].Value = brand;
                    return; 
                }
                if (cond == "قسم محدد") { 
                    string cat = Microsoft.VisualBasic.Interaction.InputBox("ادخل اسم القسم", "بحث");
                    if (!string.IsNullOrEmpty(cat)) _dgvRules.Rows[e.RowIndex].Cells["colCondValName"].Value = cat;
                    return; 
                }
                
                if (cond == "مجموعة مخصصة") {
                    if (_collections.Count == 0) { MessageBox.Show("لايوجد مجموعات"); return; }
                    
                    // Dynamic Picker
                    var f = new Form() { Width = 300, Height = 150, Text = "اختر المجموعة", StartPosition = FormStartPosition.CenterParent, Font = this.Font, RightToLeft = RightToLeft.Yes, RightToLeftLayout = true };
                    var cmb = new ComboBox() { DataSource = _collections, DisplayMember = "Name", ValueMember = "Id", Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
                    var btn = new Button() { Text = "موافق", Dock = DockStyle.Bottom };
                    btn.Click += (s, ev) => f.DialogResult = DialogResult.OK;
                    f.Controls.Add(cmb); f.Controls.Add(btn);
                    
                    if (f.ShowDialog() == DialogResult.OK && cmb.SelectedItem is CollectionListDto c)
                    {
                        _dgvRules.Rows[e.RowIndex].Cells["colCondValName"].Value = c.Name;
                        _dgvRules.Rows[e.RowIndex].Cells["colCondValId"].Value = c.Id;
                    }
                    return;
                }

                using var fSales = new Frm_ItemSales();
                if (fSales.ShowDialog() == DialogResult.OK && fSales.SelectedProductId.HasValue)
                {
                    _dgvRules.Rows[e.RowIndex].Cells["colCondValId"].Value = fSales.SelectedProductId.Value;
                    _dgvRules.Rows[e.RowIndex].Cells["colCondValName"].Value = fSales.SelectedProductName;
                }
            }
            else if (colName == "colPickReward")
            {
                using var fSales = new Frm_ItemSales();
                if (fSales.ShowDialog() == DialogResult.OK && fSales.SelectedProductId.HasValue)
                {
                    _dgvRules.Rows[e.RowIndex].Cells["colRewardProdId"].Value = fSales.SelectedProductId.Value;
                    _dgvRules.Rows[e.RowIndex].Cells["colRewardProdName"].Value = fSales.SelectedProductName;
                }
            }
        }

        // ── BEHAVIOR: COLLECTIONS ──────────────────────────
        private void ResetCollection()
        {
            _currentCollectionId = null;
            _txtColName.Text = "";
            _dgvColItems.Rows.Clear();
        }

        private async Task LoadCollectionDetails()
        {
            if (_lstCollections.SelectedItem is not CollectionListDto c) return;
            try
            {
                using var http = Http();
                var cdet = await http.GetFromJsonAsync<CollectionDetailDto>($"api/promotions/collections/{c.Id}");
                if (cdet == null) return;
                ResetCollection();
                _currentCollectionId = cdet.Id;
                _txtColName.Text = cdet.Name;
                foreach(var i in cdet.Items)
                {
                    int r = _dgvColItems.Rows.Add();
                    _dgvColItems.Rows[r].Cells["colItemId"].Value = i.ProductId;
                    _dgvColItems.Rows[r].Cells["colItemCode"].Value = i.ProductCode;
                    _dgvColItems.Rows[r].Cells["colItemName"].Value = i.ProductName;
                }
            }
            catch { }
        }

        private async void BtnSaveCol_Click(object s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtColName.Text)) return;
            var ids = new List<Guid>();
            for(int i=0; i<_dgvColItems.Rows.Count; i++)
            {
                if (Guid.TryParse(_dgvColItems.Rows[i].Cells["colItemId"].Value?.ToString(), out var gid))
                    ids.Add(gid);
            }
            var req = new CreateColReq(_txtColName.Text, ids);

            try
            {
                using var http = Http();
                HttpResponseMessage resp;
                if (_currentCollectionId.HasValue) resp = await http.PutAsJsonAsync($"api/promotions/collections/{_currentCollectionId}", req);
                else resp = await http.PostAsJsonAsync("api/promotions/collections", req);

                if (resp.IsSuccessStatusCode) { MessageBox.Show("تم الحفظ"); await RefreshLists(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnSearchForCollection_Click(object s, EventArgs e)
        {
            using var fSales = new Frm_ItemSales();
            if (fSales.ShowDialog() == DialogResult.OK && fSales.SelectedProductId.HasValue)
            {
                // check if already added
                for(int i=0; i<_dgvColItems.Rows.Count; i++)
                {
                    if (_dgvColItems.Rows[i].Cells["colItemId"].Value?.ToString() == fSales.SelectedProductId.Value.ToString())
                    {
                        MessageBox.Show("المنتج مضاف بالفعل للمجموعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                int r = _dgvColItems.Rows.Add();
                _dgvColItems.Rows[r].Cells["colItemId"].Value = fSales.SelectedProductId.Value;
                _dgvColItems.Rows[r].Cells["colItemCode"].Value = fSales.SelectedProductBarcode;
                _dgvColItems.Rows[r].Cells["colItemName"].Value = fSales.SelectedProductName;
            }
        }

        private void DgvColItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _dgvColItems.Columns[e.ColumnIndex].Name == "colItemDel")
            {
                _dgvColItems.Rows.RemoveAt(e.RowIndex);
            }
        }
        
        // Removed unnecessary event handler declarations down here as lambdas handle most.
        private void _btnNewPromo_Click(object sender, EventArgs e) { ResetPromo(); }
        private void _btnNewCol_Click(object sender, EventArgs e) { ResetCollection(); }
        private async void _lstPromos_SelectedIndexChanged(object sender, EventArgs e) { await LoadPromoDetails(); }
        private async void _lstCollections_SelectedIndexChanged(object sender, EventArgs e) { await LoadCollectionDetails(); }
    }
}
