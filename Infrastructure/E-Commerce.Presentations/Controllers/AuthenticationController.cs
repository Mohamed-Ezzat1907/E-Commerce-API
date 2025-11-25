using E_Commerce.Services.Abstractions.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.IdentityDTOs;

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
    }
}
