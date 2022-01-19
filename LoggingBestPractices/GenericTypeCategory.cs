using Microsoft.Extensions.Logging;

namespace LoggingBestPractices;

public class GenericTypeCategory
{
    private readonly ILogger _logger;
    private readonly ILogger<GenericTypeCategory> _genericLogger;

    public GenericTypeCategory(ILogger logger, ILogger<GenericTypeCategory> genericLogger)
    {
        _logger = logger;
        _genericLogger = genericLogger;
    }

    public void Log()
    {
        _logger.LogWarning("Logged into logger that was created by injecting the non-generic interface.");
        _genericLogger.LogWarning("Logged into logger that was created by injecting the generic interface.");
    }
}
