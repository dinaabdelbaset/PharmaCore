namespace PharmaCore.POS.PL
{
    partial class Frm_StockBankTransfire
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMoneyBank = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMoneyStock = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStock = new System.Windows.Forms.ComboBox();
            this.NudPrice = new DevExpress.XtraEditors.TextEdit();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.rbtnFromStockTobank = new System.Windows.Forms.RadioButton();
            this.rbtnFromBankToStock = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(31, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 18);
            this.label6.TabIndex = 57;
            this.label6.Text = "اسم المسؤل عن التحويل:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(102, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 55;
            this.label4.Text = "تاريخ التحويل:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(61, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 53;
            this.label2.Text = "المبلغ المراد تحويلة:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMoneyBank);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblMoneyStock);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxStock);
            this.groupBox1.Location = new System.Drawing.Point(30, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 148);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // lblMoneyBank
            // 
            this.lblMoneyBank.AutoSize = true;
            this.lblMoneyBank.ForeColor = System.Drawing.Color.Blue;
            this.lblMoneyBank.Location = new System.Drawing.Point(124, 109);
            this.lblMoneyBank.Name = "lblMoneyBank";
            this.lblMoneyBank.Size = new System.Drawing.Size(23, 18);
            this.lblMoneyBank.TabIndex = 9;
            this.lblMoneyBank.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(214, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 18);
            this.label3.TabIndex = 48;
            this.label3.Text = "رصيد البنك الحالي هو:";
            // 
            // lblMoneyStock
            // 
            this.lblMoneyStock.AutoSize = true;
            this.lblMoneyStock.ForeColor = System.Drawing.Color.Blue;
            this.lblMoneyStock.Location = new System.Drawing.Point(124, 74);
            this.lblMoneyStock.Name = "lblMoneyStock";
            this.lblMoneyStock.Size = new System.Drawing.Size(23, 18);
            this.lblMoneyStock.TabIndex = 8;
            this.lblMoneyStock.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(222, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 31;
            this.label1.Text = "رصيد الخزنة المحددة:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(297, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 18);
            this.label5.TabIndex = 30;
            this.label5.Text = "اختر خزنة:";
            // 
            // cbxStock
            // 
            this.cbxStock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStock.FormattingEnabled = true;
            this.cbxStock.Location = new System.Drawing.Point(6, 23);
            this.cbxStock.Name = "cbxStock";
            this.cbxStock.Size = new System.Drawing.Size(276, 26);
            this.cbxStock.TabIndex = 1;
            this.cbxStock.SelectionChangeCommitted += new System.EventHandler(this.cbxStock_SelectionChangeCommitted);
            // 
            // NudPrice
            // 
            this.NudPrice.EditValue = "";
            this.NudPrice.Location = new System.Drawing.Point(209, 225);
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
            this.NudPrice.Size = new System.Drawing.Size(213, 24);
            this.NudPrice.TabIndex = 290;
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(209, 267);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(213, 26);
            this.DtpDate.TabIndex = 291;
            this.DtpDate.Visible = false;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(209, 314);
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
            this.txtName.Size = new System.Drawing.Size(213, 24);
            this.txtName.TabIndex = 292;
            // 
            // rbtnFromStockTobank
            // 
            this.rbtnFromStockTobank.AutoSize = true;
            this.rbtnFromStockTobank.Checked = true;
            this.rbtnFromStockTobank.Location = new System.Drawing.Point(34, 184);
            this.rbtnFromStockTobank.Name = "rbtnFromStockTobank";
            this.rbtnFromStockTobank.Size = new System.Drawing.Size(191, 22);
            this.rbtnFromStockTobank.TabIndex = 282;
            this.rbtnFromStockTobank.TabStop = true;
            this.rbtnFromStockTobank.Text = "تحويل من الخزنة الى البنك";
            this.rbtnFromStockTobank.UseVisualStyleBackColor = true;
            // 
            // rbtnFromBankToStock
            // 
            this.rbtnFromBankToStock.AutoSize = true;
            this.rbtnFromBankToStock.Location = new System.Drawing.Point(231, 184);
            this.rbtnFromBankToStock.Name = "rbtnFromBankToStock";
            this.rbtnFromBankToStock.Size = new System.Drawing.Size(191, 22);
            this.rbtnFromBankToStock.TabIndex = 283;
            this.rbtnFromBankToStock.Text = "تحويل من البنك الى الخزنة";
            this.rbtnFromBankToStock.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(158, 366);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 38);
            this.btnAdd.TabIndex = 293;
            this.btnAdd.Text = "إضافة ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Frm_StockBankTransfire
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(454, 428);
            this.Controls.Add(this.rbtnFromBankToStock);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.rbtnFromStockTobank);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.NudPrice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StockBankTransfire";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تحويل رصيد من الخزنة الى البنك والعكس";
            this.Load += new System.EventHandler(this.Frm_StockBankTransfire_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMoneyBank;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMoneyStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxStock;
        private DevExpress.XtraEditors.TextEdit NudPrice;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private DevExpress.XtraEditors.TextEdit txtName;
        private System.Windows.Forms.RadioButton rbtnFromBankToStock;
        private System.Windows.Forms.RadioButton rbtnFromStockTobank;
        private System.Windows.Forms.Button btnAdd;
    }
}