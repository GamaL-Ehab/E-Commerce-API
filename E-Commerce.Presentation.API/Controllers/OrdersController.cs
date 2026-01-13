using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Presentation.API.Controllers
{
    public class OrdersController(IOrderService _orderService) : APIBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _orderService.CreateOrderAsync(request, userEmail.Value);

            return Ok(result);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodsAsync();

            return Ok(result);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllUserOrders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _orderService.GetAllOrdersForSpecificUserAsync(userEmail.Value);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var result = await _orderService.GetOrderByIdForSpecificUserAsync(id, userEmail.Value);

            return Ok(result);
        }
    }
}
