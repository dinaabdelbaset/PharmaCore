using DevExpress.XtraEditors;
using PharmaCore.POS.Services.Security;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public partial class Frm_BranchEmployeeAccess : XtraForm
    {
        private readonly Guid _orgId;
        private readonly Guid _branchId;

        private readonly BindingSource _bs = new();
        private List<AccessRow> _rows = new();

        public Frm_BranchEmployeeAccess()
        {
            InitializeComponent();

            var meta = ActivationMetadataStorage.Load();
            _orgId = meta?.OrgId ?? Guid.Empty;
            _branchId = meta?.BranchId ?? Guid.Empty;

            grid.DataSource = _bs;

            btnRefresh.Click += async (_, __) => await LoadAsync();
            btnAdd.Click += async (_, __) => await AddOrUpdateAsync(isUpdate: false);
            btnUpdate.Click += async (_, __) => await AddOrUpdateAsync(isUpdate: true);
            btnDelete.Click += async (_, __) => await DeleteAsync();

            Shown += async (_, __) => await LoadAsync();
        }

        private sealed record AccessRow
        {
            public Guid Id { get; set; }
            public Guid OrgId { get; set; }
            public Guid EmployeeId { get; set; }
            public Guid BranchId { get; set; }
            public int AccessType { get; set; }
            public DateTime? AllowedFrom { get; set; }
            public DateTime? AllowedTo { get; set; }
            public bool IsActive { get; set; }
        }

        private HttpClient CreateHttp()
        {
            var baseUrl = Properties.Settings.Default.ApiBase?.Trim();
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new InvalidOperationException("API base URL is missing");
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            var _sslHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true };
            return new HttpClient(_sslHandler) { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(20) };
        }

        private async Task LoadAsync()
        {
            if (_branchId == Guid.Empty) { XtraMessageBox.Show("BranchId غير متاح", "خطأ"); return; }
            using var http = CreateHttp();
            var res = await http.GetFromJsonAsync<List<AccessRow>>($"api/branches/{_branchId}/employee-access");
            _rows = res ?? new();
            _bs.DataSource = _rows;
        }

        private async Task AddOrUpdateAsync(bool isUpdate)
        {
            if (_branchId == Guid.Empty) { XtraMessageBox.Show("BranchId غير متاح", "خطأ"); return; }
            if (!Guid.TryParse(txtEmployeeId.Text.Trim(), out var empId)) { XtraMessageBox.Show("ادخل EmployeeId صحيح", "تنبيه"); return; }

            var accessType = cmbAccessType.SelectedIndex <= 0 ? 0 : 1;
            DateTime? from = chkWindow.Checked ? dtFrom.Value.ToUniversalTime() : null;
            DateTime? to = chkWindow.Checked ? dtTo.Value.ToUniversalTime() : null;

            var dto = new
            {
                id = isUpdate && Guid.TryParse(txtId.Text.Trim(), out var idVal) ? idVal : (Guid?)null,
                orgId = _orgId,
                employeeId = empId,
                branchId = _branchId,
                accessType = accessType,
                allowedFrom = from,
                allowedTo = to,
                isActive = chkActive.Checked
            };

            using var http = CreateHttp();

            HttpResponseMessage resp;
            if (!isUpdate)
                resp = await http.PostAsJsonAsync($"api/branches/{_branchId}/employee-access", dto);
            else
            {
                if (!Guid.TryParse(txtId.Text.Trim(), out var id)) { XtraMessageBox.Show("ادخل Id صحيح للتعديل", "تنبيه"); return; }
                resp = await http.PutAsJsonAsync($"api/branches/{_branchId}/employee-access/{id}", dto);
            }

            if (!resp.IsSuccessStatusCode)
            {
                XtraMessageBox.Show("فشل الحفظ", "خطأ");
                return;
            }

            await LoadAsync();
        }

        private async Task DeleteAsync()
        {
            if (!Guid.TryParse(txtId.Text.Trim(), out var id)) { XtraMessageBox.Show("ادخل Id صحيح للحذف", "تنبيه"); return; }
            using var http = CreateHttp();
            var resp = await http.DeleteAsync($"api/branches/{_branchId}/employee-access/{id}");
            if (!resp.IsSuccessStatusCode) { XtraMessageBox.Show("فشل الحذف", "خطأ"); return; }
            await LoadAsync();
        }
    }
}
