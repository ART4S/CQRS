using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validation)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(validation.Errors));
            }
            catch (FilteringException filtering)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                await context.Response.WriteAsync(filtering.Message);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                await context.Response.WriteAsync("Unknown error");
            }
        }
    }
}
