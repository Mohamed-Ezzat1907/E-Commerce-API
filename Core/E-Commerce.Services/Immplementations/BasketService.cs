using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Services.Abstractions.Contracts;
using Shared.Dtos.BasketDTOs;

namespace E_Commerce.Services.Immplementations
{
    public class BasketService(IBasketRepository basketRepository , IMapper mapper) : IBasketService
    {
        // create or update basket
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basketDto)
        {
            var customMapper = mapper.Map<CustomerBasket>(basketDto);
            var createdOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(customMapper);

            return createdOrUpdatedBasket is null ?
                throw new Exception("Can Not Create Or Updated Basket.") : mapper.Map<BasketDTO>(createdOrUpdatedBasket);
        }

        // delete basket
        public async Task<bool> DeleteBasketAsync(string id)
            => await basketRepository.DeleteBasketAsync(id);

        // get basket by id
        public async Task<BasketDTO> GetBasketAsync(string id)
        {
           var basket = await basketRepository.GetBasketAsync(id);

            return basket is null ?
                throw new BasketNotFoundException(id) : mapper.Map<BasketDTO>(basket);
        }
    }
}
