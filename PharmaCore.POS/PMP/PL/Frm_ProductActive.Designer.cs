using System.Drawing;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    partial class Frm_ProductActive
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pnlSidebar = new Panel();
            btnNavCompany = new Button();
            btnNavUsage = new Button();
            btnNavActiveIngredient = new Button();
            pnlMain = new Panel();
            dgvItems = new DataGridView();
            pnlContent = new Panel();
            groupBox1 = new GroupBox();
            lblItemName = new Label();
            txtItemName = new TextBox();
            pnlActions = new Panel();
            btnSave = new Button();
            btnDelete = new Button();
            btnNew = new Button();
            pnlFooter = new Panel();
            lblTotalCount = new Label();
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlSidebar.SuspendLayout();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlContent.SuspendLayout();
            groupBox1.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(44, 62, 80);
            pnlSidebar.Controls.Add(btnNavCompany);
            pnlSidebar.Controls.Add(btnNavUsage);
            pnlSidebar.Controls.Add(btnNavActiveIngredient);
            pnlSidebar.Dock = DockStyle.Right;
            pnlSidebar.Location = new Point(550, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 500);
            pnlSidebar.TabIndex = 0;
            // 
            // btnNavCompany
            // 
            btnNavCompany.Dock = DockStyle.Top;
            btnNavCompany.FlatAppearance.BorderSize = 0;
            btnNavCompany.FlatStyle = FlatStyle.Flat;
            btnNavCompany.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavCompany.ForeColor = Color.White;
            btnNavCompany.Location = new Point(0, 120);
            btnNavCompany.Name = "btnNavCompany";
            btnNavCompany.Size = new Size(200, 60);
            btnNavCompany.TabIndex = 3;
            btnNavCompany.Text = "الشركات المصنعة";
            btnNavCompany.UseVisualStyleBackColor = true;
            // 
            // btnNavUsage
            // 
            btnNavUsage.Dock = DockStyle.Top;
            btnNavUsage.FlatAppearance.BorderSize = 0;
            btnNavUsage.FlatStyle = FlatStyle.Flat;
            btnNavUsage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavUsage.ForeColor = Color.White;
            btnNavUsage.Location = new Point(0, 60);
            btnNavUsage.Name = "btnNavUsage";
            btnNavUsage.Size = new Size(200, 60);
            btnNavUsage.TabIndex = 2;
            btnNavUsage.Text = "استخدامات الـدواء";
            btnNavUsage.UseVisualStyleBackColor = true;
            // 
            // btnNavActiveIngredient
            // 
            btnNavActiveIngredient.Dock = DockStyle.Top;
            btnNavActiveIngredient.FlatAppearance.BorderSize = 0;
            btnNavActiveIngredient.FlatStyle = FlatStyle.Flat;
            btnNavActiveIngredient.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavActiveIngredient.ForeColor = Color.White;
            btnNavActiveIngredient.Location = new Point(0, 0);
            btnNavActiveIngredient.Name = "btnNavActiveIngredient";
            btnNavActiveIngredient.Size = new Size(200, 60);
            btnNavActiveIngredient.TabIndex = 1;
            btnNavActiveIngredient.Text = "المواد الفعالة";
            btnNavActiveIngredient.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(dgvItems);
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlFooter);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(550, 500);
            pnlMain.TabIndex = 1;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(236, 240, 241);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvItems.ColumnHeadersHeight = 40;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.GridColor = Color.FromArgb(224, 224, 224);
            dgvItems.Location = new Point(0, 220);
            dgvItems.MultiSelect = false;
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowTemplate.Height = 35;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.Size = new Size(550, 240);
            dgvItems.TabIndex = 2;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.WhiteSmoke;
            pnlContent.Controls.Add(groupBox1);
            pnlContent.Dock = DockStyle.Top;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(20);
            pnlContent.Size = new Size(550, 160);
            pnlContent.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblItemName);
            groupBox1.Controls.Add(txtItemName);
            groupBox1.Controls.Add(pnlActions);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(52, 73, 94);
            groupBox1.Location = new Point(20, 20);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(510, 120);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "إضافة / تعديل";
            // 
            // lblItemName
            // 
            lblItemName.AutoSize = true;
            lblItemName.Location = new Point(400, 35);
            lblItemName.Name = "lblItemName";
            lblItemName.Size = new Size(46, 19);
            lblItemName.TabIndex = 0;
            lblItemName.Text = "الاسم:";
            // 
            // txtItemName
            // 
            txtItemName.Font = new Font("Segoe UI", 12F);
            txtItemName.Location = new Point(40, 30);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(350, 29);
            txtItemName.TabIndex = 1;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnSave);
            pnlActions.Controls.Add(btnDelete);
            pnlActions.Controls.Add(btnNew);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(3, 77);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(504, 40);
            pnlActions.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(52, 152, 219);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(300, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 1;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(200, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "حذف";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.FromArgb(46, 204, 113);
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.ForeColor = Color.White;
            btnNew.Location = new Point(400, 5);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(90, 30);
            btnNew.TabIndex = 0;
            btnNew.Text = "جديد";
            btnNew.UseVisualStyleBackColor = false;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(236, 240, 241);
            pnlFooter.Controls.Add(lblTotalCount);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 460);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(550, 40);
            pnlFooter.TabIndex = 3;
            // 
            // lblTotalCount
            // 
            lblTotalCount.Dock = DockStyle.Left;
            lblTotalCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCount.ForeColor = Color.FromArgb(127, 140, 141);
            lblTotalCount.Location = new Point(0, 0);
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.Padding = new Padding(10, 0, 0, 0);
            lblTotalCount.Size = new Size(200, 40);
            lblTotalCount.TabIndex = 0;
            lblTotalCount.Text = "الإجمالي: 0";
            lblTotalCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(550, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(41, 128, 185);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(550, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "إدارة تفاصيل المنتجات";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Frm_ProductActive
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(750, 500);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Frm_ProductActive";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تفاصيل المنتج";
            pnlSidebar.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlContent.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnNavActiveIngredient;
        private System.Windows.Forms.Button btnNavUsage;
        private System.Windows.Forms.Button btnNavCompany;
        
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTotalCount;
    }
}