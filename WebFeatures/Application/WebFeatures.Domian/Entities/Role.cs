using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Role : IdentityEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
