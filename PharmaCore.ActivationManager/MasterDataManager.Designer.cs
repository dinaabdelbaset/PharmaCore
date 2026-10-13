using System.Windows.Forms;
using System.Drawing;

namespace PharmaCore.ActivationManager
{
    partial class MasterDataManager
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView grdOrgs;
        private TextBox txtOrgCode;
        private TextBox txtOrgName;
        private CheckBox chkOrgActive;
        private Button btnOrgAdd;
        private Button btnOrgUpdate;
        private Button btnOrgClear;

        private ComboBox cboTenantOrg;
        private DataGridView grdTenants;
        private TextBox txtTenantCode;
        private TextBox txtTenantName;
        private CheckBox chkTenantActive;
        private Button btnTenantAdd;
        private Button btnTenantUpdate;
        private Button btnTenantClear;

        private ComboBox cboRegionTenant;
        private DataGridView grdRegions;
        private TextBox txtRegionCode;
        private TextBox txtRegionName;
        private CheckBox chkRegionActive;
        private Button btnRegionAdd;
        private Button btnRegionUpdate;
        private Button btnRegionClear;

        private ComboBox cboBranchRegion;
        private DataGridView grdBranches;
        private TextBox txtBranchCode;
        private TextBox txtBranchName;
        private CheckBox chkBranchActive;
        private Button btnBranchAdd;
        private Button btnBranchUpdate;
        private Button btnBranchClear;

        private GroupBox grpOrgs;
        private GroupBox grpTenants;
        private GroupBox grpRegions;
        private GroupBox grpBranches;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grdOrgs = new DataGridView();
            txtOrgCode = new TextBox();
            txtOrgName = new TextBox();
            chkOrgActive = new CheckBox();
            btnOrgAdd = new Button();
            btnOrgUpdate = new Button();
            btnOrgClear = new Button();
            cboTenantOrg = new ComboBox();
            grdTenants = new DataGridView();
            txtTenantCode = new TextBox();
            txtTenantName = new TextBox();
            chkTenantActive = new CheckBox();
            btnTenantAdd = new Button();
            btnTenantUpdate = new Button();
            btnTenantClear = new Button();
            cboRegionTenant = new ComboBox();
            grdRegions = new DataGridView();
            txtRegionCode = new TextBox();
            txtRegionName = new TextBox();
            chkRegionActive = new CheckBox();
            btnRegionAdd = new Button();
            btnRegionUpdate = new Button();
            btnRegionClear = new Button();
            cboBranchRegion = new ComboBox();
            grdBranches = new DataGridView();
            txtBranchCode = new TextBox();
            txtBranchName = new TextBox();
            chkBranchActive = new CheckBox();
            btnBranchAdd = new Button();
            btnBranchUpdate = new Button();
            btnBranchClear = new Button();
            grpOrgs = new GroupBox();
            grpTenants = new GroupBox();
            grpRegions = new GroupBox();
            grpBranches = new GroupBox();
            SuspendLayout();
            void SetupGrid(DataGridView g)
            {
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.ReadOnly = true;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.MultiSelect = false;
            }
            grpOrgs.Text = "Organizations"; grpOrgs.Location = new Point(10, 10); grpOrgs.Size = new Size(520, 280);
            grdOrgs.Location = new Point(10, 20); grdOrgs.Size = new Size(500, 150); SetupGrid(grdOrgs);
            var lblOrgCode = new Label { Text = "Code", Location = new Point(10, 180), AutoSize = true };
            var lblOrgName = new Label { Text = "Name", Location = new Point(10, 210), AutoSize = true };
            txtOrgCode.Location = new Point(70, 176); txtOrgCode.Width = 140;
            txtOrgName.Location = new Point(70, 206); txtOrgName.Width = 220;
            chkOrgActive.Text = "Active"; chkOrgActive.Location = new Point(310, 176); chkOrgActive.AutoSize = true;
            btnOrgAdd.Text = "Add"; btnOrgAdd.Location = new Point(70, 240); btnOrgAdd.Width = 80;
            btnOrgUpdate.Text = "Update"; btnOrgUpdate.Location = new Point(160, 240); btnOrgUpdate.Width = 80;
            btnOrgClear.Text = "Clear"; btnOrgClear.Location = new Point(250, 240); btnOrgClear.Width = 80;
            grpOrgs.Controls.AddRange(new Control[] { grdOrgs, lblOrgCode, lblOrgName, txtOrgCode, txtOrgName, chkOrgActive, btnOrgAdd, btnOrgUpdate, btnOrgClear });

            grpTenants.Text = "Tenants"; grpTenants.Location = new Point(540, 10); grpTenants.Size = new Size(520, 280);
            var lblTenantOrg = new Label { Text = "Organization", Location = new Point(10, 22), AutoSize = true };
            cboTenantOrg.DropDownStyle = ComboBoxStyle.DropDownList; cboTenantOrg.Location = new Point(100, 18); cboTenantOrg.Width = 160;
            grdTenants.Location = new Point(10, 50); grdTenants.Size = new Size(500, 120); SetupGrid(grdTenants);
            var lblTenantCode = new Label { Text = "Code", Location = new Point(10, 180), AutoSize = true };
            var lblTenantName = new Label { Text = "Name", Location = new Point(10, 210), AutoSize = true };
            txtTenantCode.Location = new Point(70, 176); txtTenantCode.Width = 140;
            txtTenantName.Location = new Point(70, 206); txtTenantName.Width = 220;
            chkTenantActive.Text = "Active"; chkTenantActive.Location = new Point(310, 176); chkTenantActive.AutoSize = true;
            btnTenantAdd.Text = "Add"; btnTenantAdd.Location = new Point(70, 240); btnTenantAdd.Width = 80;
            btnTenantUpdate.Text = "Update"; btnTenantUpdate.Location = new Point(160, 240); btnTenantUpdate.Width = 80;
            btnTenantClear.Text = "Clear"; btnTenantClear.Location = new Point(250, 240); btnTenantClear.Width = 80;
            grpTenants.Controls.AddRange(new Control[] { lblTenantOrg, cboTenantOrg, grdTenants, lblTenantCode, lblTenantName, txtTenantCode, txtTenantName, chkTenantActive, btnTenantAdd, btnTenantUpdate, btnTenantClear });

            grpRegions.Text = "Regions"; grpRegions.Location = new Point(10, 300); grpRegions.Size = new Size(520, 280);
            var lblRegionTenant = new Label { Text = "Tenant", Location = new Point(10, 22), AutoSize = true };
            cboRegionTenant.DropDownStyle = ComboBoxStyle.DropDownList; cboRegionTenant.Location = new Point(70, 18); cboRegionTenant.Width = 160;
            grdRegions.Location = new Point(10, 50); grdRegions.Size = new Size(500, 120); SetupGrid(grdRegions);
            var lblRegionCode = new Label { Text = "Code", Location = new Point(10, 180), AutoSize = true };
            var lblRegionName = new Label { Text = "Name", Location = new Point(10, 210), AutoSize = true };
            txtRegionCode.Location = new Point(70, 176); txtRegionCode.Width = 140;
            txtRegionName.Location = new Point(70, 206); txtRegionName.Width = 220;
            chkRegionActive.Text = "Active"; chkRegionActive.Location = new Point(310, 176); chkRegionActive.AutoSize = true;
            btnRegionAdd.Text = "Add"; btnRegionAdd.Location = new Point(70, 240); btnRegionAdd.Width = 80;
            btnRegionUpdate.Text = "Update"; btnRegionUpdate.Location = new Point(160, 240); btnRegionUpdate.Width = 80;
            btnRegionClear.Text = "Clear"; btnRegionClear.Location = new Point(250, 240); btnRegionClear.Width = 80;
            grpRegions.Controls.AddRange(new Control[] { lblRegionTenant, cboRegionTenant, grdRegions, lblRegionCode, lblRegionName, txtRegionCode, txtRegionName, chkRegionActive, btnRegionAdd, btnRegionUpdate, btnRegionClear });

            grpBranches.Text = "Branches"; grpBranches.Location = new Point(540, 300); grpBranches.Size = new Size(520, 280);
            var lblBranchRegion = new Label { Text = "Region", Location = new Point(10, 22), AutoSize = true };
            cboBranchRegion.DropDownStyle = ComboBoxStyle.DropDownList; cboBranchRegion.Location = new Point(70, 18); cboBranchRegion.Width = 160;
            grdBranches.Location = new Point(10, 50); grdBranches.Size = new Size(500, 120); SetupGrid(grdBranches);
            var lblBranchCode = new Label { Text = "Code", Location = new Point(10, 180), AutoSize = true };
            var lblBranchName = new Label { Text = "Name", Location = new Point(10, 210), AutoSize = true };
            txtBranchCode.Location = new Point(70, 176); txtBranchCode.Width = 140;
            txtBranchName.Location = new Point(70, 206); txtBranchName.Width = 220;
            chkBranchActive.Text = "Active"; chkBranchActive.Location = new Point(310, 176); chkBranchActive.AutoSize = true;
            btnBranchAdd.Text = "Add"; btnBranchAdd.Location = new Point(70, 240); btnBranchAdd.Width = 80;
            btnBranchUpdate.Text = "Update"; btnBranchUpdate.Location = new Point(160, 240); btnBranchUpdate.Width = 80;
            btnBranchClear.Text = "Clear"; btnBranchClear.Location = new Point(250, 240); btnBranchClear.Width = 80;
            grpBranches.Controls.AddRange(new Control[] { lblBranchRegion, cboBranchRegion, grdBranches, lblBranchCode, lblBranchName, txtBranchCode, txtBranchName, chkBranchActive, btnBranchAdd, btnBranchUpdate, btnBranchClear });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 590);
            Controls.AddRange(new Control[] { grpOrgs, grpTenants, grpRegions, grpBranches });
            Name = "MasterDataManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Master Data Manager";
            ResumeLayout(false);
        }
    }
}