using System.Security.Claims;

namespace PharmaCore.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims, int minutes = 60);
    }
}
