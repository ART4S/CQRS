using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Common;
using WebFeatures.Infrastructure.Common;
using WebFeatures.Infrastructure.DataAccess;
using WebFeatures.Infrastructure.Logging;
using WebFeatures.Infrastructure.Pipeline;

namespace WebFeatures.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddSingleton<IMediator, Mediator>();
            services.AddSingleton<IDateTime, MachineDateTime>();
            services.AddSingleton(typeof(ILogger<>), typeof(LoggerFacade<>));
        }
    }
}
