namespace PharmaCore.POS.PL
{
    partial class Frm_Permission
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
            lblEmployeeCode = new Label();
            txtEmployeeCode = new TextBox();
            lblEmployeeName = new Label();
            lblRole = new Label();
            cmbRole = new ComboBox();
            btnManageRoles = new Button();
            treePermissions = new TreeView();
            btnCheckAll = new Button();
            btnUncheckAll = new Button();
            btnSave = new Button();
            lblStatus = new Label();
            panelTop = new Panel();
            panelBottom = new Panel();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // lblEmployeeCode
            // 
            lblEmployeeCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEmployeeCode.AutoSize = true;
            lblEmployeeCode.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEmployeeCode.ForeColor = Color.FromArgb(33, 33, 33);
            lblEmployeeCode.Location = new Point(580, 16);
            lblEmployeeCode.Name = "lblEmployeeCode";
            lblEmployeeCode.Size = new Size(97, 21);
            lblEmployeeCode.TabIndex = 0;
            lblEmployeeCode.Text = "كود الموظف:";
            // 
            // txtEmployeeCode
            // 
            txtEmployeeCode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEmployeeCode.Font = new Font("Segoe UI", 12F);
            txtEmployeeCode.Location = new Point(393, 12);
            txtEmployeeCode.Name = "txtEmployeeCode";
            txtEmployeeCode.Size = new Size(180, 29);
            txtEmployeeCode.TabIndex = 1;
            txtEmployeeCode.TextAlign = HorizontalAlignment.Center;
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEmployeeName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEmployeeName.ForeColor = Color.FromArgb(0, 120, 215);
            lblEmployeeName.Location = new Point(18, 80);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(670, 25);
            lblEmployeeName.TabIndex = 2;
            lblEmployeeName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblRole
            // 
            lblRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRole.ForeColor = Color.FromArgb(33, 33, 33);
            lblRole.Location = new Point(580, 52);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(47, 21);
            lblRole.TabIndex = 5;
            lblRole.Text = "الرتبة:";
            // 
            // cmbRole
            // 
            cmbRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Font = new Font("Segoe UI", 12F);
            cmbRole.Location = new Point(393, 49);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(180, 29);
            cmbRole.TabIndex = 6;
            // 
            // btnManageRoles
            // 
            btnManageRoles.BackColor = Color.FromArgb(108, 117, 125);
            btnManageRoles.FlatStyle = FlatStyle.Flat;
            btnManageRoles.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnManageRoles.ForeColor = Color.White;
            btnManageRoles.Location = new Point(18, 48);
            btnManageRoles.Name = "btnManageRoles";
            btnManageRoles.Size = new Size(140, 31);
            btnManageRoles.TabIndex = 7;
            btnManageRoles.Text = "⚙ إدارة الرتب";
            btnManageRoles.UseVisualStyleBackColor = false;
            btnManageRoles.Click += btnManageRoles_Click_1;
            // 
            // treePermissions
            // 
            treePermissions.CheckBoxes = true;
            treePermissions.Dock = DockStyle.Fill;
            treePermissions.Font = new Font("Segoe UI", 11F);
            treePermissions.ItemHeight = 28;
            treePermissions.Location = new Point(0, 115);
            treePermissions.Name = "treePermissions";
            treePermissions.RightToLeftLayout = true;
            treePermissions.ShowNodeToolTips = true;
            treePermissions.Size = new Size(700, 325);
            treePermissions.TabIndex = 3;
            // 
            // btnCheckAll
            // 
            btnCheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCheckAll.BackColor = Color.FromArgb(0, 120, 215);
            btnCheckAll.FlatStyle = FlatStyle.Flat;
            btnCheckAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCheckAll.ForeColor = Color.White;
            btnCheckAll.Location = new Point(580, 13);
            btnCheckAll.Name = "btnCheckAll";
            btnCheckAll.Size = new Size(105, 35);
            btnCheckAll.TabIndex = 0;
            btnCheckAll.Text = "تحديد الكل";
            btnCheckAll.UseVisualStyleBackColor = false;
            // 
            // btnUncheckAll
            // 
            btnUncheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUncheckAll.BackColor = Color.FromArgb(200, 60, 60);
            btnUncheckAll.FlatStyle = FlatStyle.Flat;
            btnUncheckAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUncheckAll.ForeColor = Color.White;
            btnUncheckAll.Location = new Point(468, 13);
            btnUncheckAll.Name = "btnUncheckAll";
            btnUncheckAll.Size = new Size(105, 35);
            btnUncheckAll.TabIndex = 1;
            btnUncheckAll.Text = "إلغاء الكل";
            btnUncheckAll.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(290, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "💾 حفظ الصلاحيات";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(15, 20);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 19);
            lblStatus.TabIndex = 3;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblEmployeeCode);
            panelTop.Controls.Add(txtEmployeeCode);
            panelTop.Controls.Add(lblRole);
            panelTop.Controls.Add(cmbRole);
            panelTop.Controls.Add(btnManageRoles);
            panelTop.Controls.Add(lblEmployeeName);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(10);
            panelTop.Size = new Size(700, 115);
            panelTop.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnCheckAll);
            panelBottom.Controls.Add(btnUncheckAll);
            panelBottom.Controls.Add(btnSave);
            panelBottom.Controls.Add(lblStatus);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 440);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(10);
            panelBottom.Size = new Size(700, 60);
            panelBottom.TabIndex = 4;
            // 
            // Frm_Permission
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 500);
            Controls.Add(treePermissions);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Font = new Font("Tahoma", 11F);
            MinimumSize = new Size(600, 400);
            Name = "Frm_Permission";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إدارة صلاحيات الموظفين";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblEmployeeCode;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.TreeView treePermissions;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnManageRoles;
    }
}