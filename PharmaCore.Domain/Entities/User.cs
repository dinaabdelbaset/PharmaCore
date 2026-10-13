using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public string UserCode { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
    }
}
