using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductController(IProductService productService) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();

            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);

            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync([FromQuery] ProductQueryParameters parameters)
        {
            var products = await productService.GetProductsAsync(parameters);

            return Ok(products);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();

            return Ok(types);
        }
    }
}
