#nullable enable
using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_BlackBox : Form
    {
        private int _page = 1;
        private int _pageSize = 50;
        private int _totalCount;

        private static readonly string[] Categories = { "الكل", "Auth", "Settings", "Permissions", "Products", "Sales", "Purchases", "Returns", "Treasury", "Employees", "Accounting", "Warehouse", "Customers", "Suppliers", "Reports", "Backup", "System" };
        private static readonly string[] EventTypes = { "الكل", "Login", "LoginFailed", "Logout", "PermissionDenied", "ScreenAccess", "Create", "Update", "Delete", "SettingsChanged", "PermissionsChanged", "Backup", "Restore", "Export" };
        private static readonly string[] Screens = { "الكل", "Frm_LoginEmployee", "Form1", "Frm_Setting", "Frm_Permission", "Frm_Products", "Frm_ProductActive", "Frm_Sales", "Frm_BlackBox" };

        private static readonly Dictionary<string, string> CategoryLabels = new()
        {
            ["Auth"] = "🔐 تسجيل الدخول",
            ["Settings"] = "⚙️ الإعدادات",
            ["Permissions"] = "🛡️ الصلاحيات",
            ["Products"] = "📦 المنتجات",
            ["Sales"] = "💰 المبيعات",
            ["Purchases"] = "🛒 المشتريات",
            ["Returns"] = "↩️ المرتجعات",
            ["Treasury"] = "🏦 الخزينة",
            ["Employees"] = "👥 الموظفين",
            ["Accounting"] = "📊 الحسابات",
            ["Warehouse"] = "🏢 المخازن",
            ["Customers"] = "👤 العملاء",
            ["Suppliers"] = "🚚 الموردين",
            ["Reports"] = "📈 التقارير",
            ["Backup"] = "💾 النسخ الاحتياطي",
            ["System"] = "🖥️ النظام",
        };

        private static readonly Dictionary<string, string> EventTypeLabels = new()
        {
            ["Login"] = "تسجيل دخول",
            ["LoginFailed"] = "فشل تسجيل دخول",
            ["Logout"] = "تسجيل خروج",
            ["PermissionDenied"] = "رفض صلاحية",
            ["ScreenAccess"] = "دخول شاشة",
            ["Create"] = "إضافة",
            ["Update"] = "تعديل",
            ["Delete"] = "حذف",
            ["SettingsChanged"] = "تغيير إعدادات",
            ["PermissionsChanged"] = "تغيير صلاحيات",
            ["Backup"] = "نسخ احتياطي",
            ["Restore"] = "استرجاع",
            ["Export"] = "تصدير",
        };

        public Frm_BlackBox()
        {
            InitializeComponent();
            Load += Frm_BlackBox_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnPrev.Click += BtnPrev_Click;
            btnNext.Click += BtnNext_Click;
            dgvEvents.SelectionChanged += DgvEvents_SelectionChanged;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private async void Frm_BlackBox_Load(object? sender, EventArgs e)
        {
            // Populate combo filters
            cmbCategory.Items.AddRange(Categories);
            cmbCategory.SelectedIndex = 0;
            cmbEventType.Items.AddRange(EventTypes);
            cmbEventType.SelectedIndex = 0;
            cmbScreen.Items.AddRange(Screens);
            cmbScreen.SelectedIndex = 0;

            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today.AddDays(1);

            SetupEventsGrid();
            SetupSessionsGrid();
            SetupChangesGrid();

            await LoadEventsAsync();

            AuditLogger.Log("ScreenAccess", "System", "Frm_BlackBox", "فتح شاشة الصندوق الأسود");
        }

        private void SetupEventsGrid()
        {
            dgvEvents.Columns.Clear();
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_time", HeaderText = "الوقت", Width = 140 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_emp", HeaderText = "الموظف", Width = 120 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_category", HeaderText = "التصنيف", Width = 100 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_event", HeaderText = "الأمر", Width = 110 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_screen", HeaderText = "الشاشة", Width = 120 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_desc", HeaderText = "الوصف", Width = 250 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_device", HeaderText = "الجهاز", Width = 100 });
            dgvEvents.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_id", HeaderText = "ID", Visible = false });
        }

        private void SetupSessionsGrid()
        {
            dgvSessions.Columns.Clear();
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_emp", HeaderText = "الموظف", Width = 150 });
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_code", HeaderText = "الكود", Width = 80 });
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_login", HeaderText = "وقت الدخول", Width = 150 });
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_logout", HeaderText = "وقت الخروج", Width = 150 });
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_device", HeaderText = "الجهاز", Width = 120 });
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_status", HeaderText = "الحالة", Width = 80 });
        }

        private void SetupChangesGrid()
        {
            dgvChanges.Columns.Clear();
            dgvChanges.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_field", HeaderText = "الحقل", Width = 150 });
            dgvChanges.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_label", HeaderText = "الاسم", Width = 150 });
            dgvChanges.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_old", HeaderText = "القيمة القديمة", Width = 200 });
            dgvChanges.Columns.Add(new DataGridViewTextBoxColumn { Name = "col_new", HeaderText = "القيمة الجديدة", Width = 200 });
        }

        private async Task LoadEventsAsync()
        {
            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var cat = cmbCategory.SelectedIndex > 0 ? Categories[cmbCategory.SelectedIndex] : null;
                var evt = cmbEventType.SelectedIndex > 0 ? EventTypes[cmbEventType.SelectedIndex] : null;
                var scr = cmbScreen.SelectedIndex > 0 ? Screens[cmbScreen.SelectedIndex] : null;
                var search = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text.Trim();

                var url = $"api/audit-logs?page={_page}&size={_pageSize}" +
                    $"&from={dtFrom.Value:yyyy-MM-dd}&to={dtTo.Value:yyyy-MM-dd}" +
                    (cat != null ? $"&category={cat}" : "") +
                    (evt != null ? $"&eventType={evt}" : "") +
                    (scr != null ? $"&screenName={scr}" : "") +
                    (search != null ? $"&search={Uri.EscapeDataString(search)}" : "");

                var resp = await http.GetFromJsonAsync<PagedResult<AuditEventDto>>(url, JsonOpts);
                if (resp == null) return;

                _totalCount = resp.TotalCount;
                lblCount.Text = $"النتائج: {_totalCount}";
                lblPage.Text = $"صفحة {_page}";
                btnPrev.Enabled = _page > 1;
                btnNext.Enabled = _page * _pageSize < _totalCount;

                dgvEvents.Rows.Clear();
                foreach (var ev in resp.Items)
                {
                    var row = dgvEvents.Rows.Add();
                    dgvEvents.Rows[row].Cells["col_time"].Value = ev.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    dgvEvents.Rows[row].Cells["col_emp"].Value = ev.EmployeeName ?? "-";
                    dgvEvents.Rows[row].Cells["col_category"].Value = CategoryLabels.GetValueOrDefault(ev.Category, ev.Category);
                    dgvEvents.Rows[row].Cells["col_event"].Value = EventTypeLabels.GetValueOrDefault(ev.EventType, ev.EventType);
                    dgvEvents.Rows[row].Cells["col_screen"].Value = ev.ScreenName ?? "-";
                    dgvEvents.Rows[row].Cells["col_desc"].Value = ev.Description;
                    dgvEvents.Rows[row].Cells["col_device"].Value = ev.DeviceName ?? "-";
                    dgvEvents.Rows[row].Cells["col_id"].Value = ev.Id.ToString();

                    // Color by severity
                    var color = ev.Severity switch
                    {
                        1 => Color.FromArgb(255, 248, 225),  // Warning - yellow
                        2 => Color.FromArgb(255, 230, 230),  // Error - red
                        3 => Color.FromArgb(200, 50, 50),    // Critical - dark red
                        _ => Color.White
                    };
                    dgvEvents.Rows[row].DefaultCellStyle.BackColor = color;
                    if (ev.Severity == 3) dgvEvents.Rows[row].DefaultCellStyle.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                lblCount.Text = $"خطأ: {ex.Message}";
            }
        }

        private async Task LoadSessionsAsync()
        {
            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var url = $"api/audit-logs/sessions?page=1&size=100" +
                    $"&from={dtFrom.Value:yyyy-MM-dd}&to={dtTo.Value:yyyy-MM-dd}";

                var resp = await http.GetFromJsonAsync<PagedResult<AuditSessionDto>>(url, JsonOpts);
                if (resp == null) return;

                dgvSessions.Rows.Clear();
                foreach (var s in resp.Items)
                {
                    var row = dgvSessions.Rows.Add();
                    dgvSessions.Rows[row].Cells["col_emp"].Value = s.EmployeeName;
                    dgvSessions.Rows[row].Cells["col_code"].Value = s.EmployeeCode;
                    dgvSessions.Rows[row].Cells["col_login"].Value = s.LoginAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    dgvSessions.Rows[row].Cells["col_logout"].Value = s.LogoutAt?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") ?? "— نشط —";
                    dgvSessions.Rows[row].Cells["col_device"].Value = s.DeviceName ?? "-";
                    dgvSessions.Rows[row].Cells["col_status"].Value = s.Status switch
                    {
                        "Active" => "🟢 نشط",
                        "Closed" => "⚫ مغلق",
                        "Expired" => "🔴 منتهي",
                        _ => s.Status
                    };

                    if (s.Status == "Active")
                        dgvSessions.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                }
            }
            catch { }
        }

        private async void DgvEvents_SelectionChanged(object? sender, EventArgs e)
        {
            dgvChanges.Rows.Clear();
            if (dgvEvents.SelectedRows.Count == 0) return;

            var idStr = dgvEvents.SelectedRows[0].Cells["col_id"].Value?.ToString();
            if (string.IsNullOrEmpty(idStr) || !Guid.TryParse(idStr, out var id)) return;

            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var changes = await http.GetFromJsonAsync<FieldChangeDto[]>($"api/audit-logs/{id}/changes", JsonOpts);
                if (changes == null || changes.Length == 0)
                {
                    lblDetailTitle.Text = "📝 لا توجد تغييرات مفصلة";
                    return;
                }

                lblDetailTitle.Text = $"📝 تفاصيل التغييرات ({changes.Length} حقل):";
                foreach (var c in changes)
                {
                    var row = dgvChanges.Rows.Add();
                    dgvChanges.Rows[row].Cells["col_field"].Value = c.FieldName;
                    dgvChanges.Rows[row].Cells["col_label"].Value = c.FieldLabel ?? "";
                    dgvChanges.Rows[row].Cells["col_old"].Value = c.OldValue ?? "-";
                    dgvChanges.Rows[row].Cells["col_new"].Value = c.NewValue ?? "-";
                }
            }
            catch { lblDetailTitle.Text = "📝 تفاصيل التغييرات:"; }
        }

        private async void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabSessions)
                await LoadSessionsAsync();
        }

        private async void BtnSearch_Click(object? sender, EventArgs e)
        {
            _page = 1;
            await LoadEventsAsync();
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            _page = 1;
            await LoadEventsAsync();
            if (tabControl.SelectedTab == tabSessions)
                await LoadSessionsAsync();
        }

        private async void BtnPrev_Click(object? sender, EventArgs e)
        {
            if (_page > 1) { _page--; await LoadEventsAsync(); }
        }

        private async void BtnNext_Click(object? sender, EventArgs e)
        {
            if (_page * _pageSize < _totalCount) { _page++; await LoadEventsAsync(); }
        }

        private HttpClient? CreateHttpClient()
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch { return null; }
            }

            var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            var http = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(30) };
            var token = Properties.Settings.Default.EmployeeToken;
            if (string.IsNullOrWhiteSpace(token))
                token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return http;
        }

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        // ── DTOs ──
        private sealed record PagedResult<T>
        {
            public T[] Items { get; init; } = Array.Empty<T>();
            public int TotalCount { get; init; }
            public int Page { get; init; }
            public int PageSize { get; init; }
        }
        private sealed record AuditEventDto(
            Guid Id, Guid OrgId, Guid? BranchId,
            Guid? EmployeeId, string? EmployeeCode, string? EmployeeName,
            string EventType, string Category, string? ScreenName,
            string? TargetTable, string? TargetId, string Description,
            int Severity, string? IpAddress, string? DeviceName, DateTime CreatedAt);
        private sealed record AuditSessionDto(
            Guid Id, Guid OrgId, Guid? BranchId,
            Guid EmployeeId, string EmployeeCode, string EmployeeName,
            DateTime LoginAt, DateTime? LogoutAt,
            string? DeviceName, string? IpAddress, string Status);
        private sealed record FieldChangeDto(string FieldName, string? FieldLabel, string? OldValue, string? NewValue);
    }
}
