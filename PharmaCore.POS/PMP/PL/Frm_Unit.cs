using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;using System.Windows.Forms;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Unit : Form
    {
        private Guid? _selectedId = null;
        private List<UnitDto> _units = new List<UnitDto>();

        public Frm_Unit()
        {
            InitializeComponent();
            SetupGrid();
            this.Load += async (s, e) => 
            {
                AuditLogger.Log("ScreenAccess", "System", this.Name, "فتح شاشة الوحدات");
                await LoadDataAsync();
            };
            this.btnNew.Click += (s, e) => PrepareNew();
            this.btnSave.Click += async (s, e) => await SaveAsync();
            this.btnDelete.Click += async (s, e) => await DeleteAsync();
            this.dgvUnits.DoubleClick += DgvUnits_DoubleClick;
            this.dgvUnits.CellClick += DgvUnits_CellClick;

            PrepareNew();
        }

        private void SetupGrid()
        {
            dgvUnits.AutoGenerateColumns = false;
            dgvUnits.Columns.Clear();
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "الوحدة" });
            dgvUnits.Columns.Add(new DataGridViewTextBoxColumn { Name = "Abbreviation", DataPropertyName = "Abbreviation", HeaderText = "الاختصار" });
        }


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

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            var client = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
            string token = Properties.Settings.Default.ApiToken;
            
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var http = Http();
                var result = await http.GetFromJsonAsync<List<UnitDto>>("api/units");
                if (result != null)
                {
                    _units = result;
                    dgvUnits.DataSource = _units;
                    lblTotalCount.Text = $"الإجمالي: {_units.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الاتصال: {ex.Message}");
            }
        }

        private void PrepareNew()
        {
            _selectedId = null;
            txtUnitName.Text = "";
            btnDelete.Enabled = false;
            btnDelete.BackColor = Color.Gray;
            txtUnitName.Focus();
        }

        private async Task SaveAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUnitName.Text))
                {
                    MessageBox.Show("من فضلك أدخل اسم الوحدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSave.Enabled = false;
                using var http = Http();
                var req = new { Name = txtUnitName.Text };

                if (_selectedId == null)
                {
                    var res = await http.PostAsJsonAsync("api/units", req);
                    if (res.IsSuccessStatusCode)
                    {
                        var created = await res.Content.ReadFromJsonAsync<UnitDto>();
                        AuditLogger.Log("إضافة", "ProductUnits", this.Name, $"تمت إضافة وحدة جديدة: {txtUnitName.Text}", 0, "ProductUnits", created?.Id.ToString() ?? "");
                        MessageBox.Show("تم الحفظ بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(await res.Content.ReadAsStringAsync());
                    }
                }
                else
                {
                    var res = await http.PutAsJsonAsync($"api/units/{_selectedId}", req);
                    if (res.IsSuccessStatusCode)
                    {
                        AuditLogger.Log("تعديل", "ProductUnits", this.Name, $"تم تعديل وحدة إلى: {txtUnitName.Text}", 0, "ProductUnits", _selectedId.ToString() ?? "");
                        MessageBox.Show("تم التعديل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(await res.Content.ReadAsStringAsync());
                    }
                }

                await LoadDataAsync();
                PrepareNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private async Task DeleteAsync()
        {
            if (_selectedId == null) return;
            var confirm = MessageBox.Show("هل أنت متأكد من مسح هذه الوحدة؟", "تأكيد المسح", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                btnDelete.Enabled = false;
                using var http = Http();
                var res = await http.DeleteAsync($"api/units/{_selectedId}");
                if (res.IsSuccessStatusCode)
                {
                    AuditLogger.Log("حذف", "ProductUnits", this.Name, $"تم حذف وحدة: {txtUnitName.Text}", 0, "ProductUnits", _selectedId.ToString() ?? "");
                    MessageBox.Show("تم المسح بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    PrepareNew();
                }
                else
                {
                    MessageBox.Show(await res.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}");
            }
            finally
            {
                if (_selectedId != null) btnDelete.Enabled = true;
            }
        }

        private void DgvUnits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectRow(e.RowIndex);
        }

        private void DgvUnits_DoubleClick(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow != null) SelectRow(dgvUnits.CurrentRow.Index);
        }

        private void SelectRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUnits.Rows.Count) return;
            var row = dgvUnits.Rows[rowIndex];
            var item = row.DataBoundItem as UnitDto;
            if (item != null)
            {
                _selectedId = item.Id;
                txtUnitName.Text = item.Name;
                btnDelete.Enabled = true;
                btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            }
        }

        // Removed old LogAuditAsync method
        public class UnitDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Abbreviation { get; set; }
        }
    }
}