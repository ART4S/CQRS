﻿using System;

namespace WebFeatures.Application.Features.Products.Dto
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public Guid? PictureId { get; set; }
    }
}