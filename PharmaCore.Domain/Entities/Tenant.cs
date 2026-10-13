using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Tenant : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
        public ICollection<Region>? Regions { get; set; }
    }
}
