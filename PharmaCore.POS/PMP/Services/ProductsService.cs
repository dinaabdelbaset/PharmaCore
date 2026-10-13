using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaCore.POS.Services
{
    public interface IProductsService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
    }

    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductsService : IProductsService
    {
        private readonly IApiClient _api;
        public ProductsService(IApiClient api){_api = api;}

        public Task<IReadOnlyList<ProductDto>> GetAllAsync() => _api.GetAsync<IReadOnlyList<ProductDto>>("products");
        public Task<ProductDto?> GetByIdAsync(int id) => _api.GetAsync<ProductDto>($"products/{id}");
    }
}