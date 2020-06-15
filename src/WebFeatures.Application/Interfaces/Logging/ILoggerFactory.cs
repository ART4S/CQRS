namespace WebFeatures.Application.Interfaces.Logging
{
    public interface ILoggerFactory
    {
        ILogger<T> CreateLogger<T>();
    }
}
