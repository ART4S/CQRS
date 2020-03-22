using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; }
        public Order Order { get; }

        public Guid ProductId { get; }
        public Product Product { get; }

        public int Quantity { get; }

        public OrderItem(Guid orderId, Guid productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public OrderItem() { } // For EF
    }
}
