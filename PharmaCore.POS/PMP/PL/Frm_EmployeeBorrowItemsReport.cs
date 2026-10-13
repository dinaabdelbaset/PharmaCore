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
    public partial class Frm_EmployeeBorrowItemsReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_EmployeeBorrowItemsReport()
        {
            InitializeComponent();
        }

        private void Frm_EmployeeBorrowItemsReport_Load(object sender, EventArgs e)
        {
            FillEmployee();
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void FillEmployee()
        {
            cbxEmployee.DataSource = db.readData("select * from Employee", "");
            cbxEmployee.DisplayMember = "Emp_Name";
            cbxEmployee.ValueMember = "Emp_ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnAllEmp.Checked == true)
            {
                tbl = db.readData(" SELECT Order_ID as 'رقم الفاتورة' ,Emp_Name  as 'اسم الموظف' ,TotalOrder  as 'إجمالي الفاتورة' ,TotalNeed  as 'إجمالي المطلوب',[Us_Name] as 'المستحدم' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].Emp_Sales where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");
            }
            else
            {
                tbl = db.readData(" SELECT Order_ID as 'رقم الفاتورة' ,Emp_Name  as 'اسم الموظف' ,TotalOrder  as 'إجمالي الفاتورة' ,TotalNeed  as 'إجمالي المطلوب',[Us_Name] as 'المستحدم' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].Emp_Sales where Emp_Name like N'%"+ cbxEmployee.Text +"%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");

            }
            //like N'%"+cbxEmployee.Text +"%'

            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][4]);
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
                        db.readData("delete from Employee_BorrowItems where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
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
                if (MessageBox.Show("هل انت متاكد من مسح التقارير بين فترتي التاريخ المحددة؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Employee_BorrowItems where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح التقارير بنجاح");
                    btnSearch_Click(null, null);
                }
            }
        }
    }
}