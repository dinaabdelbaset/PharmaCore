namespace PharmaCore.POS.PL
{
    partial class Frm_Roles
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
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            treeRoles = new TreeView();
            panelLeftBottom = new Panel();
            btnDeleteRole = new Button();
            btnAddNewRole = new Button();
            groupBox2 = new GroupBox();
            treePermissions = new TreeView();
            panelRoleDetailsTop = new Panel();
            cmbParentRole = new ComboBox();
            lblParentRole = new Label();
            txtRoleName = new TextBox();
            lblRoleName = new Label();
            panelRightBottom = new Panel();
            lblStatus = new Label();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            panelLeftBottom.SuspendLayout();
            groupBox2.SuspendLayout();
            panelRoleDetailsTop.SuspendLayout();
            panelRightBottom.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Padding = new Padding(10);
            splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Panel2.Padding = new Padding(10);
            splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
            splitContainer1.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitContainer1.Size = new Size(1015, 561);
            splitContainer1.SplitterDistance = 321;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(treeRoles);
            groupBox1.Controls.Add(panelLeftBottom);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(10, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(301, 541);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "شجرة الرتب (الوظائف)";
            // 
            // treeRoles
            // 
            treeRoles.Dock = DockStyle.Fill;
            treeRoles.Font = new Font("Segoe UI", 11F);
            treeRoles.HideSelection = false;
            treeRoles.ItemHeight = 28;
            treeRoles.Location = new Point(3, 21);
            treeRoles.Name = "treeRoles";
            treeRoles.RightToLeftLayout = true;
            treeRoles.Size = new Size(295, 463);
            treeRoles.TabIndex = 0;
            // 
            // panelLeftBottom
            // 
            panelLeftBottom.Controls.Add(btnDeleteRole);
            panelLeftBottom.Controls.Add(btnAddNewRole);
            panelLeftBottom.Dock = DockStyle.Bottom;
            panelLeftBottom.Location = new Point(3, 484);
            panelLeftBottom.Name = "panelLeftBottom";
            panelLeftBottom.Size = new Size(295, 54);
            panelLeftBottom.TabIndex = 1;
            // 
            // btnDeleteRole
            // 
            btnDeleteRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteRole.BackColor = Color.FromArgb(200, 60, 60);
            btnDeleteRole.FlatStyle = FlatStyle.Flat;
            btnDeleteRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDeleteRole.ForeColor = Color.White;
            btnDeleteRole.Location = new Point(45, 9);
            btnDeleteRole.Name = "btnDeleteRole";
            btnDeleteRole.Size = new Size(100, 35);
            btnDeleteRole.TabIndex = 1;
            btnDeleteRole.Text = "حذف المحددة";
            btnDeleteRole.UseVisualStyleBackColor = false;
            // 
            // btnAddNewRole
            // 
            btnAddNewRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddNewRole.BackColor = Color.FromArgb(0, 120, 215);
            btnAddNewRole.FlatStyle = FlatStyle.Flat;
            btnAddNewRole.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddNewRole.ForeColor = Color.White;
            btnAddNewRole.Location = new Point(151, 9);
            btnAddNewRole.Name = "btnAddNewRole";
            btnAddNewRole.Size = new Size(141, 35);
            btnAddNewRole.TabIndex = 0;
            btnAddNewRole.Text = "➕ إضافة رتبة جديدة";
            btnAddNewRole.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(treePermissions);
            groupBox2.Controls.Add(panelRoleDetailsTop);
            groupBox2.Controls.Add(panelRightBottom);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(10, 10);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(670, 541);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "تفاصيل الرتبة والصلاحيات";
            // 
            // treePermissions
            // 
            treePermissions.CheckBoxes = true;
            treePermissions.Dock = DockStyle.Fill;
            treePermissions.Font = new Font("Segoe UI", 11F);
            treePermissions.ItemHeight = 28;
            treePermissions.Location = new Point(3, 108);
            treePermissions.Name = "treePermissions";
            treePermissions.RightToLeftLayout = true;
            treePermissions.Size = new Size(664, 376);
            treePermissions.TabIndex = 1;
            // 
            // panelRoleDetailsTop
            // 
            panelRoleDetailsTop.Controls.Add(cmbParentRole);
            panelRoleDetailsTop.Controls.Add(lblParentRole);
            panelRoleDetailsTop.Controls.Add(txtRoleName);
            panelRoleDetailsTop.Controls.Add(lblRoleName);
            panelRoleDetailsTop.Dock = DockStyle.Top;
            panelRoleDetailsTop.Location = new Point(3, 21);
            panelRoleDetailsTop.Name = "panelRoleDetailsTop";
            panelRoleDetailsTop.Size = new Size(664, 87);
            panelRoleDetailsTop.TabIndex = 0;
            // 
            // cmbParentRole
            // 
            cmbParentRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbParentRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbParentRole.Font = new Font("Segoe UI", 11F);
            cmbParentRole.FormattingEnabled = true;
            cmbParentRole.Location = new Point(207, 43);
            cmbParentRole.Name = "cmbParentRole";
            cmbParentRole.Size = new Size(340, 28);
            cmbParentRole.TabIndex = 3;
            // 
            // lblParentRole
            // 
            lblParentRole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblParentRole.AutoSize = true;
            lblParentRole.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblParentRole.Location = new Point(553, 46);
            lblParentRole.Name = "lblParentRole";
            lblParentRole.Size = new Size(87, 20);
            lblParentRole.TabIndex = 2;
            lblParentRole.Text = "الإدارة العليا:";
            // 
            // txtRoleName
            // 
            txtRoleName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtRoleName.Font = new Font("Segoe UI", 11F);
            txtRoleName.Location = new Point(207, 10);
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new Size(340, 27);
            txtRoleName.TabIndex = 1;
            // 
            // lblRoleName
            // 
            lblRoleName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRoleName.AutoSize = true;
            lblRoleName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoleName.Location = new Point(553, 13);
            lblRoleName.Name = "lblRoleName";
            lblRoleName.Size = new Size(82, 20);
            lblRoleName.TabIndex = 0;
            lblRoleName.Text = "اسم الرتبة*:";
            // 
            // panelRightBottom
            // 
            panelRightBottom.Controls.Add(lblStatus);
            panelRightBottom.Controls.Add(btnSave);
            panelRightBottom.Dock = DockStyle.Bottom;
            panelRightBottom.Location = new Point(3, 484);
            panelRightBottom.Name = "panelRightBottom";
            panelRightBottom.Size = new Size(664, 54);
            panelRightBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Location = new Point(12, 18);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 19);
            lblStatus.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(491, 8);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "💾 حفظ البيانات";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // Frm_Roles
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1015, 561);
            Controls.Add(splitContainer1);
            Font = new Font("Tahoma", 11F);
            MinimumSize = new Size(800, 500);
            Name = "Frm_Roles";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "التسلسل الهرمي للمؤسسة (إدارة الرتب)";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panelLeftBottom.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            panelRoleDetailsTop.ResumeLayout(false);
            panelRoleDetailsTop.PerformLayout();
            panelRightBottom.ResumeLayout(false);
            panelRightBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeRoles;
        private System.Windows.Forms.Panel panelLeftBottom;
        private System.Windows.Forms.Button btnDeleteRole;
        private System.Windows.Forms.Button btnAddNewRole;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelRoleDetailsTop;
        private System.Windows.Forms.ComboBox cmbParentRole;
        private System.Windows.Forms.Label lblParentRole;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.TreeView treePermissions;
        private System.Windows.Forms.Panel panelRightBottom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblStatus;
    }
}
