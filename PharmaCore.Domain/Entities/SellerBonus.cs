using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class SellerBonus : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid BatchId { get; set; }
        public decimal BonusPerMainUnit { get; set; }
        public bool IsPercentage { get; set; } // true = % of sale price, false = fixed amount
        public Organization? Organization { get; set; }
        public ProductBatch? Batch { get; set; }
    }
}
