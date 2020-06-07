using System;
using System.Collections.Generic;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Product : Entity, IHasCreateDate
    {
        public Product()
        {
            ProductFiles = new HashSet<ProductFile>();
        }

        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductFile> ProductFiles { get; private set; }
    }
}
