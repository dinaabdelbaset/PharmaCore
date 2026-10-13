using DevExpress.XtraEditors;
using System;
using PharmaCore.POS.RE;
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
    public partial class Frm_CutTameenReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
        DataTable tbl2 = new DataTable();


        public Frm_CutTameenReport()
        {
            InitializeComponent();
        }

        private void Frm_CutTameenReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
            FillCompany();
        }

        public void FillCompany()
        {

            cbxCompany.DataSource = db.readData("select * from Company", "");
            cbxCompany.DisplayMember = "Cop_Name";
            cbxCompany.ValueMember = "Cop_ID";

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
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    if (RadioCode.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Tam_Cod like N'%" + txtTotalNumb.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioName.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Cust_Name like N'%" + txtName.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    if (RadioPhone.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Cust_Phone like N'%" + txtPhone.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioCompany.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Tam_Company like N'%" + cbxCompany.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                }
            }
            else if (checkOrdNum.Checked == true)
            {
                if (rbtnAllUser.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Order_ID=" + txtOrderNum.Text + "  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    if (RadioCode.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Order_ID=" + txtOrderNum.Text + "  and Tam_Cod like N'%" + txtTotalNumb.Text + "%'  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioName.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Order_ID=" + txtOrderNum.Text + "  and Cust_Name like N'%" + txtName.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    if (RadioPhone.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Order_ID=" + txtOrderNum.Text + "  and Cust_Phone like N'%" + txtPhone.Text + "%'  and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }
                    else if (RadioCompany.Checked == true)
                    {
                        tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,[Total] as 'إجمالي الفاتورة' ,[Tam_Percenc] as 'نسبة التحمل' ,[TotalNeed] as 'إجمالي المطلوب' ,[Cust_Phone] as 'تليفون العميل' ,[Tam_Cod] as 'رقم التأمين' ,[Tam_Company] as 'شركة التأمين' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,[Us_Name] as 'الكاشير' FROM [dbo].Sales_Tameen where Order_ID=" + txtOrderNum.Text + "  and Tam_Company like N'%" + cbxCompany.Text + "%' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID DESC", "");
                    }

                }
            }

            DgvSearch.DataSource = tbl;

            txtTotalNumb.Text = (DgvSearch.Rows.Count).ToString();

            decimal totalPrice = 0 , totalget = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);
                totalget += Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalGet.Text = Math.Round(totalget, 2).ToString();
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

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                Frm_ShowDetils frm = new Frm_ShowDetils();

                tbl2.Clear();
                tbl2 = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,Pro_Name as 'المنتج' ,[Unit] as 'الوحدة' ,[Price] as 'سعر قبل الضريبة' ,Sales_Det_Tameen.[Tax_Value] as 'الضريبة',[Price_Tax] as 'سعر شامل الضريبة' , Sales_Det_Tameen.[Qty] as 'الكمية' ,[TotalBefor] as 'إجمالي الصنف' ,[Discount] as 'نسبة التحمل' ,[TotalAfter] as 'إجمالي تحمل العميل' FROM [dbo].[Sales_Det_Tameen] where Order_ID=" + id + "", "");
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
                        db.readData("delete from Sales_Det_Tameen where Order_ID=" + id + "", "");
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
                MessageBox.Show(" حدد على فاتورة أولًا لحذفها ");
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
                    db.exceuteData("delete from Sales_Det_Tameen where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

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
            if (Properties.Settings.Default.TameenPrintKind == "8CM")
            {

                SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
                RE.rpt_viewer rv = new RE.rpt_viewer();
                RE.RptSalesTameen1 rpt = new RptSalesTameen1();
                int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
                DataTable tblRpt = new DataTable();
                rpt = new RptSalesTameen1();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,Pro_Name as 'المنتج' ,[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريب' ,[TotalBefor] as 'إجمالي الصنف' ,[Discount] as 'نسبة التحمل' ,[TotalAfter] as 'إجمالي تحمل العميل',[TotalOrder] as 'اجمالي الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقي' ,[TotalNeed] as 'المبلغ المطلوب' ,[User_Name] as 'الكاشير' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,Tax_Value as 'الضريبة'  ,Cust_Phone as 'الموبايل' ,Tam_Cod as 'رقم التأمين' ,Tam_Company as 'الشركة' ,Tam_Percenc as 'نسبة التحمل'  ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2  FROM [dbo].[Sales_Det_Tameen] ,[dbo].[OrderPrintData] where Order_ID=" + id + "", "");


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
            else if (Properties.Settings.Default.TameenPrintKind == "A4")
            {
                SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
                RE.rpt_viewer rv = new RE.rpt_viewer();
                RE.RptSalesTameenA41 rpt = new RptSalesTameenA41();
                int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
                DataTable tblRpt = new DataTable();
                rpt = new RptSalesTameenA41();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة' ,[Cust_Name] as 'اسم العميل' ,Pro_Name as 'المنتج' ,[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريب' ,[TotalBefor] as 'إجمالي الصنف' ,[Discount] as 'نسبة التحمل' ,[TotalAfter] as 'إجمالي تحمل العميل',[TotalOrder] as 'اجمالي الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقي' ,[TotalNeed] as 'المبلغ المطلوب' ,[User_Name] as 'الكاشير' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' ,Tax_Value as 'الضريبة'  ,Cust_Phone as 'الموبايل' ,Tam_Cod as 'رقم التأمين' ,Tam_Company as 'الشركة' ,Tam_Percenc as 'نسبة التحمل'  ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2  FROM [dbo].[Sales_Det_Tameen] ,[dbo].[OrderPrintData] where Order_ID=" + id + "", "");


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

            tblRpt = db.readData(" SELECT Sales_Detalis.[Order_ID] as 'رقم الفاتورة' ,[Sales_Detalis].[Cust_Name] as 'اسم العميل' ,Products.Pro_Name as 'المنتج' ,Sales_Detalis.[Qty] as 'الكمية' ,Unit as 'الوحدة' ,[Price_Tax] as 'السعر شامل الضريبة' ,[Discount] as 'الخصم' ,[Total] as 'الاجمالى' ,[TotalOrder] as 'اجمالى الفاتورة' ,[Madfou3] as 'المبلغ المدفوع' ,[Baky] as 'المتبقى' ,[User_Name] as 'الكاشير' ,Sales_Detalis.[Date] as 'التاريخ',Sales_Detalis.Tax_Value as 'الضريبة' ,Sales_Detalis.TotalNeed as 'إجمالي المطلوب' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].Sales_Detalis,[dbo].[OrderPrintData] , Products where Products.Pro_ID = Sales_Detalis.Pro_ID and Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID ASC ", "");

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