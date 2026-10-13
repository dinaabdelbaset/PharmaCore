namespace PharmaCore.POS.PL
{
    partial class Frm_PaySale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PaySale));
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatloub = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaky = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMadfou3 = new System.Windows.Forms.TextBox();
            this.checkVisa = new System.Windows.Forms.CheckBox();
            this.checkPrint = new System.Windows.Forms.CheckBox();
            this.btnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.checkDiscount = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 50;
            this.label2.Text = "المطلوب :";
            // 
            // txtMatloub
            // 
            this.txtMatloub.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatloub.Location = new System.Drawing.Point(92, 31);
            this.txtMatloub.Multiline = true;
            this.txtMatloub.Name = "txtMatloub";
            this.txtMatloub.ReadOnly = true;
            this.txtMatloub.Size = new System.Drawing.Size(304, 55);
            this.txtMatloub.TabIndex = 49;
            this.txtMatloub.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(21, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 18);
            this.label1.TabIndex = 48;
            this.label1.Text = "المدفوع :";
            // 
            // txtBaky
            // 
            this.txtBaky.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaky.Location = new System.Drawing.Point(92, 201);
            this.txtBaky.Multiline = true;
            this.txtBaky.Name = "txtBaky";
            this.txtBaky.ReadOnly = true;
            this.txtBaky.Size = new System.Drawing.Size(304, 55);
            this.txtBaky.TabIndex = 47;
            this.txtBaky.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(27, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "الباقي : ";
            // 
            // txtMadfou3
            // 
            this.txtMadfou3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMadfou3.Location = new System.Drawing.Point(92, 117);
            this.txtMadfou3.Multiline = true;
            this.txtMadfou3.Name = "txtMadfou3";
            this.txtMadfou3.Size = new System.Drawing.Size(304, 55);
            this.txtMadfou3.TabIndex = 45;
            this.txtMadfou3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMadfou3.TextChanged += new System.EventHandler(this.txtMadfou3_TextChanged);
            this.txtMadfou3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMadfou3_KeyPress);
            // 
            // checkVisa
            // 
            this.checkVisa.AutoSize = true;
            this.checkVisa.Location = new System.Drawing.Point(153, 276);
            this.checkVisa.Name = "checkVisa";
            this.checkVisa.Size = new System.Drawing.Size(148, 22);
            this.checkVisa.TabIndex = 55;
            this.checkVisa.Text = "دفع عن طريق الفيزا";
            this.checkVisa.UseVisualStyleBackColor = true;
            // 
            // checkPrint
            // 
            this.checkPrint.AutoSize = true;
            this.checkPrint.Location = new System.Drawing.Point(153, 313);
            this.checkPrint.Name = "checkPrint";
            this.checkPrint.Size = new System.Drawing.Size(157, 22);
            this.checkPrint.TabIndex = 56;
            this.checkPrint.Text = "تجاهل طباعة الإيصال";
            this.checkPrint.UseVisualStyleBackColor = true;
            this.checkPrint.CheckedChanged += new System.EventHandler(this.checkPrint_CheckedChanged);
            // 
            // btnReturn
            // 
            this.btnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Appearance.Options.UseFont = true;
            this.btnReturn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.ImageOptions.Image")));
            this.btnReturn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnReturn.Location = new System.Drawing.Point(250, 388);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(113, 46);
            this.btnReturn.TabIndex = 52;
            this.btnReturn.Text = "رجوع";
            this.btnReturn.ToolTipTitle = "مفتاح الإختصار F12";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEnter.ImageOptions.Image")));
            this.btnEnter.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnEnter.Location = new System.Drawing.Point(59, 388);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(185, 46);
            this.btnEnter.TabIndex = 51;
            this.btnEnter.Text = "حفظ و طباعة";
            this.btnEnter.ToolTipTitle = "مفتاح الإختصار Enter";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // checkDiscount
            // 
            this.checkDiscount.AutoSize = true;
            this.checkDiscount.Location = new System.Drawing.Point(153, 351);
            this.checkDiscount.Name = "checkDiscount";
            this.checkDiscount.Size = new System.Drawing.Size(174, 22);
            this.checkDiscount.TabIndex = 57;
            this.checkDiscount.Text = "إظهار تفاصيل الخصومات";
            this.checkDiscount.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(132, 437);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 20);
            this.label9.TabIndex = 167;
            this.label9.Text = "Enter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(289, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 20);
            this.label6.TabIndex = 168;
            this.label6.Text = "Esc";
            // 
            // Frm_PaySale
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(408, 469);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkDiscount);
            this.Controls.Add(this.checkPrint);
            this.Controls.Add(this.checkVisa);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMatloub);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBaky);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMadfou3);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_PaySale";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_PaySale";
            this.Load += new System.EventHandler(this.Frm_PaySale_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_PaySale_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnReturn;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatloub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaky;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMadfou3;
        private System.Windows.Forms.CheckBox checkVisa;
        private System.Windows.Forms.CheckBox checkPrint;
        public System.Windows.Forms.CheckBox checkDiscount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
    }
}