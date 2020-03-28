using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.GetProduct
{
    public class GetProduct : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<GetProduct>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.Id)
                    .MustAsync(async (x, t) => await db.Products.FindAsync(x) != null);
            }
        }
    }
}
