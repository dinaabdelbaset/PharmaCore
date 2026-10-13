using System.Windows.Forms;
using System.Drawing;

namespace PharmaCore.ActivationManager
{
    partial class MasterDataManager : Form
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

        private CheckBox chkFilterActive; // new global filter checkbox

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            label1 = new Label();
            lblRegion = new Label();
            lblOrgCode = new Label();
            grpTenants = new GroupBox();
            label4 = new Label();
            label5 = new Label();
            grpRegions = new GroupBox();
            label2 = new Label();
            label3 = new Label();
            grpBranches = new GroupBox();
            label6 = new Label();
            label7 = new Label();
            chkFilterActive = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)grdOrgs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grdTenants).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grdRegions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grdBranches).BeginInit();
            grpOrgs.SuspendLayout();
            grpTenants.SuspendLayout();
            grpRegions.SuspendLayout();
            grpBranches.SuspendLayout();
            SuspendLayout();
            // 
            // grdOrgs
            // 
            grdOrgs.AllowUserToAddRows = false;
            grdOrgs.AllowUserToDeleteRows = false;
            grdOrgs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdOrgs.EditMode = DataGridViewEditMode.EditProgrammatically;
            grdOrgs.Location = new Point(10, 20);
            grdOrgs.MultiSelect = false;
            grdOrgs.Name = "grdOrgs";
            grdOrgs.ReadOnly = true;
            grdOrgs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdOrgs.Size = new Size(500, 150);
            grdOrgs.TabIndex = 0;
            // 
            // txtOrgCode
            // 
            txtOrgCode.Location = new Point(81, 177);
            txtOrgCode.Name = "txtOrgCode";
            txtOrgCode.Size = new Size(140, 23);
            txtOrgCode.TabIndex = 3;
            // 
            // txtOrgName
            // 
            txtOrgName.Location = new Point(81, 206);
            txtOrgName.Name = "txtOrgName";
            txtOrgName.Size = new Size(220, 23);
            txtOrgName.TabIndex = 4;
            // 
            // chkOrgActive
            // 
            chkOrgActive.AutoSize = true;
            chkOrgActive.Location = new Point(310, 176);
            chkOrgActive.Name = "chkOrgActive";
            chkOrgActive.Size = new Size(59, 19);
            chkOrgActive.TabIndex = 5;
            chkOrgActive.Text = "Active";
            // 
            // btnOrgAdd
            // 
            btnOrgAdd.Location = new Point(70, 240);
            btnOrgAdd.Name = "btnOrgAdd";
            btnOrgAdd.Size = new Size(80, 23);
            btnOrgAdd.TabIndex = 6;
            btnOrgAdd.Text = "Add";
            // 
            // btnOrgUpdate
            // 
            btnOrgUpdate.Location = new Point(160, 240);
            btnOrgUpdate.Name = "btnOrgUpdate";
            btnOrgUpdate.Size = new Size(80, 23);
            btnOrgUpdate.TabIndex = 7;
            btnOrgUpdate.Text = "Update";
            // 
            // btnOrgClear
            // 
            btnOrgClear.Location = new Point(250, 240);
            btnOrgClear.Name = "btnOrgClear";
            btnOrgClear.Size = new Size(80, 23);
            btnOrgClear.TabIndex = 8;
            btnOrgClear.Text = "Clear";
            // 
            // cboTenantOrg
            // 
            cboTenantOrg.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTenantOrg.Location = new Point(100, 18);
            cboTenantOrg.Name = "cboTenantOrg";
            cboTenantOrg.Size = new Size(160, 23);
            cboTenantOrg.TabIndex = 1;
            // 
            // grdTenants
            // 
            grdTenants.AllowUserToAddRows = false;
            grdTenants.AllowUserToDeleteRows = false;
            grdTenants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdTenants.EditMode = DataGridViewEditMode.EditProgrammatically;
            grdTenants.Location = new Point(10, 50);
            grdTenants.MultiSelect = false;
            grdTenants.Name = "grdTenants";
            grdTenants.ReadOnly = true;
            grdTenants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdTenants.Size = new Size(500, 120);
            grdTenants.TabIndex = 2;
            // 
            // txtTenantCode
            // 
            txtTenantCode.Location = new Point(84, 176);
            txtTenantCode.Name = "txtTenantCode";
            txtTenantCode.Size = new Size(140, 23);
            txtTenantCode.TabIndex = 5;
            // 
            // txtTenantName
            // 
            txtTenantName.Location = new Point(84, 206);
            txtTenantName.Name = "txtTenantName";
            txtTenantName.Size = new Size(220, 23);
            txtTenantName.TabIndex = 6;
            // 
            // chkTenantActive
            // 
            chkTenantActive.AutoSize = true;
            chkTenantActive.Location = new Point(310, 176);
            chkTenantActive.Name = "chkTenantActive";
            chkTenantActive.Size = new Size(59, 19);
            chkTenantActive.TabIndex = 7;
            chkTenantActive.Text = "Active";
            // 
            // btnTenantAdd
            // 
            btnTenantAdd.Location = new Point(70, 240);
            btnTenantAdd.Name = "btnTenantAdd";
            btnTenantAdd.Size = new Size(80, 23);
            btnTenantAdd.TabIndex = 8;
            btnTenantAdd.Text = "Add";
            // 
            // btnTenantUpdate
            // 
            btnTenantUpdate.Location = new Point(160, 240);
            btnTenantUpdate.Name = "btnTenantUpdate";
            btnTenantUpdate.Size = new Size(80, 23);
            btnTenantUpdate.TabIndex = 9;
            btnTenantUpdate.Text = "Update";
            // 
            // btnTenantClear
            // 
            btnTenantClear.Location = new Point(250, 240);
            btnTenantClear.Name = "btnTenantClear";
            btnTenantClear.Size = new Size(80, 23);
            btnTenantClear.TabIndex = 10;
            btnTenantClear.Text = "Clear";
            // 
            // cboRegionTenant
            // 
            cboRegionTenant.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRegionTenant.Location = new Point(70, 18);
            cboRegionTenant.Name = "cboRegionTenant";
            cboRegionTenant.Size = new Size(160, 23);
            cboRegionTenant.TabIndex = 1;
            // 
            // grdRegions
            // 
            grdRegions.AllowUserToAddRows = false;
            grdRegions.AllowUserToDeleteRows = false;
            grdRegions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdRegions.EditMode = DataGridViewEditMode.EditProgrammatically;
            grdRegions.Location = new Point(10, 50);
            grdRegions.MultiSelect = false;
            grdRegions.Name = "grdRegions";
            grdRegions.ReadOnly = true;
            grdRegions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdRegions.Size = new Size(500, 120);
            grdRegions.TabIndex = 2;
            // 
            // txtRegionCode
            // 
            txtRegionCode.Location = new Point(88, 177);
            txtRegionCode.Name = "txtRegionCode";
            txtRegionCode.Size = new Size(140, 23);
            txtRegionCode.TabIndex = 5;
            // 
            // txtRegionName
            // 
            txtRegionName.Location = new Point(88, 206);
            txtRegionName.Name = "txtRegionName";
            txtRegionName.Size = new Size(220, 23);
            txtRegionName.TabIndex = 6;
            // 
            // chkRegionActive
            // 
            chkRegionActive.AutoSize = true;
            chkRegionActive.Location = new Point(310, 176);
            chkRegionActive.Name = "chkRegionActive";
            chkRegionActive.Size = new Size(59, 19);
            chkRegionActive.TabIndex = 7;
            chkRegionActive.Text = "Active";
            // 
            // btnRegionAdd
            // 
            btnRegionAdd.Location = new Point(70, 240);
            btnRegionAdd.Name = "btnRegionAdd";
            btnRegionAdd.Size = new Size(80, 23);
            btnRegionAdd.TabIndex = 8;
            btnRegionAdd.Text = "Add";
            // 
            // btnRegionUpdate
            // 
            btnRegionUpdate.Location = new Point(160, 240);
            btnRegionUpdate.Name = "btnRegionUpdate";
            btnRegionUpdate.Size = new Size(80, 23);
            btnRegionUpdate.TabIndex = 9;
            btnRegionUpdate.Text = "Update";
            // 
            // btnRegionClear
            // 
            btnRegionClear.Location = new Point(250, 240);
            btnRegionClear.Name = "btnRegionClear";
            btnRegionClear.Size = new Size(80, 23);
            btnRegionClear.TabIndex = 10;
            btnRegionClear.Text = "Clear";
            // 
            // cboBranchRegion
            // 
            cboBranchRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBranchRegion.Location = new Point(70, 18);
            cboBranchRegion.Name = "cboBranchRegion";
            cboBranchRegion.Size = new Size(160, 23);
            cboBranchRegion.TabIndex = 1;
            // 
            // grdBranches
            // 
            grdBranches.AllowUserToAddRows = false;
            grdBranches.AllowUserToDeleteRows = false;
            grdBranches.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdBranches.EditMode = DataGridViewEditMode.EditProgrammatically;
            grdBranches.Location = new Point(10, 50);
            grdBranches.MultiSelect = false;
            grdBranches.Name = "grdBranches";
            grdBranches.ReadOnly = true;
            grdBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdBranches.Size = new Size(500, 120);
            grdBranches.TabIndex = 2;
            // 
            // txtBranchCode
            // 
            txtBranchCode.Location = new Point(84, 174);
            txtBranchCode.Name = "txtBranchCode";
            txtBranchCode.Size = new Size(140, 23);
            txtBranchCode.TabIndex = 5;
            // 
            // txtBranchName
            // 
            txtBranchName.Location = new Point(84, 206);
            txtBranchName.Name = "txtBranchName";
            txtBranchName.Size = new Size(220, 23);
            txtBranchName.TabIndex = 6;
            // 
            // chkBranchActive
            // 
            chkBranchActive.AutoSize = true;
            chkBranchActive.Location = new Point(310, 176);
            chkBranchActive.Name = "chkBranchActive";
            chkBranchActive.Size = new Size(59, 19);
            chkBranchActive.TabIndex = 7;
            chkBranchActive.Text = "Active";
            // 
            // btnBranchAdd
            // 
            btnBranchAdd.Location = new Point(70, 240);
            btnBranchAdd.Name = "btnBranchAdd";
            btnBranchAdd.Size = new Size(80, 23);
            btnBranchAdd.TabIndex = 8;
            btnBranchAdd.Text = "Add";
            // 
            // btnBranchUpdate
            // 
            btnBranchUpdate.Location = new Point(160, 240);
            btnBranchUpdate.Name = "btnBranchUpdate";
            btnBranchUpdate.Size = new Size(80, 23);
            btnBranchUpdate.TabIndex = 9;
            btnBranchUpdate.Text = "Update";
            // 
            // btnBranchClear
            // 
            btnBranchClear.Location = new Point(250, 240);
            btnBranchClear.Name = "btnBranchClear";
            btnBranchClear.Size = new Size(80, 23);
            btnBranchClear.TabIndex = 10;
            btnBranchClear.Text = "Clear";
            // 
            // grpOrgs
            // 
            grpOrgs.Controls.Add(label1);
            grpOrgs.Controls.Add(lblRegion);
            grpOrgs.Controls.Add(grdOrgs);
            grpOrgs.Controls.Add(lblOrgCode);
            grpOrgs.Controls.Add(txtOrgCode);
            grpOrgs.Controls.Add(txtOrgName);
            grpOrgs.Controls.Add(chkOrgActive);
            grpOrgs.Controls.Add(btnOrgAdd);
            grpOrgs.Controls.Add(btnOrgUpdate);
            grpOrgs.Controls.Add(btnOrgClear);
            grpOrgs.Location = new Point(10, 10);
            grpOrgs.Name = "grpOrgs";
            grpOrgs.Size = new Size(520, 280);
            grpOrgs.TabIndex = 0;
            grpOrgs.TabStop = false;
            grpOrgs.Text = "Organizations";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 209);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 10;
            label1.Text = "OrgName";
            // 
            // lblRegion
            // 
            lblRegion.AutoSize = true;
            lblRegion.Location = new Point(20, 180);
            lblRegion.Name = "lblRegion";
            lblRegion.Size = new Size(55, 15);
            lblRegion.TabIndex = 9;
            lblRegion.Text = "OrgCode";
            // 
            // lblOrgCode
            // 
            lblOrgCode.Location = new Point(81, 50);
            lblOrgCode.Name = "lblOrgCode";
            lblOrgCode.Size = new Size(100, 23);
            lblOrgCode.TabIndex = 1;
            // 
            // grpTenants
            // 
            grpTenants.Controls.Add(label4);
            grpTenants.Controls.Add(label5);
            grpTenants.Controls.Add(cboTenantOrg);
            grpTenants.Controls.Add(grdTenants);
            grpTenants.Controls.Add(txtTenantCode);
            grpTenants.Controls.Add(txtTenantName);
            grpTenants.Controls.Add(chkTenantActive);
            grpTenants.Controls.Add(btnTenantAdd);
            grpTenants.Controls.Add(btnTenantUpdate);
            grpTenants.Controls.Add(btnTenantClear);
            grpTenants.Location = new Point(540, 10);
            grpTenants.Name = "grpTenants";
            grpTenants.Size = new Size(520, 280);
            grpTenants.TabIndex = 1;
            grpTenants.TabStop = false;
            grpTenants.Text = "Tenants";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 209);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 14;
            label4.Text = "TenantName";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 180);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 13;
            label5.Text = "TenantCode";
            // 
            // grpRegions
            // 
            grpRegions.Controls.Add(label2);
            grpRegions.Controls.Add(label3);
            grpRegions.Controls.Add(cboRegionTenant);
            grpRegions.Controls.Add(grdRegions);
            grpRegions.Controls.Add(txtRegionCode);
            grpRegions.Controls.Add(txtRegionName);
            grpRegions.Controls.Add(chkRegionActive);
            grpRegions.Controls.Add(btnRegionAdd);
            grpRegions.Controls.Add(btnRegionUpdate);
            grpRegions.Controls.Add(btnRegionClear);
            grpRegions.Location = new Point(10, 300);
            grpRegions.Name = "grpRegions";
            grpRegions.Size = new Size(520, 280);
            grpRegions.TabIndex = 2;
            grpRegions.TabStop = false;
            grpRegions.Text = "Regions";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 209);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 12;
            label2.Text = "RegionName";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 180);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 11;
            label3.Text = "RegionCode";
            // 
            // grpBranches
            // 
            grpBranches.Controls.Add(label6);
            grpBranches.Controls.Add(label7);
            grpBranches.Controls.Add(cboBranchRegion);
            grpBranches.Controls.Add(grdBranches);
            grpBranches.Controls.Add(txtBranchCode);
            grpBranches.Controls.Add(txtBranchName);
            grpBranches.Controls.Add(chkBranchActive);
            grpBranches.Controls.Add(btnBranchAdd);
            grpBranches.Controls.Add(btnBranchUpdate);
            grpBranches.Controls.Add(btnBranchClear);
            grpBranches.Location = new Point(540, 300);
            grpBranches.Name = "grpBranches";
            grpBranches.Size = new Size(520, 280);
            grpBranches.TabIndex = 3;
            grpBranches.TabStop = false;
            grpBranches.Text = "Branches";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 209);
            label6.Name = "label6";
            label6.Size = new Size(76, 15);
            label6.TabIndex = 16;
            label6.Text = "BranchName";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 180);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 15;
            label7.Text = "BranchCode";
            // 
            // chkFilterActive
            // 
            chkFilterActive.AutoSize = true;
            chkFilterActive.Location = new Point(950, 8);
            chkFilterActive.Name = "chkFilterActive";
            chkFilterActive.Size = new Size(87, 19);
            chkFilterActive.TabIndex = 99;
            chkFilterActive.Text = "Active Only";
            // 
            // MasterDataManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 590);
            Controls.Add(chkFilterActive);
            Controls.Add(grpOrgs);
            Controls.Add(grpTenants);
            Controls.Add(grpRegions);
            Controls.Add(grpBranches);
            Name = "MasterDataManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Master Data Manager";
            ((System.ComponentModel.ISupportInitialize)grdOrgs).EndInit();
            ((System.ComponentModel.ISupportInitialize)grdTenants).EndInit();
            ((System.ComponentModel.ISupportInitialize)grdRegions).EndInit();
            ((System.ComponentModel.ISupportInitialize)grdBranches).EndInit();
            grpOrgs.ResumeLayout(false);
            grpOrgs.PerformLayout();
            grpTenants.ResumeLayout(false);
            grpTenants.PerformLayout();
            grpRegions.ResumeLayout(false);
            grpRegions.PerformLayout();
            grpBranches.ResumeLayout(false);
            grpBranches.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label lblRegion;
        private Label label4;
        private Label label5;
        private Label label2;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label lblOrgCode;
    }
}
