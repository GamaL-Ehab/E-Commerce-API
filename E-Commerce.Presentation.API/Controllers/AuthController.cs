using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controllers
{
    public class AuthController(IAuthService _authService) : APIBaseController
    {
        //Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        //Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        //Check if email exists
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            var result = await _authService.CheckEmailExists(email);
            return Ok(result);
        }

        //Get current user
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _authService.GetCurrentUserAsync(email.Value);
            return Ok(result);
        }
        //Get current user address
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _authService.GetCurrentUserAddressAsync(email.Value);
            return Ok(result);
        }

        //Update current user address
        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto newAddress)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await _authService.UpdateCurrentUserAddressAsync(newAddress, email.Value);
            return Ok(result);
        }
    }
}
