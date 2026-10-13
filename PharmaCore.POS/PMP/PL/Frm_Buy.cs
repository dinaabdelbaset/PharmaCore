using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using PharmaCore.POS.RE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraReports.UI;
using Microsoft.SqlServer.Management.Smo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace PharmaCore.POS.PL
{
    public partial class Frm_Buy : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();

        private static Frm_Buy frm;
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static Frm_Buy GetFormBuy
        {
            get
            {
                if (frm == null)
                {
                    frm = new Frm_Buy();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public Frm_Buy()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
        }

        private void Frm_Buy_Load(object sender, EventArgs e)
        {
            FillItems();
            FillSupplier();
            FillStore();
            try
            {
                AutoNumber();
            }
            catch (Exception) { }
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
            lblUsername.Text = Properties.Settings.Default.USERNAME;
        }

        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Buy", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            DtpDate.Text = DateTime.Now.ToShortDateString();
            DtpAagel.Text = DateTime.Now.ToShortDateString();
            try
            {
                cbxItems.SelectedIndex = 0;
                cbxSupplier.SelectedIndex = 0;
                cbxStore.SelectedIndex = 0;
            }
            catch (Exception) { };
            cbxItems.Text = "اختر منتج";
            DgvBuy.Rows.Clear();
            rbtnCash.Checked = true;
            txtbarcode.EditValue = null;
            txtbarcode.Focus();
            txtTotal.EditValue = null;
        }

        public void FillItems()
        {
            cbxItems.DataSource = db.readData("select * from Products", "");
            cbxItems.DisplayMember = "Pro_Name";
            cbxItems.ValueMember = "Pro_ID";
        }
        public void FillSupplier()
        {
            cbxSupplier.DataSource = db.readData("select * from Suppliers", "");
            cbxSupplier.DisplayMember = "Sup_Name";
            cbxSupplier.ValueMember = "Sup_ID";
        }
        public void FillStore()
        {
            cbxStore.DataSource = db.readData("select * from Store", "");
            cbxStore.DisplayMember = "Store_Name";
            cbxStore.ValueMember = "Store_ID";
        }

        private void btnSupplierbrowse_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Open_Form = 5;
            Properties.Settings.Default.Save();
            Frm_Suppliers frm = new Frm_Suppliers();
            frm.ShowDialog();
        }

        public void AddItems(int Type)
        {
            DataTable tblItems = new DataTable();
            tblItems.Clear();

            if (Type == 1)
            {
                if (cbxItems.Text == "اختر منتج")
                {
                    MessageBox.Show("من فضلك اختر منتج", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cbxItems.Items.Count <= 0)
                {
                    MessageBox.Show("من فضلك ادخل المنتجات اولا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                tblItems = db.readData("select * , FORMAT (Expier , 'yyyy/MM') as dateExpier from Products where Pro_ID=" + cbxItems.SelectedValue + "", "");
            }
            else if (Type == 2)
            {
                tblItems = db.readData("select * , FORMAT (Expier , 'yyyy/MM') as dateExpier from Products where Barcode=N'" + txtbarcode.Text + "'", "");
            }
            else if (Type == 3)
            {
                tblItems = db.readData("select * , FORMAT (Expier , 'yyyy/MM') as dateExpier from Products where Pro_ID=" + Properties.Settings.Default.sales_ID + "", "");
            }

            if (tblItems.Rows.Count >= 1)
            {
                string Product_ID = tblItems.Rows[0]["Pro_ID"].ToString();
                string Product_Name = tblItems.Rows[0]["Pro_Name"].ToString();
                string Product_Expier = tblItems.Rows[0]["dateExpier"].ToString();
                decimal Product_Store = Convert.ToDecimal(tblItems.Rows[0]["Qty"]);
                string Product_Unit = tblItems.Rows[0]["Buy_UnitName"].ToString();
                string Product_Qty = "1";
                string Product_Price = tblItems.Rows[0]["PH_Price"].ToString();
                string Product_Pricez = tblItems.Rows[0]["Sale_PriceTax"].ToString();
                decimal Product_bouns = 0;

                if (DgvBuy.Rows.Count >= 1)
                {
                    for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                    {
                        int iDProduct = Convert.ToInt32(Product_ID);
                        if (iDProduct == Convert.ToInt32(DgvBuy.Rows[i].Cells["dgvID"].Value))
                        {
                            DgvBuy.Rows[i].Cells["dgvQty"].Value = Convert.ToDecimal(DgvBuy.Rows[i].Cells["dgvQty"].Value) + 1;
                            return;
                        }
                    }
                }

                DgvBuy.Rows.Add(1);
                int rowindex = DgvBuy.Rows.Count - 1;
                DgvBuy.Rows[rowindex].Cells["dgvID"].Value = Product_ID;
                DgvBuy.Rows[rowindex].Cells["dgvName"].Value = Product_Name;
                DgvBuy.Rows[rowindex].Cells["dgvExpier"].Value = Product_Expier;
                DgvBuy.Rows[rowindex].Cells["dgvStore"].Value = Math.Round(Product_Store, 2).ToString();
                DgvBuy.Rows[rowindex].Cells["dgvUnit"].Value = Product_Unit;
                DgvBuy.Rows[rowindex].Cells["dgvQty"].Value = Product_Qty;
                DgvBuy.Rows[rowindex].Cells["dgvPrice"].Value = Product_Price;
                DgvBuy.Rows[rowindex].Cells["dgvPriceZ"].Value = Product_Pricez;
                DgvBuy.Rows[rowindex].Cells["dgvBouns"].Value = Product_bouns;
                decimal total = Convert.ToDecimal(Product_Qty) * Convert.ToDecimal(Product_Price);
                DgvBuy.Rows[rowindex].Cells["dgvTotal"].Value = Math.Round(total, 2);
            }
            else
            {
                MessageBox.Show("هناك خطأ في إدخال هذا المنتج الرجاء ، مراجعه المنتج", "تاكيد");
                return;
            }

            try
            {
                decimal TotalOrder = 0;
                for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                {
                    TotalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells["dgvTotal"].Value);
                }
                DgvBuy.ClearSelection();
                DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                DgvBuy.Rows[DgvBuy.Rows.Count - 1].Selected = true;
                txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                lblItemsCount.Text = (DgvBuy.Rows.Count).ToString();
                AutoNumic();
            }
            catch (Exception) { }
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            AddItems(1);
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AddItems(2);
                AutoNumic();
                txtbarcode.EditValue = null;
                txtbarcode.Focus();
            }
        }

        public void AutoNumic()
        {
            for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
            {
                DgvBuy.Rows[i].Cells["I"].Value = (i + 1).ToString();
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (DgvBuy.Rows.Count >= 1)
            {
                int index = DgvBuy.SelectedRows[0].Index;
                DgvBuy.Rows.RemoveAt(index);
                if (DgvBuy.Rows.Count <= 0)
                {
                    txtTotal.Text = "0";
                }
                try
                {
                    decimal TotalOrder = 0;
                    for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                    {
                        TotalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells["dgvTotal"].Value);
                        DgvBuy.ClearSelection();
                        DgvBuy.FirstDisplayedScrollingRowIndex = DgvBuy.Rows.Count - 1;
                        DgvBuy.Rows[DgvBuy.Rows.Count - 1].Selected = true;
                    }
                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                    lblItemsCount.Text = (DgvBuy.Rows.Count).ToString();
                }
                catch (Exception) { }
            }
            AutoNumic();
        }

        string stock_ID = "";

        // طباعة الفاتورة: تعليق مؤقت حتى توفير ملفات التقارير
        private void Print()
        {
            MessageBox.Show("ميزة طباعة الفاتورة غير متاحة حالياً (ملفات التقرير مفقودة)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateQty()
        {
            if (DgvBuy.Rows.Count >= 1)
            {
                int index = DgvBuy.SelectedRows[0].Index;
                Properties.Settings.Default.Item_Expier = (DgvBuy.Rows[index].Cells["dgvExpier"].Value).ToString();
                Properties.Settings.Default.Item_Qty = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvQty"].Value);
                Properties.Settings.Default.Item_BuyPrice = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvPrice"].Value);
                Properties.Settings.Default.Item_Selas = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvPriceZ"].Value);
                Properties.Settings.Default.Item_Bouns = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvBouns"].Value);
                Properties.Settings.Default.Pro_ID = Convert.ToInt32(DgvBuy.Rows[index].Cells["dgvID"].Value);
                Properties.Settings.Default.Save();
                Frm_BuyQty frm = new Frm_BuyQty();
                frm.ShowDialog();
                try
                {
                    int index2 = DgvBuy.SelectedRows[0].Index;
                    DgvBuy.Rows[index2].Cells["dgvExpier"].Value = Properties.Settings.Default.Item_Expier;
                    DgvBuy.Rows[index2].Cells["dgvQty"].Value = Properties.Settings.Default.Item_Qty;
                    DgvBuy.Rows[index2].Cells["dgvPrice"].Value = Properties.Settings.Default.Item_BuyPrice;
                    DgvBuy.Rows[index2].Cells["dgvPriceZ"].Value = Properties.Settings.Default.Item_Selas;
                    DgvBuy.Rows[index2].Cells["dgvBouns"].Value = Properties.Settings.Default.Item_Bouns;
                }
                catch (Exception) { }
            }
        }

        private void Frm_Buy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Properties.Settings.Default.Open_Form = 1;
                Properties.Settings.Default.Save();
                Frm_ItemSales frm = new Frm_ItemSales();
                frm.ShowDialog();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnItems_Click(null, null);
            }
            else if (e.KeyCode == Keys.F3)
            {
                txtbarcode.EditValue = null;
                txtbarcode.Focus();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDeleteItem_Click(null, null);
            }
            else if (e.KeyCode == Keys.F11)
            {
                UpdateQty();
            }
        }

        private void DgvBuy_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal Item_Qty = 0, Item_BuyPrice = 0;
            try
            {
                int index = DgvBuy.SelectedRows[0].Index;
                Item_Qty = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvQty"].Value);
                Item_BuyPrice = Convert.ToDecimal(DgvBuy.Rows[index].Cells["dgvPrice"].Value);
                try
                {
                    decimal Total = (Item_Qty * Item_BuyPrice);
                    DgvBuy.Rows[index].Cells["dgvTotal"].Value = Total;
                    decimal TotalOrder = 0;
                    for (int i = 0; i <= DgvBuy.Rows.Count - 1; i++)
                    {
                        TotalOrder += Convert.ToDecimal(DgvBuy.Rows[i].Cells["dgvTotal"].Value);
                    }
                    txtTotal.Text = Math.Round(TotalOrder, 2).ToString();
                }
                catch (Exception) { }
                AutoNumic();
            }
            catch (Exception) { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //PayOrder(); (معلق)
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            UpdateQty();
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Open_Form = 4;
            Properties.Settings.Default.Save();
            Frm_Products frm = new Frm_Products();
            frm.ShowDialog();
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            // الاستدعاء الأصلي للطباعة معلق حتى تتوفر ملفات التقارير
            Print();
        }

        private void DgvBuy_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = DgvBuy.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)DgvBuy.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)DgvBuy.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }
    }
}
