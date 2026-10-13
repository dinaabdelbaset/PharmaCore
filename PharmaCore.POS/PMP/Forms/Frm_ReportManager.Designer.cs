using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PharmaCore.POS.Forms
{
    partial class Frm_ReportManager
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
            this.listBoxReports = new System.Windows.Forms.ListBox();
            this.btnDesign = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxReports
            // 
            this.listBoxReports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxReports.FormattingEnabled = true;
            this.listBoxReports.ItemHeight = 28;
            this.listBoxReports.Location = new System.Drawing.Point(34, 69);
            this.listBoxReports.Name = "listBoxReports";
            this.listBoxReports.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listBoxReports.Size = new System.Drawing.Size(359, 256);
            this.listBoxReports.TabIndex = 0;
            this.listBoxReports.SelectedIndexChanged += new System.EventHandler(this.listBoxReports_SelectedIndexChanged);
            // 
            // btnDesign
            // 
            this.btnDesign.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDesign.Location = new System.Drawing.Point(423, 69);
            this.btnDesign.Name = "btnDesign";
            this.btnDesign.Size = new System.Drawing.Size(186, 57);
            this.btnDesign.TabIndex = 1;
            this.btnDesign.Text = "تعديل تصميم التقرير";
            this.btnDesign.UseVisualStyleBackColor = true;
            this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReset.ForeColor = System.Drawing.Color.DarkRed;
            this.btnReset.Location = new System.Drawing.Point(423, 147);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(186, 57);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "استعادة الافتراضي";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(236, 21);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(157, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "إدارة التقارير :";
            // 
            // Frm_ReportManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnDesign);
            this.Controls.Add(this.listBoxReports);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ReportManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة تصاميم الطباعة";
            this.Load += new System.EventHandler(this.Frm_ReportManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ListBox listBoxReports;
        private System.Windows.Forms.Button btnDesign;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label1;
    }
}
