using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.API.Dtos;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.API.Helper
{
    public class OrderItemPictureResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.ProductUrl))
            {
                return $"{_configuration["APIBaseURL"]}/{source.Product.ProductUrl}";
            }
            return string.Empty;
        }
    }
}

