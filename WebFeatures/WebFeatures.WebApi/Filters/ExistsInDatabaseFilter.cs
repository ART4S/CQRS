using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.WebApi.Filters
{
    public class ExistsInDatabaseFilter<TEntity> : IActionFilter 
        where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repo;

        public ExistsInDatabaseFilter(IRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("id", out var id) && id is Guid idGuid)
            {
                if (!_repo.Exists(idGuid))
                {
                    context.Result = new NotFoundObjectResult(idGuid);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
