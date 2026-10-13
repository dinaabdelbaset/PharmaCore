using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class BatchStock : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid BatchId { get; set; }
        public Guid BranchId { get; set; }
        public Guid? WarehouseId { get; set; }
        public decimal Quantity { get; set; }
        public Organization? Organization { get; set; }
        public ProductBatch? Batch { get; set; }
        public Branch? Branch { get; set; }
        public Warehouse? WarehouseNav { get; set; }
    }
}
