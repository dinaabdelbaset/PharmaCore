using DevExpress.XtraCharts.Native;
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
    public partial class Frm_CutSalesReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
        DataTable tbl2 = new DataTable();

        public Frm_CutSalesReport()
        {
            InitializeComponent();
        }

        private void Frm_CutSalesReport_Load(object sender, EventArgs e)
        {
            RadioName.Checked = true;
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
            FillCustomer();
        }
        public void FillCustomer()
        {

            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Cust_ID";
        }

        private void RadioCode_Click(object sender, EventArgs e)
        {
            if (RadioCode.Checked)
            {
                cbxCustomer.Text= "";
                db.close();
                db.open();
                DataTable table = new DataTable();
                string searchQuery = " SELECT [Cust_Name] FROM [dbo].[Customers] WHERE [Cust_ID] LIKE '%" + txtCode.Text + "%' ";
                SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
                adapter.Fill(table);

                cbxCustomer.DataSource = table;

                db.close();
            }
        }

        private void RadioName_Click(object sender, EventArgs e)
        {
            if (RadioName.Checked)
            {
                txtCode.EditValue = null;
                db.close();
                db.open();
                DataTable table = new DataTable();
                string searchQuery = " SELECT * FROM [dbo].[Customers] WHERE [Cust_Name] LIKE '%" + cbxCustomer.Text + "%' ";
                SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
                adapter.Fill(table);

                foreach (DataRow dr in table.Rows)
                {

                    txtCode.Text = dr["Cust_ID"].ToString();
                    //textCust_Money.Text = dr["Cust_Money"].ToString();

                }

                db.close();
            }
        }

        private void checkOrdNum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOrdNum.Checked == true)
            {
                txtOrderNum.Enabled = true;
            }
            else if (checkOrdNum.Checked == false)
            {
                txtOrderNum.Enabled = false;
            }
        }

        private void rbtnAllUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAllUser.Checked == true)
            {
                gpBoxEmp.Visible = false; 
            }
        }

        private void rbtnOneUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOneUser.Checked == true)
            {
                gpBoxEmp.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (checkOrdNum.Checked == false)
            {
                if (rbtnAllUser.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    if (RadioCode.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Customers.[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales , Customers where Customers.Cust_ID =" + txtCode.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioName.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Cust_Name=N'" + cbxCustomer.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    
                }
            }
            else if (checkOrdNum.Checked == true)
            {
                if (rbtnAllUser.Checked == true)
                {

                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Order_ID=" + txtOrderNum.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {

                    if (RadioCode.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Customers.[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales , Customers where Customers.Cust_ID =" + txtCode.Text + " and Sales.Order_ID=" + txtOrderNum.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioName.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Cust_Name=N'" + cbxCustomer.Text + "' and Order_ID=" + txtOrderNum.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                }
            }
            DgvSearch.DataSource = tbl;


            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value) ;

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                Frm_ShowDetils frm = new Frm_ShowDetils();

                tbl2.Clear();
                tbl2 = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Pro_Name as 'المنتج' ,Unit as 'الوحدة' , [Sales_Detalis].[Tax_Value] as 'الضريبة' ,[Sales_Detalis].[Price_Tax] as 'السعر بعد الضريبة',[Sales_Detalis].[Qty] as 'الكمية' ,[Sales_Detalis].[Price] as 'إجمالي السعر',[Discount] as 'الخصم'  ,[Total] as 'إجمالي المدفوع بعد الخصم' FROM [dbo].[Sales_Detalis] where Order_ID=" + id + "", "");
                frm.DgvDteils.DataSource = tbl2;

                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("لا يوجد بيانات بالتفصيل لهذه العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح الفاتورة المحددة ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Sales where Order_ID=" + id + "", "");
                        db.readData("delete from Sales_Detalis where Order_ID=" + id + "", "تم مسح الفاتورة بنجاح");
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

                if (MessageBox.Show("هل انت متاكد من مسح جميع الفواتير المعروضة على الشاشة ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Sales where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");
                    db.exceuteData("delete from Sales_Detalis where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم المسح بنجاح");

                    btnSearch_Click(null, null);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                Print();
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintAll();
            }
        }

        private void Print()
        {
            if (Properties.Settings.Default.SalePrintKind == "8CM")
            {
                SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
                RE.rpt_viewer rv = new RE.rpt_viewer();
                RE.RptOrderSales1 rpt = new RptOrderSales1();

                int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                DataTable tblRpt = new DataTable();
                rpt = new RptOrderSales1();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT Sales_Detalis.[Order_ID] as 'رقم الفاتورة' ,[Sales_Detalis].[Cust_Name] as 'اسم العميل' ,Pro_Name as 'المنتج' ,Sales_Detalis.[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريبة' ,[Discount] as 'الخصم' ,[Total] as 'الاجمالى' ,[TotalOrder] as 'اجمالى الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقى' ,[User_Name] as 'الكاشير' ,Sales_Detalis.[Date] as 'التاريخ',Sales_Detalis.Tax_Value as 'الضريبة' ,Sales_Detalis.TotalNeed as 'إجمالي المطلوب' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].Sales_Detalis,[dbo].[OrderPrintData] where Sales_Detalis.Order_ID=" + id + "", "");

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


                rpt.Parameters["ID"].Value = id;

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
            else if (Properties.Settings.Default.SalePrintKind == "A4")
            {

                SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
                RE.rpt_viewer rv = new RE.rpt_viewer();
                RE.RptOrderSalesA41 rpt = new RptOrderSalesA41();

                int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                DataTable tblRpt = new DataTable();
                rpt = new RptOrderSalesA41();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT Sales_Detalis.[Order_ID] as 'رقم الفاتورة' ,[Sales_Detalis].[Cust_Name] as 'اسم العميل' ,Pro_Name as 'المنتج' ,Sales_Detalis.[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريبة' ,[Discount] as 'الخصم' ,[Total] as 'الاجمالى' ,[TotalOrder] as 'اجمالى الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقى' ,[User_Name] as 'الكاشير' ,Sales_Detalis.[Date] as 'التاريخ',Sales_Detalis.Tax_Value as 'الضريبة' ,Sales_Detalis.TotalNeed as 'إجمالي المطلوب' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].Sales_Detalis,[dbo].[OrderPrintData] where Sales_Detalis.Order_ID=" + id + "", "");

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


                rpt.Parameters["ID"].Value = id;

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
        }

        private void PrintAll()
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSalesReport1 rpt = new RptSalesReport1();

            int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            DataTable tblRpt = new DataTable();
            rpt = new RptSalesReport1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT Sales_Detalis.[Order_ID] as 'رقم الفاتورة' ,[Sales_Detalis].[Cust_Name] as 'اسم العميل' ,Pro_Name as 'المنتج' ,Sales_Detalis.[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريبة' ,[Discount] as 'الخصم' ,[Total] as 'الاجمالى' ,[TotalOrder] as 'اجمالى الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقى' ,[User_Name] as 'الكاشير' ,Sales_Detalis.[Date] as 'التاريخ',Sales_Detalis.Tax_Value as 'الضريبة' ,Sales_Detalis.TotalNeed as 'إجمالي المطلوب' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].Sales_Detalis,[dbo].[OrderPrintData]  where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID ASC ", "");

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
    }
}