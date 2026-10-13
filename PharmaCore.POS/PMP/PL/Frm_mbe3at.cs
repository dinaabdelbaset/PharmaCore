using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PharmaCore.POS.Enums;
using PharmaCore.POS.Services;
using PharmaCore.POS.RE;

namespace PharmaCore.POS.PL
{
    public partial class Frm_mbe3at : DevExpress.XtraEditors.XtraForm
    {

        DataTable tbl = new DataTable();
        DataTable tbl2 = new DataTable();
        BL.DDatabase db = new BL.DDatabase();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);

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

        public Frm_mbe3at()
        {
            InitializeComponent();
            this.Print.Click += new System.EventHandler(this.Print_Click);
            this.DgvSearch.SelectionChanged += new System.EventHandler(this.DgvSearch_SelectionChanged);
            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DgvSearch);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DgvDteils);
        }

        private void Frm_mbe3at_Load(object sender, EventArgs e)
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
                // Clear totals
                txtTotal.Text = "0";
                txtNeed.Text = "0";
                txtN8dy.Text = "0";
                txtVisa.Text = "0";

                var queryParams = new List<string>();

                // dates
                queryParams.Add($"from={FromDate.Value:yyyy-MM-dd}");
                queryParams.Add($"to={ToDate.Value:yyyy-MM-dd}");

                if (coType.SelectedIndex > 0 && coType.SelectedItem != null)
                {
                    queryParams.Add($"saleType={Uri.EscapeDataString(coType.SelectedItem.ToString())}");
                }

                if (coMovement.SelectedIndex > 0 && coMovement.SelectedItem != null)
                {
                    queryParams.Add($"movementType={Uri.EscapeDataString(coMovement.SelectedItem.ToString())}");
                }

                if (!string.IsNullOrWhiteSpace(SellerCode.Text))
                {
                    queryParams.Add($"sellerCode={Uri.EscapeDataString(SellerCode.Text)}");
                }

                if (!string.IsNullOrWhiteSpace(ItemBarcode.Text))
                {
                    queryParams.Add($"barcode={Uri.EscapeDataString(ItemBarcode.Text)}");
                }

                if (!string.IsNullOrWhiteSpace(CustomerCode.Text))
                {
                    queryParams.Add($"customerCode={Uri.EscapeDataString(CustomerCode.Text)}");
                }

                if (!string.IsNullOrWhiteSpace(Phone.Text))
                {
                    queryParams.Add($"phone={Uri.EscapeDataString(Phone.Text)}");
                }

                if (!string.IsNullOrWhiteSpace(Drg.Text))
                {
                    queryParams.Add($"invoiceNumber={Uri.EscapeDataString(Drg.Text)}");
                }

                string queryString = string.Join("&", queryParams);
                string url = "api/sales" + (queryParams.Count > 0 ? "?" + queryString : "");

                using (var http = ConnectionManager.GetHttpClient())
                {
                    var res = await http.GetAsync(url);
                    res.EnsureSuccessStatusCode();
                    var json = await res.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);

                    if (data != null && data["items"] != null)
                    {
                        var items = data["items"].ToObject<JArray>();
                        var dt = new DataTable();
                        dt.Columns.Add("Guid_ID", typeof(string));
                        dt.Columns.Add("Order_ID", typeof(string));
                        dt.Columns.Add("Date", typeof(string));
                        dt.Columns.Add("Us_Name", typeof(string));
                        dt.Columns.Add("Saller", typeof(string));
                        dt.Columns.Add("NO_mtrg3", typeof(string));
                        dt.Columns.Add("mrtg3", typeof(string));
                        dt.Columns.Add("type", typeof(string));
                        dt.Columns.Add("SubType", typeof(string));
                        dt.Columns.Add("TotalOrder", typeof(decimal));
                        dt.Columns.Add("TotalNeed", typeof(decimal));
                        dt.Columns.Add("n8dy", typeof(decimal));
                        dt.Columns.Add("visa", typeof(decimal));
                        dt.Columns.Add("rsed_mdfo3", typeof(decimal));
                        dt.Columns.Add("baky", typeof(decimal));
                        dt.Columns.Add("mrtg3_mony", typeof(decimal));
                        dt.Columns.Add("Customer", typeof(string));
                        dt.Columns.Add("Cus_cod", typeof(string));
                        dt.Columns.Add("drg_NO", typeof(string));
                        dt.Columns.Add("precenteg", typeof(decimal));
                        dt.Columns.Add("phone", typeof(string));
                        dt.Columns.Add("sell_not", typeof(string));
                        dt.Columns.Add("df3_Date", typeof(string));
                        dt.Columns.Add("location", typeof(string));
                        dt.Columns.Add("emp_need", typeof(decimal));

                        foreach (var item in items)
                        {
                            dt.Rows.Add(
                                item["id"]?.ToString() ?? "",
                                item["invoiceNumber"]?.ToString() ?? "",
                                item["date"]?.ToString() ?? "",
                                item["sellerName"]?.ToString() ?? "",
                                item["sellerCode"]?.ToString() ?? "",
                                item["no_mtrg3"]?.ToString() ?? "",
                                item["mrtg3"]?.ToString() ?? "",
                                item["saleType"]?.ToString() ?? "",
                                item["movementType"]?.ToString() ?? "",
                                Convert.ToDecimal(item["subtotal"] ?? 0),
                                Convert.ToDecimal(item["total"] ?? 0),
                                Convert.ToDecimal(item["paidCash"] ?? 0),
                                Convert.ToDecimal(item["paidVisa"] ?? 0),
                                Convert.ToDecimal(item["paidFromBalance"] ?? 0),
                                Convert.ToDecimal(item["remaining"] ?? 0),
                                Convert.ToDecimal(item["mrtg3_mony"] ?? 0),
                                item["customerName"]?.ToString() ?? "",
                                item["customerCode"]?.ToString() ?? "",
                                item["drg_NO"]?.ToString() ?? "",
                                Convert.ToDecimal(item["discountPercent"] ?? 0),
                                item["customerPhone"]?.ToString() ?? "",
                                item["notes"]?.ToString() ?? "",
                                item["reminderDate"]?.ToString() ?? "",
                                item["deliveryAddress"]?.ToString() ?? "",
                                Convert.ToDecimal(item["emp_need"] ?? 0)
                            );
                        }

                        DgvSearch.AutoGenerateColumns = false;
                        DgvSearch.DataSource = dt;
                    }
                    else
                    {
                        DgvSearch.DataSource = null;
                    }
                }

                a3dad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في البحث عن الفواتير: " + ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Print_Click(object sender, EventArgs e)
        {
            if (DgvSearch.CurrentRow == null)
            {
                MessageBox.Show("من فضلك اختر الفاتورة المراد طباعتها أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cellVal = DgvSearch.CurrentRow.Cells["dgvOrder_ID"].Value;
            if (cellVal == null || string.IsNullOrWhiteSpace(cellVal.ToString()))
            {
                MessageBox.Show("لم يتم العثور على معرف الفاتورة المحددة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Guid.TryParse(cellVal.ToString(), out Guid saleId))
            {
                MessageBox.Show("معرف الفاتورة غير صالح. يجب أن يكون معرفاً فريداً (Guid).", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int printNum = Properties.Settings.Default.SalesPrintNum;
                if (printNum <= 0) printNum = 1;

                for (int i = 0; i < printNum; i++)
                {
                    await PrintApiAsync(saleId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء محاولة الطباعة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PrintApiAsync(Guid saleId)
        {
            try
            {
                var saleData = await ApiGetAsync($"api/sales/{saleId}");
                if (saleData == null) return;

                // Create DataTable matching the old SQL structure
                DataTable tblRpt = new DataTable();
                tblRpt.Columns.Add("رقم الفاتورة", typeof(string));
                tblRpt.Columns.Add("اسم العميل", typeof(string));
                tblRpt.Columns.Add("المنتج", typeof(string));
                tblRpt.Columns.Add("الكمية", typeof(decimal));
                tblRpt.Columns.Add("الوحدة", typeof(string));
                tblRpt.Columns.Add("السعر شامل الضريبة", typeof(decimal));
                tblRpt.Columns.Add("الخصم", typeof(decimal));
                tblRpt.Columns.Add("الاجمالي", typeof(decimal));
                tblRpt.Columns.Add("اجمالي الفاتورة", typeof(decimal));
                tblRpt.Columns.Add("المبلغ المدفوع", typeof(decimal));
                tblRpt.Columns.Add("المتبقي", typeof(decimal));
                tblRpt.Columns.Add("الكاشير", typeof(string));
                tblRpt.Columns.Add("التاريخ", typeof(string));
                tblRpt.Columns.Add("الضريبة", typeof(decimal));
                tblRpt.Columns.Add("إجمالي المطلوب", typeof(decimal));
                tblRpt.Columns.Add("الوقت", typeof(string));
                tblRpt.Columns.Add("Name", typeof(string));
                tblRpt.Columns.Add("Address", typeof(string));
                tblRpt.Columns.Add("Description", typeof(string));
                tblRpt.Columns.Add("Phone1", typeof(string));
                tblRpt.Columns.Add("Phone2", typeof(string));

                string invNum = saleData["invoiceNumber"]?.ToString() ?? "";
                string custName = saleData["customerName"]?.ToString() ?? "";
                decimal totalOrd = Convert.ToDecimal(saleData["subtotal"] ?? 0);
                decimal totalNeed = Convert.ToDecimal(saleData["total"] ?? 0);
                decimal paidCash = Convert.ToDecimal(saleData["paidCash"] ?? 0) + Convert.ToDecimal(saleData["paidVisa"] ?? 0);
                decimal baky = Convert.ToDecimal(saleData["remaining"] ?? 0);
                string seller = Properties.Settings.Default.USERNAME;
                DateTime saleDate = Convert.ToDateTime(saleData["date"]);
                string dtStr = saleDate.ToString("yyyy-MM-dd");
                string tmStr = saleDate.ToString("hh:mm tt");

                // Get local company data (OrderPrintData)
                string compName = AppSettingsStore.Get("InvoiceName") ?? "";
                string compAddress = AppSettingsStore.Get("InvoiceAddress") ?? "";
                string compDesc = AppSettingsStore.Get("InvoiceDescription") ?? "";
                string compPhone1 = AppSettingsStore.Get("InvoicePhone1") ?? "";
                string compPhone2 = AppSettingsStore.Get("InvoicePhone2") ?? "";
                byte[] logoBytes = null;
                try
                {
                    string logoPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore", "invoice_logo.png");
                    if (File.Exists(logoPath))
                    {
                        logoBytes = File.ReadAllBytes(logoPath);
                    }
                }
                catch { }

                // Fallback to database just in case
                if (string.IsNullOrEmpty(compName))
                {
                    try
                    {
                        DataTable dtComp = db.readData("SELECT * FROM OrderPrintData", "");
                        if (dtComp.Rows.Count > 0)
                        {
                            var r = dtComp.Rows[0];
                            compName = r["Name"]?.ToString() ?? "";
                            compAddress = r["Address"]?.ToString() ?? "";
                            compDesc = r["Description"]?.ToString() ?? "";
                            compPhone1 = r["Phone1"]?.ToString() ?? "";
                            compPhone2 = r["Phone2"]?.ToString() ?? "";
                            if (r["Logo"] != DBNull.Value) logoBytes = (byte[])r["Logo"];
                        }
                    }
                    catch { }
                }

                var lines = saleData["lines"]?.ToObject<JArray>();
                if (lines != null)
                {
                    foreach (var line in lines)
                    {
                        tblRpt.Rows.Add(
                            invNum, custName,
                            line["productName"]?.ToString() ?? "",
                            Convert.ToDecimal(line["quantity"] ?? 0),
                            line["unitName"]?.ToString() ?? "",
                            Convert.ToDecimal(line["unitPrice"] ?? 0),
                            Convert.ToDecimal(line["discount"] ?? 0),
                            Convert.ToDecimal(line["lineTotalAfterDiscount"] ?? 0),
                            totalOrd, paidCash, baky, seller,
                            dtStr,
                            Convert.ToDecimal(line["taxValue"] ?? 0),
                            totalNeed, tmStr,
                            compName, compAddress, compDesc, compPhone1, compPhone2
                        );
                    }
                }

                Image logoImg = null;
                if (logoBytes != null)
                {
                    using (var ms = new MemoryStream(logoBytes))
                    {
                        logoImg = Image.FromStream(ms);
                    }
                }

                PharmaCore.POS.RE.rpt_viewer rv = new PharmaCore.POS.RE.rpt_viewer();

                if (Properties.Settings.Default.SalePrintKind == "8CM")
                {
                    if (Properties.Settings.Default.Show_Discount == false)
                    {
                        PharmaCore.POS.RE.RptOrderSales1 rpt = new PharmaCore.POS.RE.RptOrderSales1();
                        Services.ReportManager.LoadCustomLayoutIfExists(rpt, "RptOrderSales1");
                        if (logoImg != null) { try { ((DevExpress.XtraReports.UI.XRPictureBox)rpt.FindControl("Logo1", true)).Image = logoImg; } catch { } }
                        rpt.DataSource = tblRpt;
                        rpt.RequestParameters = false;
                        rv.documentViewer1.DocumentSource = rpt;
                        rpt.Print(Properties.Settings.Default.PrinterName1);
                    }
                    else
                    {
                        PharmaCore.POS.RE.RptOrderSales2 rpt = new PharmaCore.POS.RE.RptOrderSales2();
                        Services.ReportManager.LoadCustomLayoutIfExists(rpt, "RptOrderSales2");
                        if (logoImg != null) { try { ((DevExpress.XtraReports.UI.XRPictureBox)rpt.FindControl("Logo1", true)).Image = logoImg; } catch { } }
                        rpt.DataSource = tblRpt;
                        rpt.RequestParameters = false;
                        rv.documentViewer1.DocumentSource = rpt;
                        rpt.Print(Properties.Settings.Default.PrinterName1);
                    }
                }
                else if (Properties.Settings.Default.SalePrintKind == "A4")
                {
                    PharmaCore.POS.RE.RptOrderSalesA41 rpt = new PharmaCore.POS.RE.RptOrderSalesA41();
                    Services.ReportManager.LoadCustomLayoutIfExists(rpt, "RptOrderSalesA41");
                    if (logoImg != null) { try { ((DevExpress.XtraReports.UI.XRPictureBox)rpt.FindControl("Logo1", true)).Image = logoImg; } catch { } }
                    rpt.DataSource = tblRpt;
                    rpt.RequestParameters = false;
                    rv.documentViewer1.DocumentSource = rpt;
                    rpt.Print(Properties.Settings.Default.PrinterName3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في طباعة الفاتورة: " + ex.Message);
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

        private void Frm_mbe3at_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Serach_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                //PayOrder();
            }
        }

        private async void DgvSearch_SelectionChanged(object sender, EventArgs e)
        {
            if (DgvDteils.DataSource != null)
                DgvDteils.DataSource = null;
            else
                DgvDteils.Rows.Clear();

            if (DgvSearch.CurrentRow != null)
            {
                string idVal = null;
                var drv = DgvSearch.CurrentRow.DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains("Guid_ID"))
                {
                    idVal = drv.Row["Guid_ID"]?.ToString();
                }
                if (string.IsNullOrEmpty(idVal) && DgvSearch.CurrentRow.Cells["dgvOrder_ID"].Value != null)
                {
                    idVal = DgvSearch.CurrentRow.Cells["dgvOrder_ID"].Value.ToString();
                }

                if (!string.IsNullOrWhiteSpace(idVal) && Guid.TryParse(idVal, out Guid id))
                {
                    try
                    {
                        using (var http = ConnectionManager.GetHttpClient())
                        {
                            var res = await http.GetAsync($"api/sales/{id}");
                            if (res.IsSuccessStatusCode)
                            {
                                var json = await res.Content.ReadAsStringAsync();
                                var data = JToken.Parse(json);
                                if (data != null && data["lines"] != null)
                                {
                                    var lines = data["lines"].ToObject<JArray>();
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("Barcode", typeof(string));
                                    dt.Columns.Add("Pro_Name", typeof(string));
                                    dt.Columns.Add("Expier", typeof(string));
                                    dt.Columns.Add("Unit", typeof(string));
                                    dt.Columns.Add("Price", typeof(decimal));
                                    dt.Columns.Add("Qty", typeof(decimal));
                                    dt.Columns.Add("Total_Bf", typeof(decimal));
                                    dt.Columns.Add("Discount", typeof(decimal));
                                    dt.Columns.Add("Total_Af", typeof(decimal));
                                    dt.Columns.Add("Price_Tax", typeof(decimal));
                                    dt.Columns.Add("Tax_Value", typeof(decimal));
                                    dt.Columns.Add("Buy_Price", typeof(decimal));

                                    foreach (var line in lines)
                                    {
                                        string expStr = "";
                                        if (line["expiryDate"] != null && DateTime.TryParse(line["expiryDate"].ToString(), out DateTime dtExp))
                                        {
                                            expStr = dtExp.ToString("yyyy-MM-dd");
                                        }

                                        dt.Rows.Add(
                                            line["barcode"]?.ToString() ?? "",
                                            line["productName"]?.ToString() ?? "",
                                            expStr,
                                            line["unitName"]?.ToString() ?? "",
                                            Convert.ToDecimal(line["unitPrice"] ?? 0),
                                            Convert.ToDecimal(line["quantity"] ?? 0),
                                            Convert.ToDecimal(line["lineTotal"] ?? 0),
                                            Convert.ToDecimal(line["discount"] ?? 0),
                                            Convert.ToDecimal(line["lineTotalAfterDiscount"] ?? 0),
                                            Convert.ToDecimal(line["priceBeforeTax"] ?? 0),
                                            Convert.ToDecimal(line["taxValue"] ?? 0),
                                            Convert.ToDecimal(line["buyPrice"] ?? 0)
                                        );
                                    }

                                    DgvDteils.AutoGenerateColumns = false;
                                    DgvDteils.DataSource = dt;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("خطأ في جلب تفاصيل الفاتورة: " + ex.Message, "خطأ");
                    }
                }
            }
        }
    }
}