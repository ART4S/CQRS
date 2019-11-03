﻿using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Infrastructure.Pipeline.Mediators
{
    /// <summary>
    /// Посредник для отправки запросов подходящим обработчикам
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Передать запрос обработчику запросов
        /// </summary>
        /// <typeparam name="TOut">Результат выполнения запроса</typeparam>
        /// <param name="query">Запрос</param>
        /// <returns></returns>
        TOut SendQuery<TOut>(IQuery<TOut> query);

        /// <summary>
        /// Передать запрос обработчику команд
        /// </summary>
        /// <typeparam name="TOut">Результат выполнения команды</typeparam>
        /// <param name="command">Команда</param>
        /// <returns></returns>
        TOut SendCommand<TOut>(ICommand<TOut> command);
    }
}
