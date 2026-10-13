using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class PromotionCollection : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
        public ICollection<PromotionCollectionItem>? Items { get; set; }
    }
}
