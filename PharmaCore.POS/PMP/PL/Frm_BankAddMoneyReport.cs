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
    public partial class Frm_BankAddMoneyReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_BankAddMoneyReport()
        {
            InitializeComponent();
        }

        private void Frm_BankAddMoneyReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Money] as 'المبلغ',[Date] as 'التاريخ',[Name] as 'اسم الشخص المسؤل',[Type] as 'نوع الايداع' ,[Reason] as 'السبب' FROM [Bank_Insert] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][1]);
                }

                txtTotal.Text = Math.Round(Sum, 2).ToString();
            }
            else
            { txtTotal.Text = "0"; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح التقرير المحدد ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Bank_Insert where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
                        btnSearch_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(" حدد على تقرير أولًا لحذفه ");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                string date1;
                string date2;
                date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
                date2 = DtpTo.Value.ToString("yyyy-MM-dd");
                if (MessageBox.Show("هل انت متاكد من مسح  جميع التقارير  بين فترتي التاريخ المحددة؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Bank_Insert where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح التقارير بنجاح");
                    btnSearch_Click(null, null);
                }
            }
        }
    }
}