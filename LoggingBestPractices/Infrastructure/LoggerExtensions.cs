using LoggingBestPractices.Infrastructure.LogEvents;
using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Infrastructure;

public static partial class LoggerExtensions
{
    private static Action<ILogger, Guid, string, string, Exception?>? _preDefinedLoggerMessage;

    public static void Init()
    {
        _preDefinedLoggerMessage = LoggerMessage.Define<Guid, string, string>(LogLevel.Warning, AppLogIds.ChangeEmailAddressId, AppLogEvents.ChangeEmailAddressEvent.Message + " invoked from LoggerMessage");
    }
}
