using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_Customer : DevExpress.XtraEditors.XtraForm
    {
        private DataTable tblGroup = new DataTable();
        private Guid? selectedCustomerId = null;

        // ── DTOs matching API ──
        public class CustomerDto
        {
            public Guid Id { get; set; }
            public string CustomerCode { get; set; }
            public string Name { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public string? Notes { get; set; }
            public decimal PointsBalance { get; set; }
        }

        public class CreateCustomerReq
        {
            public string CustomerCode { get; set; }
            public string Name { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public string? Notes { get; set; }
        }

        public class UpdateCustomerReq
        {
            public string Name { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public string? Notes { get; set; }
        }

        public Frm_Customer()
        {
            InitializeComponent();
            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
        }

        private void SetupGridColumns()
        {
            tblGroup = new DataTable();
            tblGroup.Columns.Add("Cust_ID", typeof(string));
            tblGroup.Columns.Add("Cust_Name", typeof(string));
            tblGroup.Columns.Add("Cust_Phone", typeof(string));
            tblGroup.Columns.Add("Cust_Address", typeof(string));
            tblGroup.Columns.Add("Notes", typeof(string));
            tblGroup.Columns.Add("PointsBalance", typeof(decimal));
            tblGroup.Columns.Add("Id", typeof(Guid));

            DataCust.DataSource = tblGroup;

            if (DataCust.Columns.Contains("Cust_ID")) DataCust.Columns["Cust_ID"].HeaderText = "كود العميل";
            if (DataCust.Columns.Contains("Cust_Name")) DataCust.Columns["Cust_Name"].HeaderText = "اسم العميل";
            if (DataCust.Columns.Contains("Cust_Phone")) DataCust.Columns["Cust_Phone"].HeaderText = "رقم التليفون";
            if (DataCust.Columns.Contains("Cust_Address")) DataCust.Columns["Cust_Address"].HeaderText = "العنوان";
            if (DataCust.Columns.Contains("Notes")) DataCust.Columns["Notes"].HeaderText = "ملاحظات";
            if (DataCust.Columns.Contains("PointsBalance")) DataCust.Columns["PointsBalance"].HeaderText = "رصيد النقاط";
            if (DataCust.Columns.Contains("Id")) DataCust.Columns["Id"].Visible = false;
        }

        private void EnableFields(bool enabled)
        {
            txtID.Enabled = enabled;
            txtName.Enabled = enabled;
            txtPhone.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtNotes.Enabled = enabled;
        }

        private void AutoNumber()
        {
            selectedCustomerId = null;
            txtID.Text = (DataCust.Rows.Count + 1).ToString();
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtNotes.Text = "";

            EnableFields(true);

            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            DtpDate.Text = DateTime.Now.ToShortDateString();
            txtTotal.Text = DataCust.Rows.Count.ToString();
        }

        private async void Frm_Customer_Load(object sender, EventArgs e)
        {
            SetupGridColumns();
            AutoNumber();

            // Enable search text boxes
            Main_name.Enabled = true;
            Main_mobile.Enabled = true;
            Main_person.Enabled = true;
            Main_Notes.Enabled = true;

            // Handle general cell DoubleClick
            DataCust.CellDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0)
                {
                    PopulateCustomerFields(DataCust.Rows[ev.RowIndex]);
                    if (Properties.Settings.Default.Open_Form == 16 && DataCust.Rows.Count >= 1)
                    {
                        if (int.TryParse(txtID.Text, out int parsedCode))
                        {
                            Properties.Settings.Default.CustCod = parsedCode;
                        }
                        else
                        {
                            Properties.Settings.Default.CustCod = 1;
                        }
                        Properties.Settings.Default.Save();
                        Close();
                    }
                }
            };

            await LoadCustomersAsync();
        }

        private void PopulateCustomerFields(DataGridViewRow row)
        {
            if (row == null) return;

            txtID.Text = row.Cells["Cust_ID"].Value?.ToString() ?? "";
            txtName.Text = row.Cells["Cust_Name"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["Cust_Phone"].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells["Cust_Address"].Value?.ToString() ?? "";
            txtNotes.Text = row.Cells["Notes"].Value?.ToString() ?? "";

            if (row.Cells["Id"].Value is Guid guidId)
            {
                selectedCustomerId = guidId;
            }
            else if (row.Cells["Id"].Value != null && Guid.TryParse(row.Cells["Id"].Value.ToString(), out Guid parsedId))
            {
                selectedCustomerId = parsedId;
            }
            else if (row.Tag is Guid tagId)
            {
                selectedCustomerId = tagId;
            }
            else
            {
                selectedCustomerId = null;
            }

            txtID.Enabled = false; // Code remains read-only during edit
            txtName.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
            txtNotes.Enabled = true;

            btnAdd.Enabled = false;
            btnSave.Enabled = true;
        }

        private async Task LoadCustomersAsync(string query = null)
        {
            try
            {
                using (var http = ConnectionManager.GetHttpClient())
                {
                    string url = "/api/customers";
                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        url += "?q=" + Uri.EscapeDataString(query);
                    }

                    var response = await http.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CustomerDto>>(json);

                    tblGroup.Rows.Clear();
                    if (list != null)
                    {
                        foreach (var cust in list)
                        {
                            tblGroup.Rows.Add(
                                cust.CustomerCode,
                                cust.Name,
                                cust.Phone,
                                cust.Address,
                                cust.Notes,
                                cust.PointsBalance,
                                cust.Id
                            );
                        }
                    }

                    txtTotal.Text = tblGroup.Rows.Count.ToString();

                    // Map grid row tags to Guidance ID
                    for (int i = 0; i < DataCust.Rows.Count; i++)
                    {
                        var drv = DataCust.Rows[i].DataBoundItem as DataRowView;
                        if (drv != null)
                        {
                            DataCust.Rows[i].Tag = drv["Id"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء تحميل قائمة العملاء: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                XtraMessageBox.Show("يرجى إدخال اسم العميل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string customerCode = txtID.Text?.Trim();
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                XtraMessageBox.Show("يرجى إدخال كود العميل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new CreateCustomerReq
            {
                CustomerCode = customerCode,
                Name = name,
                Phone = txtPhone.Text?.Trim(),
                Address = txtAddress.Text?.Trim(),
                Notes = txtNotes.Text?.Trim()
            };

            try
            {
                using (var http = ConnectionManager.GetHttpClient())
                {
                    var content = new StringContent(
                        Newtonsoft.Json.JsonConvert.SerializeObject(req),
                        Encoding.UTF8,
                        "application/json"
                    );
                    var response = await http.PostAsync("api/customers", content);
                    if (response.IsSuccessStatusCode)
                    {
                        XtraMessageBox.Show("تم إضافة العميل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomersAsync();
                        AutoNumber();
                    }
                    else
                    {
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        XtraMessageBox.Show($"فشلت عملية الإضافة: {errorMsg}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء الاتصال بالسيرفر: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == null)
            {
                XtraMessageBox.Show("يرجى اختيار عميل من القائمة للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtName.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                XtraMessageBox.Show("يرجى إدخال اسم العميل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new UpdateCustomerReq
            {
                Name = name,
                Phone = txtPhone.Text?.Trim(),
                Address = txtAddress.Text?.Trim(),
                Notes = txtNotes.Text?.Trim()
            };

            try
            {
                using (var http = ConnectionManager.GetHttpClient())
                {
                    var content = new StringContent(
                        Newtonsoft.Json.JsonConvert.SerializeObject(req),
                        Encoding.UTF8,
                        "application/json"
                    );
                    var response = await http.PutAsync($"api/customers/{selectedCustomerId}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        XtraMessageBox.Show("تم تعديل العميل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomersAsync();
                        AutoNumber();
                    }
                    else
                    {
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        XtraMessageBox.Show($"فشلت عملية التعديل: {errorMsg}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء الاتصال بالسيرفر: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DataCust.CurrentRow != null && DataCust.CurrentRow.Index >= 0)
            {
                PopulateCustomerFields(DataCust.CurrentRow);
            }
            else
            {
                XtraMessageBox.Show("يرجى تحديد عميل من الجدول للتعديل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == null)
            {
                XtraMessageBox.Show("يرجى اختيار عميل من القائمة للمسح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = XtraMessageBox.Show("هل أنت متأكد من مسح هذا العميل؟", "تأكيد المسح", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var http = ConnectionManager.GetHttpClient())
                {
                    var response = await http.DeleteAsync($"api/customers/{selectedCustomerId}");
                    if (response.IsSuccessStatusCode)
                    {
                        XtraMessageBox.Show("تم مسح العميل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomersAsync();
                        AutoNumber();
                    }
                    else
                    {
                        string errorMsg = await response.Content.ReadAsStringAsync();
                        XtraMessageBox.Show($"فشلت عملية المسح: {errorMsg}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ أثناء الاتصال بالسيرفر: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Main_name_TextChanged(object sender, EventArgs e)
        {
            await LoadCustomersAsync(Main_name.Text);
        }

        private async void Main_person_TextChanged(object sender, EventArgs e)
        {
            await LoadCustomersAsync(Main_person.Text);
        }

        private async void Main_mobile_TextChanged(object sender, EventArgs e)
        {
            await LoadCustomersAsync(Main_mobile.Text);
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            await LoadCustomersAsync(Main_Notes.Text);
        }

        private void DataCust_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PopulateCustomerFields(DataCust.Rows[e.RowIndex]);
                if (Properties.Settings.Default.Open_Form == 16 && DataCust.Rows.Count >= 1)
                {
                    if (int.TryParse(txtID.Text, out int parsedCode))
                    {
                        Properties.Settings.Default.CustCod = parsedCode;
                    }
                    else
                    {
                        Properties.Settings.Default.CustCod = 1;
                    }
                    Properties.Settings.Default.Save();
                    Close();
                }
            }
        }

        private void Frm_Customers_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* legacy close logic removed */
        }
    }
}