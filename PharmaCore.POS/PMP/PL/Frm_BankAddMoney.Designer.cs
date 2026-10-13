namespace PharmaCore.POS.PL
{
    partial class Frm_BankAddMoney
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NudPrice = new DevExpress.XtraEditors.TextEdit();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtreason = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreason.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(66, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 18);
            this.label7.TabIndex = 45;
            this.label7.Text = "سبب الايداع:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(69, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 18);
            this.label6.TabIndex = 44;
            this.label6.Text = "اسم المودع:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(72, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 18);
            this.label4.TabIndex = 41;
            this.label4.Text = "تاريخ الايداع:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 18);
            this.label2.TabIndex = 39;
            this.label2.Text = "اضافة رصيد جديد:";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.ForeColor = System.Drawing.Color.Red;
            this.lblMoney.Location = new System.Drawing.Point(235, 27);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(23, 18);
            this.lblMoney.TabIndex = 6;
            this.lblMoney.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(37, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 23);
            this.label3.TabIndex = 37;
            this.label3.Text = "رصيد البنك الحالي هو:";
            // 
            // NudPrice
            // 
            this.NudPrice.EditValue = "";
            this.NudPrice.Enabled = false;
            this.NudPrice.Location = new System.Drawing.Point(162, 82);
            this.NudPrice.Name = "NudPrice";
            this.NudPrice.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.NudPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.Appearance.Options.UseBorderColor = true;
            this.NudPrice.Properties.Appearance.Options.UseFont = true;
            this.NudPrice.Properties.Appearance.Options.UseTextOptions = true;
            this.NudPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NudPrice.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.NudPrice.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.NudPrice.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NudPrice.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.NudPrice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.NudPrice.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.NudPrice.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.NudPrice.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.NudPrice.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.NudPrice.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.NudPrice.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.NudPrice.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.NudPrice.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.NudPrice.Properties.Name = "textEdit1";
            this.NudPrice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NudPrice.Size = new System.Drawing.Size(195, 24);
            this.NudPrice.TabIndex = 261;
            // 
            // DtpDate
            // 
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpDate.Location = new System.Drawing.Point(162, 134);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DtpDate.Size = new System.Drawing.Size(195, 26);
            this.DtpDate.TabIndex = 262;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(162, 186);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.Appearance.Options.UseBorderColor = true;
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.Appearance.Options.UseTextOptions = true;
            this.txtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtName.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtName.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtName.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtName.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtName.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtName.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtName.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtName.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtName.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtName.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtName.Properties.Name = "textEdit1";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(195, 24);
            this.txtName.TabIndex = 263;
            // 
            // txtreason
            // 
            this.txtreason.EditValue = "";
            this.txtreason.Enabled = false;
            this.txtreason.Location = new System.Drawing.Point(162, 252);
            this.txtreason.Name = "txtreason";
            this.txtreason.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtreason.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtreason.Properties.Appearance.Options.UseBorderColor = true;
            this.txtreason.Properties.Appearance.Options.UseFont = true;
            this.txtreason.Properties.Appearance.Options.UseTextOptions = true;
            this.txtreason.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtreason.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtreason.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtreason.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtreason.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtreason.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtreason.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtreason.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtreason.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtreason.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtreason.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtreason.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtreason.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtreason.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtreason.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtreason.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtreason.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtreason.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtreason.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtreason.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtreason.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtreason.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtreason.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtreason.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtreason.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtreason.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtreason.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtreason.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtreason.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtreason.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtreason.Properties.Name = "textEdit1";
            this.txtreason.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtreason.Size = new System.Drawing.Size(195, 24);
            this.txtreason.TabIndex = 264;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(174, 316);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(143, 53);
            this.btnAdd.TabIndex = 266;
            this.btnAdd.Text = "إضافة ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Frm_BankAddMoney
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(390, 396);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtreason);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.NudPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BankAddMoney";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إيداع رصيد في البنك";
            this.Load += new System.EventHandler(this.Frm_BankAddMoney_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtreason.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit NudPrice;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtreason;
        private System.Windows.Forms.Button btnAdd;
    }
}