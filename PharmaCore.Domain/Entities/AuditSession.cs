using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class AuditSession
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime LoginAt { get; set; }
        public DateTime? LogoutAt { get; set; }
        public string? DeviceName { get; set; }
        public string? IpAddress { get; set; }
        public string Status { get; set; } = "Active"; // Active, Closed, Expired
    }
}
