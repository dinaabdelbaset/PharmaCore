
using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using PharmaCore.POS.Enums;
using PharmaCore.POS.RE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using PharmaCore.POS.Services;
using System.Xml.Linq;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Sales : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        // Removed global SqlConnection, now localized to methods that still need legacy SQL fallback

        // ✅ هل الوضع الحالي API؟
        private bool IsApi => ConnectionManager.CurrentMode == ConnectionMode.Api;

        // المتغير الخاص بحفظ مسار הפاتورة المعلقة (لاسترجاعها ومسح القديمة لاحقا)
        public Guid? CurrentTempSaleId = null;

        // ✅ دالة مساعدة تجيب JSON من API (بدون موديلات)
        private async Task<JToken> ApiGetAsync(string relative)
        {
            using (var http = ConnectionManager.GetHttpClient())
            {
                // relative بدون "/" لأن BaseAddress مضبوط
                var res = await http.GetAsync(relative.StartsWith("/") ? relative.Substring(1) : relative);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                return JToken.Parse(json);
            }
        }

        private async Task<JToken> ApiPostAsync(string relative, object body)
        {
            using (var http = ConnectionManager.GetHttpClient())
            {
                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(body),
                    Encoding.UTF8, "application/json");

                var res = await http.PostAsync(relative.StartsWith("/") ? relative.Substring(1) : relative, content);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                return JToken.Parse(json);
            }
        }

        private async Task<bool> ApiDeleteAsync(string relative)
        {
            using (var http = ConnectionManager.GetHttpClient())
            {
                var res = await http.DeleteAsync(relative.StartsWith("/") ? relative.Substring(1) : relative);
                res.EnsureSuccessStatusCode();
                return true;
            }
        }

        private static Frm_Sales frm;

        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static Frm_Sales GetFormSales
        {
            get
            {
                if (frm == null)
                {
                    frm = new Frm_Sales();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public Frm_Sales()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.DgvSale.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DgvSale.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSale_CellEnter);
            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DgvSale);

            // Add Export Context Menu dynamically
            ContextMenuStrip dgvMenu = new ContextMenuStrip();
            dgvMenu.Items.Add("تصدير إلى Excel", null, (s, ev) => Helpers.ExportHelper.ExportToExcel(DgvSale));
            dgvMenu.Items.Add("تصدير إلى PDF (طباعة)", null, (s, ev) => Helpers.ExportHelper.ExportToPdf(DgvSale));
            DgvSale.ContextMenuStrip = dgvMenu;
        }

        private async void Frm_Sales_Load(object sender, EventArgs e)
        {
            await LoadDropdownsAsync();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                // ── Load Employees ──
                List<object> empList = null;
                if (IsApi)
                {
                    try
                    {
                        var data = await ApiGetAsync("api/employees");
                        if (data != null)
                        {
                            empList = new List<object>();
                            foreach (var emp in data)
                            {
                                empList.Add(new { Emp_ID = emp["employeeCode"]?.ToString(), Emp_Name = emp["fullName"]?.ToString() });
                            }
                        }
                    }
                    catch
                    {
                        // Fallback will handle it
                    }
                }

                if (empList == null)
                {
                    try
                    {
                        DataTable dt = db.readData("select Employee_ID as Emp_ID, Employee_Name as Emp_Name from Employees", "");
                        if (dt != null)
                        {
                            empList = new List<object>();
                            foreach (DataRow row in dt.Rows)
                            {
                                empList.Add(new { Emp_ID = row["Emp_ID"]?.ToString(), Emp_Name = row["Emp_Name"]?.ToString() });
                            }
                        }
                    }
                    catch { }
                }

                if (empList != null && empList.Count > 0)
                {
                    EmpName.DataSource = empList;
                    EmpName.DisplayMember = "Emp_Name";
                    EmpName.ValueMember = "Emp_ID";
                }
            }
            catch { }

            try
            {
                // ── Load Companies ──
                List<object> companyList = null;
                if (IsApi)
                {
                    try
                    {
                        var data = await ApiGetAsync("api/companies");
                        if (data != null)
                        {
                            companyList = new List<object>();
                            foreach (var comp in data)
                            {
                                companyList.Add(new { Cop_ID = comp["id"]?.ToString(), Cop_Name = comp["name"]?.ToString() });
                            }
                        }
                    }
                    catch
                    {
                        // Fallback will handle it
                    }
                }

                if (companyList == null)
                {
                    try
                    {
                        DataTable dt = db.readData("select Cop_ID, Cop_Name from Company", "");
                        if (dt != null)
                        {
                            companyList = new List<object>();
                            foreach (DataRow row in dt.Rows)
                            {
                                companyList.Add(new { Cop_ID = row["Cop_ID"]?.ToString(), Cop_Name = row["Cop_Name"]?.ToString() });
                            }
                        }
                    }
                    catch { }
                }

                if (companyList != null && companyList.Count > 0)
                {
                    cbxCompany.Properties.DataSource = companyList;
                    cbxCompany.Properties.DisplayMember = "Cop_Name";
                    cbxCompany.Properties.ValueMember = "Cop_ID";
                    cbxCompany.Properties.NullText = "";
                    cbxCompany.Enabled = true;
                }
            }
            catch { }
        }

        string stock_ID = "";

        private void AutoNumber()
        {
            CurrentTempSaleId = null;
            DgvSale.Rows.Clear();
            rbtnNakdy.Select();

            txtCustomer.ResetText();
            txt2_Code.ResetText(); txt2_Name.ResetText(); txt2_Not.ResetText(); txt2_Phone.ResetText();
            txt3_Code.ResetText(); txt3_Name.ResetText();  txt3_Phone.ResetText();
            txt4_Code.ResetText(); txt4_Name.ResetText(); textCust_Money.ResetText(); txt4_Phone.ResetText();
            EmpName.ResetText(); EmpPay.ResetText();
            txt6_Code.ResetText(); txt6_Name.ResetText(); txt6_Address.ResetText(); txt6_Phone.ResetText();
            txtbarcode.ResetText();
            txtbarcode.Focus();
            txtTotal.Text="";
            txtNeed.Text="";
            textPercenc.Value = 0;

            DtpReminder.Enabled = false;
            txtCustomer.Enabled = true;
            txtSaleCode.Enabled = false;

            if (Properties.Settings.Default.ItemDiscount == "Value")
            {
                LblDis.Text = "ج.م.";
            }
            else if (Properties.Settings.Default.ItemDiscount == "Present")
            {
                LblDis.Text = "%";
            }

        }

        public void CalculateDGV()
        {
            try
            {
                decimal TotalOrder = 0;
                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {

                    TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);

                }

                decimal TotalNeed = 0;
                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {

                    TotalNeed += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value);

                }


                txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                txtNeed.Text = Math.Round(TotalNeed, 2).ToString();

            }
            catch (Exception) { }
            DgvSale.ClearSelection();
            DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
            DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
        }


        private DataTable BuildUnitsDataTable(JToken batchData)
        {
            var dt = new DataTable();
            dt.Columns.Add("Unit_Name", typeof(string));
            dt.Columns.Add("Unit_ID", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("QtyINmain", typeof(decimal));

            if (batchData != null)
            {
                string mainUnitName = batchData["mainUnitName"]?.ToString() ?? "الوحدة الرئيسية";
                string mainUnitId = batchData["mainUnitId"]?.ToString() ?? "";
                decimal mainPrice = batchData["priceAfterTax"]?.ToObject<decimal>() ?? batchData["price"]?.ToObject<decimal>() ?? 0;

                dt.Rows.Add(mainUnitName, mainUnitId, mainPrice, 1m);

                var unitsNode = batchData["units"] ?? batchData["product"]?["units"];
                if (unitsNode is JArray unitsArr)
                {
                    foreach (var u in unitsArr)
                    {
                        string uId = u["unitId"]?.ToString() ?? u["id"]?.ToString() ?? "";
                        if (uId == mainUnitId || string.IsNullOrEmpty(uId)) continue; // avoid duplicate main unit or empty

                        string uName = u["unitName"]?.ToString() ?? u["name"]?.ToString() ?? "";
                        decimal uPrice = u["priceAfterTax"]?.ToObject<decimal>() ?? u["price"]?.ToObject<decimal>() ?? u["salePrice"]?.ToObject<decimal>() ?? 0;
                        decimal uQtyInMain = u["qtyInMain"]?.ToObject<decimal>() ?? u["conversionFactor"]?.ToObject<decimal>() ?? u["qtyInMainUnit"]?.ToObject<decimal>() ?? 1m;

                        dt.Rows.Add(uName, uId, uPrice, uQtyInMain);
                    }
                }
            }
            return dt;
        }

        // ══════════════════════════════════════════════════════════
        //  ✅ API-based AddItems — single API call replaces 3+ SQL queries
        // ══════════════════════════════════════════════════════════
        public async Task AddItems(int Type)
        {
            try
            {
                string barcode = "";
                if (Type == 1)
                    barcode = txtbarcode.Text.Trim();
                else if (Type == 2)
                    barcode = Properties.Settings.Default.sales_ID.ToString();

                if (string.IsNullOrWhiteSpace(barcode)) return;

                // ── Call API or Fallback to Local Cache ──
                JToken data = null;
                try
                {
                    data = await ApiGetAsync($"api/batches/by-barcode/{Uri.EscapeDataString(barcode)}");
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException || ex is System.Net.Sockets.SocketException || ex is TimeoutException)
                {
                    data = LocalCatalogCache.LookupBarcode(barcode);
                    if (data == null)
                    {
                        throw;
                    }
                }

                if (data == null) return;

                decimal stockQty = data["stockInBranch"]?.ToObject<decimal>() ?? 0;
                string barcodeResult = data["barcode"]?.ToString() ?? barcode;
                string productName = data["productName"]?.ToString() ?? "";
                string productId = data["productId"]?.ToString() ?? "";
                decimal salePrice = data["priceAfterTax"]?.ToObject<decimal>() ?? 0;
                string expiryStr = "";
                DateTime? expiryDate = null;

                if (data["expiryDate"]?.Type != JTokenType.Null && data["expiryDate"] != null)
                {
                    expiryDate = data["expiryDate"]?.ToObject<DateTime>();
                    expiryStr = expiryDate?.ToString("yyyy/MM") ?? "";
                }

                // ── Check stock ──
                if (stockQty <= 0)
                {
                    XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج غير متوفر"; arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                    return;
                }

                // ── Check if already in DGV → increment qty ──
                if (DgvSale.Rows.Count >= 1)
                {
                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                    {
                        string existingBar = DgvSale.Rows[i].Cells["dgvBar"].Value?.ToString() ?? "";
                        if (existingBar == barcodeResult)
                        {
                            if (Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvQty"].Value) < stockQty)
                            {
                                DgvSale.Rows[i].Cells["dgvQty"].Value = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvQty"].Value) + 1;
                                return;
                            }
                            XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج غير متوفر"; arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                            return;
                        }
                    }
                }

                // ── Add to DGV ──
                decimal Discount = Convert.ToDecimal(textPercenc.Text);

                DgvSale.Rows.Add(1);
                int rowindex = DgvSale.Rows.Count - 1;

                DgvSale.Rows[rowindex].Cells["dgvID"].Value = productId;
                DgvSale.Rows[rowindex].Cells["dgvBar"].Value = barcodeResult;
                DgvSale.Rows[rowindex].Cells["dgvName"].Value = productName;
                DgvSale.Rows[rowindex].Cells["dgvExpier"].Value = expiryStr;
                DgvSale.Rows[rowindex].Cells["dgvStore"].Value = Math.Round(stockQty, 2).ToString();
                DgvSale.Rows[rowindex].Cells["dgvQty"].Value = "1";
                DgvSale.Rows[rowindex].Cells["dgvDiscount"].Value = Discount;

                // ── Expiry warnings ──
                if (expiryDate.HasValue)
                {
                    if (DateTime.Now.Date >= expiryDate.Value.Date)
                    {
                        XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج انتهت فترة صلاحيته"; arg.Text = "المنتج المضاف انتهت مدة الصلاحية بالفعل ... ـ"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                    }
                    else if (DateTime.Now.AddMonths(4).Date >= expiryDate.Value.Date)
                    {
                        XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج قارب على انتهاء مدة الصلاحيه"; arg.Text = "المنتج المضاف تبقى على انتهاء صلاحيته فترة اقل من 3 اشهر "; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                    }
                }

                // ── Load units dropdown from API data ──
                var unitsDt = BuildUnitsDataTable(data);
                var cell = (DataGridViewComboBoxCell)DgvSale.Rows[rowindex].Cells["dgvUnit"];
                cell.DataSource = unitsDt;
                cell.DisplayMember = "Unit_Name";
                cell.ValueMember = "Unit_ID";
                cell.Value = data["mainUnitId"]?.ToString() ?? "";

                string batchId = data["batchId"]?.ToString() ?? data["id"]?.ToString() ?? "";
                DgvSale.Rows[rowindex].Tag = batchId;

                // ── Set price ──
                DgvSale.Rows[rowindex].Cells["dgvPrice"].Value = Math.Round(salePrice, 2);
                decimal total = 1 * salePrice;
                DgvSale.Rows[rowindex].Cells["dgvBefor"].Value = total;

                // ── Apply discount ──
                decimal After = 0;
                if (Discount > 0)
                {
                    if (Properties.Settings.Default.ItemDiscount == "Value")
                        After = total - Discount;
                    else if (Properties.Settings.Default.ItemDiscount == "Present")
                        After = total - ((Discount / 100) * total);
                    DgvSale.Rows[rowindex].Cells["dgvTotal"].Value = After;
                }
                else
                {
                    DgvSale.Rows[rowindex].Cells["dgvTotal"].Value = total;
                }

                CalculateDGV();
                AutoNumic();

                // ── BlackBox: log item added to sale ──
                try
                {
                    await ApiPostAsync("api/audit-logs", new
                    {
                        eventType = "Create",
                        category = "Sales",
                        screenName = "Frm_Sales",
                        targetTable = "SaleLine",
                        targetId = barcodeResult,
                        description = $"تمت إضافة المنتج {productName} (باركود: {barcodeResult}) إلى الفاتورة",
                        severity = 0
                    });
                }
                catch { }
            }
            catch (Exception)
            {
                txtbarcode.ResetText();
                txtbarcode.Focus();
            }
        }

        private void textPercenc_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal After = 0, Item_Discount = 0;

                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {
                    Item_Discount = Convert.ToDecimal(textPercenc.Text);

                    DgvSale.Rows[i].Cells["dgvDiscount"].Value = Item_Discount;

                    if (Item_Discount > 0)
                    {
                        if (Properties.Settings.Default.ItemDiscount == "Value")
                        {
                            After = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value) - Item_Discount;
                        }
                        else if (Properties.Settings.Default.ItemDiscount == "Present")
                        {
                            After = (Item_Discount / 100) * Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                        }

                        DgvSale.Rows[i].Cells["dgvTotal"].Value = After;
                    }
                    else if (Item_Discount <= 0)
                    {
                        DgvSale.Rows[i].Cells["dgvTotal"].Value = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                    }
                }
            }
            catch (Exception) { }
        }

        public void AutoNumic()
        {
            for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
            {
                DgvSale.Rows[i].Cells["I"].Value = (i + 1).ToString();
            }
        }

        private void UpdateQty()
        {

            if (DgvSale.Rows.Count >= 1)
            {
                int index = DgvSale.SelectedRows[0].Index;
                Properties.Settings.Default.Pro_ID = Convert.ToInt32(DgvSale.Rows[index].Cells["dgvID"].Value);
                Properties.Settings.Default.Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvQty"].Value);
                Properties.Settings.Default.Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvPrice"].Value);
                Properties.Settings.Default.Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvDiscount"].Value);
                
                int unitIdx = 0;
                try
                {
                    var cell = DgvSale.Rows[index].Cells["dgvUnit"] as DataGridViewComboBoxCell;
                    object dataSource = cell?.DataSource ?? dgvUnit.DataSource;
                    string valueMember = cell?.ValueMember ?? dgvUnit.ValueMember ?? "Unit_ID";
                    object cellVal = DgvSale.Rows[index].Cells["dgvUnit"].Value;
                    if (dataSource is DataTable dt)
                    {
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            if (dt.Rows[r][valueMember]?.ToString() == cellVal?.ToString())
                            {
                                unitIdx = r;
                                break;
                            }
                        }
                    }
                    else if (dataSource is System.Collections.IList list)
                    {
                        for (int r = 0; r < list.Count; r++)
                        {
                            var item = list[r];
                            if (item != null)
                            {
                                var prop = item.GetType().GetProperty(valueMember);
                                if (prop != null)
                                {
                                    var val = prop.GetValue(item, null);
                                    if (val?.ToString() == cellVal?.ToString())
                                    {
                                        unitIdx = r;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        unitIdx = Convert.ToInt32(cellVal);
                    }
                }
                catch { }
                Properties.Settings.Default.Pro_Unit = unitIdx;
                Properties.Settings.Default.Save();


                Frm_SaleQty frm = new Frm_SaleQty();
                frm.ShowDialog();


                try
                {

                    int index2 = DgvSale.SelectedRows[0].Index;
                    object newValue = null;
                    try
                    {
                        var cell2 = DgvSale.Rows[index2].Cells["dgvUnit"] as DataGridViewComboBoxCell;
                        object dataSource2 = cell2?.DataSource ?? dgvUnit.DataSource;
                        string valueMember2 = cell2?.ValueMember ?? dgvUnit.ValueMember ?? "Unit_ID";
                        int targetIndex = Properties.Settings.Default.Pro_Unit;
                        if (dataSource2 is DataTable dt2)
                        {
                            if (targetIndex >= 0 && targetIndex < dt2.Rows.Count)
                            {
                                newValue = dt2.Rows[targetIndex][valueMember2];
                            }
                        }
                        else if (dataSource2 is System.Collections.IList list2)
                        {
                            if (targetIndex >= 0 && targetIndex < list2.Count)
                            {
                                var item = list2[targetIndex];
                                if (item != null)
                                {
                                    var prop = item.GetType().GetProperty(valueMember2);
                                    if (prop != null)
                                    {
                                        newValue = prop.GetValue(item, null);
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    DgvSale.Rows[index2].Cells["dgvUnit"].Value = newValue ?? Properties.Settings.Default.Pro_Unit;
                    DgvSale.Rows[index2].Cells["dgvQty"].Value = Properties.Settings.Default.Item_Qty;
                    DgvSale.Rows[index2].Cells["dgvPrice"].Value = Properties.Settings.Default.Item_SalePrice;
                    DgvSale.Rows[index2].Cells["dgvDiscount"].Value = Properties.Settings.Default.Item_Discount;

                }
                catch (Exception) { }

            }

        }
        private async void Frm_Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Properties.Settings.Default.sales_ID = 0;
                Properties.Settings.Default.Save();

                Frm_ItemSales frm = new Frm_ItemSales();
                frm.ShowDialog();

                if (Properties.Settings.Default.sales_ID != 0)
                {
                    await AddItems(2);
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtbarcode.ResetText();
                txtbarcode.Focus();
            }
            else if (e.Control && e.KeyCode == Keys.E )
            {
                UpdateQty();
            }
            else if (e.Control && e.KeyCode == Keys.S )
            {
                PayOrder();
            }
            else if (e.Control && e.KeyCode == Keys.R )
            {
                Frm_m3l8at frm = new Frm_m3l8at();
                frm.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                Frm_mbe3at frm = new Frm_mbe3at();
                frm.ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                Last_Print_Click(null, null);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDeleteItem_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("ـ .. سوف يتم إعادة تعين كل البيانات المدخلة." + "\n" +  "هل تريد المتابعة ؟", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    AutoNumber();
                }
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                NO_UP();
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                NO_Down();
            }
        }

        private void NO_UP()
        {
            if (DgvSale.Rows.Count >= 1)
            {

                int index = DgvSale.SelectedRows[0].Index;

                decimal qt = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvQty"].Value) + 1 ;
                DgvSale.Rows[index].Cells["dgvQty"].Value = qt ;


                try
                {
                    decimal TotalOrder = 0, TotalNeed = 0;
                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                        TotalNeed += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value);
                        DgvSale.ClearSelection();
                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    txtNeed.Text = Math.Round(TotalNeed, 2).ToString();

                }
                catch (Exception) { }

            }
        }

        private void NO_Down()
        {
            if (DgvSale.Rows.Count >= 1)
            {

                int index = DgvSale.SelectedRows[0].Index;

                decimal qt = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvQty"].Value) - 1;
               
                if ( qt < 0)
                {
                    qt = 0;
                }

                DgvSale.Rows[index].Cells["dgvQty"].Value = qt;


                try
                {
                    decimal TotalOrder = 0, TotalNeed = 0;
                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                        TotalNeed += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value);
                        DgvSale.ClearSelection();
                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    txtNeed.Text = Math.Round(TotalNeed, 2).ToString();

                }
                catch (Exception) { }

            }
        }

        private async void PayOrder(string finalStatus = "Completed")
        {
            if (DgvSale.Rows.Count < 1) return;

            Properties.Settings.Default.TotalOrder = Convert.ToDecimal(txtTotal.Text);
            Properties.Settings.Default.TotalNeed = Convert.ToDecimal(txtNeed.Text);
            
            if (finalStatus == "Temp")
            {
                Properties.Settings.Default.Madfou3 = 0;
                Properties.Settings.Default.Bakey = Properties.Settings.Default.TotalNeed;
                Properties.Settings.Default.visa = 0;
                Properties.Settings.Default.rsed_mdfo3 = 0;
                Properties.Settings.Default.Print_Not = true; // no need to print temp invoices
            }
            else
            {
                Properties.Settings.Default.Madfou3 = 0;
                Properties.Settings.Default.Print_Not = false;
                Properties.Settings.Default.Bakey = 0;
                Properties.Settings.Default.Save();

                if (xControl.SelectedTabPage == rbtnCustAagel || EmpPay.SelectedIndex == 1)
                {
                    MessageBox.Show("تذكر !! في حين الدفع الآجل ،المبلغ الذي سوف تضعه في خانة المدفوع و الباقي سوف يسجل على العميل في السستم", "تاكيد");
                }

                Frm_PaySale frm = new Frm_PaySale();
                frm.checkDiscount.Visible = true;
                frm.ShowDialog();

                if (Properties.Settings.Default.CheckButton != true) return;
            }

            try
            {
                // ── Determine sale type ──
                string saleType = "عميل نقدي";
                string customerName = "";
                string customerCode = "";
                string customerPhone = "";
                string employeePayType = "";
                string deliveryAddress = "";

                string insuranceCompanyName = "";
                string insuranceCode = "";
                string approvalCode = "";
                decimal insurancePercent = 0m;
                string insuranceMobile = "";

                if (xControl.SelectedTabPage == rbtnNakdy)
                {
                    saleType = "عميل نقدي";
                    customerName = string.IsNullOrWhiteSpace(txtCustomer.Text) ? "عميل نقدي" : txtCustomer.Text;
                    customerPhone = txt1_Phone.Text;
                }
                else if (xControl.SelectedTabPage == rbtnCustNakdy)
                {
                    saleType = "عميل مسجل";
                    customerName = txt2_Name.Text;
                    customerCode = txt2_Code.Text;
                    customerPhone = txt2_Phone.Text;
                }
                else if (xControl.SelectedTabPage == rbtnCustAagel)
                {
                    saleType = "عميل آجل";
                    customerName = txt3_Name.Text;
                    customerCode = txt3_Code.Text;
                    customerPhone = txt3_Phone.Text;
                }
                else if (xControl.SelectedTabPage == rbtnCustPoint)
                {
                    saleType = "سحب من رصيد العميل";
                    customerName = txt4_Name.Text;
                    customerCode = txt4_Code.Text;
                    customerPhone = txt4_Phone.Text;
                }
                else if (xControl.SelectedTabPage == rbtnEmp)
                {
                    saleType = "سحوبات موظف";
                    customerName = EmpName.Text;
                    if (EmpPay.SelectedIndex == 0) employeePayType = "نقدي";
                    else if (EmpPay.SelectedIndex == 1) employeePayType = "آجل";
                    else if (EmpPay.SelectedIndex == 2) employeePayType = "من الراتب";
                }
                else if (xControl.SelectedTabPage == rbtnDelivery)
                {
                    saleType = "توصيل";
                    customerName = txt6_Name.Text;
                    customerCode = txt6_Code.Text;
                    customerPhone = txt6_Phone.Text;
                    deliveryAddress = txt6_Address.Text;
                }
                else if (xControl.SelectedTabPage == xtraTameen)
                {
                    saleType = "تأمين";
                    insuranceCompanyName = cbxCompany.Text;
                    insuranceCode = tam_cod.Text;
                    approvalCode = apv_cod.Text;
                    insurancePercent = Convert.ToDecimal(string.IsNullOrWhiteSpace(t7ml.Text) ? "0" : t7ml.Text);
                    insuranceMobile = tam_mobile.Text;
                }

                // ── Determine seller ──
                string sellerName = Properties.Settings.Default.USERNAME;
                string sellerCode = Properties.Settings.Default.cod.ToString();
                bool useAlt = ChSale.Checked;
                if (useAlt)
                {
                    sellerName = txtSaName.Text;
                    sellerCode = labSaleCode.Text;
                }

                // ── Build lines ──
                var lines = new List<object>();
                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {
                    string pId = DgvSale.Rows[i].Cells["dgvID"].Value?.ToString();
                    string bId = DgvSale.Rows[i].Tag?.ToString();
                    string uId = DgvSale.Rows[i].Cells["dgvUnit"].Value?.ToString();

                    lines.Add(new
                    {
                        lineNumber = i + 1,
                        productId = string.IsNullOrWhiteSpace(pId) ? Guid.Empty.ToString() : pId,
                        batchId = string.IsNullOrWhiteSpace(bId) ? Guid.Empty.ToString() : bId,
                        barcode = DgvSale.Rows[i].Cells["dgvBar"].Value?.ToString() ?? "",
                        productName = DgvSale.Rows[i].Cells["dgvName"].Value?.ToString() ?? "",
                        unitId = string.IsNullOrWhiteSpace(uId) ? Guid.Empty.ToString() : uId,
                        unitName = DgvSale.Rows[i].Cells["dgvUnit"].FormattedValue?.ToString() ?? "",
                        quantity = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvQty"].Value),
                        stockQtyAtSale = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvStore"].Value),
                        priceBeforeTax = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvPrice"].Value),
                        taxValue = 0m,
                        unitPrice = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvPrice"].Value),
                        discount = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvDiscount"].Value),
                        lineTotal = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value),
                        lineTotalAfterDiscount = Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value),
                        buyPrice = 0m,
                        expiryDate = (object)null
                    });
                }

                // ── Build sale request ──
                var saleReq = new
                {
                    saleType = saleType,
                    movementType = "بيع",
                    customerId = (string)null,
                    customerName = customerName,
                    customerCode = customerCode,
                    customerPhone = customerPhone,
                    deliveryAddress = deliveryAddress,
                    useAlternateSeller = useAlt,
                    sellerEmployeeId = (string)null,
                    sellerName = sellerName,
                    sellerCode = sellerCode,
                    subtotal = Properties.Settings.Default.TotalOrder,
                    discountPercent = textPercenc.Value,
                    discountAmount = Properties.Settings.Default.TotalOrder - Properties.Settings.Default.TotalNeed,
                    taxTotal = 0m,
                    total = Properties.Settings.Default.TotalNeed,
                    paidCash = Properties.Settings.Default.Madfou3,
                    paidVisa = Properties.Settings.Default.visa,
                    paidFromBalance = Properties.Settings.Default.rsed_mdfo3,
                    remaining = Properties.Settings.Default.Bakey,
                    insuranceCompanyName = insuranceCompanyName,
                    insuranceCode = insuranceCode,
                    approvalCode = approvalCode,
                    insurancePercent = insurancePercent,
                    insuranceMobile = insuranceMobile,
                    employeeCustomerId = (string)null,
                    employeePayType = employeePayType,
                    notes = Not_fa.Text,
                    reminderDate = (xControl.SelectedTabPage == rbtnCustAagel || EmpPay.SelectedIndex == 1)
                        ? DtpReminder.Text : null,
                    status = finalStatus,
                    lines = lines
                };

                // ── POST to API ──
                JToken result = null;
                bool isOfflineMode = false;
                string offlineInvoiceNumber = null;
                try
                {
                    result = await ApiPostAsync("api/sales", saleReq);
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is System.Net.Sockets.SocketException || ex is TimeoutException || ex is TaskCanceledException)
                {
                    isOfflineMode = true;
                    MessageBox.Show("تم فقدان الاتصال بالخادم. سيتم تحويل النظام تلقائياً إلى وضع العمل دون اتصال (Offline Mode).", "تنبيه - وضع العمل دون اتصال", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    try
                    {
                        offlineInvoiceNumber = OfflineSalesManager.QueueOfflineSale(saleReq);
                        MessageBox.Show($"تم حفظ الفاتورة بنجاح في وضع العمل دون اتصال برقم: {offlineInvoiceNumber}\nوسيتم مزامنتها تلقائياً عند عودة الاتصال بالخادم.", "تم الحفظ أوفلاين", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception queueEx)
                    {
                        MessageBox.Show("خطأ أثناء حفظ الفاتورة محلياً: " + queueEx.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Stop clearing cart if queueing failed
                    }
                }

                if (isOfflineMode)
                {
                    AutoNumber();
                    return;
                }

                // ── Delete Old Temp Invoice if overwriting/completing one ──
                if (CurrentTempSaleId.HasValue)
                {
                    try { await ApiDeleteAsync($"api/sales/temp/{CurrentTempSaleId.Value}"); } 
                    catch { }
                    CurrentTempSaleId = null;
                }

                Properties.Settings.Default.Last_ID = 0; // API uses GUID now
                Properties.Settings.Default.Save();

                if (finalStatus == "Temp")
                {
                    AutoNumber();
                    return;
                }

                // ── BlackBox: log sale creation ──
                try
                {
                    await ApiPostAsync("api/audit-logs", new
                    {
                        eventType = "Create",
                        category = "Sales",
                        screenName = "Frm_Sales",
                        targetTable = "Sale",
                        targetId = result?["invoiceNumber"]?.ToString() ?? "",
                        description = $"تم حفظ فاتورة مبيعات رقم {result?["invoiceNumber"]} - {saleType} - {customerName} - المبلغ: {Properties.Settings.Default.TotalNeed}",
                        severity = 0
                    });
                }
                catch { }

                // ── Print if needed ──
                if (Properties.Settings.Default.SalesPrint == true && Properties.Settings.Default.Print_Not == false)
                {
                    if (string.IsNullOrEmpty(Properties.Settings.Default.PrinterName1))
                    {
                        MessageBox.Show("من فضلك حدد اسم الطابعة من شاشة اعدادت البرنامج", "تاكيد");
                    }
                    else
                    {
                        Guid newSaleId = Guid.Parse(result["id"].ToString());
                        for (int i = 0; i <= Properties.Settings.Default.SalesPrintNum - 1; i++)
                        {
                            await PrintApiAsync(newSaleId);
                        }
                    }
                }
                AutoNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في حفظ الفاتورة: " + ex.Message, "خطأ");
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
                string compName = "", compAddress = "", compDesc = "", compPhone1 = "", compPhone2 = "";
                byte[] logoBytes = null;
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

                RE.rpt_viewer rv = new RE.rpt_viewer();

                if (Properties.Settings.Default.SalePrintKind == "8CM")
                {
                    if (Properties.Settings.Default.Show_Discount == false)
                    {
                        RE.RptOrderSales1 rpt = new RptOrderSales1();
                        Services.ReportManager.LoadCustomLayoutIfExists(rpt, "RptOrderSales1");
                        if (logoImg != null) { try { ((DevExpress.XtraReports.UI.XRPictureBox)rpt.FindControl("Logo1", true)).Image = logoImg; } catch { } }
                        rpt.DataSource = tblRpt;
                        rpt.RequestParameters = false;
                        rv.documentViewer1.DocumentSource = rpt;
                        rpt.Print(Properties.Settings.Default.PrinterName1);
                    }
                    else
                    {
                        RE.RptOrderSales2 rpt = new RptOrderSales2();
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
                    RE.RptOrderSalesA41 rpt = new RptOrderSalesA41();
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


        private void DgvSale_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal Item_Qty = 0, Item_SalePrice = 0, Item_Discount = 0, Item_Store = 0, Item_Unit = 0, Item_QtyINmain = 0, Item_QtyCanSeals = 0;
            try
            {
                int index = DgvSale.SelectedRows[0].Index;

                Item_Qty = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvQty"].Value);
                Item_SalePrice = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvPrice"].Value);
                Item_Discount = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvDiscount"].Value);
                Item_Store = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvStore"].Value);
                if (!IsApi)
                {
                    try
                    {
                        Item_Unit = Convert.ToDecimal(DgvSale.Rows[index].Cells["dgvUnit"].Value);

                        DataTable tblItems = new DataTable();
                        tblItems.Clear();
                        DataTable tblunit = new DataTable();
                        tblunit.Clear();

                        tblunit = db.readData("select * from Products_Unit where Pro_ID=" + DgvSale.Rows[index].Cells["dgvID"].Value + " and Unit_Name=N'" + DgvSale.Rows[index].Cells["dgvUnit"].FormattedValue + "'", "");

                        try
                        {
                            Item_SalePrice = Convert.ToDecimal(tblunit.Rows[0]["TotalSalePrice"]) / Convert.ToDecimal(tblunit.Rows[0]["QtyINmain"]);

                            DgvSale.Rows[index].Cells["dgvPrice"].Value = Math.Round(Item_SalePrice, 2).ToString();
                            //DgvSale.Rows[index].Cells["dgvPrice"].Value = Item_SalePrice;

                        }
                        catch (Exception) { }


                        if (Item_Unit == 1)
                        {
                            if (Item_Qty > Item_Store)
                            {
                                XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج غير متوفر"; arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                                DgvSale.Rows[index].Cells["dgvQty"].Value = Item_Store;
                                //return;
                            }
                        }
                        else
                        {
                            Item_QtyINmain = Convert.ToDecimal(tblunit.Rows[0]["QtyINmain"]);
                            Item_QtyCanSeals = Math.Round(Item_QtyINmain * Item_Store, 0);

                            if (Item_Qty > Item_QtyCanSeals)
                            {
                                XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج غير متوفر"; arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg);
                                DgvSale.Rows[index].Cells["dgvQty"].Value = Math.Round(Item_QtyCanSeals, 0).ToString();
                                //return;
                            }
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    try
                    {
                        var cell = DgvSale.Rows[index].Cells["dgvUnit"] as DataGridViewComboBoxCell;
                        object dataSource = cell?.DataSource ?? dgvUnit.DataSource;
                        string valueMember = cell?.ValueMember ?? dgvUnit.ValueMember ?? "Unit_ID";
                        object cellVal = DgvSale.Rows[index].Cells["dgvUnit"].Value;

                        decimal selectedPrice = 0;
                        decimal selectedQtyInMain = 1;
                        bool found = false;

                        if (dataSource is DataTable dt)
                        {
                            for (int r = 0; r < dt.Rows.Count; r++)
                            {
                                if (dt.Rows[r][valueMember]?.ToString() == cellVal?.ToString())
                                {
                                    selectedPrice = dt.Rows[r]["Price"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[r]["Price"]) : 0;
                                    selectedQtyInMain = dt.Rows[r]["QtyINmain"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[r]["QtyINmain"]) : 1;
                                    found = true;
                                    break;
                                }
                            }
                        }
                        else if (dataSource is System.Collections.IList list)
                        {
                            for (int r = 0; r < list.Count; r++)
                            {
                                var item = list[r];
                                if (item != null)
                                {
                                    var propId = item.GetType().GetProperty(valueMember);
                                    var propPrice = item.GetType().GetProperty("Price");
                                    var propQtyInMain = item.GetType().GetProperty("QtyINmain");

                                    var val = propId?.GetValue(item, null);
                                    if (val?.ToString() == cellVal?.ToString())
                                    {
                                        selectedPrice = propPrice != null ? Convert.ToDecimal(propPrice.GetValue(item, null)) : 0;
                                        selectedQtyInMain = propQtyInMain != null ? Convert.ToDecimal(propQtyInMain.GetValue(item, null)) : 1;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (found)
                        {
                            Item_SalePrice = selectedPrice;
                            DgvSale.Rows[index].Cells["dgvPrice"].Value = Math.Round(Item_SalePrice, 2).ToString();

                            if (selectedQtyInMain == 1)
                            {
                                if (Item_Qty > Item_Store)
                                {
                                    XtraMessageBoxArgs arg = new XtraMessageBoxArgs();
                                    arg.AutoCloseOptions.Delay = 20000;
                                    arg.Caption = "منتج غير متوفر";
                                    arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن";
                                    arg.Buttons = new DialogResult[] { DialogResult.OK };
                                    XtraMessageBox.Show(arg);
                                    DgvSale.Rows[index].Cells["dgvQty"].Value = Item_Store;
                                }
                            }
                            else
                            {
                                Item_QtyCanSeals = Math.Round(selectedQtyInMain * Item_Store, 2);
                                if (Item_Qty > Item_QtyCanSeals)
                                {
                                    XtraMessageBoxArgs arg = new XtraMessageBoxArgs();
                                    arg.AutoCloseOptions.Delay = 20000;
                                    arg.Caption = "منتج غير متوفر";
                                    arg.Text = "كمية المنتج غير كافية للزيادة .. يجب الشراء او تعديل الكمية في المخزن";
                                    arg.Buttons = new DialogResult[] { DialogResult.OK };
                                    XtraMessageBox.Show(arg);
                                    DgvSale.Rows[index].Cells["dgvQty"].Value = Math.Round(Item_QtyCanSeals, 2).ToString();
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                }

                decimal Total = 0, Need = 0;

                Total = (Item_Qty * Item_SalePrice);

                DgvSale.Rows[index].Cells["dgvBefor"].Value = Math.Round(Total, 2).ToString();
                //DgvSale.Rows[index].Cells["dgvBefor"].Value = Total;


                if (Properties.Settings.Default.ItemDiscount == "Value")
                {
                    Need = Total - Item_Discount;
                }
                else if (Properties.Settings.Default.ItemDiscount == "Present")
                {
                    Need = Total - ((Item_Discount / 100) * Total);
                }

                DgvSale.Rows[index].Cells["dgvTotal"].Value = Math.Round(Need, 2).ToString();


                decimal TotalOrder = 0, TotalNeed = 0;
                for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                {

                    TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                    TotalNeed += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value);

                }

                txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                txtNeed.Text = Math.Round(TotalNeed, 2).ToString();

                AutoNumic();
            }
            catch (Exception) { }
        }

        private async void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    await AddItems(1);
                    txtbarcode.ResetText();
                    txtbarcode.Focus();
                }
            }catch
            {
                txtbarcode.ResetText();
                txtbarcode.Focus();
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (DgvSale.Rows.Count >= 1)
            {

                int index = DgvSale.SelectedRows[0].Index;

                DgvSale.Rows.RemoveAt(index);


                if (DgvSale.Rows.Count <= 0)
                {

                    txtTotal.Text = "0";
                    txtNeed.Text = "0";
                }

                try
                {
                    decimal TotalOrder = 0, TotalNeed = 0;
                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                    {

                        TotalOrder += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                        TotalNeed += Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvTotal"].Value);
                        DgvSale.ClearSelection();
                        DgvSale.FirstDisplayedScrollingRowIndex = DgvSale.Rows.Count - 1;
                        DgvSale.Rows[DgvSale.Rows.Count - 1].Selected = true;
                    }


                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    txtNeed.Text = Math.Round(TotalNeed, 2).ToString();
                }
                catch (Exception) { }

                AutoNumic();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string text = Clipboard.GetText();
                if (string.IsNullOrWhiteSpace(text)) return;

                string[] items = text.Split(new char[] { ' ', ',', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                {
                    string trimmed = item.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        txtbarcode.Text = trimmed;
                        await AddItems(1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في لصق البيانات: " + ex.Message, "خطأ");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            UpdateQty();
        }

        int USER_ID = 0;

        private bool checkUser(string filed, string table)
        {
            DataTable tblSearch = new DataTable();
            tblSearch = db.readData("select " + filed + " from " + table + " where User_ID=" + USER_ID + "", "");
            if (Convert.ToDecimal(tblSearch.Rows[0][0]) == 0)
            {
                string diag = $"User: {CurrentEmployeeSession.FullName}\nRole: {CurrentEmployeeSession.Role}\nIsSystemRole: {CurrentEmployeeSession.IsSystemRole}";
                MessageBox.Show($"انت لا تملك صلاحيه الدخول لهذه الشاشة\n\n{diag}", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void DgvSale_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is ArgumentException)
            {
                e.ThrowException = false;
            }

            //This event is used to avoid the error of DataGridviewCombobox Cell 
            if (e.Exception?.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = DgvSale.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)DgvSale.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)DgvSale.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }

        private async void Last_Print_Click(object sender, EventArgs e)
        {
            if (IsApi)
            {
                try
                {
                    var data = await ApiGetAsync("api/sales?page=1&pageSize=1");
                    var items = data?["items"]?.ToObject<Newtonsoft.Json.Linq.JArray>();
                    if (items != null && items.Count > 0)
                    {
                        Guid lastId = Guid.Parse(items[0]["id"].ToString());
                        await PrintApiAsync(lastId);
                    }
                    else
                    {
                        MessageBox.Show("لا توجد فواتير لطباعتها", "تنبيه");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ في جلب آخر فاتورة: " + ex.Message);
                }
            }
        }

        private void DgvSale_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (DgvSale.CurrentCell.ColumnIndex == DgvSale.Columns["dgvUnit"].Index && e.Control is System.Windows.Forms.ComboBox comboBox)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // جعل القائمة تعمل كـ DropDown فقط
                comboBox.DroppedDown = true; // فتح القائمة المنسدلة تلقائيًا

                // إزالة الحدث السابق (لمنع إضافته أكثر من مرة)
                comboBox.Enter -= combo_Enter;
                // إضافة الحدث من جديد عند دخول المستخدم إلى القائمة
                comboBox.Enter += combo_Enter;

                comboBox.SelectedIndexChanged -= combo_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += combo_SelectedIndexChanged;
            }
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox combo && combo.Focused && DgvSale.CurrentRow != null)
            {
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (DgvSale.IsCurrentCellInEditMode)
                        {
                            DgvSale.EndEdit();
                            CalculateDGV();
                            txtbarcode.Focus();
                        }
                    }
                    catch { }
                }));
            }
        }

        private void DgvSale_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (DgvSale.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    DgvSale.BeginEdit(true);
                    if (DgvSale.EditingControl is System.Windows.Forms.ComboBox comboBox)
                    {
                        comboBox.DroppedDown = true;
                    }
                }
            }
        }

        private async void combo_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = sender as System.Windows.Forms.ComboBox;
            if (combo != null && DgvSale.CurrentRow != null)
            {
                var cell = DgvSale.CurrentRow.Cells["dgvUnit"] as DataGridViewComboBoxCell;
                if (cell != null)
                {
                    if (cell.DataSource != null)
                    {
                        combo.DataSource = cell.DataSource;
                        combo.DisplayMember = "Unit_Name";
                        combo.ValueMember = "Unit_ID";
                        combo.DroppedDown = true;
                        return;
                    }

                    if (IsApi)
                    {
                        try
                        {
                            var barcode = DgvSale.CurrentRow.Cells["dgvBar"].Value?.ToString();
                            JToken data = null;
                            try
                            {
                                data = await ApiGetAsync($"api/batches/by-barcode/{Uri.EscapeDataString(barcode)}");
                            }
                            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException || ex is System.Net.Sockets.SocketException || ex is TimeoutException)
                            {
                                data = LocalCatalogCache.LookupBarcode(barcode);
                            }

                            if (data == null)
                            {
                                try
                                {
                                    data = await ApiGetAsync($"api/batches/by-barcode?barcode={barcode}");
                                }
                                catch { }
                            }

                            if (data != null)
                            {
                                var unitsDt = BuildUnitsDataTable(data);
                                if (unitsDt.Rows.Count > 0)
                                {
                                    cell.DataSource = unitsDt;
                                    combo.DataSource = cell.DataSource;
                                    combo.DisplayMember = "Unit_Name";
                                    combo.ValueMember = "Unit_ID";
                                    combo.DroppedDown = true;
                                }
                            }
                        }
                        catch { }
                        return;
                    }

                    try
                    {
                        var unitsTable = db.readData("select * from Products_Unit where Pro_ID=" + DgvSale.CurrentRow.Cells["dgvID"].Value + "", "");
                        cell.DataSource = unitsTable;
                        combo.DataSource = cell.DataSource;
                        combo.DisplayMember = "Unit_Name";
                        combo.ValueMember = "Unit_ID";
                        combo.DroppedDown = true;
                    }
                    catch { }
                }
            }
        }

        private void EmpPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmpPay.SelectedIndex == 1)
            {
                DtpEmpReminder.Enabled = true;
            }
            else
            {
                DtpEmpReminder.Enabled = false;
            }
        }

        private async void txt2_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    var cust = await ApiGetAsync($"api/customers/by-code/{txt2_Code.Text}");
                    if (cust != null)
                    {
                        txt2_Name.Text = cust["name"]?.ToString();
                        txt2_Not.Text = cust["notes"]?.ToString();
                        txt2_Phone.Text = cust["phone"]?.ToString();
                    }
                }
                catch { /* Not found */ }
            }
        }

        private async void txt3_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    var cust = await ApiGetAsync($"api/customers/by-code/{txt3_Code.Text}");
                    if (cust != null)
                    {
                        txt3_Name.Text = cust["name"]?.ToString();
                        txt3_Phone.Text = cust["phone"]?.ToString();
                    }
                }
                catch { /* Not found */ }
            }
        }

        private async void txt4_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    var cust = await ApiGetAsync($"api/customers/by-code/{txt4_Code.Text}");
                    if (cust != null)
                    {
                        txt4_Name.Text = cust["name"]?.ToString();
                        textCust_Money.Text = cust["balance"]?.ToString();
                        txt4_Phone.Text = cust["phone"]?.ToString();
                    }
                }
                catch { /* Not found */ }
            }
        }

        private async void txt6_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    var cust = await ApiGetAsync($"api/customers/by-code/{txt6_Code.Text}");
                    if (cust != null)
                    {
                        txt6_Name.Text = cust["name"]?.ToString();
                        txt6_Address.Text = cust["address"]?.ToString();
                        txt6_Phone.Text = cust["phone"]?.ToString();
                    }
                }
                catch { /* Not found */ }
            }
        }

        private async void txt6_Addres_Leave(object sender, EventArgs e)
        {
            try
            {
                var cust = await ApiGetAsync($"api/customers/by-code/{txt6_Code.Text}");
                if (cust != null)
                {
                    string Old_address = cust["address"]?.ToString() ?? "";
                    string New_address = txt6_Address.Text;
                    if (Old_address != New_address)
                    {
                        if (MessageBox.Show("ـ .. تم التغيير بالعنوان المسجل للعميل." + "\n" + "هل تريد حفظ التغيير على العنوان بشكل دائم ؟", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            cust["address"] = New_address;
                            using (var http = ConnectionManager.GetHttpClient())
                            {
                                var content = new StringContent(
                                    Newtonsoft.Json.JsonConvert.SerializeObject(cust),
                                    Encoding.UTF8, "application/json");
                                await http.PutAsync($"api/customers/{cust["id"]}", content);
                            }
                            MessageBox.Show("تم تعديل العنوان بنجاح");
                        }
                    }
                }
            }
            catch { }
        }

        private async void EmpName_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var data = await ApiGetAsync("api/employees");
                if (data != null)
                {
                    var empList = new List<object>();
                    foreach (var emp in data)
                    {
                        empList.Add(new { Emp_ID = emp["employeeCode"]?.ToString(), Emp_Name = emp["fullName"]?.ToString() });
                    }
                    EmpName.DataSource = empList;
                    EmpName.DisplayMember = "Emp_Name";
                    EmpName.ValueMember = "Emp_ID";
                }
            }
            catch { }
        }

        private void ChSale_CheckedChanged(object sender, EventArgs e)
        {
            if (ChSale.Checked == true)
            {
                txtSaleCode.Enabled = true;
            }
            else
            {
                txtSaleCode.Enabled = false;
            }
        }

        private async void txtSaleCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    var data = await ApiGetAsync("api/employees"); // Fallback to employees array if Users api isn't present
                    bool found = false;
                    if (data != null)
                    {
                        foreach (var emp in data)
                        {
                            if (emp["employeeCode"]?.ToString() == txtSaleCode.Text)
                            {
                                labSaleCode.Text = txtSaleCode.Text;
                                txtSaName.Text = emp["fullName"]?.ToString();
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                    {
                        labSaleCode.Text = txtSaleCode.Text;
                        txtSaName.Text = "!!";
                    }
                }
                catch { }
            }
        }

        private void xtraTabControl1_HeaderButtonClick(object sender, DevExpress.XtraTab.ViewInfo.HeaderButtonEventArgs e)
        {
            if (xtraTab.SelectedTabPage == xtraTameen)
            {
                textPercenc.Enabled = false;
            }
            else if (xtraTab.SelectedTabPage == xtraBe3)
            {
                textPercenc.Enabled = true;
            }
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Customer", "User_Customer");
            if (check == true)
            {
                Frm_Customer frm = new Frm_Customer();
                frm.ShowDialog();
            }
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.web);
        }

        private void btnComp_Click(object sender, EventArgs e)
        {
            bool check = checkUser("TameenCompany", "User_Sale");
            if (check == true)
            {
                Frm_Company frm = new Frm_Company();
                frm.Show();
            }
        }

        private void butEmp_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Add_Emp", "User_Emp");
            if (check == true)
            {
                Frm_EmployeeManagement frm = new Frm_EmployeeManagement();
                frm.ShowDialog();
            }
        }

        private void cbxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lookup insurance company from API later if needed
        }

        private void cbxCompany_DragDrop(object sender, DragEventArgs e)
        {
            //cbxCompany.Properties.DataSource = db.readData("select * from Company", "");
            //cbxCompany.Properties.DisplayMember = "Cop_Name";
            //cbxCompany.Properties.ValueMember = "Cop_ID";
            //cbxCompany.Properties.NullText = ""; // علشان يظهر فاضي في البداية LookUpEdit
        }

        private void t7ml_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // التحقق من الضغط على Enter
            {
                e.SuppressKeyPress = true; // منع صدور صوت التنبيه عند الضغط على Enter

                try
                {
                    decimal After = 0, Item_Discount = 0;

                    for (int i = 0; i <= DgvSale.Rows.Count - 1; i++)
                    {
                        Item_Discount = Convert.ToDecimal(t7ml.Text);

                        DgvSale.Rows[i].Cells["dgvDiscount"].Value = Item_Discount;
                        After = (Item_Discount / 100) * Convert.ToDecimal(DgvSale.Rows[i].Cells["dgvBefor"].Value);
                        //DgvSale.Rows[i].Cells["dgvDiscount"].Value = Item_Discount;
                        DgvSale.Rows[i].Cells["dgvTotal"].Value = After;

                    }
                }
                catch (Exception) { }
            }
        }

        private void t7ml_KeyPress(object sender, KeyPressEventArgs e)
        {
            // السماح بالأرقام، Backspace، ونقطة عشرية واحدة فقط
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // منع إدخال أكثر من نقطة عشرية
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            PayOrder("Completed");
        }

        private void t3le8_Click(object sender, EventArgs e)
        {
            PayOrder("Temp");
        }

        private void Coby_ID_Click(object sender, EventArgs e)
        {
            string coby = (DgvSale.CurrentRow.Cells["dgvBar"].Value).ToString();
            Clipboard.SetText(coby);
        }

        private void multi_ID_Click(object sender, EventArgs e)
        {
            // قائمة لحفظ كل الباركودات
            List<string> barcodeList = new List<string>();

            // افتراض أن اسم عمود الباركود هو dgvBar
            foreach (DataGridViewRow row in DgvSale.Rows)
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

        private async void butUnit_Click(object sender, EventArgs e)
        {
            // التأكد من أن هناك صف محدد
            if (DgvSale.CurrentRow == null) return;

            // الحصول على الصف المحدد
            DataGridViewRow selectedRow = DgvSale.CurrentRow;

            // التأكد من أن الصف يحتوي على باركود
            if (selectedRow.Cells["dgvBar"].Value == null) return;

            string barcode = selectedRow.Cells["dgvBar"].Value.ToString();
            string currentUnitId = selectedRow.Cells["dgvUnit"].Value?.ToString() ?? ""; // Guid unit ID الحالية

            try
            {
                JToken data = null;
                try
                {
                    data = await ApiGetAsync($"api/batches/by-barcode/{Uri.EscapeDataString(barcode)}");
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException || ex is System.Net.Sockets.SocketException || ex is TimeoutException)
                {
                    data = LocalCatalogCache.LookupBarcode(barcode);
                }

                if (data == null)
                {
                    try
                    {
                        data = await ApiGetAsync($"api/batches/by-barcode?barcode={barcode}");
                    }
                    catch { }
                }

                if (data != null)
                {
                    List<UnitItem> unitsList = new List<UnitItem>();

                    var mainUnitId = (data["mainUnitId"] ?? data["product"]?["mainUnitId"])?.ToString() ?? "";
                    var mainUnitName = (data["mainUnitName"] ?? data["product"]?["mainUnitName"])?.ToString() ?? "الوحدة الرئيسية";
                    if (!string.IsNullOrEmpty(mainUnitId))
                    {
                        unitsList.Add(new UnitItem { Id = mainUnitId, Name = mainUnitName });
                    }

                    var unitsNode = data["product"]?["units"] ?? data["units"];
                    if (unitsNode is JArray unitsArr)
                    {
                        foreach (var u in unitsArr)
                        {
                            var uId = (u["id"] ?? u["unitId"])?.ToString() ?? "";
                            var uName = (u["name"] ?? u["unitName"])?.ToString() ?? "";
                            if (!string.IsNullOrEmpty(uId) && !unitsList.Any(x => x.Id.Equals(uId, StringComparison.OrdinalIgnoreCase)))
                            {
                                unitsList.Add(new UnitItem { Id = uId, Name = uName });
                            }
                        }
                    }

                    if (unitsList.Count > 0)
                    {
                        int currentIndex = unitsList.FindIndex(u => u.Id.Equals(currentUnitId, StringComparison.OrdinalIgnoreCase));
                        int nextIndex = 0;
                        if (currentIndex != -1)
                        {
                            nextIndex = (currentIndex + 1) % unitsList.Count;
                        }
                        selectedRow.Cells["dgvUnit"].Value = unitsList[nextIndex].Id;
                    }
                }
            }
            catch { }
        }

        private void butQty_Click(object sender, EventArgs e)
        {
            // التأكد من أن هناك صف محدد في DataGridView
            if (DgvSale.CurrentRow == null) return;

            // الحصول على الصف المحدد
            DataGridViewRow selectedRow = DgvSale.CurrentRow;

            // التأكد من أن الخلية ليست فارغة
            if (selectedRow.Cells["dgvQty"].Value == null || selectedRow.Cells["dgvQty"].Value.ToString() == "")
            {
                selectedRow.Cells["dgvQty"].Value = 1; // تعيين القيمة إلى 1 إذا كانت فارغة
            }
            else
            {
                // تحويل القيمة إلى رقم وزيادتها بمقدار 1
                int currentQty = Convert.ToInt32(selectedRow.Cells["dgvQty"].Value);
                selectedRow.Cells["dgvQty"].Value = currentQty + 1;
            }
        }

        private void txtSaleCode_EditValueChanged(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => SendKeys.Send("{ENTER}")));
        }

        private void reprint_Click(object sender, EventArgs e)
        {
            Frm_mbe3at frm = new Frm_mbe3at();
            frm.ShowDialog();
        }

        private class UnitItem
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }
    }
}
