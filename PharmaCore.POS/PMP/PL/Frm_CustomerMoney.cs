using DevExpress.Data.Utils;
using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
using PharmaCore.POS.RE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_CustomerMoney : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();


        public Frm_CustomerMoney()
        {
            InitializeComponent();
        }



        string stock_ID = "";
        private void Frm_CustomerMoney_Load(object sender, EventArgs e)
        {
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
            try
            {
                FillCustomer();
            }
            catch (Exception) { }

            DtpDate.Text = DateTime.Now.ToShortDateString();
            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل' ,[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] ", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();

        }

        private void FillCustomer()
        {

            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Order_ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbl.Clear();

            if (rbtnAllCust.Checked == true)
            {
                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل' ,[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] ", "");
            }
            else if (rbtnOneCustomer.Checked==true)
            {

                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل' ,[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] where Cus_Name=N'" + cbxCustomer.Text+"' ", "");

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

                        Frm_CustomerMoney_Load(null, null);
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
                            MessageBox.Show("قيمة المال المدفوعة أكثر من القيمة المسجلة على العميل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                        Frm_CustomerMoney_Load(null, null);
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
            db.exceuteData("delete from Customer_Money where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price =" + DgvSearch.CurrentRow.Cells[2].Value + "", "");
            db.exceuteData("update Customer_Report set state =N'سدد الآجل بالكامل' where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " ", "تم تسديد المبلغ بنجاح");

            db.exceuteData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[2].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'دفع آجل من عميل', N'') ", "");
            db.exceuteData("update stock set Money=Money + " + DgvSearch.CurrentRow.Cells[2].Value + " where Stock_ID=" + stock_ID + "", "");
        }
        private void PayPart(decimal money , string d )
        {
            db.exceuteData("update Customer_Money set Price=" + money + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price=" + DgvSearch.CurrentRow.Cells[2].Value + "", "");
            db.exceuteData("update Customer_Report set state =N'سدد مبلغ من الآجل' where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " ", "تم تسديد المبلغ بنجاح");

            db.exceuteData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'دفع آجل من عميل', N'') ", "");
            db.exceuteData("update stock set Money=Money + " + NudPrice.EditValue + " where Stock_ID=" + stock_ID + "", "");
        }

        private void PrintOneCustomer()
        {

            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptCustomerMoney1 rpt = new RptCustomerMoney1();
            

            string name = Convert.ToString(cbxCustomer.Text);

            DataTable tblRpt = new DataTable();
            rpt = new RptCustomerMoney1();
            tblRpt.Clear();
            
            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل',[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Customer_Money] ,[dbo].[OrderPrintData] and Cus_Name=N'"+name+"' ", "");
            
            
            String selectQuerys = "SELECT Logo FROM OrderPrintData";
            SqlCommand commandss;
            SqlDataAdapter da3s;
            commandss = new SqlCommand(selectQuerys, sqlconn);
            da3s = new SqlDataAdapter(commandss);
            DataTable table = new DataTable();
            da3s.Fill(table);
            byte[] img = (byte[])table.Rows[0][0];
            MemoryStream ms = new MemoryStream(img);
            rpt.Logo1.Image = Image.FromStream(ms);
            
            
            
            sqlconn.Close();
            
            
            rpt.DataSource = tblRpt;
            rpt.RequestParameters = false;
            rv.documentViewer1.DocumentSource = rpt;
            
            // للطباعه
            
            string PrinterName = Properties.Settings.Default.PrinterName1;
            rpt.Print(PrinterName);
            
            // للمعاينه بالبرنامج 
            
            //rv.ShowDialog();

            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbtnOneCustomer.Checked == true)
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    PrintOneCustomer();

                }
                else
                {
                    MessageBox.Show("يجب تحديد عميل أولًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void rbtnPayPart_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPayPart.Checked == true)
            {
                NudPrice.Enabled = true;
            }
        }

        private void rbtnPayAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPayAll.Checked == true)
            {
                NudPrice.Enabled = false;
            }
        }
    }

}