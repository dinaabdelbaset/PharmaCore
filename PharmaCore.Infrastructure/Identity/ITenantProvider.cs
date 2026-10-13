namespace PharmaCore.Infrastructure.Identity
{
    public interface ITenantProvider
    {
        string? GetCurrentTenantIdentifier();
    }
}
