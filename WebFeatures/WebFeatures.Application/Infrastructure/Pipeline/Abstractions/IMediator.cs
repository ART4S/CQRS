namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Посредник для отправки запросов подходящим обработчикам
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Направить запрос подходящему обработчику запросов
        /// </summary>
        /// <typeparam name="TResponse">Результат выполнения запроса</typeparam>
        /// <param name="query">Запрос</param>
        /// <returns></returns>
        TResponse SendQuery<TResponse>(IQuery<TResponse> query);

        /// <summary>
        /// Передать команду подходящему обработчику команд
        /// </summary>
        /// <typeparam name="TResponse">Результат выполнения команды</typeparam>
        /// <param name="command">Команда</param>
        /// <returns></returns>
        TResponse SendCommand<TResponse>(ICommand<TResponse> command);
    }
}
