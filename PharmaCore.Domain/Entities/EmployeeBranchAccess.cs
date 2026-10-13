using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class EmployeeBranchAccess : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid BranchId { get; set; }
        public BranchAccessType AccessType { get; set; } = BranchAccessType.Always;
        public DateTime? AllowedFrom { get; set; }
        public DateTime? AllowedTo { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Organization? Organization { get; set; }
        public Employee? Employee { get; set; }
        public Branch? Branch { get; set; }
    }
}
