using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class GenericTypeCategory
{
    private readonly ILogger _logger;
    private readonly ILogger<GenericTypeCategory> _genericLogger;
    private readonly ILogger _customCategoryLogger;

    public GenericTypeCategory(ILogger logger, ILogger<GenericTypeCategory> genericLogger, ILogger customCategoryLogger)
    {
        _logger = logger;
        _genericLogger = genericLogger;
        _customCategoryLogger = customCategoryLogger;
    }

    public void Log()
    {
        _logger.LogWarning("Logged into logger that was created by injecting the non-generic interface.");
        _genericLogger.LogWarning("Logged into logger that was created by injecting the generic interface.");
        _customCategoryLogger.LogWarning("Logged into logger that has a custom category.");
    }
}
