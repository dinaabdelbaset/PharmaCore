using DevExpress.XtraEditors;
using PharmaCore.POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmaCore.POS.PL
{
    public sealed partial class Frm_EmployeeAdmin : XtraForm
    {
        private readonly IConnectionStringProvider _csProvider = new ConnectionStringProvider();
        private Guid _orgId; private Guid _branchId; private Guid? _tenantId; private Guid? _regionId;
        private List<EmployeeAdminDb.EmployeeRow> _rows = new();
        private Guid _editingId = Guid.Empty;

        public Frm_EmployeeAdmin()
        {
            InitializeComponent();
            var meta = Services.Security.ActivationMetadataStorage.Load(); if (meta != null) { _orgId = meta.OrgId; _branchId = meta.BranchId; _tenantId = meta.TenantId; _regionId = meta.RegionId; }
            grid.SelectionChanged += (_, __) => LoadSelection();
            btnNew.Click += (_, __) => PrepareNew();
            btnSave.Click += async (_, __) => await CreateAsync();
            btnUpdate.Click += async (_, __) => await UpdateAsync();
            btnResetPin.Click += async (_, __) => await ResetPinAsync();
            btnDelete.Click += async (_, __) => await DeleteAsync();
            Shown += async (_, __) => await RefreshAsync();
            PrepareNew();
        }

        private async Task RefreshAsync()
        {
            try
            {
                var cs = _csProvider.GetConnectionString(); if (string.IsNullOrWhiteSpace(cs)) { MessageBox.Show("سلسلة الاتصال غير متاحة"); return; }
                _rows = (await EmployeeAdminDb.LoadAllAsync(cs, _orgId)).ToList();
                bs.DataSource = _rows;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "خطأ تحميل"); }
        }

        private async Task CreateAsync()
        {
            var cs = _csProvider.GetConnectionString(); if (string.IsNullOrWhiteSpace(cs)) { MessageBox.Show("سلسلة الاتصال غير متاحة"); return; }
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("ادخل الاسم"); return; }
            if (string.IsNullOrWhiteSpace(txtPin.Text)) { MessageBox.Show("ادخل PIN"); return; }
            var isCodeManual = !string.IsNullOrWhiteSpace(txtCode.Text);
            var code = isCodeManual ? txtCode.Text.Trim() : await EmployeeAdminDb.GenerateNextEmployeeCodeAsync(cs, _orgId);
            var id = await EmployeeAdminDb.CreateAsync(cs, _orgId, _tenantId, _regionId, _branchId, code, txtName.Text.Trim(), txtPin.Text.Trim(), txtRole.Text.Trim(), txtJob.Text.Trim(), txtGrade.Text.Trim(), chkActive.Checked);
            if (id == null) { MessageBox.Show("فشل الإضافة"); return; }
            await RefreshAsync(); PrepareNew(); MessageBox.Show("تم الإضافة");
        }

        private async Task UpdateAsync()
        {
            if (_editingId == Guid.Empty) { MessageBox.Show("لا يوجد عنصر محدد"); return; }
            var cs = _csProvider.GetConnectionString(); if (string.IsNullOrWhiteSpace(cs)) { MessageBox.Show("سلسلة الاتصال غير متاحة"); return; }
            if (!await EmployeeAdminDb.UpdateAsync(cs, _editingId, txtName.Text.Trim(), txtJob.Text.Trim(), txtGrade.Text.Trim(), txtRole.Text.Trim(), chkActive.Checked)) { MessageBox.Show("فشل التعديل"); return; }
            await RefreshAsync(); MessageBox.Show("تم الحفظ");
        }

        private async Task ResetPinAsync()
        {
            if (_editingId == Guid.Empty) { MessageBox.Show("حدد موظف أولاً"); return; }
            if (string.IsNullOrWhiteSpace(txtPin.Text)) { MessageBox.Show("ادخل PIN جديد"); return; }
            var cs = _csProvider.GetConnectionString(); if (string.IsNullOrWhiteSpace(cs)) { MessageBox.Show("سلسلة الاتصال غير متاحة"); return; }
            if (!await EmployeeAdminDb.ResetPinAsync(cs, _editingId, txtPin.Text.Trim())) { MessageBox.Show("فشل تحديث PIN"); return; }
            txtPin.Clear(); MessageBox.Show("تم تغيير PIN");
        }

        private async Task DeleteAsync()
        {
            if (_editingId == Guid.Empty) { MessageBox.Show("حدد موظف أولاً"); return; }
            if (MessageBox.Show("تأكيد الحذف؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            var cs = _csProvider.GetConnectionString(); if (string.IsNullOrWhiteSpace(cs)) { MessageBox.Show("سلسلة الاتصال غير متاحة"); return; }
            if (!await EmployeeAdminDb.DeleteAsync(cs, _editingId)) { MessageBox.Show("فشل الحذف"); return; }
            await RefreshAsync(); PrepareNew(); MessageBox.Show("تم الحذف");
        }

        private void PrepareNew()
        {
            _editingId = Guid.Empty; txtCode.Clear(); txtName.Clear(); txtJob.Clear(); txtGrade.Clear(); txtRole.Text = "Staff"; txtPin.Clear(); chkActive.Checked = true;
            btnSave.Enabled = true; btnUpdate.Enabled = false; btnResetPin.Enabled = false; btnDelete.Enabled = false; grid.ClearSelection();
        }

        private void LoadSelection()
        {
            if (grid.CurrentRow?.DataBoundItem is EmployeeAdminDb.EmployeeRow row)
            {
                _editingId = row.Id; txtCode.Text = row.EmployeeCode; txtName.Text = row.FullName; txtJob.Text = row.JobTitle ?? string.Empty; txtGrade.Text = row.Grade ?? string.Empty; txtRole.Text = row.Role; chkActive.Checked = row.IsActive;
                btnSave.Enabled = false; btnUpdate.Enabled = true; btnResetPin.Enabled = true; btnDelete.Enabled = true; txtPin.Clear();
            }
        }

    }
}
