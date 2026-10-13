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
using System.Data.SqlClient;
using System.IO;

namespace PharmaCore.POS.PL
{
    public partial class Frm_SheftReport : DevExpress.XtraEditors.XtraForm
    {


        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();


        public Frm_SheftReport()
        {
            InitializeComponent();
        }

        private void Frm_SheftReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
            fillUser();
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
            if (checkDate.Checked == false)
            {
                if (rbtnAllUser.Checked == true)
                {

                    tbl = db.readData(" SELECT Sheft_Close.[ID] as 'م' ,Sheft_Close.[Name] as 'اسم المستخدم' ,Sheft_Open.[Day] as ' تاريخ فتح الشيفت' ,Sheft_Open.[Time] as 'وقت الفتح' ,Sheft_Close.[Day] as 'تاريخ الغلق' ,Sheft_Close.[Time] as 'وقت الغلق' ,Sheft_Open.[total] as 'إجمالي مبلغ الفتح' ,Sheft_Close.[total] as 'إجمالي مبلغ الغلق' ,[Visa] as 'إجمالي مبيعات الفيزا' ,[Total_Rb7] as 'صافي البيع' FROM [dbo].[Sheft_Close] , [Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID ORDER BY Sheft_Close.ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    tbl = db.readData(" SELECT Sheft_Close.[ID] as 'م' ,Sheft_Close.[Name] as 'اسم المستخدم' ,Sheft_Open.[Day] as 'تاريخ الفتح' ,Sheft_Open.[Time] as 'وقت الفتح' ,Sheft_Close.[Day] as 'تاريخ الغلق' ,Sheft_Close.[Time] as 'وقت الغلق' ,Sheft_Open.[total] as 'إجمالي مبلغ الفتح' ,Sheft_Close.[total] as 'إجمالي مبلغ الغلق' ,[Visa] as 'إجمالي مبيعات الفيزا' ,[Total_Rb7] as 'صافي البيع' FROM [dbo].[Sheft_Close] , [Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID  and Us_Name=N'" + cbxUser.Text + "'  ORDER BY Sheft_Close.ID DESC", "");
                }
            }
            else if (checkDate.Checked == true)
            {
                if (rbtnAllUser.Checked == true)
                {

                    tbl = db.readData(" SELECT Sheft_Close.[ID] as 'م' ,Sheft_Close.[Name] as 'اسم المستخدم' ,Sheft_Open.[Day] as 'تاريخ الفتح' ,Sheft_Open.[Time] as 'وقت الفتح' ,Sheft_Close.[Day] as 'تاريخ الغلق' ,Sheft_Close.[Time] as 'وقت الغلق' ,Sheft_Open.[total] as 'إجمالي مبلغ الفتح' ,Sheft_Close.[total] as 'إجمالي مبلغ الغلق' ,[Visa] as 'إجمالي مبيعات الفيزا' ,[Total_Rb7] as 'صافي البيع' FROM [dbo].[Sheft_Close] , [Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID and Convert(date,Sheft_Open.[Day] ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Sheft_Close.ID DESC", "");
                }
                else if (rbtnOneUser.Checked == true)
                {
                    tbl = db.readData(" SELECT Sheft_Close.[ID] as 'م' ,Sheft_Close.[Name] as 'اسم المستخدم' ,Sheft_Open.[Day] as 'تاريخ الفتح' ,Sheft_Open.[Time] as 'وقت الفتح' ,Sheft_Close.[Day] as 'تاريخ الغلق' ,Sheft_Close.[Time] as 'وقت الغلق' ,Sheft_Open.[total] as 'إجمالي مبلغ الفتح' ,Sheft_Close.[total] as 'إجمالي مبلغ الغلق' ,[Visa] as 'إجمالي مبيعات الفيزا' ,[Total_Rb7] as 'صافي البيع' FROM [dbo].[Sheft_Close] , [Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID and Us_Name=N'" + cbxUser.Text + "' and Convert(date,Sheft_Open.[Day] ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Sheft_Close.ID DESC", "");

                }
            }
            DgvSearch.DataSource = tbl;


            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[9].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnOrderPrint_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();

            if (Properties.Settings.Default.Show_Discount == false)
            {
                RE.RptSheftClsoe rpt = new RptSheftClsoe();
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
                DataTable tblRpt = new DataTable();
                rpt = new RptSheftClsoe();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT [Sheft_Close].[ID] as 'مسلسل' ,[Sheft_Open].[Time] as 'وقت الفتح' ,[Sheft_Close].[Day] as 'تاريخ الغلق' ,[Sheft_Close].[Time] as 'وقت الغلق' ,[Sheft_Open].[Day] as 'تاريخ الفتح' ,[Sheft_Close].[Name] as 'الكاشير' ,[Sheft_Close].[one] as 'واحد جنيه' ,[Sheft_Close].[five] as 'حمسة جنيه' ,[Sheft_Close].[ten] as 'عشرة جنيه' ,[Sheft_Close].[twenty] as 'عشرين جنيه' ,[Sheft_Close].[fifty] as 'خمسين جنيه' ,[Sheft_Close].[one_hundred] as 'مئة جنيه' ,[Sheft_Close].[two_hundred] as 'مئتين جنيه' ,[Sheft_Open].[total] as 'توتل الفتح' ,[Sheft_Close].[total] as 'توتل الغلق' ,[Sheft_Close].[Visa] as 'توتل الفيزا' ,[Sheft_Close].[Total_Rb7] as 'توتل الربح' ,[OrderPrintData].Name FROM [dbo].[Sheft_Close],[dbo].[OrderPrintData],[Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID and Sheft_Close.ID =" + id + "", "");


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
                string PrinterName = Properties.Settings.Default.PrinterName1;
                rpt.Print(PrinterName);

                // للمعاينه بالبرنامج 

                //rv.ShowDialog();
            }
        }

        private void btnPrintDetile_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();

            if (Properties.Settings.Default.Show_Discount == false)
            {
                RE.RptSheftReport rpt = new RptSheftReport();
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
                DataTable tblRpt = new DataTable();
                rpt = new RptSheftReport();
                tblRpt.Clear();

                tblRpt = db.readData(" SELECT Sheft_Close.[ID] as 'م' ,Sheft_Close.[Name] as 'اسم المستخدم' ,Sheft_Open.[Day] as 'تاريخ الفتح' ,Sheft_Open.[Time] as 'وقت الفتح' ,Sheft_Close.[Day] as 'تاريخ الغلق' ,Sheft_Close.[Time] as 'وقت الغلق' ,Sheft_Open.[total] as 'إجمالي مبلغ الفتح' ,Sheft_Close.[total] as 'إجمالي مبلغ الغلق' ,[Visa] as 'إجمالي مبيعات الفيزا' ,[Total_Rb7] as 'صافي البيع'  ,[OrderPrintData].Name FROM [dbo].[Sheft_Close],[dbo].[OrderPrintData],[Sheft_Open] where Sheft_Close.ID = Sheft_Open.ID and Sheft_Close.ID =" + id + "", "");


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
                string PrinterName = Properties.Settings.Default.PrinterName1;
                rpt.Print(PrinterName);

                // للمعاينه بالبرنامج 

                //rv.ShowDialog();
            }
        }
    }
}