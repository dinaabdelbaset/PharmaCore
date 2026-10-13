using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class DrugEyeActiveIngredient : BaseEntity
    {
        public string NameEn { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string? Description { get; set; }
        public ICollection<DrugEyeDrug>? Drugs { get; set; }
    }
}
