using DevExpress.XtraEditors;
using PharmaCore.POS.BL;
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
    public partial class Frm_CustomerReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_CustomerReport()
        {
            InitializeComponent();
        }

        private void Frm_CustomerReport_Load(object sender, EventArgs e)
        {
            FillCustomer();
            DtpDate.Text = DateTime.Now.ToShortDateString();


            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,[Cust_Name] as 'اسم العميل' FROM [dbo].[Customer_Report]", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[1].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();

        }

        private void FillCustomer()
        {

            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Cust_ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            if (rbtnAllCust.Checked == true)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,[Cust_Name] as 'اسم العميل' FROM [dbo].[Customer_Report]", "");
            }
            else
            {

                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة' ,[Price] as 'المبلغ المدفوع' ,[Date] as 'تاريخ الدفع'  ,[Cust_Name] as 'اسم العميل' FROM [dbo].[Customer_Report] where Cust_Name=N'"+cbxCustomer.Text+"'", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[1].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnOneCustomer.Checked == true)
                    {

                        db.exceuteData("delete from Customer_Report where Cust_Name=N'" + cbxCustomer.Text + "' and Order_ID=" +DgvSearch.CurrentRow.Cells[0].Value+ "  ", "تم مسح البيانات بنجاح");
                        Frm_CustomerReport_Load(null, null);
                    }
                    else { MessageBox.Show("من فضلك حدد عميل اولا", "تاكيد"); return; }
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnOneCustomer.Checked == true)
                    {

                        db.exceuteData("delete from Customer_Report where Cust_Name=N'" + cbxCustomer.Text+ "'", "تم مسح البيانات بنجاح");
                        Frm_CustomerReport_Load(null, null);
                    }
                    else { MessageBox.Show("من فضلك حدد عميل اولا", "تاكيد"); return; }
                }
            }
        }
    }
}