namespace PharmaCore.POS.PL
{
    partial class Frm_DrugAlternatives
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle cellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DgvAlternatives = new System.Windows.Forms.DataGridView();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lblLegend = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.DgvAlternatives)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvAlternatives
            // 
            this.DgvAlternatives.AllowUserToAddRows = false;
            this.DgvAlternatives.AllowUserToDeleteRows = false;
            this.DgvAlternatives.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvAlternatives.BackgroundColor = System.Drawing.Color.White;
            this.DgvAlternatives.BorderStyle = System.Windows.Forms.BorderStyle.None;
            cellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            cellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            cellStyle1.ForeColor = System.Drawing.Color.White;
            cellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvAlternatives.ColumnHeadersDefaultCellStyle = cellStyle1;
            this.DgvAlternatives.ColumnHeadersHeight = 40;
            this.DgvAlternatives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            cellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            cellStyle2.BackColor = System.Drawing.Color.White;
            cellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            cellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            cellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            cellStyle2.SelectionForeColor = System.Drawing.Color.White;
            cellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvAlternatives.DefaultCellStyle = cellStyle2;
            this.DgvAlternatives.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvAlternatives.EnableHeadersVisualStyles = false;
            this.DgvAlternatives.Location = new System.Drawing.Point(0, 60);
            this.DgvAlternatives.Name = "DgvAlternatives";
            this.DgvAlternatives.ReadOnly = true;
            this.DgvAlternatives.RowHeadersVisible = false;
            this.DgvAlternatives.RowTemplate.Height = 35;
            this.DgvAlternatives.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvAlternatives.Size = new System.Drawing.Size(900, 400);
            this.DgvAlternatives.TabIndex = 0;
            this.DgvAlternatives.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAlternatives_CellContentDoubleClick);
            this.DgvAlternatives.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DgvAlternatives_RowPrePaint);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTop.Controls.Add(this.lblLegend);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 60);
            this.panelTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(740, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "البدائل والمثائل";
            // 
            // lblLegend
            // 
            this.lblLegend.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular);
            this.lblLegend.Appearance.Options.UseFont = true;
            this.lblLegend.Location = new System.Drawing.Point(150, 20);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(350, 16);
            this.lblLegend.TabIndex = 2;
            this.lblLegend.Text = "🟢 متوفر بالصيدلية | 🟡 مسجل وكميته صفر | 🔴 غير مسجل بالصيدلية";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(20, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "إغلاق";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.picLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLoading.Location = new System.Drawing.Point(0, 0);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(900, 460);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoading.TabIndex = 2;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // Frm_DrugAlternatives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 460);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.DgvAlternatives);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Frm_DrugAlternatives";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PharmaCore Drug Eye - البدائل والمثائل";
            this.Load += new System.EventHandler(this.Frm_DrugAlternatives_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvAlternatives)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView DgvAlternatives;
        private System.Windows.Forms.Panel panelTop;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.PictureBox picLoading;
        private DevExpress.XtraEditors.LabelControl lblLegend;
    }
}
