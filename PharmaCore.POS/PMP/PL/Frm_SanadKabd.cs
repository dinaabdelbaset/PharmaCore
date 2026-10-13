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
    public partial class Frm_SanadKabd : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        DataTable tblSanad = new DataTable();

        public Frm_SanadKabd()
        {
            InitializeComponent();
        }

        private void Frm_SanadKabd_Load(object sender, EventArgs e)
        {
            AutoNumber();
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
        }

        private void AutoNumber()
        {
            tblSanad.Clear();
            tblSanad = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_Kabd].[Name] as 'اسم المسؤل عن القبض' ,[Price] as 'المبلغ' ,[From_] as 'تم القبض من ' ,[Reason] as 'السبب' ,[Date] as 'تاريخ العملية' FROM [dbo].[Sanad_Kabd] ", "");
            DataCust.DataSource = tblSanad;

            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Sanad_Kabd", "");

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
            txtName.EditValue = null;
            txtFrom.EditValue = null;
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;

            txtTotal.Text = (DataCust.Rows.Count).ToString();
        }

        string stock_ID = "";

        private void Print()
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSanadKabd1 rpt = new RptSanadKabd1();

            int id = Convert.ToInt32(txtID.Text);

            DataTable tblRpt = new DataTable();
            rpt = new RptSanadKabd1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_Kabd].[Name] as 'اسم المسؤل عن القبض' ,[Price] as 'المبلغ' ,[Date] as 'تاريخ العملية' ,[From_] as 'تم القبض من ' ,[Reason] as 'السبب' ,[OrderPrintData].Name ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Sanad_Kabd] ,[dbo].[OrderPrintData] where Order_ID = " + id + " ", "");


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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtName.Text =="")
            {
                MessageBox.Show("من فضلك اكمل البيانات");
                return;
            }
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            db.exceuteData("update stock set Money=Money + "+NudPrice.EditValue + " where Stock_ID="+stock_ID+"", "");
            db.exceuteData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'سند قبض', N'" + txtReason.Text + "') ", "");

            db.exceuteData("insert into Sanad_Kabd Values (" + txtID.Text + " ,N'"+txtName.Text+"' ,"+NudPrice.EditValue + " ,N'"+d+"' ,N'"+txtFrom.Text+"' ,N'"+txtReason.Text+"')", "تم الادخال بنجاح");
            Print();
            AutoNumber();
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
                        db.readData("delete from Sanad_Kabd where Order_ID=" + id + "", "");
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSanadKabd1 rpt = new RptSanadKabd1();

            int id = Convert.ToInt32(DataCust.CurrentRow.Cells[0].Value);

            DataTable tblRpt = new DataTable();
            rpt = new RptSanadKabd1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Sanad_Kabd].[Name] as 'اسم المسؤل عن القبض' ,[Price] as 'المبلغ' ,[Date] as 'تاريخ العملية' ,[From_] as 'تم القبض من ' ,[Reason] as 'السبب' ,[OrderPrintData].Name ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Sanad_Kabd] ,[dbo].[OrderPrintData] where Order_ID = " + id + " ", "");


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
    }
}