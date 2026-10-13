namespace PharmaCore.POS.PL
{
    partial class Frm_StockTransfire
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
            this.lblMoney2 = new System.Windows.Forms.Label();
            this.lblMoney1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStockTo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStockFrom = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtreason = new System.Windows.Forms.TextBox();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.NudPrice = new DevExpress.XtraEditors.TextEdit();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMoney2
            // 
            this.lblMoney2.AutoSize = true;
            this.lblMoney2.ForeColor = System.Drawing.Color.Blue;
            this.lblMoney2.Location = new System.Drawing.Point(182, 137);
            this.lblMoney2.Name = "lblMoney2";
            this.lblMoney2.Size = new System.Drawing.Size(23, 18);
            this.lblMoney2.TabIndex = 9;
            this.lblMoney2.Text = "...";
            // 
            // lblMoney1
            // 
            this.lblMoney1.AutoSize = true;
            this.lblMoney1.ForeColor = System.Drawing.Color.Blue;
            this.lblMoney1.Location = new System.Drawing.Point(182, 61);
            this.lblMoney1.Name = "lblMoney1";
            this.lblMoney1.Size = new System.Drawing.Size(23, 18);
            this.lblMoney1.TabIndex = 8;
            this.lblMoney1.Text = "...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(125, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 18);
            this.label8.TabIndex = 62;
            this.label8.Text = "الرصيد:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(125, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 61;
            this.label3.Text = "الرصيد:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(80, 355);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 18);
            this.label7.TabIndex = 59;
            this.label7.Text = "سبب التحويل:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(5, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 18);
            this.label6.TabIndex = 58;
            this.label6.Text = "اسم المسؤل عن التحويل:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(86, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 55;
            this.label4.Text = "تاريخ التحويل:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(45, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 53;
            this.label2.Text = "المبلغ المراد تحويلة:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(68, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 18);
            this.label1.TabIndex = 51;
            this.label1.Text = "تحويل الى خزنة:";
            // 
            // cbxStockTo
            // 
            this.cbxStockTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStockTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStockTo.FormattingEnabled = true;
            this.cbxStockTo.Location = new System.Drawing.Point(185, 97);
            this.cbxStockTo.Name = "cbxStockTo";
            this.cbxStockTo.Size = new System.Drawing.Size(213, 26);
            this.cbxStockTo.TabIndex = 2;
            this.cbxStockTo.SelectionChangeCommitted += new System.EventHandler(this.cbxStockTo_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(72, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 18);
            this.label5.TabIndex = 49;
            this.label5.Text = "تحويل من خزنة:";
            // 
            // cbxStockFrom
            // 
            this.cbxStockFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStockFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStockFrom.FormattingEnabled = true;
            this.cbxStockFrom.Location = new System.Drawing.Point(185, 20);
            this.cbxStockFrom.Name = "cbxStockFrom";
            this.cbxStockFrom.Size = new System.Drawing.Size(213, 26);
            this.cbxStockFrom.TabIndex = 1;
            this.cbxStockFrom.SelectionChangeCommitted += new System.EventHandler(this.cbxStockFrom_SelectionChangeCommitted);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(141, 423);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 38);
            this.btnAdd.TabIndex = 286;
            this.btnAdd.Text = "تحويل";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtreason
            // 
            this.txtreason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtreason.Location = new System.Drawing.Point(185, 328);
            this.txtreason.Multiline = true;
            this.txtreason.Name = "txtreason";
            this.txtreason.Size = new System.Drawing.Size(213, 75);
            this.txtreason.TabIndex = 287;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(185, 289);
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
            this.txtName.TabIndex = 288;
            // 
            // NudPrice
            // 
            this.NudPrice.EditValue = "";
            this.NudPrice.Location = new System.Drawing.Point(185, 184);
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
            this.NudPrice.TabIndex = 289;
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(185, 231);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(213, 26);
            this.DtpDate.TabIndex = 290;
            this.DtpDate.Visible = false;
            // 
            // Frm_StockTransfire
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(441, 485);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.NudPrice);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtreason);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblMoney2);
            this.Controls.Add(this.lblMoney1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxStockTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxStockFrom);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StockTransfire";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تحويل رصيد بين الخزنات";
            this.Load += new System.EventHandler(this.Frm_StockTransfire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMoney2;
        private System.Windows.Forms.Label lblMoney1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStockTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxStockFrom;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtreason;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit NudPrice;
        private System.Windows.Forms.DateTimePicker DtpDate;
    }
}