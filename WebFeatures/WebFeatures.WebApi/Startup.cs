using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Infrastructure.Pipeline.Concerns;
using WebFeatures.Application.Infrastructure.Pipeline.Mediators;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.WebApi.Configuration;
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
            services.AddDbContext<IAppContext, SqlAppContext>();

            services.AddScoped<IMediator, Mediator>();

            AddCommands(services);
            AddQueries(services);
            AddValidators(services);
            AddMapperProfiles(services);

            services.AddDataProtection();
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
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = MediaTypeNames.Text.Plain;

                    var responseBody = "Внутренняя ошибка сервера";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is ValidationException validationEx)
                    {
                        context.Response.StatusCode = 400;
                        responseBody = validationEx.Message;
                    }

                    if (exceptionHandlerPathFeature?.Error is FilteringException filteringEx)
                    {
                        context.Response.StatusCode = 400;
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


        private static void AddCommands(IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(typeof(IHandler<,>).Assembly)
                    .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(SaveChangesHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(ValidationHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(LoggingHandlerDecorator<,>));

            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(PerformanceHandlerDecorator<,>));
        }

        private static void AddQueries(IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(typeof(IHandler<,>).Assembly)
                    .AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(ValidationHandlerDecorator<,>));

            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(LoggingHandlerDecorator<,>));

            services.Decorate(
                typeof(IQueryHandler<,>),
                typeof(PerformanceHandlerDecorator<,>));
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(typeof(IHandler<,>).Assembly)
                    .AddClasses(x => x.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
        }

        private static void AddMapperProfiles(IServiceCollection services)
        {
            var config = new MapperConfiguration(opt =>
            {
                var profiles = typeof(IHandler<,>).Assembly
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(Profile)));

                foreach (var profile in profiles)
                {
                    opt.AddProfile(profile);
                }
            });

            config.AssertConfigurationIsValid();

            services.AddSingleton(provider => config.CreateMapper());
        }
    }
}
