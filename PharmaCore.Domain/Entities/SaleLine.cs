using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class SaleLine : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid SaleId { get; set; }
        public int LineNumber { get; set; }
        public Guid ProductId { get; set; }
        public Guid BatchId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public Guid UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal StockQtyAtSale { get; set; }
        public decimal PriceBeforeTax { get; set; }
        public decimal TaxValue { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LineTotal { get; set; }
        public decimal LineTotalAfterDiscount { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Organization? Organization { get; set; }
        public Sale? Sale { get; set; }
        public Product? Product { get; set; }
        public ProductBatch? Batch { get; set; }
    }
}
