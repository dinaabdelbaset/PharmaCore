using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace PharmaCore.API.Controllers
{
    [ApiController]
    [Route("api/standard-items")]
    [Authorize]
    public class StandardItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public StandardItemsController(AppDbContext db)
        {
            _db = db;
        }

        private Guid OrgId => Guid.Parse(User.FindFirst("org_id")?.Value ?? throw new UnauthorizedAccessException());

        // GET api/standard-items/search?q=pan*adv
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? q)
        {
            var orgId = OrgId;
            var query = _db.StandardItems.Where(x => x.OrgId == orgId);

            if (!string.IsNullOrWhiteSpace(q))
            {
                var parts = q.Split(new[] { '*', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    var p = part.ToLower();
                    query = query.Where(x => x.SearchIndex.Contains(p));
                }
            }

            var results = await query.Take(100).Select(x => new 
            {
                x.Id,
                x.NameEn,
                x.NameAr,
                x.ActiveIngredient,
                x.Usage,
                x.Company
            }).ToListAsync();

            return Ok(results);
        }

        // POST api/standard-items/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            var orgId = OrgId;
            var itemsToAdd = new List<StandardItem>();

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Assuming header on row 1

                // Helper to prevent DB string truncation errors (max 300 chars)
                string Trunc(string? s, int maxLen = 300) => string.IsNullOrWhiteSpace(s) ? s : (s.Length > maxLen ? s.Substring(0, maxLen) : s);

                // Load existing items for Upsert Strategy, handling duplicates safely
                var existingItemsList = await _db.StandardItems
                    .Where(x => x.OrgId == orgId)
                    .ToListAsync();

                var existingItemsDict = existingItemsList
                    .GroupBy(x => x.NameEn?.ToLower() ?? "")
                    .Where(g => !string.IsNullOrWhiteSpace(g.Key)) // skip entirely empty names
                    .ToDictionary(g => g.Key, g => g.First());

                var newActivesList = new List<string>();
                var newUsagesList = new List<string>();
                var newCompaniesList = new List<string>();

                foreach (var row in rows)
                {
                    var nameEn = Trunc(row.Cell(1).GetString()?.Trim());
                    if (string.IsNullOrWhiteSpace(nameEn)) continue;

                    var nameAr = Trunc(row.Cell(2).GetString()?.Trim());
                    var active = Trunc(row.Cell(3).GetString()?.Trim());
                    var usage = Trunc(row.Cell(4).GetString()?.Trim());
                    var company = Trunc(row.Cell(5).GetString()?.Trim());

                    if (!string.IsNullOrWhiteSpace(active)) newActivesList.Add(active);
                    if (!string.IsNullOrWhiteSpace(usage)) newUsagesList.Add(usage);
                    if (!string.IsNullOrWhiteSpace(company)) newCompaniesList.Add(company);

                    var searchIndex = Trunc($"{nameEn} {nameAr} {active} {company} {usage}".ToLower());
                    var nameEnLower = nameEn.ToLower();

                    if (existingItemsDict.TryGetValue(nameEnLower, out var existingItem))
                    {
                        // Update
                        if (existingItem.NameAr != nameAr || existingItem.ActiveIngredient != active ||
                            existingItem.Usage != usage || existingItem.Company != company)
                        {
                            existingItem.NameAr = nameAr;
                            existingItem.ActiveIngredient = active;
                            existingItem.Usage = usage;
                            existingItem.Company = company;
                            existingItem.SearchIndex = searchIndex ?? string.Empty;
                            existingItem.UpdatedAt = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        // Insert
                        var newItem = new StandardItem
                        {
                            Id = Guid.NewGuid(),
                            OrgId = orgId,
                            NameEn = nameEn,
                            NameAr = nameAr,
                            ActiveIngredient = active,
                            Usage = usage,
                            Company = company,
                            SearchIndex = searchIndex ?? string.Empty,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        };
                        itemsToAdd.Add(newItem);
                        existingItemsDict[nameEnLower] = newItem; // Prevent duplicates within the same sheet
                    }
                }

                int activesAdded = 0, usagesAdded = 0, companiesAdded = 0;
                
                if (itemsToAdd.Any())
                {
                    await _db.StandardItems.AddRangeAsync(itemsToAdd);
                }

                // Process smart lookups
                if (newActivesList.Any() || newUsagesList.Any() || newCompaniesList.Any())
                {
                    var newActives = newActivesList.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                    var newUsages = newUsagesList.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                    var newCompanies = newCompaniesList.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

                    var existingActives = await _db.ActiveIngredients.Where(x => x.OrgId == orgId).Select(x => x.Name).ToListAsync();
                    var existingUsages = await _db.ProductUsages.Where(x => x.OrgId == orgId).Select(x => x.Name).ToListAsync();
                    var existingCompanies = await _db.ProductCompanies.Where(x => x.OrgId == orgId).Select(x => x.Name).ToListAsync();

                    var activesToAdd = newActives.Except(existingActives, StringComparer.OrdinalIgnoreCase)
                        .Select(x => new ActiveIngredient { Id = Guid.NewGuid(), OrgId = orgId, Name = x, CreatedAt = DateTime.UtcNow }).ToList();
                    var usagesToAdd = newUsages.Except(existingUsages, StringComparer.OrdinalIgnoreCase)
                        .Select(x => new ProductUsage { Id = Guid.NewGuid(), OrgId = orgId, Name = x, CreatedAt = DateTime.UtcNow }).ToList();
                    var companiesToAdd = newCompanies.Except(existingCompanies, StringComparer.OrdinalIgnoreCase)
                        .Select(x => new ProductCompany { Id = Guid.NewGuid(), OrgId = orgId, Name = x, CreatedAt = DateTime.UtcNow }).ToList();

                    activesAdded = activesToAdd.Count;
                    usagesAdded = usagesToAdd.Count;
                    companiesAdded = companiesToAdd.Count;

                    if (activesToAdd.Any()) await _db.ActiveIngredients.AddRangeAsync(activesToAdd);
                    if (usagesToAdd.Any()) await _db.ProductUsages.AddRangeAsync(usagesToAdd);
                    if (companiesToAdd.Any()) await _db.ProductCompanies.AddRangeAsync(companiesToAdd);

                    await _db.SaveChangesAsync();
                }

                // If itemsToAdd was 0 (no new items), we should still save changes if we updated existing items!
                if (!itemsToAdd.Any())
                {
                    await _db.SaveChangesAsync();
                }

                return Ok(new { count = itemsToAdd.Count, lookupsAdded = new { actives = activesAdded, usages = usagesAdded, companies = companiesAdded } });
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, msg);
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAll()
        {
            var orgId = OrgId;
            var items = await _db.StandardItems.Where(x => x.OrgId == orgId).ToListAsync();
            _db.StandardItems.RemoveRange(items);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var orgId = OrgId;
            var item = await _db.StandardItems.FirstOrDefaultAsync(x => x.Id == id && x.OrgId == orgId);
            if (item == null) return NotFound();
            
            _db.StandardItems.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel()
        {
            var orgId = OrgId;
            var items = await _db.StandardItems.Where(x => x.OrgId == orgId).OrderBy(x => x.NameEn).ToListAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Standard Items");

            worksheet.Cell(1, 1).Value = "Name EN";
            worksheet.Cell(1, 2).Value = "Name AR";
            worksheet.Cell(1, 3).Value = "Active Ingredient";
            worksheet.Cell(1, 4).Value = "Usage";
            worksheet.Cell(1, 5).Value = "Company";

            var row = 2;
            foreach (var item in items)
            {
                worksheet.Cell(row, 1).Value = item.NameEn;
                worksheet.Cell(row, 2).Value = item.NameAr;
                worksheet.Cell(row, 3).Value = item.ActiveIngredient;
                worksheet.Cell(row, 4).Value = item.Usage;
                worksheet.Cell(row, 5).Value = item.Company;
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StandardItems_Export.xlsx");
        }
    }
}
