namespace PharmaCore.POS.PL
{
    partial class Frm_BuyReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DgvDteils = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.checkOrderNumber = new DevExpress.XtraEditors.CheckEdit();
            this.rbtnAllSup = new System.Windows.Forms.RadioButton();
            this.rbtnOneSupplier = new System.Windows.Forms.RadioButton();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPtintOrder = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalTax = new DevExpress.XtraEditors.TextEdit();
            this.txtOrderNumber = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrderNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(704, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 18);
            this.label4.TabIndex = 42;
            this.label4.Text = "اجمالي مبالغ المشتريات:";
            // 
            // DgvSearch
            // 
            this.DgvSearch.AllowUserToAddRows = false;
            this.DgvSearch.AllowUserToDeleteRows = false;
            this.DgvSearch.AllowUserToResizeColumns = false;
            this.DgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvSearch.BackgroundColor = System.Drawing.Color.White;
            this.DgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgvSearch.Location = new System.Drawing.Point(83, 132);
            this.DgvSearch.MultiSelect = false;
            this.DgvSearch.Name = "DgvSearch";
            this.DgvSearch.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(921, 191);
            this.DgvSearch.TabIndex = 39;
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(249, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "الى:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(417, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 18);
            this.label3.TabIndex = 31;
            this.label3.Text = "من:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOrderNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxSupplier);
            this.groupBox1.Controls.Add(this.rbtnAllSup);
            this.groupBox1.Controls.Add(this.checkOrderNumber);
            this.groupBox1.Controls.Add(this.DtpTo);
            this.groupBox1.Controls.Add(this.DtpFrom);
            this.groupBox1.Controls.Add(this.rbtnOneSupplier);
            this.groupBox1.Location = new System.Drawing.Point(21, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1049, 80);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(677, 35);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(152, 26);
            this.cbxSupplier.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(395, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(300, 23);
            this.label2.TabIndex = 36;
            this.label2.Text = "تقارير المشتريات فى فترة محددة";
            // 
            // DgvDteils
            // 
            this.DgvDteils.AllowUserToAddRows = false;
            this.DgvDteils.AllowUserToDeleteRows = false;
            this.DgvDteils.AllowUserToResizeColumns = false;
            this.DgvDteils.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvDteils.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvDteils.BackgroundColor = System.Drawing.Color.White;
            this.DgvDteils.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDteils.DefaultCellStyle = dataGridViewCellStyle7;
            this.DgvDteils.Location = new System.Drawing.Point(21, 394);
            this.DgvDteils.MultiSelect = false;
            this.DgvDteils.Name = "DgvDteils";
            this.DgvDteils.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvDteils.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DgvDteils.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvDteils.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDteils.Size = new System.Drawing.Size(1049, 191);
            this.DgvDteils.TabIndex = 175;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(753, 606);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 18);
            this.label5.TabIndex = 176;
            this.label5.Text = "اجمالي الضريبة :";
            // 
            // DtpFrom
            // 
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFrom.Location = new System.Drawing.Point(291, 34);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DtpFrom.Size = new System.Drawing.Size(122, 26);
            this.DtpFrom.TabIndex = 190;
            // 
            // DtpTo
            // 
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpTo.Location = new System.Drawing.Point(121, 34);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DtpTo.Size = new System.Drawing.Size(122, 26);
            this.DtpTo.TabIndex = 191;
            // 
            // checkOrderNumber
            // 
            this.checkOrderNumber.Location = new System.Drawing.Point(565, 37);
            this.checkOrderNumber.Name = "checkOrderNumber";
            this.checkOrderNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.checkOrderNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.checkOrderNumber.Properties.Appearance.Options.UseFont = true;
            this.checkOrderNumber.Properties.Appearance.Options.UseForeColor = true;
            this.checkOrderNumber.Properties.Appearance.Options.UseTextOptions = true;
            this.checkOrderNumber.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.checkOrderNumber.Properties.Caption = "رقم الفاتورة";
            this.checkOrderNumber.Properties.CheckBoxOptions.SvgColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.checkOrderNumber.Properties.CheckBoxOptions.SvgColorUnchecked = System.Drawing.Color.Gray;
            this.checkOrderNumber.Size = new System.Drawing.Size(106, 23);
            this.checkOrderNumber.TabIndex = 257;
            this.checkOrderNumber.CheckedChanged += new System.EventHandler(this.checkOrderNumber_CheckedChanged);
            // 
            // rbtnAllSup
            // 
            this.rbtnAllSup.AutoSize = true;
            this.rbtnAllSup.Location = new System.Drawing.Point(934, 37);
            this.rbtnAllSup.Name = "rbtnAllSup";
            this.rbtnAllSup.Size = new System.Drawing.Size(100, 22);
            this.rbtnAllSup.TabIndex = 258;
            this.rbtnAllSup.TabStop = true;
            this.rbtnAllSup.Text = "كل الموردين";
            this.rbtnAllSup.UseVisualStyleBackColor = true;
            // 
            // rbtnOneSupplier
            // 
            this.rbtnOneSupplier.AutoSize = true;
            this.rbtnOneSupplier.Location = new System.Drawing.Point(839, 37);
            this.rbtnOneSupplier.Name = "rbtnOneSupplier";
            this.rbtnOneSupplier.Size = new System.Drawing.Size(91, 22);
            this.rbtnOneSupplier.TabIndex = 259;
            this.rbtnOneSupplier.TabStop = true;
            this.rbtnOneSupplier.Text = "مورد محدد";
            this.rbtnOneSupplier.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(180, 341);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(135, 40);
            this.simpleButton1.TabIndex = 260;
            this.simpleButton1.Text = "معاينة الفاتورة";
            this.simpleButton1.UseVisualStyleBackColor = true;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(497, 337);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(135, 44);
            this.btnPrintAll.TabIndex = 261;
            this.btnPrintAll.Text = "طباعة كل الفواتير";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(36, 339);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 40);
            this.btnDelete.TabIndex = 262;
            this.btnDelete.Text = "مسح";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPtintOrder
            // 
            this.btnPtintOrder.Location = new System.Drawing.Point(336, 339);
            this.btnPtintOrder.Name = "btnPtintOrder";
            this.btnPtintOrder.Size = new System.Drawing.Size(135, 40);
            this.btnPtintOrder.TabIndex = 263;
            this.btnPtintOrder.Text = "طباعة الفاتورة";
            this.btnPtintOrder.UseVisualStyleBackColor = true;
            this.btnPtintOrder.Click += new System.EventHandler(this.btnPtintOrder_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(19, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(89, 47);
            this.btnSearch.TabIndex = 264;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(873, 338);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotal.Properties.Appearance.Options.UseFont = true;
            this.txtTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotal.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotal.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotal.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotal.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotal.Properties.Name = "textEdit1";
            this.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotal.Size = new System.Drawing.Size(161, 36);
            this.txtTotal.TabIndex = 265;
            // 
            // txtTotalTax
            // 
            this.txtTotalTax.EditValue = "";
            this.txtTotalTax.Enabled = false;
            this.txtTotalTax.Location = new System.Drawing.Point(873, 594);
            this.txtTotalTax.Name = "txtTotalTax";
            this.txtTotalTax.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalTax.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalTax.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalTax.Properties.Appearance.Options.UseFont = true;
            this.txtTotalTax.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalTax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalTax.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalTax.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalTax.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalTax.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalTax.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalTax.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalTax.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalTax.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalTax.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalTax.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalTax.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalTax.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalTax.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalTax.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalTax.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalTax.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalTax.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalTax.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalTax.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalTax.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalTax.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalTax.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalTax.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalTax.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalTax.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalTax.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalTax.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalTax.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalTax.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalTax.Properties.Name = "textEdit1";
            this.txtTotalTax.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalTax.Size = new System.Drawing.Size(161, 36);
            this.txtTotalTax.TabIndex = 266;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.EditValue = "";
            this.txtOrderNumber.Location = new System.Drawing.Point(455, 37);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtOrderNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNumber.Properties.Appearance.Options.UseBorderColor = true;
            this.txtOrderNumber.Properties.Appearance.Options.UseFont = true;
            this.txtOrderNumber.Properties.Appearance.Options.UseTextOptions = true;
            this.txtOrderNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOrderNumber.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNumber.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtOrderNumber.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNumber.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNumber.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtOrderNumber.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtOrderNumber.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtOrderNumber.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtOrderNumber.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtOrderNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtOrderNumber.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNumber.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtOrderNumber.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtOrderNumber.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtOrderNumber.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtOrderNumber.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNumber.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtOrderNumber.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNumber.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNumber.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtOrderNumber.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtOrderNumber.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtOrderNumber.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtOrderNumber.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNumber.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOrderNumber.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtOrderNumber.Properties.Name = "textEdit1";
            this.txtOrderNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOrderNumber.Size = new System.Drawing.Size(118, 24);
            this.txtOrderNumber.TabIndex = 267;
            // 
            // Frm_BuyReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1090, 646);
            this.Controls.Add(this.txtTotalTax);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnPtintOrder);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DgvDteils);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_BuyReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقارير المشتريات";
            this.Load += new System.EventHandler(this.Frm_BuyReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrderNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxSupplier;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DgvDteils;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private DevExpress.XtraEditors.CheckEdit checkOrderNumber;
        private System.Windows.Forms.RadioButton rbtnAllSup;
        private System.Windows.Forms.RadioButton rbtnOneSupplier;
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPtintOrder;
        private System.Windows.Forms.Button btnSearch;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtTotalTax;
        private DevExpress.XtraEditors.TextEdit txtOrderNumber;
    }
}