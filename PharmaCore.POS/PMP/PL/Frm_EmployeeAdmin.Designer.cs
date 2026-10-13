namespace PharmaCore.POS.PL
{
    partial class Frm_EmployeeAdmin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtJob;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.TextBox txtRole;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnResetPin;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.Panel panelTop;

        // Designer-friendly label fields
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblJob;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblPin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtCode = new TextBox();
            txtName = new TextBox();
            txtJob = new TextBox();
            txtGrade = new TextBox();
            txtRole = new TextBox();
            txtPin = new TextBox();
            chkActive = new CheckBox();
            grid = new DataGridView();
            bs = new BindingSource(components);
            btnNew = new Button();
            btnSave = new Button();
            btnUpdate = new Button();
            btnResetPin = new Button();
            btnDelete = new Button();
            panelTop = new Panel();
            lblCode = new Label();
            lblName = new Label();
            lblJob = new Label();
            lblGrade = new Label();
            lblRole = new Label();
            lblPin = new Label();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bs).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // txtCode
            // 
            txtCode.Location = new Point(909, 47);
            txtCode.Name = "txtCode";
            txtCode.RightToLeft = RightToLeft.Yes;
            txtCode.Size = new Size(150, 21);
            txtCode.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(683, 47);
            txtName.Name = "txtName";
            txtName.RightToLeft = RightToLeft.Yes;
            txtName.Size = new Size(220, 21);
            txtName.TabIndex = 3;
            // 
            // txtJob
            // 
            txtJob.Location = new Point(527, 47);
            txtJob.Name = "txtJob";
            txtJob.RightToLeft = RightToLeft.Yes;
            txtJob.Size = new Size(150, 21);
            txtJob.TabIndex = 5;
            // 
            // txtGrade
            // 
            txtGrade.Location = new Point(421, 44);
            txtGrade.Name = "txtGrade";
            txtGrade.RightToLeft = RightToLeft.Yes;
            txtGrade.Size = new Size(100, 21);
            txtGrade.TabIndex = 7;
            // 
            // txtRole
            // 
            txtRole.Location = new Point(295, 44);
            txtRole.Name = "txtRole";
            txtRole.RightToLeft = RightToLeft.Yes;
            txtRole.Size = new Size(120, 21);
            txtRole.TabIndex = 9;
            // 
            // txtPin
            // 
            txtPin.Location = new Point(159, 44);
            txtPin.Name = "txtPin";
            txtPin.RightToLeft = RightToLeft.Yes;
            txtPin.Size = new Size(120, 21);
            txtPin.TabIndex = 11;
            txtPin.UseSystemPasswordChar = true;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(37, 44);
            chkActive.Name = "chkActive";
            chkActive.RightToLeft = RightToLeft.Yes;
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 12;
            chkActive.Text = "مفعل";
            // 
            // grid
            // 
            grid.AutoGenerateColumns = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DataSource = bs;
            grid.Dock = DockStyle.Fill;
            grid.Location = new Point(0, 140);
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.Size = new Size(1100, 510);
            grid.TabIndex = 0;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(10, 80);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 23);
            btnNew.TabIndex = 13;
            btnNew.Text = "جديد";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(90, 80);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 14;
            btnSave.Text = "حفظ";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(170, 80);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 15;
            btnUpdate.Text = "تعديل";
            // 
            // btnResetPin
            // 
            btnResetPin.Location = new Point(250, 80);
            btnResetPin.Name = "btnResetPin";
            btnResetPin.Size = new Size(75, 23);
            btnResetPin.TabIndex = 16;
            btnResetPin.Text = "تغيير PIN";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(350, 80);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "حذف";
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblCode);
            panelTop.Controls.Add(txtCode);
            panelTop.Controls.Add(lblName);
            panelTop.Controls.Add(txtName);
            panelTop.Controls.Add(lblJob);
            panelTop.Controls.Add(txtJob);
            panelTop.Controls.Add(lblGrade);
            panelTop.Controls.Add(txtGrade);
            panelTop.Controls.Add(lblRole);
            panelTop.Controls.Add(txtRole);
            panelTop.Controls.Add(lblPin);
            panelTop.Controls.Add(txtPin);
            panelTop.Controls.Add(chkActive);
            panelTop.Controls.Add(btnNew);
            panelTop.Controls.Add(btnSave);
            panelTop.Controls.Add(btnUpdate);
            panelTop.Controls.Add(btnResetPin);
            panelTop.Controls.Add(btnDelete);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1100, 140);
            panelTop.TabIndex = 1;
            // 
            // lblCode
            // 
            lblCode.Location = new Point(909, 22);
            lblCode.Name = "lblCode";
            lblCode.RightToLeft = RightToLeft.Yes;
            lblCode.Size = new Size(150, 23);
            lblCode.TabIndex = 0;
            lblCode.Text = "الكود";
            // 
            // lblName
            // 
            lblName.Location = new Point(683, 22);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(220, 23);
            lblName.TabIndex = 2;
            lblName.Text = "الاسم الكامل";
            // 
            // lblJob
            // 
            lblJob.Location = new Point(527, 22);
            lblJob.Name = "lblJob";
            lblJob.RightToLeft = RightToLeft.Yes;
            lblJob.Size = new Size(150, 23);
            lblJob.TabIndex = 4;
            lblJob.Text = "المسمى";
            // 
            // lblGrade
            // 
            lblGrade.Location = new Point(421, 19);
            lblGrade.Name = "lblGrade";
            lblGrade.RightToLeft = RightToLeft.Yes;
            lblGrade.Size = new Size(100, 23);
            lblGrade.TabIndex = 6;
            lblGrade.Text = "الدرجة";
            // 
            // lblRole
            // 
            lblRole.Location = new Point(295, 19);
            lblRole.Name = "lblRole";
            lblRole.RightToLeft = RightToLeft.Yes;
            lblRole.Size = new Size(120, 23);
            lblRole.TabIndex = 8;
            lblRole.Text = "الدور/Role";
            // 
            // lblPin
            // 
            lblPin.Location = new Point(159, 19);
            lblPin.Name = "lblPin";
            lblPin.RightToLeft = RightToLeft.Yes;
            lblPin.Size = new Size(120, 23);
            lblPin.TabIndex = 10;
            lblPin.Text = "PIN ";
            // 
            // Frm_EmployeeAdmin
            // 
            ClientSize = new Size(1100, 650);
            Controls.Add(grid);
            Controls.Add(panelTop);
            Name = "Frm_EmployeeAdmin";
            Text = "إدارة الموظفين";
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)bs).EndInit();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }
    }
}
