namespace PharmaCore.POS.PL
{
    partial class Frm_ProductsOutStore
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStoreFrom = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxProducts = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.NudQty = new DevExpress.XtraEditors.TextEdit();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtReason);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Controls.Add(this.DtpDate);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(514, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(423, 253);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "بيانات العملية";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(271, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 18);
            this.label11.TabIndex = 24;
            this.label11.Text = "الاسباب:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(271, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 18);
            this.label10.TabIndex = 18;
            this.label10.Text = "المسؤل عن الاخراج:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(272, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "التاريخ:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBarcode);
            this.groupBox1.Controls.Add(this.NudQty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxUnit);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxStoreFrom);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxProducts);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 253);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات المخزن";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(305, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 17;
            this.label4.Text = "الوحدة:";
            // 
            // cbxUnit
            // 
            this.cbxUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUnit.FormattingEnabled = true;
            this.cbxUnit.Location = new System.Drawing.Point(61, 165);
            this.cbxUnit.Name = "cbxUnit";
            this.cbxUnit.Size = new System.Drawing.Size(242, 26);
            this.cbxUnit.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(305, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "الكمية:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(304, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 18);
            this.label1.TabIndex = 13;
            this.label1.Text = "اسم المخزن :";
            // 
            // cbxStoreFrom
            // 
            this.cbxStoreFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStoreFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStoreFrom.FormattingEnabled = true;
            this.cbxStoreFrom.Location = new System.Drawing.Point(61, 119);
            this.cbxStoreFrom.Name = "cbxStoreFrom";
            this.cbxStoreFrom.Size = new System.Drawing.Size(242, 26);
            this.cbxStoreFrom.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(305, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "المنتج:";
            // 
            // cbxProducts
            // 
            this.cbxProducts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxProducts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProducts.FormattingEnabled = true;
            this.cbxProducts.Location = new System.Drawing.Point(61, 77);
            this.cbxProducts.Name = "cbxProducts";
            this.cbxProducts.Size = new System.Drawing.Size(242, 26);
            this.cbxProducts.TabIndex = 2;
            this.cbxProducts.SelectionChangeCommitted += new System.EventHandler(this.cbxProducts_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(305, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "الباركود:";
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(131, 36);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(134, 26);
            this.DtpDate.TabIndex = 281;
            this.DtpDate.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(116, 196);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(183, 38);
            this.btnAdd.TabIndex = 286;
            this.btnAdd.Text = "اتمام العملية";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // NudQty
            // 
            this.NudQty.EditValue = "";
            this.NudQty.Location = new System.Drawing.Point(61, 206);
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
            this.NudQty.Size = new System.Drawing.Size(242, 24);
            this.NudQty.TabIndex = 18;
            // 
            // txtBarcode
            // 
            this.txtBarcode.EditValue = "";
            this.txtBarcode.Location = new System.Drawing.Point(61, 35);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBarcode.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtBarcode.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtBarcode.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtBarcode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtBarcode.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtBarcode.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtBarcode.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBarcode.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtBarcode.Properties.Name = "textEdit1";
            this.txtBarcode.Size = new System.Drawing.Size(242, 24);
            this.txtBarcode.TabIndex = 19;
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(23, 81);
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
            this.txtName.Size = new System.Drawing.Size(242, 24);
            this.txtName.TabIndex = 20;
            // 
            // txtReason
            // 
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReason.Location = new System.Drawing.Point(23, 111);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(242, 70);
            this.txtReason.TabIndex = 287;
            // 
            // Frm_ProductsOutStore
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(964, 287);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ProductsOutStore";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اخراج المنتجات التالفه من المخزن";
            this.Load += new System.EventHandler(this.Frm_ProductsOutStore_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStoreFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxProducts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.TextEdit NudQty;
        private System.Windows.Forms.TextBox txtReason;
    }
}