using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Exceptions;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Infrastructure.Pipeline.Decorators
{
    /// <summary>
    /// Request data validation
    /// </summary>
    public class ValidationHandlerDecorator<TRequest, TResponse> : RequestHandlerDecoratorBase<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationHandlerDecorator(
            IRequestHandler<TRequest, TResponse> decoratee,
            IEnumerable<IValidator<TRequest>> validators) : base(decoratee)
        {
            _validators = validators;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            var errors = (await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request))))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ModelValidationException(errors);
            }

            return await Decoratee.HandleAsync(request);
        }
    }
}
