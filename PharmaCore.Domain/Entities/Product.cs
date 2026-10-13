using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string ProductCode { get; set; } = string.Empty; // Auto P-00001
        public string NameEn { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string SearchName { get; set; } = string.Empty; // Auto-computed
        public Guid? CategoryId { get; set; }
        public Guid? ActiveIngredientId { get; set; }
        public string? UsageType { get; set; }
        public string? CompanyName { get; set; }
        public bool IsEnglishPrimary { get; set; } = true;
        public decimal TotalSoldQty { get; set; }
        public decimal TotalPurchasedQty { get; set; }
        public decimal TotalReturnedQty { get; set; }
        public string? Notes { get; set; }
        public Organization? Organization { get; set; }
        public ProductCategory? Category { get; set; }
        public ActiveIngredient? ActiveIngredientNav { get; set; }
        public ICollection<ProductBatch>? Batches { get; set; }
    }
}
