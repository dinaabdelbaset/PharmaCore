namespace PharmaCore.POS.PL
{
    partial class Frm_EmployeeBorrowMoney
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxEmployee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtname = new DevExpress.XtraEditors.TextEdit();
            this.txtCreditor = new DevExpress.XtraEditors.TextEdit();
            this.NudPrice = new DevExpress.XtraEditors.TextEdit();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.DtpReminder = new System.Windows.Forms.DateTimePicker();
            this.rbtnEmployee = new System.Windows.Forms.RadioButton();
            this.rbtnNormal = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(331, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 25);
            this.label6.TabIndex = 28;
            this.label6.Text = "السلفيات";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNotes);
            this.groupBox1.Controls.Add(this.rbtnNormal);
            this.groupBox1.Controls.Add(this.rbtnEmployee);
            this.groupBox1.Controls.Add(this.DtpReminder);
            this.groupBox1.Controls.Add(this.DtpDate);
            this.groupBox1.Controls.Add(this.NudPrice);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtCreditor);
            this.groupBox1.Controls.Add(this.txtname);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxEmployee);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(24, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(711, 260);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات السلف";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(254, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 18);
            this.label9.TabIndex = 19;
            this.label9.Text = "ملاحظات:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(254, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "تاريخ السلف:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(604, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "اسم الشخص:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(604, 174);
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
            this.cbxEmployee.Location = new System.Drawing.Point(408, 171);
            this.cbxEmployee.Name = "cbxEmployee";
            this.cbxEmployee.Size = new System.Drawing.Size(195, 26);
            this.cbxEmployee.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(609, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "اسم الدائن:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(254, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "تاريخ الاستحقاق:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(254, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "المبلغ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(604, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "رقم العملية:";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(457, 34);
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
            this.txtID.Size = new System.Drawing.Size(141, 24);
            this.txtID.TabIndex = 286;
            // 
            // txtname
            // 
            this.txtname.EditValue = "";
            this.txtname.Location = new System.Drawing.Point(408, 123);
            this.txtname.Name = "txtname";
            this.txtname.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtname.Properties.Appearance.Options.UseBorderColor = true;
            this.txtname.Properties.Appearance.Options.UseFont = true;
            this.txtname.Properties.Appearance.Options.UseTextOptions = true;
            this.txtname.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtname.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtname.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtname.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtname.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtname.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtname.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtname.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtname.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtname.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtname.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtname.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtname.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtname.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtname.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtname.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtname.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtname.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtname.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtname.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtname.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtname.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtname.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtname.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtname.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtname.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtname.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtname.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtname.Properties.Name = "textEdit1";
            this.txtname.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtname.Size = new System.Drawing.Size(190, 24);
            this.txtname.TabIndex = 287;
            // 
            // txtCreditor
            // 
            this.txtCreditor.EditValue = "";
            this.txtCreditor.Location = new System.Drawing.Point(408, 215);
            this.txtCreditor.Name = "txtCreditor";
            this.txtCreditor.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtCreditor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCreditor.Properties.Appearance.Options.UseBorderColor = true;
            this.txtCreditor.Properties.Appearance.Options.UseFont = true;
            this.txtCreditor.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCreditor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtCreditor.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCreditor.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtCreditor.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtCreditor.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCreditor.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCreditor.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtCreditor.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtCreditor.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtCreditor.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCreditor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtCreditor.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtCreditor.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCreditor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtCreditor.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtCreditor.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtCreditor.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtCreditor.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCreditor.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtCreditor.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtCreditor.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtCreditor.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtCreditor.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtCreditor.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtCreditor.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtCreditor.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtCreditor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCreditor.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtCreditor.Properties.Name = "textEdit1";
            this.txtCreditor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCreditor.Size = new System.Drawing.Size(195, 24);
            this.txtCreditor.TabIndex = 288;
            // 
            // NudPrice
            // 
            this.NudPrice.EditValue = "";
            this.NudPrice.Location = new System.Drawing.Point(6, 130);
            this.NudPrice.Name = "NudPrice";
            this.NudPrice.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.NudPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.Appearance.Options.UseBorderColor = true;
            this.NudPrice.Properties.Appearance.Options.UseFont = true;
            this.NudPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.NudPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NudPrice.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.NudPrice.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.NudPrice.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.NudPrice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.NudPrice.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.NudPrice.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.NudPrice.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.NudPrice.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.NudPrice.Properties.Name = "textEdit1";
            this.NudPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NudPrice.Size = new System.Drawing.Size(242, 24);
            this.NudPrice.TabIndex = 289;
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(6, 25);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(242, 26);
            this.DtpDate.TabIndex = 290;
            this.DtpDate.Visible = false;
            // 
            // DtpReminder
            // 
            this.DtpReminder.CustomFormat = "yyyy-MM-dd";
            this.DtpReminder.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpReminder.Location = new System.Drawing.Point(6, 75);
            this.DtpReminder.Name = "DtpReminder";
            this.DtpReminder.RightToLeftLayout = true;
            this.DtpReminder.Size = new System.Drawing.Size(242, 26);
            this.DtpReminder.TabIndex = 291;
            this.DtpReminder.Visible = false;
            // 
            // rbtnEmployee
            // 
            this.rbtnEmployee.AutoSize = true;
            this.rbtnEmployee.Location = new System.Drawing.Point(448, 79);
            this.rbtnEmployee.Name = "rbtnEmployee";
            this.rbtnEmployee.Size = new System.Drawing.Size(65, 22);
            this.rbtnEmployee.TabIndex = 292;
            this.rbtnEmployee.TabStop = true;
            this.rbtnEmployee.Text = "موظف";
            this.rbtnEmployee.UseVisualStyleBackColor = true;
            this.rbtnEmployee.CheckedChanged += new System.EventHandler(this.rbtnEmployee_CheckedChanged);
            // 
            // rbtnNormal
            // 
            this.rbtnNormal.AutoSize = true;
            this.rbtnNormal.Location = new System.Drawing.Point(577, 79);
            this.rbtnNormal.Name = "rbtnNormal";
            this.rbtnNormal.Size = new System.Drawing.Size(106, 22);
            this.rbtnNormal.TabIndex = 293;
            this.rbtnNormal.TabStop = true;
            this.rbtnNormal.Text = "شخص عادي";
            this.rbtnNormal.UseVisualStyleBackColor = true;
            this.rbtnNormal.CheckedChanged += new System.EventHandler(this.rbtnNormal_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(286, 305);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 38);
            this.btnAdd.TabIndex = 290;
            this.btnAdd.Text = "حفظ العملية";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Location = new System.Drawing.Point(6, 171);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(242, 79);
            this.txtNotes.TabIndex = 294;
            this.txtNotes.TextChanged += new System.EventHandler(this.txtNotes_TextChanged);
            // 
            // Frm_EmployeeBorrowMoney
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(755, 363);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EmployeeBorrowMoney";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "السلفيات";
            this.Load += new System.EventHandler(this.Frm_EmployeeBorrowMoney_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxEmployee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtCreditor;
        private DevExpress.XtraEditors.TextEdit txtname;
        private DevExpress.XtraEditors.TextEdit NudPrice;
        private System.Windows.Forms.DateTimePicker DtpReminder;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.RadioButton rbtnNormal;
        private System.Windows.Forms.RadioButton rbtnEmployee;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtNotes;
    }
}