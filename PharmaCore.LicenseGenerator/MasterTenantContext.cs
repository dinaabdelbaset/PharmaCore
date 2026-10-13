using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;

namespace PharmaCore.ActivationManager
{
    internal sealed class MasterTenantContext : ICurrentTenantContext
    {
        public Guid OrgId { get; }
        public Guid? BranchId { get; } = null;

        public MasterTenantContext(Guid orgId)
        {
            OrgId = orgId;
        }
    }
}
