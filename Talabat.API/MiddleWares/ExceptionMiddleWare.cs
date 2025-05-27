using System.Net;
using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;
        //                    Delegete to the next invoke of middleware
        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        //                    This method is called when the middleware is invoked
        // Context of the same request at the same time
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                //log Exception in Database [Productions]


                //header
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var response = env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase

                };
                var json = JsonSerializer.Serialize(response, options);
                //
                await context.Response.WriteAsync(json);
            }
        }
    }
}
