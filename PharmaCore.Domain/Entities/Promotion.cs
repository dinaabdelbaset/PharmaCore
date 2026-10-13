using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Promotion : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public PromotionType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? BranchId { get; set; } // null = all branches
        public int Priority { get; set; }
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<PromotionRule>? Rules { get; set; }
    }
}
