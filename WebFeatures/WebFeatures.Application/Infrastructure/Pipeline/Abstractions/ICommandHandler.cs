namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Обработчик команды
    /// </summary>
    /// <typeparam name="TCommand">Команда</typeparam>
    /// <typeparam name="TResult">Результат выполнения команды</typeparam>
    public interface ICommandHandler<in TCommand, out TResult> : IHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
