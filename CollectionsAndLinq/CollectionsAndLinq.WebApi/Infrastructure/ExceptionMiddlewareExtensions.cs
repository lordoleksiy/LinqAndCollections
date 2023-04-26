using CollectionsAndLinq.BLL.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
namespace CollectionsAndLinq.WebApi.Infrastructure;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                    var error = new ErrorDetails();
                    if (contextFeature.Error is ValidationException)
                    {
                        error.StatusCode = (contextFeature.Error as ValidationException).Status; 
                    }
                    else
                    {
                        error.StatusCode = context.Response.StatusCode;
                    }
                    error.Message = contextFeature.Error.Message;
                    context.Response.StatusCode = error.StatusCode;
                    await context.Response.WriteAsync(error.ToString());
                }
            });
        });
    }
}