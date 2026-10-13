namespace PharmaCore.POS.PL
{
    partial class Frm_SalesRb7h
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxUser = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DgvDteils = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkOrderNumber = new DevExpress.XtraEditors.CheckEdit();
            this.rbtnAllUser = new System.Windows.Forms.RadioButton();
            this.rbtnOneUser = new System.Windows.Forms.RadioButton();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.txtOrderNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPtintOrderAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTotalRb7h = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrderNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalRb7h.Properties)).BeginInit();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvSearch.Location = new System.Drawing.Point(43, 211);
            this.DgvSearch.Name = "DgvSearch";
            this.DgvSearch.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(972, 190);
            this.DgvSearch.TabIndex = 49;
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxUser);
            this.groupBox1.Controls.Add(this.rbtnAllUser);
            this.groupBox1.Controls.Add(this.rbtnOneUser);
            this.groupBox1.Location = new System.Drawing.Point(43, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 67);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // cbxUser
            // 
            this.cbxUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUser.FormattingEnabled = true;
            this.cbxUser.Location = new System.Drawing.Point(15, 26);
            this.cbxUser.Name = "cbxUser";
            this.cbxUser.Size = new System.Drawing.Size(196, 26);
            this.cbxUser.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.DtpTo);
            this.groupBox2.Controls.Add(this.txtOrderNumber);
            this.groupBox2.Controls.Add(this.checkOrderNumber);
            this.groupBox2.Controls.Add(this.DtpFrom);
            this.groupBox2.Location = new System.Drawing.Point(91, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(876, 65);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(383, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "الى:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(576, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 18);
            this.label3.TabIndex = 31;
            this.label3.Text = "من:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(390, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(382, 23);
            this.label2.TabIndex = 43;
            this.label2.Text = "إجمالي ارباح المبيعات للمستخدم فى فترة";
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDteils.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvDteils.Location = new System.Drawing.Point(41, 440);
            this.DgvDteils.MultiSelect = false;
            this.DgvDteils.Name = "DgvDteils";
            this.DgvDteils.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvDteils.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvDteils.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvDteils.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDteils.Size = new System.Drawing.Size(974, 191);
            this.DgvDteils.TabIndex = 176;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotalRb7h);
            this.groupBox3.Controls.Add(this.txtTotal);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(41, 631);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(974, 72);
            this.groupBox3.TabIndex = 177;
            this.groupBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(218, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(264, 18);
            this.label5.TabIndex = 46;
            this.label5.Text = "اجمالي ارباح المبيعات فى الفترة المحددة:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(785, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 18);
            this.label4.TabIndex = 44;
            this.label4.Text = "اجمالي مبلغ المبيعات:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(451, 413);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 19);
            this.label7.TabIndex = 179;
            this.label7.Text = "تفصيل الفاتورة المحددة";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(462, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 19);
            this.label6.TabIndex = 178;
            this.label6.Text = "جميع فواتير المستخدم";
            // 
            // checkOrderNumber
            // 
            this.checkOrderNumber.Location = new System.Drawing.Point(737, 26);
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
            this.checkOrderNumber.Size = new System.Drawing.Size(118, 23);
            this.checkOrderNumber.TabIndex = 272;
            // 
            // rbtnAllUser
            // 
            this.rbtnAllUser.AutoSize = true;
            this.rbtnAllUser.Checked = true;
            this.rbtnAllUser.Location = new System.Drawing.Point(359, 27);
            this.rbtnAllUser.Name = "rbtnAllUser";
            this.rbtnAllUser.Size = new System.Drawing.Size(129, 22);
            this.rbtnAllUser.TabIndex = 282;
            this.rbtnAllUser.TabStop = true;
            this.rbtnAllUser.Text = "كل المستخدمين";
            this.rbtnAllUser.UseVisualStyleBackColor = true;
            // 
            // rbtnOneUser
            // 
            this.rbtnOneUser.AutoSize = true;
            this.rbtnOneUser.Location = new System.Drawing.Point(217, 27);
            this.rbtnOneUser.Name = "rbtnOneUser";
            this.rbtnOneUser.Size = new System.Drawing.Size(120, 22);
            this.rbtnOneUser.TabIndex = 283;
            this.rbtnOneUser.Text = "مستخدم محدد";
            this.rbtnOneUser.UseVisualStyleBackColor = true;
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(229, 23);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(148, 26);
            this.DtpTo.TabIndex = 284;
            this.DtpTo.Visible = false;
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(425, 25);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(148, 26);
            this.DtpFrom.TabIndex = 285;
            this.DtpFrom.Visible = false;
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.EditValue = "";
            this.txtOrderNumber.Location = new System.Drawing.Point(614, 26);
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
            this.txtOrderNumber.Size = new System.Drawing.Size(142, 24);
            this.txtOrderNumber.TabIndex = 290;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(16, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(183, 38);
            this.btnSearch.TabIndex = 291;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPtintOrderAll
            // 
            this.btnPtintOrderAll.Location = new System.Drawing.Point(563, 127);
            this.btnPtintOrderAll.Name = "btnPtintOrderAll";
            this.btnPtintOrderAll.Size = new System.Drawing.Size(183, 38);
            this.btnPtintOrderAll.TabIndex = 292;
            this.btnPtintOrderAll.Text = "طباعة تقرير مفصل";
            this.btnPtintOrderAll.UseVisualStyleBackColor = true;
            this.btnPtintOrderAll.Click += new System.EventHandler(this.btnPtintOrderAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(782, 127);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(169, 38);
            this.btnDelete.TabIndex = 293;
            this.btnDelete.Text = "مسح الفاتورة ";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTotalRb7h
            // 
            this.txtTotalRb7h.EditValue = "";
            this.txtTotalRb7h.Enabled = false;
            this.txtTotalRb7h.Location = new System.Drawing.Point(48, 19);
            this.txtTotalRb7h.Name = "txtTotalRb7h";
            this.txtTotalRb7h.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalRb7h.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRb7h.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalRb7h.Properties.Appearance.Options.UseFont = true;
            this.txtTotalRb7h.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalRb7h.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalRb7h.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalRb7h.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalRb7h.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalRb7h.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalRb7h.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalRb7h.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalRb7h.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalRb7h.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalRb7h.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalRb7h.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalRb7h.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalRb7h.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalRb7h.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalRb7h.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalRb7h.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalRb7h.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalRb7h.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalRb7h.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalRb7h.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalRb7h.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalRb7h.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalRb7h.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalRb7h.Properties.Name = "textEdit1";
            this.txtTotalRb7h.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalRb7h.Size = new System.Drawing.Size(161, 36);
            this.txtTotalRb7h.TabIndex = 294;
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(618, 19);
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
            this.txtTotal.TabIndex = 295;
            // 
            // Frm_SalesRb7h
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1056, 715);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPtintOrderAll);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.DgvDteils);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_SalesRb7h";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير أرباح المبيعات";
            this.Load += new System.EventHandler(this.Frm_SalesRb7h_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrderNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalRb7h.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DgvDteils;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.CheckEdit checkOrderNumber;
        private System.Windows.Forms.RadioButton rbtnAllUser;
        private System.Windows.Forms.RadioButton rbtnOneUser;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private DevExpress.XtraEditors.TextEdit txtOrderNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPtintOrderAll;
        private System.Windows.Forms.Button btnDelete;
        private DevExpress.XtraEditors.TextEdit txtTotalRb7h;
        private DevExpress.XtraEditors.TextEdit txtTotal;
    }
}