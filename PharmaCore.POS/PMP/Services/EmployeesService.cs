using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    public record EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? BranchId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? JobTitle { get; set; }
        public string? Grade { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public interface IEmployeesService
    {
        Task<IReadOnlyList<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetAsync(Guid id);
        Task<EmployeeDto?> CreateAsync(EmployeeDto dto);
        Task<bool> UpdateAsync(EmployeeDto dto);
        Task<bool> DeleteAsync(Guid id);
    }

    public class EmployeesService : IEmployeesService
    {
        private readonly HttpClient _http;
        public EmployeesService(HttpClient http) => _http = http;

        public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<IReadOnlyList<EmployeeDto>>("api/employees") ?? Array.Empty<EmployeeDto>();

        public async Task<EmployeeDto?> GetAsync(Guid id)
            => await _http.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");

        public async Task<EmployeeDto?> CreateAsync(EmployeeDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/employees", dto);
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadFromJsonAsync<EmployeeDto>();
        }

        public async Task<bool> UpdateAsync(EmployeeDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/employees/{dto.Id}", dto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resp = await _http.DeleteAsync($"api/employees/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}