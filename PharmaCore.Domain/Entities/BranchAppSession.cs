using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaCore.Domain.Entities
{
    public sealed class BranchAppSession : BaseEntity
    {
        public Guid BranchAppAccountId { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime LoginAt { get; set; }
        public DateTime? LogoutAt { get; set; }
        public BranchAppAccount? BranchAppAccount { get; set; }
        public Device? Device { get; set; }

        [NotMapped]
        public string? IpAddress { get; set; }
    }
}
