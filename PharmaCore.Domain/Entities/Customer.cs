using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public decimal TotalPoints { get; set; }
        public decimal PointsBalance { get; set; }
        public decimal MoneyBalance { get; set; }
        public int VisitCount { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
    }
}
