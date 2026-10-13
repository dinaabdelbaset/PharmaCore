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
    public partial class Frm_BuyReport : DevExpress.XtraEditors.XtraForm
    {
        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();
        DataTable tbl2 = new DataTable();

        public Frm_BuyReport()
        {
            InitializeComponent();
        }

        private void Frm_BuyReport_Load(object sender, EventArgs e)
        {
            FillSupplier();
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void FillSupplier()
        {
            cbxSupplier.DataSource = db.readData("select * from Suppliers", "");
            cbxSupplier.DisplayMember = "Sup_Name";
            cbxSupplier.ValueMember = "Sup_ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            string date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            string date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            if (rbtnAllSup.Checked)
            {
                if (!checkOrderNumber.Checked)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Sup_Name as 'اسم المورد' , [TotalOrder] as 'إجمالي الفاتورة' ,[User_Name] as 'المسئول عن الشراء' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].[Buy] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");
                }
                else
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Sup_Name as 'اسم المورد' , [TotalOrder] as 'إجمالي الفاتورة' ,[User_Name] as 'المسئول عن الشراء' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].[Buy] where Order_ID =" + txtOrderNumber.Text + " and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ORDER BY Order_ID DESC ", "");
                }
            }
            else if (rbtnOneSupplier.Checked)
            {
                if (!checkOrderNumber.Checked)
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Sup_Name as 'اسم المورد' , [TotalOrder] as 'إجمالي الفاتورة' ,[User_Name] as 'المسئول عن الشراء' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].[Buy] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Sup_Name=N'" + cbxSupplier.Text + "' ORDER BY Order_ID DESC ", "");
                }
                else
                {
                    tbl = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Sup_Name as 'اسم المورد' , [TotalOrder] as 'إجمالي الفاتورة' ,[User_Name] as 'المسئول عن الشراء' ,[Date] as 'التاريخ' ,[Time] as 'الوقت' FROM [dbo].[Buy] where Order_ID =" + txtOrderNumber.Text + " and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Sup_Name=N'" + cbxSupplier.Text + "' ORDER BY Order_ID DESC ", "");
                }
            }

            DgvSearch.DataSource = tbl;

            if (DgvDteils.DataSource != null)
                DgvDteils.DataSource = null;
            else
                DgvDteils.Rows.Clear();

            decimal totalPrice = 0;
            for (int i = 0; i < DgvSearch.Rows.Count; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalTax.Text = "0";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                MessageBox.Show("عرض التقرير التفصيلي معطل مؤقتاً (ملفات التقارير غير متوفرة)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPtintOrder_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                MessageBox.Show("طباعة فاتورة واحدة معطلة مؤقتاً (ملفات التقارير غير متوفرة)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                MessageBox.Show("طباعة جميع الفواتير معطلة مؤقتاً (ملفات التقارير غير متوفرة)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انت متاكد من حذف الفاتورة بالكامل ؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.exceuteData("delete from Buy where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");
                    db.exceuteData("delete from Buy_Detalis where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح");
                    btnSearch_Click(null, null);
                }
            }
        }

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id = Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);
                tbl2.Clear();
                tbl2 = db.readData(" SELECT [Order_ID] as 'رقم الفاتورة',Products.Pro_Name as 'اسم المنتج' ,[Buy_Detalis].[Expier] as 'الصلاحية' ,Unit_Name as 'الوحدة',[Buy_Detalis].[Qty] as 'الكمية' , [Bouns] as 'البونص' ,[Price_Ph] as 'سعر شراء الصيدلي',[Buy_Detalis].Tax_Value as 'الضريبة',Price_Tax as 'سعر البيع بعد الضريبة',[Discount] as 'الخصم',[Total] as 'اجمالي الصنف'  FROM [dbo].[Buy_Detalis],Products where Products.Pro_ID =[Buy_Detalis].Pro_ID and Order_ID =" + id + " ORDER BY Order_ID DESC ", "");
                DgvDteils.DataSource = tbl2;
            }
            else
            {
                MessageBox.Show("لا يوجد بيانات بالتفصيل لهذه الفاتورة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtTotalTax.Text = "0";
            if (DgvDteils.Rows.Count >= 1)
            {
                decimal totalTax = 0;
                for (int i = 0; i < DgvDteils.Rows.Count; i++)
                {
                    totalTax += Convert.ToDecimal(DgvDteils.Rows[i].Cells[7].Value) * Convert.ToDecimal(DgvDteils.Rows[i].Cells[4].Value);
                }
                try
                {
                    txtTotalTax.Text = Math.Round(totalTax, 2).ToString();
                }
                catch (Exception) { }
            }
        }

        private void checkOrderNumber_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}