﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Infrastructure.Requests
{
    internal abstract class Pipeline<TResponse>
    {
        public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken);
    }

    internal class Pipeline<TRequest, TResponse> : Pipeline<TResponse>
        where TRequest : IRequest<TResponse>
    {
        public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider services, CancellationToken cancellationToken)
        {
            var handler = services.GetService<IRequestHandler<TRequest, TResponse>>();

            RequestDelegate<Task<TResponse>> pipeline =
                () => handler.HandleAsync((TRequest)request, cancellationToken);

            var middlewares = services.GetServices<IRequestMiddleware<TRequest, TResponse>>().Reverse();

            foreach (var middleware in middlewares)
            {
                RequestDelegate<Task<TResponse>> next = pipeline; // for closure
                pipeline = () => middleware.HandleAsync((TRequest)request, next, cancellationToken);
            }

            return pipeline();
        }
    }
}