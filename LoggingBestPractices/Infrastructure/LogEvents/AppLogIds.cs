using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Infrastructure.LogEvents;

public static class AppLogIds
{
    public static EventId ChangeEmailAddressId { get; } = new EventId(17, nameof(ChangeEmailAddressId)[..^2]);
}
