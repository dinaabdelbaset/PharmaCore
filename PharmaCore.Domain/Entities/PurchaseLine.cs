using System;

namespace PharmaCore.Domain.Entities
{
    public sealed class PurchaseLine : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? BatchId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Organization? Organization { get; set; }
        public Purchase? Purchase { get; set; }
        public Product? Product { get; set; }
        public ProductBatch? Batch { get; set; }
    }
}
