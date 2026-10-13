using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using PharmaCore.POS.BL;
using PharmaCore.POS.Enums;
using PharmaCore.POS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_SaleQty : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();

        // ✅ هل الوضع الحالي API؟
        private bool IsApi => ConnectionManager.CurrentMode == ConnectionMode.Api;

        public Frm_SaleQty()
        {
            InitializeComponent();
        }

        private async void Frm_SaleQty_Load(object sender, EventArgs e)
        {
            txtQty.Text = (Properties.Settings.Default.Item_Qty).ToString();
            txtSalePrice.Text = (Properties.Settings.Default.Item_SalePrice).ToString();
            txtDiscount.Text = (Properties.Settings.Default.Item_Discount).ToString();

            if (IsApi)
            {
                await LoadUnitsFromApiAsync();
            }
            else
            {
                try
                {
                    cbxUnit.DataSource = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
                    cbxUnit.DisplayMember = "Unit_Name";
                    cbxUnit.ValueMember = "Unit_ID";
                }
                catch (Exception) { }
            }

            cbxUnit.Text = (Properties.Settings.Default.Pro_Unit).ToString();
            txtQty.Focus();
        }

        private async System.Threading.Tasks.Task LoadUnitsFromApiAsync()
        {
            try
            {
                // We get the barcode from the current DGV row via sales_ID
                var barcode = Properties.Settings.Default.sales_ID.ToString();
                using (var http = ConnectionManager.GetHttpClient())
                {
                    var res = await http.GetAsync($"api/batches/by-barcode/{Uri.EscapeDataString(barcode)}");
                    if (!res.IsSuccessStatusCode) return;

                    var json = await res.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json);

                    var dt = new DataTable();
                    dt.Columns.Add("Unit_ID", typeof(string));
                    dt.Columns.Add("Unit_Name", typeof(string));
                    dt.Columns.Add("UnitSalePrice", typeof(decimal));
                    dt.Columns.Add("QtyPerMainUnit", typeof(int));

                    // Add main unit
                    dt.Rows.Add(
                        data["mainUnitId"]?.ToString() ?? "",
                        data["mainUnitName"]?.ToString() ?? "الوحدة الرئيسية",
                        data["publicSalePrice"]?.ToObject<decimal>() ?? 0m,
                        1
                    );

                    // Add additional units
                    var units = data["units"] as JArray;
                    if (units != null)
                    {
                        foreach (var u in units)
                        {
                            dt.Rows.Add(
                                u["unitId"]?.ToString() ?? "",
                                u["unitName"]?.ToString() ?? "",
                                u["unitSalePrice"]?.ToObject<decimal>() ?? 0m,
                                u["qtyPerMainUnit"]?.ToObject<int>() ?? 1
                            );
                        }
                    }

                    cbxUnit.DataSource = dt;
                    cbxUnit.DisplayMember = "Unit_Name";
                    cbxUnit.ValueMember = "Unit_ID";
                }
            }
            catch (Exception) { }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "") { MessageBox.Show("من فضلك ادخل الكمية", "تاكيد"); return; }
            if (txtSalePrice.Text == "") { MessageBox.Show("من فضلك ادخل سعر البيع", "تاكيد"); return; }
            if (txtDiscount.Text == "") { MessageBox.Show("من فضلك ادخل  الخصم", "تاكيد"); return; }

            if (!IsApi)
            {
                // Legacy stock check
                decimal qtyi = Convert.ToDecimal(txtQty.Text);
                DataTable table = new DataTable();
                table.Clear();
                table = db.readData("select SUM(Qty) from Products_Qty where Pro_ID = " + Properties.Settings.Default.Pro_ID + " and Store_ID = " + Properties.Settings.Default.StoreID + "", "");
                if (table.Rows[0][0].ToString() != DBNull.Value.ToString())
                {
                    qtyi = Convert.ToDecimal(table.Rows[0][0]);
                }
                else
                {
                    qtyi = 0;
                }
                if (qtyi < Convert.ToDecimal(txtQty.Text))
                {
                    XtraMessageBoxArgs arg = new XtraMessageBoxArgs(); arg.AutoCloseOptions.Delay = 20000; arg.Caption = "منتج غير متوفر"; arg.Text = "لاتستطيع بيع منتج غير متوفر لتمكين هذه الخاصية يرجى الذهاب الى اعدادات البرنامج الرئيسية"; arg.Buttons = new DialogResult[] { DialogResult.OK }; XtraMessageBox.Show(arg); return;
                }
            }

            Properties.Settings.Default.Item_Qty = Convert.ToDecimal(txtQty.Text);
            Properties.Settings.Default.Item_SalePrice = Convert.ToDecimal(txtSalePrice.Text);
            Properties.Settings.Default.Pro_Unit = cbxUnit.SelectedIndex;

            if (Properties.Settings.Default.SaleDiscountForCasher == true)
            {
                try
                {
                    if (Properties.Settings.Default.ItemDiscount == "Value")
                    {
                        Properties.Settings.Default.Item_Discount = Convert.ToDecimal(txtDiscount.Text);
                        Properties.Settings.Default.Save();
                    }
                    else if (Properties.Settings.Default.ItemDiscount == "Present")
                    {
                        decimal presentValue = 0;
                        presentValue = (Convert.ToDecimal(txtSalePrice.Text) / 100) * Convert.ToDecimal(txtDiscount.Text);
                        Properties.Settings.Default.Item_Discount = presentValue;
                        Properties.Settings.Default.Save();
                    }
                }
                catch (Exception) { }
            }
            else
            {
                if (Convert.ToDecimal(txtDiscount.Text) >= 1)
                {
                    MessageBox.Show("غير مسموح لك بعمل خصم على المنتج", "تاكيد");
                    txtDiscount.Text = "0";
                    return;
                }
            }

            Properties.Settings.Default.Save();
            Close();
        }

        private void Frm_SaleQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
            }
        }

        private void Frm_SaleQty_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                int index = Frm_Sales.GetFormSales.DgvSale.SelectedRows[0].Index;
                var selectedUnitId = cbxUnit.SelectedValue?.ToString() ?? "";
                if (!string.IsNullOrEmpty(selectedUnitId))
                {
                    Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells["dgvUnit"].Value = selectedUnitId;
                }
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells["dgvQty"].Value = Properties.Settings.Default.Item_Qty;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells["dgvPrice"].Value = Properties.Settings.Default.Item_SalePrice;
                Frm_Sales.GetFormSales.DgvSale.Rows[index].Cells["dgvDiscount"].Value = Properties.Settings.Default.Item_Discount;
            }
            catch (Exception) { }
        }

        private void cbxUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (IsApi)
            {
                // Get price from the loaded DataTable
                try
                {
                    var dt = cbxUnit.DataSource as DataTable;
                    if (dt != null && cbxUnit.SelectedIndex >= 0 && cbxUnit.SelectedIndex < dt.Rows.Count)
                    {
                        decimal price = Convert.ToDecimal(dt.Rows[cbxUnit.SelectedIndex]["UnitSalePrice"]);
                        int qtyPerMain = Convert.ToInt32(dt.Rows[cbxUnit.SelectedIndex]["QtyPerMainUnit"]);
                        decimal realPrice = qtyPerMain > 0 ? price / qtyPerMain : price;
                        txtSalePrice.Text = Math.Round(realPrice, 2).ToString();
                    }
                }
                catch (Exception) { }
                return;
            }

            // ── Legacy SQL ──
            DataTable tblItems = new DataTable();
            tblItems.Clear();
            DataTable tblunit = new DataTable();
            tblunit.Clear();
            try
            {
                tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue + "", "");
                decimal realPrice2 = 0;
                try
                {
                    realPrice2 = Convert.ToDecimal(tblunit.Rows[0][5]) / Convert.ToDecimal(tblunit.Rows[0][3]);
                }
                catch (Exception) { }
                txtSalePrice.Text = Math.Round(realPrice2, 2) + "";
            }
            catch (Exception) { }
        }
    }
    
}