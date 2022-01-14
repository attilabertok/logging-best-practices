using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace LoggingBestPractices;

public class NullObjectPattern
{
    private readonly ILogger _logger;

    public NullObjectPattern(ILogger? logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }

    public void Log(string message)
    {
        _logger.LogWarning("This is logged from a class with a NullObjectPattern logger. The message was: {Message}", message);
    }
}
