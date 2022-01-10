using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task1.Extensions;
using Task1.ViewModels;

namespace Task1.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            var request = httpContext.Request;
            var controller = request.RouteValues["controller"].ToString();
            var action = request.RouteValues["action"].ToString();
            _logger.LogWarning($"Unhandled exception occured controller: {controller} : action: {action} => {ex.Message}");

            var result = new ViewResult
            {
                ViewName = "Error"                
            };
            
            //var result = new ObjectResult(errorView);
            // TODO: here we should return view
            await httpContext.WriteResultAsync(result);
        }
    }
}
