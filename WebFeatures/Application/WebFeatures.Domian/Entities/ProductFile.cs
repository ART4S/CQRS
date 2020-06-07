using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class ProductFile : Entity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
