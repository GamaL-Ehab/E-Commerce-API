using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Specifications;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;

namespace E_Commerce.Service.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            return mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var specs = new ProductSpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specs);

            if (product is null) throw new ProductNotFoundException(id);

            return mapper.Map<ProductDto>(product);
        }

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var specs = new ProductSpecifications(parameters);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);

            var countSpecs = new ProductCountSpecification(parameters);
            var totalCount = await unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpecs);

            var result = mapper.Map<IEnumerable<ProductDto>>(products);

            return new PaginatedResult<ProductDto>(parameters.PageIndex, result.Count(), totalCount, result);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}
