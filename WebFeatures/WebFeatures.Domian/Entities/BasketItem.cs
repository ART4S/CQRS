using System;
using WebFeatures.Domian.Common;
using WebFeatures.Domian.Exceptions;

namespace WebFeatures.Domian.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

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