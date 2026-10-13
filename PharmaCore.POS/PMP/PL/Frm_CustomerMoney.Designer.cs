namespace PharmaCore.POS.PL
{
    partial class Frm_CustomerMoney
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnPayPart = new System.Windows.Forms.RadioButton();
            this.NudPrice = new DevExpress.XtraEditors.TextEdit();
            this.rbtnPayAll = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rbtnOneCustomer = new System.Windows.Forms.RadioButton();
            this.rbtnAllCust = new System.Windows.Forms.RadioButton();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.btnPay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 38);
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // DtpDate
            // 
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpDate.Location = new System.Drawing.Point(15, 22);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(111, 26);
            this.DtpDate.TabIndex = 42;
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
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle3;
            this.DgvSearch.Location = new System.Drawing.Point(15, 147);
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
            this.DgvSearch.Size = new System.Drawing.Size(883, 274);
            this.DgvSearch.TabIndex = 37;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnPayPart);
            this.groupBox2.Controls.Add(this.NudPrice);
            this.groupBox2.Controls.Add(this.rbtnPayAll);
            this.groupBox2.Location = new System.Drawing.Point(15, 414);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 69);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // rbtnPayPart
            // 
            this.rbtnPayPart.AutoSize = true;
            this.rbtnPayPart.Location = new System.Drawing.Point(121, 27);
            this.rbtnPayPart.Name = "rbtnPayPart";
            this.rbtnPayPart.Size = new System.Drawing.Size(120, 22);
            this.rbtnPayPart.TabIndex = 271;
            this.rbtnPayPart.TabStop = true;
            this.rbtnPayPart.Text = "تسديد جزء منه";
            this.rbtnPayPart.UseVisualStyleBackColor = true;
            this.rbtnPayPart.CheckedChanged += new System.EventHandler(this.rbtnPayPart_CheckedChanged);
            // 
            // NudPrice
            // 
            this.NudPrice.EditValue = "";
            this.NudPrice.Enabled = false;
            this.NudPrice.Location = new System.Drawing.Point(14, 26);
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
            this.NudPrice.Size = new System.Drawing.Size(101, 24);
            this.NudPrice.TabIndex = 265;
            // 
            // rbtnPayAll
            // 
            this.rbtnPayAll.AutoSize = true;
            this.rbtnPayAll.Location = new System.Drawing.Point(247, 27);
            this.rbtnPayAll.Name = "rbtnPayAll";
            this.rbtnPayAll.Size = new System.Drawing.Size(142, 22);
            this.rbtnPayAll.TabIndex = 272;
            this.rbtnPayAll.TabStop = true;
            this.rbtnPayAll.Text = "تسديد المبلغ كامل";
            this.rbtnPayAll.UseVisualStyleBackColor = true;
            this.rbtnPayAll.CheckedChanged += new System.EventHandler(this.rbtnPayAll_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(594, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 36);
            this.label3.TabIndex = 38;
            this.label3.Text = "اجمالي المبالغ \r\nالمستحقة :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxCustomer);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.rbtnOneCustomer);
            this.groupBox1.Controls.Add(this.rbtnAllCust);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Location = new System.Drawing.Point(40, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(824, 84);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCustomer.Enabled = false;
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(416, 36);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(179, 26);
            this.cbxCustomer.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(274, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(113, 38);
            this.btnAdd.TabIndex = 267;
            this.btnAdd.Text = "بحث";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // rbtnOneCustomer
            // 
            this.rbtnOneCustomer.AutoSize = true;
            this.rbtnOneCustomer.Location = new System.Drawing.Point(616, 38);
            this.rbtnOneCustomer.Name = "rbtnOneCustomer";
            this.rbtnOneCustomer.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneCustomer.TabIndex = 270;
            this.rbtnOneCustomer.TabStop = true;
            this.rbtnOneCustomer.Text = "عميل محدد";
            this.rbtnOneCustomer.UseVisualStyleBackColor = true;
            // 
            // rbtnAllCust
            // 
            this.rbtnAllCust.AutoSize = true;
            this.rbtnAllCust.Location = new System.Drawing.Point(720, 38);
            this.rbtnAllCust.Name = "rbtnAllCust";
            this.rbtnAllCust.Size = new System.Drawing.Size(90, 22);
            this.rbtnAllCust.TabIndex = 269;
            this.rbtnAllCust.TabStop = true;
            this.rbtnAllCust.Text = "كل العملاء";
            this.rbtnAllCust.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(27, 26);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(169, 45);
            this.btnPrint.TabIndex = 268;
            this.btnPrint.Text = "طباعة تقرير مفصل";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(333, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(317, 23);
            this.label2.TabIndex = 34;
            this.label2.Text = "المبالغ المتبقية على العملاء -الآجل";
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(703, 436);
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
            this.txtTotal.TabIndex = 264;
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(443, 437);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(116, 41);
            this.btnPay.TabIndex = 266;
            this.btnPay.Text = "تسديد المحدد";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // Frm_CustomerMoney
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(912, 494);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_CustomerMoney";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "حسابات العملاء اجل";
            this.Load += new System.EventHandler(this.Frm_CustomerMoney_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit NudPrice;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.RadioButton rbtnAllCust;
        private System.Windows.Forms.RadioButton rbtnOneCustomer;
        private System.Windows.Forms.RadioButton rbtnPayPart;
        private System.Windows.Forms.RadioButton rbtnPayAll;
    }
}