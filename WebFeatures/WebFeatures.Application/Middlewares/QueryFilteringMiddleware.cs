using LinqToQuerystring;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class QueryFilteringMiddleware<TRequest> : IRequestMiddleware<TRequest, IQueryable>
        where TRequest : IQuery<IQueryable>
    {
        private readonly IRequestFilterService _filterService;

        public QueryFilteringMiddleware(IRequestFilterService filterService)
        {
            _filterService = filterService;
        }

        public async Task<IQueryable> HandleAsync(TRequest request, Func<Task<IQueryable>> next, CancellationToken cancellationToken)
        {
            try
            {
                return (await next()).ApplyQuery(_filterService.GetFilter());
            }
            catch
            {
                throw new ApplicationValidationException("Error while applying filter to response");
            }
        }
    }
}
