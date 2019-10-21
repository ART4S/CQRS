using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Threading.Tasks;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.WebApi.Configuration;
using ValidationException = WebFeatures.Application.Infrastructure.Exceptions.ValidationException;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
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
            services.AddAppContext();
            services.AddPipeline();
            services.AddValidators();
            services.AddAutomapper();
            services.AddDataProtection();

            services.AddControllers();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSwaggerDocument();
        }

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = MediaTypeNames.Text.Html;

                    var responseBody = "Внутренняя ошибка сервера";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is ValidationException validationEx)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = MediaTypeNames.Application.Json;

                        responseBody = JsonConvert.SerializeObject(validationEx.Fail);
                    }

                    if (exceptionHandlerPathFeature?.Error is FilteringException filteringEx)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = MediaTypeNames.Text.Html;

                        responseBody = filteringEx.Message;
                    }

                    context.Response.WriteAsync(responseBody);
                    return Task.CompletedTask;
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Register the Swagger generator and the Swagger UI middlewares
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
