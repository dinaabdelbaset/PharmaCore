using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_ItemSales : DevExpress.XtraEditors.XtraForm
    {
        // ── Public Properties for Dialog Usage ──
        public Guid? SelectedProductId { get; private set; }
        public string? SelectedProductName { get; private set; }
        public string? SelectedProductBarcode { get; private set; }

        public Frm_ItemSales()
        {
            InitializeComponent();
            SetupGridColumns();
        }

        private void SetupGridColumns()
        {
            DgvSearch.AutoGenerateColumns = false;
            DgvSearch.Columns.Clear();
            
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", Name = "colProductId", HeaderText = "رقم المنتج", Visible = false });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", Name = "colName", HeaderText = "اسم المنتج", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Barcode", Name = "colBarcode", HeaderText = "الباركود", Width = 150 });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExpiryDateStr", Name = "colExpiry", HeaderText = "الصلاحية", Width = 100 });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockQty", Name = "colStock", HeaderText = "الكمية", Width = 100 });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SalePrice", Name = "colPrice", HeaderText = "السعر", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" } });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ActiveIngredient", Name = "colIngredient", HeaderText = "المادة الفعالة", Width = 150 });
            DgvSearch.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CompanyName", Name = "colCompany", HeaderText = "الشركة", Width = 150 });
        }

        private void Frm_ItemSales_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            // Optional: load all or initial top 100 on open
            timerSearch.Start();
        }

        private void Frm_ItemSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.Open_Form == 1)
                {
                    Frm_Buy.GetFormBuy?.AddItems(3);
                }
                if (Properties.Settings.Default.Open_Form == 2)
                {
                    Frm_Sales.GetFormSales?.AddItems(3);
                }
            }
            catch { }
        }

        // ── Search Logic (Debounced) ─────────────────────
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            timerSearch.Stop();
            timerSearch.Start();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                timerSearch.Stop();
                _ = PerformSearchAsync(txtSearch.Text);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (DgvSearch.Rows.Count > 0)
                {
                    DgvSearch.Focus();
                }
            }
        }

        private async void timerSearch_Tick(object sender, EventArgs e)
        {
            timerSearch.Stop();
            await PerformSearchAsync(txtSearch.Text);
        }

        private async Task PerformSearchAsync(string query)
        {
            try
            {
                ShowBlackout();
                using var http = ConnectionManager.GetHttpClient();
                // We use field=all to leverage the universal search via API
                var url = string.IsNullOrWhiteSpace(query) 
                    ? "api/batches/search?q=*&field=all" // Load default recent
                    : $"api/batches/search?q={Uri.EscapeDataString(query)}&field=all";

                var res = await http.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var items = await res.Content.ReadFromJsonAsync<List<BatchSearchResultDto>>() ?? new();
                    DgvSearch.DataSource = items;
                }
            }
            catch (Exception ex)
            {
                // Silent catch or log to avoid interrupting fast typing
                Console.WriteLine(ex.Message);
            }
            finally
            {
                HideBlackout();
            }
        }

        // ── Selection Handling ───────────────────────────
        private void DgvSearch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectAndClose();
        }

        private void DgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SelectAndClose();
            }
        }

        private void SelectAndClose()
        {
            if (DgvSearch.CurrentRow == null) return;
            
            var row = DgvSearch.CurrentRow;
            var item = row.DataBoundItem as BatchSearchResultDto;
            if (item == null) return;

            SelectedProductId = item.ProductId;
            SelectedProductName = item.ProductName;
            SelectedProductBarcode = item.Barcode;

            // Legacy fallback for callers tracking integer Sales IDs based on Barcode mapping
            if (int.TryParse(SelectedProductBarcode, out int sid))
            {
                Properties.Settings.Default.sales_ID = sid;
                Properties.Settings.Default.Save();
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnAlternatives_Click(object sender, EventArgs e)
        {
            if (DgvSearch.CurrentRow == null)
            {
                XtraMessageBox.Show("الرجاء تحديد صنف أولاً من القائمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = DgvSearch.CurrentRow.DataBoundItem as BatchSearchResultDto;
            if (item == null) return;

            string targetName = item.ProductName;
            using (var frm = new Frm_DrugAlternatives(targetName))
            {
                if (frm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(frm.SelectedTradeName))
                {
                    txtSearch.Text = frm.SelectedTradeName; // Auto-search the chosen alternative
                }
            }
        }

        // ── UI Helpers ───────────────────────────────────
        private void ShowBlackout()
        {
            picLoading.Visible = true;
            picLoading.BringToFront();
            Application.DoEvents();
        }

        private void HideBlackout()
        {
            picLoading.Visible = false;
        }

        // ── Inner DTOs ───────────────────────────────────
        private class BatchSearchResultDto
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = "";
            public string? ProductNameAr { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public decimal StockQty { get; set; }
            public decimal SalePrice { get; set; }
            public string Barcode { get; set; } = "";
            public decimal BuyPrice { get; set; }
            public decimal MinQtyAlert { get; set; }
            public string? CompanyName { get; set; }
            public string? ActiveIngredient { get; set; }
            public Guid BatchId { get; set; }

            public string ExpiryDateStr => ExpiryDate.HasValue ? ExpiryDate.Value.ToString("yyyy/MM") : "";
        }
    }
}