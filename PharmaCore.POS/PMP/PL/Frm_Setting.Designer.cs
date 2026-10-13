namespace PharmaCore.POS.PL
{
    partial class Frm_Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnSavePrinter1 = new Button();
            label12 = new Label();
            cbxPrinter3 = new ComboBox();
            label11 = new Label();
            cbxPrinter2 = new ComboBox();
            label5 = new Label();
            cbxPrinter1 = new ComboBox();
            tabPage2 = new TabPage();
            txtDescription = new TextBox();
            txtAddress = new DevExpress.XtraEditors.TextEdit();
            txtName = new DevExpress.XtraEditors.TextEdit();
            txtPhone1 = new DevExpress.XtraEditors.TextEdit();
            txtPhone2 = new DevExpress.XtraEditors.TextEdit();
            btnSaveOrder = new Button();
            label7 = new Label();
            label4 = new Label();
            label6 = new Label();
            label3 = new Label();
            label2 = new Label();
            btndelete = new DevExpress.XtraEditors.SimpleButton();
            btnChoose = new DevExpress.XtraEditors.SimpleButton();
            label1 = new Label();
            pictureLogo = new PictureBox();
            tabPage3 = new TabPage();
            btnSaveGenralSetting = new Button();
            NudTameenNumber = new DevExpress.XtraEditors.TextEdit();
            NudSaleNumber = new DevExpress.XtraEditors.TextEdit();
            NudBuyNumber = new DevExpress.XtraEditors.TextEdit();
            rbtnPresent = new RadioButton();
            rbtnVale = new RadioButton();
            label18 = new Label();
            groupBox3 = new GroupBox();
            rbtnA4Tameen = new RadioButton();
            rbtn8cmTameen = new RadioButton();
            checkBuyMoney = new CheckBox();
            groupBox2 = new GroupBox();
            rbtnA4Buy = new RadioButton();
            rbtn8cmBuy = new RadioButton();
            groupBox1 = new GroupBox();
            rbtnA4Sales = new RadioButton();
            rbtn8cmSales = new RadioButton();
            checkBuyPrint = new CheckBox();
            checkSalePrint = new CheckBox();
            checkdiscount = new CheckBox();
            label10 = new Label();
            label9 = new Label();
            checkTaxes = new CheckBox();
            label8 = new Label();
            tabPage4 = new TabPage();
            simpleButton1 = new Button();
            NudPoint = new DevExpress.XtraEditors.TextEdit();
            NudChange = new DevExpress.XtraEditors.TextEdit();
            NudMin = new DevExpress.XtraEditors.TextEdit();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            label13 = new Label();
            label14 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtAddress.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NudTameenNumber.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NudSaleNumber.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NudBuyNumber.Properties).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NudPoint.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NudChange.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NudMin.Properties).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.RightToLeftLayout = true;
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(848, 374);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnSavePrinter1);
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(cbxPrinter3);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(cbxPrinter2);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(cbxPrinter1);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(840, 343);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "اعدادت الطابعات";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSavePrinter1
            // 
            btnSavePrinter1.Location = new Point(290, 240);
            btnSavePrinter1.Name = "btnSavePrinter1";
            btnSavePrinter1.Size = new Size(260, 42);
            btnSavePrinter1.TabIndex = 289;
            btnSavePrinter1.Text = "حفظ إعدادات الطابعات";
            btnSavePrinter1.UseVisualStyleBackColor = true;
            btnSavePrinter1.Click += btnSavePrinter_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.Black;
            label12.Location = new Point(60, 100);
            label12.Name = "label12";
            label12.Size = new Size(186, 18);
            label12.TabIndex = 17;
            label12.Text = "اختر طابعة ورق ال A4 العادية\r\n";
            // 
            // cbxPrinter3
            // 
            cbxPrinter3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxPrinter3.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbxPrinter3.FormattingEnabled = true;
            cbxPrinter3.Location = new Point(35, 130);
            cbxPrinter3.Name = "cbxPrinter3";
            cbxPrinter3.Size = new Size(230, 26);
            cbxPrinter3.TabIndex = 16;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.Black;
            label11.Location = new Point(359, 100);
            label11.Name = "label11";
            label11.Size = new Size(120, 18);
            label11.TabIndex = 14;
            label11.Text = "اختر طابعة الباركود";
            // 
            // cbxPrinter2
            // 
            cbxPrinter2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxPrinter2.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbxPrinter2.FormattingEnabled = true;
            cbxPrinter2.Location = new Point(306, 130);
            cbxPrinter2.Name = "cbxPrinter2";
            cbxPrinter2.Size = new Size(230, 26);
            cbxPrinter2.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Black;
            label5.Location = new Point(633, 100);
            label5.Name = "label5";
            label5.Size = new Size(119, 18);
            label5.TabIndex = 11;
            label5.Text = "اختر طابعة الفواتير";
            // 
            // cbxPrinter1
            // 
            cbxPrinter1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxPrinter1.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbxPrinter1.FormattingEnabled = true;
            cbxPrinter1.Location = new Point(577, 130);
            cbxPrinter1.Name = "cbxPrinter1";
            cbxPrinter1.Size = new Size(230, 26);
            cbxPrinter1.TabIndex = 10;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txtDescription);
            tabPage2.Controls.Add(txtAddress);
            tabPage2.Controls.Add(txtName);
            tabPage2.Controls.Add(txtPhone1);
            tabPage2.Controls.Add(txtPhone2);
            tabPage2.Controls.Add(btnSaveOrder);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(btndelete);
            tabPage2.Controls.Add(btnChoose);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(pictureLogo);
            tabPage2.ForeColor = SystemColors.ActiveCaptionText;
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(840, 343);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "اعدادت الفاتورة";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(46, 34);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(194, 82);
            txtDescription.TabIndex = 294;
            // 
            // txtAddress
            // 
            txtAddress.EditValue = "";
            txtAddress.Location = new Point(489, 259);
            txtAddress.Name = "txtAddress";
            txtAddress.Properties.Appearance.BorderColor = Color.Gray;
            txtAddress.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtAddress.Properties.Appearance.Options.UseBorderColor = true;
            txtAddress.Properties.Appearance.Options.UseFont = true;
            txtAddress.Properties.Appearance.Options.UseTextOptions = true;
            txtAddress.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtAddress.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtAddress.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtAddress.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtAddress.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtAddress.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtAddress.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtAddress.Properties.AppearanceDisabled.Options.UseFont = true;
            txtAddress.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtAddress.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtAddress.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtAddress.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtAddress.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtAddress.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtAddress.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtAddress.Properties.AppearanceFocused.Options.UseFont = true;
            txtAddress.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtAddress.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtAddress.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtAddress.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtAddress.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtAddress.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtAddress.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtAddress.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtAddress.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtAddress.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtAddress.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtAddress.Properties.Name = "textEdit1";
            txtAddress.RightToLeft = RightToLeft.No;
            txtAddress.Size = new Size(228, 24);
            txtAddress.TabIndex = 293;
            // 
            // txtName
            // 
            txtName.EditValue = "";
            txtName.Location = new Point(489, 207);
            txtName.Name = "txtName";
            txtName.Properties.Appearance.BorderColor = Color.Gray;
            txtName.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtName.Properties.Appearance.Options.UseBorderColor = true;
            txtName.Properties.Appearance.Options.UseFont = true;
            txtName.Properties.Appearance.Options.UseTextOptions = true;
            txtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtName.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtName.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtName.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtName.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtName.Properties.AppearanceDisabled.Options.UseFont = true;
            txtName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtName.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtName.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtName.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtName.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtName.Properties.AppearanceFocused.Options.UseFont = true;
            txtName.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtName.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtName.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtName.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtName.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtName.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtName.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtName.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtName.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtName.Properties.Name = "textEdit1";
            txtName.RightToLeft = RightToLeft.No;
            txtName.Size = new Size(228, 24);
            txtName.TabIndex = 292;
            // 
            // txtPhone1
            // 
            txtPhone1.EditValue = "";
            txtPhone1.Location = new Point(46, 151);
            txtPhone1.Name = "txtPhone1";
            txtPhone1.Properties.Appearance.BorderColor = Color.Gray;
            txtPhone1.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtPhone1.Properties.Appearance.Options.UseBorderColor = true;
            txtPhone1.Properties.Appearance.Options.UseFont = true;
            txtPhone1.Properties.Appearance.Options.UseTextOptions = true;
            txtPhone1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtPhone1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone1.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtPhone1.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtPhone1.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtPhone1.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtPhone1.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtPhone1.Properties.AppearanceDisabled.Options.UseFont = true;
            txtPhone1.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtPhone1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone1.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtPhone1.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtPhone1.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtPhone1.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtPhone1.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtPhone1.Properties.AppearanceFocused.Options.UseFont = true;
            txtPhone1.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtPhone1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone1.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtPhone1.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtPhone1.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtPhone1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtPhone1.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtPhone1.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtPhone1.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtPhone1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtPhone1.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtPhone1.Properties.Name = "textEdit1";
            txtPhone1.RightToLeft = RightToLeft.No;
            txtPhone1.Size = new Size(194, 24);
            txtPhone1.TabIndex = 291;
            // 
            // txtPhone2
            // 
            txtPhone2.EditValue = "";
            txtPhone2.Location = new Point(46, 207);
            txtPhone2.Name = "txtPhone2";
            txtPhone2.Properties.Appearance.BorderColor = Color.Gray;
            txtPhone2.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtPhone2.Properties.Appearance.Options.UseBorderColor = true;
            txtPhone2.Properties.Appearance.Options.UseFont = true;
            txtPhone2.Properties.Appearance.Options.UseTextOptions = true;
            txtPhone2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtPhone2.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone2.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtPhone2.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtPhone2.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtPhone2.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtPhone2.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtPhone2.Properties.AppearanceDisabled.Options.UseFont = true;
            txtPhone2.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtPhone2.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone2.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtPhone2.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtPhone2.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtPhone2.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtPhone2.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtPhone2.Properties.AppearanceFocused.Options.UseFont = true;
            txtPhone2.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtPhone2.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone2.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtPhone2.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtPhone2.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtPhone2.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtPhone2.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtPhone2.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtPhone2.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtPhone2.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtPhone2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtPhone2.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtPhone2.Properties.Name = "textEdit1";
            txtPhone2.RightToLeft = RightToLeft.No;
            txtPhone2.Size = new Size(194, 24);
            txtPhone2.TabIndex = 290;
            // 
            // btnSaveOrder
            // 
            btnSaveOrder.Location = new Point(46, 262);
            btnSaveOrder.Name = "btnSaveOrder";
            btnSaveOrder.Size = new Size(194, 38);
            btnSaveOrder.TabIndex = 287;
            btnSaveOrder.Text = "حفظ";
            btnSaveOrder.UseVisualStyleBackColor = true;
            btnSaveOrder.Click += btnSaveOrder_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.Location = new Point(246, 210);
            label7.Name = "label7";
            label7.Size = new Size(65, 18);
            label7.TabIndex = 22;
            label7.Text = "تليفون 2:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(246, 154);
            label4.Name = "label4";
            label4.Size = new Size(60, 18);
            label4.TabIndex = 20;
            label4.Text = "تليفون1:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.Location = new Point(246, 67);
            label6.Name = "label6";
            label6.Size = new Size(163, 18);
            label6.TabIndex = 18;
            label6.Text = "جملة في اسفل الفاتورة:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(723, 262);
            label3.Name = "label3";
            label3.Size = new Size(105, 18);
            label3.TabIndex = 16;
            label3.Text = "عنوان الصيدلية:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(723, 207);
            label2.Name = "label2";
            label2.Size = new Size(99, 18);
            label2.TabIndex = 14;
            label2.Text = "اسم الصيدلية:";
            // 
            // btndelete
            // 
            btndelete.Appearance.BackColor = Color.FromArgb(41, 128, 185);
            btndelete.Appearance.BackColor2 = Color.FromArgb(41, 128, 185);
            btndelete.Appearance.BorderColor = Color.FromArgb(41, 128, 185);
            btndelete.Appearance.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btndelete.Appearance.ForeColor = Color.White;
            btndelete.Appearance.Options.UseBackColor = true;
            btndelete.Appearance.Options.UseBorderColor = true;
            btndelete.Appearance.Options.UseFont = true;
            btndelete.Appearance.Options.UseForeColor = true;
            btndelete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            btndelete.Location = new Point(489, 144);
            btndelete.Name = "btndelete";
            btndelete.Size = new Size(104, 38);
            btndelete.TabIndex = 12;
            btndelete.Text = "مسح الصورة";
            btndelete.Click += btndelete_Click;
            // 
            // btnChoose
            // 
            btnChoose.Appearance.BackColor = Color.FromArgb(41, 128, 185);
            btnChoose.Appearance.BackColor2 = Color.FromArgb(41, 128, 185);
            btnChoose.Appearance.BorderColor = Color.FromArgb(41, 128, 185);
            btnChoose.Appearance.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnChoose.Appearance.ForeColor = Color.White;
            btnChoose.Appearance.Options.UseBackColor = true;
            btnChoose.Appearance.Options.UseBorderColor = true;
            btnChoose.Appearance.Options.UseFont = true;
            btnChoose.Appearance.Options.UseForeColor = true;
            btnChoose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            btnChoose.Location = new Point(612, 144);
            btnChoose.Name = "btnChoose";
            btnChoose.Size = new Size(105, 38);
            btnChoose.TabIndex = 11;
            btnChoose.Text = "اختر صورة";
            btnChoose.Click += btnChoose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(724, 75);
            label1.Name = "label1";
            label1.Size = new Size(106, 18);
            label1.TabIndex = 10;
            label1.Text = " شعار الصيدلية:";
            // 
            // pictureLogo
            // 
            pictureLogo.BorderStyle = BorderStyle.FixedSingle;
            pictureLogo.Location = new Point(489, 49);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(228, 79);
            pictureLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureLogo.TabIndex = 0;
            pictureLogo.TabStop = false;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(btnSaveGenralSetting);
            tabPage3.Controls.Add(NudTameenNumber);
            tabPage3.Controls.Add(NudSaleNumber);
            tabPage3.Controls.Add(NudBuyNumber);
            tabPage3.Controls.Add(rbtnPresent);
            tabPage3.Controls.Add(rbtnVale);
            tabPage3.Controls.Add(label18);
            tabPage3.Controls.Add(groupBox3);
            tabPage3.Controls.Add(checkBuyMoney);
            tabPage3.Controls.Add(groupBox2);
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(checkBuyPrint);
            tabPage3.Controls.Add(checkSalePrint);
            tabPage3.Controls.Add(checkdiscount);
            tabPage3.Controls.Add(label10);
            tabPage3.Controls.Add(label9);
            tabPage3.Controls.Add(checkTaxes);
            tabPage3.Controls.Add(label8);
            tabPage3.Location = new Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(840, 343);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "اعدادت عامة";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSaveGenralSetting
            // 
            btnSaveGenralSetting.Location = new Point(77, 286);
            btnSaveGenralSetting.Name = "btnSaveGenralSetting";
            btnSaveGenralSetting.Size = new Size(194, 38);
            btnSaveGenralSetting.TabIndex = 293;
            btnSaveGenralSetting.Text = "حفظ بيانات الاعدادات";
            btnSaveGenralSetting.UseVisualStyleBackColor = true;
            btnSaveGenralSetting.Click += btnSaveGenralSetting_Click;
            // 
            // NudTameenNumber
            // 
            NudTameenNumber.EditValue = "";
            NudTameenNumber.Location = new Point(470, 166);
            NudTameenNumber.Name = "NudTameenNumber";
            NudTameenNumber.Properties.Appearance.BorderColor = Color.Gray;
            NudTameenNumber.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudTameenNumber.Properties.Appearance.Options.UseBorderColor = true;
            NudTameenNumber.Properties.Appearance.Options.UseFont = true;
            NudTameenNumber.Properties.Appearance.Options.UseTextOptions = true;
            NudTameenNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudTameenNumber.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudTameenNumber.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudTameenNumber.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudTameenNumber.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudTameenNumber.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudTameenNumber.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudTameenNumber.Properties.AppearanceDisabled.Options.UseFont = true;
            NudTameenNumber.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudTameenNumber.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudTameenNumber.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudTameenNumber.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudTameenNumber.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudTameenNumber.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudTameenNumber.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudTameenNumber.Properties.AppearanceFocused.Options.UseFont = true;
            NudTameenNumber.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudTameenNumber.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudTameenNumber.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudTameenNumber.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudTameenNumber.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudTameenNumber.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudTameenNumber.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudTameenNumber.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudTameenNumber.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudTameenNumber.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudTameenNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudTameenNumber.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudTameenNumber.Properties.Name = "textEdit1";
            NudTameenNumber.RightToLeft = RightToLeft.No;
            NudTameenNumber.Size = new Size(119, 24);
            NudTameenNumber.TabIndex = 292;
            // 
            // NudSaleNumber
            // 
            NudSaleNumber.EditValue = "";
            NudSaleNumber.Location = new Point(470, 124);
            NudSaleNumber.Name = "NudSaleNumber";
            NudSaleNumber.Properties.Appearance.BorderColor = Color.Gray;
            NudSaleNumber.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudSaleNumber.Properties.Appearance.Options.UseBorderColor = true;
            NudSaleNumber.Properties.Appearance.Options.UseFont = true;
            NudSaleNumber.Properties.Appearance.Options.UseTextOptions = true;
            NudSaleNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudSaleNumber.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudSaleNumber.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudSaleNumber.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudSaleNumber.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudSaleNumber.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudSaleNumber.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudSaleNumber.Properties.AppearanceDisabled.Options.UseFont = true;
            NudSaleNumber.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudSaleNumber.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudSaleNumber.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudSaleNumber.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudSaleNumber.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudSaleNumber.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudSaleNumber.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudSaleNumber.Properties.AppearanceFocused.Options.UseFont = true;
            NudSaleNumber.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudSaleNumber.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudSaleNumber.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudSaleNumber.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudSaleNumber.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudSaleNumber.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudSaleNumber.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudSaleNumber.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudSaleNumber.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudSaleNumber.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudSaleNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudSaleNumber.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudSaleNumber.Properties.Name = "textEdit1";
            NudSaleNumber.RightToLeft = RightToLeft.No;
            NudSaleNumber.Size = new Size(119, 24);
            NudSaleNumber.TabIndex = 291;
            // 
            // NudBuyNumber
            // 
            NudBuyNumber.EditValue = "";
            NudBuyNumber.Location = new Point(470, 84);
            NudBuyNumber.Name = "NudBuyNumber";
            NudBuyNumber.Properties.Appearance.BorderColor = Color.Gray;
            NudBuyNumber.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudBuyNumber.Properties.Appearance.Options.UseBorderColor = true;
            NudBuyNumber.Properties.Appearance.Options.UseFont = true;
            NudBuyNumber.Properties.Appearance.Options.UseTextOptions = true;
            NudBuyNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudBuyNumber.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudBuyNumber.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudBuyNumber.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudBuyNumber.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudBuyNumber.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudBuyNumber.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudBuyNumber.Properties.AppearanceDisabled.Options.UseFont = true;
            NudBuyNumber.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudBuyNumber.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudBuyNumber.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudBuyNumber.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudBuyNumber.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudBuyNumber.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudBuyNumber.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudBuyNumber.Properties.AppearanceFocused.Options.UseFont = true;
            NudBuyNumber.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudBuyNumber.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudBuyNumber.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudBuyNumber.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudBuyNumber.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudBuyNumber.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudBuyNumber.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudBuyNumber.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudBuyNumber.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudBuyNumber.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudBuyNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudBuyNumber.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudBuyNumber.Properties.Name = "textEdit1";
            NudBuyNumber.RightToLeft = RightToLeft.No;
            NudBuyNumber.Size = new Size(119, 24);
            NudBuyNumber.TabIndex = 290;
            // 
            // rbtnPresent
            // 
            rbtnPresent.AutoSize = true;
            rbtnPresent.Checked = true;
            rbtnPresent.Location = new Point(666, 51);
            rbtnPresent.Name = "rbtnPresent";
            rbtnPresent.Size = new Size(93, 22);
            rbtnPresent.TabIndex = 276;
            rbtnPresent.TabStop = true;
            rbtnPresent.Text = "نسبة مؤية";
            rbtnPresent.UseVisualStyleBackColor = true;
            // 
            // rbtnVale
            // 
            rbtnVale.AutoSize = true;
            rbtnVale.Location = new Point(470, 51);
            rbtnVale.Name = "rbtnVale";
            rbtnVale.Size = new Size(115, 22);
            rbtnVale.TabIndex = 275;
            rbtnVale.TabStop = true;
            rbtnVale.Text = "قيمة من المال";
            rbtnVale.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ForeColor = Color.Red;
            label18.Location = new Point(594, 169);
            label18.Name = "label18";
            label18.Size = new Size(213, 18);
            label18.TabIndex = 159;
            label18.Text = "عدد طباعة فواتير مبيعات التأمين :";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rbtnA4Tameen);
            groupBox3.Controls.Add(rbtn8cmTameen);
            groupBox3.Location = new Point(23, 196);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(329, 79);
            groupBox3.TabIndex = 39;
            groupBox3.TabStop = false;
            groupBox3.Text = "طباعة فواتير التأمينات";
            // 
            // rbtnA4Tameen
            // 
            rbtnA4Tameen.AutoSize = true;
            rbtnA4Tameen.Location = new Point(15, 33);
            rbtnA4Tameen.Name = "rbtnA4Tameen";
            rbtnA4Tameen.Size = new Size(85, 22);
            rbtnA4Tameen.TabIndex = 287;
            rbtnA4Tameen.Text = "طباعة A4";
            rbtnA4Tameen.UseVisualStyleBackColor = true;
            // 
            // rbtn8cmTameen
            // 
            rbtn8cmTameen.AutoSize = true;
            rbtn8cmTameen.Checked = true;
            rbtn8cmTameen.Location = new Point(139, 33);
            rbtn8cmTameen.Name = "rbtn8cmTameen";
            rbtn8cmTameen.Size = new Size(176, 22);
            rbtn8cmTameen.TabIndex = 284;
            rbtn8cmTameen.TabStop = true;
            rbtn8cmTameen.Text = "طباعة حراري (8 سنتي)";
            rbtn8cmTameen.UseVisualStyleBackColor = true;
            // 
            // checkBuyMoney
            // 
            checkBuyMoney.AutoSize = true;
            checkBuyMoney.Location = new Point(485, 311);
            checkBuyMoney.Name = "checkBuyMoney";
            checkBuyMoney.Size = new Size(318, 22);
            checkBuyMoney.TabIndex = 40;
            checkBuyMoney.Text = "تفعيل التأكد من مبلغ الخزنة قبل عمليات الشراء";
            checkBuyMoney.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rbtnA4Buy);
            groupBox2.Controls.Add(rbtn8cmBuy);
            groupBox2.Location = new Point(23, 110);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(329, 76);
            groupBox2.TabIndex = 39;
            groupBox2.TabStop = false;
            groupBox2.Text = "طباعة فواتير المشتريات";
            // 
            // rbtnA4Buy
            // 
            rbtnA4Buy.AutoSize = true;
            rbtnA4Buy.Location = new Point(15, 34);
            rbtnA4Buy.Name = "rbtnA4Buy";
            rbtnA4Buy.Size = new Size(85, 22);
            rbtnA4Buy.TabIndex = 282;
            rbtnA4Buy.Text = "طباعة A4";
            rbtnA4Buy.UseVisualStyleBackColor = true;
            // 
            // rbtn8cmBuy
            // 
            rbtn8cmBuy.AutoSize = true;
            rbtn8cmBuy.Checked = true;
            rbtn8cmBuy.Location = new Point(139, 34);
            rbtn8cmBuy.Name = "rbtn8cmBuy";
            rbtn8cmBuy.Size = new Size(176, 22);
            rbtn8cmBuy.TabIndex = 285;
            rbtn8cmBuy.TabStop = true;
            rbtn8cmBuy.Text = "طباعة حراري (8 سنتي)";
            rbtn8cmBuy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbtnA4Sales);
            groupBox1.Controls.Add(rbtn8cmSales);
            groupBox1.Location = new Point(23, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(329, 75);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "طباعة فواتير المبيعات";
            // 
            // rbtnA4Sales
            // 
            rbtnA4Sales.AutoSize = true;
            rbtnA4Sales.Location = new Point(15, 34);
            rbtnA4Sales.Name = "rbtnA4Sales";
            rbtnA4Sales.Size = new Size(85, 22);
            rbtnA4Sales.TabIndex = 283;
            rbtnA4Sales.Text = "طباعة A4";
            rbtnA4Sales.UseVisualStyleBackColor = true;
            // 
            // rbtn8cmSales
            // 
            rbtn8cmSales.AutoSize = true;
            rbtn8cmSales.Checked = true;
            rbtn8cmSales.Location = new Point(139, 34);
            rbtn8cmSales.Name = "rbtn8cmSales";
            rbtn8cmSales.Size = new Size(176, 22);
            rbtn8cmSales.TabIndex = 286;
            rbtn8cmSales.TabStop = true;
            rbtn8cmSales.Text = "طباعة حراري (8 سنتي)";
            rbtn8cmSales.UseVisualStyleBackColor = true;
            // 
            // checkBuyPrint
            // 
            checkBuyPrint.AutoSize = true;
            checkBuyPrint.Location = new Point(593, 281);
            checkBuyPrint.Name = "checkBuyPrint";
            checkBuyPrint.Size = new Size(210, 22);
            checkBuyPrint.TabIndex = 35;
            checkBuyPrint.Text = "تفعيل طباعة فواتير المشتريات";
            checkBuyPrint.UseVisualStyleBackColor = true;
            // 
            // checkSalePrint
            // 
            checkSalePrint.AutoSize = true;
            checkSalePrint.Location = new Point(607, 251);
            checkSalePrint.Name = "checkSalePrint";
            checkSalePrint.Size = new Size(196, 22);
            checkSalePrint.TabIndex = 34;
            checkSalePrint.Text = "تفعيل طباعة فواتير المبيعات";
            checkSalePrint.UseVisualStyleBackColor = true;
            // 
            // checkdiscount
            // 
            checkdiscount.AutoSize = true;
            checkdiscount.Location = new Point(470, 221);
            checkdiscount.Name = "checkdiscount";
            checkdiscount.Size = new Size(333, 22);
            checkdiscount.TabIndex = 33;
            checkdiscount.Text = "تفعيل امكانيه الخصم على فواتير المبيعات للكاشير";
            checkdiscount.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.Red;
            label10.Location = new Point(594, 87);
            label10.Name = "label10";
            label10.Size = new Size(229, 18);
            label10.TabIndex = 32;
            label10.Text = "عدد طباعة نسخه فواتير المشتريات:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Red;
            label9.Location = new Point(595, 127);
            label9.Name = "label9";
            label9.Size = new Size(215, 18);
            label9.TabIndex = 30;
            label9.Text = "عدد طباعة نسخه فواتير المبيعات:";
            // 
            // checkTaxes
            // 
            checkTaxes.AutoSize = true;
            checkTaxes.Location = new Point(602, 191);
            checkTaxes.Name = "checkTaxes";
            checkTaxes.Size = new Size(201, 22);
            checkTaxes.TabIndex = 27;
            checkTaxes.Text = "تفعيل ضريبة القيمة المضافه";
            checkTaxes.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Red;
            label8.Location = new Point(388, 20);
            label8.Name = "label8";
            label8.Size = new Size(423, 18);
            label8.TabIndex = 24;
            label8.Text = "هل تريد ان يكون الخصم على المنتج نسبة مؤيه ام قيمة من المال؟";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(simpleButton1);
            tabPage4.Controls.Add(NudPoint);
            tabPage4.Controls.Add(NudChange);
            tabPage4.Controls.Add(NudMin);
            tabPage4.Controls.Add(label17);
            tabPage4.Controls.Add(label16);
            tabPage4.Controls.Add(label15);
            tabPage4.Controls.Add(label13);
            tabPage4.Controls.Add(label14);
            tabPage4.Location = new Point(4, 27);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(840, 343);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "نظام النقاط";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new Point(284, 257);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(194, 38);
            simpleButton1.TabIndex = 291;
            simpleButton1.Text = "حفظ إعدادات نظام النقاط";
            simpleButton1.UseVisualStyleBackColor = true;
            simpleButton1.Click += simpleButton1_Click;
            // 
            // NudPoint
            // 
            NudPoint.EditValue = "";
            NudPoint.Location = new Point(590, 84);
            NudPoint.Name = "NudPoint";
            NudPoint.Properties.Appearance.BorderColor = Color.Gray;
            NudPoint.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudPoint.Properties.Appearance.Options.UseBorderColor = true;
            NudPoint.Properties.Appearance.Options.UseFont = true;
            NudPoint.Properties.Appearance.Options.UseTextOptions = true;
            NudPoint.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudPoint.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudPoint.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudPoint.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudPoint.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudPoint.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudPoint.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudPoint.Properties.AppearanceDisabled.Options.UseFont = true;
            NudPoint.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudPoint.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudPoint.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudPoint.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudPoint.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudPoint.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudPoint.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudPoint.Properties.AppearanceFocused.Options.UseFont = true;
            NudPoint.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudPoint.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudPoint.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudPoint.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudPoint.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudPoint.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudPoint.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudPoint.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudPoint.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudPoint.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudPoint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudPoint.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudPoint.Properties.Name = "textEdit1";
            NudPoint.RightToLeft = RightToLeft.No;
            NudPoint.Size = new Size(142, 24);
            NudPoint.TabIndex = 290;
            // 
            // NudChange
            // 
            NudChange.EditValue = "";
            NudChange.Location = new Point(272, 84);
            NudChange.Name = "NudChange";
            NudChange.Properties.Appearance.BorderColor = Color.Gray;
            NudChange.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudChange.Properties.Appearance.Options.UseBorderColor = true;
            NudChange.Properties.Appearance.Options.UseFont = true;
            NudChange.Properties.Appearance.Options.UseTextOptions = true;
            NudChange.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudChange.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudChange.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudChange.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudChange.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudChange.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudChange.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudChange.Properties.AppearanceDisabled.Options.UseFont = true;
            NudChange.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudChange.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudChange.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudChange.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudChange.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudChange.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudChange.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudChange.Properties.AppearanceFocused.Options.UseFont = true;
            NudChange.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudChange.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudChange.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudChange.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudChange.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudChange.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudChange.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudChange.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudChange.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudChange.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudChange.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudChange.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudChange.Properties.Name = "textEdit1";
            NudChange.RightToLeft = RightToLeft.No;
            NudChange.Size = new Size(142, 24);
            NudChange.TabIndex = 288;
            // 
            // NudMin
            // 
            NudMin.EditValue = "";
            NudMin.Location = new Point(272, 158);
            NudMin.Name = "NudMin";
            NudMin.Properties.Appearance.BorderColor = Color.Gray;
            NudMin.Properties.Appearance.Font = new Font("Tahoma", 11F);
            NudMin.Properties.Appearance.Options.UseBorderColor = true;
            NudMin.Properties.Appearance.Options.UseFont = true;
            NudMin.Properties.Appearance.Options.UseTextOptions = true;
            NudMin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            NudMin.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudMin.Properties.AppearanceDisabled.BackColor = Color.Silver;
            NudMin.Properties.AppearanceDisabled.BorderColor = Color.Black;
            NudMin.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            NudMin.Properties.AppearanceDisabled.Options.UseBackColor = true;
            NudMin.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            NudMin.Properties.AppearanceDisabled.Options.UseFont = true;
            NudMin.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            NudMin.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudMin.Properties.AppearanceFocused.BackColor = Color.Cyan;
            NudMin.Properties.AppearanceFocused.BorderColor = Color.Teal;
            NudMin.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            NudMin.Properties.AppearanceFocused.Options.UseBackColor = true;
            NudMin.Properties.AppearanceFocused.Options.UseBorderColor = true;
            NudMin.Properties.AppearanceFocused.Options.UseFont = true;
            NudMin.Properties.AppearanceFocused.Options.UseTextOptions = true;
            NudMin.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudMin.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            NudMin.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            NudMin.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            NudMin.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            NudMin.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            NudMin.Properties.AppearanceReadOnly.Options.UseFont = true;
            NudMin.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            NudMin.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            NudMin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            NudMin.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            NudMin.Properties.Name = "textEdit1";
            NudMin.RightToLeft = RightToLeft.No;
            NudMin.Size = new Size(142, 24);
            NudMin.TabIndex = 287;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Tahoma", 12F);
            label17.ForeColor = Color.Black;
            label17.ImeMode = ImeMode.NoControl;
            label17.Location = new Point(166, 160);
            label17.Name = "label17";
            label17.Size = new Size(97, 19);
            label17.TabIndex = 131;
            label17.Text = "جنيه مصري .";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Tahoma", 12F);
            label16.ForeColor = Color.Black;
            label16.ImeMode = ImeMode.NoControl;
            label16.Location = new Point(420, 160);
            label16.Name = "label16";
            label16.Size = new Size(411, 19);
            label16.TabIndex = 129;
            label16.Text = "الحد الأدنى من إجمالي المشتريات لبدأ نظام النقاط للعميل :\r\n";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Tahoma", 12F);
            label15.ForeColor = Color.Black;
            label15.ImeMode = ImeMode.NoControl;
            label15.Location = new Point(166, 83);
            label15.Name = "label15";
            label15.Size = new Size(97, 19);
            label15.TabIndex = 128;
            label15.Text = "جنيه مصري .";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Tahoma", 12F);
            label13.ForeColor = Color.Black;
            label13.ImeMode = ImeMode.NoControl;
            label13.Location = new Point(420, 83);
            label13.Name = "label13";
            label13.Size = new Size(164, 19);
            label13.TabIndex = 118;
            label13.Text = "نقطة ، يتم تحويلها إلى ";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Tahoma", 12F);
            label14.ForeColor = Color.Black;
            label14.ImeMode = ImeMode.NoControl;
            label14.Location = new Point(751, 83);
            label14.Name = "label14";
            label14.Size = new Size(29, 19);
            label14.TabIndex = 116;
            label14.Text = "كل";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Frm_Setting
            // 
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(848, 374);
            Controls.Add(tabControl1);
            Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Frm_Setting";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "اعدادت البرنامج";
            Load += Frm_Setting_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtAddress.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPhone2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NudTameenNumber.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NudSaleNumber.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NudBuyNumber.Properties).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NudPoint.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NudChange.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)NudMin.Properties).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxPrinter1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btndelete;
        private DevExpress.XtraEditors.SimpleButton btnChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBuyPrint;
        private System.Windows.Forms.CheckBox checkSalePrint;
        private System.Windows.Forms.CheckBox checkdiscount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkTaxes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbxPrinter3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxPrinter2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBuyMoney;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnSavePrinter1;
        private System.Windows.Forms.Button btnSaveOrder;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtPhone1;
        private DevExpress.XtraEditors.TextEdit txtPhone2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button simpleButton1;
        private DevExpress.XtraEditors.TextEdit NudPoint;
        private DevExpress.XtraEditors.TextEdit NudChange;
        private DevExpress.XtraEditors.TextEdit NudMin;
        private System.Windows.Forms.RadioButton rbtnPresent;
        private System.Windows.Forms.RadioButton rbtnVale;
        private System.Windows.Forms.RadioButton rbtnA4Tameen;
        private System.Windows.Forms.RadioButton rbtn8cmTameen;
        private System.Windows.Forms.RadioButton rbtnA4Buy;
        private System.Windows.Forms.RadioButton rbtn8cmBuy;
        private System.Windows.Forms.RadioButton rbtnA4Sales;
        private System.Windows.Forms.RadioButton rbtn8cmSales;
        private DevExpress.XtraEditors.TextEdit NudTameenNumber;
        private DevExpress.XtraEditors.TextEdit NudSaleNumber;
        private DevExpress.XtraEditors.TextEdit NudBuyNumber;
        private System.Windows.Forms.Button btnSaveGenralSetting;
    }
}