using DevExpress.XtraEditors;
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
    public partial class Frm_SanadSarf : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        DataTable tblSanad = new DataTable();


        public Frm_SanadSarf()
        {
            InitializeComponent();
        }

        private void Frm_SanadSarf_Load(object sender, EventArgs e)
        {
            AutoNumber();
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
        }


        private void AutoNumber()
        {
            tblSanad.Clear();
            tblSanad = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_sarf].[Name] as 'اسم المسؤل عن الصرف' ,[Price] as 'المبلغ'  ,[To_] as 'تم الصرف لــ ' ,[Reason] as 'السبب' ,[Date] as 'تاريخ العملية' FROM [dbo].[Sanad_sarf] ", "");
            DataCust.DataSource = tblSanad;


            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Sanad_Sarf", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            NudPrice.EditValue = 1;
            DtpDate.Text = DateTime.Now.ToShortDateString();
            txtReason.Clear();
            txtName.EditValue = 0;
            txtTo.EditValue = 0;
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;

            txtTotal.Text = (DataCust.Rows.Count).ToString();


        }

        string stock_ID = "";


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTo.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات");
                return;
            }
            string d = DtpDate.Value.ToString("dd/MM/yyyy");

            decimal stock_Money = 0;
            try
            {
                stock_Money =Convert.ToDecimal(db.readData("select * from Stock where Stock_ID="+stock_ID+"", "").Rows[0][1]);
            }
            catch (Exception) { }

            if (Convert.ToDecimal(NudPrice.EditValue) > stock_Money)
            {
                MessageBox.Show(" لايوجد رصيد كافي في الخزنة لاجراء العملية");
                return;
            }
            db.exceuteData("update stock set Money=Money - " + NudPrice.EditValue + " where Stock_ID=" + stock_ID + "", "");

            db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'سند صرف', N'" + txtReason.Text + "') ", "");

            db.exceuteData("insert into Sanad_Sarf Values (" + txtID.Text + " ,N'" + txtName.Text + "' ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + txtTo.Text + "' ,N'" + txtReason.Text + "')", "تم الادخال بنجاح");
            Print();
            AutoNumber();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSanadSarf1 rpt = new RptSanadSarf1();

            int id = Convert.ToInt32(DataCust.CurrentRow.Cells[0].Value);

            DataTable tblRpt = new DataTable();
            rpt = new RptSanadSarf1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_sarf].[Name] as 'اسم المسؤل عن الصرف' ,[Price] as 'المبلغ' ,[Date] as 'تاريخ العملية' ,[To_] as 'تم الصرف لــ ' ,[Reason] as 'السبب' ,[OrderPrintData].Name ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Sanad_sarf],[dbo].[OrderPrintData] where Order_ID = " + id + " ", "");


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

        private void Print()
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSanadSarf1 rpt = new RptSanadSarf1();

            int id = Convert.ToInt32(txtID.Text);

            DataTable tblRpt = new DataTable();
            rpt = new RptSanadSarf1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_sarf].[Name] as 'اسم المسؤل عن الصرف' ,[Price] as 'المبلغ' ,[Date] as 'تاريخ العملية' ,[To_] as 'تم الصرف لــ ' ,[Reason] as 'السبب' ,[OrderPrintData].Name ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Sanad_sarf],[dbo].[OrderPrintData] where Order_ID = " + id + " ", "");


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

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DataCust.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح السند المحدد ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Sanad_Sarf where Order_ID=" + id + "", "");
                        MessageBox.Show("تم المسح بنجاح");
                        AutoNumber();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("لا يوجد سند لحذفه");
            }
        }
    }
}