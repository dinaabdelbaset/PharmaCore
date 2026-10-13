using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Company : DevExpress.XtraEditors.XtraForm
    {
        DataTable tblGroup = new DataTable();

        public Frm_Company()
        {
            InitializeComponent();
        }

        private void Frm_Company_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void AutoNumber()
        {
            tblGroup.Rows.Clear();
            DataCust.DataSource = tblGroup;

            txtID.Text = "1";
            txtName.EditValue = null;
            txtPhone.EditValue = null;
            txtNotes.Clear();
            txtWeb.EditValue = null;
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            txtName.Focus();

            txtTotal.Text = (DataCust.Rows.Count).ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            MessageBox.Show("إضافة الشركات معطلة بعد إزالة SQL", "تنبيه");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("تعديل القراءة معطل بعد إزالة SQL", "تنبيه");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("حفظ التعديلات معطل بعد إزالة SQL", "تنبيه");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("المسح معطل بعد إزالة SQL", "تنبيه");
        }

        private void Frm_company_FormClosing(object sender, FormClosingEventArgs e){ /* legacy close logic removed */ }
    }
}
