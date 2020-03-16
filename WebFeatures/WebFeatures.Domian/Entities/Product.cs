using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Product : BaseEntity, IUpdatable
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public Guid ManufacturerId { get; }
        public Manufacturer Manufacturer { get; }

        public Guid? CategoryId { get; private set; }
        public Category Category { get; private set; }

        public Guid BrandId { get; }
        public Brand Brand { get; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Product(string name, string description, Manufacturer manufacturer, Brand brand)
        {
            Name = name;
            Description = description;

            ManufacturerId = manufacturer.Id;
            Manufacturer = manufacturer;

            BrandId = brand.Id;
            Brand = brand;
        }

        public void SetCategory(Category category)
        {
            CategoryId = category.Id;
            Category = category;
        }

        private Product() { } // For EF
    }
}
