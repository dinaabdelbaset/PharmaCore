using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class ProductUnit : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Abbreviation { get; set; }
        public Organization? Organization { get; set; }
    }
}
