namespace PharmaCore.POS.PL
{
    partial class Frm_GlobalCatalogManager
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnImportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnImportExcel.Appearance.Options.UseFont = true;
            this.btnImportExcel.Location = new System.Drawing.Point(180, 110);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(200, 50);
            this.btnImportExcel.TabIndex = 0;
            this.btnImportExcel.Text = "استيراد ملف إكسيل (26K)";
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "إدارة الكتالوج الطبي المركزي";
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Location = new System.Drawing.Point(60, 60);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(430, 34);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "ارفع ملف الإكسيل بالأعمدة التالية: name, arabic, oldprice, price, active, company, description.\nسيتم تحديث الجداول المركزية للبرنامج فوراً.";
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Appearance.Options.UseForeColor = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 180);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 3;
            // 
            // Frm_GlobalCatalogManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 230);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnImportExcel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Frm_GlobalCatalogManager";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PharmaCore - DrugEye System Manager";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private DevExpress.XtraEditors.SimpleButton btnImportExcel;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl lblStatus;
    }
}
