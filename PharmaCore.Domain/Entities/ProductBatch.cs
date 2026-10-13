using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class ProductBatch : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }
        public decimal PharmacyBuyPrice { get; set; }
        public decimal PublicSalePrice { get; set; }
        public bool IsTaxable { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal PriceAfterTax { get; set; }
        public decimal MaxDiscount { get; set; }
        public decimal MinQtyAlert { get; set; }
        public Guid MainUnitId { get; set; }
        public int MainUnitQty { get; set; } = 1; // small units in main unit
        public Guid SaleUnitId { get; set; }
        public Guid PurchaseUnitId { get; set; }
        public Guid? CreatedByEmployeeId { get; set; }
        public Organization? Organization { get; set; }
        public Product? Product { get; set; }
        public ICollection<BatchStock>? Stocks { get; set; }
        public ICollection<BatchUnit>? Units { get; set; }
        public SellerBonus? SellerBonus { get; set; }
    }
}
