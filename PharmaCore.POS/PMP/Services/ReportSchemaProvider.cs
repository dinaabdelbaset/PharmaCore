using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UserDesigner;

namespace PharmaCore.POS.Services
{
    public class SilentSaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        private DevExpress.XtraReports.UserDesigner.XRDesignPanel panel;
        private string filePath;

        public SilentSaveCommandHandler(DevExpress.XtraReports.UserDesigner.XRDesignPanel panel, string filePath)
        {
            this.panel = panel;
            this.filePath = filePath;
        }

        public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, object[] args)
        {
            try
            {
                panel.Report.SaveLayoutToXml(filePath);
                panel.ReportState = DevExpress.XtraReports.UserDesigner.ReportState.Saved;
            }
            catch { }
        }

        public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = !(command == DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFile || 
                               command == DevExpress.XtraReports.UserDesigner.ReportCommand.SaveFileAs);
            return !useNextHandler;
        }
    }

    public static class ReportSchemaProvider
    {
        public static DataTable GetSchemaForReport(string reportClassName)
        {
            if (reportClassName.StartsWith("RptOrderSales"))
            {
                return GetSalesSchema();
            }
            else if (reportClassName == "RptSanadKabd1")
            {
                return GetSanadKabdSchema();
            }
            else if (reportClassName == "RptSanadSarf1")
            {
                return GetSanadSarfSchema();
            }
            else if (reportClassName == "RptCrystalReport1")
            {
                return GetBarcodeSchema();
            }

            return new DataTable(); // Default empty if unsupported
        }

        private static DataTable GetSalesSchema()
        {
            DataTable dt = new DataTable("المبيعات");
            
            // Replicating exactly the Arabic schema from Frm_Sales.cs for 100% interoperability
            dt.Columns.Add("رقم الفاتورة", typeof(string));
            dt.Columns.Add("اسم العميل", typeof(string));
            dt.Columns.Add("المنتج", typeof(string));
            dt.Columns.Add("الكمية", typeof(decimal));
            dt.Columns.Add("الوحدة", typeof(string));
            dt.Columns.Add("السعر شامل الضريبة", typeof(decimal));
            dt.Columns.Add("الخصم", typeof(decimal));
            dt.Columns.Add("الاجمالي", typeof(decimal));
            dt.Columns.Add("اجمالي الفاتورة", typeof(decimal));
            dt.Columns.Add("المبلغ المدفوع", typeof(decimal));
            dt.Columns.Add("المتبقي", typeof(decimal));
            dt.Columns.Add("الكاشير", typeof(string));
            dt.Columns.Add("التاريخ", typeof(string));
            dt.Columns.Add("الضريبة", typeof(decimal));
            dt.Columns.Add("إجمالي المطلوب", typeof(decimal));
            dt.Columns.Add("الوقت", typeof(string));
            dt.Columns.Add("Name", typeof(string)).Caption = "اسم الصيدلية / الشعار";
            dt.Columns.Add("Address", typeof(string)).Caption = "عنوان الصيدلية";
            dt.Columns.Add("Description", typeof(string)).Caption = "وصف الفاتورة";
            dt.Columns.Add("Phone1", typeof(string)).Caption = "تليفون 1";
            dt.Columns.Add("Phone2", typeof(string)).Caption = "تليفون 2";

            // Add dummy data for visualization in the Designer
            dt.Rows.Add(
                "INV-10042", "عميل نقدي", "بانادول أدفانس 500 ملجم", 2m, "قرص", 
                15.50m, 0m, 31.00m, 31.00m, 50m, 19m, "أحمد محمد", 
                DateTime.Now.ToString("yyyy-MM-dd"), 4.5m, 35.50m, 
                DateTime.Now.ToString("hh:mm tt"), "صيدلية الشفاء", "الرياض - شارع العليا", 
                "نهتم بصحتك دائماً", "0501234567", "0112345678"
            );
            
            dt.Rows.Add(
                "INV-10042", "عميل نقدي", "فيتامين سي فوار", 1m, "علبة", 
                20.00m, 2m, 18.00m, 49.00m, 50m, 1m, "أحمد محمد", 
                DateTime.Now.ToString("yyyy-MM-dd"), 2.7m, 53.70m, 
                DateTime.Now.ToString("hh:mm tt"), "صيدلية الشفاء", "الرياض - شارع العليا", 
                "نهتم بصحتك دائماً", "0501234567", "0112345678"
            );

            return dt;
        }

        private static DataTable GetSanadKabdSchema()
        {
            DataTable dt = new DataTable("سند_قبض");
            
            // Extracted from Frm_SanadKabd.cs SELECT AS ...
            dt.Columns.Add("رقم العملية", typeof(int));
            dt.Columns.Add("اسم المسؤل عن القبض", typeof(string));
            dt.Columns.Add("المبلغ", typeof(decimal));
            dt.Columns.Add("تاريخ العملية", typeof(string));
            dt.Columns.Add("تم القبض من ", typeof(string)); // Notice the exact space at the end
            dt.Columns.Add("السبب", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone1", typeof(string));
            dt.Columns.Add("Phone2", typeof(string));

            dt.Rows.Add(105, "محمد المحاسب", 1500.00m, DateTime.Now.ToString("dd/MM/yyyy"), 
                        "شركة الأدوية المتحدة", "دفعة من حساب عميل", 
                        "صيدلية الشفاء", "0501234567", "0112345678");

            return dt;
        }

        private static DataTable GetSanadSarfSchema()
        {
            DataTable dt = new DataTable("سند_صرف");

            // Extracted from Frm_SanadSarf.cs SELECT AS ...
            dt.Columns.Add("رقم العملية", typeof(int));
            dt.Columns.Add("اسم المسؤل عن الصرف", typeof(string));
            dt.Columns.Add("المبلغ", typeof(decimal));
            dt.Columns.Add("تاريخ العملية", typeof(string));
            dt.Columns.Add("تم الصرف لــ ", typeof(string)); // Notice the exact spaces at the end
            dt.Columns.Add("السبب", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone1", typeof(string));
            dt.Columns.Add("Phone2", typeof(string));

            dt.Rows.Add(210, "محمود مدير الفرع", 850.50m, DateTime.Now.ToString("dd/MM/yyyy"), 
                        "عامل النظافة", "مصروفات نثرية - أدوات تنظيف", 
                        "صيدلية الشفاء", "0501234567", "0112345678");

            return dt;
        }

        private static DataTable GetBarcodeSchema()
        {
            DataTable dt = new DataTable("Barcod");

            dt.Columns.Add("اسم المنتج AR", typeof(string)).Caption = "اسم المنتج AR";
            dt.Columns.Add("اسم المنتج EN", typeof(string)).Caption = "اسم المنتج EN";
            dt.Columns.Add("اسم المنتج", typeof(string)).Caption = "اسم المنتج";
            dt.Columns.Add("المجموعة", typeof(string)).Caption = "المجموعة / القسم";
            dt.Columns.Add("المادة الفعالة", typeof(string)).Caption = "المادة الفعالة";
            dt.Columns.Add("اسم الشركة", typeof(string)).Caption = "اسم الشركة";
            dt.Columns.Add("رقم الباركود", typeof(string)).Caption = "رقم الباركود";
            dt.Columns.Add("سعر البيع", typeof(decimal)).Caption = "سعر البيع";
            dt.Columns.Add("تاريخ الصلاحية", typeof(DateTime)).Caption = "تاريخ الصلاحية";
            dt.Columns.Add("اسم الصيدلية", typeof(string)).Caption = "اسم الصيدلية";
            dt.Columns.Add("رقم الهاتف", typeof(string)).Caption = "رقم الهاتف";

            dt.Rows.Add("بانادول أدفانس عربي", "Banadol Advance EN", "Banadol Advance EN", "مسكنات", "باراسيتامول", "GSK", "6281001234567", 25.50m, DateTime.Now.AddYears(2), "صيدلية الشفاء", "0501234567");

            return dt;
        }

        public static void FormatReportForSmartArabic(DevExpress.XtraReports.UI.XtraReport rpt)
        {
            // Remove old SQL DataSources from ComponentStorage so they don't pollute the Field List
            for (int i = rpt.ComponentStorage.Count - 1; i >= 0; i--)
            {
                if (rpt.ComponentStorage[i] is DevExpress.DataAccess.Sql.SqlDataSource)
                {
                    rpt.ComponentStorage.RemoveAt(i);
                }
            }

            // Auto-translate any existing Expression Bindings to map to the new Arabic Schema
            foreach (DevExpress.XtraReports.UI.XRControl control in rpt.AllControls<DevExpress.XtraReports.UI.XRControl>())
            {
                var bindings = control.ExpressionBindings.ToArray();
                bool hasTextBinding = false;
                foreach (var b in bindings) { if (b.PropertyName == "Text") hasTextBinding = true; }

                foreach (var binding in bindings)
                {
                    string expr = binding.Expression;
                    bool changed = false;

                    if (expr.Contains("Pro_Name")) { expr = expr.Replace("Pro_Name", "اسم المنتج"); changed = true; }
                    if (expr.Contains("Sale_PriceTax")) { expr = expr.Replace("Sale_PriceTax", "سعر البيع"); changed = true; }
                    if (expr.Contains("Barcode")) { expr = expr.Replace("Barcode", "رقم الباركود"); changed = true; }
                    if (expr.Contains("Expier")) { expr = expr.Replace("Expier", "تاريخ الصلاحية"); changed = true; }
                    
                    // Legacy OrderPrintData translation
                    if (expr.Contains("Name")) { expr = expr.Replace("Name", "اسم الصيدلية"); changed = true; }
                    if (expr.Contains("Phone1")) { expr = expr.Replace("Phone1", "رقم الهاتف"); changed = true; }

                    if (binding.PropertyName == "BeforePrint" || binding.PropertyName == "Tag")
                    {
                        // Clean up corrupted bindings generated migrating from older DevExpress versions
                        control.ExpressionBindings.Remove(binding);
                        if (!hasTextBinding)
                        {
                            control.ExpressionBindings.Add(new DevExpress.XtraReports.UI.ExpressionBinding("Text", expr));
                            hasTextBinding = true; // prevent duplicate adding
                        }
                    }
                    else if (changed)
                    {
                        control.ExpressionBindings.Remove(binding);
                        control.ExpressionBindings.Add(new DevExpress.XtraReports.UI.ExpressionBinding(binding.PropertyName, expr));
                    }
                }
            }

            // AUTO-HEALING: Structural repair for legacy labels imported from Crystal Reports.
            // If the layout has all controls sitting in PageHeaderBand while DetailBand is empty and hidden,
            // DevExpress will abort data iteration and print blanks. This flawlessly repairs it by moving them.
            var detailBand = rpt.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.DetailBand)) as DevExpress.XtraReports.UI.DetailBand;
            var pageHeaderBand = rpt.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageHeaderBand)) as DevExpress.XtraReports.UI.PageHeaderBand;
            
            if (detailBand != null && pageHeaderBand != null)
            {
                if (detailBand.Controls.Count == 0 && pageHeaderBand.Controls.Count > 0)
                {
                    var ctrls = pageHeaderBand.Controls.Cast<DevExpress.XtraReports.UI.XRControl>().ToArray();
                    foreach (DevExpress.XtraReports.UI.XRControl c in ctrls)
                    {
                        pageHeaderBand.Controls.Remove(c);
                        detailBand.Controls.Add(c);
                    }
                    detailBand.HeightF = pageHeaderBand.HeightF;
                    pageHeaderBand.HeightF = 0;
                    detailBand.Visible = true;
                    pageHeaderBand.Visible = false;
                }
            }

            // AUTO-FIT: Fix invisible/clipped labels.
            foreach (DevExpress.XtraReports.UI.XRLabel lbl in rpt.AllControls<DevExpress.XtraReports.UI.XRLabel>())
            {
                lbl.BeforePrint += (s, ev) =>
                {
                    try
                    {
                        var bindings = lbl.ExpressionBindings.ToArray();
                        string exp = "";
                        foreach (var b in bindings) { if (b.PropertyName == "Text") exp = b.Expression; }

                        if (exp.Contains("اسم المنتج EN") && rpt.GetCurrentColumnValue("اسم المنتج EN") != null) lbl.Text = rpt.GetCurrentColumnValue("اسم المنتج EN").ToString();
                        else if (exp.Contains("اسم المنتج AR") && rpt.GetCurrentColumnValue("اسم المنتج AR") != null) lbl.Text = rpt.GetCurrentColumnValue("اسم المنتج AR").ToString();
                        else if (exp.Contains("اسم المنتج") && rpt.GetCurrentColumnValue("اسم المنتج") != null) lbl.Text = rpt.GetCurrentColumnValue("اسم المنتج").ToString();

                        if (exp.Contains("المجموعة") && rpt.GetCurrentColumnValue("المجموعة") != null) lbl.Text = rpt.GetCurrentColumnValue("المجموعة").ToString();
                        if (exp.Contains("المادة الفعالة") && rpt.GetCurrentColumnValue("المادة الفعالة") != null) lbl.Text = rpt.GetCurrentColumnValue("المادة الفعالة").ToString();
                        if (exp.Contains("اسم الشركة") && rpt.GetCurrentColumnValue("اسم الشركة") != null) lbl.Text = rpt.GetCurrentColumnValue("اسم الشركة").ToString();
                        
                        if (exp.Contains("سعر البيع") && rpt.GetCurrentColumnValue("سعر البيع") != null)
                            lbl.Text = string.Format("{0:F2}", rpt.GetCurrentColumnValue("سعر البيع"));
                        
                        if (exp.Contains("تاريخ الصلاحية") && rpt.GetCurrentColumnValue("تاريخ الصلاحية") != null)
                        {
                            var dt = rpt.GetCurrentColumnValue("تاريخ الصلاحية");
                            if (dt is DateTime dtm) lbl.Text = dtm.ToString("MM-yyyy");
                        }
                        
                        if (exp.Contains("اسم الصيدلية") && rpt.GetCurrentColumnValue("اسم الصيدلية") != null)
                            lbl.Text = rpt.GetCurrentColumnValue("اسم الصيدلية").ToString();
                        
                        if (exp.Contains("رقم الهاتف") && rpt.GetCurrentColumnValue("رقم الهاتف") != null)
                            lbl.Text = rpt.GetCurrentColumnValue("رقم الهاتف").ToString();
                    }
                    catch { }
                };
            }
        }
    }
}
