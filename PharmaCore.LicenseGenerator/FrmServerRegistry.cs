using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmaCore.ActivationManager.ServerRegistry;

namespace PharmaCore.ActivationManager
{
    /// <summary>
    /// Server Registry Dashboard — manage all PharmaCore database servers from one place.
    /// Features: add/edit/remove servers, health check with status indicators, aggregate stats.
    /// </summary>
    public sealed class FrmServerRegistry : Form
    {
        private readonly string _controlCs;

        // ── Controls ──
        private DataGridView grdServers;
        private Panel pnlDetails;
        private TextBox txtServerName, txtHost, txtPort, txtDbName, txtUsername, txtPassword, txtDescription;
        private ComboBox cboSslMode;
        private CheckBox chkActive;
        private Button btnAdd, btnUpdate, btnDelete, btnTestConnection, btnPingAll, btnRefresh, btnClear;
        private Label lblStatusBar;
        private Panel pnlStatusBar;
        private ToolTip toolTip;

        // ── State ──
        private List<ServerEntry> _servers = new();
        private Guid? _selectedId;

        public FrmServerRegistry(string controlCs)
        {
            _controlCs = controlCs;
            InitUI();
            Load += async (_, __) => await RefreshListAsync();
        }

        // ───────── UI Construction ─────────

        private void InitUI()
        {
            Text = "🌐 Server Registry — PharmaCore Multi-Server Control";
            Size = new Size(1200, 700);
            MinimumSize = new Size(1000, 550);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9.5F);
            BackColor = Color.FromArgb(30, 30, 46);
            ForeColor = Color.FromArgb(205, 214, 244);
            toolTip = new ToolTip();

            // ── TOOLBAR ──
            var pnlToolbar = new Panel
            {
                Dock = DockStyle.Top, Height = 55,
                BackColor = Color.FromArgb(24, 24, 37),
                Padding = new Padding(10, 10, 10, 10)
            };

            btnPingAll = CreateToolButton("🔄 Ping All Servers", Color.FromArgb(137, 180, 250));
            btnPingAll.Click += async (_, __) => await PingAllServersAsync();
            btnPingAll.Location = new Point(10, 10);

            btnRefresh = CreateToolButton("🔃 Refresh", Color.FromArgb(166, 227, 161));
            btnRefresh.Click += async (_, __) => await RefreshListAsync();
            btnRefresh.Location = new Point(190, 10);

            var lblTitle = new Label
            {
                Text = "🌐 Server Registry",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(203, 166, 247),
                AutoSize = true,
                Location = new Point(340, 13)
            };

            pnlToolbar.Controls.AddRange(new Control[] { btnPingAll, btnRefresh, lblTitle });
            Controls.Add(pnlToolbar);

            // ── STATUS BAR ──
            pnlStatusBar = new Panel
            {
                Dock = DockStyle.Bottom, Height = 35,
                BackColor = Color.FromArgb(24, 24, 37),
                Padding = new Padding(10, 5, 10, 5)
            };
            lblStatusBar = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(166, 227, 161),
                Font = new Font("Segoe UI", 9F),
                Text = "Ready"
            };
            pnlStatusBar.Controls.Add(lblStatusBar);
            Controls.Add(pnlStatusBar);

            // ── TWO-PANEL LAYOUT: Grid (left) + Details (right) ──
            pnlDetails = new Panel
            {
                Dock = DockStyle.Right, Width = 370,
                BackColor = Color.FromArgb(36, 36, 52),
                Padding = new Padding(15),
                AutoScroll = true
            };
            Controls.Add(pnlDetails);

            var pnlGrid = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 46)
            };
            Controls.Add(pnlGrid);
            pnlGrid.BringToFront();

            // ── LEFT: Server Grid ──
            grdServers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false, MultiSelect = false,
                BackgroundColor = Color.FromArgb(30, 30, 46),
                GridColor = Color.FromArgb(69, 71, 90),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(30, 30, 46),
                    ForeColor = Color.FromArgb(205, 214, 244),
                    SelectionBackColor = Color.FromArgb(69, 71, 90),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5F)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(24, 24, 37),
                    ForeColor = Color.FromArgb(203, 166, 247),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                },
                EnableHeadersVisualStyles = false,
                RowTemplate = { Height = 36 },
                BorderStyle = BorderStyle.None
            };
            grdServers.CellClick += (_, __) => LoadSelection();
            grdServers.CellFormatting += GrdServers_CellFormatting;
            pnlGrid.Controls.Add(grdServers);

            // ── RIGHT: Details content ──
            int y = 10;
            var lblHeader = new Label
            {
                Text = "📋 Server Details", Left = 10, Top = y, Width = 300,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(245, 194, 231)
            };
            pnlDetails.Controls.Add(lblHeader);
            y += 40;

            txtServerName = AddField(pnlDetails, "Server Name", ref y, "e.g. Cairo-Main, Alexandria-01");
            txtHost = AddField(pnlDetails, "Host / IP", ref y, "e.g. localhost or your-server-ip");
            txtPort = AddField(pnlDetails, "Port", ref y, "5432");
            txtPort.Text = "5432";
            txtDbName = AddField(pnlDetails, "Database Name", ref y, "pharmacore");
            txtUsername = AddField(pnlDetails, "Username", ref y, "postgres");
            txtPassword = AddField(pnlDetails, "Password", ref y, "");
            txtPassword.UseSystemPasswordChar = true;

            // SSL Mode dropdown
            var lblSsl = new Label { Text = "SSL Mode", Left = 10, Top = y, Width = 110, ForeColor = Color.FromArgb(186, 194, 222) };
            cboSslMode = new ComboBox
            {
                Left = 125, Top = y, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(49, 50, 68), ForeColor = Color.White
            };
            cboSslMode.Items.AddRange(new[] { "Disable", "Prefer", "Require" });
            cboSslMode.SelectedIndex = 1;
            pnlDetails.Controls.AddRange(new Control[] { lblSsl, cboSslMode });
            y += 35;

            chkActive = new CheckBox
            {
                Text = "Active", Left = 125, Top = y, Width = 100, Checked = true,
                ForeColor = Color.FromArgb(166, 227, 161)
            };
            pnlDetails.Controls.Add(chkActive);
            y += 30;

            txtDescription = AddField(pnlDetails, "Description", ref y, "Optional notes");
            y += 10;

            // ── Action Buttons ──
            btnTestConnection = CreateActionButton("⚡ Test Connection", Color.FromArgb(250, 179, 135), 10, y, 155);
            btnTestConnection.Click += async (_, __) => await TestConnectionAsync();

            btnAdd = CreateActionButton("➕ Add Server", Color.FromArgb(166, 227, 161), 170, y, 125);
            btnAdd.Click += async (_, __) => await AddServerAsync();

            pnlDetails.Controls.AddRange(new Control[] { btnTestConnection, btnAdd });
            y += 45;

            btnUpdate = CreateActionButton("💾 Update", Color.FromArgb(137, 180, 250), 10, y, 115);
            btnUpdate.Click += async (_, __) => await UpdateServerAsync();

            btnDelete = CreateActionButton("🗑 Delete", Color.FromArgb(243, 139, 168), 130, y, 100);
            btnDelete.Click += async (_, __) => await DeleteServerAsync();

            btnClear = CreateActionButton("🧹 Clear", Color.FromArgb(180, 190, 210), 235, y, 90);
            btnClear.Click += (_, __) => ClearForm();

            pnlDetails.Controls.AddRange(new Control[] { btnUpdate, btnDelete, btnClear });

            // pnlDetails already added to form via Dock = Right
        }

        // ───────── UI Helpers ─────────

        private TextBox AddField(Panel parent, string label, ref int y, string placeholder)
        {
            var lbl = new Label { Text = label, Left = 10, Top = y + 3, Width = 110, ForeColor = Color.FromArgb(186, 194, 222) };
            var txt = new TextBox
            {
                Left = 125, Top = y, Width = 200,
                BackColor = Color.FromArgb(49, 50, 68),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = placeholder
            };
            parent.Controls.AddRange(new Control[] { lbl, txt });
            y += 32;
            return txt;
        }

        private Button CreateToolButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text, Size = new Size(165, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor, ForeColor = Color.FromArgb(30, 30, 46),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private Button CreateActionButton(string text, Color backColor, int x, int y, int w)
        {
            return new Button
            {
                Text = text, Location = new Point(x, y), Size = new Size(w, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = backColor, ForeColor = Color.FromArgb(30, 30, 46),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        // ───────── Grid Formatting ─────────

        private void GrdServers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdServers.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                var val = e.Value.ToString();
                switch (val)
                {
                    case "Online": e.CellStyle.ForeColor = Color.FromArgb(166, 227, 161); break;
                    case "Slow": e.CellStyle.ForeColor = Color.FromArgb(249, 226, 175); break;
                    case "Offline": e.CellStyle.ForeColor = Color.FromArgb(243, 139, 168); break;
                    case "Timeout": e.CellStyle.ForeColor = Color.FromArgb(243, 139, 168); break;
                    default: e.CellStyle.ForeColor = Color.FromArgb(147, 153, 178); break;
                }
            }
        }

        // ───────── Data Operations ─────────

        private async Task RefreshListAsync()
        {
            try
            {
                lblStatusBar.Text = "🔄 Loading servers...";
                _servers = await ServerRegistry.ListAsync(_controlCs, includeInactive: true);

                var displayData = _servers.Select(s => new
                {
                    s.Id,
                    Status = StatusIcon(s.LastHealthStatus),
                    Name = s.ServerName,
                    Host = $"{s.Host}:{s.Port}",
                    Database = s.DatabaseName,
                    Latency = s.LatencyMs.HasValue ? $"{s.LatencyMs}ms" : "—",
                    Version = s.ServerVersion ?? "—",
                    Active = s.IsActive ? "✅" : "❌",
                    LastCheck = s.LastHealthCheck?.ToLocalTime().ToString("HH:mm:ss") ?? "Never"
                }).ToList();

                grdServers.DataSource = displayData;
                if (grdServers.Columns.Contains("Id"))
                    grdServers.Columns["Id"].Visible = false;

                int online = _servers.Count(s => s.LastHealthStatus == "Online");
                lblStatusBar.Text = $"📡 {online}/{_servers.Count} servers online | Total servers: {_servers.Count}";
                lblStatusBar.ForeColor = online == _servers.Count ? Color.FromArgb(166, 227, 161) : Color.FromArgb(249, 226, 175);
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = $"❌ Error: {ex.Message}";
                lblStatusBar.ForeColor = Color.FromArgb(243, 139, 168);
            }
        }

        private string StatusIcon(string? status) => status switch
        {
            "Online" => "🟢 Online",
            "Slow" => "🟡 Slow",
            "Offline" => "🔴 Offline",
            "Timeout" => "🔴 Timeout",
            _ => "⚪ Unknown"
        };

        private void LoadSelection()
        {
            if (grdServers.CurrentRow == null) return;
            var id = (Guid)grdServers.CurrentRow.Cells["Id"].Value;
            var server = _servers.FirstOrDefault(s => s.Id == id);
            if (server == null) return;

            _selectedId = id;
            txtServerName.Text = server.ServerName;
            txtHost.Text = server.Host;
            txtPort.Text = server.Port.ToString();
            txtDbName.Text = server.DatabaseName;
            txtUsername.Text = server.Username;
            txtPassword.Text = server.Password;
            cboSslMode.SelectedItem = server.SslMode;
            chkActive.Checked = server.IsActive;
            txtDescription.Text = server.Description ?? "";
        }

        private ServerEntry BuildEntryFromForm()
        {
            return new ServerEntry
            {
                Id = _selectedId ?? Guid.NewGuid(),
                ServerName = txtServerName.Text.Trim(),
                Host = txtHost.Text.Trim(),
                Port = int.TryParse(txtPort.Text, out var p) ? p : 5432,
                DatabaseName = txtDbName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                SslMode = cboSslMode.SelectedItem?.ToString() ?? "Prefer",
                Description = txtDescription.Text.Trim(),
                IsActive = chkActive.Checked
            };
        }

        private async Task TestConnectionAsync()
        {
            var entry = BuildEntryFromForm();
            if (string.IsNullOrWhiteSpace(entry.Host) || string.IsNullOrWhiteSpace(entry.DatabaseName))
            {
                MessageBox.Show("Please fill Host and Database fields.", "⚠ Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblStatusBar.Text = $"⚡ Testing connection to {entry.Host}...";
            btnTestConnection.Enabled = false;
            try
            {
                var result = await ServerRegistry.CheckHealthAsync(entry);
                if (result.IsOnline)
                {
                    MessageBox.Show($"✅ Connection successful!\n\nServer: {result.ServerVersion}\nLatency: {result.LatencyMs}ms",
                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatusBar.Text = $"✅ {entry.Host} — Online ({result.LatencyMs}ms, v{result.ServerVersion})";
                }
                else
                {
                    MessageBox.Show($"❌ Connection failed!\n\n{result.ErrorMessage}",
                        "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusBar.Text = $"❌ {entry.Host} — {result.Status}";
                }
            }
            finally { btnTestConnection.Enabled = true; }
        }

        private async Task AddServerAsync()
        {
            var entry = BuildEntryFromForm();
            if (string.IsNullOrWhiteSpace(entry.ServerName) || string.IsNullOrWhiteSpace(entry.Host))
            {
                MessageBox.Show("Server Name and Host are required.", "⚠", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                entry.Id = Guid.NewGuid();
                await ServerRegistry.AddAsync(_controlCs, entry);
                ClearForm();
                await RefreshListAsync();
                lblStatusBar.Text = $"✅ Server '{entry.ServerName}' added successfully!";
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "❌", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async Task UpdateServerAsync()
        {
            if (_selectedId == null) { MessageBox.Show("Select a server first."); return; }
            try
            {
                var entry = BuildEntryFromForm();
                await ServerRegistry.UpdateAsync(_controlCs, entry);
                await RefreshListAsync();
                lblStatusBar.Text = $"💾 Server '{entry.ServerName}' updated!";
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private async Task DeleteServerAsync()
        {
            if (_selectedId == null) { MessageBox.Show("Select a server first."); return; }
            var name = txtServerName.Text;
            if (MessageBox.Show($"Are you sure you want to delete server '{name}'?", "⚠ Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                await ServerRegistry.DeleteAsync(_controlCs, _selectedId.Value);
                ClearForm();
                await RefreshListAsync();
                lblStatusBar.Text = $"🗑 Server '{name}' deleted.";
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private async Task PingAllServersAsync()
        {
            btnPingAll.Enabled = false;
            lblStatusBar.Text = $"🔄 Pinging {_servers.Count} servers...";
            try
            {
                await ServerRegistry.PingAllAsync(_controlCs);
                await RefreshListAsync();
            }
            catch (Exception ex)
            {
                lblStatusBar.Text = $"❌ Ping error: {ex.Message}";
            }
            finally { btnPingAll.Enabled = true; }
        }

        private void ClearForm()
        {
            _selectedId = null;
            txtServerName.Clear();
            txtHost.Clear();
            txtPort.Text = "5432";
            txtDbName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboSslMode.SelectedIndex = 1;
            chkActive.Checked = true;
            txtDescription.Clear();
        }
    }
}
