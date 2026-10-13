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
    public partial class Frm_SalesRb7h : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl2 = new DataTable();
        DataTable tbl = new DataTable();


        public Frm_SalesRb7h()
        {
            InitializeComponent();
        }

        private void Frm_SalesRb7h_Load(object sender, EventArgs e)
        {
            fillUser();
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void fillUser()
        {
            cbxUser.DataSource = db.readData("select * from Users", "");
            cbxUser.DisplayMember = "User_Name";
            cbxUser.ValueMember = "User_ID";
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {

            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (checkOrderNumber.Checked == false)
            {
                if (rbtnAllUser.Checked == true)
                {

                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where User_Name =N'" + cbxUser.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC", "");
                }
            }
            else if (checkOrderNumber.Checked == true)
            {
                if (rbtnAllUser.Checked == true)
                {

                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Order_ID =" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where User_Name =N'" + cbxUser.Text + "' and Order_ID=" + txtOrderNumber.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC", "");

                }
            }

            DgvSearch.DataSource = tbl;

            if (this.DgvDteils.DataSource != null)
            {
                this.DgvDteils.DataSource = null;
            }
            else
            {
                this.DgvDteils.Rows.Clear();
            }


            decimal totalOrder = 0 ;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
            }
            txtTotal.Text = Math.Round(totalOrder, 2).ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من حذف الفاتورة بالكامل ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Buy where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");
                    db.exceuteData("delete from Buy_Detalis where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح");

                    btnSearch_Click(null, null);
                }
            }
        }

        private void btnPtintOrderAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                PrintAll();
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
            RE.RptSalesRb7h1 rpt = new RptSalesRb7h1();

            int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            DataTable tblRpt = new DataTable();
            rpt = new RptSalesRb7h1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل',Pro_Name as 'المنتج',[Unit] as 'الوحدة',[Sales_Detalis].[Qty] as 'الكمية',[Buy_Price] as 'سعر شراء الصيدلي',[Discount] as 'الخصم',[Sales_Detalis].[Tax_Value] as 'قيمة الضريبة',[Price_Tax] as 'سعر شراء الجمهور',([Price_Tax] -  [Buy_Price]) * [Sales_Detalis].[Qty] as 'الربح',[Total] as 'اجمالي الصنف' ,[TotalOrder] as 'اجمالي الفاتورة',[Madfou3] as 'المبلغ المدفوع',[Baky] as 'المتبقي',[Date]  as 'التاريخ',[User_Name] as 'اسم الكاشير',[Time] as 'الوقت' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Sales_Detalis] ,[dbo].[OrderPrintData] where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID ASC ", "");

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

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);


                tbl2.Clear();
                tbl2 = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Pro_Name as 'المنتج' ,Unit as 'الوحدة' ,[Buy_Price] as 'سعر الشراء' , [Sales_Detalis].[Tax_Value] as 'الضريبة' ,[Sales_Detalis].[Price_Tax] as 'السعر بعد الضريبة',[Sales_Detalis].[Qty] as 'الكمية' ,([Price_Tax] -  [Buy_Price]) * Sales_Detalis.[Qty] as 'الربح' ,[Sales_Detalis].[Price] as 'إجمالي السعر',[Discount] as 'الخصم'  ,[Total] as 'إجمالي المدفوع بعد الخصم' FROM [dbo].[Sales_Detalis] where Order_ID=" + id + "", "");
                DgvDteils.DataSource = tbl2;

            }
            else
            {
                MessageBox.Show("لا يوجد بيانات بالتفصيل لهذه العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (DgvDteils.Rows.Count >= 1)
            {
                decimal totalrb7 = 0;
                for (int i = 0; i <= DgvDteils.Rows.Count - 1; i++)
                {
                    totalrb7 += Convert.ToDecimal(DgvDteils.Rows[i].Cells[7].Value);
                }
                try
                {
                    txtTotalRb7h.Text = (Math.Round(totalrb7, 2)).ToString();
                }
                catch (Exception) { }
            }
        }
    }
}