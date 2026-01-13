using E_Commerce.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class PaymentsController(IPaymentService _paymentService) : APIBaseController
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _paymentService.CreatePaymentIntentAsync(basketId); 
            return Ok(result);
        }
    }
}
