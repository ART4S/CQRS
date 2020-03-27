using System;
using WebFeatures.Common;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Header { get; private set; }
        public string Body { get; private set; }

        public Review(Guid userId, Guid productId, string header, string body)
        {
            Guard.NotNullOrWhitespace(header, nameof(header));
            Guard.NotNullOrWhitespace(body, nameof(body));

            UserId = userId;
            ProductId = productId;
            Header = header;
            Body = body;
        }

        private Review() { } // For EF

        public void SetHeader(string header)
        {
            Guard.NotNullOrWhitespace(header, nameof(header));
            Header = header;
        }

        public void SetBody(string body)
        {
            Guard.NotNullOrWhitespace(body, nameof(body));
            Body = body;
        }
    }
}
