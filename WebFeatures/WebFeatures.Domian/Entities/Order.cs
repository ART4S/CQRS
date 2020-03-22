using System;
using System.Collections.Generic;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Order : BaseEntity, IUpdatable, ISoftDelete
    {
        public Guid UserId { get; }
        public User User { get; }

        public Guid ShipperId { get; }
        public Shipper Shipper { get; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        private readonly List<OrderItem> _items = new List<OrderItem>();

        public Order(Guid userId, Guid shipperId, List<OrderItem> items)
        {
            UserId = userId;
            ShipperId = shipperId;
            _items = items;
        }

        public Order() { } // For EF
    }
}
