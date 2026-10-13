using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class PromotionCollectionItem : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid CollectionId { get; set; }
        public Guid ProductId { get; set; }
        public Organization? Organization { get; set; }
        public PromotionCollection? Collection { get; set; }
        public Product? Product { get; set; }
    }
}
