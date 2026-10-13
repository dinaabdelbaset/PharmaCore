namespace PharmaCore.POS.PL
{
    partial class Frm_Products
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            pnlTopBar = new Panel();
            label1 = new Label();
            lblTitle = new Label();
            lblSearch = new Label();
            txtSearch = new TextBox();
            grpBasicData = new GroupBox();
            btnSetName = new Button();
            lblProductCode = new Label();
            txtProductCode = new TextBox();
            lblNameEn = new Label();
            txtNameEn = new TextBox();
            lblNameAr = new Label();
            txtNameAr = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblIngredient = new Label();
            cmbIngredient = new ComboBox();
            lblUsage = new Label();
            cmbUsage = new ComboBox();
            lblCompany = new Label();
            cmbCompany = new ComboBox();
            btnDetails = new Button();
            lblNotes = new Label();
            txtNotes = new TextBox();
            chkEnglishPrimary = new CheckBox();
            btnAddProduct = new Button();
            btnSaveProduct = new Button();
            btnDeleteProduct = new Button();
            btnNewProduct = new Button();
            pnlSidebar = new Panel();
            lstProducts = new ListBox();
            grpBatch = new GroupBox();
            btnSetUnit = new Button();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            btnGenBarcode = new Button();
            nudBarcodeLength = new NumericUpDown();
            lblExpiry = new Label();
            dtpExpiry = new DateTimePicker();
            lblBuyPrice = new Label();
            nudBuyPrice = new NumericUpDown();
            lblSalePrice = new Label();
            nudSalePrice = new NumericUpDown();
            chkTax = new CheckBox();
            nudTaxPercent = new NumericUpDown();
            lblPercent = new Label();
            lblPriceAfterTax = new Label();
            txtPriceAfterTax = new TextBox();
            lblMaxDiscount = new Label();
            nudMaxDiscount = new NumericUpDown();
            lblQty = new Label();
            nudQty = new NumericUpDown();
            lblWarehouse = new Label();
            cmbWarehouse = new ComboBox();
            lblMinAlert = new Label();
            nudMinAlert = new NumericUpDown();
            lblMainUnit = new Label();
            cmbMainUnit = new ComboBox();
            lblSaleUnit = new Label();
            cmbSaleUnit = new ComboBox();
            lblBuyUnit = new Label();
            cmbBuyUnit = new ComboBox();
            lblSubUnit = new Label();
            cmbSubUnit = new ComboBox();
            lblSubQty = new Label();
            nudSubQty = new NumericUpDown();
            btnAddUnit = new Button();
            dgvUnits = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            delCol = new DataGridViewButtonColumn();
            chkBonus = new CheckBox();
            nudBonusValue = new NumericUpDown();
            chkBonusPercent = new CheckBox();
            btnAddBatch = new Button();
            btnSaveBatch = new Button();
            grpPromo = new GroupBox();
            lstPromos = new ListBox();
            btnManagePromos = new Button();
            grpBranchStock = new GroupBox();
            dgvBranchStock = new DataGridView();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            btnLoadStock = new Button();
            grpStats = new GroupBox();
            lblStatSold = new Label();
            lblStatPurchased = new Label();
            lblStatReturned = new Label();
            grpBatches = new GroupBox();
            dgvBatches = new DataGridView();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            btnEditBatchFromGrid = new Button();
            btnPrintBarcode = new Button();
            btnDeleteBatch = new Button();
            lblTotalQty = new Label();
            txtTotalQty = new TextBox();
            btnHouses = new Button();
            pnlTopBar.SuspendLayout();
            grpBasicData.SuspendLayout();
            pnlSidebar.SuspendLayout();
            grpBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBarcodeLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBuyPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTaxPercent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMinAlert).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSubQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUnits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBonusValue).BeginInit();
            grpPromo.SuspendLayout();
            grpBranchStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBranchStock).BeginInit();
            grpStats.SuspendLayout();
            grpBatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBatches).BeginInit();
            SuspendLayout();
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.FromArgb(0, 100, 100);
            pnlTopBar.Controls.Add(label1);
            pnlTopBar.Controls.Add(lblTitle);
            pnlTopBar.Controls.Add(lblSearch);
            pnlTopBar.Controls.Add(txtSearch);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Size = new Size(1310, 50);
            pnlTopBar.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 10F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(812, 15);
            label1.Name = "label1";
            label1.Size = new Size(23, 17);
            label1.TabIndex = 41;
            label1.Text = "F1";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Tahoma", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(969, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(227, 23);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "إدارة المنتجات والباتشات";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Tahoma", 10F);
            lblSearch.ForeColor = Color.White;
            lblSearch.Location = new Point(766, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(40, 17);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "بحث:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Tahoma", 10F);
            txtSearch.Location = new Point(300, 12);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "رقم المنتج أو الباركود";
            txtSearch.Size = new Size(460, 24);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // grpBasicData
            // 
            grpBasicData.Controls.Add(btnSetName);
            grpBasicData.Controls.Add(lblProductCode);
            grpBasicData.Controls.Add(txtProductCode);
            grpBasicData.Controls.Add(lblNameEn);
            grpBasicData.Controls.Add(txtNameEn);
            grpBasicData.Controls.Add(lblNameAr);
            grpBasicData.Controls.Add(txtNameAr);
            grpBasicData.Controls.Add(lblCategory);
            grpBasicData.Controls.Add(cmbCategory);
            grpBasicData.Controls.Add(lblIngredient);
            grpBasicData.Controls.Add(cmbIngredient);
            grpBasicData.Controls.Add(lblUsage);
            grpBasicData.Controls.Add(cmbUsage);
            grpBasicData.Controls.Add(lblCompany);
            grpBasicData.Controls.Add(cmbCompany);
            grpBasicData.Controls.Add(btnDetails);
            grpBasicData.Controls.Add(lblNotes);
            grpBasicData.Controls.Add(txtNotes);
            grpBasicData.Controls.Add(chkEnglishPrimary);
            grpBasicData.Controls.Add(btnAddProduct);
            grpBasicData.Controls.Add(btnSaveProduct);
            grpBasicData.Controls.Add(btnDeleteProduct);
            grpBasicData.Controls.Add(btnNewProduct);
            grpBasicData.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            grpBasicData.ForeColor = Color.FromArgb(0, 100, 100);
            grpBasicData.Location = new Point(270, 65);
            grpBasicData.Name = "grpBasicData";
            grpBasicData.Size = new Size(274, 545);
            grpBasicData.TabIndex = 1;
            grpBasicData.TabStop = false;
            grpBasicData.Text = "البيانات الأساسية للمنتج";
            // 
            // btnSetName
            // 
            btnSetName.BackColor = Color.FromArgb(0, 140, 140);
            btnSetName.FlatStyle = FlatStyle.Flat;
            btnSetName.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnSetName.ForeColor = Color.White;
            btnSetName.Location = new Point(12, 16);
            btnSetName.Name = "btnSetName";
            btnSetName.Size = new Size(30, 25);
            btnSetName.TabIndex = 39;
            btnSetName.Text = "+";
            btnSetName.UseVisualStyleBackColor = false;
            // 
            // lblProductCode
            // 
            lblProductCode.AutoSize = true;
            lblProductCode.Font = new Font("Tahoma", 10F);
            lblProductCode.ForeColor = Color.Red;
            lblProductCode.Location = new Point(187, 25);
            lblProductCode.Name = "lblProductCode";
            lblProductCode.Size = new Size(74, 17);
            lblProductCode.TabIndex = 0;
            lblProductCode.Text = "رقم المنتج:";
            // 
            // txtProductCode
            // 
            txtProductCode.BackColor = Color.FromArgb(230, 230, 230);
            txtProductCode.Font = new Font("Tahoma", 10F);
            txtProductCode.Location = new Point(10, 45);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.ReadOnly = true;
            txtProductCode.Size = new Size(251, 24);
            txtProductCode.TabIndex = 1;
            txtProductCode.TextAlign = HorizontalAlignment.Center;
            // 
            // lblNameEn
            // 
            lblNameEn.AutoSize = true;
            lblNameEn.Font = new Font("Tahoma", 10F);
            lblNameEn.ForeColor = Color.Black;
            lblNameEn.Location = new Point(160, 75);
            lblNameEn.Name = "lblNameEn";
            lblNameEn.Size = new Size(101, 17);
            lblNameEn.TabIndex = 2;
            lblNameEn.Text = "اسم المنتج EN:";
            // 
            // txtNameEn
            // 
            txtNameEn.Font = new Font("Tahoma", 10F);
            txtNameEn.Location = new Point(10, 95);
            txtNameEn.Name = "txtNameEn";
            txtNameEn.Size = new Size(251, 24);
            txtNameEn.TabIndex = 3;
            // 
            // lblNameAr
            // 
            lblNameAr.AutoSize = true;
            lblNameAr.Font = new Font("Tahoma", 10F);
            lblNameAr.ForeColor = Color.Black;
            lblNameAr.Location = new Point(160, 125);
            lblNameAr.Name = "lblNameAr";
            lblNameAr.Size = new Size(101, 17);
            lblNameAr.TabIndex = 4;
            lblNameAr.Text = "اسم المنتج AR:";
            // 
            // txtNameAr
            // 
            txtNameAr.Font = new Font("Tahoma", 10F);
            txtNameAr.Location = new Point(10, 145);
            txtNameAr.Name = "txtNameAr";
            txtNameAr.Size = new Size(251, 24);
            txtNameAr.TabIndex = 5;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Tahoma", 10F);
            lblCategory.ForeColor = Color.Black;
            lblCategory.Location = new Point(191, 185);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(70, 17);
            lblCategory.TabIndex = 6;
            lblCategory.Text = "المجموعة:";
            // 
            // cmbCategory
            // 
            cmbCategory.Font = new Font("Tahoma", 10F);
            cmbCategory.Location = new Point(10, 205);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(251, 24);
            cmbCategory.TabIndex = 7;
            // 
            // lblIngredient
            // 
            lblIngredient.AutoSize = true;
            lblIngredient.Font = new Font("Tahoma", 10F);
            lblIngredient.ForeColor = Color.Black;
            lblIngredient.Location = new Point(173, 235);
            lblIngredient.Name = "lblIngredient";
            lblIngredient.Size = new Size(88, 17);
            lblIngredient.TabIndex = 8;
            lblIngredient.Text = "المادة الفعالة:";
            // 
            // cmbIngredient
            // 
            cmbIngredient.Font = new Font("Tahoma", 10F);
            cmbIngredient.Location = new Point(10, 255);
            cmbIngredient.Name = "cmbIngredient";
            cmbIngredient.Size = new Size(251, 24);
            cmbIngredient.TabIndex = 9;
            // 
            // lblUsage
            // 
            lblUsage.AutoSize = true;
            lblUsage.Font = new Font("Tahoma", 10F);
            lblUsage.ForeColor = Color.Black;
            lblUsage.Location = new Point(188, 285);
            lblUsage.Name = "lblUsage";
            lblUsage.Size = new Size(73, 17);
            lblUsage.TabIndex = 10;
            lblUsage.Text = "الاستخدام:";
            // 
            // cmbUsage
            // 
            cmbUsage.Font = new Font("Tahoma", 10F);
            cmbUsage.Location = new Point(10, 305);
            cmbUsage.Name = "cmbUsage";
            cmbUsage.Size = new Size(251, 24);
            cmbUsage.TabIndex = 11;
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Font = new Font("Tahoma", 10F);
            lblCompany.ForeColor = Color.Black;
            lblCompany.Location = new Point(174, 335);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(87, 17);
            lblCompany.TabIndex = 12;
            lblCompany.Text = "اسم الشركة:";
            // 
            // cmbCompany
            // 
            cmbCompany.Font = new Font("Tahoma", 10F);
            cmbCompany.Location = new Point(10, 354);
            cmbCompany.Name = "cmbCompany";
            cmbCompany.Size = new Size(251, 24);
            cmbCompany.TabIndex = 13;
            // 
            // btnDetails
            // 
            btnDetails.BackColor = Color.FromArgb(0, 140, 140);
            btnDetails.FlatStyle = FlatStyle.Flat;
            btnDetails.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnDetails.ForeColor = Color.White;
            btnDetails.Location = new Point(10, 175);
            btnDetails.Name = "btnDetails";
            btnDetails.Size = new Size(30, 26);
            btnDetails.TabIndex = 38;
            btnDetails.Text = "+";
            btnDetails.UseVisualStyleBackColor = false;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Tahoma", 10F);
            lblNotes.ForeColor = Color.Black;
            lblNotes.Location = new Point(197, 385);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(64, 17);
            lblNotes.TabIndex = 14;
            lblNotes.Text = "ملاحظات:";
            // 
            // txtNotes
            // 
            txtNotes.Font = new Font("Tahoma", 10F);
            txtNotes.Location = new Point(10, 405);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(251, 50);
            txtNotes.TabIndex = 15;
            // 
            // chkEnglishPrimary
            // 
            chkEnglishPrimary.AutoSize = true;
            chkEnglishPrimary.Checked = true;
            chkEnglishPrimary.CheckState = CheckState.Checked;
            chkEnglishPrimary.Font = new Font("Tahoma", 9F);
            chkEnglishPrimary.ForeColor = Color.Black;
            chkEnglishPrimary.Location = new Point(93, 461);
            chkEnglishPrimary.Name = "chkEnglishPrimary";
            chkEnglishPrimary.Size = new Size(166, 18);
            chkEnglishPrimary.TabIndex = 16;
            chkEnglishPrimary.Text = "الإسم الإنجليزي هو الأساس";
            // 
            // btnAddProduct
            // 
            btnAddProduct.BackColor = Color.FromArgb(0, 140, 100);
            btnAddProduct.FlatStyle = FlatStyle.Flat;
            btnAddProduct.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnAddProduct.ForeColor = Color.White;
            btnAddProduct.Location = new Point(203, 488);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(58, 32);
            btnAddProduct.TabIndex = 17;
            btnAddProduct.Text = "إضافة";
            btnAddProduct.UseVisualStyleBackColor = false;
            btnAddProduct.Click += btnAddProduct_Click;
            // 
            // btnSaveProduct
            // 
            btnSaveProduct.BackColor = Color.FromArgb(0, 100, 180);
            btnSaveProduct.FlatStyle = FlatStyle.Flat;
            btnSaveProduct.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnSaveProduct.ForeColor = Color.White;
            btnSaveProduct.Location = new Point(139, 488);
            btnSaveProduct.Name = "btnSaveProduct";
            btnSaveProduct.Size = new Size(58, 32);
            btnSaveProduct.TabIndex = 18;
            btnSaveProduct.Text = "حفظ";
            btnSaveProduct.UseVisualStyleBackColor = false;
            btnSaveProduct.Click += btnSaveProduct_Click;
            // 
            // btnDeleteProduct
            // 
            btnDeleteProduct.BackColor = Color.FromArgb(180, 40, 40);
            btnDeleteProduct.FlatStyle = FlatStyle.Flat;
            btnDeleteProduct.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnDeleteProduct.ForeColor = Color.White;
            btnDeleteProduct.Location = new Point(12, 488);
            btnDeleteProduct.Name = "btnDeleteProduct";
            btnDeleteProduct.Size = new Size(58, 32);
            btnDeleteProduct.TabIndex = 19;
            btnDeleteProduct.Text = "مسح";
            btnDeleteProduct.UseVisualStyleBackColor = false;
            btnDeleteProduct.Click += btnDeleteProduct_Click;
            // 
            // btnNewProduct
            // 
            btnNewProduct.BackColor = Color.DarkOrange;
            btnNewProduct.FlatStyle = FlatStyle.Flat;
            btnNewProduct.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnNewProduct.ForeColor = Color.White;
            btnNewProduct.Location = new Point(76, 488);
            btnNewProduct.Name = "btnNewProduct";
            btnNewProduct.Size = new Size(58, 32);
            btnNewProduct.TabIndex = 20;
            btnNewProduct.Text = "جــديــد";
            btnNewProduct.UseVisualStyleBackColor = false;
            btnNewProduct.Click += btnNewProduct_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(lstProducts);
            pnlSidebar.Location = new Point(12, 55);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(240, 765);
            pnlSidebar.TabIndex = 100;
            // 
            // lstProducts
            // 
            lstProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstProducts.BorderStyle = BorderStyle.None;
            lstProducts.Font = new Font("Tahoma", 11F);
            lstProducts.FormattingEnabled = true;
            lstProducts.ItemHeight = 18;
            lstProducts.Location = new Point(3, 5);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new Size(234, 756);
            lstProducts.TabIndex = 0;
            lstProducts.SelectedIndexChanged += lstProducts_SelectedIndexChanged;
            // 
            // grpBatch
            // 
            grpBatch.Controls.Add(btnHouses);
            grpBatch.Controls.Add(btnSetUnit);
            grpBatch.Controls.Add(lblBarcode);
            grpBatch.Controls.Add(txtBarcode);
            grpBatch.Controls.Add(btnGenBarcode);
            grpBatch.Controls.Add(nudBarcodeLength);
            grpBatch.Controls.Add(lblExpiry);
            grpBatch.Controls.Add(dtpExpiry);
            grpBatch.Controls.Add(lblBuyPrice);
            grpBatch.Controls.Add(nudBuyPrice);
            grpBatch.Controls.Add(lblSalePrice);
            grpBatch.Controls.Add(nudSalePrice);
            grpBatch.Controls.Add(chkTax);
            grpBatch.Controls.Add(nudTaxPercent);
            grpBatch.Controls.Add(lblPercent);
            grpBatch.Controls.Add(lblPriceAfterTax);
            grpBatch.Controls.Add(txtPriceAfterTax);
            grpBatch.Controls.Add(lblMaxDiscount);
            grpBatch.Controls.Add(nudMaxDiscount);
            grpBatch.Controls.Add(lblQty);
            grpBatch.Controls.Add(nudQty);
            grpBatch.Controls.Add(lblWarehouse);
            grpBatch.Controls.Add(cmbWarehouse);
            grpBatch.Controls.Add(lblMinAlert);
            grpBatch.Controls.Add(nudMinAlert);
            grpBatch.Controls.Add(lblMainUnit);
            grpBatch.Controls.Add(cmbMainUnit);
            grpBatch.Controls.Add(lblSaleUnit);
            grpBatch.Controls.Add(cmbSaleUnit);
            grpBatch.Controls.Add(lblBuyUnit);
            grpBatch.Controls.Add(cmbBuyUnit);
            grpBatch.Controls.Add(lblSubUnit);
            grpBatch.Controls.Add(cmbSubUnit);
            grpBatch.Controls.Add(lblSubQty);
            grpBatch.Controls.Add(nudSubQty);
            grpBatch.Controls.Add(btnAddUnit);
            grpBatch.Controls.Add(dgvUnits);
            grpBatch.Controls.Add(chkBonus);
            grpBatch.Controls.Add(nudBonusValue);
            grpBatch.Controls.Add(chkBonusPercent);
            grpBatch.Controls.Add(btnAddBatch);
            grpBatch.Controls.Add(btnSaveBatch);
            grpBatch.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            grpBatch.ForeColor = Color.FromArgb(0, 100, 100);
            grpBatch.Location = new Point(550, 65);
            grpBatch.Name = "grpBatch";
            grpBatch.Size = new Size(460, 545);
            grpBatch.TabIndex = 2;
            grpBatch.TabStop = false;
            grpBatch.Text = "بيانات الباتشة";
            // 
            // btnSetUnit
            // 
            btnSetUnit.BackColor = Color.FromArgb(0, 140, 140);
            btnSetUnit.FlatStyle = FlatStyle.Flat;
            btnSetUnit.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnSetUnit.ForeColor = Color.White;
            btnSetUnit.Location = new Point(411, 223);
            btnSetUnit.Name = "btnSetUnit";
            btnSetUnit.Size = new Size(30, 25);
            btnSetUnit.TabIndex = 41;
            btnSetUnit.Text = "+";
            btnSetUnit.UseVisualStyleBackColor = false;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Tahoma", 10F);
            lblBarcode.ForeColor = Color.Red;
            lblBarcode.Location = new Point(394, 25);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(54, 17);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "الباركود:";
            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Tahoma", 10F);
            txtBarcode.Location = new Point(274, 23);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(115, 24);
            txtBarcode.TabIndex = 1;
            // 
            // btnGenBarcode
            // 
            btnGenBarcode.BackColor = Color.FromArgb(0, 140, 140);
            btnGenBarcode.FlatStyle = FlatStyle.Flat;
            btnGenBarcode.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnGenBarcode.ForeColor = Color.White;
            btnGenBarcode.Location = new Point(242, 23);
            btnGenBarcode.Name = "btnGenBarcode";
            btnGenBarcode.Size = new Size(30, 25);
            btnGenBarcode.TabIndex = 2;
            btnGenBarcode.Text = "+";
            btnGenBarcode.UseVisualStyleBackColor = false;
            btnGenBarcode.Click += btnGenBarcode_Click;
            // 
            // nudBarcodeLength
            // 
            nudBarcodeLength.Font = new Font("Tahoma", 10F);
            nudBarcodeLength.Location = new Point(192, 23);
            nudBarcodeLength.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            nudBarcodeLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudBarcodeLength.Name = "nudBarcodeLength";
            nudBarcodeLength.Size = new Size(45, 24);
            nudBarcodeLength.TabIndex = 3;
            nudBarcodeLength.TextAlign = HorizontalAlignment.Center;
            nudBarcodeLength.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // lblExpiry
            // 
            lblExpiry.AutoSize = true;
            lblExpiry.Font = new Font("Tahoma", 10F);
            lblExpiry.ForeColor = Color.Black;
            lblExpiry.Location = new Point(121, 31);
            lblExpiry.Name = "lblExpiry";
            lblExpiry.Size = new Size(51, 17);
            lblExpiry.TabIndex = 3;
            lblExpiry.Text = "الانتهاء:";
            // 
            // dtpExpiry
            // 
            dtpExpiry.CustomFormat = "MM/yyyy";
            dtpExpiry.Font = new Font("Tahoma", 10F);
            dtpExpiry.Format = DateTimePickerFormat.Custom;
            dtpExpiry.Location = new Point(12, 25);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.RightToLeftLayout = true;
            dtpExpiry.ShowUpDown = true;
            dtpExpiry.Size = new Size(103, 24);
            dtpExpiry.TabIndex = 4;
            // 
            // lblBuyPrice
            // 
            lblBuyPrice.AutoSize = true;
            lblBuyPrice.Font = new Font("Tahoma", 10F);
            lblBuyPrice.ForeColor = Color.Black;
            lblBuyPrice.Location = new Point(317, 66);
            lblBuyPrice.Name = "lblBuyPrice";
            lblBuyPrice.Size = new Size(125, 17);
            lblBuyPrice.TabIndex = 5;
            lblBuyPrice.Text = "سعر شراء الصيدلي:";
            // 
            // nudBuyPrice
            // 
            nudBuyPrice.DecimalPlaces = 2;
            nudBuyPrice.Font = new Font("Tahoma", 10F);
            nudBuyPrice.Location = new Point(207, 64);
            nudBuyPrice.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudBuyPrice.Name = "nudBuyPrice";
            nudBuyPrice.Size = new Size(105, 24);
            nudBuyPrice.TabIndex = 6;
            nudBuyPrice.TextAlign = HorizontalAlignment.Center;
            // 
            // lblSalePrice
            // 
            lblSalePrice.AutoSize = true;
            lblSalePrice.Font = new Font("Tahoma", 10F);
            lblSalePrice.ForeColor = Color.Black;
            lblSalePrice.Location = new Point(146, 97);
            lblSalePrice.Name = "lblSalePrice";
            lblSalePrice.Size = new Size(120, 17);
            lblSalePrice.TabIndex = 7;
            lblSalePrice.Text = "سعر البيع للجمهور:";
            // 
            // nudSalePrice
            // 
            nudSalePrice.DecimalPlaces = 2;
            nudSalePrice.Font = new Font("Tahoma", 10F);
            nudSalePrice.Location = new Point(36, 95);
            nudSalePrice.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudSalePrice.Name = "nudSalePrice";
            nudSalePrice.Size = new Size(105, 24);
            nudSalePrice.TabIndex = 8;
            nudSalePrice.TextAlign = HorizontalAlignment.Center;
            nudSalePrice.ValueChanged += nudSalePrice_ValueChanged;
            // 
            // chkTax
            // 
            chkTax.AutoSize = true;
            chkTax.Font = new Font("Tahoma", 10F);
            chkTax.ForeColor = Color.Black;
            chkTax.Location = new Point(380, 127);
            chkTax.Name = "chkTax";
            chkTax.Size = new Size(61, 21);
            chkTax.TabIndex = 9;
            chkTax.Text = "ضريبة";
            chkTax.CheckedChanged += chkTax_CheckedChanged;
            // 
            // nudTaxPercent
            // 
            nudTaxPercent.Font = new Font("Tahoma", 10F);
            nudTaxPercent.Location = new Point(330, 127);
            nudTaxPercent.Name = "nudTaxPercent";
            nudTaxPercent.Size = new Size(45, 24);
            nudTaxPercent.TabIndex = 10;
            nudTaxPercent.TextAlign = HorizontalAlignment.Center;
            nudTaxPercent.ValueChanged += nudTaxPercent_ValueChanged;
            // 
            // lblPercent
            // 
            lblPercent.AutoSize = true;
            lblPercent.Font = new Font("Tahoma", 10F);
            lblPercent.Location = new Point(302, 128);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(22, 17);
            lblPercent.TabIndex = 11;
            lblPercent.Text = "%";
            // 
            // lblPriceAfterTax
            // 
            lblPriceAfterTax.AutoSize = true;
            lblPriceAfterTax.Font = new Font("Tahoma", 10F);
            lblPriceAfterTax.ForeColor = Color.Black;
            lblPriceAfterTax.Location = new Point(201, 130);
            lblPriceAfterTax.Name = "lblPriceAfterTax";
            lblPriceAfterTax.Size = new Size(77, 17);
            lblPriceAfterTax.TabIndex = 12;
            lblPriceAfterTax.Text = "بعد الضريبة:";
            // 
            // txtPriceAfterTax
            // 
            txtPriceAfterTax.BackColor = Color.FromArgb(230, 230, 230);
            txtPriceAfterTax.Font = new Font("Tahoma", 10F);
            txtPriceAfterTax.Location = new Point(96, 128);
            txtPriceAfterTax.Name = "txtPriceAfterTax";
            txtPriceAfterTax.ReadOnly = true;
            txtPriceAfterTax.Size = new Size(100, 24);
            txtPriceAfterTax.TabIndex = 13;
            txtPriceAfterTax.TextAlign = HorizontalAlignment.Center;
            // 
            // lblMaxDiscount
            // 
            lblMaxDiscount.AutoSize = true;
            lblMaxDiscount.Font = new Font("Tahoma", 10F);
            lblMaxDiscount.ForeColor = Color.Black;
            lblMaxDiscount.Location = new Point(69, 198);
            lblMaxDiscount.Name = "lblMaxDiscount";
            lblMaxDiscount.Size = new Size(86, 17);
            lblMaxDiscount.TabIndex = 14;
            lblMaxDiscount.Text = "أكبر خصم %:";
            // 
            // nudMaxDiscount
            // 
            nudMaxDiscount.Font = new Font("Tahoma", 10F);
            nudMaxDiscount.Location = new Point(9, 196);
            nudMaxDiscount.Name = "nudMaxDiscount";
            nudMaxDiscount.Size = new Size(60, 24);
            nudMaxDiscount.TabIndex = 15;
            nudMaxDiscount.TextAlign = HorizontalAlignment.Center;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Font = new Font("Tahoma", 10F);
            lblQty.ForeColor = Color.Black;
            lblQty.Location = new Point(369, 168);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(89, 17);
            lblQty.TabIndex = 16;
            lblQty.Text = "كمية الباتشة:";
            // 
            // nudQty
            // 
            nudQty.DecimalPlaces = 2;
            nudQty.Font = new Font("Tahoma", 10F);
            nudQty.Location = new Point(259, 166);
            nudQty.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudQty.Name = "nudQty";
            nudQty.Size = new Size(105, 24);
            nudQty.TabIndex = 17;
            nudQty.TextAlign = HorizontalAlignment.Center;
            // 
            // lblWarehouse
            // 
            lblWarehouse.AutoSize = true;
            lblWarehouse.Font = new Font("Tahoma", 10F);
            lblWarehouse.ForeColor = Color.Black;
            lblWarehouse.Location = new Point(199, 168);
            lblWarehouse.Name = "lblWarehouse";
            lblWarehouse.Size = new Size(54, 17);
            lblWarehouse.TabIndex = 18;
            lblWarehouse.Text = "المخزن:";
            // 
            // cmbWarehouse
            // 
            cmbWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWarehouse.Font = new Font("Tahoma", 10F);
            cmbWarehouse.Location = new Point(9, 166);
            cmbWarehouse.Name = "cmbWarehouse";
            cmbWarehouse.Size = new Size(185, 24);
            cmbWarehouse.TabIndex = 19;
            // 
            // lblMinAlert
            // 
            lblMinAlert.AutoSize = true;
            lblMinAlert.Font = new Font("Tahoma", 10F);
            lblMinAlert.ForeColor = Color.Black;
            lblMinAlert.Location = new Point(369, 198);
            lblMinAlert.Name = "lblMinAlert";
            lblMinAlert.Size = new Size(65, 17);
            lblMinAlert.TabIndex = 20;
            lblMinAlert.Text = "تنبيه عند:";
            // 
            // nudMinAlert
            // 
            nudMinAlert.DecimalPlaces = 2;
            nudMinAlert.Font = new Font("Tahoma", 10F);
            nudMinAlert.Location = new Point(259, 196);
            nudMinAlert.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudMinAlert.Name = "nudMinAlert";
            nudMinAlert.Size = new Size(105, 24);
            nudMinAlert.TabIndex = 21;
            nudMinAlert.TextAlign = HorizontalAlignment.Center;
            // 
            // lblMainUnit
            // 
            lblMainUnit.AutoSize = true;
            lblMainUnit.Font = new Font("Tahoma", 10F);
            lblMainUnit.ForeColor = Color.Black;
            lblMainUnit.Location = new Point(357, 251);
            lblMainUnit.Name = "lblMainUnit";
            lblMainUnit.Size = new Size(91, 17);
            lblMainUnit.TabIndex = 22;
            lblMainUnit.Text = "الوحدة الكبرى:";
            // 
            // cmbMainUnit
            // 
            cmbMainUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMainUnit.Font = new Font("Tahoma", 10F);
            cmbMainUnit.Location = new Point(207, 249);
            cmbMainUnit.Name = "cmbMainUnit";
            cmbMainUnit.Size = new Size(145, 24);
            cmbMainUnit.TabIndex = 23;
            // 
            // lblSaleUnit
            // 
            lblSaleUnit.AutoSize = true;
            lblSaleUnit.Font = new Font("Tahoma", 10F);
            lblSaleUnit.ForeColor = Color.Black;
            lblSaleUnit.Location = new Point(357, 281);
            lblSaleUnit.Name = "lblSaleUnit";
            lblSaleUnit.Size = new Size(72, 17);
            lblSaleUnit.TabIndex = 24;
            lblSaleUnit.Text = "وحدة البيع:";
            // 
            // cmbSaleUnit
            // 
            cmbSaleUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSaleUnit.Font = new Font("Tahoma", 10F);
            cmbSaleUnit.Location = new Point(207, 279);
            cmbSaleUnit.Name = "cmbSaleUnit";
            cmbSaleUnit.Size = new Size(145, 24);
            cmbSaleUnit.TabIndex = 25;
            // 
            // lblBuyUnit
            // 
            lblBuyUnit.AutoSize = true;
            lblBuyUnit.Font = new Font("Tahoma", 10F);
            lblBuyUnit.ForeColor = Color.Black;
            lblBuyUnit.Location = new Point(117, 251);
            lblBuyUnit.Name = "lblBuyUnit";
            lblBuyUnit.Size = new Size(83, 17);
            lblBuyUnit.TabIndex = 26;
            lblBuyUnit.Text = "وحدة الشراء:";
            // 
            // cmbBuyUnit
            // 
            cmbBuyUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBuyUnit.Font = new Font("Tahoma", 10F);
            cmbBuyUnit.Location = new Point(17, 249);
            cmbBuyUnit.Name = "cmbBuyUnit";
            cmbBuyUnit.Size = new Size(95, 24);
            cmbBuyUnit.TabIndex = 27;
            // 
            // lblSubUnit
            // 
            lblSubUnit.AutoSize = true;
            lblSubUnit.Font = new Font("Tahoma", 10F);
            lblSubUnit.ForeColor = Color.FromArgb(0, 100, 100);
            lblSubUnit.Location = new Point(369, 318);
            lblSubUnit.Name = "lblSubUnit";
            lblSubUnit.Size = new Size(80, 17);
            lblSubUnit.TabIndex = 28;
            lblSubUnit.Text = "وحدة فرعية:";
            // 
            // cmbSubUnit
            // 
            cmbSubUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSubUnit.Font = new Font("Tahoma", 10F);
            cmbSubUnit.Location = new Point(259, 316);
            cmbSubUnit.Name = "cmbSubUnit";
            cmbSubUnit.Size = new Size(105, 24);
            cmbSubUnit.TabIndex = 29;
            // 
            // lblSubQty
            // 
            lblSubQty.AutoSize = true;
            lblSubQty.Font = new Font("Tahoma", 10F);
            lblSubQty.ForeColor = Color.Black;
            lblSubQty.Location = new Point(109, 318);
            lblSubQty.Name = "lblSubQty";
            lblSubQty.Size = new Size(114, 17);
            lblSubQty.TabIndex = 30;
            lblSubQty.Text = "عددها في الكبرى:";
            // 
            // nudSubQty
            // 
            nudSubQty.Font = new Font("Tahoma", 10F);
            nudSubQty.Location = new Point(49, 316);
            nudSubQty.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudSubQty.Name = "nudSubQty";
            nudSubQty.Size = new Size(55, 24);
            nudSubQty.TabIndex = 31;
            nudSubQty.TextAlign = HorizontalAlignment.Center;
            // 
            // btnAddUnit
            // 
            btnAddUnit.BackColor = Color.FromArgb(0, 140, 140);
            btnAddUnit.FlatStyle = FlatStyle.Flat;
            btnAddUnit.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnAddUnit.ForeColor = Color.White;
            btnAddUnit.Location = new Point(9, 316);
            btnAddUnit.Name = "btnAddUnit";
            btnAddUnit.Size = new Size(40, 25);
            btnAddUnit.TabIndex = 32;
            btnAddUnit.Text = "➕";
            btnAddUnit.UseVisualStyleBackColor = false;
            btnAddUnit.Click += btnAddUnit_Click;
            // 
            // dgvUnits
            // 
            dgvUnits.AllowUserToAddRows = false;
            dgvUnits.AllowUserToDeleteRows = false;
            dgvUnits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUnits.BackgroundColor = Color.White;
            dgvUnits.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, delCol });
            dgvUnits.Font = new Font("Tahoma", 9F);
            dgvUnits.Location = new Point(10, 351);
            dgvUnits.Name = "dgvUnits";
            dgvUnits.ReadOnly = true;
            dgvUnits.RowHeadersVisible = false;
            dgvUnits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUnits.Size = new Size(440, 85);
            dgvUnits.TabIndex = 33;
            dgvUnits.CellContentClick += dgvUnits_CellContentClick;
            dgvUnits.CellDoubleClick += dgvUnits_CellDoubleClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "الوحدة";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "العدد";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "السعر";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // delCol
            // 
            delCol.HeaderText = "حذف";
            delCol.Name = "delCol";
            delCol.ReadOnly = true;
            delCol.Text = "✕";
            delCol.UseColumnTextForButtonValue = true;
            // 
            // chkBonus
            // 
            chkBonus.AutoSize = true;
            chkBonus.Font = new Font("Tahoma", 10F);
            chkBonus.ForeColor = Color.FromArgb(0, 100, 100);
            chkBonus.Location = new Point(362, 457);
            chkBonus.Name = "chkBonus";
            chkBonus.Size = new Size(89, 21);
            chkBonus.TabIndex = 34;
            chkBonus.Text = "بونص البائع";
            // 
            // nudBonusValue
            // 
            nudBonusValue.DecimalPlaces = 2;
            nudBonusValue.Font = new Font("Tahoma", 10F);
            nudBonusValue.Location = new Point(242, 455);
            nudBonusValue.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            nudBonusValue.Name = "nudBonusValue";
            nudBonusValue.Size = new Size(110, 24);
            nudBonusValue.TabIndex = 35;
            nudBonusValue.TextAlign = HorizontalAlignment.Center;
            // 
            // chkBonusPercent
            // 
            chkBonusPercent.AutoSize = true;
            chkBonusPercent.Font = new Font("Tahoma", 9F);
            chkBonusPercent.ForeColor = Color.Black;
            chkBonusPercent.Location = new Point(110, 457);
            chkBonusPercent.Name = "chkBonusPercent";
            chkBonusPercent.Size = new Size(98, 18);
            chkBonusPercent.TabIndex = 36;
            chkBonusPercent.Text = "نسبة مئوية %";
            // 
            // btnAddBatch
            // 
            btnAddBatch.BackColor = Color.FromArgb(0, 140, 100);
            btnAddBatch.FlatStyle = FlatStyle.Flat;
            btnAddBatch.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnAddBatch.ForeColor = Color.White;
            btnAddBatch.Location = new Point(240, 487);
            btnAddBatch.Name = "btnAddBatch";
            btnAddBatch.Size = new Size(210, 35);
            btnAddBatch.TabIndex = 37;
            btnAddBatch.Text = "➕ إضافة الباتشة";
            btnAddBatch.UseVisualStyleBackColor = false;
            btnAddBatch.Click += btnAddBatch_Click;
            // 
            // btnSaveBatch
            // 
            btnSaveBatch.BackColor = Color.FromArgb(0, 100, 180);
            btnSaveBatch.Enabled = false;
            btnSaveBatch.FlatStyle = FlatStyle.Flat;
            btnSaveBatch.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnSaveBatch.ForeColor = Color.White;
            btnSaveBatch.Location = new Point(10, 487);
            btnSaveBatch.Name = "btnSaveBatch";
            btnSaveBatch.Size = new Size(210, 35);
            btnSaveBatch.TabIndex = 38;
            btnSaveBatch.Text = "💾 حفظ تعديل الباتشة";
            btnSaveBatch.UseVisualStyleBackColor = false;
            btnSaveBatch.Click += btnSaveBatch_Click;
            // 
            // grpPromo
            // 
            grpPromo.Controls.Add(lstPromos);
            grpPromo.Controls.Add(btnManagePromos);
            grpPromo.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            grpPromo.ForeColor = Color.FromArgb(0, 100, 100);
            grpPromo.Location = new Point(1016, 65);
            grpPromo.Name = "grpPromo";
            grpPromo.Size = new Size(281, 175);
            grpPromo.TabIndex = 3;
            grpPromo.TabStop = false;
            grpPromo.Text = "العروض النشطة";
            // 
            // lstPromos
            // 
            lstPromos.Font = new Font("Tahoma", 9F);
            lstPromos.ItemHeight = 14;
            lstPromos.Location = new Point(5, 20);
            lstPromos.Name = "lstPromos";
            lstPromos.Size = new Size(268, 102);
            lstPromos.TabIndex = 0;
            // 
            // btnManagePromos
            // 
            btnManagePromos.BackColor = Color.FromArgb(180, 120, 0);
            btnManagePromos.FlatStyle = FlatStyle.Flat;
            btnManagePromos.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnManagePromos.ForeColor = Color.White;
            btnManagePromos.Location = new Point(5, 140);
            btnManagePromos.Name = "btnManagePromos";
            btnManagePromos.Size = new Size(268, 28);
            btnManagePromos.TabIndex = 1;
            btnManagePromos.Text = "إدارة العروض";
            btnManagePromos.UseVisualStyleBackColor = false;
            btnManagePromos.Click += btnManagePromos_Click;
            // 
            // grpBranchStock
            // 
            grpBranchStock.Controls.Add(dgvBranchStock);
            grpBranchStock.Controls.Add(btnLoadStock);
            grpBranchStock.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            grpBranchStock.ForeColor = Color.FromArgb(0, 100, 100);
            grpBranchStock.Location = new Point(1016, 245);
            grpBranchStock.Name = "grpBranchStock";
            grpBranchStock.Size = new Size(281, 240);
            grpBranchStock.TabIndex = 4;
            grpBranchStock.TabStop = false;
            grpBranchStock.Text = "مخزون الفروع";
            // 
            // dgvBranchStock
            // 
            dgvBranchStock.AllowUserToAddRows = false;
            dgvBranchStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBranchStock.BackgroundColor = Color.White;
            dgvBranchStock.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            dgvBranchStock.Font = new Font("Tahoma", 9F);
            dgvBranchStock.Location = new Point(5, 20);
            dgvBranchStock.Name = "dgvBranchStock";
            dgvBranchStock.ReadOnly = true;
            dgvBranchStock.RowHeadersVisible = false;
            dgvBranchStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBranchStock.Size = new Size(268, 180);
            dgvBranchStock.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "المنطقة";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "الفرع";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "الكمية";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // btnLoadStock
            // 
            btnLoadStock.BackColor = Color.FromArgb(0, 100, 180);
            btnLoadStock.FlatStyle = FlatStyle.Flat;
            btnLoadStock.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnLoadStock.ForeColor = Color.White;
            btnLoadStock.Location = new Point(5, 205);
            btnLoadStock.Name = "btnLoadStock";
            btnLoadStock.Size = new Size(268, 28);
            btnLoadStock.TabIndex = 1;
            btnLoadStock.Text = "🔄 عرض المخزون";
            btnLoadStock.UseVisualStyleBackColor = false;
            btnLoadStock.Click += btnLoadStock_Click;
            // 
            // grpStats
            // 
            grpStats.Controls.Add(lblStatSold);
            grpStats.Controls.Add(lblStatPurchased);
            grpStats.Controls.Add(lblStatReturned);
            grpStats.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            grpStats.ForeColor = Color.FromArgb(0, 100, 100);
            grpStats.Location = new Point(1016, 490);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(281, 120);
            grpStats.TabIndex = 5;
            grpStats.TabStop = false;
            grpStats.Text = "إحصائيات";
            // 
            // lblStatSold
            // 
            lblStatSold.AutoSize = true;
            lblStatSold.Font = new Font("Tahoma", 10F);
            lblStatSold.ForeColor = Color.Green;
            lblStatSold.Location = new Point(126, 34);
            lblStatSold.Name = "lblStatSold";
            lblStatSold.Size = new Size(120, 17);
            lblStatSold.TabIndex = 0;
            lblStatSold.Text = "إجمالي المبيعات: 0";
            // 
            // lblStatPurchased
            // 
            lblStatPurchased.AutoSize = true;
            lblStatPurchased.Font = new Font("Tahoma", 10F);
            lblStatPurchased.ForeColor = Color.Blue;
            lblStatPurchased.Location = new Point(113, 59);
            lblStatPurchased.Name = "lblStatPurchased";
            lblStatPurchased.Size = new Size(133, 17);
            lblStatPurchased.TabIndex = 1;
            lblStatPurchased.Text = "إجمالي المشتريات: 0";
            // 
            // lblStatReturned
            // 
            lblStatReturned.AutoSize = true;
            lblStatReturned.Font = new Font("Tahoma", 10F);
            lblStatReturned.ForeColor = Color.Red;
            lblStatReturned.Location = new Point(116, 84);
            lblStatReturned.Name = "lblStatReturned";
            lblStatReturned.Size = new Size(130, 17);
            lblStatReturned.TabIndex = 2;
            lblStatReturned.Text = "إجمالي المرتجعات: 0";
            // 
            // grpBatches
            // 
            grpBatches.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpBatches.Controls.Add(dgvBatches);
            grpBatches.Controls.Add(btnEditBatchFromGrid);
            grpBatches.Controls.Add(btnPrintBarcode);
            grpBatches.Controls.Add(btnDeleteBatch);
            grpBatches.Controls.Add(lblTotalQty);
            grpBatches.Controls.Add(txtTotalQty);
            grpBatches.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            grpBatches.ForeColor = Color.FromArgb(0, 100, 100);
            grpBatches.Location = new Point(265, 614);
            grpBatches.Name = "grpBatches";
            grpBatches.Size = new Size(1032, 205);
            grpBatches.TabIndex = 6;
            grpBatches.TabStop = false;
            grpBatches.Text = "باتشات المنتج";
            // 
            // dgvBatches
            // 
            dgvBatches.AllowUserToAddRows = false;
            dgvBatches.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBatches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBatches.BackgroundColor = Color.White;
            dgvBatches.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn12, dataGridViewTextBoxColumn13, dataGridViewTextBoxColumn14, dataGridViewTextBoxColumn15, dataGridViewTextBoxColumn16 });
            dgvBatches.Font = new Font("Tahoma", 9F);
            dgvBatches.Location = new Point(10, 20);
            dgvBatches.Name = "dgvBatches";
            dgvBatches.ReadOnly = true;
            dgvBatches.RowHeadersVisible = false;
            dgvBatches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBatches.Size = new Size(1010, 135);
            dgvBatches.TabIndex = 0;
            dgvBatches.CellClick += dgvBatches_CellClick;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "ID";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "الباركود";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "الانتهاء";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.HeaderText = "الكمية";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.HeaderText = "سعر الشراء";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.HeaderText = "سعر البيع";
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.HeaderText = "الضريبة%";
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewTextBoxColumn14.HeaderText = "بعد الضريبة";
            dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewTextBoxColumn15.HeaderText = "المخزن";
            dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewTextBoxColumn16.HeaderText = "البونص";
            dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            dataGridViewTextBoxColumn16.ReadOnly = true;
            // 
            // btnEditBatchFromGrid
            // 
            btnEditBatchFromGrid.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditBatchFromGrid.BackColor = Color.FromArgb(0, 100, 180);
            btnEditBatchFromGrid.FlatStyle = FlatStyle.Flat;
            btnEditBatchFromGrid.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnEditBatchFromGrid.ForeColor = Color.White;
            btnEditBatchFromGrid.Location = new Point(870, 162);
            btnEditBatchFromGrid.Name = "btnEditBatchFromGrid";
            btnEditBatchFromGrid.Size = new Size(150, 30);
            btnEditBatchFromGrid.TabIndex = 1;
            btnEditBatchFromGrid.Text = "📝 تعديل الباتشة";
            btnEditBatchFromGrid.UseVisualStyleBackColor = false;
            btnEditBatchFromGrid.Click += btnEditBatchFromGrid_Click;
            // 
            // btnPrintBarcode
            // 
            btnPrintBarcode.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrintBarcode.BackColor = Color.FromArgb(80, 80, 80);
            btnPrintBarcode.FlatStyle = FlatStyle.Flat;
            btnPrintBarcode.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnPrintBarcode.ForeColor = Color.White;
            btnPrintBarcode.Location = new Point(715, 162);
            btnPrintBarcode.Name = "btnPrintBarcode";
            btnPrintBarcode.Size = new Size(150, 30);
            btnPrintBarcode.TabIndex = 2;
            btnPrintBarcode.Text = "🖨 طباعة الباركود";
            btnPrintBarcode.UseVisualStyleBackColor = false;
            btnPrintBarcode.Click += btnPrintBarcode_Click;
            // 
            // btnDeleteBatch
            // 
            btnDeleteBatch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDeleteBatch.BackColor = Color.FromArgb(180, 40, 40);
            btnDeleteBatch.FlatStyle = FlatStyle.Flat;
            btnDeleteBatch.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnDeleteBatch.ForeColor = Color.White;
            btnDeleteBatch.Location = new Point(560, 162);
            btnDeleteBatch.Name = "btnDeleteBatch";
            btnDeleteBatch.Size = new Size(150, 30);
            btnDeleteBatch.TabIndex = 3;
            btnDeleteBatch.Text = "🗑 حذف الباتشة";
            btnDeleteBatch.UseVisualStyleBackColor = false;
            btnDeleteBatch.Click += btnDeleteBatch_Click;
            // 
            // lblTotalQty
            // 
            lblTotalQty.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotalQty.AutoSize = true;
            lblTotalQty.Font = new Font("Tahoma", 10F);
            lblTotalQty.ForeColor = Color.Black;
            lblTotalQty.Location = new Point(140, 167);
            lblTotalQty.Name = "lblTotalQty";
            lblTotalQty.Size = new Size(85, 17);
            lblTotalQty.TabIndex = 4;
            lblTotalQty.Text = "الكمية الكلية:";
            // 
            // txtTotalQty
            // 
            txtTotalQty.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtTotalQty.BackColor = Color.FromArgb(230, 230, 230);
            txtTotalQty.Font = new Font("Tahoma", 10F);
            txtTotalQty.Location = new Point(5, 164);
            txtTotalQty.Name = "txtTotalQty";
            txtTotalQty.ReadOnly = true;
            txtTotalQty.Size = new Size(130, 24);
            txtTotalQty.TabIndex = 5;
            txtTotalQty.TextAlign = HorizontalAlignment.Center;
            // 
            // btnHouses
            // 
            btnHouses.BackColor = Color.FromArgb(0, 140, 140);
            btnHouses.FlatStyle = FlatStyle.Flat;
            btnHouses.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            btnHouses.ForeColor = Color.White;
            btnHouses.Location = new Point(19, 135);
            btnHouses.Name = "btnHouses";
            btnHouses.Size = new Size(30, 25);
            btnHouses.TabIndex = 42;
            btnHouses.Text = "+";
            btnHouses.UseVisualStyleBackColor = false;
            // 
            // Frm_Products
            // 
            BackColor = Color.FromArgb(240, 244, 248);
            ClientSize = new Size(1310, 828);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            Controls.Add(grpBasicData);
            Controls.Add(grpBatch);
            Controls.Add(grpPromo);
            Controls.Add(grpBranchStock);
            Controls.Add(grpStats);
            Controls.Add(grpBatches);
            KeyPreview = true;
            Name = "Frm_Products";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إدارة المنتجات والباتشات";
            Load += Frm_Products_Load;
            KeyDown += Frm_Products_KeyDown;
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            grpBasicData.ResumeLayout(false);
            grpBasicData.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            grpBatch.ResumeLayout(false);
            grpBatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBarcodeLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBuyPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSalePrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTaxPercent).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMinAlert).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSubQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUnits).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBonusValue).EndInit();
            grpPromo.ResumeLayout(false);
            grpBranchStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBranchStock).EndInit();
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            grpBatches.ResumeLayout(false);
            grpBatches.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBatches).EndInit();
            ResumeLayout(false);
        }
        #endregion

        // TOP BAR
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.ListBox lstProducts;

        // RIGHT — Basic Data
        private System.Windows.Forms.GroupBox grpBasicData;
        private System.Windows.Forms.Label lblProductCode, lblNameEn, lblNameAr, lblCategory, lblIngredient, lblUsage, lblCompany, lblNotes;
        private System.Windows.Forms.TextBox txtProductCode, txtNameEn, txtNameAr, txtNotes;
        private System.Windows.Forms.ComboBox cmbCategory, cmbIngredient, cmbUsage, cmbCompany;
        private System.Windows.Forms.CheckBox chkEnglishPrimary;
        private System.Windows.Forms.Button btnAddProduct, btnSaveProduct, btnDeleteProduct, btnNewProduct, btnDetails;

        // MIDDLE — Batch Data
        private System.Windows.Forms.GroupBox grpBatch;
        private System.Windows.Forms.Label lblBarcode, lblExpiry, lblBuyPrice, lblSalePrice, lblPriceAfterTax, lblPercent;
        private System.Windows.Forms.Label lblMaxDiscount, lblQty, lblWarehouse, lblMinAlert;
        private System.Windows.Forms.Label lblMainUnit, lblSaleUnit, lblBuyUnit, lblSubUnit, lblSubQty;
        private System.Windows.Forms.TextBox txtBarcode, txtPriceAfterTax;
        private System.Windows.Forms.NumericUpDown nudBarcodeLength;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.NumericUpDown nudBuyPrice, nudSalePrice, nudTaxPercent, nudMaxDiscount;
        private System.Windows.Forms.NumericUpDown nudQty, nudMinAlert, nudSubQty, nudBonusValue;
        private System.Windows.Forms.ComboBox cmbWarehouse, cmbMainUnit, cmbSaleUnit, cmbBuyUnit, cmbSubUnit;
        private System.Windows.Forms.CheckBox chkTax, chkBonus, chkBonusPercent;
        private System.Windows.Forms.Button btnGenBarcode, btnAddUnit, btnAddBatch, btnSaveBatch;
        private System.Windows.Forms.DataGridView dgvUnits;

        // LEFT — Promos + Stock
        private System.Windows.Forms.GroupBox grpPromo, grpBranchStock, grpStats;
        private System.Windows.Forms.ListBox lstPromos;
        private System.Windows.Forms.Button btnManagePromos, btnLoadStock;
        private System.Windows.Forms.DataGridView dgvBranchStock;
        private System.Windows.Forms.Label lblStatSold, lblStatPurchased, lblStatReturned;

        // BOTTOM — Batches Grid
        private System.Windows.Forms.GroupBox grpBatches;
        private System.Windows.Forms.DataGridView dgvBatches;
        private System.Windows.Forms.Button btnDeleteBatch, btnPrintBarcode, btnEditBatchFromGrid;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.TextBox txtTotalQty;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewButtonColumn delCol;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private Button btnSetName;
        private Label label1;
        private Button btnSetUnit;
        private Button btnHouses;
    }
}