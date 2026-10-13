namespace PharmaCore.POS.PL
{
    partial class Frm_EmployeeSalary
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
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxEmployee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.DtpReminder = new System.Windows.Forms.DateTimePicker();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalSalary = new DevExpress.XtraEditors.TextEdit();
            this.txtToalBorrow = new DevExpress.XtraEditors.TextEdit();
            this.txtSafySalary = new DevExpress.XtraEditors.TextEdit();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSalary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToalBorrow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafySalary.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(271, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 25);
            this.label6.TabIndex = 31;
            this.label6.Text = "صرف مرتبات الموظفين";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNotes);
            this.groupBox1.Controls.Add(this.txtSafySalary);
            this.groupBox1.Controls.Add(this.txtToalBorrow);
            this.groupBox1.Controls.Add(this.txtTotalSalary);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.DtpReminder);
            this.groupBox1.Controls.Add(this.DtpDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxEmployee);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 202);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "اذن صرف مرتب";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(591, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "صافي المرتب:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(591, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "المسحوبات والسلف:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(212, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 18);
            this.label9.TabIndex = 19;
            this.label9.Text = "ملاحظات:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(211, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "تاريخ اذن الصرف:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(211, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "اختر موظف:";
            // 
            // cbxEmployee
            // 
            this.cbxEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEmployee.FormattingEnabled = true;
            this.cbxEmployee.Location = new System.Drawing.Point(15, 17);
            this.cbxEmployee.Name = "cbxEmployee";
            this.cbxEmployee.Size = new System.Drawing.Size(195, 26);
            this.cbxEmployee.TabIndex = 1;
            this.cbxEmployee.SelectionChangeCommitted += new System.EventHandler(this.cbxEmployee_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(591, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "المرتب كامل:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(212, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "تاريخ الاستحقاق:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(591, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم العملية:";
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(15, 56);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(195, 26);
            this.DtpDate.TabIndex = 281;
            this.DtpDate.Visible = false;
            // 
            // DtpReminder
            // 
            this.DtpReminder.CustomFormat = "yyyy-MM-dd";
            this.DtpReminder.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpReminder.Location = new System.Drawing.Point(15, 92);
            this.DtpReminder.Name = "DtpReminder";
            this.DtpReminder.RightToLeftLayout = true;
            this.DtpReminder.Size = new System.Drawing.Size(195, 26);
            this.DtpReminder.TabIndex = 282;
            this.DtpReminder.Visible = false;
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(483, 19);
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
            this.txtID.Size = new System.Drawing.Size(102, 24);
            this.txtID.TabIndex = 286;
            // 
            // txtTotalSalary
            // 
            this.txtTotalSalary.EditValue = "";
            this.txtTotalSalary.Enabled = false;
            this.txtTotalSalary.Location = new System.Drawing.Point(435, 61);
            this.txtTotalSalary.Name = "txtTotalSalary";
            this.txtTotalSalary.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalSalary.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSalary.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalSalary.Properties.Appearance.Options.UseFont = true;
            this.txtTotalSalary.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalSalary.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSalary.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSalary.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalSalary.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalSalary.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSalary.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalSalary.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalSalary.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalSalary.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalSalary.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSalary.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSalary.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalSalary.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalSalary.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSalary.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalSalary.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalSalary.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalSalary.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalSalary.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSalary.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalSalary.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalSalary.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSalary.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalSalary.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalSalary.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalSalary.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalSalary.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSalary.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSalary.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalSalary.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalSalary.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtTotalSalary.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtTotalSalary.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtTotalSalary.Properties.Name = "textEdit1";
            this.txtTotalSalary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalSalary.Size = new System.Drawing.Size(150, 24);
            this.txtTotalSalary.TabIndex = 287;
            // 
            // txtToalBorrow
            // 
            this.txtToalBorrow.EditValue = "";
            this.txtToalBorrow.Enabled = false;
            this.txtToalBorrow.Location = new System.Drawing.Point(435, 107);
            this.txtToalBorrow.Name = "txtToalBorrow";
            this.txtToalBorrow.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtToalBorrow.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtToalBorrow.Properties.Appearance.Options.UseBorderColor = true;
            this.txtToalBorrow.Properties.Appearance.Options.UseFont = true;
            this.txtToalBorrow.Properties.Appearance.Options.UseTextOptions = true;
            this.txtToalBorrow.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtToalBorrow.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtToalBorrow.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtToalBorrow.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtToalBorrow.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtToalBorrow.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtToalBorrow.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtToalBorrow.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtToalBorrow.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtToalBorrow.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtToalBorrow.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtToalBorrow.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtToalBorrow.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtToalBorrow.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtToalBorrow.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtToalBorrow.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtToalBorrow.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtToalBorrow.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtToalBorrow.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtToalBorrow.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtToalBorrow.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtToalBorrow.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtToalBorrow.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtToalBorrow.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtToalBorrow.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtToalBorrow.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtToalBorrow.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtToalBorrow.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtToalBorrow.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtToalBorrow.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtToalBorrow.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtToalBorrow.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtToalBorrow.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtToalBorrow.Properties.Name = "textEdit1";
            this.txtToalBorrow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtToalBorrow.Size = new System.Drawing.Size(150, 24);
            this.txtToalBorrow.TabIndex = 288;
            // 
            // txtSafySalary
            // 
            this.txtSafySalary.EditValue = "";
            this.txtSafySalary.Enabled = false;
            this.txtSafySalary.Location = new System.Drawing.Point(435, 154);
            this.txtSafySalary.Name = "txtSafySalary";
            this.txtSafySalary.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtSafySalary.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSafySalary.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSafySalary.Properties.Appearance.Options.UseFont = true;
            this.txtSafySalary.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSafySalary.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSafySalary.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSafySalary.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtSafySalary.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtSafySalary.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSafySalary.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtSafySalary.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtSafySalary.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtSafySalary.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtSafySalary.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSafySalary.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSafySalary.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtSafySalary.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtSafySalary.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSafySalary.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSafySalary.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtSafySalary.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtSafySalary.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtSafySalary.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSafySalary.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtSafySalary.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtSafySalary.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSafySalary.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtSafySalary.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtSafySalary.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtSafySalary.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtSafySalary.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSafySalary.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSafySalary.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSafySalary.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtSafySalary.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.txtSafySalary.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.txtSafySalary.Properties.MaskSettings.Set("mask", "\\d+");
            this.txtSafySalary.Properties.Name = "textEdit1";
            this.txtSafySalary.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSafySalary.Size = new System.Drawing.Size(150, 24);
            this.txtSafySalary.TabIndex = 289;
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Location = new System.Drawing.Point(15, 130);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(195, 58);
            this.txtNotes.TabIndex = 290;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(276, 261);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 38);
            this.btnAdd.TabIndex = 291;
            this.btnAdd.Text = "حفظ العملية";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Frm_EmployeeSalary
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(746, 311);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EmployeeSalary";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "صرف مرتبات الموظفين";
            this.Load += new System.EventHandler(this.Frm_EmployeeSalary_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSalary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToalBorrow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafySalary.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxEmployee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DtpReminder;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private DevExpress.XtraEditors.TextEdit txtSafySalary;
        private DevExpress.XtraEditors.TextEdit txtToalBorrow;
        private DevExpress.XtraEditors.TextEdit txtTotalSalary;
        private DevExpress.XtraEditors.TextEdit txtID;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnAdd;
    }
}