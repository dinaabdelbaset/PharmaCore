using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Open : DevExpress.XtraEditors.XtraForm
    {
        // Legacy DB removed
        DataTable tbl = new DataTable();
        public Frm_Open(){ InitializeComponent(); }
        private void Frm_Open_Load(object sender, EventArgs e){ lblUsername.Text = Properties.Settings.Default.USERNAME; Clear(); }
        private void Clear(){ tbl.Columns.Clear(); tbl.Rows.Clear(); txtID.Text = "1"; one.EditValue = 0; five.EditValue = 0; ten.EditValue = 0; twenty.EditValue = 0; fifty.EditValue = 0; one_hundred.EditValue = 0; two_hundred.EditValue = 0; total.EditValue = 0; one.Focus(); }
        private void total_ValueChanged(object sender, EventArgs e){ decimal T_One = Convert.ToDecimal(one.EditValue); decimal T_Five = Convert.ToDecimal(five.EditValue)*5; decimal T_Ten = Convert.ToDecimal(ten.EditValue)*10; decimal T_Twenty = Convert.ToDecimal(twenty.EditValue)*20; decimal T_Fifty = Convert.ToDecimal(fifty.EditValue)*50; decimal T_One_hundred = Convert.ToDecimal(one_hundred.EditValue)*100; decimal T_Two_hundred = Convert.ToDecimal(two_hundred.EditValue)*200; total.EditValue = T_One+T_Five+T_Ten+T_Twenty+T_Fifty+T_One_hundred+T_Two_hundred; }
        private void btnSave_Click(object sender, EventArgs e){ if (Properties.Settings.Default.Total_Open >= 1){ MessageBox.Show("الشيفت مفتوح من قبل ، يرجى تقفيل الشفت أولًا", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } if (Convert.ToDecimal(total.EditValue) < 1){ MessageBox.Show("ادخل مبلغ صالح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); return; } Properties.Settings.Default.Total_Open = Convert.ToDecimal(total.EditValue); Properties.Settings.Default.Save(); MessageBox.Show("تم فتح الشفت (تخزين محلي فقط)", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information); Clear(); }
    }
}