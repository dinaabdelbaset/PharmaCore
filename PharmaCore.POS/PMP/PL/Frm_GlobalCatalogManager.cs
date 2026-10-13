using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmaCore.POS.Services;

namespace PharmaCore.POS.PL
{
    public partial class Frm_GlobalCatalogManager : DevExpress.XtraEditors.XtraForm
    {
        public Frm_GlobalCatalogManager()
        {
            InitializeComponent();
        }

        private async void btnImportExcel_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "اختر ملف كتالوج الأدوية"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                btnImportExcel.Enabled = false;
                lblStatus.Text = "جاري رفع الملف ومعالجته بالمخدم السحابي... يرجى الانتظار (قد يستغرق دقائق).";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                using var http = ConnectionManager.GetHttpClient();
                // We set a long timeout for 26k rows over network
                http.Timeout = TimeSpan.FromMinutes(10); 

                using var content = new MultipartFormDataContent();
                await using var fileStream = File.OpenRead(ofd.FileName);
                var fileContent = new StreamContent(fileStream);
                content.Add(fileContent, "file", Path.GetFileName(ofd.FileName));

                var res = await http.PostAsync("api/global-catalog/import", content);
                var resBody = await res.Content.ReadAsStringAsync();

                if (res.IsSuccessStatusCode)
                {
                    XtraMessageBox.Show("تم الاستيراد بنجاح!\n" + resBody, "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "جاهز";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    XtraMessageBox.Show("حدث خطأ أثناء الاستيراد:\n" + resBody, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "فشل الاستيراد";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("حدث خطأ:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "حدث خطأ غير متوقع";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                btnImportExcel.Enabled = true;
            }
        }
    }
}
