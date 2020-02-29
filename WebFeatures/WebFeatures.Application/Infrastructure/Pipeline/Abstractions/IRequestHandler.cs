namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик запросов
    /// </summary>
    /// <typeparam name="TRequest">Тип входного параметра</typeparam>
    /// <typeparam name="TResponse">Тип результата</typeparam>
    public interface IRequestHandler<in TRequest, out TResponse>
    {
        /// <summary>
        /// Метод обработки
        /// </summary>
        /// <param name="request">Входной параметр</param>
        TResponse Handle(TRequest request);
    }
}
