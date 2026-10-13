using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [Route("api/global-catalog")]
    [ApiController]
    [Authorize]
    public class GlobalCatalogController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ICurrentTenantContext _tenant;

        public GlobalCatalogController(AppDbContext db, ICurrentTenantContext tenant)
        {
            _db = db;
            _tenant = tenant;
        }

        // ── Search & Alternatives Endpoint ──────────────────────────
        [HttpGet("alternatives")]
        public async Task<IActionResult> GetAlternatives([FromQuery] string name, [FromQuery] string type = "similar")
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Product name is required.");

            // 1. Find the target drug in the global catalog
            var originDrug = await _db.DrugEyeDrugs
                .FirstOrDefaultAsync(d => d.TradeNameEn.ToLower() == name.ToLower() || 
                                          (d.TradeNameAr != null && d.TradeNameAr.ToLower() == name.ToLower()));

            if (originDrug == null || originDrug.ActiveIngredientId == null)
            {
                // Fallback: Return empty list if not found in catalog, UI handles as "Not in DrugEye"
                return Ok(new List<GlobalDrugSearchResultDto>());
            }

            // 2. Fetch alternatives from global catalog
            var results = new List<GlobalDrugSearchResultDto>();

            if (type == "similar")
            {
                // Similar = exact same active ingredient
                var similars = await _db.DrugEyeDrugs
                    .AsNoTracking()
                    .Where(d => d.ActiveIngredientId == originDrug.ActiveIngredientId)
                    .Select(d => new GlobalDrugSearchResultDto
                    {
                        TradeNameEn = d.TradeNameEn,
                        TradeNameAr = d.TradeNameAr,
                        CompanyName = d.CompanyName,
                        Description = d.Description,
                        PublicPrice = d.PublicPrice
                    })
                    .ToListAsync();
                results.AddRange(similars);
            }
            // Can add "near" or "alters" logic using UsageId when applicable

            // 3. Cross-reference with Local Pharmacy Stock
            // Query all local products. If database is huge, we should only query products that match names.
            var localNames = results.Select(r => r.TradeNameEn.ToLower()).ToList();
            
            // Note: In real-world, we'd sync this better, but contains/equals check works for now.
            var localProducts = await _db.Products
                .AsNoTracking()
                .Where(p => p.OrgId == _tenant.OrgId)
                // Filter down to reduce memory footprint
                .Select(p => new
                {
                    p.Id,
                    NameEn = p.NameEn.ToLower(),
                    NameAr = p.NameAr == null ? null : p.NameAr.ToLower(),
                    // Calculate sum of stock across all warehouse locations for this pharmacy
                    TotalStock = p.Batches.SelectMany(b => b.Stocks).Sum(s => s.Quantity)
                })
                .ToListAsync();

            foreach (var res in results)
            {
                // Match by exact or partial english/arabic name
                var match = localProducts.FirstOrDefault(lp => 
                    lp.NameEn == res.TradeNameEn.ToLower() || 
                    (res.TradeNameAr != null && lp.NameAr != null && lp.NameAr == res.TradeNameAr.ToLower()));

                if (match != null)
                {
                    res.HasLocalProduct = true;
                    res.LocalStockQty = match.TotalStock;
                }
            }

            return Ok(results);
        }

        // ── Admin Excel Import Endpoint ─────────────────────────────
        [HttpPost("import")]
        [RequestSizeLimit(100_000_000)] // Allow large excel files
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load existing ingredients and drugs into memory for O(1) lookup
            var existingIngredients = await _db.DrugEyeActiveIngredients
                .ToDictionaryAsync(a => a.NameEn.ToLower(), a => a);
            
            var existingDrugs = await _db.DrugEyeDrugs
                .ToDictionaryAsync(d => d.TradeNameEn.ToLower(), d => d);

            var newDrugs = new List<DrugEyeDrug>();
            var newIngredients = new List<DrugEyeActiveIngredient>();
            
            int addedCount = 0;
            int updatedCount = 0;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Essential for EPPlus

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null) return BadRequest("Excel file is empty.");

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
                            edurg.ActiveIngredient = currentIngredient; // Assign ref directly to rely on EF tracking
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
                }
            }

            // Save new ingredients first
            if (newIngredients.Any())
            {
                _db.DrugEyeActiveIngredients.AddRange(newIngredients);
            }

            // Save new drugs
            if (newDrugs.Any())
            {
                _db.DrugEyeDrugs.AddRange(newDrugs);
            }

            // Commit all changes in one bulk transaction
            await _db.SaveChangesAsync();

            return Ok(new { Message = $"Import successful. Added {addedCount} new drugs, updated {updatedCount} existing drugs.", Success = true });
        }
    }

    public class GlobalDrugSearchResultDto
    {
        public string TradeNameEn { get; set; } = string.Empty;
        public string? TradeNameAr { get; set; }
        public string? CompanyName { get; set; }
        public decimal? PublicPrice { get; set; }
        public string? Description { get; set; }

        // Local Status Flags
        public bool HasLocalProduct { get; set; } = false;
        public decimal LocalStockQty { get; set; } = 0;
    }
}
