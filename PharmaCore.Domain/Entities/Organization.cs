using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Organization : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int MaxTenants { get; set; }
        public int MaxRegions { get; set; }
        public int MaxBranches { get; set; }
        public int MaxDevices { get; set; }
        public string? ActiveAddons { get; set; } = string.Empty;
        public ICollection<Tenant>? Tenants { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Device>? Devices { get; set; }
        public ICollection<Branch>? Branches { get; set; }
    }
}
