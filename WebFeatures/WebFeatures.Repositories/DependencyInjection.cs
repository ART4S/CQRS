using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Interfaces.DataAccess;

namespace WebFeatures.Repositories
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
        }
    }
}
