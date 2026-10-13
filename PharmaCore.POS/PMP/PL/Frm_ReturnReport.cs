using DevExpress.XtraEditors;
using Microsoft.SqlServer.Management.Smo;
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
    public partial class Frm_ReturnReport : DevExpress.XtraEditors.XtraForm
    {

        DDatabase db = new DDatabase();
        DataTable tbl = new DataTable();
        DataTable tbl2 = new DataTable();


        public Frm_ReturnReport()
        {
            InitializeComponent();
        }

        private void Frm_ReturnReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnBuyReturn.Checked == true)
            {
                tbl = db.readData(" SELECT [Order_ID] as 'رقم العملية'  ,[Order_Type] as 'نوع العملية'  ,[Sup_Name] as 'اسم العميل' ,[Total] as 'اجمالي المبلغ المسترد للعميل' ,[Order_Date] as 'التاريخ' FROM [dbo].[ReturnsBuy] where  Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            }
            else if (rbtnSalesReturn.Checked == true)
            {
                tbl = db.readData(" SELECT [Order_ID] as 'رقم العملية'  ,[Order_Type] as 'نوع العملية'  ,[Cust_Name] as 'اسم العميل' ,[Total] as 'اجمالي المبلغ المسترد للعميل' ,[Order_Date] as 'التاريخ' FROM [dbo].[Returns] where  Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
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

                        if (rbtnBuyReturn.Checked == true)
                        {
                            db.readData("delete from ReturnsBuy where Order_ID=" + id + "", "");
                            db.readData("delete from Returns_DetalisBuy where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
                        }
                        else if (rbtnSalesReturn.Checked == true)
                        {
                            db.readData("delete from Returns where Order_ID=" + id + "", "");
                            db.readData("delete from Returns_Detalis where Order_ID=" + id + "", "تم مسح التقرير بنجاح");
                        }
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
                if (MessageBox.Show("هل انت متاكد من مسح التقارير بين فترتي التاريخ المحددة؟", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (rbtnBuyReturn.Checked == true)
                    {
                        db.exceuteData("delete from ReturnsBuy where  where Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

                        db.exceuteData("delete from Returns_DetalisBuy where  where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "تم المسح التقارير بنجاح");
                    }
                    else if (rbtnSalesReturn.Checked == true)
                    {
                        db.exceuteData("delete from Returns where  where Convert(date,[Order_Date] ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

                        db.exceuteData("delete from Returns_Detalis where  where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "تم المسح التقارير بنجاح");
                    }
                    btnSearch_Click(null, null);
                }
            }
        }

        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                int id;
                id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

                if (rbtnBuyReturn.Checked == true)
                {
                    tbl2.Clear();
                    tbl2 = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Pro_Name] as 'المنتج',[Unit_Name] as 'الوحدة',[Price] as 'السعر بعد الضريبة',[Qty] as 'الكمية' ,[Bouns] as 'البونص',[Total] as 'المبلغ المسترد من المورد',[User_Name] as 'اسم المستخدم' FROM [dbo].[Returns_DetalisBuy] where Order_ID=" + id + "", "");
                    DgvDteils.DataSource = tbl2;
                }
                else if (rbtnSalesReturn.Checked == true)
                {
                    tbl2.Clear();
                    tbl2 = db.readData(" SELECT [Order_ID] as 'رقم العملية' ,[Pro_Name] as 'المنتج',[Unit_Name] as 'الوحدة' ,[Price] as 'السعر بعد الضريبة' ,[Qty] as 'الكمية' ,[Total] as 'اجمالي الصنف',[Total_Order] as 'اجمالي الفاتورة',[Madfou3] as 'المبلغ المسترد للعميل',[User_Name] as 'اسم المستخدم' FROM [dbo].[Returns_Detalis] where Order_ID=" + id + "", "");
                    DgvDteils.DataSource = tbl2;
                }

            }
            else
            {
                MessageBox.Show("لا يوجد بيانات بالتفصيل لهذه العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
    }
}