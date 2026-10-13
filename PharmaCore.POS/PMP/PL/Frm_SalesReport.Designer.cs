namespace PharmaCore.POS.PL
{
    partial class Frm_SalesReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gpBoxEmp = new System.Windows.Forms.GroupBox();
            this.cbxUser = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtnOneUser = new System.Windows.Forms.RadioButton();
            this.rbtnAllUser = new System.Windows.Forms.RadioButton();
            this.checkOrdNum = new DevExpress.XtraEditors.CheckEdit();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.txtOrderNum = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnOrderPrint = new System.Windows.Forms.Button();
            this.btnPtintOrderAll = new System.Windows.Forms.Button();
            this.txtUserRb7h = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gpBoxEmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserRb7h.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvSearch.Location = new System.Drawing.Point(29, 157);
            this.DgvSearch.Name = "DgvSearch";
            this.DgvSearch.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(993, 332);
            this.DgvSearch.TabIndex = 42;
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(218, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 18);
            this.label10.TabIndex = 137;
            this.label10.Text = "اسم المستخدم :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(445, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 23);
            this.label2.TabIndex = 37;
            this.label2.Text = "تقارير مبيعات المستخدم";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(902, 595);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 18);
            this.label8.TabIndex = 53;
            this.label8.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(461, 595);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(250, 18);
            this.label7.TabIndex = 51;
            this.label7.Text = "اجمالي نسبة الربح للمستخدم المحدد:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(91, 594);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 18);
            this.label4.TabIndex = 45;
            this.label4.Text = "اجمالي مبلغ المبيعات:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOrderNum);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.DtpTo);
            this.groupBox1.Controls.Add(this.rbtnAllUser);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.rbtnOneUser);
            this.groupBox1.Controls.Add(this.DtpFrom);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.checkOrdNum);
            this.groupBox1.Controls.Add(this.gpBoxEmp);
            this.groupBox1.Location = new System.Drawing.Point(113, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 115);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(394, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 18);
            this.label9.TabIndex = 35;
            this.label9.Text = "الى:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(603, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 18);
            this.label11.TabIndex = 31;
            this.label11.Text = "من:";
            // 
            // gpBoxEmp
            // 
            this.gpBoxEmp.Controls.Add(this.label10);
            this.gpBoxEmp.Controls.Add(this.cbxUser);
            this.gpBoxEmp.Location = new System.Drawing.Point(164, 55);
            this.gpBoxEmp.Name = "gpBoxEmp";
            this.gpBoxEmp.Size = new System.Drawing.Size(343, 54);
            this.gpBoxEmp.TabIndex = 209;
            this.gpBoxEmp.TabStop = false;
            // 
            // cbxUser
            // 
            this.cbxUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUser.FormattingEnabled = true;
            this.cbxUser.Location = new System.Drawing.Point(12, 20);
            this.cbxUser.Name = "cbxUser";
            this.cbxUser.Size = new System.Drawing.Size(201, 26);
            this.cbxUser.TabIndex = 129;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(778, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 20);
            this.label5.TabIndex = 215;
            this.label5.Text = "إضغط مرتين على الفاتورة لأجل التفاصيل ";
            // 
            // rbtnOneUser
            // 
            this.rbtnOneUser.AutoSize = true;
            this.rbtnOneUser.Location = new System.Drawing.Point(569, 77);
            this.rbtnOneUser.Name = "rbtnOneUser";
            this.rbtnOneUser.Size = new System.Drawing.Size(120, 22);
            this.rbtnOneUser.TabIndex = 282;
            this.rbtnOneUser.Text = "مستخدم محدد";
            this.rbtnOneUser.UseVisualStyleBackColor = true;
            // 
            // rbtnAllUser
            // 
            this.rbtnAllUser.AutoSize = true;
            this.rbtnAllUser.Checked = true;
            this.rbtnAllUser.Location = new System.Drawing.Point(712, 76);
            this.rbtnAllUser.Name = "rbtnAllUser";
            this.rbtnAllUser.Size = new System.Drawing.Size(129, 22);
            this.rbtnAllUser.TabIndex = 283;
            this.rbtnAllUser.TabStop = true;
            this.rbtnAllUser.Text = "كل المستخدمين";
            this.rbtnAllUser.UseVisualStyleBackColor = true;
            // 
            // checkOrdNum
            // 
            this.checkOrdNum.Location = new System.Drawing.Point(747, 27);
            this.checkOrdNum.Name = "checkOrdNum";
            this.checkOrdNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.checkOrdNum.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.checkOrdNum.Properties.Appearance.Options.UseFont = true;
            this.checkOrdNum.Properties.Appearance.Options.UseForeColor = true;
            this.checkOrdNum.Properties.Appearance.Options.UseTextOptions = true;
            this.checkOrdNum.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.checkOrdNum.Properties.Caption = "رقم الفاتورة";
            this.checkOrdNum.Properties.CheckBoxOptions.SvgColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.checkOrdNum.Properties.CheckBoxOptions.SvgColorUnchecked = System.Drawing.Color.Gray;
            this.checkOrdNum.Size = new System.Drawing.Size(107, 23);
            this.checkOrdNum.TabIndex = 284;
            this.checkOrdNum.CheckedChanged += new System.EventHandler(this.checkOrdNum_CheckedChanged);
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(436, 23);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(163, 26);
            this.DtpFrom.TabIndex = 285;
            this.DtpFrom.Visible = false;
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(225, 23);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(163, 26);
            this.DtpTo.TabIndex = 286;
            this.DtpTo.Visible = false;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.EditValue = "";
            this.txtOrderNum.Location = new System.Drawing.Point(641, 27);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtOrderNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.Appearance.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.Appearance.Options.UseFont = true;
            this.txtOrderNum.Properties.Appearance.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOrderNum.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtOrderNum.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNum.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtOrderNum.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtOrderNum.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtOrderNum.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNum.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOrderNum.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtOrderNum.Properties.Name = "textEdit1";
            this.txtOrderNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOrderNum.Size = new System.Drawing.Size(107, 24);
            this.txtOrderNum.TabIndex = 290;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(36, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(107, 38);
            this.btnSearch.TabIndex = 291;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(262, 512);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(227, 38);
            this.btnDeleteAll.TabIndex = 293;
            this.btnDeleteAll.Text = "مسح الفواتير المعروضه كلها";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnOrderPrint
            // 
            this.btnOrderPrint.Location = new System.Drawing.Point(552, 512);
            this.btnOrderPrint.Name = "btnOrderPrint";
            this.btnOrderPrint.Size = new System.Drawing.Size(180, 38);
            this.btnOrderPrint.TabIndex = 294;
            this.btnOrderPrint.Text = "طباعة الفاتورة المحددة";
            this.btnOrderPrint.UseVisualStyleBackColor = true;
            this.btnOrderPrint.Click += new System.EventHandler(this.btnOrderPrint_Click);
            // 
            // btnPtintOrderAll
            // 
            this.btnPtintOrderAll.Location = new System.Drawing.Point(807, 512);
            this.btnPtintOrderAll.Name = "btnPtintOrderAll";
            this.btnPtintOrderAll.Size = new System.Drawing.Size(151, 38);
            this.btnPtintOrderAll.TabIndex = 295;
            this.btnPtintOrderAll.Text = "طباعة تقرير مفصل";
            this.btnPtintOrderAll.UseVisualStyleBackColor = true;
            this.btnPtintOrderAll.Click += new System.EventHandler(this.btnPtintOrderAll_Click);
            // 
            // txtUserRb7h
            // 
            this.txtUserRb7h.EditValue = "";
            this.txtUserRb7h.Enabled = false;
            this.txtUserRb7h.Location = new System.Drawing.Point(717, 583);
            this.txtUserRb7h.Name = "txtUserRb7h";
            this.txtUserRb7h.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtUserRb7h.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserRb7h.Properties.Appearance.Options.UseBorderColor = true;
            this.txtUserRb7h.Properties.Appearance.Options.UseFont = true;
            this.txtUserRb7h.Properties.Appearance.Options.UseTextOptions = true;
            this.txtUserRb7h.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtUserRb7h.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtUserRb7h.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtUserRb7h.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtUserRb7h.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtUserRb7h.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtUserRb7h.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtUserRb7h.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtUserRb7h.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtUserRb7h.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtUserRb7h.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtUserRb7h.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtUserRb7h.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtUserRb7h.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtUserRb7h.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUserRb7h.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtUserRb7h.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtUserRb7h.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtUserRb7h.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtUserRb7h.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtUserRb7h.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtUserRb7h.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtUserRb7h.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtUserRb7h.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtUserRb7h.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtUserRb7h.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtUserRb7h.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtUserRb7h.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtUserRb7h.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtUserRb7h.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtUserRb7h.Properties.Name = "textEdit1";
            this.txtUserRb7h.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserRb7h.Size = new System.Drawing.Size(161, 36);
            this.txtUserRb7h.TabIndex = 296;
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(243, 583);
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
            this.txtTotal.TabIndex = 297;
            // 
            // Frm_SalesReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1053, 640);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtUserRb7h);
            this.Controls.Add(this.btnPtintOrderAll);
            this.Controls.Add(this.btnOrderPrint);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_SalesReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تقارير مبيعات المستخدمين";
            this.Load += new System.EventHandler(this.Frm_SalesReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpBoxEmp.ResumeLayout(false);
            this.gpBoxEmp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserRb7h.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gpBoxEmp;
        public System.Windows.Forms.ComboBox cbxUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnOneUser;
        private System.Windows.Forms.RadioButton rbtnAllUser;
        private DevExpress.XtraEditors.CheckEdit checkOrdNum;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private DevExpress.XtraEditors.TextEdit txtOrderNum;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnOrderPrint;
        private System.Windows.Forms.Button btnPtintOrderAll;
        private DevExpress.XtraEditors.TextEdit txtUserRb7h;
        private DevExpress.XtraEditors.TextEdit txtTotal;
    }
}