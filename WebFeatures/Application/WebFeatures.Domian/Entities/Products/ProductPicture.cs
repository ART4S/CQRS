using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities.Products
{
    public class ProductPicture : Entity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
