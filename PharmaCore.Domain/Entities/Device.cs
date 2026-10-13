using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Device : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public string MachineId { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime ActivatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<BranchAppSession>? Sessions { get; set; }
    }
}
