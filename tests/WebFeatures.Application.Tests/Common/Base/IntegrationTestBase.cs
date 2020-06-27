using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Accounts.Login;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Application.Middlewares;
using WebFeatures.Application.Tests.Common.Extensions;
using WebFeatures.Application.Tests.Common.Stubs.Services;
using WebFeatures.Infrastructure;
using Xunit;

namespace WebFeatures.Application.Tests.Common.Base
{
    public class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly IServiceProvider ServiceProvider;

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

            services.AddScoped<ICurrentUserService, CustomCurrentUserService>();

            // remove transaction from request's pipeline cause we are using it here
            var transaction = services.First(x => x.ImplementationType == typeof(TransactionMiddleware<,>));

            services.Remove(transaction);

            ServiceProvider = services.BuildServiceProvider();
        }

        public IntegrationTestBase()
        {
            _scope = ServiceProvider.CreateScope();

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

        protected async Task<Guid> AuthenticateAdminAsync()
        {
            return await AuthenticateAsync("admin@mail.com", "12345");
        }

        protected async Task<Guid> AuthenticateAsync(string email, string password)
        {
            var request = new LoginCommand()
            {
                Email = email,
                Password = password
            };

            Guid userId = await Mediator.SendAsync(request);

            var currentUser = (CustomCurrentUserService)_scope.ServiceProvider.GetService<ICurrentUserService>();

            currentUser.UserId = userId;
            currentUser.IsAuthenticated = true;

            return userId;
        }
    }
}