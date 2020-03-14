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

        public Guid? CategoryId { get; }
        public Category Category { get; }

        public Guid BrandId { get; }
        public Brand Brand { get; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Product(string name, string description, Manufacturer manufacturer, Category category, Brand brand)
        {
            Name = name;
            Description = description;

            ManufacturerId = manufacturer.Id;
            Manufacturer = manufacturer;

            CategoryId = category.Id;
            Category = category;

            BrandId = brand.Id;
            Brand = brand;
        }

        private Product() { } // For EF
    }
}
