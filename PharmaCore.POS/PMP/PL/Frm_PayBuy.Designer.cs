namespace PharmaCore.POS.PL
{
    partial class Frm_PayBuy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PayBuy));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReturn = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatloub = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBaky = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMadfou3 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkPrint = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBarcod = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(128, 383);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 44;
            this.label5.Text = "Enter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(294, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "F12";
            // 
            // btnReturn
            // 
            this.btnReturn.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Appearance.Options.UseFont = true;
            this.btnReturn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.ImageOptions.Image")));
            this.btnReturn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnReturn.Location = new System.Drawing.Point(250, 334);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(113, 46);
            this.btnReturn.TabIndex = 42;
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
            this.btnEnter.Location = new System.Drawing.Point(59, 334);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(185, 46);
            this.btnEnter.TabIndex = 41;
            this.btnEnter.Text = "حفظ و طباعة";
            this.btnEnter.ToolTipTitle = "مفتاح الإختصار Enter";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 40;
            this.label2.Text = "المطلوب:";
            // 
            // txtMatloub
            // 
            this.txtMatloub.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatloub.Location = new System.Drawing.Point(92, 43);
            this.txtMatloub.Multiline = true;
            this.txtMatloub.Name = "txtMatloub";
            this.txtMatloub.ReadOnly = true;
            this.txtMatloub.Size = new System.Drawing.Size(304, 55);
            this.txtMatloub.TabIndex = 39;
            this.txtMatloub.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 38;
            this.label1.Text = "المدفوع:";
            // 
            // txtBaky
            // 
            this.txtBaky.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBaky.Location = new System.Drawing.Point(92, 213);
            this.txtBaky.Multiline = true;
            this.txtBaky.Name = "txtBaky";
            this.txtBaky.ReadOnly = true;
            this.txtBaky.Size = new System.Drawing.Size(304, 55);
            this.txtBaky.TabIndex = 37;
            this.txtBaky.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(27, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 36;
            this.label3.Text = "الباقي:";
            // 
            // txtMadfou3
            // 
            this.txtMadfou3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMadfou3.Location = new System.Drawing.Point(92, 129);
            this.txtMadfou3.Multiline = true;
            this.txtMadfou3.Name = "txtMadfou3";
            this.txtMadfou3.Size = new System.Drawing.Size(304, 55);
            this.txtMadfou3.TabIndex = 35;
            this.txtMadfou3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMadfou3.TextChanged += new System.EventHandler(this.txtMadfou3_TextChanged);
            this.txtMadfou3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMadfou3_KeyPress);
            // 
            // checkPrint
            // 
            this.checkPrint.AutoSize = true;
            this.checkPrint.Location = new System.Drawing.Point(128, 279);
            this.checkPrint.Name = "checkPrint";
            this.checkPrint.Size = new System.Drawing.Size(157, 22);
            this.checkPrint.TabIndex = 57;
            this.checkPrint.Text = "تجاهل طباعة الإيصال";
            this.checkPrint.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 445);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 58;
            this.label6.Text = "الباقي:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(51, 445);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(354, 14);
            this.label7.TabIndex = 59;
            this.label7.Text = "سوف يتم تسجيله في جدول المديونات الى المورد ، ليتم سداده لاحقًا.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(4, 425);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 14);
            this.label8.TabIndex = 60;
            this.label8.Text = "المدفوع:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(51, 425);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(255, 14);
            this.label9.TabIndex = 61;
            this.label9.Text = "هو المبلغ المدفوع في وقت تسجيل الفاتورة للمورد.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Crimson;
            this.label10.Location = new System.Drawing.Point(4, 405);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 14);
            this.label10.TabIndex = 62;
            this.label10.Text = "في حالة الدفع الآجل :";
            // 
            // checkBarcod
            // 
            this.checkBarcod.AutoSize = true;
            this.checkBarcod.Location = new System.Drawing.Point(128, 306);
            this.checkBarcod.Name = "checkBarcod";
            this.checkBarcod.Size = new System.Drawing.Size(206, 22);
            this.checkBarcod.TabIndex = 63;
            this.checkBarcod.Text = "طباعه باركود المنتجات تلقائي";
            this.checkBarcod.UseVisualStyleBackColor = true;
            // 
            // Frm_PayBuy
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(408, 472);
            this.Controls.Add(this.checkBarcod);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkPrint);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_PayBuy";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الدفع";
            this.Load += new System.EventHandler(this.Frm_PayBuy_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_PayBuy_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton btnReturn;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatloub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBaky;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMadfou3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkPrint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBarcod;
    }
}