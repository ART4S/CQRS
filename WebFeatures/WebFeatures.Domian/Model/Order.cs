using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class Order : BaseEntity, IUpdatable, ISoftDelete
    {
        public Guid UserId { get; }
        public User User { get; }
        public decimal Total { get; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public IReadOnlyCollection<Product> Products => _products.ToImmutableList();
        private readonly List<Product> _products = new List<Product>();

        public Order(User user, decimal total, List<Product> products)
        {
            UserId = user.Id;
            User = user;

            Total = total;
            _products = products;
        }

        public Order() { } // For EF
    }
}
