using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PharmaCore.POS.Enums;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_m3l8at : DevExpress.XtraEditors.XtraForm
    {
        DataTable tbl = new DataTable();
        DataTable tbl2 = new DataTable();

        private bool IsApi => ConnectionManager.CurrentMode == ConnectionMode.Api;

        private async Task<JToken> ApiGetAsync(string relative)
        {
            using (var http = ConnectionManager.GetHttpClient())
            {
                var res = await http.GetAsync(relative.StartsWith("/") ? relative.Substring(1) : relative);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                return JToken.Parse(json);
            }
        }

        public Frm_m3l8at()
        {
            InitializeComponent();
            
            // إضافة حدث استرجاع الفاتورة عند الضغط المزدوج
            DgvSearch.CellDoubleClick += DgvSearch_CellDoubleClick;
        }

        private void Frm_m3l8_Load(object sender, EventArgs e)
        {
            try
            {
                FromDate.Text = DateTime.Now.ToShortDateString();
                ToDate.Text = DateTime.Now.ToShortDateString();
                coType.SelectedIndex = 0;
                coMovement.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private async void Serach_Click(object sender, EventArgs e)
        {
            try
            {
                // مسح تفاصيل الفاتورة السابقة
                if (DgvDteils.DataSource != null) 
                    DgvDteils.DataSource = null;
                else 
                    DgvDteils.Rows.Clear();

                tbl.Rows.Clear();

                if (IsApi)
                {
                    // جلب المسودات فقط
                    string url = $"api/sales?status=Temp&from={FromDate.Value:yyyy-MM-dd}&to={ToDate.Value:yyyy-MM-dd}";
                    
                    var data = await ApiGetAsync(url);
                    if (data != null && data["items"] != null)
                    {
                        var items = data["items"].ToObject<JArray>();
                        var dt = new DataTable();
                        dt.Columns.Add("dgvOrder_ID", typeof(Guid));
                        dt.Columns.Add("Order_ID", typeof(string));
                        dt.Columns.Add("Date", typeof(string));
                        dt.Columns.Add("Saller", typeof(string));
                        dt.Columns.Add("TotalNeed", typeof(decimal));
                        dt.Columns.Add("n8dy", typeof(decimal));
                        dt.Columns.Add("visa", typeof(decimal));

                        foreach (var item in items)
                        {
                            dt.Rows.Add(
                                Guid.Parse(item["id"].ToString()),
                                item["invoiceNumber"]?.ToString() ?? "",
                                item["date"]?.ToString() ?? "",
                                item["sellerName"]?.ToString() ?? "",
                                Convert.ToDecimal(item["total"] ?? 0),
                                Convert.ToDecimal(item["paidCash"] ?? 0),
                                Convert.ToDecimal(item["paidVisa"] ?? 0)
                            );
                        }

                        DgvSearch.AutoGenerateColumns = false;
                        DgvSearch.DataSource = dt;
                    }
                }
                else
                {
                    MessageBox.Show("لابد من تفعيل خيار الـ API للبحث عن فواتيرك", "الوضع المباشر متوقف");
                }

                a3dad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في جلب الفواتير المعلقة: " + ex.Message);
            }

        }

        private async void DgvSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (DgvSearch.CurrentRow != null && DgvSearch.CurrentRow.Cells["dgvOrder_ID"].Value != null)
            {
                try
                {
                    Guid id = Guid.Parse(DgvSearch.CurrentRow.Cells["dgvOrder_ID"].Value.ToString());

                    if (IsApi)
                    {
                        var data = await ApiGetAsync($"api/sales/{id}");
                        if (data != null && data["lines"] != null)
                        {
                            var lines = data["lines"].ToObject<JArray>();
                            var dt = new DataTable();
                            dt.Columns.Add("Barcode", typeof(string));
                            dt.Columns.Add("Pro_Name", typeof(string));
                            dt.Columns.Add("Unit", typeof(string));
                            dt.Columns.Add("Price_Tax", typeof(decimal));
                            dt.Columns.Add("Qty", typeof(decimal));
                            dt.Columns.Add("Total_Bf", typeof(decimal));
                            dt.Columns.Add("Discount", typeof(decimal));
                            dt.Columns.Add("Total_Af", typeof(decimal));

                            foreach (var line in lines)
                            {
                                dt.Rows.Add(
                                    line["barcode"]?.ToString() ?? "",
                                    line["productName"]?.ToString() ?? "",
                                    line["unitName"]?.ToString() ?? "",
                                    Convert.ToDecimal(line["unitPrice"] ?? 0),
                                    Convert.ToDecimal(line["quantity"] ?? 0),
                                    Convert.ToDecimal(line["lineTotal"] ?? 0),
                                    Convert.ToDecimal(line["discount"] ?? 0),
                                    Convert.ToDecimal(line["lineTotalAfterDiscount"] ?? 0)
                                );
                            }

                            DgvDteils.AutoGenerateColumns = false;
                            DgvDteils.DataSource = dt;
                        }
                    }
                }
                catch { }
            }
        }

        private async void DgvSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && DgvSearch.Rows[e.RowIndex].Cells["dgvOrder_ID"].Value != null)
            {
                if (MessageBox.Show("هل تود استرجاع هذه الفاتورة المبدئية واستكمالها؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Guid id = Guid.Parse(DgvSearch.Rows[e.RowIndex].Cells["dgvOrder_ID"].Value.ToString());

                    var parentFrm = Application.OpenForms.OfType<Frm_Sales>().FirstOrDefault();
                    if (parentFrm != null)
                    {
                        var data = await ApiGetAsync($"api/sales/{id}");
                        if (data != null && data["lines"] != null)
                        {
                            // Clear existing rows
                            parentFrm.DgvSale.Rows.Clear();

                            var lines = data["lines"].ToObject<JArray>();
                            foreach (var line in lines)
                            {
                                int idx = parentFrm.DgvSale.Rows.Add();
                                parentFrm.DgvSale.Rows[idx].Cells["dgvID"].Value = 1; // dummy or fetch ID if returned
                                parentFrm.DgvSale.Rows[idx].Cells["dgvBar"].Value = line["barcode"]?.ToString();
                                parentFrm.DgvSale.Rows[idx].Cells["dgvName"].Value = line["productName"]?.ToString();
                                parentFrm.DgvSale.Rows[idx].Cells["dgvUnit"].Value = 1; // dummy or actual ID
                                parentFrm.DgvSale.Rows[idx].Cells["dgvUnit"].Value = line["unitName"]?.ToString();
                                parentFrm.DgvSale.Rows[idx].Cells["dgvQty"].Value = Convert.ToDecimal(line["quantity"] ?? 0);
                                parentFrm.DgvSale.Rows[idx].Cells["dgvStore"].Value = Convert.ToDecimal(line["stockQtyAtSale"] ?? 999);
                                parentFrm.DgvSale.Rows[idx].Cells["dgvPrice"].Value = Convert.ToDecimal(line["unitPrice"] ?? 0);
                                parentFrm.DgvSale.Rows[idx].Cells["dgvDiscount"].Value = Convert.ToDecimal(line["discount"] ?? 0);
                                parentFrm.DgvSale.Rows[idx].Cells["dgvBefor"].Value = Convert.ToDecimal(line["lineTotal"] ?? 0);
                                parentFrm.DgvSale.Rows[idx].Cells["dgvTotal"].Value = Convert.ToDecimal(line["lineTotalAfterDiscount"] ?? 0);
                            }

                            // Pass ID to main form 
                            parentFrm.CurrentTempSaleId = id;
                            parentFrm.CalculateDGV();

                            this.Close();
                        }
                    }
                }
            }
        }

        private void Coby_ID_Click(object sender, EventArgs e)
        {
            string coby = (DgvDteils.CurrentRow.Cells["dgvBarcod"].Value).ToString();
            Clipboard.SetText(coby);
        }

        private void MultiCoby_Click(object sender, EventArgs e)
        {
            // قائمة لحفظ كل الباركودات
            List<string> barcodeList = new List<string>();

            // افتراض أن اسم عمود الباركود هو dgvBar
            foreach (DataGridViewRow row in DgvDteils.Rows)
            {
                if (row.Cells["dgvBar"].Value != null) // التأكد من أن القيمة ليست فارغة
                {
                    barcodeList.Add(row.Cells["dgvBar"].Value.ToString());
                }
            }

            // حفظ الباركودات في متغير أو عرضها في `MessageBox`
            string barcodes = string.Join(", ", barcodeList);
            Clipboard.SetText(barcodes); // نسخها إلى الحافظة (Clipboard)
            MessageBox.Show("تم نسخ الاصناف:\n" + barcodes);
        }

        private void MutCob_Click(object sender, EventArgs e)
        {
            if (DgvDteils.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    DgvDteils.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;

                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(DgvDteils.GetClipboardContent());

                    // Replace the text box contents with the clipboard text.
                    //TextBox1.Text = Clipboard.GetText();
                }
                catch { }
            }
        }

        private void DgvSearch_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            a3dad();
        }

        private void a3dad()
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                decimal Need = 0; decimal n8dey = 0; decimal visa = 0;
                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    Need += Convert.ToDecimal(DgvSearch.Rows[i].Cells["dgvTotalNeed"].Value);
                    n8dey += Convert.ToDecimal(DgvSearch.Rows[i].Cells["dgvn8dy"].Value);
                    visa += Convert.ToDecimal(DgvSearch.Rows[i].Cells["dgvVisa"].Value);
                }

                try
                {
                    txtTotal.Text = (DgvSearch.Rows.Count).ToString();
                    txtNeed.Text = (Math.Round(Need, 2)).ToString();
                    txtN8dy.Text = (Math.Round(n8dey, 2)).ToString();
                    txtVisa.Text = (Math.Round(visa, 2)).ToString();
                }
                catch (Exception) { }
            }
        }

        private void Frm_m3l8at_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Serach_Click(null,null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                //PayOrder();
            }
        }
    }
}