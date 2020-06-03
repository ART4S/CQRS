using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebFeatures.WebApi.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void UseAntiforgery(this IApplicationBuilder app)
        {
            IAntiforgery antiforgery = app.ApplicationServices.GetRequiredService<IAntiforgery>();

            app.Use(next => context =>
            {
                antiforgery.GetAndStoreTokens(context);

                return next(context);
            });
        }
    }
}
