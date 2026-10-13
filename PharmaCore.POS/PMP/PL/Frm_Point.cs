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
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using DevExpress.CodeParser;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Point : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        DataTable tblGroup = new DataTable();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);


        public Frm_Point()
        {
            InitializeComponent();
        }

        private void Frm_Point_Load(object sender, EventArgs e)
        {
            AutoNumber();
            lblUsername.Text =  Properties.Settings.Default.USERNAME;
        }

        private void AutoNumber()
        {
            tblGroup.Clear();
            tblGroup = db.readData(" SELECT [Cust_ID] as 'كود' ,[Cust_Name] as 'الإسم' ,[Cust_Address] as 'العنوان' ,[Cust_Phone] as 'الموبايل' ,[Notes] as 'ملاحظات' ,[Cust_First] as 'أول شراء' ,[Cust_Last] as 'آخر شراء' ,[Cust_TotalPoint] as 'إجمالي مشتريات العميل' ,[Cust_PointLeft] as 'نقاط غير مستخدمة' ,[Cust_Money] as 'المبلغ المستحق' FROM [dbo].[Customers] ", "");
            DataCust.DataSource = tblGroup;


            tbl.Clear();
            tbl = db.readData("select max (Order_ID) from Point_Report", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }


            NudPoint.EditValue = Properties.Settings.Default.Nud_Point;
            NudChange.EditValue = Properties.Settings.Default.Nud_Change;

            DtpDate.Text = DateTime.Now.ToShortDateString();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.TextEdit txt)
                {
                    txt.EditValue = null;   // أو txt.Text = string.Empty;
                }
            }
        }

        private void Main_name_TextChanged(object sender, EventArgs e)
        {
            db.close();
            db.open();
            DataTable table = new DataTable();
            string searchQuery = " SELECT [Cust_ID] as 'كود' ,[Cust_Name] as 'الإسم' ,[Cust_Address] as 'العنوان' ,[Cust_Phone] as 'الموبايل' ,[Notes] as 'ملاحظات' ,[Cust_First] as 'أول شراء' ,[Cust_Last] as 'آخر شراء' ,[Cust_TotalPoint] as 'إجمالي مشتريات العميل' ,[Cust_PointLeft] as 'نقاط غير مستخدمة' ,[Cust_Money] as 'المبلغ المستحق' FROM [dbo].[Customers] WHERE [Cust_ID] LIKE '%" + Main_name.Text + "%' ";
            SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
            adapter.Fill(table);

            DataCust.DataSource = table;

            db.close();
        }

        private void Main_person_TextChanged(object sender, EventArgs e)
        {
            db.close();
            db.open();
            DataTable table = new DataTable();
            string searchQuery = " SELECT [Cust_ID] as 'كود' ,[Cust_Name] as 'الإسم' ,[Cust_Address] as 'العنوان' ,[Cust_Phone] as 'الموبايل' ,[Notes] as 'ملاحظات' ,[Cust_First] as 'أول شراء' ,[Cust_Last] as 'آخر شراء' ,[Cust_TotalPoint] as 'إجمالي مشتريات العميل' ,[Cust_PointLeft] as 'نقاط غير مستخدمة' ,[Cust_Money] as 'المبلغ المستحق' FROM [dbo].[Customers] WHERE [Cust_Name] LIKE '%" + Main_person.Text + "%'";
            SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
            adapter.Fill(table);

            DataCust.DataSource = table;

            db.close();
        }

        private void Main_mobile_TextChanged(object sender, EventArgs e)
        {
            db.close();
            db.open();
            DataTable table = new DataTable();
            string searchQuery = " SELECT [Cust_ID] as 'كود' ,[Cust_Name] as 'الإسم' ,[Cust_Address] as 'العنوان' ,[Cust_Phone] as 'الموبايل' ,[Notes] as 'ملاحظات' ,[Cust_First] as 'أول شراء' ,[Cust_Last] as 'آخر شراء' ,[Cust_TotalPoint] as 'إجمالي مشتريات العميل' ,[Cust_PointLeft] as 'نقاط غير مستخدمة' ,[Cust_Money] as 'المبلغ المستحق' FROM [dbo].[Customers] WHERE [Cust_Phone] LIKE '%" + Main_mobile.Text + "%'";
            SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
            adapter.Fill(table);

            DataCust.DataSource = table;

            db.close();
        }

        private void Main_Notes_TextChanged(object sender, EventArgs e)
        {
            db.close();
            db.open();
            DataTable table = new DataTable();
            string searchQuery = " SELECT [Cust_ID] as 'كود' ,[Cust_Name] as 'الإسم' ,[Cust_Address] as 'العنوان' ,[Cust_Phone] as 'الموبايل' ,[Notes] as 'ملاحظات' ,[Cust_First] as 'أول شراء' ,[Cust_Last] as 'آخر شراء' ,[Cust_TotalPoint] as 'إجمالي مشتريات العميل' ,[Cust_PointLeft] as 'نقاط غير مستخدمة' ,[Cust_Money] as 'المبلغ المستحق' FROM [dbo].[Customers] WHERE [Notes] LIKE '%" + Main_Notes.Text + "%'";
            SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
            adapter.Fill(table);

            DataCust.DataSource = table;

            db.close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DataCust.CurrentRow.Cells[0].Value);

            DataTable tblSearch = new DataTable();
            tblSearch.Clear();

            tblSearch = db.readData("select * from Customers where Cust_ID=" + id + " ", "");
            try
            {
                Pnt_ID.Text= tblSearch.Rows[0][0].ToString();
                txtName.Text= tblSearch.Rows[0][1].ToString();
                txtTotal.Text= tblSearch.Rows[0][7].ToString();
                txtPoint.Text= tblSearch.Rows[0][8].ToString();
                txtMoney.Text= tblSearch.Rows[0][9].ToString();
            }
            catch (Exception) { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool check = false;
            check = Validation();
            if (check == true)
            {

                decimal x = 0, y = 0, t = 0, z = 0 , z2 = 0;
                try
                {
                    x = Properties.Settings.Default.Nud_Change;
                    y = Properties.Settings.Default.Nud_Point;
                    t = Convert.ToDecimal(txtChange.EditValue);
                    z = ( x * t ) / y;
                    z2 = z + Convert.ToDecimal(txtMoney.Text);

                    if ( MessageBox.Show("المبلغ المستحق" + z + ". جنيه مصري" + "\n" + "وسوف يصبح مجموع المبلغ المالي للعميل : " + z2 + ". جنيه مصري " + "\n" + "أكمل عمليه التحويل ؟", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        string d = DtpDate.Value.ToString("dd/MM/yyyy");

                        db.exceuteData("update Customers set Cust_PointLeft = Cust_PointLeft - " + txtChange.Text + " , Cust_Money=Cust_Money + " + z + " where Cust_ID=" + Pnt_ID.Text + "", "");

                        db.exceuteData("insert into Point_Report Values (" + txtID.Text + " , " + Pnt_ID.Text + " ,N'" + txtName.Text + "' ,N'" + Properties.Settings.Default.USERNAME + "' , '" + d + "' ," + txtPoint.EditValue + " ," + txtChange.EditValue + " ," + z + " ," + z2 + " )", "");
                        
                        MessageBox.Show("تم التحويل بنجاح" + "\n" + "مجموع المبلغ المالي للعميل : " + z2 + ". جنيه مصري " , "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AutoNumber();
                    }
                    else
                    {
                        return;
                    }
                }
                catch { }      
            }
        }

        private bool Validation()
        {
            if (Pnt_ID.Text == "")
            {
                MessageBox.Show("من فضلك اختر عميل اولا");
                return false;
            }

            if (Convert.ToDecimal(txtTotal.EditValue) < Properties.Settings.Default.Nud_Min)
            {
                MessageBox.Show("هذا العميل لم تبلغ إجمالي مشترياته الحد الأدنى لتفعيل نظام النقاط له بعد");
                MessageBox.Show("الحد الأدنى لبدأ نظام النقاط للعميل هو" + Properties.Settings.Default.Nud_Min + "جنيه مصري", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToDecimal(txtChange.EditValue) <= 0)
            {
                MessageBox.Show("لا يمكن ان يتم تحويل صفر نقطة");
                return false;
            }

            if (Convert.ToDecimal(txtChange.EditValue) > Convert.ToDecimal(txtPoint.EditValue))
            {
                MessageBox.Show("لا يمكن ان يتم تحويل نقاط اكثر من قيمة النقاط المتوفرة للعميل");
                return false;
            }

            if (Convert.ToDecimal(NudPoint.EditValue) < 1 || Convert.ToDecimal(NudChange.EditValue) < 1)
            {
                MessageBox.Show("نظام النقاط غير مفعل ، يرجى تفعيله من شاشة الإعدادات");
                return false;
            }

            return true;
        }
    }
}