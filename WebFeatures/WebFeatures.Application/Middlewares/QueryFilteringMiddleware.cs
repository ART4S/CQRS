using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Exceptions;
using WebFeatures.Application.Interfaces;
using WebFeatures.QueryFilters;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    public class QueryFiltersMiddleware<TRequest, TResponse> : IQueryMiddleware<TRequest, TResponse>
    {
        private readonly IRequestFilterService _filterService;

        public QueryFiltersMiddleware(IRequestFilterService filterService)
        {
            _filterService = filterService;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var response = await next(request);

            if (string.IsNullOrWhiteSpace(_filterService.Filter) || !(response is IQueryable queryable))
                return response;

            try
            {
                return (TResponse)queryable.ApplyQuery(_filterService.Filter);
            }
            catch
            {
                throw new StatusCodeException("Error while applying filter to response");
            }
        }
    }
}
