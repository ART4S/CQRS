using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class SaveChangesMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IDbContext _db;

        public SaveChangesMiddleware(IDbContext db)
        {
            _db = db;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            _db.SaveChanges();

            return response;
        }
    }
}
