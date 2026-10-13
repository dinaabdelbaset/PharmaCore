using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_PointReport : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);

        public Frm_PointReport()
        {
            InitializeComponent();
        }

        private void Frm_PointReport_Load(object sender, EventArgs e)
        {
            FillCustomer();
            DtpDate.Text = DateTime.Now.ToShortDateString();


            tbl.Clear();
            tbl = db.readData("SELECT [Order_ID] as 'م ' , [Pnt_ID] as 'كود العميل' ,[Pnt_Name] as 'اسم العميل' ,[Pnt_User] as 'المستخدم' ,[Pnt_Date] as 'تاريخ التحويل' ,[Pnt_WasPoint] as 'النقاط سابقًا' ,[Pnt_changePint] as 'النقاط المحوله' ,[Pnt_Money] as 'المبلغ المكافئ' ,[Pnt_TotalMoney] as 'إجمالي المبلغ' FROM [dbo].[Point_Report]", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void FillCustomer()
        {

            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Cust_ID";
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
                if (rbtnAllCust.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'م ' , [Pnt_ID] as 'كود العميل' ,[Pnt_Name] as 'اسم العميل' ,[Pnt_User] as 'المستخدم' ,[Pnt_Date] as 'تاريخ التحويل' ,[Pnt_WasPoint] as 'النقاط سابقًا' ,[Pnt_changePint] as 'النقاط المحوله' ,[Pnt_Money] as 'المبلغ المكافئ' ,[Pnt_TotalMoney] as 'إجمالي المبلغ' FROM [dbo].[Point_Report] ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneCustomer.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'م ' , [Pnt_ID] as 'كود العميل' ,[Pnt_Name] as 'اسم العميل' ,[Pnt_User] as 'المستخدم' ,[Pnt_Date] as 'تاريخ التحويل' ,[Pnt_WasPoint] as 'النقاط سابقًا' ,[Pnt_changePint] as 'النقاط المحوله' ,[Pnt_Money] as 'المبلغ المكافئ' ,[Pnt_TotalMoney] as 'إجمالي المبلغ' FROM [dbo].[Point_Report] where Pnt_ID=N'" + txtCode.Text + "' ORDER BY Order_ID ASC", "");
                }
            }
            else if (checkDate.Checked == true)
            {
                if (rbtnAllCust.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'م ' , [Pnt_ID] as 'كود العميل' ,[Pnt_Name] as 'اسم العميل' ,[Pnt_User] as 'المستخدم' ,[Pnt_Date] as 'تاريخ التحويل' ,[Pnt_WasPoint] as 'النقاط سابقًا' ,[Pnt_changePint] as 'النقاط المحوله' ,[Pnt_Money] as 'المبلغ المكافئ' ,[Pnt_TotalMoney] as 'إجمالي المبلغ' FROM [dbo].[Point_Report] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");
                }
                else if (rbtnOneCustomer.Checked == true)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'م ' , [Pnt_ID] as 'كود العميل' ,[Pnt_Name] as 'اسم العميل' ,[Pnt_User] as 'المستخدم' ,[Pnt_Date] as 'تاريخ التحويل' ,[Pnt_WasPoint] as 'النقاط سابقًا' ,[Pnt_changePint] as 'النقاط المحوله' ,[Pnt_Money] as 'المبلغ المكافئ' ,[Pnt_TotalMoney] as 'إجمالي المبلغ' FROM [dbo].[Point_Report] where Pnt_ID=N'" + txtCode.Text + "' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'   ORDER BY Order_ID ASC", "");

                }
            }

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من مسح تقرير تحويل العميل ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnOneCustomer.Checked == true)
                    {

                        db.exceuteData("delete from Point_Report where Pnt_Name =N'" + cbxCustomer.Text + "' and Pnt_ID=N'" + txtCode.Text + "' and Order_ID=" +DgvSearch.CurrentRow.Cells[0].Value+ "  ", "تم مسح عمليه التحويل بنجاح");
                        Frm_PointReport_Load(null, null);
                    }
                    else { MessageBox.Show("من فضلك حدد عميل اولا", "تاكيد"); return; }
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متأكد من مسح جميع تقارير تحويلات العميل ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnOneCustomer.Checked == true)
                    {

                        db.exceuteData("delete from Point_Report where Pnt_Name =N'" + cbxCustomer.Text+ "' and Pnt_ID=N'" + txtCode.Text + "' ", "تم مسح البيانات بنجاح");
                        Frm_PointReport_Load(null, null);
                    }
                    else { MessageBox.Show("من فضلك حدد عميل اولا", "تاكيد"); return; }
                }
            }
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDate.Checked == true)
            {
                DtpFrom.Enabled = true;
                DtpTo.Enabled = true;

            }
            else if (checkDate.Checked == false)
            {
                DtpFrom.Enabled = false;
                DtpTo.Enabled = false;
            }
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

                }

                db.close();
            }
        }

        private void rbtnAllCust_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAllCust.Checked == true)
            {
                gpBoxEmp.Visible = false;
            }
        }

        private void rbtnOneCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnOneCustomer.Checked == true)
            {
                gpBoxEmp.Visible = true;
            }
        }
    }
}