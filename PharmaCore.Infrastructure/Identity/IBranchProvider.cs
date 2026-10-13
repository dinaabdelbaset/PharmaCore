namespace PharmaCore.Infrastructure.Identity
{
    public interface IBranchProvider
    {
        string? GetCurrentBranchIdentifier();
        void SetCurrentBranchIdentifier(string id);
    }
}
