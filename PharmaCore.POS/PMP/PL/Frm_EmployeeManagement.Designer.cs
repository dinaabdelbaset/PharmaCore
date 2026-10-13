namespace PharmaCore.POS.PL
{
    partial class Frm_EmployeeManagement
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grid = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblJob = new System.Windows.Forms.Label();
            this.txtJob = new System.Windows.Forms.TextBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.lblPin = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnResetPin = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.splitContainer.Panel1.Controls.Add(this.grid);
            this.splitContainer.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer.Panel1.Controls.Add(this.lblTitle);
            this.splitContainer.Panel1.Controls.Add(this.lblCount);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(12, 10, 6, 10);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.splitContainer.Panel2.Controls.Add(this.lblStatus);
            this.splitContainer.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer.Panel2.Controls.Add(this.btnResetPin);
            this.splitContainer.Panel2.Controls.Add(this.btnUpdate);
            this.splitContainer.Panel2.Controls.Add(this.btnSave);
            this.splitContainer.Panel2.Controls.Add(this.btnNew);
            this.splitContainer.Panel2.Controls.Add(this.chkActive);
            this.splitContainer.Panel2.Controls.Add(this.txtPin);
            this.splitContainer.Panel2.Controls.Add(this.lblPin);
            this.splitContainer.Panel2.Controls.Add(this.txtGrade);
            this.splitContainer.Panel2.Controls.Add(this.lblGrade);
            this.splitContainer.Panel2.Controls.Add(this.txtJob);
            this.splitContainer.Panel2.Controls.Add(this.lblJob);
            this.splitContainer.Panel2.Controls.Add(this.cmbRole);
            this.splitContainer.Panel2.Controls.Add(this.lblRole);
            this.splitContainer.Panel2.Controls.Add(this.txtName);
            this.splitContainer.Panel2.Controls.Add(this.lblName);
            this.splitContainer.Panel2.Controls.Add(this.txtCode);
            this.splitContainer.Panel2.Controls.Add(this.lblCode);
            this.splitContainer.Panel2.Controls.Add(this.lblFormTitle);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer.Size = new System.Drawing.Size(900, 600);
            this.splitContainer.SplitterDistance = 520;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(52)))));
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(80)))));
            this.grid.Location = new System.Drawing.Point(12, 80);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 32;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(502, 486);
            this.grid.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(12, 50);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "🔍  بحث بالاسم أو الكود ...";
            this.txtSearch.Size = new System.Drawing.Size(502, 30);
            this.txtSearch.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(502, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "إدارة الموظفين";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(173)))), ((int)(((byte)(200)))));
            this.lblCount.Location = new System.Drawing.Point(12, 566);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(502, 24);
            this.lblCount.TabIndex = 3;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(180)))), ((int)(((byte)(250)))));
            this.lblFormTitle.Location = new System.Drawing.Point(10, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(280, 30);
            this.lblFormTitle.TabIndex = 4;
            this.lblFormTitle.Text = "بيانات الموظف";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblCode.Location = new System.Drawing.Point(10, 48);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(89, 23);
            this.lblCode.TabIndex = 5;
            this.lblCode.Text = "كود الموظف";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.ForeColor = System.Drawing.Color.White;
            this.txtCode.Location = new System.Drawing.Point(10, 70);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(270, 30);
            this.txtCode.TabIndex = 6;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblName.Location = new System.Drawing.Point(10, 106);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(95, 23);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "الاسم بالكامل";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(10, 128);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(270, 30);
            this.txtName.TabIndex = 8;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblRole.Location = new System.Drawing.Point(10, 164);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(43, 23);
            this.lblRole.TabIndex = 9;
            this.lblRole.Text = "الرتبة";
            // 
            // cmbRole
            // 
            this.cmbRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRole.ForeColor = System.Drawing.Color.White;
            this.cmbRole.Location = new System.Drawing.Point(10, 186);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbRole.Size = new System.Drawing.Size(270, 31);
            this.cmbRole.TabIndex = 10;
            // 
            // lblJob
            // 
            this.lblJob.AutoSize = true;
            this.lblJob.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJob.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblJob.Location = new System.Drawing.Point(10, 222);
            this.lblJob.Name = "lblJob";
            this.lblJob.Size = new System.Drawing.Size(126, 23);
            this.lblJob.TabIndex = 11;
            this.lblJob.Text = "المسمى الوظيفي";
            // 
            // txtJob
            // 
            this.txtJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.txtJob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJob.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtJob.ForeColor = System.Drawing.Color.White;
            this.txtJob.Location = new System.Drawing.Point(10, 244);
            this.txtJob.Name = "txtJob";
            this.txtJob.Size = new System.Drawing.Size(270, 30);
            this.txtJob.TabIndex = 12;
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGrade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblGrade.Location = new System.Drawing.Point(10, 280);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(53, 23);
            this.lblGrade.TabIndex = 13;
            this.lblGrade.Text = "الدرجة";
            // 
            // txtGrade
            // 
            this.txtGrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.txtGrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGrade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGrade.ForeColor = System.Drawing.Color.White;
            this.txtGrade.Location = new System.Drawing.Point(10, 302);
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(270, 30);
            this.txtGrade.TabIndex = 14;
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblPin.Location = new System.Drawing.Point(10, 338);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(147, 23);
            this.lblPin.TabIndex = 15;
            this.lblPin.Text = "الرقم السري (PIN)";
            // 
            // txtPin
            // 
            this.txtPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(75)))));
            this.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPin.ForeColor = System.Drawing.Color.White;
            this.txtPin.Location = new System.Drawing.Point(10, 360);
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(270, 30);
            this.txtPin.TabIndex = 16;
            this.txtPin.UseSystemPasswordChar = true;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.chkActive.Location = new System.Drawing.Point(10, 396);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(63, 27);
            this.chkActive.TabIndex = 17;
            this.chkActive.Text = "نشط";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(180)))), ((int)(((byte)(250)))));
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnNew.Location = new System.Drawing.Point(10, 432);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(130, 36);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "جديد ✚";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(227)))), ((int)(((byte)(161)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnSave.Location = new System.Drawing.Point(150, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 36);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "حفظ ✓";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnUpdate.Location = new System.Drawing.Point(10, 476);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 36);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "تعديل ✎";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnResetPin
            // 
            this.btnResetPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(194)))), ((int)(((byte)(231)))));
            this.btnResetPin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetPin.FlatAppearance.BorderSize = 0;
            this.btnResetPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetPin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResetPin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnResetPin.Location = new System.Drawing.Point(150, 476);
            this.btnResetPin.Name = "btnResetPin";
            this.btnResetPin.Size = new System.Drawing.Size(130, 36);
            this.btnResetPin.TabIndex = 21;
            this.btnResetPin.Text = "تغيير PIN 🔑";
            this.btnResetPin.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(139)))), ((int)(((byte)(168)))));
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnDelete.Location = new System.Drawing.Point(10, 520);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 36);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "حذف ✕";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(173)))), ((int)(((byte)(200)))));
            this.lblStatus.Location = new System.Drawing.Point(10, 568);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(270, 25);
            this.lblStatus.TabIndex = 23;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frm_EmployeeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Frm_EmployeeManagement";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إدارة الموظفين";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblJob;
        private System.Windows.Forms.TextBox txtJob;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnResetPin;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblStatus;
    }
}
