using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [TableName("Categories")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}
