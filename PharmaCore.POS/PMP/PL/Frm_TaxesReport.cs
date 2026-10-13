using DevExpress.XtraEditors;
using PharmaCore.POS.RE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Frm_TaxesReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        DataTable tbl2 = new DataTable();



        public Frm_TaxesReport()
        {
            InitializeComponent();
        }

        private void Frm_TaxesReport_Load(object sender, EventArgs e)
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

            string sale = "فاتورة مبيعات", buy = "فاتورة مشتريات", returnSale = "مرتجعات مبيعات", returnBuy = "مرتجعات مشتريات";
            if (checkSale.Checked == true)
            {
                sale = "فاتورة مبيعات";
            }
            else
            {
                sale = "";
            }
            if (checkBuy.Checked == true)
            {
                buy = "فاتورة مشتريات";
            }
            else
            {
                buy = "";
            }
            if (checkSaleReturn.Checked == true)
            {
                returnSale = "مرتجعات مبيعات";
            }
            else
            {
                returnSale = "";
            }
            if (checkBuyReturn.Checked == true)
            {
                returnBuy = "مرتجعات مشتريات";
            }
            else
            {
                returnBuy = "";
            }

            tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Order_Num] as 'رقم الفاتورة',[Order_Type] as 'نوع العملية',[Tax_Type] as 'نوع الضريبة',[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',[Total_Order] as 'اجمالى الفاتورة قبل الضريبة',[Total_Tax] as 'اجمالى الضريبة',[Total_AfterTax] as 'اجمالى الفاتورة بعد الضريبة',[Date] as 'التاريخ' FROM [dbo].[Taxes_Report] where Order_Type in (N'"+sale+"' ,N'"+buy+"' ,N'"+returnSale+"' ,N'"+returnBuy+"') and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice1 = 0, totalPrice2 = 0 , totalPrice3 = 0, totalPrice4 = 0 , 
                totalTax1 = 0, totalTax2 = 0, totalTax3 = 0, totalTax4 = 0,
                totalAfterTax1 = 0 , totalAfterTax2 = 0 , totalAfterTax3 = 0 , totalAfterTax4 = 0  ;

            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                switch (DgvSearch.Rows[i].Cells[2].Value )
                {
                    case "فاتورة مشتريات":
                        totalPrice1 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);
                        totalAfterTax1 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);

                        break;

                    case "فاتورة مبيعات":
                        totalPrice2 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);
                        totalAfterTax2 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);


                        break;

                    case "مرتجعات مشتريات":
                        totalPrice3 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);
                        totalAfterTax3 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);


                        break;

                    //default:
                    case "مرتجعات مبيعات":
                        totalPrice4 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);
                        totalAfterTax4 += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);


                        break;
                }
            }
            totalTax1 = totalAfterTax1 - totalPrice1 ;
            totalTax2 = totalAfterTax2 - totalPrice2 ;
            totalTax3 = totalAfterTax3 - totalPrice3 ;
            totalTax4 = totalAfterTax4 - totalPrice4 ;

            txtPrice1.Text = Math.Round(totalPrice1, 2).ToString();            txtPrice2.Text = Math.Round(totalPrice2, 2).ToString();
            txtAfterTax1.Text = Math.Round(totalAfterTax1, 2).ToString();      txtAfterTax2.Text = Math.Round(totalAfterTax2, 2).ToString();
            txtTax1.Text = Math.Round(totalTax1, 2).ToString();                txtTax2.Text = Math.Round(totalTax2, 2).ToString();

            txtPrice3.Text = Math.Round(totalPrice3, 2).ToString();             txtPrice4.Text = Math.Round(totalPrice4, 2).ToString();
            txtAfterTax3.Text = Math.Round(totalAfterTax3, 2).ToString();       txtAfterTax4.Text = Math.Round(totalAfterTax4, 2).ToString();
            txtTax3.Text = Math.Round(totalTax3, 2).ToString();                 txtTax4.Text = Math.Round(totalTax4, 2).ToString();


            if ((totalTax2 + totalTax3) - (totalTax1 + totalTax4) < 0)
            {
                labInfo.Text = "المبلغ المستحق لك";
                txtSafy.Text = ((totalTax2 + totalTax3) - (totalTax1 + totalTax4)).ToString();
            }

            else if ((totalTax2 + totalTax3) -  (totalTax1 + totalTax4) == 0)
            {
                labInfo.Text = "لا يوجد اي مبالغ مستحقة";
                txtSafy.Text = "0";
            }

            else if((totalTax2 + totalTax3) -  (totalTax1 + totalTax4) > 0)
            {
                labInfo.Text = "المبلغ المستحق عليك";
                txtSafy.Text = ((totalTax2 + totalTax3) - (totalTax1 + totalTax4)).ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Taxes_Report where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح");

                    btnSearch_Click(null, null);
                }
            }
        }


        private void PrintAll()
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptTaxesReport1 rpt = new RptTaxesReport1();

            DataTable tblRpt = new DataTable();
            rpt = new RptTaxesReport1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم العملية',[Order_Num] as 'رقم الفاتورة',[Order_Type] as 'نوع العملية',[Tax_Type] as 'نوع الضريبة',[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',[Total_Order] as 'اجمالى الفاتورة قبل الضريبة',[Total_Tax] as 'اجمالى الضريبة',[Total_AfterTax] as 'اجمالى الفاتورة بعد الضريبة',[Date] as 'التاريخ' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Taxes_Report],[dbo].[OrderPrintData] where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            rpt.Parameters["From"].Value = date1;
            rpt.Parameters["To"].Value = date2;

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

            string PrinterName = Properties.Settings.Default.PrinterName3;
            rpt.Print(PrinterName);

            // للمعاينه بالبرنامج 

            //rv.ShowDialog();
        }

        private void PrintSummary()
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");


            db.exceuteData("insert into Tax_Sammury values ('" + date1 + "' , '" + date2 + "' , '" + txtTax1.Text + "'  , '" + txtTax2.Text + "' , '" + txtTax3.Text + "'  , '" + txtTax4.Text + "'  , '" + txtPrice1.Text + "' , '" + txtPrice2.Text + "', '" + txtPrice3.Text + "'  , '" + txtPrice4.Text + "' , '" + txtAfterTax1.Text + "', '" + txtAfterTax2.Text + "'  , '" + txtAfterTax3.Text + "' , '" + txtAfterTax4.Text + "' , N'" + labInfo.Text + "', N'" + txtSafy.Text + "')", "");


            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();

            RE.RptTaxesReportsammury1 rpt = new RptTaxesReportsammury1();
            
            //tbl2.Clear();
            //tbl2 = db.readData("select max (Order_ID) from Tax_Sammury", "");
            int id = Convert.ToInt32(db.readData("select max (Order_ID) from Tax_Sammury", "").Rows[0][0]);
            
            DataTable tblRpt = new DataTable();
            rpt = new RptTaxesReportsammury1();
            tblRpt.Clear();
            
            tblRpt = db.readData(" SELECT [date_from] ,[date_to] ,[Tax1] ,[Tax2] ,[Tax3] ,[Tax4] ,[Price1] ,[Price2] ,[Price3] ,[Price4] ,[AfterTax1] ,[AfterTax2] ,[AfterTax3] ,[AfterTax4] ,[Info] ,[Safy] ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Tax_Sammury],[dbo].[OrderPrintData] where Tax_Sammury.Order_ID=" + id + "", "");
            
            
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
            
            // للطباعه مباشرة
            string PrinterName = Properties.Settings.Default.PrinterName3;
            rpt.Print(PrinterName);
            
            // للمعاينه بالبرنامج 
            
            //rv.ShowDialog();
            
        }

            private void btnPtintReport_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintAll();
            }
        }

        private void btnPrintSummary_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintSummary();
            }
        }
    }
}