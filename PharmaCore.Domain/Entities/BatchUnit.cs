using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class BatchUnit : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid BatchId { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int QtyPerMainUnit { get; set; }
        public decimal UnitSalePrice { get; set; }
        public Organization? Organization { get; set; }
        public ProductBatch? Batch { get; set; }
    }
}
