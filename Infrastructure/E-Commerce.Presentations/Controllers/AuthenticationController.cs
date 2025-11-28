using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.IdentityDTOs;
using Shared.Dtos.OrderDTOs;
using System.Security.Claims;

namespace E_Commerce.Presentations.Controllers
{
    public class AuthenticationController(IServiceManger serviceManger) : ApiControllerBase
    {
        [HttpPost("Login")] // POST: BaseUrl/api/Authentication/Login
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO loginDTO)
            => Ok(await serviceManger.AuthenticationService.LoginAsync(loginDTO));

        [HttpPost("Register")] // POST: BaseUrl/api/Authentication/Register
        public async Task<ActionResult<UserResultDTO>> Register(RegisterDTO registerDTO)
            => Ok(await serviceManger.AuthenticationService.RegisterAsync(registerDTO));

        [HttpGet("EmailExists")] // GET: BaseUrl/api/Authentication/EmailExists
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
            => Ok(await serviceManger.AuthenticationService.CheckEmailExistsAsync(email));

        [Authorize]
        [HttpGet] // GET: BaseUrl/api/Authentication
        public async Task<ActionResult<UserResultDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.AuthenticationService.GetUserByEmailAsync(email!);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")] // GET: BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.AuthenticationService.GetUserAddressAsync(email!);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Address")] // PUT: BaseUrl/api/Authentication/Address
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.AuthenticationService.UpdateUserAddressAsync(addressDTO, email!);
            return Ok(result);
        }
    }
}
