using System;
using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model
{
    public class BasketItem : BaseEntity
    {
        public Guid BasketId { get; }
        public Basket Basket { get; }

        public Guid ProductId { get; }
        public Product Product { get; }

        public int Quantity { get; private set; }

        public BasketItem(Basket basket, Product product)
        {
            BasketId = basket.Id;
            Basket = basket;

            ProductId = product.Id;
            Product = product;

            Quantity = 1;
        }

        private BasketItem() { } // For EF

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;

            if (Quantity < 1)
                throw new Exception($"BasketItemId: {Id}. Quantity shouldn't be less than 1");
        }
    }
}