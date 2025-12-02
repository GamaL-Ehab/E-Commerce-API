using E_Commerce.Shared.Dtos.Auth;

namespace E_Commerce.Services.Abstraction
{
    public interface IAuthService
    {
        Task<UserResponse?> LoginAsync(LoginRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);
    }
}
