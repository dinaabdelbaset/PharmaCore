using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class ActiveIngredient : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
    }
}
