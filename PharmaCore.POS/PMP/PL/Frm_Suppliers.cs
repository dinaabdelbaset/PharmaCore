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
using System.Xml.Linq;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Suppliers : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();
        DataTable tblGroup = new DataTable();


        public Frm_Suppliers()
        {
            InitializeComponent();

            AutoNumber();
        }

        private void Frm_Suppliers_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void AutoNumber()
        {
            tblGroup.Clear();
            tblGroup = db.readData(" SELECT [Sup_ID] as 'مسلسل' ,[Sup_Name] as 'الإسم'  ,[Sup_Phone1] as 'تليفون 1' ,[Sup_Phone2] as 'تليفون 2' ,[Sup_Address] as 'العنوان',[Notes] as 'الملاحظات' FROM [dbo].[Suppliers] ", "");
            DataSup.DataSource = tblGroup;

            tbl.Clear();
            tbl = db.readData("select max (Sup_ID) from Suppliers", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.EditValue = null;
            txtNotes.Clear();
            txtPhone1.EditValue = null;
            txtPhone2.EditValue = null;
            txtAddress.Clear();
            txtName.Focus();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;


            txtTotal.Text = (DataSup.Rows.Count).ToString();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المورد");
                return;
            }
            db.exceuteData("insert into Suppliers Values (" + txtID.Text + " ,N'" + txtName.Text + "' ,N'" + txtAddress.Text + "'  ,N'" + txtPhone1.Text + "' ,N'" + txtPhone2.Text + "' ,N'" + txtNotes.Text + "')", "تم الادخال بنجاح");

            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.readData("update Suppliers set Sup_Name=N'" + txtName.Text + "' ,Sup_Address =N'" + txtAddress.Text + "' ,Sup_Phone1=N'" + txtPhone1.Text + "' ,Sup_Phone2=N'" + txtPhone2.Text + "' , Notes=N'" + txtNotes.Text + "' where Sup_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DataSup.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح بيانات المورد المحدد بشكل دائم ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Suppliers where Sup_ID=" + id + "", "");
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
                MessageBox.Show("لا يوجد مورد لحذفه");
            }
        }

        private void Frm_Suppliers_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.Open_Form == 5)
                {
                    Frm_Buy.GetFormBuy.FillSupplier();
                }
            }
            catch (Exception) { }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DataSup.CurrentRow.Cells[0].Value);

            DataTable tblSearch = new DataTable();
            tblSearch.Clear();

            tblSearch = db.readData("select * from Suppliers where Sup_ID=" + id + " ", "");
            try
            {
                txtID.Text= tblSearch.Rows[0][0].ToString();
                txtName.Text = tblSearch.Rows[0][1].ToString();
                txtAddress.Text = tblSearch.Rows[0][2].ToString();
                txtPhone1.Text = tblSearch.Rows[0][3].ToString();
                txtPhone2.Text = tblSearch.Rows[0][4].ToString();
                txtNotes.Text = tblSearch.Rows[0][5].ToString();

                btnSave.Enabled = true;
                btnAdd.Enabled = false;

            }
            catch (Exception) { }
        }
    }
    
}