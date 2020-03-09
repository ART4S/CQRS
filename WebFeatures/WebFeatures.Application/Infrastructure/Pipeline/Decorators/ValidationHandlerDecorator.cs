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

        public override Task<TResponse> HandleAsync(TRequest request)
        {
            var errors = _validators
                .Select(x => x.Validate(request))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ModelValidationException(errors);
            }

            return Decoratee.HandleAsync(request);
        }
    }
}
