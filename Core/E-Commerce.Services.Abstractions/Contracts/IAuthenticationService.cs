using Shared.Dtos.IdentityDTOs;
using Shared.Dtos.OrderDTOs;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IAuthenticationService
    {
        // login returns a UserResultDTO[DisplayName,Email,Token] ===> take LoginDTO[Email,Password]
        public Task<UserResultDTO> LoginAsync(LoginDTO loginDTO);

        // register returns a UserResultDTO[DisplayName,Email,Token] ===> take RegisterDTO[DisplayName,UserName,Email,Password,PhoneNumber]
        public Task<UserResultDTO> RegisterAsync(RegisterDTO registerDTO);

        // check if email exists returns bool ===> take email string
        public Task<bool> CheckEmailExistsAsync(string email);

        // get current user returns UserResultDTO[DisplayName,Email,Token] ===> no parameters
        public Task<UserResultDTO> GetUserByEmailAsync(string email);

        //Get User Address
        public Task<AddressDTO> GetUserAddressAsync(string email);

        // Update User Address
        public Task<AddressDTO> UpdateUserAddressAsync(AddressDTO addressDTO , string email);
    }
}
