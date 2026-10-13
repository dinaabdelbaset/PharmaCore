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
    public partial class Frm_EmployeeBorrowMoneyReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();

        public Frm_EmployeeBorrowMoneyReport()
        {
            InitializeComponent();
        }

        private void Frm_EmployeeBorrowMoneyReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
            txtName.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnAll.Checked == true)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية' ,[Borrow_From] as 'الدائن',[Borrow_To] as 'المدين-سلف لـ',[Order_Date] as 'تاريخ السلف',[Date_Reminder] as 'تاريخ الاستحقاق',[Price] as 'المبلغ',[Notes] as 'ملاحظات' FROM [dbo].[Employee_BorrowMoney] where  Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");
            }
            else
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية' ,[Borrow_From] as 'الدائن',[Borrow_To] as 'المدين-سلف لـ',[Order_Date] as 'تاريخ السلف',[Date_Reminder] as 'تاريخ الاستحقاق',[Price] as 'المبلغ',[Notes] as 'ملاحظات' FROM [dbo].[Employee_BorrowMoney] where [Borrow_To]=N'"+txtName.Text+"' and Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");
            }
            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][5]);
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
                        db.readData("delete from Employee_BorrowMoney where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
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
                if (MessageBox.Show("هل انت متاكد من مسح جميع التقارير  بين فترتي التاريخ المحددة؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Employee_BorrowMoney where Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح التقارير بنجاح");
                    btnSearch_Click(null, null);
                }
            }
        }

        private void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAll.Checked == true) 
            {
                txtName.Enabled = false;
            }
        }

        private void rbtnOne_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOne.Checked == true)
            {
                txtName.Enabled = true;
            }
        }
    }
}