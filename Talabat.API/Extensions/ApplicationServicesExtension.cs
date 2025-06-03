using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUnitofWork), typeof(UnitOfwork));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
           services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            // Register ProductPictureURLResolver
            services.AddTransient<ProductPictureURLResolver>();
            services.AddTransient<OrderItemPictureResolver>();
            services.Configure<ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0).
                    SelectMany(P => P.Value.Errors).Select(E => E.ErrorMessage).ToArray();
                var validationErrorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(validationErrorResponse);
            }
            );

            return services;
        }
    }
}
