using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using PharmaCore.Infrastructure.Entities;
using System;

namespace PharmaCore.LicenseGenerator
{
    internal sealed class SimpleTenantContext : ICurrentTenantContext
    {
        public Guid OrgId { get; }
        public Guid? BranchId { get; }

        public SimpleTenantContext(Guid orgId, Guid? branchId = null)
        {
            OrgId = orgId;
            BranchId = branchId;
        }
    }
}
