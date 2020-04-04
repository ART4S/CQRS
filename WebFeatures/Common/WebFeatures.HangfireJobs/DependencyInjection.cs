using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WebFeatures.HangfireJobs.Internal;

namespace WebFeatures.HangfireJobs
{
    public static class DependencyInjection
    {
        public static void AddHangfireServices(this IServiceCollection services, Action<IServiceProvider, IGlobalConfiguration> configAction)
        {
            services.AddSingleton<IBackgroundJobManager, BackgroundJobManager>();
            services.AddHangfire(configAction);
        }

        public static void AddHangfireJobs(this IServiceCollection services, Assembly assemblyToScan)
        {

        }
    }
}
