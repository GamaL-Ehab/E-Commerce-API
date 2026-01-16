using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.Dtos.Products;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        public ProductSpecifications(ProductQueryParameters queryParameters)
            : base(CreateCriteria(queryParameters))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            ApplyPagination(queryParameters.PageSize, queryParameters.PageIndex);

            switch (queryParameters.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(x => x.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(x => x.Name);
                    break;
                case ProductSortingOptions.PriceAsc
                    :
                    AddOrderBy(x => x.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Id);
                    break;
            }
        }
        public ProductSpecifications(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }

        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return x => (!parameters.BrandId.HasValue || x.BrandId == parameters.BrandId.Value)
                     && (!parameters.TypeId.HasValue || x.TypeId == parameters.TypeId.Value)
                     && (string.IsNullOrWhiteSpace(parameters.Search) || x.Name.Contains(parameters.Search));
        }
    }
}
