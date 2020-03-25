using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class File : BaseEntity
    {
        public byte[] Content { get; }

        public File(byte[] content)
        {
            Content = content;
        }

        private File() { } // For EF
    }
}
