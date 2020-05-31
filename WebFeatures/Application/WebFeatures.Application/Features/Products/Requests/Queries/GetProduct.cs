using FluentValidation;
using System;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    public class GetProduct : IQuery<ProductInfoDto>
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<GetProduct>
        {
            public Validator(IWriteDbContext db)
            {
                RuleFor(x => x.Id).MustAsync(async (x, t) => await db.Products.ExistsAsync(x));
            }
        }
    }
}
