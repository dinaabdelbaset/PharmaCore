using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class EmployeePermission : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Permission { get; set; } = string.Empty; // e.g. "sales.create"
        public Organization? Organization { get; set; }
        public Employee? Employee { get; set; }
    }
}
