﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Blogs.GetBlogsInfo
{
    public class GetBlogInfosQueryHandler : IQueryHandler<GetBlogInfosQuery, IQueryable<BlogInfoDto>>
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;

        public GetBlogInfosQueryHandler(IAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<BlogInfoDto> Handle(GetBlogInfosQuery input)
        {
            var blogs = _context
                .Set<Blog>()
                .AsNoTracking()
                .ProjectTo<BlogInfoDto>(_mapper.ConfigurationProvider);

            return blogs;
        }
    }
}
