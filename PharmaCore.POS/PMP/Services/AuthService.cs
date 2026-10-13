using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PharmaCore.POS.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly IApiClient _apiClient;
        public AuthService(IApiClient apiClient){_apiClient = apiClient;}

        public async Task<bool> LoginAsync(string username, string password)
        {
            var resp = await _apiClient.PostAsync<LoginRequest, LoginResponse>("auth/login", new LoginRequest{UserName=username,Password=password});
            if (resp?.Token == null) return false;
            _apiClient.BearerToken = resp.Token;
            return true;
        }
    }

    public record LoginRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public record LoginResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}