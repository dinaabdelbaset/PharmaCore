using DevExpress.XtraReports.UI;
using PharmaCore.POS.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmaCore.POS.Forms
{
    public partial class Frm_ReportManager : Form
    {
        private class ReportDefinition
        {
            public string DisplayName { get; set; }
            public string ClassName { get; set; }
            public Type ReportType { get; set; }
        }

        private List<ReportDefinition> _reports;

        public Frm_ReportManager()
        {
            InitializeComponent();
        }

        private void Frm_ReportManager_Load(object sender, EventArgs e)
        {
            // Load available reports - this can be expanded dynamically later
            _reports = new List<ReportDefinition>
            {
                new ReportDefinition { DisplayName = "فاتورة المبيعات", ClassName = "RptOrderSales1", ReportType = typeof(RE.RptOrderSales2) },
                new ReportDefinition { DisplayName = "ملصق باركود صنف", ClassName = "RptCrystalReport1", ReportType = typeof(RE.RptCrystalReport1) },
                new ReportDefinition { DisplayName = "سند قبض", ClassName = "RptSanadKabd1", ReportType = typeof(RE.RptSanadKabd1) },
                new ReportDefinition { DisplayName = "سند صرف", ClassName = "RptSanadSarf1", ReportType = typeof(RE.RptSanadSarf1) }
            };

            listBoxReports.DisplayMember = "DisplayName";
            listBoxReports.ValueMember = "ClassName";
            listBoxReports.DataSource = _reports;

            UpdateUIStatus();
        }

        private void listBoxReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIStatus();
        }

        private void UpdateUIStatus()
        {
            if (listBoxReports.SelectedItem is ReportDefinition item)
            {
                bool hasCustom = ReportManager.HasCustomLayout(item.ClassName);
                btnReset.Enabled = hasCustom;
                
                if (hasCustom)
                    btnReset.Text = "استعادة الافتراضي\r\n(معدل حالياً)";
                else
                    btnReset.Text = "استعادة الافتراضي\r\n(أصلي)";
            }
        }

        private async void btnDesign_Click(object sender, EventArgs e)
        {
            if (listBoxReports.SelectedItem is ReportDefinition item)
            {
                // Create an instance of the default compiled report
                var defaultReport = (XtraReport)Activator.CreateInstance(item.ReportType);
                
                // If there's an existing custom layout, load it first so they edit their customized version
                string filePath = ReportManager.GetReportFilePath(item.ClassName);
                if (System.IO.File.Exists(filePath))
                {
                    defaultReport.LoadLayoutFromXml(filePath);
                }

                // INJECT SMART ARABIC SCHEMA BEFORE OPENING DESIGNER
                // This ensures the generic "Field List" panel displays the correct columns and mock data
                System.Data.DataTable schema = ReportSchemaProvider.GetSchemaForReport(item.ClassName);
                if (schema.Columns.Count > 0)
                {
                    defaultReport.DataSource = schema;
                    defaultReport.DataMember = ""; // Ensure DataMember is cleared because we bind directly to DataTable
                }

                // CRUCIAL: Clean out legacy DevExpress DataSources so they don't pollute the Designer Field List
                for (int i = defaultReport.ComponentStorage.Count - 1; i >= 0; i--)
                {
                    if (defaultReport.ComponentStorage[i] is DevExpress.DataAccess.Sql.SqlDataSource)
                    {
                        defaultReport.ComponentStorage.RemoveAt(i);
                    }
                }

                // Show the DevExpress designer
                DevExpress.XtraReports.UI.ReportDesignTool designTool = new DevExpress.XtraReports.UI.ReportDesignTool(defaultReport);
                
                designTool.DesignRibbonForm.DesignMdiController.DesignPanelLoaded += (s, ev) =>
                {
                    var panel = (DevExpress.XtraReports.UserDesigner.XRDesignPanel)s;
                    panel.AddCommandHandler(new PharmaCore.POS.Services.SilentSaveCommandHandler(panel, filePath));
                };

                // This line will block until the designer is closed
                designTool.ShowRibbonDesignerDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);

                // Save automatically upon closing designer if changes were made
                defaultReport.SaveLayoutToXml(filePath);
                
                // Upload to Central API Database
                btnDesign.Enabled = false;
                btnDesign.Text = "جاري الرفع...";
                await ReportManager.UploadLayoutToApiAsync(item.ClassName);
                btnDesign.Enabled = true;
                btnDesign.Text = "تعديل تصميم التقرير";

                MessageBox.Show("تم حفظ التصميم بنجاح ورفعه على السيرفر المركزي", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                UpdateUIStatus();
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            if (listBoxReports.SelectedItem is ReportDefinition item)
            {
                if (MessageBox.Show($"هل أنت متأكد من مسح التعديلات والعودة للتصميم الأصلي لـ {item.DisplayName}؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    btnReset.Enabled = false;
                    await ReportManager.ResetToDefaultAsync(item.ClassName);
                    btnReset.Enabled = true;
                    MessageBox.Show("تم استعادة التصميم الأصلي من السيرفر بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateUIStatus();
                }
            }
        }
    }
}
