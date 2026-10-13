namespace PharmaCore.POS.PL
{
    partial class Frm_BuyQty
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtSelas = new DevExpress.XtraEditors.TextEdit();
            this.txtBuyPrice = new DevExpress.XtraEditors.TextEdit();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.txtBouns = new DevExpress.XtraEditors.TextEdit();
            this.txtExpier = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBouns.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(31, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "البونص :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(31, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "الكمية :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 48;
            this.label1.Text = "الصلاحية :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 36);
            this.label4.TabIndex = 50;
            this.label4.Text = "سعر شراء\r\n الصيدلي :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(20, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 36);
            this.label5.TabIndex = 51;
            this.label5.Text = "سعر بيع\r\n الجمهور :";
            // 
            // btnEnter
            // 
            this.btnEnter.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Location = new System.Drawing.Point(132, 300);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(143, 53);
            this.btnEnter.TabIndex = 271;
            this.btnEnter.Text = "إضافة ";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtSelas
            // 
            this.txtSelas.EditValue = "";
            this.txtSelas.Enabled = false;
            this.txtSelas.Location = new System.Drawing.Point(115, 195);
            this.txtSelas.Name = "txtSelas";
            this.txtSelas.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtSelas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSelas.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSelas.Properties.Appearance.Options.UseFont = true;
            this.txtSelas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtSelas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSelas.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSelas.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtSelas.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtSelas.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSelas.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtSelas.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtSelas.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtSelas.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtSelas.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSelas.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSelas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtSelas.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtSelas.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSelas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSelas.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtSelas.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtSelas.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtSelas.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSelas.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtSelas.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtSelas.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtSelas.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtSelas.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtSelas.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtSelas.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtSelas.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtSelas.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtSelas.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSelas.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtSelas.Properties.Name = "textEdit1";
            this.txtSelas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSelas.Size = new System.Drawing.Size(195, 24);
            this.txtSelas.TabIndex = 270;
            // 
            // txtBuyPrice
            // 
            this.txtBuyPrice.EditValue = "";
            this.txtBuyPrice.Enabled = false;
            this.txtBuyPrice.Location = new System.Drawing.Point(115, 136);
            this.txtBuyPrice.Name = "txtBuyPrice";
            this.txtBuyPrice.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtBuyPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBuyPrice.Properties.Appearance.Options.UseBorderColor = true;
            this.txtBuyPrice.Properties.Appearance.Options.UseFont = true;
            this.txtBuyPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.txtBuyPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBuyPrice.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBuyPrice.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtBuyPrice.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtBuyPrice.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBuyPrice.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtBuyPrice.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtBuyPrice.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtBuyPrice.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtBuyPrice.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBuyPrice.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBuyPrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtBuyPrice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtBuyPrice.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBuyPrice.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBuyPrice.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtBuyPrice.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtBuyPrice.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtBuyPrice.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBuyPrice.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtBuyPrice.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtBuyPrice.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBuyPrice.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtBuyPrice.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtBuyPrice.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtBuyPrice.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtBuyPrice.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBuyPrice.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBuyPrice.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBuyPrice.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtBuyPrice.Properties.Name = "textEdit1";
            this.txtBuyPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBuyPrice.Size = new System.Drawing.Size(195, 24);
            this.txtBuyPrice.TabIndex = 269;
            // 
            // txtQty
            // 
            this.txtQty.EditValue = "";
            this.txtQty.Enabled = false;
            this.txtQty.Location = new System.Drawing.Point(115, 79);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtQty.Properties.Appearance.Options.UseBorderColor = true;
            this.txtQty.Properties.Appearance.Options.UseFont = true;
            this.txtQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtQty.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtQty.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtQty.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtQty.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtQty.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtQty.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtQty.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtQty.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtQty.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtQty.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtQty.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtQty.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtQty.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtQty.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtQty.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtQty.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtQty.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtQty.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtQty.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtQty.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtQty.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtQty.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtQty.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtQty.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtQty.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtQty.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtQty.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtQty.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtQty.Properties.Name = "textEdit1";
            this.txtQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtQty.Size = new System.Drawing.Size(195, 24);
            this.txtQty.TabIndex = 267;
            // 
            // txtBouns
            // 
            this.txtBouns.EditValue = "";
            this.txtBouns.Enabled = false;
            this.txtBouns.Location = new System.Drawing.Point(115, 259);
            this.txtBouns.Name = "txtBouns";
            this.txtBouns.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtBouns.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBouns.Properties.Appearance.Options.UseBorderColor = true;
            this.txtBouns.Properties.Appearance.Options.UseFont = true;
            this.txtBouns.Properties.Appearance.Options.UseTextOptions = true;
            this.txtBouns.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBouns.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBouns.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtBouns.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtBouns.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBouns.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtBouns.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtBouns.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtBouns.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtBouns.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBouns.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBouns.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtBouns.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtBouns.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBouns.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBouns.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtBouns.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtBouns.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtBouns.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBouns.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtBouns.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtBouns.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBouns.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtBouns.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtBouns.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtBouns.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtBouns.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBouns.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBouns.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBouns.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtBouns.Properties.Name = "textEdit1";
            this.txtBouns.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBouns.Size = new System.Drawing.Size(195, 24);
            this.txtBouns.TabIndex = 272;
            // 
            // txtExpier
            // 
            this.txtExpier.CustomFormat = "yyyy-MM-dd";
            this.txtExpier.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtExpier.Location = new System.Drawing.Point(115, 25);
            this.txtExpier.Name = "txtExpier";
            this.txtExpier.RightToLeftLayout = true;
            this.txtExpier.Size = new System.Drawing.Size(195, 26);
            this.txtExpier.TabIndex = 282;
            this.txtExpier.Visible = false;
            // 
            // Frm_BuyQty
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(343, 396);
            this.Controls.Add(this.txtExpier);
            this.Controls.Add(this.txtBouns);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtSelas);
            this.Controls.Add(this.txtBuyPrice);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BuyQty";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تعديل بيانات المنتج";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_BuyQty_FormClosing);
            this.Load += new System.EventHandler(this.Frm_BuyQty_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_BuyQty_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtSelas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBouns.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEnter;
        private DevExpress.XtraEditors.TextEdit txtSelas;
        private DevExpress.XtraEditors.TextEdit txtBuyPrice;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.TextEdit txtBouns;
        private System.Windows.Forms.DateTimePicker txtExpier;
    }
}