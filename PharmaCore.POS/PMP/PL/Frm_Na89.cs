using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Na89 : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_Na89()
        {
            InitializeComponent();
        }

        private void Frm_Na89_Load(object sender, EventArgs e)
        {
            FillItems();
            AutoNumber();
        }

        private void FillItems()
        {

            cbxItems.DataSource = db.readData("select * from Products", "");
            cbxItems.DisplayMember = "Pro_Name";
            cbxItems.ValueMember = "Pro_ID";

        }

        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData(" SELECT [N89_ID] as 'مسلسل' ,[N89_Name] as 'المنتج' ,[barcode] as 'باركود' ,[N89_Have] as 'الكمية المتوفرة' ,[N89_Need] as 'الكمية المطلوبة' ,[Username] as 'المستخدم' ,[Date] as 'التاريخ' FROM [dbo].[N89_Table] ", "");
            DgvSearch.DataSource = tbl;

            DtpDate.Text = DateTime.Now.ToShortDateString();
            try
            {
                cbxItems.SelectedIndex = 0;
            }
            catch (Exception) { };
            cbxItems.Text = "اختر منتج";
            txtbarcode.Focus();
            txtTotalQty.Text = (DgvSearch.Rows.Count).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtNeed.EditValue) <= 0)
            {
                MessageBox.Show("من فضلك ادخل الكمية المطلوبة ");
                return;
            }
            string proName = "";
            proName = (db.readData("select Pro_Name from Products where Barcode=N'" + txtbarcode.Text + "'", "").Rows[0][0]).ToString();

            db.exceuteData("insert into N89_Table values ( N'" + proName + "' ,N'" + txtbarcode.Text + "' ,N'" + txtHave.Text + "' ,N'" + txtNeed.Text + "' , N'"+Properties.Settings.Default.USERNAME+"' ,N'" + DtpDate.Text + "'  )", "");
            AutoNumber();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtNeed.EditValue) <= 0)
            {
                MessageBox.Show("من فضلك ادخل الكمية المطلوبة ");
                return;
            }
            string proBarcode = "";
            proBarcode = (db.readData("select Barcode from Products where Pro_ID=" + cbxItems.SelectedValue + "", "").Rows[0][0]).ToString();

            db.exceuteData("insert into N89_Table values ( N'" + cbxItems.Text + "' ,N'" + proBarcode + "'  ,N'" + txtHave.Text + "' ,N'" + txtNeed.Text + "' , N'"+Properties.Settings.Default.USERNAME+"' ,N'" + DtpDate.Text + "'  )", "");
            AutoNumber();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح المنتج بشكل نهائي من كامل البرنامج ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string bar = "";
                bar = (DgvSearch.CurrentRow.Cells[2].Value).ToString();

                if (bar == "New")
                {
                    button3_Click(null, null);
                }
                else
                {
                    try
                    {

                        int proID = 1;
                        proID = Convert.ToInt32(db.readData("select Pro_ID from Products where Pro_Name=N'"+ DgvSearch.CurrentRow.Cells[1].Value + "' and Barcode = N'"+ DgvSearch.CurrentRow.Cells[2].Value + "' ", "").Rows[0][0]);

                        db.readData("delete from N89_Table where N89_ID= " + DgvSearch.CurrentRow.Cells[0].Value + " ", "");
                        db.readData("delete from Products where Pro_ID= " + proID + " ", "تم مسح البيانات بنجاح");
                        AutoNumber();
                    }
                    catch { }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح المنتج من القائمة ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from N89_Table where N89_ID= " + DgvSearch.CurrentRow.Cells[0].Value + " ", "تم مسح البيانات بنجاح");
                AutoNumber();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtNeed.EditValue) <= 0)
            {
                MessageBox.Show("من فضلك ادخل الكمية المطلوبة ");
                return;
            }
            db.exceuteData("insert into N89_Table values ( N'" + txtNew.Text + "' ,N'New' ,N'" + txtHave.Text + "' ,N'" + txtNeed.Text + "' , N'"+Properties.Settings.Default.USERNAME+"' ,N'" + DtpDate.Text + "'  )", "");
            AutoNumber();
        }
    }
}