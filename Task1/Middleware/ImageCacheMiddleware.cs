using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task1.Attributes;
using Task1.Models;
using Task1.Services;
using Task1.Services.Contracts;

namespace Task1.Middleware
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICacheService cacheService)
        {
            var endpoint = context.GetEndpoint();
            var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (controllerActionDescriptor is null)
            {
                await _next(context);
                return;
            }

            var cacheAttribute = (CacheAttribute)controllerActionDescriptor.MethodInfo
                                .GetCustomAttributes(true)
                                .SingleOrDefault(w => w.GetType() == typeof(CacheAttribute));

            if (cacheAttribute is null)
            {
                await _next(context);
                return;
            }

            if(int.TryParse(context.Request.Query["id"], out int id))
            {
                await cacheService.CacheAsync(id);
            }

            await _next(context);
        }
    }
}
