using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductDTOs;

namespace E_Commerce.Services.MappingProfiles
{
    internal class PictureResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
               return string.Empty;

            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
