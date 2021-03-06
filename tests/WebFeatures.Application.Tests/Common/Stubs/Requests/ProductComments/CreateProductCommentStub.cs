﻿using Bogus;
using Bogus.Extensions;
using WebFeatures.Application.Features.ProductComments.CreateProductComment;

namespace WebFeatures.Application.Tests.Common.Stubs.Requests.ProductComments
{
    internal class CreateProductCommentStub : Faker<CreateProductCommentCommand>
    {
        public CreateProductCommentStub()
        {
            StrictMode(true);

            RuleFor(x => x.ProductId, x => x.Random.Guid());
            RuleFor(x => x.Body, x => x.Lorem.Sentences());
            RuleFor(x => x.ParentCommentId, x => x.Random.Guid().OrNull(x));
        }
    }
}