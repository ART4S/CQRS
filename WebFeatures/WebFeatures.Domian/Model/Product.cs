using System;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ManufacturerId { get; }
        public Manufacturer Manufacturer { get; }

        public Product(string name, string description, Manufacturer manufacturer)
        {
            Name = name;
            Description = description;

            ManufacturerId = manufacturer.Id;
            Manufacturer = manufacturer;
        }

    }
}
