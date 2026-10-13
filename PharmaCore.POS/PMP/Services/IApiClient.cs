using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace PharmaCore.POS.Services
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string relativeUrl);
        Task<TResponse?> PostAsync<TRequest,TResponse>(string relativeUrl, TRequest body);
        Task<bool> DeleteAsync(string relativeUrl);
        string? BearerToken { get; set; }
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

        public ApiClient(HttpClient http)
        {
            _http = http;
            _http.Timeout = TimeSpan.FromSeconds(30);
        }

        public string? BearerToken { get; set; }

        private void ApplyAuth()
        {
            _http.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrWhiteSpace(BearerToken))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            }
        }

        public async Task<T?> GetAsync<T>(string relativeUrl)
        {
            ApplyAuth();
            using var resp = await _http.GetAsync(relativeUrl);
            if (!resp.IsSuccessStatusCode) return default;
            var stream = await resp.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream,_jsonOptions);
        }

        public async Task<TResponse?> PostAsync<TRequest,TResponse>(string relativeUrl, TRequest body)
        {
            ApplyAuth();
            var json = JsonSerializer.Serialize(body,_jsonOptions);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var resp = await _http.PostAsync(relativeUrl, content);
            if (!resp.IsSuccessStatusCode) return default;
            var stream = await resp.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(stream,_jsonOptions);
        }

        public async Task<bool> DeleteAsync(string relativeUrl)
        {
            ApplyAuth();
            using var resp = await _http.DeleteAsync(relativeUrl);
            return resp.IsSuccessStatusCode;
        }
    }
}