namespace PharmaCore.POS.PL
{
    partial class Frm_currentMoney
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMoneyBank = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMoneyStock = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStock = new System.Windows.Forms.ComboBox();
            this.btnBank = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMoneyBank);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblMoneyStock);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxStock);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 148);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // lblMoneyBank
            // 
            this.lblMoneyBank.AutoSize = true;
            this.lblMoneyBank.ForeColor = System.Drawing.Color.Blue;
            this.lblMoneyBank.Location = new System.Drawing.Point(124, 109);
            this.lblMoneyBank.Name = "lblMoneyBank";
            this.lblMoneyBank.Size = new System.Drawing.Size(23, 18);
            this.lblMoneyBank.TabIndex = 49;
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
            this.lblMoneyStock.TabIndex = 47;
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
            this.cbxStock.TabIndex = 29;
            this.cbxStock.SelectionChangeCommitted += new System.EventHandler(this.cbxStock_SelectionChangeCommitted);
            // 
            // btnBank
            // 
            this.btnBank.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBank.Location = new System.Drawing.Point(226, 164);
            this.btnBank.Name = "btnBank";
            this.btnBank.Size = new System.Drawing.Size(164, 52);
            this.btnBank.TabIndex = 267;
            this.btnBank.Text = "ايداع رصيد بنك";
            this.btnBank.UseVisualStyleBackColor = true;
            this.btnBank.Click += new System.EventHandler(this.btnBank_Click);
            // 
            // btnStock
            // 
            this.btnStock.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStock.Location = new System.Drawing.Point(32, 164);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(164, 52);
            this.btnStock.TabIndex = 268;
            this.btnStock.Text = "ايداع رصيد خزنة";
            this.btnStock.UseVisualStyleBackColor = true;
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            // 
            // Frm_currentMoney
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(426, 235);
            this.Controls.Add(this.btnBank);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_currentMoney";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الرصيد الحالي";
            this.Load += new System.EventHandler(this.Frm_currentMoney_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMoneyBank;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMoneyStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxStock;
        private System.Windows.Forms.Button btnBank;
        private System.Windows.Forms.Button btnStock;
    }
}