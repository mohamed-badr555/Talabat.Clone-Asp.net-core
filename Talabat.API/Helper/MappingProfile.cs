using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.API.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>().
                ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name)).
                ForMember(d => d.Category, o => o.MapFrom(S => S.Category.Name)).
                ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureURLResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDTo, Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName)).
                ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductUrl, O => O.MapFrom(s => s.Product.ProductUrl))
                .ForMember(d => d.ProductUrl, O => O.MapFrom<OrderItemPictureResolver>());


        }
    }
}
