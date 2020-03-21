using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                case ValidationException validation:
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        responseBody = JsonConvert.SerializeObject(validation.Errors);

                        break;
                    }

                case UserInfoException user:
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;
                        responseBody = user.Info;

                        break;
                    }

                default:
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;
                        responseBody = "Something went wrong";

                        var logger = context.RequestServices.GetService<ILogger<ExceptionHandlingMiddleware>>();
                        logger.LogError(exception.Message, exception);

                        break;
                    }
            }

            return context.Response.WriteAsync(responseBody);
        }
    }
}
