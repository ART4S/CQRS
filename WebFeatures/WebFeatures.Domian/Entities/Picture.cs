using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Picture : BaseEntity
    {
        public byte[] Content { get; }

        public Picture(byte[] content)
        {
            Content = content;
        }

        private Picture() { } // For EF
    }
}
