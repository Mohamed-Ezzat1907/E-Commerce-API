using Shared.Dtos.IdentityDTOs;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IAuthenticationService
    {
        // login returns a UserResultDTO[DisplayName,Email,Token] ===> take LoginDTO[Email,Password]
        public Task<UserResultDTO> LoginAsync(LoginDTO loginDTO);

        // register returns a UserResultDTO[DisplayName,Email,Token] ===> take RegisterDTO[DisplayName,UserName,Email,Password,PhoneNumber]
        public Task<UserResultDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
