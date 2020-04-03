using WebFeatures.Domian.Attibutes;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    [TableName("Files")]
    public class File : BaseEntity
    {
        public byte[] Content { get; set; }
    }
}
