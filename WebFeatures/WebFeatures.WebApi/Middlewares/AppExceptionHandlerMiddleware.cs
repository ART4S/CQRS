using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Exceptions;
using WebFeatures.Application.Interfaces;
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
            string responseBody;

            switch (exception)
            {
                case ModelValidationException validation:
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        responseBody = JsonConvert.SerializeObject(validation.Errors);

                        break;
                    }

                case FilteringException filtering:
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;
                        responseBody = filtering.Message;

                        break;
                    }

                case StatusCodeException status:
                    {
                        context.Response.StatusCode = status.StatusCode;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;
                        responseBody = status.Message;

                        break;
                    }

                default:
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;
                        responseBody = "Something went wrong";

                        var logger = context.RequestServices.GetService<ILogger<AppExceptionHandlerMiddleware>>();
                        logger.LogError(exception.Message, exception);

                        break;
                    }
            }

            return context.Response.WriteAsync(responseBody);
        }
    }
}
