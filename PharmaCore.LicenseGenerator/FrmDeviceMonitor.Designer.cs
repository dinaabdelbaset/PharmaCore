namespace PharmaCore.ActivationManager
{
    partial class FrmDeviceMonitor
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing){ if (disposing && (components != null)){ components.Dispose(); } base.Dispose(disposing);}        
        private void InitializeComponent()
        {
            this.grd = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.cboOrg = new System.Windows.Forms.ComboBox();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.flwCards = new System.Windows.Forms.FlowLayoutPanel();
            this.cboStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnExportCsv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.SuspendLayout();
            // grd
            this.grd.AllowUserToAddRows = false; this.grd.AllowUserToDeleteRows=false; this.grd.ReadOnly=true; this.grd.Anchor=System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            this.grd.Location = new System.Drawing.Point(12,80); this.grd.Size = new System.Drawing.Size(960,460); this.grd.Name="grd"; this.grd.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // buttons
            this.btnRefresh.Text="Refresh"; this.btnRefresh.Location=new System.Drawing.Point(12,12); this.btnRefresh.Size=new System.Drawing.Size(90,30); this.btnRefresh.Name="btnRefresh";            
            this.btnDeactivate.Text="Deactivate"; this.btnDeactivate.Location=new System.Drawing.Point(108,12); this.btnDeactivate.Size=new System.Drawing.Size(90,30); this.btnDeactivate.Name="btnDeactivate";
            this.btnActivate.Text="Activate"; this.btnActivate.Location=new System.Drawing.Point(204,12); this.btnActivate.Size=new System.Drawing.Size(90,30); this.btnActivate.Name="btnActivate";
            // combos
            this.cboOrg.Location=new System.Drawing.Point(320,15); this.cboOrg.Size=new System.Drawing.Size(200,24); this.cboOrg.Name="cboOrg"; this.cboOrg.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.Location=new System.Drawing.Point(526,15); this.cboBranch.Size=new System.Drawing.Size(200,24); this.cboBranch.Name="cboBranch"; this.cboBranch.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            // status label
            this.lblStatus.AutoSize=true; this.lblStatus.Location=new System.Drawing.Point(740,20); this.lblStatus.Name="lblStatus"; this.lblStatus.Text="Ready";
            // flow cards
            this.flwCards.Location = new System.Drawing.Point(12, 80);
            this.flwCards.Name = "flwCards";
            this.flwCards.Size = new System.Drawing.Size(960, 460);
            this.flwCards.Anchor = System.Windows.Forms.AnchorStyles.Top|System.Windows.Forms.AnchorStyles.Bottom|System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Right;
            this.flwCards.AutoScroll = true;
            // status filter
            this.cboStatusFilter.Location = new System.Drawing.Point(732, 15);
            this.cboStatusFilter.Size = new System.Drawing.Size(110,24);
            this.cboStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusFilter.Name = "cboStatusFilter";
            // export csv
            this.btnExportCsv.Text = "Export CSV"; this.btnExportCsv.Location = new System.Drawing.Point(848,12); this.btnExportCsv.Size = new System.Drawing.Size(124,30); this.btnExportCsv.Name = "btnExportCsv";
            // remove data grid from layout (keep for internal use hidden)
            this.grd.Visible = false;
            this.Controls.Add(this.flwCards);
            this.Controls.Add(this.cboStatusFilter);
            this.Controls.Add(this.btnExportCsv);
            // initialize status filter values
            this.cboStatusFilter.Items.AddRange(new object[]{"All","Active","Inactive","Stale"});
            this.cboStatusFilter.SelectedIndex = 0;
            // form
            this.ClientSize=new System.Drawing.Size(984,561); this.Controls.Add(this.grd); this.Controls.Add(this.btnRefresh); this.Controls.Add(this.btnDeactivate); this.Controls.Add(this.btnActivate); this.Controls.Add(this.cboOrg); this.Controls.Add(this.cboBranch); this.Controls.Add(this.lblStatus); this.Text="Device Monitor"; this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += FrmDeviceMonitor_Load;
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView grd; private System.Windows.Forms.Button btnRefresh; private System.Windows.Forms.Button btnDeactivate; private System.Windows.Forms.Button btnActivate; private System.Windows.Forms.ComboBox cboOrg; private System.Windows.Forms.ComboBox cboBranch; private System.Windows.Forms.Label lblStatus;private System.Windows.Forms.FlowLayoutPanel flwCards; private System.Windows.Forms.ComboBox cboStatusFilter; private System.Windows.Forms.Button btnExportCsv;
    }
}