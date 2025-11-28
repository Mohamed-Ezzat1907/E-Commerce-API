global using ShippingAddress = E_Commerce.Domain.Entities.OrderAggregate.Address;
using AutoMapper;
using E_Commerce.Domain.Entities.OrderAggregate;
using Shared.Dtos.OrderDTOs;

namespace E_Commerce.Services.MappingProfiles
{
    internal class OrderProfile : Profile 
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress , AddressDTO>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>();

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, options => options.MapFrom<OrderPictureResolver>());

            CreateMap<Order, OrderResult>().
                ForMember(d => d.PaymentStatus, options => options.MapFrom(s => s.PaymentStatus))
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total , options => options.MapFrom(s => s.Subtotal + s.DeliveryMethod.Price));
        }
    }
}
