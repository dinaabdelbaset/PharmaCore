namespace PharmaCore.POS.PL
{
    partial class Frm_BranchEmployeeAccess
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.ComboBox cmbAccessType;
        private System.Windows.Forms.CheckBox chkWindow;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Panel panelTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelTop = new System.Windows.Forms.Panel();
            txtId = new System.Windows.Forms.TextBox();
            txtEmployeeId = new System.Windows.Forms.TextBox();
            cmbAccessType = new System.Windows.Forms.ComboBox();
            chkWindow = new System.Windows.Forms.CheckBox();
            dtFrom = new System.Windows.Forms.DateTimePicker();
            dtTo = new System.Windows.Forms.DateTimePicker();
            chkActive = new System.Windows.Forms.CheckBox();
            btnRefresh = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            grid = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();

            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 110;

            var lblId = new System.Windows.Forms.Label { Text = "Id", Left = 10, Top = 10, Width = 60 };
            txtId.Left = 10; txtId.Top = 35; txtId.Width = 220;

            var lblEmp = new System.Windows.Forms.Label { Text = "EmployeeId", Left = 240, Top = 10, Width = 90 };
            txtEmployeeId.Left = 240; txtEmployeeId.Top = 35; txtEmployeeId.Width = 220;

            var lblType = new System.Windows.Forms.Label { Text = "النوع", Left = 470, Top = 10, Width = 60 };
            cmbAccessType.Left = 470; cmbAccessType.Top = 35; cmbAccessType.Width = 120;
            cmbAccessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbAccessType.Items.AddRange(new object[] { "دائم", "فترة" });
            cmbAccessType.SelectedIndex = 0;

            chkWindow.Text = "تحديد وقت";
            chkWindow.Left = 600; chkWindow.Top = 35; chkWindow.Width = 90;

            dtFrom.Left = 700; dtFrom.Top = 35; dtFrom.Width = 170;
            dtTo.Left = 880; dtTo.Top = 35; dtTo.Width = 170;

            chkActive.Text = "مفعل";
            chkActive.Left = 1060; chkActive.Top = 35;
            chkActive.Checked = true;

            btnRefresh.Text = "تحديث"; btnRefresh.Left = 10; btnRefresh.Top = 70;
            btnAdd.Text = "إضافة"; btnAdd.Left = 90; btnAdd.Top = 70;
            btnUpdate.Text = "تعديل"; btnUpdate.Left = 170; btnUpdate.Top = 70;
            btnDelete.Text = "حذف"; btnDelete.Left = 250; btnDelete.Top = 70;

            panelTop.Controls.Add(lblId);
            panelTop.Controls.Add(txtId);
            panelTop.Controls.Add(lblEmp);
            panelTop.Controls.Add(txtEmployeeId);
            panelTop.Controls.Add(lblType);
            panelTop.Controls.Add(cmbAccessType);
            panelTop.Controls.Add(chkWindow);
            panelTop.Controls.Add(dtFrom);
            panelTop.Controls.Add(dtTo);
            panelTop.Controls.Add(chkActive);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(btnUpdate);
            panelTop.Controls.Add(btnDelete);

            grid.Dock = System.Windows.Forms.DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            ClientSize = new System.Drawing.Size(1200, 650);
            Controls.Add(grid);
            Controls.Add(panelTop);
            Text = "صلاحيات دخول الموظفين للفرع";

            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
