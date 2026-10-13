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
    public partial class Frm_SupplierMoney : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();


        public Frm_SupplierMoney()
        {
            InitializeComponent();
        }


        string stock_ID = "";

        private void Frm_SupplierMoney_Load(object sender, EventArgs e)
        {
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
            try
            {
                FillSupplier();
            }
            catch (Exception) { }

            DtpDate.Text = DateTime.Now.ToShortDateString();
            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count -1; i++)
            {
                totalPrice +=Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            }
            txtTotal.Text =Math.Round(totalPrice, 2).ToString();
        }

        private void FillSupplier()
        {

            cbxSupplier.DataSource = db.readData("select * from Suppliers", "");
            cbxSupplier.DisplayMember = "Sup_Name";
            cbxSupplier.ValueMember = "Sup_ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbl.Clear();

            if (rbtnAllSup.Checked == true)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID", "");
            }
            else if (rbtnOneSupplier.Checked == true)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID and Suppliers.Sup_ID="+cbxSupplier.SelectedValue+" ", "");


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
                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ بالكامل للمورد ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (rbtnAllSup.Checked == true) { MessageBox.Show("من فضلك حدد اسم مورد", "تاكيد"); return; }
                        db.exceuteData("delete from Supplier_Money where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price =" + DgvSearch.CurrentRow.Cells[2].Value + "", "");
                        db.exceuteData("insert  into Supplier_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + DgvSearch.CurrentRow.Cells[2].Value + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "تم تسديد المبلغ بنجاح");
                        Frm_SupplierMoney_Load(null, null);

                    }
                }
                else if (rbtnPayPart.Checked == true)
                {

                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (rbtnAllSup.Checked == true) { MessageBox.Show("من فضلك حدد اسم مورد", "تاكيد"); return; }

                        decimal money = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[2].Value) - Convert.ToDecimal(NudPrice.EditValue);
                        db.exceuteData("update Supplier_Money set Price="+ money + " where Order_ID="+DgvSearch.CurrentRow.Cells[0].Value+" and Price="+ DgvSearch.CurrentRow.Cells[2].Value + "", "");
                        db.exceuteData("insert  into Supplier_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + NudPrice.EditValue + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "تم تسديد المبلغ بنجاح");
                        Frm_SupplierMoney_Load(null, null);

                    }
                }


                if (rbtnPayAll.Checked == true)
                {
                    db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[2].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات الى موردين', N'') ", "");
                    db.exceuteData("update stock set Money=Money - " + DgvSearch.CurrentRow.Cells[2].Value + " where Stock_ID=" + stock_ID + "", "");
                }
                else
                {
                    db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.EditValue + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات الى موردين', N'') ", "");
                    db.exceuteData("update stock set Money=Money - " + NudPrice.EditValue + " where Stock_ID=" + stock_ID + "", "");

                }
            }
        }

        private void rbtnOneSupplier_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnOneSupplier.Checked)
            {
                cbxSupplier.Enabled = true;
                btnAdd.Enabled = true;
                btnPrint.Enabled = true;
            }
            if (rbtnAllSup.Checked)
            {
                cbxSupplier.Enabled = false;
                btnAdd.Enabled = false;
                btnPrint.Enabled = false;

            }
        }

        private void rbtnPayPart_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnPayPart.Checked)
            {
                NudPrice.Enabled = true;
            }
            if (rbtnPayAll.Checked)
            {
                NudPrice.Enabled = false;
            }
        }


        private void PrintOneSupplier()
        {
            SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
            RE.rpt_viewer rv = new RE.rpt_viewer();
            RE.RptSupplierMonry1 rpt = new RptSupplierMonry1();

            int id = Convert.ToInt32(cbxSupplier.SelectedValue);

            DataTable tblRpt = new DataTable();
            rpt = new RptSupplierMonry1();
            tblRpt.Clear();

            tblRpt = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_ID as 'رقم المورد' ,Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' ,[OrderPrintData].Name ,[OrderPrintData].Address ,[OrderPrintData].Description ,[OrderPrintData].Phone1 ,[OrderPrintData].Phone2 FROM [dbo].[Supplier_Money] ,[dbo].[OrderPrintData] , Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID and Suppliers.Sup_ID = " + id + " ", "");


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


        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbtnOneSupplier.Checked == true)
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    PrintOneSupplier();

                }
            }
        }
    }
}