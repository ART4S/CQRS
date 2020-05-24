using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class File : IdentityEntity
    {
        public byte[] Content { get; set; }
    }
}
