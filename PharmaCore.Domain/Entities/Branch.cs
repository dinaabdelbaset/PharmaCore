using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Branch : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid RegionId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? NameAr { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phones { get; set; } // Semi-colon separated phone list
        public string? Email { get; set; }
        public string? ManagerName { get; set; }
        public string? ManagerPhone { get; set; }
        public string? ManagerEmail { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Description { get; set; }
        public string? DescriptionAr { get; set; }
        public string? OpeningHours { get; set; }
        public string? EstablishedDate { get; set; }
        public double? TotalArea { get; set; }
        public string Status { get; set; } = "ACTIVE"; // ACTIVE, INACTIVE, PENDING

        public Organization? Organization { get; set; }
        public Region? Region { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Device>? Devices { get; set; }
        public ICollection<BranchAppAccount>? BranchAppAccounts { get; set; }
        public ICollection<EmployeeBranchAccess>? EmployeeBranchAccesses { get; set; }
    }
}
