using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class AuditEvent
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string EventType { get; set; } = string.Empty; // Login, LoginFailed, PermissionDenied, Create, Update, Delete, ...
        public string Category { get; set; } = string.Empty;  // Auth, Settings, Permissions, Products, Sales, ...
        public string? ScreenName { get; set; }                // Frm_Login, Frm_Setting, ...
        public string? TargetTable { get; set; }               // Table/entity affected
        public string? TargetId { get; set; }                  // Record ID affected
        public string Description { get; set; } = string.Empty;
        public int Severity { get; set; }                      // 0=Info, 1=Warning, 2=Error, 3=Critical
        public string? IpAddress { get; set; }
        public string? DeviceName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<AuditDataChange>? DataChanges { get; set; }
    }
}
