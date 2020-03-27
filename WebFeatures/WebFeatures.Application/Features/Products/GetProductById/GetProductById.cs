using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.GetProductById
{
    public class GetProductById : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<GetProductById>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.Id).MustAsync(async (x, token) => await db.Products.FindAsync(x) != null);
            }
        }
    }
}
