using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PharmaCore.POS.Helpers
{
    public static class ExportHelper
    {
        public static void ExportToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xls)|*.xls";
                sfd.FileName = "Export_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">");
                        sb.AppendLine("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head>");
                        sb.AppendLine("<body><table border=\"1\">");

                        // Headers
                        sb.AppendLine("<tr>");
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                sb.AppendLine("<th style=\"background-color:#0f52ba;color:white;font-weight:bold;\">" + col.HeaderText + "</th>");
                            }
                        }
                        sb.AppendLine("</tr>");

                        // Rows
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.IsNewRow) continue;
                            sb.AppendLine("<tr>");
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.OwningColumn.Visible)
                                {
                                    sb.AppendLine("<td>" + (cell.Value?.ToString() ?? "") + "</td>");
                                }
                            }
                            sb.AppendLine("</tr>");
                        }

                        sb.AppendLine("</table></body></html>");
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("تم تصدير الملف إلى Excel بنجاح!", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ أثناء التصدير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public static void ExportToPdf(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات لتصديرها!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "HTML Files (*.html)|*.html";
                sfd.FileName = "Export_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("<html><head><meta charset=\"utf-8\"><title>تقرير تفاصيل الفاتورة</title>");
                        sb.AppendLine("<style>");
                        sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Arial; direction: rtl; padding: 20px; }");
                        sb.AppendLine("table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
                        sb.AppendLine("th, td { border: 1px solid #ccc; padding: 10px; text-align: center; }");
                        sb.AppendLine("th { background-color: #333; color: white; }");
                        sb.AppendLine("h2 { color: #333; text-align: center; }");
                        sb.AppendLine("</style></head><body>");
                        sb.AppendLine("<h2>تقرير تفاصيل الفاتورة</h2>");
                        sb.AppendLine("<p>تاريخ التصدير: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</p>");
                        sb.AppendLine("<table>");

                        // Headers
                        sb.AppendLine("<tr>");
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (col.Visible)
                            {
                                sb.AppendLine("<th>" + col.HeaderText + "</th>");
                            }
                        }
                        sb.AppendLine("</tr>");

                        // Rows
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.IsNewRow) continue;
                            sb.AppendLine("<tr>");
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.OwningColumn.Visible)
                                {
                                    sb.AppendLine("<td>" + (cell.Value?.ToString() ?? "") + "</td>");
                                }
                            }
                            sb.AppendLine("</tr>");
                        }

                        sb.AppendLine("</table></body></html>");
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("تم تصدير التقرير بنجاح! يمكنك فتحه وطباعته أو حفظه كـ PDF بسهولة من المتصفح.", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ أثناء التصدير: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
