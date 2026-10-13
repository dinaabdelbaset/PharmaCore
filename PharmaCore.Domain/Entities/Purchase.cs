using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Purchase : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string? Notes { get; set; }
        public bool IsPaid { get; set; }

        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public Supplier? Supplier { get; set; }
        public ICollection<PurchaseLine>? Lines { get; set; }
    }
}
