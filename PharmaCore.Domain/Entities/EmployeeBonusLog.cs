using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class EmployeeBonusLog : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid BatchId { get; set; }
        public Guid? SaleId { get; set; }
        public decimal QtySold { get; set; }
        public Guid UnitId { get; set; }
        public decimal BonusAmount { get; set; }
        public Organization? Organization { get; set; }
        public Employee? Employee { get; set; }
        public ProductBatch? Batch { get; set; }
    }
}
