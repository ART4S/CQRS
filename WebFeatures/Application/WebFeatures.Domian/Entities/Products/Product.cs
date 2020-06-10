using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities.Products
{
    public class Product : Entity, IHasCreateDate
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid? MainPictureId { get; set; }
        public File MainPicture { get; set; }

        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
