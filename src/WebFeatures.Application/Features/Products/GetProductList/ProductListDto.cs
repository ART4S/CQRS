using System;

namespace WebFeatures.Application.Features.Products.GetProductList
{
    /// <summary>
    /// Элемент списка товаров
    /// </summary>
    public class ProductListDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal? Price { get; set; }
    }
}