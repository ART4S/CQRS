using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Common.Extensions;
using WebFeatures.Infrastructure.DataAccess.Contexts;
using WebFeatures.Infrastructure.DataAccess.Factories;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;
using WebFeatures.Infrastructure.Events;
using WebFeatures.Infrastructure.Files;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Requests;
using WebFeatures.Infrastructure.Security;
using WebFeatures.Infrastructure.Services;
using WebFeatures.Persistence;

namespace WebFeatures.Infrastructure
{
    public static class DI
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddLogging(services);
            AddDataAccess(services, configuration);
            AddSecurity(services);
            AddEvents(services);
            AddRequests(services);
            AddFiles(services);
            AddServices(services);
        }

        private static void AddLogging(IServiceCollection services)
        {
            services.AddScoped(typeof(ILogger<>), typeof(LoggerAdapter<>));
            services.AddSingleton(typeof(ILoggerFactory), typeof(LoggerFactory));
        }

        private static void AddDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddSingleton<IDbConnectionFactory>(x => new PostgreSqlDbConnectionFactory(connectionString));
            services.AddSingleton<IEntityProfile>(x =>
            {
                var profile = new EntityProfile();

                profile.RegisterMappingsFromAssembly(Assembly.GetExecutingAssembly());

                return profile;
            });
            services.AddScoped<IWriteDbContext, WriteDbContext>();

            services.AddScoped<IReadDbContext, ReadDbContext>();
        }

        private static void AddSecurity(IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
        }

        private static void AddEvents(IServiceCollection services)
        {
            services.AddScoped<IEventMediator, EventMediator>();

            services.RegisterTypesFromAssembly(Assembly.GetExecutingAssembly())
                .Where(x => x.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .As(x => x.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                .WithLifetime(ServiceLifetime.Scoped);
        }

        private static void AddRequests(IServiceCollection services)
        {
            services.AddScoped<IRequestMediator, RequestMediator>();
        }

        private static void AddFiles(IServiceCollection services)
        {
            services.AddSingleton<IFileReader, FileReader>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}