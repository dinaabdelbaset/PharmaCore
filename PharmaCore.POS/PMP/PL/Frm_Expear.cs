using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class Frm_Expear : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();
        

        public Frm_Expear()
        {
            InitializeComponent();
        }


        private void Frm_Expear_Load(object sender, EventArgs e)
        {

        }


        private void search_Click(object sender, EventArgs e)
        {
            string now;
            now = DateTime.Now.ToString("yyyy-MM-dd");

            if (cbxDate.SelectedIndex == 0)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) < '" + now + "' ", "");

                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 1)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");

                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 2)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 3)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 4)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(4).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 5)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(5).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 6)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            if (cbxDate.SelectedIndex == 7)
            {
                try
                {

                    string now2;
                    now2 = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
                    tbl.Clear();
                    tbl = db.readData(" SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',[Qty] as 'الكمية',[PH_Price] as 'سعر الشراء',[Sale_Price] as 'سعر البيع',[Expier] as 'تاريخ الإنتهاء',[Barcode] as 'الباركود' ,Products_Group.Group_Name as 'اسم الشركة' ,[Active_Name] as 'المادة الفعالة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");
                    DgvSearch.DataSource = tbl;
                }
                catch (Exception) { }
            }

            Auto();

        }

        private void Auto()
        {

            decimal totalBuy = 0 , totalSale = 0 , totalQty = 0;


            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalQty += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);
                totalBuy += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                totalSale += Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);
            }

            txtTotal.Text = (DgvSearch.Rows.Count).ToString();
            txtTotalQty.Text = Math.Round(totalQty, 2).ToString();
            txtTotalBuy.Text = Math.Round(totalBuy, 2).ToString();
            txtTotalSale.Text = Math.Round(totalSale, 2).ToString();

        }
    }
}