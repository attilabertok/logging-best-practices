using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Infrastructure.LogEvents;

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
