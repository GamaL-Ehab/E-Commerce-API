using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.Dtos.Products;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications
{
    public class ProductCountSpecification : BaseSpecification<Product>
    {
        public ProductCountSpecification(ProductQueryParameters parameters) : base(CreateCriteria(parameters))
        {
        }
        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return x => (!parameters.BrandId.HasValue || x.BrandId == parameters.BrandId.Value)
                     && (!parameters.TypeId.HasValue || x.TypeId == parameters.TypeId.Value)
                     && (string.IsNullOrWhiteSpace(parameters.Search) || x.Name.Contains(parameters.Search));
        }
    }
}
