using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Supplier : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public decimal Balance { get; set; }
        
        public Organization? Organization { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
    }
}
