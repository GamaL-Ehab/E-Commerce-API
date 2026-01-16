using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;

namespace E_Commerce.Services.Abstraction
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();
    }
}
