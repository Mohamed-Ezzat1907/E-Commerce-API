using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Dtos.IdentityDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services.Immplementations
{
    internal class AuthenticationService(UserManager<User> _userManger ,IOptions<JwtOptions> _optionss) : IAuthenticationService
    {
        // Login existing users
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginDTO)
        {
            // Check if there is User with given email
            var user = await _userManger.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                throw new UnAuthorizedException();

            // Check if the password is correct
            var result = await _userManger.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
                throw new UnAuthorizedException();

            // Create and return UserResultDTO
            return new UserResultDTO
            (
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user)
            );
        }

        // Register a new users
        public async Task<UserResultDTO> RegisterAsync(RegisterDTO registerDTO)
        {
           var user = new User
           {
               DisplayName = registerDTO.DisplayName,
               Email = registerDTO.Email,
               UserName = registerDTO.UserName,
               PhoneNumber = registerDTO.PhoneNumber
           };
            var result = await _userManger.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
                
            return new UserResultDTO
            (
                user.DisplayName,
                user.Email!,
                await CreateTokenAsync(user)
            );
        }

        // Create JWT Token
        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = _optionss.Value;

            // clamis 
            // Name , Email , Roles[M-M]
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email),
            };
            var roles = await _userManger.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role,role));

            // Secret Key ===> SymmetricSecurityKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            // Algorithm [Key + Algorithm]
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
                signingCredentials: signingCredentials
                );

            // WriteToken [Object Member Method] ===> JwtSecurityTokenHandler
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

