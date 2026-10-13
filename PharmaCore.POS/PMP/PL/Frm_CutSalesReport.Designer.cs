namespace PharmaCore.POS.PL
{
    partial class Frm_CutSalesReport
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
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gpBoxEmp = new System.Windows.Forms.GroupBox();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtnOneUser = new System.Windows.Forms.RadioButton();
            this.rbtnAllUser = new System.Windows.Forms.RadioButton();
            this.RadioCode = new System.Windows.Forms.RadioButton();
            this.RadioName = new System.Windows.Forms.RadioButton();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.checkOrdNum = new DevExpress.XtraEditors.CheckEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtOrderNum = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gpBoxEmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(29, 568);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 18);
            this.label4.TabIndex = 60;
            this.label4.Text = "اجمالي مبلغ المشتريات:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(394, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 35;
            this.label1.Text = "الى:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(603, 29);
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
            this.label2.Location = new System.Drawing.Point(470, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 23);
            this.label2.TabIndex = 54;
            this.label2.Text = "تقارير مبيعات العملاء";
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvSearch.Location = new System.Drawing.Point(50, 155);
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
            this.DgvSearch.TabIndex = 58;
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkOrdNum);
            this.groupBox2.Controls.Add(this.txtOrderNum);
            this.groupBox2.Controls.Add(this.DtpFrom);
            this.groupBox2.Controls.Add(this.DtpTo);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.rbtnOneUser);
            this.groupBox2.Controls.Add(this.rbtnAllUser);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.gpBoxEmp);
            this.groupBox2.Location = new System.Drawing.Point(103, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(860, 115);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            // 
            // gpBoxEmp
            // 
            this.gpBoxEmp.Controls.Add(this.txtCode);
            this.gpBoxEmp.Controls.Add(this.RadioName);
            this.gpBoxEmp.Controls.Add(this.RadioCode);
            this.gpBoxEmp.Controls.Add(this.cbxCustomer);
            this.gpBoxEmp.Location = new System.Drawing.Point(15, 55);
            this.gpBoxEmp.Name = "gpBoxEmp";
            this.gpBoxEmp.Size = new System.Drawing.Size(585, 54);
            this.gpBoxEmp.TabIndex = 209;
            this.gpBoxEmp.TabStop = false;
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(12, 18);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(201, 26);
            this.cbxCustomer.TabIndex = 129;
            this.cbxCustomer.SelectedIndexChanged += new System.EventHandler(this.RadioName_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(819, 586);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 20);
            this.label5.TabIndex = 209;
            this.label5.Text = "إضغط مرتين على الفاتورة لأجل التفاصيل ";
            // 
            // rbtnOneUser
            // 
            this.rbtnOneUser.AutoSize = true;
            this.rbtnOneUser.Location = new System.Drawing.Point(626, 74);
            this.rbtnOneUser.Name = "rbtnOneUser";
            this.rbtnOneUser.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneUser.TabIndex = 272;
            this.rbtnOneUser.TabStop = true;
            this.rbtnOneUser.Text = "عميل محدد";
            this.rbtnOneUser.UseVisualStyleBackColor = true;
            this.rbtnOneUser.CheckedChanged += new System.EventHandler(this.rbtnOneUser_CheckedChanged);
            // 
            // rbtnAllUser
            // 
            this.rbtnAllUser.AutoSize = true;
            this.rbtnAllUser.Location = new System.Drawing.Point(744, 74);
            this.rbtnAllUser.Name = "rbtnAllUser";
            this.rbtnAllUser.Size = new System.Drawing.Size(90, 22);
            this.rbtnAllUser.TabIndex = 273;
            this.rbtnAllUser.TabStop = true;
            this.rbtnAllUser.Text = "كل العملاء";
            this.rbtnAllUser.UseVisualStyleBackColor = true;
            this.rbtnAllUser.CheckedChanged += new System.EventHandler(this.rbtnAllUser_CheckedChanged);
            // 
            // RadioCode
            // 
            this.RadioCode.AutoSize = true;
            this.RadioCode.Location = new System.Drawing.Point(477, 18);
            this.RadioCode.Name = "RadioCode";
            this.RadioCode.Size = new System.Drawing.Size(102, 22);
            this.RadioCode.TabIndex = 274;
            this.RadioCode.TabStop = true;
            this.RadioCode.Text = "كود العميل :";
            this.RadioCode.UseVisualStyleBackColor = true;
            this.RadioCode.CheckedChanged += new System.EventHandler(this.RadioCode_Click);
            // 
            // RadioName
            // 
            this.RadioName.AutoSize = true;
            this.RadioName.Location = new System.Drawing.Point(213, 18);
            this.RadioName.Name = "RadioName";
            this.RadioName.Size = new System.Drawing.Size(110, 22);
            this.RadioName.TabIndex = 275;
            this.RadioName.TabStop = true;
            this.RadioName.Text = "اسم العميل :";
            this.RadioName.UseVisualStyleBackColor = true;
            this.RadioName.CheckedChanged += new System.EventHandler(this.RadioName_Click);
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(431, 25);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(169, 26);
            this.DtpFrom.TabIndex = 271;
            this.DtpFrom.Visible = false;
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(219, 23);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(169, 26);
            this.DtpTo.TabIndex = 272;
            this.DtpTo.Visible = false;
            // 
            // checkOrdNum
            // 
            this.checkOrdNum.Location = new System.Drawing.Point(745, 28);
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
            this.checkOrdNum.TabIndex = 269;
            this.checkOrdNum.CheckedChanged += new System.EventHandler(this.checkOrdNum_CheckedChanged);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(195, 556);
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
            this.txtTotal.TabIndex = 276;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.EditValue = "";
            this.txtOrderNum.Enabled = false;
            this.txtOrderNum.Location = new System.Drawing.Point(641, 28);
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
            this.txtOrderNum.TabIndex = 277;
            // 
            // txtCode
            // 
            this.txtCode.EditValue = "";
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(339, 18);
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
            this.txtCode.Size = new System.Drawing.Size(133, 24);
            this.txtCode.TabIndex = 278;
            this.txtCode.TextChanged += new System.EventHandler(this.RadioCode_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(21, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(183, 38);
            this.btnSearch.TabIndex = 277;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(552, 493);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(183, 38);
            this.btnPrint.TabIndex = 278;
            this.btnPrint.Text = "طباعة الفاتورة المحددة";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(808, 493);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(183, 38);
            this.btnPrintAll.TabIndex = 279;
            this.btnPrintAll.Text = "طباعة تقرير مفصل";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(321, 493);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(183, 38);
            this.btnDeleteAll.TabIndex = 280;
            this.btnDeleteAll.Text = "مسح البيانات";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(90, 493);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(183, 38);
            this.btnDelete.TabIndex = 281;
            this.btnDelete.Text = "مسح هذه الفاتورة فقط";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Frm_CutSalesReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1092, 613);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_CutSalesReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقارير مبيعات العملاء";
            this.Load += new System.EventHandler(this.Frm_CutSalesReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpBoxEmp.ResumeLayout(false);
            this.gpBoxEmp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkOrdNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gpBoxEmp;
        public System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnOneUser;
        private System.Windows.Forms.RadioButton rbtnAllUser;
        private System.Windows.Forms.RadioButton RadioName;
        private System.Windows.Forms.RadioButton RadioCode;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private DevExpress.XtraEditors.CheckEdit checkOrdNum;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtOrderNum;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnDelete;
    }
}