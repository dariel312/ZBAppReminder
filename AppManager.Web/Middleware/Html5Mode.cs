using AppManager.Web.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppManager.Web.Middleware
{
    public class Html5Mode : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
            if (context.Response.StatusCode == 404 &&
               !Path.HasExtension(context.Request.Path.Value) &&
               !context.Request.Path.Value.StartsWith("/api/"))
            {
                context.Request.Path = "/";
                context.Response.StatusCode = 200;
                await next(context);
            }
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SOAPEndpointExtensions
    {
        /// <summary>
        /// Enables HTML5 mode using <see cref="FilmSilo.Web.Middleware.Html5mode"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHtml5Mode(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppManager.Web.Middleware.Html5Mode>();
        }



        public static IServiceCollection AddHtml5Mode(this IServiceCollection service)
        {
            return service.AddTransient<Html5Mode>();
        }
    }
}