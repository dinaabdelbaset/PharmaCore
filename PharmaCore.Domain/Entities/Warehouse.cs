using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Warehouse : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
    }
}
