using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [EntityMetadata("Roles")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
