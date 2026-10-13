using DevExpress.XtraEditors;
using PharmaCore.POS.Services;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Setting : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Setting()
        {
            InitializeComponent();
            this.Height += 60;
            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
            try { AddThemeToggleControl(); } catch { }
        }

        private void AddThemeToggleControl()
        {
            var toggleTheme = new DevExpress.XtraEditors.ToggleSwitch();
            toggleTheme.Name = "toggleTheme";
            toggleTheme.Properties.OnText = "الوضع المظلم (Dark)";
            toggleTheme.Properties.OffText = "الوضع المضيء (Light)";
            toggleTheme.IsOn = PharmaCore.POS.Helpers.ThemeHelper.IsDarkMode();
            toggleTheme.Size = new System.Drawing.Size(180, 24);
            toggleTheme.Location = new System.Drawing.Point(25, this.ClientSize.Height - 50);
            toggleTheme.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            toggleTheme.Toggled += (s, e) => {
                PharmaCore.POS.Helpers.ThemeHelper.SetThemeMode(toggleTheme.IsOn);
            };
            this.Controls.Add(toggleTheme);
            toggleTheme.BringToFront();
        }

        private void Frm_Setting_Load(object sender, EventArgs e)
        {
            try { LoadPrinterSettings(); } catch { }
            try { LoadInvoiceSettings(); } catch { }
            try { LoadGeneralSettings(); } catch { }
            try { LoadPointsSettings(); } catch { }
        }

        #region ── Tab 1: Printer Settings ──

        private void LoadPrinterSettings()
        {
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                string name = PrinterSettings.InstalledPrinters[i];
                cbxPrinter1.Items.Add(name);
                cbxPrinter2.Items.Add(name);
                cbxPrinter3.Items.Add(name);
            }

            // Load saved values (Properties → AppSettingsStore fallback)
            cbxPrinter1.Text = FirstNonEmpty(Properties.Settings.Default.PrinterName1, AppSettingsStore.Get("PrinterName1"));
            cbxPrinter2.Text = FirstNonEmpty(Properties.Settings.Default.PrinterName2, AppSettingsStore.Get("PrinterName2"));
            cbxPrinter3.Text = FirstNonEmpty(Properties.Settings.Default.PrinterName3, AppSettingsStore.Get("PrinterName3"));

            if (string.IsNullOrEmpty(cbxPrinter1.Text) && cbxPrinter1.Items.Count > 0) cbxPrinter1.SelectedIndex = 0;
            if (string.IsNullOrEmpty(cbxPrinter2.Text) && cbxPrinter2.Items.Count > 0) cbxPrinter2.SelectedIndex = 0;
            if (string.IsNullOrEmpty(cbxPrinter3.Text) && cbxPrinter3.Items.Count > 0) cbxPrinter3.SelectedIndex = 0;
        }

        // Single save for all printers
        private void btnSavePrinter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbxPrinter1.Text))
            {
                MessageBox.Show("من فضلك تاكد من بيانات طابعة الفواتير", "تاكيد");
                return;
            }

            Properties.Settings.Default.PrinterName1 = cbxPrinter1.Text;
            Properties.Settings.Default.PrinterName2 = cbxPrinter2.Text;
            Properties.Settings.Default.PrinterName3 = cbxPrinter3.Text;
            Properties.Settings.Default.Save();

            AppSettingsStore.Set("PrinterName1", cbxPrinter1.Text);
            AppSettingsStore.Set("PrinterName2", cbxPrinter2.Text);
            AppSettingsStore.Set("PrinterName3", cbxPrinter3.Text);

            MessageBox.Show("تم حفظ إعدادات الطابعات بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Log("SettingsChanged", "Settings", "Frm_Setting", "تعديل إعدادات الطابعات");
        }

        #endregion

        #region ── Tab 2: Invoice Settings ──

        private void LoadInvoiceSettings()
        {
            // Load from AppSettingsStore (was previously in SQL OrderPrintData table)
            txtName.Text = AppSettingsStore.Get("InvoiceName") ?? "";
            txtAddress.Text = AppSettingsStore.Get("InvoiceAddress") ?? "";
            txtDescription.Text = AppSettingsStore.Get("InvoiceDescription") ?? "";
            txtPhone1.Text = AppSettingsStore.Get("InvoicePhone1") ?? "";
            txtPhone2.Text = AppSettingsStore.Get("InvoicePhone2") ?? "";

            // Load logo from file
            try
            {
                string logoPath = GetLogoFilePath();
                if (File.Exists(logoPath))
                {
                    pictureLogo.BackgroundImageLayout = ImageLayout.Stretch;
                    using var fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read);
                    pictureLogo.BackgroundImage = Image.FromStream(fs);
                }
            }
            catch { }
        }

        private static string GetLogoFilePath()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCore");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "invoice_logo.png");
        }

        string imagePath = "";

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All Files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imagePath = dlg.FileName;
                pictureLogo.Image = null;
                pictureLogo.ImageLocation = imagePath;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            pictureLogo.BackgroundImage = null;
            pictureLogo.Image = null;
            imagePath = "";
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // Save text fields to AppSettingsStore
            AppSettingsStore.Set("InvoiceName", txtName.Text);
            AppSettingsStore.Set("InvoiceAddress", txtAddress.Text);
            AppSettingsStore.Set("InvoiceDescription", txtDescription.Text);
            AppSettingsStore.Set("InvoicePhone1", txtPhone1.Text);
            AppSettingsStore.Set("InvoicePhone2", txtPhone2.Text);

            // Save logo image to file
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    File.Copy(imagePath, GetLogoFilePath(), overwrite: true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ في حفظ الصورة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            imagePath = "";
            MessageBox.Show("تم حفظ بيانات الفاتورة بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Log("SettingsChanged", "Settings", "Frm_Setting", "تعديل بيانات الفاتورة");
        }

        #endregion

        #region ── Tab 3: General Settings ──

        private void LoadGeneralSettings()
        {
            // Discount type
            var discount = FirstNonEmpty(Properties.Settings.Default.ItemDiscount, AppSettingsStore.Get("ItemDiscount"));
            rbtnVale.Checked = discount == "Value";
            rbtnPresent.Checked = discount != "Value";

            // Print copies
            NudSaleNumber.EditValue = Properties.Settings.Default.SalesPrintNum > 0
                ? Properties.Settings.Default.SalesPrintNum : 1;
            NudBuyNumber.EditValue = Properties.Settings.Default.BuyPrintNum > 0
                ? Properties.Settings.Default.BuyPrintNum : 1;
            NudTameenNumber.EditValue = Properties.Settings.Default.TameenPrintNum > 0
                ? Properties.Settings.Default.TameenPrintNum : 1;

            // Checkboxes
            checkTaxes.Checked = Properties.Settings.Default.Taxes;
            checkdiscount.Checked = Properties.Settings.Default.SaleDiscountForCasher;
            checkSalePrint.Checked = Properties.Settings.Default.SalesPrint;
            checkBuyPrint.Checked = Properties.Settings.Default.BuyPrint;
            checkBuyMoney.Checked = Properties.Settings.Default.MoneyBuy;

            // Print type radio buttons
            var salePrintKind = FirstNonEmpty(Properties.Settings.Default.SalePrintKind, AppSettingsStore.Get("SalePrintKind"));
            rbtn8cmSales.Checked = salePrintKind != "A4";
            rbtnA4Sales.Checked = salePrintKind == "A4";

            var buyPrintKind = FirstNonEmpty(Properties.Settings.Default.BuyPrintKind, AppSettingsStore.Get("BuyPrintKind"));
            rbtn8cmBuy.Checked = buyPrintKind != "A4";
            rbtnA4Buy.Checked = buyPrintKind == "A4";

            var tameenPrintKind = FirstNonEmpty(Properties.Settings.Default.TameenPrintKind, AppSettingsStore.Get("TameenPrintKind"));
            rbtn8cmTameen.Checked = tameenPrintKind != "A4";
            rbtnA4Tameen.Checked = tameenPrintKind == "A4";
        }

        private void btnSaveGenralSetting_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(NudSaleNumber.EditValue) < 1 || Convert.ToDecimal(NudBuyNumber.EditValue) < 1)
            {
                MessageBox.Show("من فضلك لا يمكن ان يقل عدد الفواتير المطبوعه عن 1", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Discount type
            Properties.Settings.Default.ItemDiscount = rbtnVale.Checked ? "Value" : "Present";
            AppSettingsStore.Set("ItemDiscount", Properties.Settings.Default.ItemDiscount);

            // Print copies
            Properties.Settings.Default.SalesPrintNum = Convert.ToInt32(NudSaleNumber.EditValue);
            Properties.Settings.Default.BuyPrintNum = Convert.ToInt32(NudBuyNumber.EditValue);
            Properties.Settings.Default.TameenPrintNum = Convert.ToInt32(NudTameenNumber.EditValue);

            // Checkboxes
            Properties.Settings.Default.Taxes = checkTaxes.Checked;
            Properties.Settings.Default.SaleDiscountForCasher = checkdiscount.Checked;
            Properties.Settings.Default.SalesPrint = checkSalePrint.Checked;
            Properties.Settings.Default.BuyPrint = checkBuyPrint.Checked;
            Properties.Settings.Default.MoneyBuy = checkBuyMoney.Checked;

            // Print type
            Properties.Settings.Default.SalePrintKind = rbtnA4Sales.Checked ? "A4" : "8CM";
            Properties.Settings.Default.BuyPrintKind = rbtnA4Buy.Checked ? "A4" : "8CM";
            Properties.Settings.Default.TameenPrintKind = rbtnA4Tameen.Checked ? "A4" : "8CM";

            AppSettingsStore.Set("SalePrintKind", Properties.Settings.Default.SalePrintKind);
            AppSettingsStore.Set("BuyPrintKind", Properties.Settings.Default.BuyPrintKind);
            AppSettingsStore.Set("TameenPrintKind", Properties.Settings.Default.TameenPrintKind);

            Properties.Settings.Default.Save();
            MessageBox.Show("تم حفظ البيانات بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Log("SettingsChanged", "Settings", "Frm_Setting", "تعديل الإعدادات العامة");
        }

        #endregion

        #region ── Tab 4: Points System ──

        private void LoadPointsSettings()
        {
            NudPoint.EditValue = Properties.Settings.Default.Nud_Point > 0
                ? Properties.Settings.Default.Nud_Point : 1;
            NudChange.EditValue = Properties.Settings.Default.Nud_Change > 0
                ? Properties.Settings.Default.Nud_Change : 1;
            NudMin.EditValue = Properties.Settings.Default.Nud_Min > 0
                ? Properties.Settings.Default.Nud_Min : 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(NudPoint.EditValue) < 1 || Convert.ToDecimal(NudChange.EditValue) < 1)
            {
                MessageBox.Show("لا يمكن أن تكون قيم النقاط أو ما يعادلها من مبلغ مالي بصفر", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Properties.Settings.Default.Nud_Point = Convert.ToDecimal(NudPoint.EditValue);
            Properties.Settings.Default.Nud_Change = Convert.ToDecimal(NudChange.EditValue);
            Properties.Settings.Default.Nud_Min = Convert.ToDecimal(NudMin.EditValue);

            Properties.Settings.Default.Save();
            MessageBox.Show("تم حفظ البيانات بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AuditLogger.Log("SettingsChanged", "Settings", "Frm_Setting", "تعديل إعدادات النقاط");
        }

        #endregion

        #region ── Helpers ──

        private static string FirstNonEmpty(params string?[] vals)
        {
            foreach (var v in vals)
                if (!string.IsNullOrWhiteSpace(v)) return v!;
            return string.Empty;
        }

        #endregion

    }
}