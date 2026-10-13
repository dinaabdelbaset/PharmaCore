using DevExpress.XtraEditors;
using Microsoft.SqlServer.Management.Smo;
using PharmaCore.POS.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Return : DevExpress.XtraEditors.XtraForm
    {

        DataTable tbl = new DataTable();
        DDatabase db = new DDatabase();
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.ConnectionSrting);
        DataTable tbl2 = new DataTable();


        public Frm_Return()
        {
            InitializeComponent();
        }

        private void Frm_Return_Load(object sender, EventArgs e)
        {
            try
            {
                FillCustomer();
                DtpDate.Text = DateTime.Now.ToShortDateString();
                fillStore();
                stock_ID =Convert.ToString(Properties.Settings.Default.Stock_ID);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void FillCustomer()
        {
            cbxCustomer.DataSource = db.readData("select * from Customers", "");
            cbxCustomer.DisplayMember = "Cust_Name";
            cbxCustomer.ValueMember = "Cust_ID";
        }

        private void fillStore()
        {
            cbxStore1.DataSource = db.readData("select * from Store", "");
            cbxStore1.DisplayMember = "Store_Name";
            cbxStore1.ValueMember = "Store_ID";
            cbxStore2.DataSource = db.readData("select * from Store", "");
            cbxStore2.DisplayMember = "Store_Name";
            cbxStore2.ValueMember = "Store_ID";
        }
        string stock_ID = "";


        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل رقم فاتورة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tbl.Clear();
                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Order_ID=" + txtID.Text+ " ORDER BY Order_ID DESC ", "");
                DgvSearch.DataSource = tbl;


                txtTotalOrder.Text= "";
                txtMadfou3.Text= "";
                if (this.DgvDteils.DataSource != null)
                {
                    this.DgvDteils.DataSource = null;
                }
                else
                {
                    this.DgvDteils.Rows.Clear();
                }

                if (DgvSearch.Rows.Count >= 1)
                {
                    decimal totalOrder = 0;
                    int index = DgvSearch.SelectedRows[0].Index;
                    string name = Convert.ToString(DgvSearch.Rows[index].Cells[1].Value);

                    for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                    {
                        totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                    }

                    try
                    {
                        txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                        txtName1.Text = name;
                        txtName2.Text = name;
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("لا يوجد عمليات بيع بقيم البحث المختارة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        //when press sales return

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtCode.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل كود العميل", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cbxCustomer.Text= "";
                db.close();
                db.open();
                DataTable table = new DataTable();
                string searchQuery = " SELECT [Cust_Name] FROM [dbo].[Customers] WHERE [Cust_ID] LIKE '%" + txtCode.Text + "%' ";
                SqlDataAdapter adapter = new SqlDataAdapter(searchQuery, conn);
                adapter.Fill(table);

                cbxCustomer.DataSource = table;

                db.close();

                if (cbxCustomer.Items.Count <= 0)
                {
                    MessageBox.Show("من فضلك تأكد من كود العميل ، او اختاره يدويًا من القائمة المنسدلة .", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tbl.Clear();
                tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Cust_Name=N'"+cbxCustomer.Text+"' ORDER BY Order_ID DESC ", "");
                DgvSearch.DataSource = tbl;



                txtTotalOrder.Text= "";
                txtMadfou3.Text= "";
                if (this.DgvDteils.DataSource != null)
                {
                    this.DgvDteils.DataSource = null;
                }
                else
                {
                    this.DgvDteils.Rows.Clear();
                }

                if (DgvSearch.Rows.Count >= 1)
                {
                    decimal totalOrder = 0;
                    int index = DgvSearch.SelectedRows[0].Index;
                    string name = Convert.ToString(DgvSearch.Rows[index].Cells[1].Value);

                    for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                    {
                        totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                    }

                    try
                    {
                        txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                        txtName1.Text = name;
                        txtName2.Text = name;
                    }
                    catch (Exception) { }
                }
                else
                {
                    MessageBox.Show("لا يوجد عمليات بيع بقيم البحث المختارة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void cbxCustomer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxCustomer.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك تأكد من كود العميل ، او اختاره يدويًا من القائمة المنسدلة .", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tbl.Clear();
            tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Cust_Name=N'"+cbxCustomer.Text+"' ORDER BY Order_ID DESC ", "");
            DgvSearch.DataSource = tbl;


            txtTotalOrder.Text= "";
            txtMadfou3.Text= "";
            if (this.DgvDteils.DataSource != null)
            {
                this.DgvDteils.DataSource = null;
            }
            else
            {
                this.DgvDteils.Rows.Clear();
            }

            if (DgvSearch.Rows.Count >= 1)
            {
                decimal totalOrder = 0;
                int index = DgvSearch.SelectedRows[0].Index;
                string name = Convert.ToString(DgvSearch.Rows[index].Cells[1].Value);

                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                }

                try
                {
                    txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                    txtName1.Text = name;
                    txtName2.Text = name;
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show("لا يوجد عمليات بيع بقيم البحث المختارة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void DtpDate_ValueChanged(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',[Cust_Name] as 'اسم العميل' ,[TotalOrder] as 'اجمالي الفاتورة',[TotalNeed] as 'إجمالي المدفوع بعد الخصومات' ,[Us_Name] as 'اسم الكاشير' , [Date]  as 'التاريخ',[Time] as 'الوقت' FROM [dbo].Sales where Date=N'" + DtpDate.Text+ "' ORDER BY Order_ID DESC ", "");
            DgvSearch.DataSource = tbl;


            txtTotalOrder.Text= "";
            txtMadfou3.Text= "";
            if (this.DgvDteils.DataSource != null)
            {
                this.DgvDteils.DataSource = null;
            }
            else
            {
                this.DgvDteils.Rows.Clear();
            }

            if (DgvSearch.Rows.Count >= 1)
            {
                decimal totalOrder = 0;
                int index = DgvSearch.SelectedRows[0].Index;
                string name = Convert.ToString(DgvSearch.Rows[index].Cells[1].Value);

                for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
                {
                    totalOrder += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                }

                try
                {
                    txtTotalOrder.Text = (Math.Round(totalOrder, 2)).ToString();
                    txtName1.Text = name;
                    txtName2.Text = name;
                }
                catch (Exception) { }
            }
            else
            {
                MessageBox.Show("لا يوجد عمليات بيع بقيم البحث المختارة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                txtMadfou3.Text= "";


                tbl2.Clear();
                tbl2 = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Pro_Name as 'المنتج' ,Unit as 'الوحدة' , [Sales_Detalis].[Tax_Value] as 'الضريبة' ,[Sales_Detalis].[Price_Tax] as 'السعر بعد الضريبة',[Sales_Detalis].[Qty] as 'الكمية' ,[Sales_Detalis].[Price] as 'إجمالي السعر',[Discount] as 'الخصم'  ,[Total] as 'إجمالي المدفوع بعد الخصم' FROM [dbo].[Sales_Detalis] where Order_ID=" + id + "", "");
                DgvDteils.DataSource = tbl2;

            }
            else
            {
                MessageBox.Show("لا يوجد بيانات بالتفصيل لهذه العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (DgvDteils.Rows.Count >= 1)
            {
                decimal totalMadfou3 = 0;
                for (int i = 0; i <= DgvDteils.Rows.Count - 1; i++)
                {
                    totalMadfou3 += Convert.ToDecimal(DgvDteils.Rows[i].Cells[8].Value);
                }
                try
                {
                    txtMadfou3.Text = (Math.Round(totalMadfou3, 2)).ToString();
                }
                catch (Exception) { }
            }
        }

        private void DgvDteils_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnReturnAll.Enabled = false;
            btnReturnItemOnly.Enabled = true;
        }

            private void btnReturnAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
               returnAllSales();
            }
        }

        string d = "";


        private void onLoadScreen()
        {
            tbl.Clear();
            DgvSearch.DataSource = tbl;
            if (this.DgvDteils.DataSource != null)
            {
                this.DgvDteils.DataSource = null;
            }
            else
            {
                this.DgvDteils.Rows.Clear();
            }
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.TextEdit txt)
                {
                    txt.EditValue = null;   // أو txt.Text = string.Empty;
                }
            }

            txtMadfou3.Text = "0";
            txtID.Focus();
        }
        //when press return all order sales
        private void returnAllSales()
        {
            d= DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName1.Text == "") { MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد"); return; }
            if (DgvSearch.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة المراد ارجاعها بالكامل ... من الجدول الأول", "تاكيد"); return; }
            if (DgvDteils.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة من الجدول الأول حتى تبان تفاصيلها بالجدول الثاني", "تاكيد"); return; }

            db.exceuteData("delete from Sales where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");
            db.exceuteData("delete from Sales_Detalis where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " ", "");

            //insert data into return table
            db.exceuteData("insert into Returns (Order_Date , Order_Type, Cust_Name, Total) values ('"+d+"',N'مرتجع فاتورة مبيعات كاملة' ,N'"+DgvSearch.CurrentRow.Cells[1].Value+"' ,N'"+DgvSearch.CurrentRow.Cells[3].Value+"')", "");
            int id = 1;
            try
            {
                id =Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }

            decimal totalTax = 0;
            //insert into return detalis 
            for (int i = 0; i <=DgvDteils.Rows.Count - 1; i++)
            {
                db.exceuteData("insert into Returns_Detalis values (" + id+" ,N'"+txtName1.Text+"' ,N'"+DgvDteils.Rows[i].Cells[1].Value+"' , N'"+d+"' ,"+ DgvDteils.Rows[i].Cells[5].Value + " ,"+ DgvDteils.Rows[i].Cells[4].Value + " ,"+ DgvDteils.Rows[i].Cells[6].Value + " , N'"+Properties.Settings.Default.USERNAME+"' ,"+ DgvSearch.Rows[i].Cells[2].Value +" , "+ DgvSearch.Rows[i].Cells[3].Value + " ,N'"+ DgvDteils.Rows[i].Cells[2].Value + "')", "");
                int proID = 1;
                try
                {
                    proID = Convert.ToInt32(db.readData("select Pro_ID from Products where Pro_Name=N'"+ DgvDteils.Rows[i].Cells[1].Value + "'", "").Rows[0][0]);
                }
                catch (Exception) { }
                decimal QtyInMain = 0, realQty = 0;
                DataTable tblUnit = new DataTable();
                tblUnit.Clear();
                tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_Name=N'" + DgvDteils.Rows[i].Cells[2].Value  + "'", "");
                try
                {
                    QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][3]);
                }
                catch (Exception) { }

                realQty =Convert.ToDecimal(DgvDteils.Rows[i].Cells[5].Value) / QtyInMain;

                db.exceuteData("update Products set Qty=Qty + "+ realQty + " where Pro_ID="+proID+"", "");

                DataTable tblQty = new DataTable();
                tblQty.Clear();
                tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID="+proID+" and Store_ID="+cbxStore1.SelectedValue+" ", "");
                if (tblQty.Rows.Count >= 1)
                {
                    db.exceuteData("update Products_Qty set Qty=Qty + "+ realQty + " where Pro_ID="+ proID + " and Store_ID="+cbxStore1.SelectedValue+" and Qty="+tblQty.Rows[0][3]+" and Buy_Price="+ tblQty.Rows[0][4] + "", "");
                }
                else
                {
                    tblQty.Clear();
                    tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " ", "");
                    if (tblQty.Rows.Count >= 1)
                    {
                        db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore1.SelectedValue + " ,N'" + cbxStore1.Text + "' , " + realQty + " , " + tblQty.Rows[0][4] + " , " + DgvDteils.Rows[i].Cells[4].Value + ")", "");
                    }
                    else
                    {
                        string buyPrice = Microsoft.VisualBasic.Interaction.InputBox("ادخل سعر الشراء للمنتج " + (DgvDteils.Rows[i].Cells[1].Value), "سعر الشراء", "من فضلك ادخل السعر هنا", 1, 1);
                        db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore1.SelectedValue + " ,N'" + cbxStore1.Text + "' , " + realQty + " , " + buyPrice + " , " + DgvDteils.Rows[i].Cells[4].Value + ")", "");

                    }
                }
                totalTax += Convert.ToDecimal(DgvDteils.Rows[i].Cells[3].Value);

            }

            db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[3].Value + ",N'" + d + "' ,N'"+Properties.Settings.Default.USERNAME+"' ,N'مرتجعات مبيعات', N'') ", "");
            db.exceuteData("update stock set Money=Money - " + DgvSearch.CurrentRow.Cells[3].Value + " where Stock_ID=" + stock_ID + "", "");

            decimal totalBeforeTax = 0;
            totalBeforeTax = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[3].Value) - totalTax;
            db.exceuteData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvSearch.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة' ,N'لا يوجد' ,N'" + txtName1.Text + "' ," + totalBeforeTax + " ," + totalTax + " ," + DgvSearch.CurrentRow.Cells[3].Value + " ,N'" + d + "')", "");


            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();

        }
    

        private void btnReturnItemOnly_Click(object sender, EventArgs e)
        {
            if (DgvDteils.Rows.Count >= 1)
            {
                if (rbtnReturnItemOnly.Checked == true)
                {
                  returnItemSaleOnly();
                }
                else if (rbtnReturnQtyonly.Checked == true)
                {
                  returnQtySaleOnly();
                }
            }
        }

        //when user press return item only in sales
        private void returnItemSaleOnly()
        {

            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "") { MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد"); return; }
            if (DgvSearch.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة المراد ارجاعها بالكامل ... من الجدول الأول", "تاكيد"); return; }
            if (DgvDteils.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة من الجدول الأول حتى تبان تفاصيلها بالجدول الثاني", "تاكيد"); return; }


            int proID = 1;
            try
            {
                proID = Convert.ToInt32(db.readData("select Pro_ID from Products where Pro_Name=N'" + DgvDteils.CurrentRow.Cells[1].Value + "'", "").Rows[0][0]);
            }
            catch (Exception) { }
            db.exceuteData("delete from Sales_Detalis where Order_ID=" + DgvDteils.CurrentRow.Cells[0].Value + " and Pro_ID="+proID+" and Qty="+ DgvDteils.CurrentRow.Cells[5].Value + " and Total=" + DgvDteils.CurrentRow.Cells[8].Value + "", "");

            //insert data into return table
            db.exceuteData("insert into Returns (Order_Date , Order_Type, Cust_Name, Total) values ('"+d+"',N'مرتجع منتج واحد بالكامل' ,N'" + txtName2.Text + "' ,N'"+DgvDteils.CurrentRow.Cells[8].Value+"')", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }
            //insert into return detalis 

            db.exceuteData("insert into Returns_Detalis values (" + id + " ,N'" + txtName2.Text + "' ,N'" + DgvDteils.CurrentRow.Cells[1].Value + "' , N'" + d + "' ," + DgvDteils.CurrentRow.Cells[5].Value + " ," + DgvDteils.CurrentRow.Cells[4].Value + " ," + DgvDteils.CurrentRow.Cells[6].Value + " , N'" + Properties.Settings.Default.USERNAME + "' ," + DgvDteils.CurrentRow.Cells[8].Value + " ," + DgvDteils.CurrentRow.Cells[8].Value + " ,N'" + DgvDteils.CurrentRow.Cells[2].Value + "')", "");

            decimal QtyInMain = 0, realQty = 0, totalTax = 0;
            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_Name=N'" + DgvDteils.CurrentRow.Cells[2].Value + "'", "");
            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][3]);
            }
            catch (Exception) { }

            realQty = Convert.ToDecimal(DgvDteils.CurrentRow.Cells[5].Value) / QtyInMain;

            db.exceuteData("update Products set Qty=Qty + " + realQty + " where Pro_ID=" + proID + "", "");

            DataTable tblQty = new DataTable();
            tblQty.Clear();
            tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " ", "");
            if (tblQty.Rows.Count >= 1)
            {
                db.exceuteData("update Products_Qty set Qty=Qty + " + realQty + " where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Qty=" + tblQty.Rows[0][3] + " and Buy_Price=" + tblQty.Rows[0][4] + "", "");
            }
            else
            {
                tblQty.Clear();
                tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " ", "");
                if (tblQty.Rows.Count >= 1)
                {
                    db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + " ,N'" + cbxStore2.Text + "' , " + realQty + " , " + tblQty.Rows[0][4] + " , " + DgvDteils.CurrentRow.Cells[4].Value + ")", "");
                }
                else
                {
                    string buyPrice = Microsoft.VisualBasic.Interaction.InputBox("ادخل سعر الشراء للمنتج " + (DgvDteils.CurrentRow.Cells[1].Value), "سعر الشراء", "من فضلك ادخل السعر هنا", 1, 1);
                    db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + " ,N'" + cbxStore2.Text + "' , " + realQty + " , " + buyPrice + " , " + DgvDteils.CurrentRow.Cells[4].Value + ")", "");

                }
            }

            db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvDteils.CurrentRow.Cells[8].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مبيعات', N'') ", "");
            db.exceuteData("update stock set Money=Money - " + DgvDteils.CurrentRow.Cells[8].Value + " where Stock_ID=" + stock_ID + "", "");

            //insert into tax report
            totalTax = Convert.ToDecimal(DgvDteils.CurrentRow.Cells[3].Value);
            decimal totalBeforeTax = 0; decimal totalItem = 0;
            totalItem = Convert.ToDecimal(DgvDteils.CurrentRow.Cells[4].Value);
            totalBeforeTax = Convert.ToDecimal(totalItem) - totalTax;
            db.exceuteData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvDteils.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة' ,N'لا يوجد' ,N'" + txtName2.Text + "' ," + totalBeforeTax + " ," + totalTax + " ," + totalItem + " ,N'" + d + "')", "");

            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();

        }
        
        //when user press return qty in sales 
        private void returnQtySaleOnly()
        {
            d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (txtName2.Text == "") { MessageBox.Show("من فضلك ادخل اسم العميل", "تاكيد"); return; }
            if (DgvSearch.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة المراد ارجاعها بالكامل ... من الجدول الأول", "تاكيد"); return; }
            if (DgvDteils.Rows.Count <= 0) { MessageBox.Show("من فضلك حدد على الفاتورة من الجدول الأول حتى تبان تفاصيلها بالجدول الثاني", "تاكيد"); return; }

            if ((Convert.ToDecimal(DgvDteils.CurrentRow.Cells[5].Value) - Convert.ToDecimal(NudQty.EditValue)) < 0)
            {
                MessageBox.Show("الكمية المراد إرجاعها أكبر من الكمية التي تم بيعها بالفعل", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((Convert.ToDecimal(DgvDteils.CurrentRow.Cells[5].Value) - Convert.ToDecimal(NudQty.EditValue)) == 0)
            {
                returnItemSaleOnly();
                return;
            }

            int proID = 1;
            try
            {
                proID = Convert.ToInt32(db.readData("select Pro_ID from Products where Pro_Name=N'" + DgvDteils.CurrentRow.Cells[1].Value + "'", "").Rows[0][0]);
            }
            catch (Exception) { }
            decimal QtyInMain = 0, realQty = 0, realMoney = 0;
            DataTable tblUnit = new DataTable();
            tblUnit.Clear();
            tblUnit = db.readData("select * from Products_Unit where Pro_ID=" + proID + " and Unit_Name=N'" + DgvDteils.CurrentRow.Cells[2].Value + "'", "");
            try
            {
                QtyInMain = Convert.ToDecimal(tblUnit.Rows[0][3]);
            }
            catch (Exception) { }

            realQty = Convert.ToDecimal(NudQty.EditValue) / QtyInMain;
            realMoney = (Convert.ToDecimal(DgvDteils.CurrentRow.Cells[8].Value) * Convert.ToDecimal(NudQty.EditValue)) / Convert.ToDecimal(DgvDteils.CurrentRow.Cells[5].Value);

            db.exceuteData("update Sales_Detalis set Qty=Qty - "+ realQty + " where Order_ID=" + DgvDteils.CurrentRow.Cells[0].Value + " and Pro_ID=" + proID + " and Qty=" + DgvDteils.CurrentRow.Cells[5].Value + " and Total=" + DgvDteils.CurrentRow.Cells[8].Value + "", "");
            //insert data into return table
            db.exceuteData("insert into Returns (Order_Date , Order_Type, Cust_Name, Total) values ('"+d+"',N'مرتجع كمية معينة من منتج واحد' ,N'" + txtName2.Text + "' ,N'"+ realMoney +"')", "");
            int id = 1;
            try
            {
                id = Convert.ToInt32(db.readData("select max(Order_ID) from Returns", "").Rows[0][0]);
            }
            catch (Exception) { }
            //insert into return detalis 

            decimal realReturn = 0;
            realReturn = Convert.ToDecimal(DgvDteils.CurrentRow.Cells[8].Value ) * realQty ;

            db.exceuteData("insert into Returns_Detalis values (" + id + " ,N'" + txtName2.Text + "' ,N'" + DgvDteils.CurrentRow.Cells[1].Value + "' , N'" + d + "' ," + realQty + " ," + DgvDteils.CurrentRow.Cells[4].Value + " ," + DgvDteils.CurrentRow.Cells[6].Value + " , N'" + Properties.Settings.Default.USERNAME + "' ," + realMoney + " ," + realReturn + " ,N'" + DgvDteils.CurrentRow.Cells[2].Value + "' )", "");

            db.exceuteData("update Products set Qty=Qty + " + realQty + " where Pro_ID=" + proID + "", "");

            DataTable tblQty = new DataTable();
            tblQty.Clear();
            tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " ", "");
            if (tblQty.Rows.Count >= 1)
            {
                db.exceuteData("update Products_Qty set Qty=Qty + " + realQty + " where Pro_ID=" + proID + " and Store_ID=" + cbxStore2.SelectedValue + " and Qty=" + tblQty.Rows[0][3] + " and Buy_Price=" + tblQty.Rows[0][4] + "", "");
            }
            else
            {
                tblQty.Clear();
                tblQty = db.readData("select top 1 * from Products_Qty where Pro_ID=" + proID + " ", "");
                if (tblQty.Rows.Count >= 1)
                {
                    db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + " ,N'" + cbxStore2.Text + "' , " + realQty + " , " + tblQty.Rows[0][4] + " , " + DgvDteils.CurrentRow.Cells[4].Value + ")", "");
                }
                else
                {
                    string buyPrice = Microsoft.VisualBasic.Interaction.InputBox("ادخل سعر الشراء للمنتج " + (DgvDteils.CurrentRow.Cells[1].Value), "سعر الشراء", "من فضلك ادخل السعر هنا", 1, 1);
                    db.exceuteData("insert into Products_Qty values (" + proID + " , " + cbxStore2.SelectedValue + " ,N'" + cbxStore2.Text + "' , " + realQty + " , " + buyPrice + " , " + DgvDteils.CurrentRow.Cells[4].Value + ")", "");

                }
            }
            
            db.exceuteData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + realMoney + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مرتجعات مبيعات', N'') ", "");
            db.exceuteData("update stock set Money=Money - " + realMoney + " where Stock_ID=" + stock_ID + "", "");

            //insert into tax report
            decimal totalTax = 0, itemTax = 0;
            itemTax = Convert.ToDecimal(DgvDteils.CurrentRow.Cells[3].Value) / Convert.ToDecimal(DgvDteils.CurrentRow.Cells[5].Value);
            totalTax = itemTax * Convert.ToDecimal(NudQty.EditValue);
            decimal totalBeforeTax = 0; decimal totalItem = 0;
            totalItem = Convert.ToDecimal(NudQty.EditValue) * realMoney;
            totalBeforeTax = Convert.ToDecimal(totalItem) - totalTax;
            db.exceuteData("insert into Taxes_Report (Order_Num,Order_Type,Tax_Type,Sup_Name,Cust_Name,Total_Order,Total_Tax,Total_AfterTax,Date) values (" + Convert.ToDecimal(DgvDteils.CurrentRow.Cells[0].Value) + " ,N'مرتجعات مبيعات' ,N'قيمة مضافة'  ,N'لا يوجد' ,N'" + txtName2.Text + "'," + totalBeforeTax + " ," + totalTax + " ," + totalItem + " ,N'" + d + "')", "");


            MessageBox.Show("تمت عمليه الارجاع بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            onLoadScreen();
        }

        private void DgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnReturnAll.Enabled = true;
            btnReturnItemOnly.Enabled = false;
        }
    }
}