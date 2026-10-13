using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class PromotionRule : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid PromotionId { get; set; }
        public PromotionConditionType ConditionType { get; set; }
        public Guid? ConditionProductId { get; set; }
        public Guid? ConditionCategoryId { get; set; }
        public Guid? ConditionCollectionId { get; set; }
        public string? ConditionBrand { get; set; }
        public int BuyQty { get; set; }
        public PromotionRewardType RewardType { get; set; }
        public Guid? RewardProductId { get; set; }
        public int RewardQty { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public Organization? Organization { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
