using E_Commerce.Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.OrderDTOs;
using AutoMapper;

namespace E_Commerce.Services.MappingProfiles
{
    internal class OrderPictureResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
