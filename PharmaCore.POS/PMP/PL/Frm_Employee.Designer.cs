namespace PharmaCore.POS.PL
{
    partial class Frm_Employee
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DataEmp = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtNationalID = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            this.txtSalary = new DevExpress.XtraEditors.TextEdit();
            this.txtPhone2 = new DevExpress.XtraEditors.TextEdit();
            this.txtPhone1 = new DevExpress.XtraEditors.TextEdit();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.DtbDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNationalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_edit);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(269, 613);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 84);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "العمليات المتاحة";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.DtbDate);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtPhone2);
            this.groupBox1.Controls.Add(this.txtPhone1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.txtNationalID);
            this.groupBox1.Controls.Add(this.txtSalary);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(38, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 317);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات الموظف";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(694, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "نوع الوظيفة :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(263, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 18);
            this.label9.TabIndex = 18;
            this.label9.Text = "التليفون 2 :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(667, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "تاريخ الاستحقاق:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(263, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "التليفون 1 :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(260, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "ملاحظات و عنوان :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(692, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "الراتب الشهرى:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(263, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "البطاقة الشخصية:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(694, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "اسم الموظف:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(694, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم الموظف:";
            // 
            // DataEmp
            // 
            this.DataEmp.AllowUserToAddRows = false;
            this.DataEmp.AllowUserToDeleteRows = false;
            this.DataEmp.AllowUserToResizeColumns = false;
            this.DataEmp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataEmp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataEmp.BackgroundColor = System.Drawing.Color.White;
            this.DataEmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataEmp.DefaultCellStyle = dataGridViewCellStyle7;
            this.DataEmp.Location = new System.Drawing.Point(92, 349);
            this.DataEmp.Name = "DataEmp";
            this.DataEmp.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataEmp.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DataEmp.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DataEmp.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DataEmp.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DataEmp.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DataEmp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataEmp.Size = new System.Drawing.Size(740, 247);
            this.DataEmp.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(730, 622);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(127, 18);
            this.label8.TabIndex = 159;
            this.label8.Text = "اجمالي الموظفين :";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(15, 191);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(242, 67);
            this.txtAddress.TabIndex = 289;
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(426, 35);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.Appearance.Options.UseBorderColor = true;
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.Appearance.Options.UseTextOptions = true;
            this.txtID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtID.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtID.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtID.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtID.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtID.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtID.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtID.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtID.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtID.Properties.Name = "textEdit1";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtID.Size = new System.Drawing.Size(262, 24);
            this.txtID.TabIndex = 286;
            // 
            // txtNationalID
            // 
            this.txtNationalID.EditValue = "";
            this.txtNationalID.Location = new System.Drawing.Point(15, 35);
            this.txtNationalID.Name = "txtNationalID";
            this.txtNationalID.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtNationalID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtNationalID.Properties.Appearance.Options.UseBorderColor = true;
            this.txtNationalID.Properties.Appearance.Options.UseFont = true;
            this.txtNationalID.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNationalID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNationalID.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtNationalID.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtNationalID.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtNationalID.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtNationalID.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtNationalID.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtNationalID.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtNationalID.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtNationalID.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtNationalID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtNationalID.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtNationalID.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtNationalID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNationalID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtNationalID.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtNationalID.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtNationalID.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtNationalID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtNationalID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtNationalID.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtNationalID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtNationalID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtNationalID.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtNationalID.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtNationalID.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtNationalID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNationalID.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtNationalID.Properties.Name = "textEdit1";
            this.txtNationalID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNationalID.Size = new System.Drawing.Size(242, 24);
            this.txtNationalID.TabIndex = 290;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(426, 82);
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
            this.txtName.Size = new System.Drawing.Size(262, 24);
            this.txtName.TabIndex = 291;
            // 
            // txtType
            // 
            this.txtType.EditValue = "";
            this.txtType.Location = new System.Drawing.Point(426, 124);
            this.txtType.Name = "txtType";
            this.txtType.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtType.Properties.Appearance.Options.UseBorderColor = true;
            this.txtType.Properties.Appearance.Options.UseFont = true;
            this.txtType.Properties.Appearance.Options.UseTextOptions = true;
            this.txtType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtType.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtType.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtType.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtType.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtType.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtType.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtType.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtType.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtType.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtType.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtType.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtType.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtType.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtType.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtType.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtType.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtType.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtType.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtType.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtType.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtType.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtType.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtType.Properties.Name = "textEdit1";
            this.txtType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtType.Size = new System.Drawing.Size(264, 24);
            this.txtType.TabIndex = 292;
            // 
            // txtSalary
            // 
            this.txtSalary.EditValue = "";
            this.txtSalary.Location = new System.Drawing.Point(426, 167);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtSalary.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSalary.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSalary.Properties.Appearance.Options.UseFont = true;
            this.txtSalary.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSalary.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSalary.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSalary.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtSalary.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtSalary.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSalary.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtSalary.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtSalary.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtSalary.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtSalary.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSalary.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtSalary.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtSalary.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSalary.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSalary.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtSalary.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtSalary.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtSalary.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSalary.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtSalary.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtSalary.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSalary.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtSalary.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtSalary.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtSalary.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtSalary.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSalary.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSalary.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtSalary.Properties.Name = "textEdit1";
            this.txtSalary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSalary.Size = new System.Drawing.Size(264, 24);
            this.txtSalary.TabIndex = 293;
            // 
            // txtPhone2
            // 
            this.txtPhone2.EditValue = "";
            this.txtPhone2.Location = new System.Drawing.Point(15, 119);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtPhone2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone2.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPhone2.Properties.Appearance.Options.UseFont = true;
            this.txtPhone2.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPhone2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPhone2.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtPhone2.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtPhone2.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone2.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtPhone2.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtPhone2.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtPhone2.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtPhone2.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtPhone2.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtPhone2.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone2.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPhone2.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtPhone2.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtPhone2.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtPhone2.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone2.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtPhone2.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtPhone2.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone2.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtPhone2.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtPhone2.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtPhone2.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtPhone2.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPhone2.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtPhone2.Properties.Name = "textEdit1";
            this.txtPhone2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhone2.Size = new System.Drawing.Size(242, 24);
            this.txtPhone2.TabIndex = 294;
            // 
            // txtPhone1
            // 
            this.txtPhone1.EditValue = "";
            this.txtPhone1.Location = new System.Drawing.Point(15, 77);
            this.txtPhone1.Name = "txtPhone1";
            this.txtPhone1.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtPhone1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone1.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPhone1.Properties.Appearance.Options.UseFont = true;
            this.txtPhone1.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPhone1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPhone1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtPhone1.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtPhone1.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone1.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtPhone1.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtPhone1.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtPhone1.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtPhone1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtPhone1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtPhone1.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPhone1.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtPhone1.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtPhone1.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtPhone1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtPhone1.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtPhone1.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPhone1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtPhone1.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtPhone1.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtPhone1.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtPhone1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtPhone1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPhone1.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtPhone1.Properties.Name = "textEdit1";
            this.txtPhone1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhone1.Size = new System.Drawing.Size(242, 24);
            this.txtPhone1.TabIndex = 295;
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(170, 32);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(107, 38);
            this.btn_edit.TabIndex = 296;
            this.btn_edit.Text = "تعديل";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(30, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 38);
            this.btnDelete.TabIndex = 297;
            this.btnDelete.Text = "مسح";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(383, 263);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(107, 38);
            this.btnNew.TabIndex = 298;
            this.btnNew.Text = "إفراغ الحقول";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(235, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 38);
            this.btnSave.TabIndex = 299;
            this.btnSave.Text = "حفظ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(536, 263);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(107, 38);
            this.btnAdd.TabIndex = 300;
            this.btnAdd.Text = "إضافة ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(702, 646);
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
            this.txtTotal.TabIndex = 301;
            // 
            // DtbDate
            // 
            this.DtbDate.CustomFormat = "yyyy-MM-dd";
            this.DtbDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtbDate.Location = new System.Drawing.Point(633, 229);
            this.DtbDate.Name = "DtbDate";
            this.DtbDate.RightToLeftLayout = true;
            this.DtbDate.Size = new System.Drawing.Size(161, 26);
            this.DtbDate.TabIndex = 302;
            this.DtbDate.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(414, 229);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.RightToLeftLayout = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(161, 26);
            this.dateTimePicker1.TabIndex = 304;
            this.dateTimePicker1.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(449, 208);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 18);
            this.label11.TabIndex = 303;
            this.label11.Text = "تاريخ التعين:";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new System.Drawing.Point(15, 161);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.textEdit1.Properties.Appearance.Options.UseBorderColor = true;
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.textEdit1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.textEdit1.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.textEdit1.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.textEdit1.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.textEdit1.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.textEdit1.Properties.AppearanceDisabled.Options.UseFont = true;
            this.textEdit1.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.textEdit1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.textEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.textEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.textEdit1.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.textEdit1.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.textEdit1.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.textEdit1.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEdit1.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.textEdit1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.textEdit1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.textEdit1.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.textEdit1.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.textEdit1.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.textEdit1.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.textEdit1.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.textEdit1.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.textEdit1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.textEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.textEdit1.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.textEdit1.Properties.Name = "textEdit1";
            this.textEdit1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textEdit1.Size = new System.Drawing.Size(242, 24);
            this.textEdit1.TabIndex = 306;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(263, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 18);
            this.label12.TabIndex = 305;
            this.label12.Text = "الراتب اليومي:";
            // 
            // Frm_Employee
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(917, 719);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.DataEmp);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Frm_Employee";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ادخال بيانات موظف جديد";
            this.Load += new System.EventHandler(this.Frm_Employee_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNationalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSalary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataEmp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAddress;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtNationalID;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtType;
        private DevExpress.XtraEditors.TextEdit txtSalary;
        private DevExpress.XtraEditors.TextEdit txtPhone2;
        private DevExpress.XtraEditors.TextEdit txtPhone1;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker DtbDate;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.Label label12;
    }
}