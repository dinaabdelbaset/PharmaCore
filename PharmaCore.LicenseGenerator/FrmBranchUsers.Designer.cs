namespace PharmaCore.ActivationManager
{
    partial class FrmBranchUsers
    {
        private System.ComponentModel.IContainer components=null; protected override void Dispose(bool disposing){ if(disposing && components!=null) components.Dispose(); base.Dispose(disposing);}
        private void InitializeComponent()
        {
            cboOrg = new ComboBox();
            cboTenant = new ComboBox();
            cboRegion = new ComboBox();
            cboBranch = new ComboBox();
            txtUser = new TextBox();
            cboRole = new ComboBox();
            txtPass = new TextBox();
            numAllowed = new NumericUpDown();
            btnAdd = new Button();
            btnRefresh = new Button();
            grid = new DataGridView();
            btnUpdate = new Button();
            chkActive = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numAllowed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // cboOrg
            // 
            cboOrg.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOrg.Location = new Point(12, 12);
            cboOrg.Name = "cboOrg";
            cboOrg.Size = new Size(180, 23);
            cboOrg.TabIndex = 0;
            // 
            // cboTenant
            // 
            cboTenant.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTenant.Location = new Point(198, 12);
            cboTenant.Name = "cboTenant";
            cboTenant.Size = new Size(150, 23);
            cboTenant.TabIndex = 1;
            // 
            // cboRegion
            // 
            cboRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRegion.Location = new Point(354, 12);
            cboRegion.Name = "cboRegion";
            cboRegion.Size = new Size(150, 23);
            cboRegion.TabIndex = 2;
            // 
            // cboBranch
            // 
            cboBranch.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBranch.Location = new Point(510, 12);
            cboBranch.Name = "cboBranch";
            cboBranch.Size = new Size(150, 23);
            cboBranch.TabIndex = 3;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(666, 12);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Username";
            txtUser.Size = new Size(130, 23);
            txtUser.TabIndex = 4;
            // 
            // cboRole
            // 
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Location = new Point(802, 12);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(150, 23);
            cboRole.TabIndex = 5;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(958, 12);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Password";
            txtPass.Size = new Size(120, 23);
            txtPass.TabIndex = 6;
            txtPass.UseSystemPasswordChar = true;
            // 
            // numAllowed
            // 
            numAllowed.Location = new Point(1084, 12);
            numAllowed.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numAllowed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAllowed.Name = "numAllowed";
            numAllowed.Size = new Size(50, 23);
            numAllowed.TabIndex = 7;
            numAllowed.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(1196, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(60, 26);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Add";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(1262, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(70, 26);
            btnRefresh.TabIndex = 10;
            btnRefresh.Text = "Refresh";
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.Location = new Point(12, 80);
            grid.Name = "grid";
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(1260, 459);
            grid.TabIndex = 11;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(12, 44);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 26);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update";
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Checked = true;
            chkActive.CheckState = CheckState.Checked;
            chkActive.Location = new Point(1140, 14);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(59, 19);
            chkActive.TabIndex = 8;
            chkActive.Text = "Active";
            // 
            // FrmBranchUsers
            // 
            ClientSize = new Size(1344, 551);
            Controls.Add(cboOrg);
            Controls.Add(cboTenant);
            Controls.Add(cboRegion);
            Controls.Add(cboBranch);
            Controls.Add(txtUser);
            Controls.Add(cboRole);
            Controls.Add(txtPass);
            Controls.Add(numAllowed);
            Controls.Add(chkActive);
            Controls.Add(btnAdd);
            Controls.Add(btnRefresh);
            Controls.Add(btnUpdate);
            Controls.Add(grid);
            Name = "FrmBranchUsers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Branch Users";
            Load += FrmBranchUsers_Load;
            ((System.ComponentModel.ISupportInitialize)numAllowed).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.ComboBox cboOrg; private System.Windows.Forms.ComboBox cboTenant; private System.Windows.Forms.ComboBox cboRegion; private System.Windows.Forms.ComboBox cboBranch; private System.Windows.Forms.TextBox txtUser; private System.Windows.Forms.ComboBox cboRole; private System.Windows.Forms.TextBox txtPass; private System.Windows.Forms.NumericUpDown numAllowed; private System.Windows.Forms.Button btnAdd; private System.Windows.Forms.Button btnRefresh; private System.Windows.Forms.Button btnUpdate; private System.Windows.Forms.DataGridView grid; private System.Windows.Forms.Button btnReset; private System.Windows.Forms.CheckBox chkActive;
    }
}