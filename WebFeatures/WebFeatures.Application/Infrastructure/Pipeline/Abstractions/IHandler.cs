namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик
    /// </summary>
    /// <typeparam name="TIn">Тип входного параметра</typeparam>
    /// <typeparam name="TOut">Тип результата</typeparam>
    public interface IHandler<in TIn, out TOut>
    {
        /// <summary>
        /// Метод обработки
        /// </summary>
        /// <param name="input">Входной параметр</param>
        TOut Handle(TIn input);
    }
}
