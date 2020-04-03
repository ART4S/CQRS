using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [TableName("Brands")]
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
    }
}
