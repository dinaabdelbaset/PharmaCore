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
    public partial class Frm_Storetransfire : DevExpress.XtraEditors.XtraForm
    {


        BL.DDatabase db = new BL.DDatabase();
        DataTable tbl = new DataTable();

        public Frm_Storetransfire()
        {
            InitializeComponent();
        }

        private void Frm_Storetransfire_Load(object sender, EventArgs e)
        {
            FillPro();
            FillStore();
        }


        private void FillPro()
        {
            cbxProducts.DataSource = db.readData("select * from Products", "");
            cbxProducts.DisplayMember = "Pro_Name";
            cbxProducts.ValueMember = "Pro_ID";
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

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtBarcode.Text != "")
                {
                    tbl.Clear();
                    tbl = db.readData("select * from Products where Barcode =N'"+txtBarcode.Text+"'", "");
                    if (tbl.Rows.Count >= 1)
                    {
                        cbxProducts.SelectedValue = Convert.ToDecimal(tbl.Rows[0][0]);
                    }

                }
            }
        }

        private void cbxProducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxUnit.DataSource = db.readData("select * from Products_Unit where Pro_ID=" + cbxProducts.SelectedValue + "", "");
                cbxUnit.DisplayMember = "Unit_Name";
                cbxUnit.ValueMember = "Unit_ID";
            }
            catch (Exception) { }
        }


        //call to update qty in store
        private void updateQtyInStore(int pro_ID, decimal realQty)
        {
            DataTable tblQty = new DataTable();
            decimal QtyInStoreFirstRaw = 0;
            db.exceuteData("delete from Products_Qty where Qty <=0", "");

            tblQty.Clear();
            tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID="+ pro_ID + " and Store_ID="+cbxStoreFrom.SelectedValue+"", "");

            QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);
            if (QtyInStoreFirstRaw - realQty >= 1)
            {
                db.exceuteData("update Products_Qty set Qty=Qty - " + realQty + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");

            }
            else if (QtyInStoreFirstRaw - realQty == 0)
            {
                db.exceuteData("update Products_Qty set Qty=Qty - " + realQty + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                db.exceuteData("delete Products_Qty where Qty <= 0", "");
            }
            else if (QtyInStoreFirstRaw - realQty < 0)
            {

                db.exceuteData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                db.exceuteData("delete Products_Qty where Qty <= 0", "");

                decimal baky = Math.Abs(QtyInStoreFirstRaw - realQty);

                tblQty.Clear();
                tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

                QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);

                if (QtyInStoreFirstRaw - baky >= 0)
                {
                    db.exceuteData("update Products_Qty set Qty=Qty - " + baky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                    db.exceuteData("delete Products_Qty where Qty <= 0", "");

                }
                else if (QtyInStoreFirstRaw - baky < 0)
                {
                    decimal secondbaky = Math.Abs(QtyInStoreFirstRaw - baky);
                    db.exceuteData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                    db.exceuteData("delete Products_Qty where Qty <= 0", "");

                    tblQty.Clear();
                    tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

                    QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);

                    if (QtyInStoreFirstRaw - secondbaky >= 0)
                    {
                        db.exceuteData("update Products_Qty set Qty=Qty - " + secondbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                        db.exceuteData("delete Products_Qty where Qty <= 0", "");
                    }
                    else if (QtyInStoreFirstRaw - secondbaky < 0)
                    {
                        decimal thirdbaky = Math.Abs(QtyInStoreFirstRaw - secondbaky);
                        db.exceuteData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                        db.exceuteData("delete Products_Qty where Qty <= 0", "");

                        tblQty.Clear();
                        tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

                        QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);
                        if (QtyInStoreFirstRaw - thirdbaky >= 0)
                        {
                            db.exceuteData("update Products_Qty set Qty=Qty - " + thirdbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                            db.exceuteData("delete Products_Qty where Qty <= 0", "");
                        }
                        else if (QtyInStoreFirstRaw - thirdbaky < 0)
                        {
                            decimal forthbaky = Math.Abs(QtyInStoreFirstRaw - thirdbaky);
                            db.exceuteData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                            db.exceuteData("delete Products_Qty where Qty <= 0", "");


                            tblQty.Clear();
                            tblQty = db.readData("select Top 1 * from Products_Qty where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + "", "");

                            QtyInStoreFirstRaw = Convert.ToDecimal(tblQty.Rows[0][3]);

                            if (QtyInStoreFirstRaw - forthbaky >= 0)
                            {
                                db.exceuteData("update Products_Qty set Qty=Qty - " + forthbaky + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                                db.exceuteData("delete Products_Qty where Qty <= 0", "");
                            }
                            else if (QtyInStoreFirstRaw - forthbaky < 0)
                            {
                                db.exceuteData("update Products_Qty set Qty=Qty - " + QtyInStoreFirstRaw + " where Pro_ID=" + pro_ID + " and Store_ID=" + cbxStoreFrom.SelectedValue + " and Qty=" + QtyInStoreFirstRaw + " and Buy_Price=" + Convert.ToDecimal(tblQty.Rows[0][4]) + "", "");
                                db.exceuteData("delete Products_Qty where Qty <= 0", "");
                            }
                        }
                    }
                }

            }

        }
        private void insertQtyinStore(int pro_ID, decimal realQty)
        {
            db.exceuteData("insert into Products_Qty values ("+ pro_ID +" ,"+ cbxStoreTo.SelectedValue +" ,N'"+ cbxStoreTo.Text +"' ,"+ realQty +" ,"+ NudBuyPrice.EditValue + " ,"+ NudSalePrice.EditValue + ")", "");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable tblUnit = new DataTable();
            decimal QtyInMain = 0, realQty = 0, totalQtyInStore = 0;
            try
            {
                if (Convert.ToDecimal(NudSalePrice.EditValue) <= 0 || Convert.ToDecimal(NudBuyPrice.EditValue) <= 0)
                {
                    MessageBox.Show("من فضلك ادخل سعر الشراء وسعر البيع للمنتج الذى تم تحويله", "تاكيد");
                    return;
                }
                if (Convert.ToInt32(cbxStoreFrom.SelectedValue)== Convert.ToInt32(cbxStoreTo.SelectedValue))
                {
                    MessageBox.Show("لا يمكن التحويل لنفس المخزن", "تاكيد");
                    return;
                }
                int pro_ID = Convert.ToInt32(cbxProducts.SelectedValue);
                tbl.Clear();
                tbl = db.readData("select * from Products_Unit where Pro_ID="+ pro_ID + " and Unit_Name=N'"+cbxUnit.Text+"'", "");
                try
                {
                    QtyInMain = Convert.ToDecimal(tbl.Rows[0][3]);
                }
                catch (Exception) { }

                if (QtyInMain > 1)
                {
                    realQty = Convert.ToDecimal(NudQty.EditValue) / QtyInMain;
                }
                else
                {
                    realQty = Convert.ToDecimal(NudQty.EditValue);
                }

                try
                {
                    totalQtyInStore =Convert.ToDecimal(db.readData("select sum(Qty) from Products_Qty where Pro_ID="+pro_ID+" and Store_ID="+cbxStoreFrom.SelectedValue+"", "").Rows[0][0]);
                }
                catch (Exception) { }

                if (totalQtyInStore - realQty < 0)
                {
                    MessageBox.Show("الكمية المراد تحويلها غير موجود فى المخزن حاليا", "تاكيد");
                    return;
                }

                updateQtyInStore(pro_ID, realQty);
                insertQtyinStore(pro_ID, realQty);
                insertIntoProductTransfire();
                MessageBox.Show("تمت عملية التحويل بنجاح", "تاكيد");
                autonumber();
            }
            catch (Exception) { }
        }

        private void insertIntoProductTransfire()
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            db.exceuteData("insert into Products_Transfire (Pro_ID,Pro_Name,Store_From,Store_To,Qty,Unit,Buy_Price,Sale_Price,Date,Name,Reason) Values ("+cbxProducts.SelectedValue+" ,N'"+cbxProducts.Text+"' ,N'"+cbxStoreFrom.Text+"' ,N'"+cbxStoreTo.Text+"' ,"+NudQty.EditValue + " ,N'"+cbxUnit.Text+"' ,"+NudBuyPrice.EditValue + " ,"+NudSalePrice.EditValue + " , N'"+d+"' ,N'"+txtName.Text+"' ,N'"+txtReason.Text+"')", "");
        }


        private void autonumber()
        {
            try
            {
                cbxProducts.SelectedIndex = 0;
                cbxStoreFrom.SelectedIndex = 0;
                cbxStoreTo.SelectedIndex = 0;
                cbxUnit.SelectedIndex = 0;
                NudBuyPrice.EditValue = 1;
                NudSalePrice.EditValue = 1;
                NudQty.EditValue = 1;
                txtName.EditValue = 0;
                txtReason.Clear();
            }
            catch (Exception) { }
        }
    }
}