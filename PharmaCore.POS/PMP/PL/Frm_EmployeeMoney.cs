using DevExpress.XtraEditors;
using System;
using PharmaCore.POS.BL;
using PharmaCore.POS.RE;
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
    public partial class Frm_EmployeeMoney : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();


        public Frm_EmployeeMoney()
        {
            InitializeComponent();
        }

        private void Frm_EmployeeMoney_Load(object sender, EventArgs e)
        {
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
            try
            {
                FillEmployee();
            }
            catch (Exception) { }

            DtpDate.Text = DateTime.Now.ToShortDateString();
            tbl.Clear();
            tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Emp_Name] as 'الموظف' ,[Price] as 'المبلغ المستحق',[Date] as 'تاريخ الفاتورة' ,[Time] as 'وقت الفاتورة' ,[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Emp_Agel] ", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            }
        }

        string stock_ID = "";

        private void FillEmployee()
        {
            EmpName.DataSource = db.readData("select * from Employee", "");
            EmpName.DisplayMember = "Emp_Name";
            EmpName.ValueMember = "Emp_ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();

            if (rbtnAllCust.Checked == true)
            {
                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Emp_Name] as 'الموظف' ,[Price] as 'المبلغ المستحق',[Date] as 'تاريخ الفاتورة' ,[Time] as 'وقت الفاتورة' ,[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Emp_Agel] ", "");
            }
            else if (rbtnOneCustomer.Checked==true)
            {

                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Emp_Name] as 'الموظف' ,[Price] as 'المبلغ المستحق',[Date] as 'تاريخ الفاتورة' ,[Time] as 'وقت الفاتورة' ,[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Emp_Agel] where Emp_Name = N'" + EmpName.Text+"' ", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                string d = DtpDate.Value.ToString("dd/MM/yyyy");

                if (rbtnPayAll.Checked == true)
                {
                    if (MessageBox.Show("هل انت متأكد من تسديد المبلغ بالكامل ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        PayAll(d);

                        Frm_EmployeeMoney_Load(null, null);
                    }
                }
                else if (rbtnPayPart.Checked == true)
                {

                    if (MessageBox.Show("هل انت متأكد من تسديد المبلغ ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        decimal money = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[2].Value) - Convert.ToDecimal(NudPrice.EditValue);

                        if (money > 0)
                        {
                            PayPart(money, d);
                        }

                        else if (money == 0)
                        {
                            PayAll(d);
                        }

                        else if (money < 0)
                        {
                            MessageBox.Show("قيمة المال المدفوعة أكثر من القيمة المسجلة على الموظف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                        Frm_EmployeeMoney_Load(null, null);
                    }
                }
            }

            else
            {
                MessageBox.Show("لا يوجد فاتورة لسدادها", "تاكيد");
            }
        }


        private void PayAll(string d)
        {
            db.exceuteData("delete from Emp_Agel where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price =" + DgvSearch.CurrentRow.Cells[2].Value + "", "");
            db.exceuteData("update Sales set TotalNeed = TotalNeed + " + DgvSearch.CurrentRow.Cells[2].Value + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " ", "تم تسديد المبلغ بنجاح");

            db.exceuteData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[2].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'دفع آجل من موظف', N'') ", "");
            db.exceuteData("update stock set Money=Money + " + DgvSearch.CurrentRow.Cells[2].Value + " where Stock_ID=" + stock_ID + "", "");
        }
        private void PayPart(decimal money, string d)
        {
            db.exceuteData("update Emp_Agel set Price=" + money + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price=" + DgvSearch.CurrentRow.Cells[2].Value + "", "");
            db.exceuteData("update Sales set TotalNeed = TotalNeed + " + NudPrice.EditValue + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " ", "تم تسديد المبلغ بنجاح");

            db.exceuteData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'دفع آجل من موظف', N'') ", "");
            db.exceuteData("update stock set Money=Money + " + NudPrice.EditValue + " where Stock_ID=" + stock_ID + "", "");
        }
    }
}