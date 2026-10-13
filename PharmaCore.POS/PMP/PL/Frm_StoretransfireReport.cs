using DevExpress.XtraEditors;
using System;
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
    public partial class Frm_StoretransfireReport : DevExpress.XtraEditors.XtraForm
    {


        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_StoretransfireReport()
        {
            InitializeComponent();
        }

        private void Frm_StoretransfireReport_Load(object sender, EventArgs e)
        {
            FillStore();

            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }


        private void FillStore()
        {

            cbxStoreFrom.DataSource = db.readData("select * from Store", "");
            cbxStoreFrom.DisplayMember = "Store_Name";
            cbxStoreFrom.ValueMember = "Store_ID";

            cbxStoreTo.DataSource = db.readData("select * from Store", "");
            cbxStoreTo.DisplayMember = "Store_Name";
            cbxStoreTo.ValueMember = "Store_ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            if (rbtnAllStoreFrom.Checked == true)
            {
                if (rbtnAllStoreTo.Checked==true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreTo.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_To=N'"+cbxStoreTo.Text+ "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }
            else if (rbtnAllStoreTo.Checked == true)
            {
                if (rbtnAllStoreFrom.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreFrom.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_From=N'" + cbxStoreFrom.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }
            else if (rbtnAllStoreFrom.Checked == true)
            {
                if (rbtnAllStoreTo.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_From=N'" + cbxStoreFrom.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreTo.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_To=N'" + cbxStoreTo.Text + "' and Store_From=N'"+cbxStoreFrom.Text+ "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }
            else if (rbtnOneStoreTo.Checked == true)
            {
                if (rbtnAllStoreFrom.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_To=N'" + cbxStoreTo.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                else if (rbtnOneStoreFrom.Checked == true)
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Pro_Name] as 'اسم المنتج',[Store_From] as 'المخزن المحول منه' ,[Store_To] as 'المخزن المحول له',[Qty] as 'الكمية المحولة',[Unit] as 'الوحدة',[Buy_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Date] as 'التاريخ' ,[Name] as 'اسم المسؤل',[Reason] as 'السبب' FROM [dbo].[Products_Transfire] where Store_From=N'" + cbxStoreFrom.Text + "' and Store_To=N'" + cbxStoreTo.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }


            DgvSearch.DataSource = tbl;

            if (tbl.Rows.Count >= 1)
            {
                decimal totalQty = 0;
                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    totalQty += Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);

                }
                txtTotalQty.Text = Math.Round(totalQty, 2).ToString();
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
                    var rs = MessageBox.Show("هل أنت متأكد من مسح التقرير المحدد ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Products_Transfire where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
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
                if (MessageBox.Show("هل انت متاكد من مسح جميع التقارير  بين فترتي التاريخ المحددة؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Products_Transfire where  Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح التقارير بنجاح");

                    btnSearch_Click(null, null);
                }
            }
        }
    }
}