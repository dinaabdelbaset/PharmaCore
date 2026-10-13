using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_DeservedReport : DevExpress.XtraEditors.XtraForm
    {
        DataTable tbl = new DataTable();
        public Frm_DeservedReport(){ InitializeComponent(); }
        private void Frm_DeservedReport_Load(object sender, EventArgs e){ DtpFrom.Text = DateTime.Now.ToShortDateString(); DtpTo.Text = DateTime.Now.ToShortDateString(); }
        private void btnSearch_Click(object sender, EventArgs e){ tbl.Rows.Clear(); DgvSearch.DataSource = tbl; txtTotal.Text="0"; }
        private void btnDelete_Click(object sender, EventArgs e){ MessageBox.Show("الحذف غير متاح بعد إزالة SQL","تنبيه"); }
        private void btnDeleteAll_Click_1(object sender, EventArgs e){ MessageBox.Show("مسح التقارير غير متاح بعد إزالة SQL","تنبيه"); }
    }
}