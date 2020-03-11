using System;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class Product : BaseEntity, IUpdatable
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }

        public Guid ManufacturerId { get; }
        public Manufacturer Manufacturer { get; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Product(string name, string description, Manufacturer manufacturer)
        {
            Name = name;
            Description = description;

            ManufacturerId = manufacturer.Id;
            Manufacturer = manufacturer;
        }

        private Product() { } // For EF
    }
}
