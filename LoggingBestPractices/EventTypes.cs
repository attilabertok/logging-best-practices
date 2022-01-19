using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggingBestPractices;

public class EventTypes
{
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private readonly Serilog.ILogger _serilogLogger;

    public EventTypes(Microsoft.Extensions.Logging.ILogger logger, Serilog.ILogger serilogLogger)
    {
        _logger = logger;
        _serilogLogger = serilogLogger;
    }

    public void Log(Guid userId, string oldEmailAddress, string newEmailAddress)
    {
        _logger.LogWarning(AppLogEvents.ChangeEmailAddressEvent.Id, AppLogEvents.ChangeEmailAddressEvent.Message, userId, oldEmailAddress, newEmailAddress);
        _logger.LogEmailChange(userId, oldEmailAddress, newEmailAddress);
    }

    public void TestPerformance(Guid userId, string oldEmailAddress, string newEmailAddress, int operationCount)
    {
        using (_serilogLogger.BeginTimedOperation("Writing a log message using the default extension method"))
        {
            for (var i = 0; i < operationCount; i++)
            {
                _logger.LogWarning(AppLogEvents.ChangeEmailAddressEvent.Id, AppLogEvents.ChangeEmailAddressEvent.Message, userId, oldEmailAddress, newEmailAddress);
            }
        }

        using (_serilogLogger.BeginTimedOperation("Writing a log message using a pre-defined LogMessage"))
        {
            for (var i = 0; i < operationCount; i++)
            {
                _logger.LogEmailChange(userId, oldEmailAddress, newEmailAddress);
            }
        }
    }
}

public static class AppLogIds
{
    public static EventId ChangeEmailAddressId { get; } = new EventId(17, nameof(ChangeEmailAddressId)[..^2]);
}

public class AppLogEvents
{
    public static AppLogEvent ChangeEmailAddressEvent { get; } = new AppLogEvent(AppLogIds.ChangeEmailAddressId, "User {UserId} updated email address from {OldEmailAddress} to {NewEmailAddress}");
}

public class AppLogEvent
{
    public EventId Id { get; }

    public string? Message { get; }

    public AppLogEvent(EventId id, string? message)
    {
        Id = id;
        Message = message;
    }
}
