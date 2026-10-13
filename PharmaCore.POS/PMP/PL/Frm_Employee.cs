using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Employee : DevExpress.XtraEditors.XtraForm
    {
        private readonly IEmployeesService _employeesService;
        private List<EmployeeDto> _employees = new();

        public Frm_Employee() : this(null!) { }
        public Frm_Employee(IEmployeesService employeesService)
        {
            InitializeComponent();
            _employeesService = employeesService ?? new EmployeesService(new System.Net.Http.HttpClient{ BaseAddress = new Uri(Properties.Settings.Default.ApiBase ?? "http://localhost:5000/") });
        }

        private async void Frm_Employee_Load(object sender, EventArgs e)
        {
            await LoadEmployeesAsync();
            PrepareNew();
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                _employees = (await _employeesService.GetAllAsync()).ToList();
                DataEmp.DataSource = _employees;
                txtTotal.Text = _employees.Count.ToString();
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message, "Load Error");
#endif
            }
        }

        private void PrepareNew()
        {
            txtID.Text = Guid.NewGuid().ToString();
            txtName.Text = string.Empty;
            txtType.Text = string.Empty;
            txtSalary.Text = string.Empty; // No salary field in DTO yet
            txtNationalID.Text = string.Empty;
            txtPhone1.Text = string.Empty;
            txtPhone2.Text = string.Empty;
            txtAddress.Text = string.Empty;
            DtbDate.Text = DateTime.Now.ToShortDateString();
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("ادخل اسم الموظف"); txtName.Focus(); return; }
            var dto = new EmployeeDto
            {
                Id = Guid.NewGuid(),
                EmployeeCode = txtID.Text,
                FullName = txtName.Text,
                Role = string.IsNullOrWhiteSpace(txtType.Text) ? "Staff" : txtType.Text,
                IsActive = true,
                OrgId = Guid.Empty // placeholder if needed
            };
            var created = await _employeesService.CreateAsync(dto);
            if (created == null) { MessageBox.Show("فشل الإضافة"); return; }
            await LoadEmployeesAsync();
            PrepareNew();
            MessageBox.Show("تم الإضافة بنجاح");
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (DataEmp?.CurrentRow?.DataBoundItem is EmployeeDto emp)
            {
                txtID.Text = emp.EmployeeCode;
                txtName.Text = emp.FullName;
                txtType.Text = emp.Role;
                btnAdd.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var emp = DataEmp?.CurrentRow?.DataBoundItem as EmployeeDto;
            if (emp == null) { MessageBox.Show("لا يوجد عنصر للتعديل"); return; }
            emp.FullName = txtName.Text;
            emp.Role = txtType.Text;
            if (!await _employeesService.UpdateAsync(emp)) { MessageBox.Show("فشل التعديل"); return; }
            await LoadEmployeesAsync();
            PrepareNew();
            MessageBox.Show("تم التعديل بنجاح");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataEmp?.CurrentRow?.DataBoundItem is not EmployeeDto emp) { MessageBox.Show("لا يوجد صف محدد"); return; }
            if (MessageBox.Show("تأكيد الحذف؟", "حذف", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!await _employeesService.DeleteAsync(emp.Id)) { MessageBox.Show("فشل الحذف"); return; }
                await LoadEmployeesAsync();
                PrepareNew();
                MessageBox.Show("تم الحذف");
            }
        }

        private void btnNew_Click(object sender, EventArgs e) => PrepareNew();

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            // No numeric validation needed now (salary not stored)
        }
    }
}