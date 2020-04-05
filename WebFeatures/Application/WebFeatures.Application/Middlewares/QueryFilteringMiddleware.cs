using LinqToQuerystring;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces.Services;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class QueryFilteringMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TResponse : IQueryable
    {
        private readonly IRequestFilterService _filterService;

        public QueryFilteringMiddleware(IRequestFilterService filterService)
        {
            _filterService = filterService;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            try
            {
                return (TResponse)(await next()).ApplyQuery(_filterService.GetFilter());
            }
            catch
            {
                throw new ApplicationValidationException("Error while applying filter to response");
            }
        }
    }
}
