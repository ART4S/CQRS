using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.Infrastructure.Jobs;
using WebFeatures.ReadContext;
using WebFeatures.WriteContext;

namespace WebFeatures.Infrastructure
{
    public static class Configure
    {
        public static void UseJobsServer(this IApplicationBuilder app)
        {
            app.UseHangfireServer();

            var settings = app.ApplicationServices.GetService<HangfireSettings>();

            if (settings.EnableDashboard)
            {
                app.UseHangfireDashboard("/jobs", new DashboardOptions()
                {
                    //Authorization = new[] { new DashboardAuthorizationFilter() },
                    //IsReadOnlyFunc = ctx => true,
                    DisplayStorageConnectionString = true,
                });
            }
        }

        public static void ConfigurePersictence(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            EFWriteContext writeDb = scope.ServiceProvider.GetService<EFWriteContext>();

            writeDb.Database.EnsureDeleted();
            writeDb.Database.EnsureCreated();

            MongoDbReadContext readDb = scope.ServiceProvider.GetService<MongoDbReadContext>();

            string readDbName = readDb.Database.DatabaseNamespace.DatabaseName;
            readDb.Database.Client.DropDatabase(readDbName);

            IPasswordEncoder encoder = scope.ServiceProvider.GetService<IPasswordEncoder>();
            IDateTime dateTime = scope.ServiceProvider.GetService<IDateTime>();

            DataSeeder.SeedSampleData(writeDb, readDb, encoder, dateTime);
        }
    }
}
