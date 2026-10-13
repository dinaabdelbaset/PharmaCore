using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services.Configuration;
using System.Text.Json;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Store : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _storeId;
        private List<WarehouseDto> _allStores = new List<WarehouseDto>();
        private Panel _pnlBlackout;

        public Frm_Store()
        {
            InitializeComponent();
            InitializeBlackout();
            DgvSearch.CellClick += DgvSearch_CellClick;
        }

        private void InitializeBlackout()
        {
            _pnlBlackout = new Panel
            {
                BackColor = Color.FromArgb(150, 0, 0, 0),
                Dock = DockStyle.Fill,
                Visible = false
            };
            var lblLoad = new Label
            {
                Text = "جاري التحميل...",
                Font = new Font("Tahoma", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = true
            };
            lblLoad.Location = new Point((Width - lblLoad.Width) / 2, (Height - lblLoad.Height) / 2);
            _pnlBlackout.Controls.Add(lblLoad);
            lblLoad.Anchor = AnchorStyles.None;
            // Center it manually when resizing
            _pnlBlackout.Resize += (s, e) => {
                lblLoad.Location = new Point((_pnlBlackout.Width - lblLoad.Width) / 2, (_pnlBlackout.Height - lblLoad.Height) / 2);
            };
            Controls.Add(_pnlBlackout);
        }

        public void ShowBlackout() { _pnlBlackout.BringToFront(); _pnlBlackout.Visible = true; Application.DoEvents(); }
        public void HideBlackout() { _pnlBlackout.Visible = false; }

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

        private async void Frm_Store_Load(object sender, EventArgs e)
        {
            DgvSearch.Columns.Clear();
            DgvSearch.Columns.Add("colId", "ID");
            DgvSearch.Columns["colId"].Visible = false;
            DgvSearch.Columns.Add("colName", "اسم المخزن");
            
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            ShowBlackout();
            try
            {
                using var http = Http();
                _allStores = await http.GetFromJsonAsync<List<WarehouseDto>>("api/warehouses") ?? new List<WarehouseDto>();
                
                DgvSearch.Rows.Clear();
                foreach (var store in _allStores)
                {
                    int r = DgvSearch.Rows.Add();
                    DgvSearch.Rows[r].Cells["colId"].Value = store.Id.ToString();
                    DgvSearch.Rows[r].Cells["colName"].Value = store.Name;
                }
                
                txtTotal.Text = _allStores.Count.ToString();
                PrepareNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideBlackout();
            }
        }

        private void PrepareNew()
        {
            _storeId = null;
            txtID.Text = "(جديد)";
            txtName.Text = "";
            
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PrepareNew();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("من فضلك ادخل اسم المخزن");
                return;
            }

            ShowBlackout();
            try
            {
                var req = new { Name = txtName.Text.Trim(), BranchId = (Guid?)null };
                using var http = Http();
                var resp = await http.PostAsJsonAsync("api/warehouses", req);
                
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("تم الادخال بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}");
            }
            finally
            {
                HideBlackout();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!_storeId.HasValue) return;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("من فضلك ادخل اسم المخزن");
                return;
            }

            ShowBlackout();
            try
            {
                var req = new { Name = txtName.Text.Trim(), BranchId = (Guid?)null };
                using var http = Http();
                var resp = await http.PutAsJsonAsync($"api/warehouses/{_storeId.Value}", req);
                
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("تم تعديل البيانات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}");
            }
            finally
            {
                HideBlackout();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_storeId.HasValue)
            {
                MessageBox.Show("لا يوجد مخزن لحذفه");
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من مسح المخزن المحدد بشكل دائم ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            ShowBlackout();
            try
            {
                using var http = Http();
                var resp = await http.DeleteAsync($"api/warehouses/{_storeId.Value}");
                
                if (resp.IsSuccessStatusCode)
                {
                    MessageBox.Show("تم المسح بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show($"خطأ: {await resp.Content.ReadAsStringAsync()}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}");
            }
            finally
            {
                HideBlackout();
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            EditSelectedRow();
        }

        private void DgvSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EditSelectedRow();
        }

        private void EditSelectedRow()
        {
            if (DgvSearch.CurrentRow == null) return;
            try
            {
                var idStr = DgvSearch.CurrentRow.Cells["colId"].Value?.ToString();
                if (Guid.TryParse(idStr, out Guid id))
                {
                    _storeId = id;
                    txtID.Text = id.ToString().Substring(0, 8); // Just show a short version
                    txtName.Text = DgvSearch.CurrentRow.Cells["colName"].Value?.ToString() ?? "";
                    
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = false;
                }
            }
            catch { }
        }
        
        public class WarehouseDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid? BranchId { get; set; }
        }
    }
}
