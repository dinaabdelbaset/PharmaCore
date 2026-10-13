namespace PharmaCore.POS.PL
{
    partial class Frm_ReturnBuy
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
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.DgvDteils = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblName1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxStore2 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblName2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxStore1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.btnReturnItemOnly = new System.Windows.Forms.Button();
            this.btnReturnAll = new System.Windows.Forms.Button();
            this.txtName1 = new DevExpress.XtraEditors.TextEdit();
            this.txtName2 = new DevExpress.XtraEditors.TextEdit();
            this.NudQty = new DevExpress.XtraEditors.TextEdit();
            this.rbtnReturnItemOnly = new System.Windows.Forms.RadioButton();
            this.rbtnReturnQtyonly = new System.Windows.Forms.RadioButton();
            this.txtMadfou3 = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalOrder = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMadfou3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalOrder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Crimson;
            this.label4.Location = new System.Drawing.Point(403, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 19);
            this.label4.TabIndex = 102;
            this.label4.Text = "جميع فواتير العميل";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(61, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 18);
            this.label11.TabIndex = 101;
            this.label11.Text = "كود المورد :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(216, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 18);
            this.label9.TabIndex = 99;
            this.label9.Text = "اسم المورد :";
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(176, 59);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(179, 26);
            this.cbxSupplier.TabIndex = 2;
            this.cbxSupplier.SelectionChangeCommitted += new System.EventHandler(this.cbxCustomer_SelectionChangeCommitted);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvDteils.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvDteils.Location = new System.Drawing.Point(37, 286);
            this.DgvDteils.Name = "DgvDteils";
            this.DgvDteils.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvDteils.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvDteils.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvDteils.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvDteils.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvDteils.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDteils.Size = new System.Drawing.Size(868, 160);
            this.DgvDteils.TabIndex = 97;
            this.DgvDteils.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDteils_CellContentClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(197, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 18);
            this.label5.TabIndex = 41;
            this.label5.Text = "إجمالي مدفوع الفاتورة المحددة :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(392, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 19);
            this.label7.TabIndex = 103;
            this.label7.Text = "تفصيل الفاتورة المحددة";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnReturnItemOnly);
            this.groupBox4.Controls.Add(this.rbtnReturnQtyonly);
            this.groupBox4.Controls.Add(this.NudQty);
            this.groupBox4.Controls.Add(this.btnReturnItemOnly);
            this.groupBox4.Controls.Add(this.txtName2);
            this.groupBox4.Controls.Add(this.lblName1);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.cbxStore2);
            this.groupBox4.Location = new System.Drawing.Point(396, 508);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(509, 174);
            this.groupBox4.TabIndex = 96;
            this.groupBox4.TabStop = false;
            // 
            // lblName1
            // 
            this.lblName1.AutoSize = true;
            this.lblName1.ForeColor = System.Drawing.Color.Black;
            this.lblName1.Location = new System.Drawing.Point(190, 28);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(83, 18);
            this.lblName1.TabIndex = 104;
            this.lblName1.Text = "اسم المورد:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(355, 135);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 18);
            this.label12.TabIndex = 53;
            this.label12.Text = "الى المخزن:";
            // 
            // cbxStore2
            // 
            this.cbxStore2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStore2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStore2.FormattingEnabled = true;
            this.cbxStore2.Location = new System.Drawing.Point(22, 130);
            this.cbxStore2.Name = "cbxStore2";
            this.cbxStore2.Size = new System.Drawing.Size(327, 26);
            this.cbxStore2.TabIndex = 52;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMadfou3);
            this.groupBox2.Controls.Add(this.txtTotalOrder);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(37, 452);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 61);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(677, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 18);
            this.label8.TabIndex = 104;
            this.label8.Text = "إجمالي المدفوعات للمورد :";
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
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvSearch.Location = new System.Drawing.Point(37, 120);
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
            this.DgvSearch.Size = new System.Drawing.Size(868, 131);
            this.DgvSearch.TabIndex = 95;
            this.DgvSearch.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentClick);
            this.DgvSearch.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSearch_CellContentDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtName1);
            this.groupBox3.Controls.Add(this.lblName2);
            this.groupBox3.Controls.Add(this.btnReturnAll);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cbxStore1);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(37, 508);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(353, 174);
            this.groupBox3.TabIndex = 90;
            this.groupBox3.TabStop = false;
            // 
            // lblName2
            // 
            this.lblName2.AutoSize = true;
            this.lblName2.ForeColor = System.Drawing.Color.Black;
            this.lblName2.Location = new System.Drawing.Point(239, 32);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(83, 18);
            this.lblName2.TabIndex = 105;
            this.lblName2.Text = "اسم المورد:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(236, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 18);
            this.label10.TabIndex = 51;
            this.label10.Text = "الى المخزن:";
            // 
            // cbxStore1
            // 
            this.cbxStore1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStore1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStore1.FormattingEnabled = true;
            this.cbxStore1.Location = new System.Drawing.Point(35, 75);
            this.cbxStore1.Name = "cbxStore1";
            this.cbxStore1.Size = new System.Drawing.Size(198, 26);
            this.cbxStore1.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(623, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 94;
            this.label1.Text = "بحث برقم الفاتورة:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(385, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 92;
            this.label3.Text = "تاريخ الفاتورة:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(374, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 23);
            this.label2.TabIndex = 89;
            this.label2.Text = "ادارة مرتجعات المشتريات";
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(480, 56);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(120, 26);
            this.DtpDate.TabIndex = 295;
            this.DtpDate.Visible = false;
            this.DtpDate.ValueChanged += new System.EventHandler(this.DtpDate_ValueChanged);
            // 
            // txtCode
            // 
            this.txtCode.EditValue = "";
            this.txtCode.Location = new System.Drawing.Point(28, 59);
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
            this.txtCode.TabIndex = 294;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Location = new System.Drawing.Point(753, 56);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.Appearance.Options.UseBorderColor = true;
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.Appearance.Options.UseTextOptions = true;
            this.txtID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtID.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtID.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtID.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtID.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtID.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtID.Properties.Name = "textEdit1";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtID.Size = new System.Drawing.Size(142, 24);
            this.txtID.TabIndex = 293;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // btnReturnItemOnly
            // 
            this.btnReturnItemOnly.Location = new System.Drawing.Point(42, 68);
            this.btnReturnItemOnly.Name = "btnReturnItemOnly";
            this.btnReturnItemOnly.Size = new System.Drawing.Size(183, 38);
            this.btnReturnItemOnly.TabIndex = 296;
            this.btnReturnItemOnly.Text = "ارجاع الصنف المحدد فقط";
            this.btnReturnItemOnly.UseVisualStyleBackColor = true;
            this.btnReturnItemOnly.Click += new System.EventHandler(this.btnReturnItemOnly_Click);
            // 
            // btnReturnAll
            // 
            this.btnReturnAll.Location = new System.Drawing.Point(86, 107);
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.Size = new System.Drawing.Size(183, 38);
            this.btnReturnAll.TabIndex = 297;
            this.btnReturnAll.Text = "ارجاع الفاتورة بالكامل";
            this.btnReturnAll.UseVisualStyleBackColor = true;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // txtName1
            // 
            this.txtName1.EditValue = "";
            this.txtName1.Location = new System.Drawing.Point(91, 25);
            this.txtName1.Name = "txtName1";
            this.txtName1.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtName1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName1.Properties.Appearance.Options.UseBorderColor = true;
            this.txtName1.Properties.Appearance.Options.UseFont = true;
            this.txtName1.Properties.Appearance.Options.UseTextOptions = true;
            this.txtName1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtName1.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtName1.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName1.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtName1.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtName1.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtName1.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtName1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtName1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtName1.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtName1.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtName1.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtName1.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtName1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtName1.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtName1.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtName1.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtName1.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtName1.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtName1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName1.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtName1.Properties.Name = "textEdit1";
            this.txtName1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName1.Size = new System.Drawing.Size(142, 24);
            this.txtName1.TabIndex = 296;
            // 
            // txtName2
            // 
            this.txtName2.EditValue = "";
            this.txtName2.Location = new System.Drawing.Point(42, 25);
            this.txtName2.Name = "txtName2";
            this.txtName2.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtName2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName2.Properties.Appearance.Options.UseBorderColor = true;
            this.txtName2.Properties.Appearance.Options.UseFont = true;
            this.txtName2.Properties.Appearance.Options.UseTextOptions = true;
            this.txtName2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName2.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtName2.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtName2.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName2.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtName2.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtName2.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtName2.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtName2.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtName2.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtName2.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName2.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtName2.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtName2.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtName2.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtName2.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName2.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtName2.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtName2.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName2.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtName2.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtName2.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtName2.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtName2.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName2.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtName2.Properties.Name = "textEdit1";
            this.txtName2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName2.Size = new System.Drawing.Size(142, 24);
            this.txtName2.TabIndex = 297;
            // 
            // NudQty
            // 
            this.NudQty.EditValue = "";
            this.NudQty.Location = new System.Drawing.Point(231, 75);
            this.NudQty.Name = "NudQty";
            this.NudQty.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.NudQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudQty.Properties.Appearance.Options.UseBorderColor = true;
            this.NudQty.Properties.Appearance.Options.UseFont = true;
            this.NudQty.Properties.Appearance.Options.UseTextOptions = true;
            this.NudQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NudQty.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudQty.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.NudQty.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.NudQty.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudQty.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.NudQty.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.NudQty.Properties.AppearanceDisabled.Options.UseFont = true;
            this.NudQty.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.NudQty.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudQty.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.NudQty.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.NudQty.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudQty.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.NudQty.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.NudQty.Properties.AppearanceFocused.Options.UseFont = true;
            this.NudQty.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.NudQty.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudQty.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.NudQty.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.NudQty.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudQty.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.NudQty.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.NudQty.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.NudQty.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.NudQty.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.NudQty.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.NudQty.Properties.Name = "textEdit1";
            this.NudQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NudQty.Size = new System.Drawing.Size(105, 24);
            this.NudQty.TabIndex = 298;
            // 
            // rbtnReturnItemOnly
            // 
            this.rbtnReturnItemOnly.AutoSize = true;
            this.rbtnReturnItemOnly.Checked = true;
            this.rbtnReturnItemOnly.Location = new System.Drawing.Point(294, 30);
            this.rbtnReturnItemOnly.Name = "rbtnReturnItemOnly";
            this.rbtnReturnItemOnly.Size = new System.Drawing.Size(204, 22);
            this.rbtnReturnItemOnly.TabIndex = 296;
            this.rbtnReturnItemOnly.TabStop = true;
            this.rbtnReturnItemOnly.Text = "ارجاع الكمية المحدده بالكامل";
            this.rbtnReturnItemOnly.UseVisualStyleBackColor = true;
            // 
            // rbtnReturnQtyonly
            // 
            this.rbtnReturnQtyonly.AutoSize = true;
            this.rbtnReturnQtyonly.Location = new System.Drawing.Point(342, 76);
            this.rbtnReturnQtyonly.Name = "rbtnReturnQtyonly";
            this.rbtnReturnQtyonly.Size = new System.Drawing.Size(156, 22);
            this.rbtnReturnQtyonly.TabIndex = 297;
            this.rbtnReturnQtyonly.Text = "ارجاع جزء من المحدد";
            this.rbtnReturnQtyonly.UseVisualStyleBackColor = true;
            // 
            // txtMadfou3
            // 
            this.txtMadfou3.EditValue = "";
            this.txtMadfou3.Enabled = false;
            this.txtMadfou3.Location = new System.Drawing.Point(48, 21);
            this.txtMadfou3.Name = "txtMadfou3";
            this.txtMadfou3.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtMadfou3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMadfou3.Properties.Appearance.Options.UseBorderColor = true;
            this.txtMadfou3.Properties.Appearance.Options.UseFont = true;
            this.txtMadfou3.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMadfou3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMadfou3.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtMadfou3.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtMadfou3.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtMadfou3.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMadfou3.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtMadfou3.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtMadfou3.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtMadfou3.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtMadfou3.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMadfou3.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtMadfou3.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtMadfou3.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtMadfou3.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMadfou3.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtMadfou3.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtMadfou3.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtMadfou3.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtMadfou3.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtMadfou3.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtMadfou3.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtMadfou3.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtMadfou3.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtMadfou3.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtMadfou3.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtMadfou3.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtMadfou3.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMadfou3.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtMadfou3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtMadfou3.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtMadfou3.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtMadfou3.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtMadfou3.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtMadfou3.Properties.Name = "textEdit1";
            this.txtMadfou3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMadfou3.Size = new System.Drawing.Size(136, 24);
            this.txtMadfou3.TabIndex = 296;
            // 
            // txtTotalOrder
            // 
            this.txtTotalOrder.EditValue = "";
            this.txtTotalOrder.Enabled = false;
            this.txtTotalOrder.Location = new System.Drawing.Point(535, 21);
            this.txtTotalOrder.Name = "txtTotalOrder";
            this.txtTotalOrder.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalOrder.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalOrder.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalOrder.Properties.Appearance.Options.UseFont = true;
            this.txtTotalOrder.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalOrder.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalOrder.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalOrder.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalOrder.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalOrder.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalOrder.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalOrder.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalOrder.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalOrder.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalOrder.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalOrder.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalOrder.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalOrder.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalOrder.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalOrder.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalOrder.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalOrder.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalOrder.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalOrder.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalOrder.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalOrder.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalOrder.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalOrder.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalOrder.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalOrder.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalOrder.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalOrder.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalOrder.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalOrder.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalOrder.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalOrder.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtTotalOrder.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtTotalOrder.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtTotalOrder.Properties.Name = "textEdit1";
            this.txtTotalOrder.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalOrder.Size = new System.Drawing.Size(136, 24);
            this.txtTotalOrder.TabIndex = 297;
            // 
            // Frm_ReturnBuy
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(942, 690);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxSupplier);
            this.Controls.Add(this.DgvDteils);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_ReturnBuy";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة مرتجعات المشتريات";
            this.Load += new System.EventHandler(this.Frm_ReturnBuy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDteils)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMadfou3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalOrder.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox cbxSupplier;
        private System.Windows.Forms.DataGridView DgvDteils;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbxStore2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxStore1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblName1;
        private System.Windows.Forms.Label lblName2;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtID;
        private System.Windows.Forms.Button btnReturnItemOnly;
        private System.Windows.Forms.Button btnReturnAll;
        private DevExpress.XtraEditors.TextEdit NudQty;
        private DevExpress.XtraEditors.TextEdit txtName2;
        private DevExpress.XtraEditors.TextEdit txtName1;
        private System.Windows.Forms.RadioButton rbtnReturnItemOnly;
        private System.Windows.Forms.RadioButton rbtnReturnQtyonly;
        private DevExpress.XtraEditors.TextEdit txtMadfou3;
        private DevExpress.XtraEditors.TextEdit txtTotalOrder;
    }
}