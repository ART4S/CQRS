using System;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.Domian.Common;

namespace WebFeatures.Domian.Entities
{
    public class Basket : BaseEntity
    {
        public Guid UserId { get; }
        public User User { get; }

        public IReadOnlyCollection<BasketItem> BasketItems => _basketItems.AsReadOnly();
        private readonly List<BasketItem> _basketItems = new List<BasketItem>();

        public Basket(Guid userId)
        {
            UserId = userId;
        }

        private Basket() { } // For EF

        public void AddProduct(Guid productId)
        {
            BasketItem existingItem = _basketItems.SingleOrDefault(x => x.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.AddQuantity(1);
                return;
            }

            _basketItems.Add(new BasketItem(Id, productId));
        }
    }
}
