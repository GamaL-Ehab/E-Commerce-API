using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controllers
{
    public class BasketsController(IBasketService _basketService) : APIBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketByid(string id)
        {
            var basket = await _basketService.GetBasketAsync(id);

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var basket = await _basketService.CreateBasketAsync(basketDto, TimeSpan.FromDays(1));

            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketDto>> DeleteBasketByid(string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);

            return NoContent();
        }
    }
}
