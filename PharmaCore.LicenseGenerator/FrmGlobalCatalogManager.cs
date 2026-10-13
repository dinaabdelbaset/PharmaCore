using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

namespace PharmaCore.ActivationManager
{
    public partial class FrmGlobalCatalogManager : Form
    {
        private readonly string _connectionString;

        public FrmGlobalCatalogManager(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
            
            // Set dark theme UI identical to ActivationManager style
            BackColor = Color.FromArgb(30, 30, 46);
            ForeColor = Color.FromArgb(205, 214, 244);
            lblStatus.ForeColor = Color.FromArgb(166, 227, 161);
        }

        private AppDbContext CreateDbGlobal()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(_connectionString)
                .Options;

            return new AppDbContext(options, new MasterTenantContext(Guid.Empty));
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
                lblStatus.Text = "جاري قراءة الملف ورفعه لقاعدة البيانات المركزية... يرجى الانتظار (قد يستغرق 30-60 ثانية).";
                lblStatus.ForeColor = Color.FromArgb(137, 180, 250); // Blue
                Application.DoEvents();

                await ImportViaEFCore(ofd.FileName);

                MessageBox.Show("تم الاستيراد بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "جاهز";
                lblStatus.ForeColor = Color.FromArgb(166, 227, 161); // Green
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الاستيراد:\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "فشل الاستيراد";
                lblStatus.ForeColor = Color.FromArgb(243, 139, 168); // Red
            }
            finally
            {
                btnImportExcel.Enabled = true;
            }
        }

        private async Task ImportViaEFCore(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var db = CreateDbGlobal();

            // 1. Set very long timeout for huge insert
            db.Database.SetCommandTimeout(TimeSpan.FromMinutes(15));
            
            // 2. Load existing ingredients and drugs into memory for O(1) lookup
            var existingIngredients = await db.DrugEyeActiveIngredients
                .ToDictionaryAsync(a => a.NameEn.ToLower(), a => a);
            var existingDrugs = await db.DrugEyeDrugs
                .ToDictionaryAsync(d => d.TradeNameEn.ToLower(), d => d);

            var newDrugs = new List<DrugEyeDrug>();
            var newIngredients = new List<DrugEyeActiveIngredient>();
            
            int addedCount = 0;
            int updatedCount = 0;

            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            using var stream = new MemoryStream(fileBytes);
            using var package = new ExcelPackage(stream);
            
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null) throw new Exception("Excel file is empty.");

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Assume row 1 is headers
            {
                var name = worksheet.Cells[row, 1].Text?.Trim();
                if (string.IsNullOrWhiteSpace(name)) continue;

                var arabic = worksheet.Cells[row, 2].Text?.Trim();
                decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal oldPrice);
                decimal.TryParse(worksheet.Cells[row, 4].Text, out decimal price);
                var active = worksheet.Cells[row, 5].Text?.Trim();
                var company = worksheet.Cells[row, 6].Text?.Trim();
                var description = worksheet.Cells[row, 7].Text?.Trim();

                DrugEyeActiveIngredient? currentIngredient = null;

                // Resolve Active Ingredient
                if (!string.IsNullOrWhiteSpace(active))
                {
                    var activeKey = active.ToLower();
                    if (existingIngredients.TryGetValue(activeKey, out var ing))
                    {
                        currentIngredient = ing;
                    }
                    else
                    {
                        currentIngredient = new DrugEyeActiveIngredient { NameEn = active };
                        newIngredients.Add(currentIngredient);
                        existingIngredients[activeKey] = currentIngredient; // Add to lookup immediately
                    }
                }

                // Check if drug already exists
                if (existingDrugs.TryGetValue(name.ToLower(), out var edurg))
                {
                    edurg.TradeNameAr = arabic;
                    edurg.OldPrice = oldPrice > 0 ? oldPrice : null;
                    edurg.PublicPrice = price > 0 ? price : null;
                    edurg.CompanyName = company;
                    edurg.Description = description;
                    edurg.ActiveIngredient = currentIngredient;
                    updatedCount++;
                }
                else
                {
                    var newDrug = new DrugEyeDrug
                    {
                        TradeNameEn = name,
                        TradeNameAr = arabic,
                        OldPrice = oldPrice > 0 ? oldPrice : null,
                        PublicPrice = price > 0 ? price : null,
                        CompanyName = company,
                        Description = description,
                        ActiveIngredient = currentIngredient
                    };
                    newDrugs.Add(newDrug);
                    addedCount++;
                }
            }

            // Save new ingredients first
            if (newIngredients.Any()) db.DrugEyeActiveIngredients.AddRange(newIngredients);

            // Save new drugs
            if (newDrugs.Any()) db.DrugEyeDrugs.AddRange(newDrugs);

            // Commit all changes in one bulk transaction
            await db.SaveChangesAsync();

            lblStatus.Invoke((MethodInvoker)delegate {
                lblStatus.Text = $"جاري حفظ: أضيف {addedCount} وتم تحديث {updatedCount}. نجاح!";
            });
        }
    }
}
