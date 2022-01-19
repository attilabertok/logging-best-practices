using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Infrastructure;

public static partial class LoggerExtensions
{
    public static ILogger LogEmailChange(this ILogger logger, Guid userId, string oldEmailAddress, string newEmailAddress)
    {
        _preDefinedLoggerMessage?.Invoke(logger, userId, oldEmailAddress, newEmailAddress, null);

        return logger;
    }
}