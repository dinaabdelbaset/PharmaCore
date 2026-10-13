namespace PharmaCore.POS.PL
{
    partial class Frm_ItemName
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
            pnlTop = new Panel();
            label1 = new Label();
            txtName = new TextBox();
            btnSearch = new Button();
            btnExal = new Button();
            pnlGrid = new Panel();
            DgvSearch = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNameEn = new DataGridViewTextBoxColumn();
            colNameAr = new DataGridViewTextBoxColumn();
            colActive = new DataGridViewTextBoxColumn();
            colUsage = new DataGridViewTextBoxColumn();
            colCompany = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewButtonColumn();
            pnlBottom = new Panel();
            lblCount = new Label();
            btnClearAll = new Button();
            btnExport = new Button();
            pnlTop.SuspendLayout();
            pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvSearch).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(240, 244, 248);
            pnlTop.Controls.Add(label1);
            pnlTop.Controls.Add(txtName);
            pnlTop.Controls.Add(btnSearch);
            pnlTop.Controls.Add(btnExal);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(900, 70);
            pnlTop.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 12F, FontStyle.Bold);
            label1.Location = new Point(820, 25);
            label1.Name = "label1";
            label1.Size = new Size(48, 19);
            label1.TabIndex = 0;
            label1.Text = "بحث:";
            // 
            // txtName
            // 
            txtName.Font = new Font("Tahoma", 12F);
            txtName.Location = new Point(368, 22);
            txtName.Name = "txtName";
            txtName.Size = new Size(434, 27);
            txtName.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Teal;
            btnSearch.Font = new Font("Tahoma", 11F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(236, 17);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 36);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "بحث";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnExal
            // 
            btnExal.BackColor = Color.Peru;
            btnExal.Font = new Font("Tahoma", 11F, FontStyle.Bold);
            btnExal.ForeColor = Color.White;
            btnExal.Location = new Point(12, 17);
            btnExal.Name = "btnExal";
            btnExal.Size = new Size(205, 36);
            btnExal.TabIndex = 3;
            btnExal.Text = "استيراد ملف إكسل";
            btnExal.UseVisualStyleBackColor = false;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(DgvSearch);
            pnlGrid.Dock = DockStyle.Fill;
            pnlGrid.Location = new Point(0, 70);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Padding = new Padding(10);
            pnlGrid.Size = new Size(900, 530);
            pnlGrid.TabIndex = 1;
            // 
            // DgvSearch
            // 
            DgvSearch.AllowUserToAddRows = false;
            DgvSearch.AllowUserToDeleteRows = false;
            DgvSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvSearch.BackgroundColor = Color.White;
            DgvSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvSearch.Columns.AddRange(new DataGridViewColumn[] { colId, colNameEn, colNameAr, colActive, colUsage, colCompany, colDelete });
            DgvSearch.Dock = DockStyle.Fill;
            DgvSearch.Location = new Point(10, 10);
            DgvSearch.Name = "DgvSearch";
            DgvSearch.ReadOnly = true;
            DgvSearch.RowTemplate.Height = 35;
            DgvSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvSearch.Size = new Size(880, 510);
            DgvSearch.TabIndex = 0;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colNameEn
            // 
            colNameEn.HeaderText = "الاسم إنجليزي";
            colNameEn.Name = "colNameEn";
            colNameEn.ReadOnly = true;
            // 
            // colNameAr
            // 
            colNameAr.HeaderText = "الاسم عربي";
            colNameAr.Name = "colNameAr";
            colNameAr.ReadOnly = true;
            // 
            // colActive
            // 
            colActive.HeaderText = "المادة الفعالة";
            colActive.Name = "colActive";
            colActive.ReadOnly = true;
            // 
            // colUsage
            // 
            colUsage.HeaderText = "الاستخدام";
            colUsage.Name = "colUsage";
            colUsage.ReadOnly = true;
            // 
            // colCompany
            // 
            colCompany.HeaderText = "الشركة";
            colCompany.Name = "colCompany";
            colCompany.ReadOnly = true;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "مسح";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Text = "🗑";
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Width = 50;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(240, 244, 248);
            pnlBottom.Controls.Add(lblCount);
            pnlBottom.Controls.Add(btnExport);
            pnlBottom.Controls.Add(btnClearAll);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 550);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(900, 50);
            pnlBottom.TabIndex = 2;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Tahoma", 11F, FontStyle.Bold);
            lblCount.ForeColor = Color.Teal;
            lblCount.Location = new Point(400, 16);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(111, 18);
            lblCount.TabIndex = 2;
            lblCount.Text = "عدد المنتجات: 0";
            // 
            // btnClearAll
            // 
            btnClearAll.BackColor = Color.Maroon;
            btnClearAll.Font = new Font("Tahoma", 11F, FontStyle.Bold);
            btnClearAll.ForeColor = Color.White;
            btnClearAll.Location = new Point(12, 7);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(180, 36);
            btnClearAll.TabIndex = 0;
            btnClearAll.Text = "مسح كل الأصناف";
            btnClearAll.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.BackColor = Color.Green;
            btnExport.Font = new Font("Tahoma", 11F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(696, 7);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(180, 36);
            btnExport.TabIndex = 1;
            btnExport.Text = "تنزيل ملف إكسل";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // Frm_ItemName
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(pnlGrid);
            Controls.Add(pnlBottom);
            Controls.Add(pnlTop);
            Font = new Font("Tahoma", 11F);
            FormBorderStyle = FormBorderStyle.Sizable;
            Name = "Frm_ItemName";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "البحث في القائمة القياسية للأصناف";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvSearch).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExal;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameAr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompany;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnExport;
    }
}