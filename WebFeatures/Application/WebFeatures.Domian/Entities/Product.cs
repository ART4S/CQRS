using System;
using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Domian.Entities
{
    [EntityMetadata("Products")]
    public class Product : BaseEntity, IHasCreateDate
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public UserRating? AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid? PictureId { get; set; }
        public File Picture { get; set; }

        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
