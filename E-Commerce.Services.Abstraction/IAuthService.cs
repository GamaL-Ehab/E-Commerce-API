using E_Commerce.Shared.Dtos.Auth;

namespace E_Commerce.Services.Abstraction
{
    public interface IAuthService
    {
        Task<UserResponse?> LoginAsync(LoginRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);
        Task<bool> CheckEmailExists(string email);
        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto newAddress, string email);
    }
}
