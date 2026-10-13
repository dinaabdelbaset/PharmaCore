using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_BuyQty : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();


        public Frm_BuyQty()
        {
            InitializeComponent();
        }

        // يستقبل البيانات
        private void Frm_BuyQty_Load(object sender, EventArgs e)
        {
            txtExpier.Text =(Properties.Settings.Default.Item_Expier).ToString();
            txtQty.Text =(Properties.Settings.Default.Item_Qty).ToString();
            txtBuyPrice.Text = (Properties.Settings.Default.Item_BuyPrice).ToString();
            txtSelas.Text = (Properties.Settings.Default.Item_Selas).ToString();
            txtBouns.Text = (Properties.Settings.Default.Item_Bouns).ToString();

            //try
            //{
            //    cbxUnit.DataSource = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
            //    cbxUnit.DisplayMember = "Unit_Name";
            //    cbxUnit.ValueMember = "Unit_ID";
            //}
            //catch (Exception) { }

            //cbxUnit.Text = (Properties.Settings.Default.Pro_Unit).ToString();
            txtQty.Focus();
        }

        // يعدل البيانات و يحفظها 
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "") { MessageBox.Show("من فضلك ادخل الكمية", "تاكيد"); return; }
            if (txtBuyPrice.Text == "") { MessageBox.Show("من فضلك ادخل سعر شراء الصيدلي", "تاكيد"); return; }
            if (txtSelas.Text == "") { MessageBox.Show("من فضلك ادخل سعر بيع الجمهور", "تاكيد"); return; }
            Properties.Settings.Default.Item_Expier =(txtExpier.Text);
            Properties.Settings.Default.Item_Qty =Convert.ToDecimal(txtQty.Text);
            Properties.Settings.Default.Item_Bouns = Convert.ToDecimal(txtBouns.Text);
            Properties.Settings.Default.Item_BuyPrice= Convert.ToDecimal(txtBuyPrice.Text);
            Properties.Settings.Default.Item_Selas= Convert.ToDecimal(txtSelas.Text);
            //Properties.Settings.Default.Pro_Unit = Convert.ToString(cbxUnit.Text);
            Properties.Settings.Default.Save();

            Close();
        }

        private void Frm_BuyQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(null, null);
            }
        }

        // يرسل البيانات للجدول بمعلومية الكود اللي في الشاشه هناك

        private void Frm_BuyQty_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                int index = Frm_Buy.GetFormBuy.DgvBuy.SelectedRows[0].Index;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells["dgvExpier"].Value = Properties.Settings.Default.Item_Expier;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells["dgvQty"].Value = Properties.Settings.Default.Item_Qty;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells["dgvPrice"].Value = Properties.Settings.Default.Item_BuyPrice;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells["dgvPriceZ"].Value = Properties.Settings.Default.Item_Selas;
                Frm_Buy.GetFormBuy.DgvBuy.Rows[index].Cells["dgvBouns"].Value = Properties.Settings.Default.Item_Bouns;

            }
            catch (Exception) { }
        }

        private void cbxUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //DataTable tblItems = new DataTable();
            //tblItems.Clear();
            //DataTable tblPrice = new DataTable();
            //tblPrice.Clear();
            //DataTable tblunit = new DataTable();
            //tblunit.Clear();
            //try
            //{
            //    int countQty = 0;
            //    try
            //    {
            //        countQty = Convert.ToInt32(db.readData("select sum (Pro_ID) from Products_Qty where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "").Rows[0][0]);
            //    }
            //    catch (Exception) { }


            //    tblPrice = db.readData("select * from Products_Qty where Pro_ID=" + Properties.Settings.Default.Pro_ID + "", "");
            //    string Product_Price = tblPrice.Rows[countQty - 1][4].ToString();

            //    //tblunit = db.readData("select * from Products_Unit where Pro_ID=" + Properties.Settings.Default.Pro_ID + " and Unit_ID=" + cbxUnit.SelectedValue+ "", "");
            //    //decimal realPrice = 0;
            //    //try
            //    //{
            //    //    realPrice = Convert.ToDecimal(Product_Price) / Convert.ToDecimal(tblunit.Rows[0][3]);
            //    //}
            //    //catch (Exception) { }
            //    txtBuyPrice.Text = Math.Round(realPrice, 2) + "";
            //}
            //catch (Exception) { }
        }
    }
}