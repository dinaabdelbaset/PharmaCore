using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Employee : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string PINHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Grade { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? RoleId { get; set; }
        public string Role { get; set; } = string.Empty; // Legacy role name
        public EmployeeRole? EmployeeRole { get; set; }
        public Organization? Organization { get; set; }
        public Tenant? Tenant { get; set; }
        public Region? Region { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<EmployeeBranchAccess>? BranchAccesses { get; set; }
        public ICollection<EmployeePermission>? Permissions { get; set; }
    }
}
