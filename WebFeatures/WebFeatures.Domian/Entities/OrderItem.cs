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

        public OrderItem(Order order, Product product, int quantity)
        {
            OrderId = order.Id;
            Order = order;

            ProductId = product.Id;
            Product = product;

            Quantity = quantity;
        }

        public OrderItem() { } // For EF
    }
}
