using E_Commerce.Domain.Entities.BasketModule;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        // Get Basket by Id
        public Task<CustomerBasket?> GetBasketAsync(string id);

        // Update or Create Basket
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? timeToLive = null);

        // Delete Basket by Id
        public Task<bool> DeleteBasketAsync(string id);
    }
}
