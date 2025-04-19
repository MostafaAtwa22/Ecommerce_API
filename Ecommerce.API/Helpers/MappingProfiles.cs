using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.Core.Models.Identity;

namespace Ecommerce.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>()
                .ReverseMap();

            CreateMap<BasketItem, BasketItemDto>()
                .ReverseMap();

            CreateMap<Address, UserAddressDto>()
                .ReverseMap();

            CreateMap<UserAddressDto, Core.Models.OrderAggregate.Address>()
                .ReverseMap();

        }
    }
}
