using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class EmployeeRole : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } // Arabic label e.g. "المدير التنفيذي"
        public int SortOrder { get; set; } // For consistent display ordering
        public Guid? ParentRoleId { get; set; }
        public bool IsSystemRole { get; set; }
        public Organization? Organization { get; set; }
        public EmployeeRole? ParentRole { get; set; }
        public ICollection<EmployeeRole>? ChildRoles { get; set; }
        public ICollection<RolePermission>? Permissions { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
