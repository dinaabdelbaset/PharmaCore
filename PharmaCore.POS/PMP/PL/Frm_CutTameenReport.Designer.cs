namespace PharmaCore.POS.PL
{
    partial class Frm_CutTameenReport
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
            this.label2 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtOrderNum = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gpBoxEmp = new System.Windows.Forms.GroupBox();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.cbxCompany = new System.Windows.Forms.ComboBox();
            this.RadioName = new System.Windows.Forms.RadioButton();
            this.RadioCompany = new System.Windows.Forms.RadioButton();
            this.RadioPhone = new System.Windows.Forms.RadioButton();
            this.RadioCode = new System.Windows.Forms.RadioButton();
            this.rbtnAllUser = new System.Windows.Forms.RadioButton();
            this.rbtnOneUser = new System.Windows.Forms.RadioButton();
            this.checkOrdNum = new DevExpress.XtraEditors.CheckEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalNumb = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalGet = new DevExpress.XtraEditors.TextEdit();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).BeginInit();
            this.gpBoxEmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNumb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalGet.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(475, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 23);
            this.label2.TabIndex = 209;
            this.label2.Text = "تقارير مبيعات التأمينات";
            // 
            // DgvSearch
            // 
            this.DgvSearch.AllowUserToAddRows = false;
            this.DgvSearch.AllowUserToDeleteRows = false;
            this.DgvSearch.AllowUserToResizeColumns = false;
            this.DgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
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
            this.DgvSearch.Location = new System.Drawing.Point(70, 167);
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
            this.DgvSearch.Size = new System.Drawing.Size(993, 332);
            this.DgvSearch.TabIndex = 211;
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtOrderNum);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.DtpTo);
            this.groupBox2.Controls.Add(this.DtpFrom);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.gpBoxEmp);
            this.groupBox2.Controls.Add(this.rbtnAllUser);
            this.groupBox2.Controls.Add(this.rbtnOneUser);
            this.groupBox2.Controls.Add(this.checkOrdNum);
            this.groupBox2.Location = new System.Drawing.Point(12, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1112, 115);
            this.groupBox2.TabIndex = 210;
            this.groupBox2.TabStop = false;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.EditValue = "";
            this.txtOrderNum.Enabled = false;
            this.txtOrderNum.Location = new System.Drawing.Point(669, 26);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtOrderNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.Appearance.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.Appearance.Options.UseFont = true;
            this.txtOrderNum.Properties.Appearance.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOrderNum.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtOrderNum.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNum.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOrderNum.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtOrderNum.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtOrderNum.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtOrderNum.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtOrderNum.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtOrderNum.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtOrderNum.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtOrderNum.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOrderNum.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtOrderNum.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtOrderNum.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtOrderNum.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtOrderNum.Properties.Name = "textEdit1";
            this.txtOrderNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOrderNum.Size = new System.Drawing.Size(102, 24);
            this.txtOrderNum.TabIndex = 285;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(405, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "الى:";
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(236, 23);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(163, 26);
            this.DtpTo.TabIndex = 281;
            this.DtpTo.Visible = false;
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(447, 25);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(161, 26);
            this.DtpFrom.TabIndex = 280;
            this.DtpFrom.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(27, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(183, 38);
            this.btnSearch.TabIndex = 285;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(614, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 18);
            this.label3.TabIndex = 31;
            this.label3.Text = "من:";
            // 
            // gpBoxEmp
            // 
            this.gpBoxEmp.Controls.Add(this.txtPhone);
            this.gpBoxEmp.Controls.Add(this.txtName);
            this.gpBoxEmp.Controls.Add(this.txtCode);
            this.gpBoxEmp.Controls.Add(this.cbxCompany);
            this.gpBoxEmp.Controls.Add(this.RadioName);
            this.gpBoxEmp.Controls.Add(this.RadioCompany);
            this.gpBoxEmp.Controls.Add(this.RadioPhone);
            this.gpBoxEmp.Controls.Add(this.RadioCode);
            this.gpBoxEmp.Location = new System.Drawing.Point(15, 55);
            this.gpBoxEmp.Name = "gpBoxEmp";
            this.gpBoxEmp.Size = new System.Drawing.Size(1089, 54);
            this.gpBoxEmp.TabIndex = 209;
            this.gpBoxEmp.TabStop = false;
            // 
            // txtPhone
            // 
            this.txtPhone.EditValue = "";
            this.txtPhone.Location = new System.Drawing.Point(280, 18);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPhone.Properties.Appearance.Options.UseFont = true;
            this.txtPhone.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPhone.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPhone.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtPhone.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtPhone.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtPhone.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtPhone.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtPhone.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtPhone.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtPhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtPhone.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPhone.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtPhone.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtPhone.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtPhone.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtPhone.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtPhone.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtPhone.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtPhone.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtPhone.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtPhone.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPhone.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtPhone.Properties.Name = "textEdit1";
            this.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhone.Size = new System.Drawing.Size(142, 24);
            this.txtPhone.TabIndex = 285;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(564, 18);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.Appearance.Options.UseBorderColor = true;
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.Appearance.Options.UseTextOptions = true;
            this.txtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtName.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtName.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtName.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtName.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtName.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtName.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtName.Properties.Name = "textEdit1";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(142, 24);
            this.txtName.TabIndex = 286;
            // 
            // txtCode
            // 
            this.txtCode.EditValue = "";
            this.txtCode.Location = new System.Drawing.Point(843, 18);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtCode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtCode.Properties.Appearance.Options.UseFont = true;
            this.txtCode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCode.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtCode.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtCode.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCode.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtCode.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtCode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtCode.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtCode.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCode.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtCode.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtCode.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtCode.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtCode.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtCode.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCode.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtCode.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtCode.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtCode.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtCode.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtCode.Properties.Name = "textEdit1";
            this.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCode.Size = new System.Drawing.Size(142, 24);
            this.txtCode.TabIndex = 287;
            // 
            // cbxCompany
            // 
            this.cbxCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCompany.FormattingEnabled = true;
            this.cbxCompany.Location = new System.Drawing.Point(12, 18);
            this.cbxCompany.Name = "cbxCompany";
            this.cbxCompany.Size = new System.Drawing.Size(157, 26);
            this.cbxCompany.TabIndex = 211;
            // 
            // RadioName
            // 
            this.RadioName.AutoSize = true;
            this.RadioName.Location = new System.Drawing.Point(709, 19);
            this.RadioName.Name = "RadioName";
            this.RadioName.Size = new System.Drawing.Size(110, 22);
            this.RadioName.TabIndex = 277;
            this.RadioName.TabStop = true;
            this.RadioName.Text = "اسم العميل :";
            this.RadioName.UseVisualStyleBackColor = true;
            // 
            // RadioCompany
            // 
            this.RadioCompany.AutoSize = true;
            this.RadioCompany.Location = new System.Drawing.Point(175, 19);
            this.RadioCompany.Name = "RadioCompany";
            this.RadioCompany.Size = new System.Drawing.Size(82, 22);
            this.RadioCompany.TabIndex = 279;
            this.RadioCompany.TabStop = true;
            this.RadioCompany.Text = "الشركة :";
            this.RadioCompany.UseVisualStyleBackColor = true;
            // 
            // RadioPhone
            // 
            this.RadioPhone.AutoSize = true;
            this.RadioPhone.Location = new System.Drawing.Point(423, 19);
            this.RadioPhone.Name = "RadioPhone";
            this.RadioPhone.Size = new System.Drawing.Size(120, 22);
            this.RadioPhone.TabIndex = 276;
            this.RadioPhone.TabStop = true;
            this.RadioPhone.Text = "تليفون العميل :";
            this.RadioPhone.UseVisualStyleBackColor = true;
            // 
            // RadioCode
            // 
            this.RadioCode.AutoSize = true;
            this.RadioCode.Location = new System.Drawing.Point(982, 19);
            this.RadioCode.Name = "RadioCode";
            this.RadioCode.Size = new System.Drawing.Size(103, 22);
            this.RadioCode.TabIndex = 278;
            this.RadioCode.TabStop = true;
            this.RadioCode.Text = "رقم التأمين :";
            this.RadioCode.UseVisualStyleBackColor = true;
            // 
            // rbtnAllUser
            // 
            this.rbtnAllUser.AutoSize = true;
            this.rbtnAllUser.Location = new System.Drawing.Point(1013, 27);
            this.rbtnAllUser.Name = "rbtnAllUser";
            this.rbtnAllUser.Size = new System.Drawing.Size(90, 22);
            this.rbtnAllUser.TabIndex = 274;
            this.rbtnAllUser.TabStop = true;
            this.rbtnAllUser.Text = "كل العملاء";
            this.rbtnAllUser.UseVisualStyleBackColor = true;
            this.rbtnAllUser.CheckedChanged += new System.EventHandler(this.rbtnAllUser_CheckedChanged);
            // 
            // rbtnOneUser
            // 
            this.rbtnOneUser.AutoSize = true;
            this.rbtnOneUser.Location = new System.Drawing.Point(904, 27);
            this.rbtnOneUser.Name = "rbtnOneUser";
            this.rbtnOneUser.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneUser.TabIndex = 275;
            this.rbtnOneUser.TabStop = true;
            this.rbtnOneUser.Text = "عميل محدد";
            this.rbtnOneUser.UseVisualStyleBackColor = true;
            this.rbtnOneUser.CheckedChanged += new System.EventHandler(this.rbtnOneUser_CheckedChanged);
            // 
            // checkOrdNum
            // 
            this.checkOrdNum.Location = new System.Drawing.Point(777, 26);
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
            this.checkOrdNum.Size = new System.Drawing.Size(104, 23);
            this.checkOrdNum.TabIndex = 270;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(292, 583);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 18);
            this.label4.TabIndex = 212;
            this.label4.Text = "اجمالي مبلغ المبيعات:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(668, 583);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(213, 18);
            this.label5.TabIndex = 218;
            this.label5.Text = "اجمالي المبلغ بعد خصم التأمين :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(35, 583);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(111, 18);
            this.label6.TabIndex = 221;
            this.label6.Text = "اجمالي الفواتير :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(871, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(263, 20);
            this.label7.TabIndex = 222;
            this.label7.Text = "إضغط مرتين على الفاتورة لأجل التفاصيل ";
            // 
            // txtTotalNumb
            // 
            this.txtTotalNumb.EditValue = "";
            this.txtTotalNumb.Enabled = false;
            this.txtTotalNumb.Location = new System.Drawing.Point(152, 576);
            this.txtTotalNumb.Name = "txtTotalNumb";
            this.txtTotalNumb.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalNumb.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalNumb.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalNumb.Properties.Appearance.Options.UseFont = true;
            this.txtTotalNumb.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalNumb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalNumb.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalNumb.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalNumb.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalNumb.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalNumb.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalNumb.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalNumb.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalNumb.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalNumb.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalNumb.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalNumb.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalNumb.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalNumb.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalNumb.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalNumb.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalNumb.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalNumb.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalNumb.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalNumb.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalNumb.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalNumb.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalNumb.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalNumb.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalNumb.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalNumb.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalNumb.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalNumb.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalNumb.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalNumb.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalNumb.Properties.Name = "textEdit1";
            this.txtTotalNumb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalNumb.Size = new System.Drawing.Size(112, 36);
            this.txtTotalNumb.TabIndex = 282;
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(453, 576);
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
            this.txtTotal.TabIndex = 283;
            // 
            // txtTotalGet
            // 
            this.txtTotalGet.EditValue = "";
            this.txtTotalGet.Enabled = false;
            this.txtTotalGet.Location = new System.Drawing.Point(887, 576);
            this.txtTotalGet.Name = "txtTotalGet";
            this.txtTotalGet.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalGet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalGet.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalGet.Properties.Appearance.Options.UseFont = true;
            this.txtTotalGet.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalGet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalGet.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalGet.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalGet.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalGet.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalGet.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalGet.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalGet.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalGet.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalGet.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalGet.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalGet.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalGet.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalGet.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalGet.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalGet.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalGet.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalGet.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalGet.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalGet.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalGet.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalGet.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalGet.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalGet.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalGet.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalGet.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalGet.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalGet.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalGet.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalGet.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalGet.Properties.Name = "textEdit1";
            this.txtTotalGet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalGet.Size = new System.Drawing.Size(161, 36);
            this.txtTotalGet.TabIndex = 284;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(94, 505);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(183, 38);
            this.btnDelete.TabIndex = 286;
            this.btnDelete.Text = "مسح هذه الفاتورة فقط";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(327, 505);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(183, 38);
            this.btnDeleteAll.TabIndex = 287;
            this.btnDeleteAll.Text = "مسح البيانات";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(577, 505);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(183, 38);
            this.btnPrint.TabIndex = 288;
            this.btnPrint.Text = "طباعة الفاتورة المحددة";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(827, 505);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(183, 38);
            this.btnPrintAll.TabIndex = 289;
            this.btnPrintAll.Text = "طباعة تقرير مفصل";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // Frm_CutTameenReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1146, 635);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtTotalGet);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtTotalNumb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_CutTameenReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقارير مبيعات التأمينات";
            this.Load += new System.EventHandler(this.Frm_CutTameenReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).EndInit();
            this.gpBoxEmp.ResumeLayout(false);
            this.gpBoxEmp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalNumb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalGet.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gpBoxEmp;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cbxCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.CheckEdit checkOrdNum;
        private System.Windows.Forms.RadioButton rbtnAllUser;
        private System.Windows.Forms.RadioButton rbtnOneUser;
        private System.Windows.Forms.RadioButton RadioPhone;
        private System.Windows.Forms.RadioButton RadioName;
        private System.Windows.Forms.RadioButton RadioCode;
        private System.Windows.Forms.RadioButton RadioCompany;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private DevExpress.XtraEditors.TextEdit txtTotalNumb;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtTotalGet;
        private DevExpress.XtraEditors.TextEdit txtOrderNum;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintAll;
    }
}