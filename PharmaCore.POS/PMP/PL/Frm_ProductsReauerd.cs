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
    public partial class Frm_ProductsReauerd : DevExpress.XtraEditors.XtraForm
    {

        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();


        public Frm_ProductsReauerd()
        {
            InitializeComponent();
        }

        private void Frm_ProductsReauerd_Load(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select Pro_ID as 'كود المنتج' , Pro_Name as 'اسم المنتج' , Barcode as 'الباركود' ,Qty as 'الكمية المتوفره منه' ,MinQty as ' الحد الادنى للمنتج للتذكير' , PH_Price as 'سعر الصيدلي' , Sale_PriceTax as 'سعر الجمهور' from Products where MinQty >=1 and Qty <= MinQty", "");

            DgvSearch.DataSource = tbl;
            txtTotal.Text = (DgvSearch.Rows.Count).ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int id;
            id= Convert.ToInt32(DgvSearch.CurrentRow.Cells[0].Value);

            if (id>0)
            {
                try
                {
                    var rs = MessageBox.Show("هل أنت متأكد من مسح المنتج بشكل نهائي من البرنامج ؟ ", "عملية مسح", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        db.readData("delete from Products where Pro_ID=" + id + "", "");
                        db.readData("delete from Products_Unit where Pro_ID=" + id + "", "");
                        db.readData("delete from Products_Qty where Pro_ID=" + id + "", "");
                        MessageBox.Show("تم المسح بنجاح");
                        Frm_ProductsReauerd_Load(null , null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("حدد أولًا منتج لمسحه بشكل نهائي من البرنامج");
            }
        }
    }
}
