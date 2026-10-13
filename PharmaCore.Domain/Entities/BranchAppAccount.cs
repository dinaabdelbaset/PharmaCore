using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class BranchAppAccount : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid BranchId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int MaxConcurrentSessions { get; set; } = 1;
        public int FailedCount { get; set; }
        public DateTime? LockoutUntil { get; set; }
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<BranchAppSession>? Sessions { get; set; }
    }
}
