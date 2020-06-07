using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class File : Entity
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
