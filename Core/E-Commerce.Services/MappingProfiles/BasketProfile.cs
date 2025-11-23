using AutoMapper;
using E_Commerce.Domain.Entities.BasketModule;
using Shared.Dtos.BasketDTOs;

namespace E_Commerce.Services.MappingProfiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDTO,CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDTO,BasketItem>().ReverseMap();
        }
    }
}
