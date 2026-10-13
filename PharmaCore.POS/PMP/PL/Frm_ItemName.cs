using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services.Configuration;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ItemName : DevExpress.XtraEditors.XtraForm
    {
        public string? SelectedNameEn { get; private set; }
        public string? SelectedNameAr { get; private set; }
        public string? SelectedActiveIngredient { get; private set; }
        public string? SelectedUsage { get; private set; }
        public string? SelectedCompany { get; private set; }

        public Frm_ItemName()
        {
            InitializeComponent();
            
            btnSearch.Click += BtnSearch_Click;
            btnExal.Click += BtnExal_Click;
            txtName.KeyDown += TxtName_KeyDown;
            DgvSearch.CellDoubleClick += DgvSearch_CellDoubleClick;
            DgvSearch.CellContentClick += DgvSearch_CellContentClick;
            
            if (btnExport != null) btnExport.Click += BtnExport_Click;
            if (btnClearAll != null) btnClearAll.Click += BtnClearAll_Click;
            
            this.Load += Frm_ItemName_Load;
        }

        private Form? _dimmer;

        private void ShowBlackout()
        {
            _dimmer = new Form
            {
                StartPosition = FormStartPosition.Manual,
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.5d,
                BackColor = System.Drawing.Color.Black,
                Size = this.Size,
                Location = this.PointToScreen(System.Drawing.Point.Empty)
            };
            _dimmer.Show(this);
        }

        private void HideBlackout()
        {
            if (_dimmer != null)
            {
                _dimmer.Close();
                _dimmer = null;
            }
        }

        private void Frm_ItemName_Load(object sender, EventArgs e)
        {
            txtName.Focus();
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

            var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            var client = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
            var token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                DgvSearch.Rows.Clear();

                string query = txtName.Text.Trim();
                using var http = Http();
                var results = await http.GetFromJsonAsync<List<StandardItemDto>>($"api/standard-items/search?q={Uri.EscapeDataString(query)}");

                if (results != null)
                {
                    foreach (var item in results)
                    {
                        DgvSearch.Rows.Add(
                            item.Id,
                            item.NameEn,
                            item.NameAr,
                            item.ActiveIngredient,
                            item.Usage,
                            item.Company
                        );
                    }
                }
                
                if (lblCount != null) lblCount.Text = $"عدد المنتجات: {DgvSearch.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                DgvSearch.Focus();
                HideBlackout();
            }
        }

        private void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                BtnSearch_Click(sender, e);
            }
        }

        private async void BtnExal_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx;*.xls";
            ofd.Title = "اختر ملف الإكسل للأصناف القياسية";

            // Set default initial directory if it exists
            string defaultDir = @"D:\M7.T.S. Project";
            if (Directory.Exists(defaultDir))
                ofd.InitialDirectory = defaultDir;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ShowBlackout();
                try
                {
                    btnExal.Enabled = false;
                    btnExal.Text = "جاري الرفع...";

                    using var http = Http();
                    using var content = new MultipartFormDataContent();

                    // Use FileShare.ReadWrite to allow reading even if the file is open in Excel
                    var fileStream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    content.Add(fileContent, "file", Path.GetFileName(ofd.FileName));

                    var response = await http.PostAsync("api/standard-items/import", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("تم استيراد الملف بنجاح وحفظ الأصناف في السيرفر.", "تم بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnSearch_Click(null, EventArgs.Empty); // Refresh
                    }
                    else
                    {
                        string err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"فشل في رفع الملف:\n{err}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء رفع الملف:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnExal.Enabled = true;
                    btnExal.Text = "استيراد ملف إكسل";
                    HideBlackout();
                }
            }
        }

        private void DgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DgvSearch.Rows.Count > 0)
            {
                var row = DgvSearch.Rows[e.RowIndex];
                SelectedNameEn = row.Cells["colNameEn"].Value?.ToString();
                SelectedNameAr = row.Cells["colNameAr"].Value?.ToString();
                SelectedActiveIngredient = row.Cells["colActive"].Value?.ToString();
                SelectedUsage = row.Cells["colUsage"].Value?.ToString();
                SelectedCompany = row.Cells["colCompany"].Value?.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private async void BtnExport_Click(object? sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";
            sfd.FileName = "StandardItems_Export.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ShowBlackout();
                try
                {
                    using var http = Http();
                    var bytes = await http.GetByteArrayAsync("api/standard-items/export");
                    File.WriteAllBytes(sfd.FileName, bytes);
                    MessageBox.Show("تم تحميل الملف بنجاح!", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"عذراً، حدث خطأ أثناء التنزيل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HideBlackout();
                }
            }
        }

        private async void BtnClearAll_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من مسح جميع الأصناف القياسية بالكامل؟\nهذا الإجراء لا يمكن التراجع عنه!", "تحذير هام", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ShowBlackout();
                try
                {
                    using var http = Http();
                    var response = await http.DeleteAsync("api/standard-items/clear");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("تم مسح جميع البيانات بنجاح.", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnSearch_Click(null, EventArgs.Empty);
                    }
                    else
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"عذراً، حدث خطأ:\n{err}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"فشل في الاتصال بالخادم:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HideBlackout();
                }
            }
        }

        private async void DgvSearch_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DgvSearch.Columns[e.ColumnIndex].Name == "colDelete")
            {
                var idVal = DgvSearch.Rows[e.RowIndex].Cells["colId"].Value;
                if (idVal == null) return;
                
                if (MessageBox.Show("هل أنت متأكد من مسح هذا الصنف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ShowBlackout();
                    try
                    {
                        using var http = Http();
                        var response = await http.DeleteAsync($"api/standard-items/{idVal}");
                        if (response.IsSuccessStatusCode)
                        {
                            DgvSearch.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"خطأ:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        HideBlackout();
                    }
                }
            }
        }

    }

    public class StandardItemDto
    {
        public Guid Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? ActiveIngredient { get; set; }
        public string? Usage { get; set; }
        public string? Company { get; set; }
    }
}