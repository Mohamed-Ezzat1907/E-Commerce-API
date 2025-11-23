using Shared.Dtos.BasketDTOs;

namespace E_Commerce.Services.Abstractions.Contracts
{
    public interface IBasketService
    {
        // Get Basket By Id
        public Task<BasketDTO> GetBasketAsync(string id);

        // Update or Create Basket
        public Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basketDto);

        // Delete Basket By Id
        public Task<bool> DeleteBasketAsync(string id);
    }
}
