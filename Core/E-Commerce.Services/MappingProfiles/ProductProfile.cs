using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Entities.Products;
using Shared.Dtos;

namespace E_Commerce.Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        #region Constructor

        public ProductProfile()
        {
            CreateMap<Product , ProductResultDto>()
                .ForMember(D => D.BrandName , options => options.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.TypeName , options => options.MapFrom(S => S.ProductType.Name));

            CreateMap<ProductBrand , BrandResultDto>();

            CreateMap<ProductType , TypeResultDto>();
        }

        #endregion
    }
}
