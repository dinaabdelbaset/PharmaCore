using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Permission : Form
    {
        // PermissionKeys mirror — grouped categories with Arabic labels
        private static readonly Dictionary<string, (string key, string label)[]> _groups = new()
        {
            ["الإعدادات"] = new[] {
                ("settings.view", "عرض الإعدادات"),
                ("settings.edit", "تعديل الإعدادات"),
                ("permissions.manage", "إدارة الصلاحيات"),
            },
            ["المنتجات"] = new[] {
                ("products.add", "إضافة منتج"),
                ("products.view", "عرض المنتجات"),
                ("products.activate", "تفعيل المنتجات"),
                ("products.barcode", "باركود"),
                ("products.required", "المطلوب توفيره"),
                ("products.expire", "الصلاحية"),
                ("products.unit", "الوحدات"),
                ("products.company", "الشركات"),
                ("products.financials", "البيان المالي"),
            },
            ["العملاء"] = new[] {
                ("customers.view", "عرض العملاء"),
                ("customers.money", "مالية العملاء"),
                ("customers.report", "تقارير العملاء"),
                ("customers.points", "النقاط"),
                ("customers.points_report", "تقارير النقاط"),
            },
            ["الموردين"] = new[] {
                ("suppliers.view", "عرض الموردين"),
                ("suppliers.money", "مالية الموردين"),
                ("suppliers.report", "تقارير الموردين"),
            },
            ["المشتريات"] = new[] {
                ("purchases.create", "إنشاء فاتورة شراء"),
                ("purchases.na89", "الأصناف الناقصة"),
                ("purchases.report", "تقارير المشتريات"),
            },
            ["المبيعات"] = new[] {
                ("sales.create", "إنشاء فاتورة بيع"),
                ("sales.insurance", "بيع تأمين"),
                ("sales.insurance_company", "شركات التأمين"),
                ("sales.user_report", "تقارير مبيعات المستخدمين"),
                ("sales.report", "تقارير المبيعات"),
                ("sales.insurance_report", "تقارير التأمين"),
                ("sales.profit", "الأرباح"),
            },
            ["المرتجعات"] = new[] {
                ("returns.sale", "مرتجع بيع"),
                ("returns.buy", "مرتجع شراء"),
                ("returns.report", "تقارير المرتجعات"),
            },
            ["الخزينة والبنك"] = new[] {
                ("treasury.add_stock", "إضافة خزينة"),
                ("treasury.stock_deposit", "إيداع خزينة"),
                ("treasury.bank_deposit", "إيداع بنك"),
                ("treasury.stock_withdraw", "سحب خزينة"),
                ("treasury.bank_withdraw", "سحب بنك"),
                ("treasury.transfer", "تحويل أموال"),
                ("treasury.bank_stock_transfer", "تحويل بنك/خزينة"),
                ("treasury.balance", "الأرصدة الحالية"),
                ("treasury.report", "تقارير الخزينة"),
            },
            ["الموظفين"] = new[] {
                ("employees.add", "إضافة موظف"),
                ("employees.borrow_items", "استعارة أصناف"),
                ("employees.borrow_money", "سلف مالية"),
                ("employees.salary", "المرتبات"),
                ("employees.borrow_items_report", "تقارير استعارة أصناف"),
                ("employees.borrow_money_report", "تقارير السلف"),
                ("employees.salary_report", "تقارير المرتبات"),
            },
            ["الحسابات"] = new[] {
                ("accounting.type", "أنواع المديونيات"),
                ("accounting.entry", "المديونيات"),
                ("accounting.report", "تقارير المديونيات"),
                ("accounting.receipt", "سند قبض"),
                ("accounting.payment", "سند صرف"),
                ("accounting.voucher_report", "تقارير السندات"),
                ("accounting.tax_report", "تقارير الضرائب"),
            },
            ["التقارير"] = new[] {
                ("reports.customers", "تقرير العملاء"),
                ("reports.suppliers", "تقرير الموردين"),
                ("reports.purchases", "تقرير المشتريات"),
                ("reports.sales", "تقرير المبيعات"),
                ("reports.returns", "تقرير المرتجعات"),
                ("reports.accounting", "تقرير المديونيات"),
                ("reports.shifts", "تقرير الشيفتات"),
            },
            ["النسخ الاحتياطي"] = new[] {
                ("backup.create", "أخذ نسخة احتياطية"),
                ("backup.restore", "استرجاع نسخة احتياطية"),
            },
            ["المخازن"] = new[] {
                ("warehouse.add", "إضافة مخزن"),
                ("warehouse.inventory", "جرد المخزن"),
                ("warehouse.transfer", "تحويل بين مخازن"),
                ("warehouse.out", "إخراج من مخزن"),
                ("warehouse.transfer_report", "تقارير تحويل مخازن"),
                ("warehouse.out_report", "تقارير إخراج مخازن"),
            },
            ["الشيفتات"] = new[] {
                ("shifts.open", "فتح شيفت"),
                ("shifts.close", "إغلاق شيفت"),
            },
        };

        private Guid _currentEmployeeId;
        private bool _suppressTreeCheck;

        public Frm_Permission()
        {
            InitializeComponent();
            Load += Frm_Permission_Load;
            txtEmployeeCode.KeyDown += TxtEmployeeCode_KeyDown;
            treePermissions.AfterCheck += TreePermissions_AfterCheck;
            btnCheckAll.Click += BtnCheckAll_Click;
            btnUncheckAll.Click += BtnUncheckAll_Click;
            btnSave.Click += BtnSave_Click;
            btnManageRoles.Click += BtnManageRoles_Click;
            cmbRole.SelectedIndexChanged += CmbRole_SelectedIndexChanged;
        }

        private async void Frm_Permission_Load(object sender, EventArgs e)
        {
            BuildTree();
            await LoadRolesAsync();
            txtEmployeeCode.Focus();
        }

        private async Task LoadRolesAsync()
        {
            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var roles = await http.GetFromJsonAsync<RoleItem[]>("api/employee-roles");
                var list = new List<RoleItem> { new RoleItem(Guid.Empty, "بدون رتبة (صلاحيات مخصصة فقط)") };
                if (roles != null) list.AddRange(roles);

                cmbRole.DataSource = list;
                cmbRole.DisplayMember = "Name";
                cmbRole.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = $"خطأ في تحميل الرتب: {ex.Message}";
            }
        }

        private async void BtnManageRoles_Click(object sender, EventArgs e)
        {
            using var frm = new Frm_Roles();
            frm.ShowDialog(this);
            await LoadRolesAsync();
        }

        /// <summary>When user selects a role from the dropdown, auto-load its permissions into the tree.</summary>
        private async void CmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRole.SelectedValue is not Guid roleId || roleId == Guid.Empty) return;

            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var perms = await http.GetFromJsonAsync<string[]>($"api/employee-roles/{roleId}/permissions");
                var set = new HashSet<string>(perms ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

                _suppressTreeCheck = true;
                foreach (TreeNode parent in treePermissions.Nodes)
                {
                    int checkedCount = 0;
                    foreach (TreeNode child in parent.Nodes)
                    {
                        child.Checked = set.Contains((string)child.Tag!);
                        if (child.Checked) checkedCount++;
                    }
                    parent.Checked = checkedCount == parent.Nodes.Count && parent.Nodes.Count > 0;
                }
                _suppressTreeCheck = false;

                lblStatus.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
                lblStatus.Text = $"تم تحميل {set.Count} صلاحية من الرتبة";
            }
            catch (Exception ex)
            {
                _suppressTreeCheck = false;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = $"خطأ: {ex.Message}";
            }
        }

        /// <summary>Build the TreeView once from the static permission groups.</summary>
        private void BuildTree()
        {
            treePermissions.BeginUpdate();
            treePermissions.Nodes.Clear();

            foreach (var grp in _groups)
            {
                var parent = new TreeNode(grp.Key) { Tag = (string?)null };
                foreach (var (key, label) in grp.Value)
                {
                    var child = new TreeNode($"{label}  ({key})") { Tag = key };
                    parent.Nodes.Add(child);
                }
                treePermissions.Nodes.Add(parent);
            }

            treePermissions.ExpandAll();
            treePermissions.EndUpdate();
        }

        /// <summary>When user presses Enter in the code textbox, search for the employee.</summary>
        private async void TxtEmployeeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true; // prevent ding sound

            var code = txtEmployeeCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                lblEmployeeName.Text = "";
                lblEmployeeName.ForeColor = System.Drawing.Color.Red;
                lblEmployeeName.Text = "أدخل كود الموظف ثم اضغط Enter";
                return;
            }

            try
            {
                lblEmployeeName.ForeColor = System.Drawing.Color.Gray;
                lblEmployeeName.Text = "جاري البحث...";

                using var http = CreateHttpClient();
                if (http == null) return;

                // Search for employee by code
                var response = await http.GetAsync($"api/employees/by-code/{code}");

                if (!response.IsSuccessStatusCode)
                {
                    lblEmployeeName.ForeColor = System.Drawing.Color.Red;
                    lblEmployeeName.Text = $"❌ لا يوجد موظف بالكود: {code}";
                    _currentEmployeeId = Guid.Empty;
                    UncheckAllNodes();
                    return;
                }

                var emp = await response.Content.ReadFromJsonAsync<EmployeeListItem>();
                if (emp == null)
                {
                    lblEmployeeName.ForeColor = System.Drawing.Color.Red;
                    lblEmployeeName.Text = $"❌ لا يوجد موظف بالكود: {code}";
                    _currentEmployeeId = Guid.Empty;
                    UncheckAllNodes();
                    return;
                }

                _currentEmployeeId = emp.Id;
                lblEmployeeName.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
                lblEmployeeName.Text = $"👤 {emp.FullName}  (كود: {emp.EmployeeCode})";

                if (emp.RoleId.HasValue)
                    cmbRole.SelectedValue = emp.RoleId.Value;
                else
                    cmbRole.SelectedValue = Guid.Empty;

                // Load permissions for this employee
                await LoadPermissionsAsync(emp.Id);
            }
            catch (Exception ex)
            {
                lblEmployeeName.ForeColor = System.Drawing.Color.Red;
                lblEmployeeName.Text = $"خطأ: {ex.Message}";
            }
        }

        /// <summary>Load permissions for a specific employee from the API and check the tree nodes.</summary>
        private async Task LoadPermissionsAsync(Guid employeeId)
        {
            try
            {
                using var http = CreateHttpClient();
                if (http == null) return;

                var perms = await http.GetFromJsonAsync<string[]>($"api/employee-permissions/{employeeId}");
                var set = new HashSet<string>(perms ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

                _suppressTreeCheck = true;
                foreach (TreeNode parent in treePermissions.Nodes)
                {
                    int checkedCount = 0;
                    foreach (TreeNode child in parent.Nodes)
                    {
                        child.Checked = set.Contains((string)child.Tag!);
                        if (child.Checked) checkedCount++;
                    }
                    parent.Checked = checkedCount == parent.Nodes.Count && parent.Nodes.Count > 0;
                }
                _suppressTreeCheck = false;

                lblStatus.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
                lblStatus.Text = $"تم تحميل {set.Count} صلاحية";
            }
            catch (Exception ex)
            {
                _suppressTreeCheck = false;
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = $"خطأ: {ex.Message}";
            }
        }

        /// <summary>When a parent node is checked/unchecked, check/uncheck all children.</summary>
        private void TreePermissions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_suppressTreeCheck || e.Action == TreeViewAction.Unknown) return;
            var node = e.Node;

            _suppressTreeCheck = true;
            // If parent → toggle all children
            if (node.Tag == null && node.Nodes.Count > 0)
            {
                foreach (TreeNode child in node.Nodes)
                    child.Checked = node.Checked;
            }
            // If child → update parent state
            else if (node.Parent != null)
            {
                int checkedCount = 0;
                foreach (TreeNode sibling in node.Parent.Nodes)
                    if (sibling.Checked) checkedCount++;
                node.Parent.Checked = checkedCount == node.Parent.Nodes.Count;
            }
            _suppressTreeCheck = false;
        }

        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            _suppressTreeCheck = true;
            foreach (TreeNode parent in treePermissions.Nodes)
            {
                parent.Checked = true;
                foreach (TreeNode child in parent.Nodes)
                    child.Checked = true;
            }
            _suppressTreeCheck = false;
        }

        private void BtnUncheckAll_Click(object sender, EventArgs e)
        {
            UncheckAllNodes();
        }

        private void UncheckAllNodes()
        {
            _suppressTreeCheck = true;
            foreach (TreeNode parent in treePermissions.Nodes)
            {
                parent.Checked = false;
                foreach (TreeNode child in parent.Nodes)
                    child.Checked = false;
            }
            _suppressTreeCheck = false;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (_currentEmployeeId == Guid.Empty)
            {
                MessageBox.Show("أدخل كود الموظف أولاً واضغط Enter", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeCode.Focus();
                return;
            }

            var checkedKeys = new List<string>();

            foreach (TreeNode parent in treePermissions.Nodes)
            {
                foreach (TreeNode child in parent.Nodes)
                {
                    if (child.Checked && child.Tag is string key)
                        checkedKeys.Add(key);
                }
            }

            try
            {
                btnSave.Enabled = false;
                lblStatus.ForeColor = System.Drawing.Color.Gray;
                lblStatus.Text = "جاري الحفظ...";

                using var http = CreateHttpClient();
                if (http == null) return;

                var req = new
                {
                    RoleId = cmbRole.SelectedValue is Guid rid && rid != Guid.Empty ? (Guid?)rid : null,
                    Permissions = checkedKeys.ToArray()
                };
                var resp = await http.PutAsJsonAsync($"api/employee-permissions/{_currentEmployeeId}", req);

                if (resp.IsSuccessStatusCode)
                {
                    lblStatus.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
                    lblStatus.Text = $"✓ تم حفظ {checkedKeys.Count} صلاحية بنجاح";
                    MessageBox.Show($"تم حفظ {checkedKeys.Count} صلاحية بنجاح", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AuditLogger.Log("PermissionsChanged", "Permissions", "Frm_Permission",
                        $"تعديل صلاحيات الموظف {lblEmployeeName.Text} — {checkedKeys.Count} صلاحية",
                        targetTable: "EmployeePermissions", targetId: _currentEmployeeId.ToString());
                }
                else
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = $"فشل الحفظ: {resp.StatusCode}";
                    MessageBox.Show($"فشل الحفظ: {body}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = $"خطأ: {ex.Message}";
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private HttpClient? CreateHttpClient()
        {
            string baseUrl;
            try { baseUrl = ApiBaseResolver.GetRequiredSaasBaseUrlForCurrentOrgTenant(); }
            catch
            {
                try { baseUrl = ApiBaseResolver.GetRequiredBaseUrl(); }
                catch (Exception ex)
                {
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = $"API Error: {ex.Message}";
                    return null;
                }
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

        private void btnManageRoles_Click_1(object sender, EventArgs e)
        {

        }

        private sealed record EmployeeListItem
        {
            public Guid Id { get; init; }
            public string? EmployeeCode { get; init; }
            public string? FullName { get; init; }
            public Guid? RoleId { get; init; }
        }

        private sealed record RoleItem(Guid Id, string Name);
    }
}