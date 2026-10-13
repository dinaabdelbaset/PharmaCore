using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ProductActive : Form
    {
        private Guid? _currentEditingId = null;
        private List<LookupItemDto> _currentItems = new List<LookupItemDto>();
        private ActiveMode _currentMode = ActiveMode.ActiveIngredient;

        private enum ActiveMode
        {
            ActiveIngredient,
            Usage,
            Company
        }

        public class LookupItemDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        private class ApiNameReq
        {
            public string Name { get; set; }
        }

        public Frm_ProductActive()
        {
            InitializeComponent();
            SetupGrid();
            this.Load += async (s, e) => {
                AuditLogger.Log("ScreenAccess", "System", this.Name, "فتح شاشة تفاصيل المنتج");
                SetupSidebar();
                await SwitchModeAsync(ActiveMode.ActiveIngredient);
            };

            btnNew.Click += BtnNew_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            dgvItems.CellDoubleClick += DgvItems_CellDoubleClick;
        }

        private void SetupGrid()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "الإسم" });
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

        private void SetupSidebar()
        {
            btnNavActiveIngredient.Click += async (s, e) => await SwitchModeAsync(ActiveMode.ActiveIngredient);
            btnNavUsage.Click += async (s, e) => await SwitchModeAsync(ActiveMode.Usage);
            btnNavCompany.Click += async (s, e) => await SwitchModeAsync(ActiveMode.Company);
        }

        private async Task SwitchModeAsync(ActiveMode newMode)
        {
            _currentMode = newMode;

            // Highlight sidebar
            btnNavActiveIngredient.BackColor = _currentMode == ActiveMode.ActiveIngredient ? Color.FromArgb(52, 152, 219) : Color.Transparent;
            btnNavUsage.BackColor = _currentMode == ActiveMode.Usage ? Color.FromArgb(52, 152, 219) : Color.Transparent;
            btnNavCompany.BackColor = _currentMode == ActiveMode.Company ? Color.FromArgb(52, 152, 219) : Color.Transparent;

            // Title
            switch (_currentMode)
            {
                case ActiveMode.ActiveIngredient: lblTitle.Text = "المواد الفعالة للأدوية"; break;
                case ActiveMode.Usage: lblTitle.Text = "استخدامات الـدواء"; break;
                case ActiveMode.Company: lblTitle.Text = "الشركات والمصانع المصنعة"; break;
            }
            
            AuditLogger.Log("Activity", "System", this.Name, $"الانتقال إلى: {lblTitle.Text}");

            PrepareNew();
            await LoadDataAsync();
        }

        private string GetCurrentEndpoint()
        {
            switch (_currentMode)
            {
                case ActiveMode.ActiveIngredient: return "api/active-ingredients";
                case ActiveMode.Usage: return "api/usages";
                case ActiveMode.Company: return "api/companies";
                default: return "api/active-ingredients";
            }
        }

        // Removed old LogAuditAsync method

        private async Task LoadDataAsync()
        {
            try
            {
                using var http = Http();
                var res = await http.GetAsync(GetCurrentEndpoint());
                if (res.IsSuccessStatusCode)
                {
                    _currentItems = await res.Content.ReadFromJsonAsync<List<LookupItemDto>>() ?? new List<LookupItemDto>();
                    dgvItems.DataSource = _currentItems;
                    lblTotalCount.Text = $"الإجمالي: {_currentItems.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrepareNew()
        {
            _currentEditingId = null;
            txtItemName.Text = "";
            btnNew.Enabled = true;
            btnSave.Text = "إضافة جديد";
            txtItemName.Focus();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            PrepareNew();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtItemName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("يرجى إدخال الإسم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemName.Focus();
                return;
            }

            try
            {
                btnSave.Enabled = false;
                using var http = Http();
                var req = new ApiNameReq { Name = name };

                if (_currentEditingId.HasValue)
                {
                    // Update
                    var res = await http.PutAsJsonAsync($"{GetCurrentEndpoint()}/{_currentEditingId.Value}", req);
                    if (res.IsSuccessStatusCode)
                    {
                        AuditLogger.Log("تعديل", _currentMode.ToString(), this.Name, $"{name} تعديل", 0, _currentMode.ToString(), _currentEditingId.Value.ToString());
                        MessageBox.Show("تم التعديل بنجاح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PrepareNew();
                        await LoadDataAsync();
                    }
                    else
                    {
                        string err = await res.Content.ReadAsStringAsync();
                        MessageBox.Show("خطأ في التعديل: " + err, "تنبيه");
                    }
                }
                else
                {
                    // Add
                    var res = await http.PostAsJsonAsync(GetCurrentEndpoint(), req);
                    if (res.IsSuccessStatusCode)
                    {
                        var created = await res.Content.ReadFromJsonAsync<LookupItemDto>();
                        if (created != null)
                        {
                            AuditLogger.Log("إضافة", _currentMode.ToString(), this.Name, $"{name} إضافة", 0, _currentMode.ToString(), created.Id.ToString());
                        }
                        MessageBox.Show("تمت الإضافة بنجاح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PrepareNew();
                        await LoadDataAsync();
                    }
                    else
                    {
                        string err = await res.Content.ReadAsStringAsync();
                        MessageBox.Show("خطأ في الإضافة: " + err, "تنبيه");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد عنصر للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvItems.SelectedRows[0].DataBoundItem is LookupItemDto item)
            {
                var rs = MessageBox.Show($"هل أنت متأكد من مسح '{item.Name}'؟", "تأكيد المسح", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    try
                    {
                        btnDelete.Enabled = false;
                        using var http = Http();
                        var res = await http.DeleteAsync($"{GetCurrentEndpoint()}/{item.Id}");
                        if (res.IsSuccessStatusCode)
                        {
                            AuditLogger.Log("مسح", _currentMode.ToString(), this.Name, $"{item.Name} حذف", 0, _currentMode.ToString(), item.Id.ToString());
                            MessageBox.Show("تم المسح بنجاح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            PrepareNew();
                            await LoadDataAsync();
                        }
                        else
                        {
                            MessageBox.Show("حدث خطأ أثناء المسح", "تنبيه");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
        }

        private void DgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvItems.Rows[e.RowIndex].DataBoundItem is LookupItemDto item)
            {
                _currentEditingId = item.Id;
                txtItemName.Text = item.Name;
                btnSave.Text = "حفظ التعديل";
            }
        }
    }
}