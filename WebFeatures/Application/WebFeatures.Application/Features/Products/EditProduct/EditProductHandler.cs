﻿using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    internal class EditProductHandler : IRequestHandler<EditProduct, Empty>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public EditProductHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Empty> HandleAsync(EditProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.GetAsync(request.Id);

            _mapper.Map(request, product);

            return Empty.Value;
        }
    }
}