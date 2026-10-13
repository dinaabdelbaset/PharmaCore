using System;

namespace PharmaCore.POS.Services.Security
{
    internal readonly record struct BranchContext(Guid OrgId, Guid TenantId, Guid RegionId, Guid BranchId, string? BranchCode)
    {
        public bool IsValid => OrgId != Guid.Empty && TenantId != Guid.Empty && RegionId != Guid.Empty && BranchId != Guid.Empty;

        public static bool TryLoad(out BranchContext ctx)
        {
            ctx = default;
            var meta = ActivationMetadataStorage.Load();
            if (meta == null) return false;
            ctx = new BranchContext(meta.OrgId, meta.TenantId, meta.RegionId, meta.BranchId, meta.BranchCode);
            return ctx.IsValid;
        }

        public static BranchContext LoadRequired()
        {
            if (!TryLoad(out var ctx))
                throw new InvalidOperationException("بيانات التفعيل غير مكتملة (Org/Tenant/Region/Branch)");
            return ctx;
        }

        /// <summary>
        /// Defense-in-depth: ensures the activation metadata represents a full chain.
        /// This prevents accidental cross-branch usage if activation storage becomes inconsistent.
        /// </summary>
        public static void ValidateOrThrow()
        {
            _ = LoadRequired();
        }
    }
}
