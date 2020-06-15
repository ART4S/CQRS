using System;

namespace WebFeatures.Application.Interfaces.Logging
{
    public interface ILogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception exception);
    }
}
