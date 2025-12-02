using E_Commerce.Presentation.API.Attributes;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductsController(IProductService productService) : APIBaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();

            return Ok(brands);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);

            return Ok(product);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [Cache(60)]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsAsync([FromQuery] ProductQueryParameters parameters)
        {
            var products = await productService.GetProductsAsync(parameters);

            return Ok(products);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();

            return Ok(types);
        }
    }
}
