using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;

namespace WebFeatures.Application.Features.Products.GetProduct
{
    public class GetProduct : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<GetProduct>
        {
            public Validator(IDbContext db)
            {
                RuleFor(x => x.Id).MustAsync(async (x, t) => await db.Products.ExistsAsync(x));
            }
        }
    }
}
