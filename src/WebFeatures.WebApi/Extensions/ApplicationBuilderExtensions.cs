using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.WebApi.Middlewares;

namespace WebFeatures.WebApi.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static void UseAntiforgery(this IApplicationBuilder app)
        {
            IAntiforgery antiforgery = app.ApplicationServices.GetRequiredService<IAntiforgery>();

            app.Use(next => context =>
            {
                AntiforgeryTokenSet token = antiforgery.GetAndStoreTokens(context);

                context.Response.Cookies.Append(
                    "XSRF-TOKEN",
                    token.RequestToken,
                    new CookieOptions()
                    {
                        IsEssential = true,
                        HttpOnly = false
                    });

                return next(context);
            });
        }
    }
}