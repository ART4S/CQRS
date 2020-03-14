using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Common;

namespace WebFeatures.WebApi.Filters
{
    public class ExistsInDatabaseFilter<TEntity> : IAsyncActionFilter
        where TEntity : BaseEntity
    {
        private readonly IAsyncRepository<TEntity> _repo;

        public ExistsInDatabaseFilter(IAsyncRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var id) && id is Guid idGuid)
            {
                if (!await _repo.ExistsAsync(idGuid))
                {
                    context.Result = new NotFoundObjectResult(idGuid);
                    return;
                }
            }

            await next();
        }

        public Task OnActionExecutedAsync(ActionExecutedContext context) => Task.CompletedTask;
    }
}
