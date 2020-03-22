using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Exceptions;

namespace WebFeatures.Domian.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; }
        public Basket Basket { get; }

        public Guid ProductId { get; }
        public Product Product { get; }

        public int Quantity { get; private set; }

        public BasketItem(Guid basketId, Guid productId)
        {
            BasketId = basketId;
            ProductId = productId;
            Quantity = 1;
        }

        private BasketItem() { } // For EF

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;

            if (Quantity < 1)
                throw new DomianValidationException($"BasketItemId: {Id}. Quantity shouldn't be less than 1");
        }
    }
}