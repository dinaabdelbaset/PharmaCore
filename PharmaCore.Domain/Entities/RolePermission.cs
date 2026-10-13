using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class RolePermission : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid RoleId { get; set; }
        public string Permission { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
        public EmployeeRole? Role { get; set; }
    }
}
