namespace PharmaCore.POS.PL
{
    partial class Frm_BlackBox
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
            panelFilters = new Panel();
            lblTitle = new Label();
            lblDateFrom = new Label();
            dtFrom = new DateTimePicker();
            lblDateTo = new Label();
            dtTo = new DateTimePicker();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblEventType = new Label();
            cmbEventType = new ComboBox();
            lblScreen = new Label();
            cmbScreen = new ComboBox();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnRefresh = new Button();
            lblCount = new Label();
            tabControl = new TabControl();
            tabEvents = new TabPage();
            dgvEvents = new DataGridView();
            tabSessions = new TabPage();
            dgvSessions = new DataGridView();
            panelDetail = new Panel();
            lblDetailTitle = new Label();
            dgvChanges = new DataGridView();
            panelPaging = new Panel();
            btnPrev = new Button();
            lblPage = new Label();
            btnNext = new Button();

            panelFilters.SuspendLayout();
            tabControl.SuspendLayout();
            tabEvents.SuspendLayout();
            tabSessions.SuspendLayout();
            panelDetail.SuspendLayout();
            panelPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChanges).BeginInit();
            SuspendLayout();

            // ── panelFilters ──
            panelFilters.BackColor = Color.FromArgb(245, 245, 250);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Size = new Size(1000, 110);
            panelFilters.Padding = new Padding(8);
            panelFilters.Controls.Add(lblTitle);
            panelFilters.Controls.Add(lblDateFrom);
            panelFilters.Controls.Add(dtFrom);
            panelFilters.Controls.Add(lblDateTo);
            panelFilters.Controls.Add(dtTo);
            panelFilters.Controls.Add(lblCategory);
            panelFilters.Controls.Add(cmbCategory);
            panelFilters.Controls.Add(lblEventType);
            panelFilters.Controls.Add(cmbEventType);
            panelFilters.Controls.Add(lblScreen);
            panelFilters.Controls.Add(cmbScreen);
            panelFilters.Controls.Add(txtSearch);
            panelFilters.Controls.Add(btnSearch);
            panelFilters.Controls.Add(btnRefresh);
            panelFilters.Controls.Add(lblCount);

            // ── Title ──
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 60);
            lblTitle.Location = new Point(800, 5);
            lblTitle.Size = new Size(190, 30);
            lblTitle.Text = "📦 الصندوق الأسود";
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // ── Row 1: Date filters ──
            lblDateFrom.Text = "من:";
            lblDateFrom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateFrom.Location = new Point(930, 40);
            lblDateFrom.Size = new Size(35, 22);
            lblDateFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Font = new Font("Segoe UI", 9F);
            dtFrom.Location = new Point(800, 38);
            dtFrom.Size = new Size(125, 24);
            dtFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblDateTo.Text = "إلى:";
            lblDateTo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateTo.Location = new Point(750, 40);
            lblDateTo.Size = new Size(40, 22);
            lblDateTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Font = new Font("Segoe UI", 9F);
            dtTo.Location = new Point(620, 38);
            dtTo.Size = new Size(125, 24);
            dtTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblCategory.Text = "التصنيف:";
            lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCategory.Location = new Point(540, 40);
            lblCategory.Size = new Size(70, 22);
            lblCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 9F);
            cmbCategory.Location = new Point(400, 38);
            cmbCategory.Size = new Size(135, 24);
            cmbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // ── Row 2: More filters ──
            lblEventType.Text = "الأمر:";
            lblEventType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEventType.Location = new Point(930, 75);
            lblEventType.Size = new Size(50, 22);
            lblEventType.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbEventType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEventType.Font = new Font("Segoe UI", 9F);
            cmbEventType.Location = new Point(800, 73);
            cmbEventType.Size = new Size(125, 24);
            cmbEventType.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblScreen.Text = "الشاشة:";
            lblScreen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblScreen.Location = new Point(725, 75);
            lblScreen.Size = new Size(65, 22);
            lblScreen.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbScreen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScreen.Font = new Font("Segoe UI", 9F);
            cmbScreen.Location = new Point(585, 73);
            cmbScreen.Size = new Size(135, 24);
            cmbScreen.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(300, 73);
            txtSearch.Size = new Size(270, 24);
            txtSearch.PlaceholderText = "🔍 بحث بالوصف أو اسم الموظف...";
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnSearch.Text = "بحث";
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.Location = new Point(210, 71);
            btnSearch.Size = new Size(80, 28);

            btnRefresh.Text = "🔄 تحديث";
            btnRefresh.BackColor = Color.FromArgb(40, 167, 69);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.Location = new Point(120, 71);
            btnRefresh.Size = new Size(80, 28);

            lblCount.Font = new Font("Segoe UI", 9F);
            lblCount.ForeColor = Color.Gray;
            lblCount.Location = new Point(10, 77);
            lblCount.Size = new Size(100, 20);
            lblCount.Text = "";

            // ── TabControl ──
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.TabPages.Add(tabEvents);
            tabControl.TabPages.Add(tabSessions);

            tabEvents.Text = "📋 سجل الأحداث";
            tabEvents.Controls.Add(dgvEvents);
            tabEvents.Controls.Add(panelPaging);

            tabSessions.Text = "🔐 جلسات الدخول";
            tabSessions.Controls.Add(dgvSessions);

            // ── dgvEvents ──
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.ReadOnly = true;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.Font = new Font("Segoe UI", 9F);
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 255);

            // ── dgvSessions ──
            dgvSessions.Dock = DockStyle.Fill;
            dgvSessions.ReadOnly = true;
            dgvSessions.AllowUserToAddRows = false;
            dgvSessions.AllowUserToDeleteRows = false;
            dgvSessions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSessions.RowHeadersVisible = false;
            dgvSessions.Font = new Font("Segoe UI", 9F);
            dgvSessions.BackgroundColor = Color.White;
            dgvSessions.BorderStyle = BorderStyle.None;
            dgvSessions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 255);

            // ── panelPaging ──
            panelPaging.Dock = DockStyle.Bottom;
            panelPaging.Size = new Size(1000, 35);
            panelPaging.Controls.Add(btnPrev);
            panelPaging.Controls.Add(lblPage);
            panelPaging.Controls.Add(btnNext);

            btnPrev.Text = "◀ السابق";
            btnPrev.Location = new Point(520, 5);
            btnPrev.Size = new Size(80, 25);
            btnPrev.Font = new Font("Segoe UI", 9F);
            btnPrev.Anchor = AnchorStyles.Top;

            lblPage.Text = "صفحة 1";
            lblPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPage.Location = new Point(445, 8);
            lblPage.Size = new Size(70, 20);
            lblPage.TextAlign = ContentAlignment.MiddleCenter;
            lblPage.Anchor = AnchorStyles.Top;

            btnNext.Text = "التالي ▶";
            btnNext.Location = new Point(360, 5);
            btnNext.Size = new Size(80, 25);
            btnNext.Font = new Font("Segoe UI", 9F);
            btnNext.Anchor = AnchorStyles.Top;

            // ── panelDetail ──
            panelDetail.Dock = DockStyle.Bottom;
            panelDetail.Size = new Size(1000, 150);
            panelDetail.BackColor = Color.FromArgb(250, 250, 255);
            panelDetail.Controls.Add(dgvChanges);
            panelDetail.Controls.Add(lblDetailTitle);

            lblDetailTitle.Text = "📝 تفاصيل التغييرات:";
            lblDetailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDetailTitle.Dock = DockStyle.Top;
            lblDetailTitle.Size = new Size(1000, 25);
            lblDetailTitle.ForeColor = Color.FromArgb(30, 30, 60);
            lblDetailTitle.Padding = new Padding(5, 3, 0, 0);

            dgvChanges.Dock = DockStyle.Fill;
            dgvChanges.ReadOnly = true;
            dgvChanges.AllowUserToAddRows = false;
            dgvChanges.AllowUserToDeleteRows = false;
            dgvChanges.RowHeadersVisible = false;
            dgvChanges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChanges.Font = new Font("Segoe UI", 9F);
            dgvChanges.BackgroundColor = Color.White;
            dgvChanges.BorderStyle = BorderStyle.None;

            // ── Frm_BlackBox ──
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            Controls.Add(tabControl);
            Controls.Add(panelDetail);
            Controls.Add(panelFilters);
            Font = new Font("Tahoma", 11F);
            MinimumSize = new Size(850, 550);
            Name = "Frm_BlackBox";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "📦 الصندوق الأسود — سجل العمليات";
            WindowState = FormWindowState.Maximized;
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            tabControl.ResumeLayout(false);
            tabEvents.ResumeLayout(false);
            tabSessions.ResumeLayout(false);
            panelDetail.ResumeLayout(false);
            panelPaging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSessions).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChanges).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFilters;
        private Label lblTitle;
        private Label lblDateFrom;
        private DateTimePicker dtFrom;
        private Label lblDateTo;
        private DateTimePicker dtTo;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblEventType;
        private ComboBox cmbEventType;
        private Label lblScreen;
        private ComboBox cmbScreen;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private Label lblCount;
        private TabControl tabControl;
        private TabPage tabEvents;
        private DataGridView dgvEvents;
        private TabPage tabSessions;
        private DataGridView dgvSessions;
        private Panel panelDetail;
        private Label lblDetailTitle;
        private DataGridView dgvChanges;
        private Panel panelPaging;
        private Button btnPrev;
        private Label lblPage;
        private Button btnNext;
    }
}
