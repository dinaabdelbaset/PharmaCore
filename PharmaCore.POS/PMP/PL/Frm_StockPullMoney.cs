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
    public partial class Frm_StockPullMoney : DevExpress.XtraEditors.XtraForm
    {

        DataTable tbl = new DataTable();
        BL.DDatabase db = new BL.DDatabase();


        public Frm_StockPullMoney()
        {
            InitializeComponent();
        }

        private void Frm_StockPullMoney_Load(object sender, EventArgs e)
        {
            try
            {
                DtpDate.Text = DateTime.Now.ToShortDateString();
                onLoadScreen();
            }
            catch (Exception) { }
        }


        private void onLoadScreen()
        {
            fillStock();
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.exceuteData("insert into Stock values (" + cbxStock.SelectedValue + " , 0)", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoney.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoney.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }
            NudPrice.EditValue = 1;
            txtName.EditValue = null;
            txtreason.Clear();

        }

        private void fillStock()
        {
            cbxStock.DataSource = db.readData("select * from Stock_Data", "");
            cbxStock.DisplayMember = "Stock_Name";
            cbxStock.ValueMember = "Stock_ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbxStock.Items.Count >= 1)
            {
                string d = DtpDate.Value.ToString("dd/MM/yyyy");
                if (txtName.Text == "") { MessageBox.Show("من فضلك ادخل اسم الساحب", "تاكيد"); return; }
                if (Convert.ToDecimal(NudPrice.EditValue) <= 0) { MessageBox.Show("لابد ان يكون مبلغ السحب اكبر من صفر", "تاكيد"); return; }


                //if(NudPrice.Value >Convert.ToDecimal( lblMoney.Text))
                //{
                //    MessageBox.Show("لا يمكن سحب قيمه اكبر من الموجوده فى الخزنة", "تاكيد"); return;
                //}

                tbl.Clear();
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
                if (Convert.ToDecimal(NudPrice.EditValue) > Convert.ToDecimal(tbl.Rows[0][1]))
                {
                    MessageBox.Show("لا يمكن سحب قيمه اكبر من الموجوده فى الخزنة", "تاكيد"); return;
                }
                db.exceuteData("update Stock set Money=Money - " + NudPrice.EditValue + " where Stock_ID=" + cbxStock.SelectedValue + "", "");

                db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + cbxStock.SelectedValue + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'سحب يدوي', N'" + txtreason.Text + "') ", "تم السحب بنجاح");
                onLoadScreen();
            }
        }

        private void cbxStock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                tbl.Clear();
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
                if (tbl.Rows.Count <= 0)
                {
                    db.exceuteData("insert into Stock values (" + cbxStock.SelectedValue + " , 0)", "");
                    tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
                }
                if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
                {
                    lblMoney.Text = "0";
                }
                else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
                {
                    lblMoney.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
                }
            }
            catch (Exception) { }
        }
    }
}