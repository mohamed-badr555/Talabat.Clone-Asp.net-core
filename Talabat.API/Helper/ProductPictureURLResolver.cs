using AutoMapper;
using Talabat.API.Dtos;
using Talabat.Core.Entities;

namespace Talabat.API.Helper
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureURLResolver(IConfiguration configuration)
        {
          _configuration = configuration;
        }
        public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
         if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["APIBaseURL"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
