using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
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
    public partial class Frm_AddStock : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();
        int row;


        public Frm_AddStock()
        {
            InitializeComponent();
        }

        private void Frm_AddStock_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }


        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Stock_ID) from Stock_Data", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.EditValue = null;
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;

        }

        private void ShowData()
        {
            try
            {
                tbl.Clear();
                tbl = db.readData("select * from Stock_Data", "");

                if (tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("لا يوجد بيانات فى هذه الشاشه");
                }
                else
                {
                    txtID.Text = tbl.Rows[row][0].ToString();
                    txtName.Text = tbl.Rows[row][1].ToString();
                }

                btnAdd.Enabled = false;
                btnNew.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الخزنة");
                return;
            }
            db.exceuteData("insert into Stock_Data Values (" + txtID.Text + " ,N'" + txtName.Text + "')", "تم الادخال بنجاح");

            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم الخزنة");
                return;
            }
            db.exceuteData("update  Stock_Data set  Stock_Name=N'" + txtName.Text + "' where Stock_ID=" + txtID.Text + " ", "تم الحفظ بنجاح");

            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from Stock_Data where Stock_ID=" + txtID.Text + "", "تم مسح البيانات بنجاح");
                AutoNumber();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            row = 0;
            ShowData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (row == 0)
            {
                tbl.Clear();
                tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
                row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
                ShowData();
            }
            else
            {


                row--;
                ShowData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            tbl.Clear();
            tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
            if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
            {
                row = 0;
                ShowData();
            }
            else
            {
                row++;
                ShowData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select count(Stock_ID) from Stock_Data", "");
            row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
            ShowData();
        }
    }
}