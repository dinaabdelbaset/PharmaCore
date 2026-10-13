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
    public partial class Frm_BankPullMoney : DevExpress.XtraEditors.XtraForm
    {
        DataTable tbl = new DataTable();
        BL.DDatabase db = new BL.DDatabase();


        public Frm_BankPullMoney()
        {
            InitializeComponent();
        }

        private void Frm_BankPullMoney_Load(object sender, EventArgs e)
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
            tbl.Clear();
            tbl = db.readData("select * from Bank ", "");
            if (tbl.Rows.Count <= 0)
            {
                db.exceuteData("insert into Bank values (0)", "");
                tbl = db.readData("select * from Bank ", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][0]) <= 0)
            {
                lblMoney.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][0]) >= 1)
            {
                lblMoney.Text = Convert.ToDecimal(tbl.Rows[0][0]) + "";
            }
            NudPrice.EditValue = 1;
            txtName.EditValue = null;
            txtreason.EditValue = null;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName.Text == "") { MessageBox.Show("من فضلك ادخل اسم الساحب", "تاكيد"); return; }
            if (Convert.ToDecimal(NudPrice.EditValue) <= 0) { MessageBox.Show("لابد ان يكون مبلغ السحب اكبر من صفر", "تاكيد"); return; }

            if (Convert.ToDecimal(NudPrice.EditValue) > Convert.ToDecimal(lblMoney.Text))
            {
                MessageBox.Show("لا يمكن سحب قيمه اكبر من الموجوده فى البنك", "تاكيد"); return;
            }
            db.exceuteData("update Bank set Money=Money - " + NudPrice.EditValue + " ", "");
            db.exceuteData("insert into Bank_Pull ( Money ,Date ,Name ,Type ,Reason) values (" + NudPrice.EditValue + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'سحب يدوي', N'" + txtreason.Text + "') ", "تم السحب بنجاح");
            onLoadScreen();
        }
    }
}