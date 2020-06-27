using System;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.DeleteProduct
{
    /// <summary>
    /// Удалить товар
    /// </summary>
    public class DeleteProductCommand : ICommand<Empty>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}
