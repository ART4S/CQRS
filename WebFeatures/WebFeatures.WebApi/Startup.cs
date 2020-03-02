using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Mime;
using WebFeatures.Application;
using WebFeatures.Infrastructure;
using WebFeatures.QueryFiltering.Exceptions;
using ValidationException = WebFeatures.Application.Infrastructure.Exceptions.ValidationException;

namespace WebFeatures.WebApi
{
    /// <summary>
    /// Конфигуратор приложения
    /// </summary>
    public class Startup
    {
        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Настройки приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конфигурация сервисов приложения
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Text.Plain;

                    var responseBody = "Внутренняя ошибка сервера";
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (feature?.Error is ValidationException validation)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Application.Json;

                        responseBody = JsonConvert.SerializeObject(validation.Errors);
                    }

                    if (feature?.Error is FilteringException filtering)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = MediaTypeNames.Text.Plain;

                        responseBody = filtering.Message;
                    }

                    return context.Response.WriteAsync(responseBody);
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
