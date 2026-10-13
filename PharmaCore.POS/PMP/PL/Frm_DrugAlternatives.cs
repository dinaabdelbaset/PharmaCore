using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_DrugAlternatives : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _targetDrugName;

        // ── Public Properties for Dialog Usage ──
        public string? SelectedTradeName { get; private set; }

        public Frm_DrugAlternatives(string targetDrugName)
        {
            InitializeComponent();
            _targetDrugName = targetDrugName;
            lblTitle.Text = $"بدائل: {targetDrugName}";
            SetupGridColumns();
        }

        private void SetupGridColumns()
        {
            DgvAlternatives.AutoGenerateColumns = false;
            DgvAlternatives.Columns.Clear();

            DgvAlternatives.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TradeNameEn", Name = "colTradeEn", HeaderText = "الاسم التجاري", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            DgvAlternatives.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TradeNameAr", Name = "colTradeAr", HeaderText = "الاسم (عربي)", Width = 150 });
            DgvAlternatives.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CompanyName", Name = "colCompany", HeaderText = "الشركة", Width = 150 });
            DgvAlternatives.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PublicPrice", Name = "colPrice", HeaderText = "سعر الجمهور", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" } });
            
            // These columns contain the local match logic
            DgvAlternatives.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LocalStockQty", Name = "colLocalStock", HeaderText = "الرصيد المحلي", Width = 100 });
            
            // Hidden tracking columns
            DgvAlternatives.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "HasLocalProduct", Name = "colHasLocal", Visible = false });
        }

        private async void Frm_DrugAlternatives_Load(object sender, EventArgs e)
        {
            await LoadAlternativesAsync();
        }

        private async Task LoadAlternativesAsync()
        {
            try
            {
                ShowBlackout();
                using var http = ConnectionManager.GetHttpClient();
                var url = $"api/global-catalog/alternatives?name={Uri.EscapeDataString(_targetDrugName)}&type=similar";

                var res = await http.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var items = await res.Content.ReadFromJsonAsync<List<AlternativeDto>>() ?? new();
                    DgvAlternatives.DataSource = items;
                }
                else
                {
                    XtraMessageBox.Show("لا يوجد هذا الصنف في القاموس الطبي، أو أن السيرفر غير متصل.", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ في الاتصال: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                HideBlackout();
            }
        }

        // ── Traffic Light Logic ──────────────────────────
        private void DgvAlternatives_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= DgvAlternatives.Rows.Count) return;

            var row = DgvAlternatives.Rows[e.RowIndex];
            var item = row.DataBoundItem as AlternativeDto;
            if (item == null) return;

            // 🟢 Green = Has local AND stock > 0
            // 🟡 Yellow = Has local AND stock <= 0
            // 🔴 Red = No local product at all
            
            if (!item.HasLocalProduct)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238); // Light Red
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 115, 115);
            }
            else if (item.LocalStockQty <= 0)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225); // Light Yellow
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 183, 77);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233); // Light Green
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(129, 199, 132);
            }
        }

        // ── User Selection ──────────────────────────────
        private void DgvAlternatives_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvAlternatives.CurrentRow == null) return;
            var item = DgvAlternatives.CurrentRow.DataBoundItem as AlternativeDto;
            if (item == null) return;

            // Optional: The user might want to select this name to put into the search bar
            SelectedTradeName = item.TradeNameEn;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private class AlternativeDto
        {
            public string TradeNameEn { get; set; } = "";
            public string? TradeNameAr { get; set; }
            public string? CompanyName { get; set; }
            public decimal? PublicPrice { get; set; }
            public string? Description { get; set; }
            public bool HasLocalProduct { get; set; }
            public decimal LocalStockQty { get; set; }
        }
    }
}
