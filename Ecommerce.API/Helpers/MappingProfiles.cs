using AutoMapper;
using Ecommerce.API.Dtos;
using Ecommerce.Core.Models.Identity;
using Ecommerce.Core.Models.OrderAggregate;

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

            CreateMap<CreateProductDto, Product>()
                .ForMember(d => d.PictureUrl, o => o.Ignore())
                .ReverseMap();

            CreateMap<ProductBrandDto, ProductBrand>()
                .ReverseMap();

            CreateMap<ProductTypeDto, ProductType>()
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>()
                .ForMember(d => d.PictureUrl, o => o.Ignore())
                .ReverseMap();

            CreateMap<CustomerBasket, CustomerBasketDto>()
                .ReverseMap();

            CreateMap<BasketItem, BasketItemDto>()
                .ReverseMap();

            CreateMap<Core.Models.Identity.Address, UserAddressDto>()
                .ReverseMap();

            CreateMap<UserAddressDto, Core.Models.OrderAggregate.Address>()
                .ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>())
                .ReverseMap();
        }
    }
}
