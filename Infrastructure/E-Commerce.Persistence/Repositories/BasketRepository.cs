using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        #region Fields

        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        #endregion

        #region Methods

        // Update or Create Basket
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);

            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(30));

            return IsCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
        }

        // Delete Basket by Id
        public async Task<bool> DeleteBasketAsync(string id)
            => await _database.KeyDeleteAsync(id);

        // Get Basket by Id
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);

            if(value.IsNullOrEmpty) 
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(value!);
        } 

        #endregion
    }
}
