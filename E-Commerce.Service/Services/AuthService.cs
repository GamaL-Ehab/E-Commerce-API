using AutoMapper;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Domain.Exceptions.Unauthorized;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Service.Services
{
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> options, IMapper _mapper) : IAuthService
    {
        public async Task<bool> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;            
        }

        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null) throw new UserNotFoundException(email);

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto newAddress, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user is null) throw new UserNotFoundException(email);

            if(user.Address is null)
            {
                //Create new address
                user.Address = _mapper.Map<Address>(newAddress);
            }
            else
            {
                //Update current address
                user.Address.FirstName = newAddress.FirstName;
                user.Address.LastName = newAddress.LastName;
                user.Address.Street = newAddress.Street;
                user.Address.City = newAddress.City;
                user.Address.Country = newAddress.Country;            
            }

            await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            var isCorrectPass = await _userManager.CheckPasswordAsync(user ,request.Password);
            if (!isCorrectPass) throw new UnauthorizedException();

            return new UserResponse()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                DisplayName = request.DisplayName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegisterationBadRequestException(result.Errors.Select(err => err.Description).ToList());

            return new UserResponse()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }

        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) 
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtOptions = options.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(jwtOptions.DurationDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
