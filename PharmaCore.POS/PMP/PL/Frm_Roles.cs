using PharmaCore.POS.Services;
using PharmaCore.POS.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Roles : Form
    {
        // ─── API DTOs ──────────────────────────────────────────────────
        public record RoleResponse(Guid Id, string Name, Guid? ParentRoleId, bool IsSystemRole);
        public record CreateRoleRequest(string Name, Guid? ParentRoleId, List<string> Permissions);
        public record UpdateRoleRequest(string Name, Guid? ParentRoleId, List<string> Permissions);

        // ─── Constants for Permissions Tree ─────────────────────────────
        private static readonly Dictionary<string, (string key, string label)[]> _groups = new()
        {
            ["الإعدادات"] = new[] { ("settings.view", "عرض الإعدادات"), ("settings.edit", "تعديل الإعدادات"), ("permissions.manage", "إدارة الصلاحيات"), },
            ["المنتجات"] = new[] { ("products.add", "إضافة منتج"), ("products.view", "عرض المنتجات"), ("products.activate", "تفعيل المنتجات"), ("products.barcode", "باركود"), ("products.required", "المطلوب توفيره"), ("products.expire", "الصلاحية"), ("products.unit", "الوحدات"), ("products.company", "الشركات"), ("products.financials", "البيان المالي"), },
            ["العملاء"] = new[] { ("customers.view", "عرض العملاء"), ("customers.money", "مالية العملاء"), ("customers.report", "تقارير العملاء"), ("customers.points", "النقاط"), ("customers.points_report", "تقارير النقاط"), },
            ["الموردين"] = new[] { ("suppliers.view", "عرض الموردين"), ("suppliers.money", "مالية الموردين"), ("suppliers.report", "تقارير الموردين"), },
            ["المشتريات"] = new[] { ("purchases.create", "إنشاء فاتورة شراء"), ("purchases.na89", "الأصناف الناقصة"), ("purchases.report", "تقارير المشتريات"), },
            ["المبيعات"] = new[] { ("sales.create", "إنشاء فاتورة بيع"), ("sales.insurance", "بيع تأمين"), ("sales.insurance_company", "شركات التأمين"), ("sales.user_report", "تقارير مبيعات المستخدمين"), ("sales.report", "تقارير المبيعات"), ("sales.insurance_report", "تقارير التأمين"), ("sales.profit", "الأرباح"), },
            ["المرتجعات"] = new[] { ("returns.sale", "مرتجع بيع"), ("returns.buy", "مرتجع شراء"), ("returns.report", "تقارير المرتجعات"), },
            ["الخزينة والبنك"] = new[] { ("treasury.add_stock", "إضافة خزينة"), ("treasury.stock_deposit", "إيداع خزينة"), ("treasury.bank_deposit", "إيداع بنك"), ("treasury.stock_withdraw", "سحب خزينة"), ("treasury.bank_withdraw", "سحب بنك"), ("treasury.transfer", "تحويل أموال"), ("treasury.bank_stock_transfer", "تحويل بنك/خزينة"), ("treasury.balance", "الأرصدة الحالية"), ("treasury.report", "تقارير الخزينة"), },
            ["الموظفين"] = new[] { ("employees.add", "إضافة موظف"), ("employees.borrow_items", "استعارة أصناف"), ("employees.borrow_money", "سلف مالية"), ("employees.salary", "المرتبات"), ("employees.borrow_items_report", "تقارير استعارة أصناف"), ("employees.borrow_money_report", "تقارير السلف"), ("employees.salary_report", "تقارير المرتبات"), },
            ["الحسابات"] = new[] { ("accounting.type", "أنواع المديونيات"), ("accounting.entry", "المديونيات"), ("accounting.report", "تقارير المديونيات"), ("accounting.receipt", "سند قبض"), ("accounting.payment", "سند صرف"), ("accounting.voucher_report", "تقارير السندات"), ("accounting.tax_report", "تقارير الضرائب"), },
            ["التقارير"] = new[] { ("reports.customers", "تقرير العملاء"), ("reports.suppliers", "تقرير الموردين"), ("reports.purchases", "تقرير المشتريات"), ("reports.sales", "تقرير المبيعات"), ("reports.returns", "تقرير المرتجعات"), ("reports.accounting", "تقرير المديونيات"), ("reports.shifts", "تقرير الشيفتات"), },
            ["النسخ الاحتياطي"] = new[] { ("backup.create", "أخذ نسخة احتياطية"), ("backup.restore", "استرجاع نسخة احتياطية"), },
            ["المخازن"] = new[] { ("warehouse.add", "إضافة مخزن"), ("warehouse.inventory", "جرد المخزن"), ("warehouse.transfer", "تحويل بين مخازن"), ("warehouse.out", "إخراج من مخزن"), ("warehouse.transfer_report", "تقارير تحويل مخازن"), ("warehouse.out_report", "تقارير إخراج مخازن"), },
            ["الشيفتات"] = new[] { ("shifts.open", "فتح شيفت"), ("shifts.close", "إغلاق شيفت"), },
        };

        // ─── State ───────────────────────────────────────────────────────
        private List<RoleResponse> _allRoles = new();
        private Guid? _selectedRoleId = null;
        private bool _isNewMode = false;
        private bool _suppressTreeCheck = false;

        public Frm_Roles()
        {
            InitializeComponent();
            Load += Frm_Roles_Load;
            treeRoles.AfterSelect += TreeRoles_AfterSelect;
            treePermissions.AfterCheck += TreePermissions_AfterCheck;
            btnAddNewRole.Click += BtnAddNewRole_Click;
            btnSave.Click += BtnSave_Click;
            btnDeleteRole.Click += BtnDeleteRole_Click;
        }

        private async void Frm_Roles_Load(object sender, EventArgs e)
        {
            BuildPermissionsTree();
            await LoadRolesFromServerAsync();
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
                    ShowError($"API Error: {ex.Message}");
                    return null;
                }
            }

            var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            var http = new HttpClient(handler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(30) };
            var token = Properties.Settings.Default.EmployeeToken;
            if (string.IsNullOrWhiteSpace(token)) token = Properties.Settings.Default.ApiToken;
            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return http;
        }

        private void SetStatus(string msg, bool isError = false)
        {
            lblStatus.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.FromArgb(40, 167, 69);
            lblStatus.Text = msg;
        }

        private void ShowError(string errorMsg)
        {
            SetStatus(errorMsg, true);
            MessageBox.Show(errorMsg, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ─── 1. Load Roles & Build Tree ─────────────────────────
        private async Task LoadRolesFromServerAsync()
        {
            try
            {
                treeRoles.Enabled = false;
                SetStatus("جاري تحميل الرتب...");

                using var http = CreateHttpClient();
                if (http == null) return;

                var roles = await http.GetFromJsonAsync<List<RoleResponse>>("api/employee-roles");
                if (roles != null)
                {
                    _allRoles = roles;
                    BuildRolesTree();
                    PopulateParentComboBox();
                    SetStatus($"تم تحميل {roles.Count} רتبة.");
                }

                if (_allRoles.Count == 0 || _isNewMode)
                    PrepareNewMode();
                else if (treeRoles.Nodes.Count > 0)
                    treeRoles.SelectedNode = treeRoles.Nodes[0];
            }
            catch (Exception ex)
            {
                ShowError($"حدث خطأ أثناء تحميل الرتب: {ex.Message}");
            }
            finally
            {
                treeRoles.Enabled = true;
            }
        }

        private void BuildRolesTree()
        {
            treeRoles.BeginUpdate();
            treeRoles.Nodes.Clear();

            // Find all root nodes (ParentRoleId is null)
            var roots = _allRoles.Where(r => r.ParentRoleId == null).ToList();
            foreach (var root in roots)
            {
                var node = CreateTreeNode(root);
                treeRoles.Nodes.Add(node);
                AddChildNodes(node, root.Id);
            }

            treeRoles.ExpandAll();
            treeRoles.EndUpdate();
        }

        private TreeNode CreateTreeNode(RoleResponse role)
        {
            string icon = role.IsSystemRole ? "👑 " : "👤 ";
            var node = new TreeNode(icon + role.Name)
            {
                Tag = role
            };
            return node;
        }

        private void AddChildNodes(TreeNode parentNode, Guid parentId)
        {
            var children = _allRoles.Where(r => r.ParentRoleId == parentId).ToList();
            foreach (var child in children)
            {
                var childNode = CreateTreeNode(child);
                parentNode.Nodes.Add(childNode);
                AddChildNodes(childNode, child.Id); // Recursive
            }
        }

        private void PopulateParentComboBox()
        {
            cmbParentRole.Items.Clear();
            cmbParentRole.Items.Add(new ComboBoxItem("بدون إدارة (مستوى رئيسي)", Guid.Empty));
            foreach (var role in _allRoles)
            {
                cmbParentRole.Items.Add(new ComboBoxItem(role.Name, role.Id));
            }
            cmbParentRole.SelectedIndex = 0;
        }

        // ─── 2. Permissions Tree Setup ─────────────────────────
        private void BuildPermissionsTree()
        {
            treePermissions.BeginUpdate();
            treePermissions.Nodes.Clear();

            foreach (var grp in _groups)
            {
                var parent = new TreeNode(grp.Key) { Tag = null };
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

        private void UncheckAllPermissions()
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

        private void TreePermissions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_suppressTreeCheck || e.Action == TreeViewAction.Unknown) return;
            var node = e.Node;

            _suppressTreeCheck = true;
            if (node.Tag == null && node.Nodes.Count > 0)
            {
                foreach (TreeNode child in node.Nodes)
                    child.Checked = node.Checked;
            }
            else if (node.Parent != null)
            {
                int checkedCount = 0;
                foreach (TreeNode sibling in node.Parent.Nodes)
                    if (sibling.Checked) checkedCount++;
                node.Parent.Checked = checkedCount == node.Parent.Nodes.Count;
            }
            _suppressTreeCheck = false;
        }

        private List<string> GetCheckedPermissions()
        {
            var p = new List<string>();
            foreach (TreeNode parent in treePermissions.Nodes)
            {
                foreach (TreeNode child in parent.Nodes)
                {
                    if (child.Checked && child.Tag is string key)
                        p.Add(key);
                }
            }
            return p;
        }

        // ─── 3. UI Interactions ─────────────────────────
        private async void TreeRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is RoleResponse role)
            {
                _isNewMode = false;
                _selectedRoleId = role.Id;

                txtRoleName.Text = role.Name;

                if (role.ParentRoleId.HasValue)
                {
                    for (int i = 0; i < cmbParentRole.Items.Count; i++)
                    {
                        if (((ComboBoxItem)cmbParentRole.Items[i]).Id == role.ParentRoleId.Value)
                        {
                            cmbParentRole.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cmbParentRole.SelectedIndex = 0;
                }

                // Protect owner role
                bool canEdit = !role.IsSystemRole;
                txtRoleName.Enabled = canEdit;
                cmbParentRole.Enabled = canEdit;
                treePermissions.Enabled = canEdit;
                btnDeleteRole.Enabled = canEdit;

                await LoadRolePermissionsAsync(role.Id);
            }
        }

        private async Task LoadRolePermissionsAsync(Guid roleId)
        {
            UncheckAllPermissions();
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
            }
            catch (Exception ex)
            {
                ShowError($"خطأ في تحميل صلاحيات الرتبة: {ex.Message}");
            }
        }

        private void BtnAddNewRole_Click(object sender, EventArgs e)
        {
            PrepareNewMode();
        }

        private void PrepareNewMode()
        {
            _isNewMode = true;
            _selectedRoleId = null;
            treeRoles.SelectedNode = null;

            txtRoleName.Text = "";
            txtRoleName.Enabled = true;
            cmbParentRole.SelectedIndex = 0;
            cmbParentRole.Enabled = true;

            treePermissions.Enabled = true;
            btnDeleteRole.Enabled = false;
            UncheckAllPermissions();

            txtRoleName.Focus();
            SetStatus("وضع إنشاء رتبة جديدة...");
        }

        // ─── 4. Save & Delete ─────────────────────────
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            var name = txtRoleName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("يرجى إدخال اسم الرتبة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selParent = (ComboBoxItem)cmbParentRole.SelectedItem;
            Guid? parentId = selParent.Id == Guid.Empty ? null : selParent.Id;

            // Don't allow selecting itself as parent
            if (!_isNewMode && _selectedRoleId == parentId)
            {
                MessageBox.Show("لا يمكن تعيين الرتبة كمديرة لنفسها.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var perms = GetCheckedPermissions();

            try
            {
                btnSave.Enabled = false;
                SetStatus("جاري الحفظ...");

                using var http = CreateHttpClient();
                if (http == null) return;

                HttpResponseMessage resp;
                if (_isNewMode)
                {
                    var req = new CreateRoleRequest(name, parentId, perms);
                    resp = await http.PostAsJsonAsync("api/employee-roles", req);
                }
                else
                {
                    var req = new UpdateRoleRequest(name, parentId, perms);
                    resp = await http.PutAsJsonAsync($"api/employee-roles/{_selectedRoleId}", req);
                }

                if (resp.IsSuccessStatusCode)
                {
                    AuditLogger.Log(_isNewMode ? "RoleCreated" : "RoleUpdated", "Hierarchy", "Frm_Roles",
                        $"تم حفظ رتبة: {name} مع {perms.Count} صلاحية",
                        targetTable: "EmployeeRoles", targetId: _selectedRoleId?.ToString() ?? "New");

                    SetStatus($"تم حفظ الرتبة '{name}' بنجاح.");
                    await LoadRolesFromServerAsync(); // Reload tree
                }
                else
                {
                    if (resp.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        ShowError("عفواً، حسابك لا يملك صلاحية إدارة الرتب الهرمية (يجب أن تكون حساب مدير/Admin).");
                    }
                    else
                    {
                        var err = await resp.Content.ReadAsStringAsync();
                        ShowError($"فشل الحفظ: {err}");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"حدث خطأ: {ex.Message}");
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private async void BtnDeleteRole_Click(object sender, EventArgs e)
        {
            if (_selectedRoleId == null || _isNewMode) return;

            if (MessageBox.Show($"هل أنت متأكد من حذف هذه الرتبة نهائياً من النظام؟\nسيتم منع الحذف إذا كان هناك موظفين مسجلين عليها أو رتب داخلة تحت إدارتها.",
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                btnDeleteRole.Enabled = false;
                SetStatus("جاري الحذف...");

                using var http = CreateHttpClient();
                if (http == null) return;

                var resp = await http.DeleteAsync($"api/employee-roles/{_selectedRoleId}");
                if (resp.IsSuccessStatusCode)
                {
                    AuditLogger.Log("RoleDeleted", "Hierarchy", "Frm_Roles",
                        $"تم حذف رتبة: {txtRoleName.Text}",
                        targetTable: "EmployeeRoles", targetId: _selectedRoleId.ToString());

                    SetStatus("تم الحذف بنجاح.");
                    _isNewMode = true;
                    await LoadRolesFromServerAsync();
                }
                else
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    ShowError($"فشل الحذف: {err}");
                }
            }
            catch (Exception ex)
            {
                ShowError($"حدث خطأ أثناء الحذف: {ex.Message}");
            }
            finally
            {
                btnDeleteRole.Enabled = true;
            }
        }

        private class ComboBoxItem
        {
            public string Name { get; }
            public Guid Id { get; }
            public ComboBoxItem(string name, Guid id) { Name = name; Id = id; }
            public override string ToString() => Name;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
