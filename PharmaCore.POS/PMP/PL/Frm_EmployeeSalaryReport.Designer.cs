namespace PharmaCore.POS.PL
{
    partial class Frm_EmployeeSalaryReport
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
            this.label3 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxEmployee = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtnOneEmp = new System.Windows.Forms.RadioButton();
            this.rbtnAllEmp = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(626, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 51;
            this.label3.Text = "اجمالي المرتبات:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.DgvSearch.Location = new System.Drawing.Point(15, 135);
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
            this.DgvSearch.Size = new System.Drawing.Size(945, 297);
            this.DgvSearch.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(340, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 29);
            this.label2.TabIndex = 52;
            this.label2.Text = "اجمالي صرف المرتبات فى فترة";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DtpTo);
            this.groupBox1.Controls.Add(this.DtpFrom);
            this.groupBox1.Controls.Add(this.cbxEmployee);
            this.groupBox1.Controls.Add(this.rbtnOneEmp);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.rbtnAllEmp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(945, 73);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // cbxEmployee
            // 
            this.cbxEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEmployee.FormattingEnabled = true;
            this.cbxEmployee.Location = new System.Drawing.Point(524, 30);
            this.cbxEmployee.Name = "cbxEmployee";
            this.cbxEmployee.Size = new System.Drawing.Size(172, 26);
            this.cbxEmployee.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(274, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "الى:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(463, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "من:";
            // 
            // rbtnOneEmp
            // 
            this.rbtnOneEmp.AutoSize = true;
            this.rbtnOneEmp.Location = new System.Drawing.Point(713, 32);
            this.rbtnOneEmp.Name = "rbtnOneEmp";
            this.rbtnOneEmp.Size = new System.Drawing.Size(103, 22);
            this.rbtnOneEmp.TabIndex = 280;
            this.rbtnOneEmp.TabStop = true;
            this.rbtnOneEmp.Text = "موظف محدد";
            this.rbtnOneEmp.UseVisualStyleBackColor = true;
            this.rbtnOneEmp.CheckedChanged += new System.EventHandler(this.rbtnOneEmp_CheckedChanged);
            // 
            // rbtnAllEmp
            // 
            this.rbtnAllEmp.AutoSize = true;
            this.rbtnAllEmp.Location = new System.Drawing.Point(832, 32);
            this.rbtnAllEmp.Name = "rbtnAllEmp";
            this.rbtnAllEmp.Size = new System.Drawing.Size(107, 22);
            this.rbtnAllEmp.TabIndex = 281;
            this.rbtnAllEmp.TabStop = true;
            this.rbtnAllEmp.Text = "كل الموظفين";
            this.rbtnAllEmp.UseVisualStyleBackColor = true;
            this.rbtnAllEmp.CheckedChanged += new System.EventHandler(this.rbtnAllEmp_CheckedChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(19, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(88, 38);
            this.btnSearch.TabIndex = 282;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(178, 448);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(202, 38);
            this.btnDeleteAll.TabIndex = 283;
            this.btnDeleteAll.Text = "مسح كل ما بين فترتي التاريخ ";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(30, 448);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(113, 38);
            this.btnDelete.TabIndex = 284;
            this.btnDelete.Text = "مسح";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(755, 446);
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
            this.txtTotal.TabIndex = 285;
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(125, 28);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(143, 26);
            this.DtpTo.TabIndex = 286;
            this.DtpTo.Visible = false;
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(316, 27);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(143, 26);
            this.DtpFrom.TabIndex = 287;
            this.DtpFrom.Visible = false;
            // 
            // Frm_EmployeeSalaryReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(974, 507);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EmployeeSalaryReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير صرف المرتبات";
            this.Load += new System.EventHandler(this.Frm_EmployeeSalaryReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnOneEmp;
        private System.Windows.Forms.RadioButton rbtnAllEmp;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnDelete;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private System.Windows.Forms.DateTimePicker DtpFrom;
    }
}