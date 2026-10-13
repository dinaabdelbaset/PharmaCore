using PharmaCore.LicenseGenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmaCore.ActivationManager.ServerRegistry;

namespace PharmaCore.ActivationManager
{
    public partial class Home : Form
    {
        // ── Currently selected server ──
        private static ServerEntry? _activeServer;
        private List<ServerEntry> _servers = new();

        /// <summary>Current selected server (accessible from other forms).</summary>
        public static ServerEntry? ActiveServer => _activeServer;

        /// <summary>Connection string for the currently selected server.</summary>
        public static string? ActiveConnectionString => _activeServer?.BuildConnectionString();

        // ── UI fields (built in code) ──
        private Panel pnlHeader;
        private Label lblAppTitle, lblAppSubtitle;
        private Panel pnlServerSelector;
        private Label lblSelectServer, lblServerStatus;
        private ComboBox cboServerSelector;
        private Button btnManageServers, btnRefreshServers;
        private Panel pnlDashboard;
        private Label lblDashOrgs, lblDashBranches, lblDashDevices, lblDashCodes;
        private Panel pnlNav;
        private Button btnActMng, btnMastDtMng, btnDevMnt, btnBrchUse, btnDbSettings;
        private Panel pnlStatusBar;
        private Label lblStatus;

        public Home()
        {
            InitializeComponent();
            BuildCustomUI();
        }

        private void BuildCustomUI()
        {
            BackColor = Color.FromArgb(30, 30, 46);
            ForeColor = Color.FromArgb(205, 214, 244);
            Font = new Font("Segoe UI", 10F);
            StartPosition = FormStartPosition.CenterScreen;

            // ── Header ──
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top, Height = 70,
                BackColor = Color.FromArgb(24, 24, 37)
            };
            lblAppTitle = new Label
            {
                Text = "💊 PharmaCore Control Center",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 166, 247),
                AutoSize = true, Location = new Point(15, 8)
            };
            lblAppSubtitle = new Label
            {
                Text = "Multi-Server SaaS Management",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(147, 153, 178),
                AutoSize = true, Location = new Point(18, 42)
            };
            pnlHeader.Controls.AddRange(new Control[] { lblAppTitle, lblAppSubtitle });

            // ── Server Selector ──
            pnlServerSelector = new Panel
            {
                Dock = DockStyle.Top, Height = 65,
                BackColor = Color.FromArgb(36, 36, 52)
            };
            lblSelectServer = new Label
            {
                Text = "🌐 Active Server:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 194, 231),
                AutoSize = true, Location = new Point(15, 22)
            };
            cboServerSelector = new ComboBox
            {
                Location = new Point(150, 18), Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(49, 50, 68),
                ForeColor = Color.White
            };
            cboServerSelector.SelectedIndexChanged += CboServerSelector_SelectedIndexChanged;

            lblServerStatus = new Label
            {
                Text = "⚪", AutoSize = true,
                Location = new Point(510, 22),
                Font = new Font("Segoe UI", 10F)
            };
            btnManageServers = new Button
            {
                Text = "⚙ Manage", Location = new Point(560, 16), Size = new Size(95, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(137, 180, 250),
                ForeColor = Color.FromArgb(30, 30, 46),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnManageServers.Click += btnManageServers_Click;

            btnRefreshServers = new Button
            {
                Text = "🔄", Location = new Point(660, 16), Size = new Size(40, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(69, 71, 90),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnRefreshServers.Click += async (_, __) => await LoadServerListAsync();

            pnlServerSelector.Controls.AddRange(new Control[] { lblSelectServer, cboServerSelector, lblServerStatus, btnManageServers, btnRefreshServers });

            // ── Dashboard Cards ──
            pnlDashboard = new Panel
            {
                Dock = DockStyle.Top, Height = 90,
                BackColor = Color.FromArgb(30, 30, 46)
            };
            lblDashOrgs = CreateDashCard("🏢 Orgs", "—", 15);
            lblDashBranches = CreateDashCard("🏪 Branches", "—", 190);
            lblDashDevices = CreateDashCard("📱 Devices", "—", 365);
            lblDashCodes = CreateDashCard("🔑 Codes", "—", 540);

            // ── Navigation Buttons ──
            pnlNav = new Panel
            {
                Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 46)
            };
            btnMastDtMng = CreateNavButton("📊 Master Data Manager", Color.FromArgb(137, 180, 250), 0);
            btnMastDtMng.Click += btnMastDtMng_Click;
            btnActMng = CreateNavButton("🔑 Activation Manager", Color.FromArgb(166, 227, 161), 1);
            btnActMng.Click += btnActMng_Click;
            btnBrchUse = CreateNavButton("👥 Branch Users", Color.FromArgb(245, 194, 231), 2);
            btnBrchUse.Click += btnBrchUse_Click;
            btnDevMnt = CreateNavButton("📡 Device Monitor", Color.FromArgb(250, 179, 135), 3);
            btnDevMnt.Click += btnDevMnt_Click;
            btnDbSettings = CreateNavButton("⚙ DB Connection Settings", Color.FromArgb(147, 153, 178), 4);
            btnDbSettings.Click += btnDbSettings_Click;

            var btnGlobalCat = CreateNavButton("🌍 Global Drug Catalog (Drug Eye)", Color.FromArgb(100, 200, 255), 5);
            btnGlobalCat.Click += (s, e) => { if (EnsureServerSelected()) new FrmGlobalCatalogManager(Home.ActiveConnectionString).Show(); };

            pnlNav.Controls.AddRange(new Control[] { btnMastDtMng, btnActMng, btnBrchUse, btnDevMnt, btnDbSettings, btnGlobalCat });

            // ── Status Bar ──
            pnlStatusBar = new Panel
            {
                Dock = DockStyle.Bottom, Height = 30,
                BackColor = Color.FromArgb(24, 24, 37)
            };
            lblStatus = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(147, 153, 178),
                Font = new Font("Segoe UI", 8.5F),
                Text = "PharmaCore Control Center v2.0 — Multi-Server Edition",
                Padding = new Padding(10, 5, 0, 0)
            };
            pnlStatusBar.Controls.Add(lblStatus);

            // ── Add panels (bottom → top for Dock) ──
            Controls.Add(pnlNav);
            Controls.Add(pnlDashboard);
            Controls.Add(pnlServerSelector);
            Controls.Add(pnlHeader);
            Controls.Add(pnlStatusBar);
        }

        private Label CreateDashCard(string title, string value, int left)
        {
            var pnl = new Panel
            {
                Location = new Point(left, 5), Size = new Size(160, 70),
                BackColor = Color.FromArgb(49, 50, 68)
            };
            var lblT = new Label
            {
                Text = title, Dock = DockStyle.Top, Height = 20,
                ForeColor = Color.FromArgb(147, 153, 178),
                Font = new Font("Segoe UI", 8.5F)
            };
            var lblV = new Label
            {
                Text = value, Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnl.Controls.Add(lblV);
            pnl.Controls.Add(lblT);
            pnlDashboard.Controls.Add(pnl);
            return lblV;
        }

        private Button CreateNavButton(string text, Color accentColor, int index)
        {
            int y = 10 + index * 58;
            var btn = new Button
            {
                Text = text,
                Location = new Point(20, y),
                Size = new Size(700, 48),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(36, 36, 52),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = accentColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(49, 50, 68);
            return btn;
        }

        // ── Events ──

        private async void Home_Load(object sender, EventArgs e)
        {
            await LoadServerListAsync();
        }

        private async Task LoadServerListAsync()
        {
            try
            {
                var controlCs = ControlDbConnection.Get();
                await ServerRegistry.EnsureSchemaAsync(controlCs);
                _servers = await ServerRegistry.ListAsync(controlCs, includeInactive: false);

                cboServerSelector.DataSource = null;
                cboServerSelector.Items.Clear();

                if (_servers.Count == 0)
                {
                    cboServerSelector.Items.Add("(No servers — click Manage to add)");
                    cboServerSelector.SelectedIndex = 0;
                    lblServerStatus.Text = "⚪ No servers";
                    UpdateDashboard(null);
                }
                else
                {
                    cboServerSelector.DataSource = _servers;
                    cboServerSelector.DisplayMember = "DisplayLabel";

                    if (_activeServer != null)
                    {
                        var idx = _servers.FindIndex(s => s.Id == _activeServer.Id);
                        if (idx >= 0) cboServerSelector.SelectedIndex = idx;
                    }
                    else
                    {
                        cboServerSelector.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"❌ Error: {ex.Message}";
            }
        }

        private async void CboServerSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cboServerSelector.SelectedItem is not ServerEntry server) return;
            _activeServer = server;

            lblServerStatus.Text = server.LastHealthStatus switch
            {
                "Online" => "🟢",
                "Slow" => "🟡",
                "Offline" => "🔴",
                _ => "⚪"
            };
            lblStatus.Text = $"Active: {server.ServerName} — {server.Host}:{server.Port}/{server.DatabaseName}";
            await LoadDashboardStatsAsync(server);
        }

        private async Task LoadDashboardStatsAsync(ServerEntry server)
        {
            try
            {
                await ServerRegistry.FetchStatsAsync(server);
                UpdateDashboard(server);
            }
            catch { UpdateDashboard(null); }
        }

        private void UpdateDashboard(ServerEntry? server)
        {
            lblDashOrgs.Text = server?.OrgCount.ToString() ?? "—";
            lblDashBranches.Text = server?.BranchCount.ToString() ?? "—";
            lblDashDevices.Text = server?.DeviceCount.ToString() ?? "—";
            lblDashCodes.Text = server?.ActiveCodeCount.ToString() ?? "—";
        }

        // ── Navigation ──

        private void btnManageServers_Click(object? sender, EventArgs e)
        {
            var cs = ControlDbConnection.Get();
            var frm = new FrmServerRegistry(cs);
            frm.FormClosed += async (_, __) => await LoadServerListAsync();
            frm.Show();
        }

        private void btnActMng_Click(object sender, EventArgs e)
        {
            if (!EnsureServerSelected()) return;
            new FrmActivationManager().Show();
        }

        private void btnMastDtMng_Click(object sender, EventArgs e)
        {
            if (!EnsureServerSelected()) return;
            new MasterDataManager().Show();
        }

        private void btnDevMnt_Click(object sender, EventArgs e)
        {
            if (!EnsureServerSelected()) return;
            new FrmDeviceMonitor().Show();
        }

        private void btnBrchUse_Click(object sender, EventArgs e)
        {
            if (!EnsureServerSelected()) return;
            new FrmBranchUsers().Show();
        }

        private void btnDbSettings_Click(object sender, EventArgs e)
        {
            ReconfigureDbConnection();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e) { }

        private bool EnsureServerSelected()
        {
            if (_activeServer == null)
            {
                MessageBox.Show(
                    "يرجى اختيار سيرفر أولاً من القائمة بالأعلى.\nأو اضغط 'Manage' لإضافة سيرفر جديد.",
                    "⚠ No Server Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static void ReconfigureDbConnection()
        {
            string current = "";
            if (ControlDbConnection.TryGet(out var cs))
                current = cs;

            using var form = new Form
            {
                Text = "إعداد اتصال قاعدة البيانات المركزية",
                Width = 620, Height = 250,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false, MinimizeBox = false,
                BackColor = Color.FromArgb(30, 30, 46),
                ForeColor = Color.FromArgb(205, 214, 244)
            };
            var lbl = new Label { Text = "سلسلة اتصال PostgreSQL (Control DB):", Left = 15, Top = 15, Width = 580, ForeColor = Color.FromArgb(186, 194, 222) };
            var txt = new TextBox
            {
                Left = 15, Top = 40, Width = 575, Height = 60, Multiline = true,
                Text = current,
                BackColor = Color.FromArgb(49, 50, 68), ForeColor = Color.White,
                PlaceholderText = "Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true"
            };
            var lblSt = new Label
            {
                Text = ControlDbConnection.IsConfigured ? "✅ الاتصال مضبوط" : "⚠ غير مضبوط",
                Left = 15, Top = 110, Width = 300,
                ForeColor = ControlDbConnection.IsConfigured ? Color.FromArgb(166, 227, 161) : Color.OrangeRed
            };
            var btnTest = new Button
            {
                Text = "⚡ اختبار", Left = 310, Top = 140, Width = 85,
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(250, 179, 135),
                ForeColor = Color.FromArgb(30, 30, 46), Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            var btnSave = new Button
            {
                Text = "💾 حفظ", Left = 405, Top = 140, Width = 85, DialogResult = DialogResult.OK,
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(166, 227, 161),
                ForeColor = Color.FromArgb(30, 30, 46), Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            var btnCancel = new Button
            {
                Text = "إلغاء", Left = 500, Top = 140, Width = 85, DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(69, 71, 90), ForeColor = Color.White
            };

            btnTest.Click += async (_, __) =>
            {
                try
                {
                    var testCs = txt.Text.Trim();
                    _ = new Npgsql.NpgsqlConnectionStringBuilder(testCs);
                    await using var con = new Npgsql.NpgsqlConnection(testCs);
                    await con.OpenAsync();
                    lblSt.Text = "✅ الاتصال ناجح — " + con.ServerVersion;
                    lblSt.ForeColor = Color.FromArgb(166, 227, 161);
                }
                catch (Exception ex)
                {
                    lblSt.Text = "❌ فشل: " + ex.Message;
                    lblSt.ForeColor = Color.FromArgb(243, 139, 168);
                }
            };

            form.Controls.AddRange(new Control[] { lbl, txt, lblSt, btnTest, btnSave, btnCancel });
            form.AcceptButton = btnSave;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
            {
                var newCs = txt.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newCs))
                {
                    try
                    {
                        _ = new Npgsql.NpgsqlConnectionStringBuilder(newCs);
                        ControlDbConnection.Set(newCs);
                        MessageBox.Show("تم حفظ إعدادات الاتصال بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("صيغة الاتصال غير صحيحة:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
