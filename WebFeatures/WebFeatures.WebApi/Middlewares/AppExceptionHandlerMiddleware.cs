using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Exceptions;
using WebFeatures.QueryFiltering.Exceptions;

namespace WebFeatures.WebApi.Middlewares
{
    public class AppExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AppExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private Task HandleException(Exception exception, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Text.Plain;

            string responseBody = "Unknown error";

            if (exception is ValidationException validation)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                responseBody = JsonConvert.SerializeObject(validation.Errors);
            }

            if (exception is FilteringException filtering)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                responseBody = filtering.Message;
            }

            return context.Response.WriteAsync(responseBody);
        }
    }
}
