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

        public Order(User user, Shipper shipper, List<OrderItem> items)
        {
            UserId = user.Id;
            User = user;

            ShipperId = shipper.Id;
            Shipper = shipper;

            _items = items;
        }

        public Order() { } // For EF
    }
}
