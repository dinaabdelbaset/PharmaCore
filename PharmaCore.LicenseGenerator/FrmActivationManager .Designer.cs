namespace PharmaCore.LicenseGenerator
{
    partial class FrmActivationManager
    {
        private System.Windows.Forms.ComboBox cboOrg;
        private System.Windows.Forms.ComboBox cboTenant;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.NumericUpDown numMaxDevices;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblLastCode;
        private System.Windows.Forms.DataGridView grdCodes;
        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.Label lblTenant;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.Label lblMaxDevices;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Button btnShowAll;
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            cboOrg = new ComboBox();
            cboTenant = new ComboBox();
            cboRegion = new ComboBox();
            cboBranch = new ComboBox();
            numMaxDevices = new NumericUpDown();
            dtpExpiry = new DateTimePicker();
            btnGenerate = new Button();
            lblLastCode = new Label();
            grdCodes = new DataGridView();
            lblOrg = new Label();
            lblTenant = new Label();
            lblRegion = new Label();
            lblBranch = new Label();
            lblMaxDevices = new Label();
            lblExpiry = new Label();
            btnShowAll = new Button();
            ((System.ComponentModel.ISupportInitialize)numMaxDevices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grdCodes).BeginInit();
            SuspendLayout();
            // 
            // cboOrg
            // 
            cboOrg.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOrg.Location = new Point(110, 12);
            cboOrg.Name = "cboOrg";
            cboOrg.Size = new Size(160, 23);
            cboOrg.TabIndex = 14;
            // 
            // cboTenant
            // 
            cboTenant.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTenant.Location = new Point(390, 12);
            cboTenant.Name = "cboTenant";
            cboTenant.Size = new Size(160, 23);
            cboTenant.TabIndex = 13;
            // 
            // cboRegion
            // 
            cboRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRegion.Location = new Point(684, 12);
            cboRegion.Name = "cboRegion";
            cboRegion.Size = new Size(160, 23);
            cboRegion.TabIndex = 12;
            // 
            // cboBranch
            // 
            cboBranch.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBranch.Location = new Point(956, 12);
            cboBranch.Name = "cboBranch";
            cboBranch.Size = new Size(160, 23);
            cboBranch.TabIndex = 11;
            // 
            // numMaxDevices
            // 
            numMaxDevices.Location = new Point(110, 50);
            numMaxDevices.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            numMaxDevices.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxDevices.Name = "numMaxDevices";
            numMaxDevices.Size = new Size(160, 23);
            numMaxDevices.TabIndex = 10;
            numMaxDevices.TextAlign = HorizontalAlignment.Center;
            numMaxDevices.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dtpExpiry
            // 
            dtpExpiry.Location = new Point(360, 50);
            dtpExpiry.Name = "dtpExpiry";
            dtpExpiry.Size = new Size(220, 23);
            dtpExpiry.TabIndex = 9;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(610, 51);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(140, 25);
            btnGenerate.TabIndex = 8;
            btnGenerate.Text = "Generate Code";
            // 
            // lblLastCode
            // 
            lblLastCode.AutoSize = true;
            lblLastCode.Location = new Point(769, 58);
            lblLastCode.Name = "lblLastCode";
            lblLastCode.Size = new Size(85, 15);
            lblLastCode.TabIndex = 7;
            lblLastCode.Text = "Last Code: ----";
            // 
            // grdCodes
            // 
            grdCodes.AllowUserToAddRows = false;
            grdCodes.AllowUserToDeleteRows = false;
            grdCodes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grdCodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdCodes.Location = new Point(12, 90);
            grdCodes.Name = "grdCodes";
            grdCodes.ReadOnly = true;
            grdCodes.Size = new Size(1104, 360);
            grdCodes.TabIndex = 6;
            // 
            // lblOrg
            // 
            lblOrg.AutoSize = true;
            lblOrg.Location = new Point(29, 16);
            lblOrg.Name = "lblOrg";
            lblOrg.Size = new Size(75, 15);
            lblOrg.TabIndex = 5;
            lblOrg.Text = "Organization";
            // 
            // lblTenant
            // 
            lblTenant.AutoSize = true;
            lblTenant.Location = new Point(341, 16);
            lblTenant.Name = "lblTenant";
            lblTenant.Size = new Size(43, 15);
            lblTenant.TabIndex = 4;
            lblTenant.Text = "Tenant";
            // 
            // lblRegion
            // 
            lblRegion.AutoSize = true;
            lblRegion.Location = new Point(634, 16);
            lblRegion.Name = "lblRegion";
            lblRegion.Size = new Size(44, 15);
            lblRegion.TabIndex = 3;
            lblRegion.Text = "Region";
            // 
            // lblBranch
            // 
            lblBranch.AutoSize = true;
            lblBranch.Location = new Point(906, 16);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new Size(44, 15);
            lblBranch.TabIndex = 2;
            lblBranch.Text = "Branch";
            // 
            // lblMaxDevices
            // 
            lblMaxDevices.AutoSize = true;
            lblMaxDevices.Location = new Point(32, 52);
            lblMaxDevices.Name = "lblMaxDevices";
            lblMaxDevices.Size = new Size(72, 15);
            lblMaxDevices.TabIndex = 1;
            lblMaxDevices.Text = "Max Devices";
            // 
            // lblExpiry
            // 
            lblExpiry.AutoSize = true;
            lblExpiry.Location = new Point(289, 54);
            lblExpiry.Name = "lblExpiry";
            lblExpiry.Size = new Size(65, 15);
            lblExpiry.TabIndex = 0;
            lblExpiry.Text = "Expiry Date";
            // 
            // btnShowAll
            // 
            btnShowAll.Location = new Point(1006, 58);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(110, 25);
            btnShowAll.TabIndex = 15;
            btnShowAll.Text = "Show All";
            // 
            // FrmActivationManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 462);
            Controls.Add(lblExpiry);
            Controls.Add(lblMaxDevices);
            Controls.Add(lblBranch);
            Controls.Add(lblRegion);
            Controls.Add(lblTenant);
            Controls.Add(lblOrg);
            Controls.Add(grdCodes);
            Controls.Add(lblLastCode);
            Controls.Add(btnGenerate);
            Controls.Add(dtpExpiry);
            Controls.Add(numMaxDevices);
            Controls.Add(cboBranch);
            Controls.Add(cboRegion);
            Controls.Add(cboTenant);
            Controls.Add(cboOrg);
            Controls.Add(btnShowAll);
            Name = "FrmActivationManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Activation Manager";
            Load += FrmActivationManager_Load;
            ((System.ComponentModel.ISupportInitialize)numMaxDevices).EndInit();
            ((System.ComponentModel.ISupportInitialize)grdCodes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
