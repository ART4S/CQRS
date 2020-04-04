using Hangfire;

namespace WebFeatures.HangfireJobs.Mongo
{
    public static class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration UseMongoStorage(this IGlobalConfiguration configuration)
        {
            // TODO:
            return configuration;
        }
    }
}
