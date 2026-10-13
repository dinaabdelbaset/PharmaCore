using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ViewItem : DevExpress.XtraEditors.XtraForm
    {
        DataTable tblSearch = new DataTable();

        public Frm_ViewItem()
        {
            InitializeComponent();
            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DataPro);
        }

        private void Frm_ViewItem_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tblSearch.Rows.Clear();
            DataPro.DataSource = tblSearch;
            txtTotal.Text = "0";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("مسح المنتج معطل بعد إزالة SQL", "تنبيه");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tblSearch.Rows.Clear();
            DataPro.DataSource = tblSearch;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tblSearch.Rows.Clear();
            DataPro.DataSource = tblSearch;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button3_Click(null, null);
        }

        private void textActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button5_Click(null, null);
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                tblSearch.Rows.Clear();
                DataPro.DataSource = tblSearch;
            }
        }
    }
}