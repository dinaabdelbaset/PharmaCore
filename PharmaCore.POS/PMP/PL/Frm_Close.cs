using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Close : DevExpress.XtraEditors.XtraForm
    {
        DataTable tbl = new DataTable();
        public Frm_Close(){ InitializeComponent(); }
        private void Frm_Close_Load(object sender, EventArgs e){ lblUsername.Text = Properties.Settings.Default.USERNAME; Clear(); foreach (var ctrl in this.Controls.OfType<DevExpress.XtraEditors.TextEdit>()){ if (ctrl.Name != "txtID") ctrl.EditValueChanged += total_ValueChanged; } SetupVisaAutoSum(); }
        private void SetupVisaAutoSum(){ for(int i=1;i<=14;i++){ var txt = this.Controls.Find($"textEdit{i}", true).FirstOrDefault() as DevExpress.XtraEditors.TextEdit; if(txt!=null) txt.EditValueChanged += (s,e)=> CalculateVisaSum(); } }
        private void CalculateVisaSum(){ decimal total=0; for(int i=1;i<=14;i++){ var txt=this.Controls.Find($"textEdit{i}",true).FirstOrDefault() as DevExpress.XtraEditors.TextEdit; if(txt!=null && decimal.TryParse(txt.Text,out var v)) total+=v; } Visa.Text = total.ToString("0.00"); }
        private void Clear(){ tbl.Rows.Clear(); txtID.Text = "1"; radio_print.Checked = true; foreach (Control ctrl in this.Controls){ if (ctrl is DevExpress.XtraEditors.TextEdit t) t.Text="0"; } one.Focus(); }
        private void total_ValueChanged(object sender, EventArgs e){ decimal.TryParse(one.Text,out var a1); decimal.TryParse(five.Text,out var a5); decimal.TryParse(ten.Text,out var a10); decimal.TryParse(twenty.Text,out var a20); decimal.TryParse(fifty.Text,out var a50); decimal.TryParse(one_hundred.Text,out var a100); decimal.TryParse(two_hundred.Text,out var a200); decimal.TryParse(Visa.Text,out var visa); total.Text = (a1 + a5*5 + a10*10 + a20*20 + a50*50 + a100*100 + a200*200 + visa).ToString(); }
        private string ConvertText(string input)=> decimal.TryParse(input,out var r)? r.ToString():"0";
        private void btnSave_Click(object sender, EventArgs e){ if(Properties.Settings.Default.Total_Open>=1){ Properties.Settings.Default.Total_Open=0; Properties.Settings.Default.Save(); MessageBox.Show("تم تقفيل الشيفت (محلي بدون قاعدة بيانات)","تأكيد",MessageBoxButtons.OK,MessageBoxIcon.Information); Clear(); } else { MessageBox.Show("لا يوجد شفت مفتوح لتقفيله","تأكيد",MessageBoxButtons.OK,MessageBoxIcon.Information);} }
        private void btnPtintOrder_Click(object sender, EventArgs e){ MessageBox.Show("الطباعة معطلة بعد إزالة SQL","تنبيه"); }
    }
}