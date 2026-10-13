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
    public partial class Frm_Uses : DevExpress.XtraEditors.XtraForm
    {


        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        DataTable tblUses = new DataTable();


        public Frm_Uses()
        {
            InitializeComponent();
        }

        private void Frm_Uses_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void AutoNumber()
        {
            tblUses.Clear();
            tblUses = db.readData("select Uses_ID as 'المسلسل' ,Uses_Name as 'نوع الإستخدام' from Uses", "");
            DgvSearch.DataSource = tblUses;

            tbl.Clear();
            tbl = db.readData("select max (Uses_ID) from Uses", "");

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

            txtTotal.Text = (DgvSearch.Rows.Count).ToString();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل نوع الإستخدام");
                return;
            }
            db.exceuteData("insert into Uses Values (" + txtID.Text + " ,N'" + txtName.Text + "')", "تم الادخال بنجاح");

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
                MessageBox.Show("من فضلك ادخل نوع الإستخدام");
                return;
            }
            db.readData("update Uses set Uses_Name=N'" + txtName.Text + "'  where Uses_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح نوع الإستخدام المحدد بشكل دائم ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Uses where Uses_ID=" + id + "", "");
                        MessageBox.Show("تم المسح بنجاح");
                        AutoNumber();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("لا يوجد نوع الإستخدام لحذفه");
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            DataTable tblSearch = new DataTable();
            tblSearch.Clear();

            tblSearch = db.readData("select * from Uses where Uses_ID=" + id + " ", "");
            try
            {
                txtID.Text= tblSearch.Rows[0][0].ToString();
                txtName.Text = tblSearch.Rows[0][1].ToString();

                btnSave.Enabled = true;
                btnAdd.Enabled = false;
            }
            catch (Exception) { }
        }
    }
}