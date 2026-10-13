using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;

public sealed class ActivationCode : BaseEntity
{
    public Guid OrgId { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? BranchId { get; set; }

    // الكود اللي هتشوفه وتديه للعميل (الشكل XXXX-XXXXX-XXXX-XXXX)
    public string CodePlain { get; set; } = string.Empty;

    // اختياري: لو حابب تبنيها صح بعدين وتخزن هاش فقط
    public string? CodeHash { get; set; }

    public int MaxDevices { get; set; } = 1;
    public int UsedDevicesCount { get; set; } = 0;

    public DateTime? ExpiresAt { get; set; }

    public Organization? Organization { get; set; }
    public Tenant? Tenant { get; set; }
    public Region? Region { get; set; }
    public Branch? Branch { get; set; }
}
