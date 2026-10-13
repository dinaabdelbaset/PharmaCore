using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Region : BaseEntity
    {
        public Guid TenantId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Tenant? Tenant { get; set; }
        public ICollection<Branch>? Branches { get; set; }
    }
}
