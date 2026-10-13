using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class StandardItem : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string? ActiveIngredient { get; set; }
        public string? Usage { get; set; }
        public string? Company { get; set; }
        public string SearchIndex { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
    }
}
