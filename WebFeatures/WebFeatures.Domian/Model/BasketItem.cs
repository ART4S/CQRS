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

        public int Quantity { get; set; }

        public BasketItem(Basket basket, Product product)
        {
            BasketId = basket.Id;
            Basket = basket;

            ProductId = product.Id;
            Product = product;

            Quantity = 1;
        }

        private BasketItem() { } // For EF
    }
}