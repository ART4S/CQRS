namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик запроса
    /// </summary>
    /// <typeparam name="TQuery">Запрос</typeparam>
    /// <typeparam name="TResult">Результат выполнения запроса</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> : IHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
