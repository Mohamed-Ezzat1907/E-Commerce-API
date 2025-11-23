using E_Commerce.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Api.Middlewares
{
    public class GlobalErrorHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandelingMiddleware> _logger;
        public GlobalErrorHandelingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandelingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                // 404 product Not Found throw exception , 500 Internal Server Error 
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundAsync(httpContext);
            }
            catch (Exception exption) 
            {
                // Log the exception
                _logger.LogError($"Somthing Went Wrong {exption}");
                // Handle the exception and return a custom error response
               await HandelExceptionAsync(httpContext, exption);
            }
        }

        // Handle General Exceptions
        private async Task HandelExceptionAsync(HttpContext httpContext , Exception exception)
        {
            // Set The Response Content Type
            httpContext.Response.ContentType = "application/json";

            // Set The Response Status Code
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.StatusCode = exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
               NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            // Return The Standard Error Response   C# obhect
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = exception.Message,
            }.ToString();

            await httpContext.Response.WriteAsync(response);
            //await httpContext.Response.WriteAsJsonAsync(response);
        }

        // Handle 404 Not Found Errors
        private async Task HandleNotFoundAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The EndPoint With URL: {httpContext.Request.Path} Not Found."
            }.ToString();

            await httpContext.Response.WriteAsync(response);
        }
    }
}
