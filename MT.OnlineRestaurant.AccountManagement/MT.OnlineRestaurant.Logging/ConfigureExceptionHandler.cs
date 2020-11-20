using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace MT.OnlineRestaurant.Logging
{
    public static class ConfigureExceptionHandler
    {
        public static void ExceptionMiddlewareExtensions(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //await context.Response.WriteAsync(new Error()
                        //{
                        //    StatusCode = context.Response.StatusCode,
                        //    Message = "Internal Server Error."
                        //}.ToString());
                    }
                });
            });
        }
    }
}
