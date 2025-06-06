﻿using Microsoft.AspNetCore.Builder;

namespace Talabat.API.Extensions
{
    public static class SwaggerServicesExtention
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        public static WebApplication useSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
