using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.Products.Requests.Commands
{
    /// <summary>
    /// Удалить товар
    /// </summary>
    public class DeleteProduct : ICommand<Empty>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<DeleteProduct>
        {
            public Validator(IWriteDbContext db)
            {
                RuleFor(x => x.Id).MustAsync((x, t) => db.Products.ExistsAsync(x));
            }
        }
    }
}
