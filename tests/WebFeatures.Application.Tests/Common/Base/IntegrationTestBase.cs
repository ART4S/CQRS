using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Extensions;
using WebFeatures.Infrastructure;
using Xunit;

namespace WebFeatures.Application.Tests.Common.Base
{
    public class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly IServiceScopeFactory ScopeFactory;

        protected IWriteDbContext DbContext { get; }

        protected IRequestMediator Mediator { get; }

        private readonly IServiceScope _scope;

        private DbTransaction _transaction;

        static IntegrationTestBase()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddLogging();

            services.AddApplicationServices();

            services.AddInfrastructureServices(configuration);

            // disable transaction from request's pipeline cause we are using it here over each test
            var transaction = services.First(x => x.ImplementationType == typeof(TransactionMiddleware<,>));

            services.Remove(transaction);

            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public IntegrationTestBase()
        {
            _scope = ScopeFactory.CreateScope();

            DbContext = _scope.ServiceProvider.GetService<IWriteDbContext>();
            Mediator = _scope.ServiceProvider.GetService<IRequestMediator>();
        }

        public async Task InitializeAsync()
        {
            _transaction = await DbContext.Connection.BeginTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();

            await _scope.DisposeAsync();
        }
    }
}