using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class DrugEyeDrug : BaseEntity
    {
        public string TradeNameEn { get; set; } = string.Empty; // Mapped from 'name'
        public string? TradeNameAr { get; set; } // Mapped from 'arabic'
        public string? CompanyName { get; set; } // Mapped from 'company'
        public decimal? PublicPrice { get; set; } // Mapped from 'price'
        public decimal? OldPrice { get; set; } // Mapped from 'oldprice'
        public string? Description { get; set; } // Mapped from 'description'
        
        public Guid? ActiveIngredientId { get; set; } // Mapped from 'active'
        public Guid? UsageId { get; set; }
        
        public DrugEyeActiveIngredient? ActiveIngredient { get; set; }
        public DrugEyeUsage? Usage { get; set; }
    }
}
