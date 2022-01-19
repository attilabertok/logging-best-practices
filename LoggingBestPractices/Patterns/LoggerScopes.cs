using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class LoggerScopes
{
    private readonly ILogger _logger;

    public LoggerScopes(ILogger logger)
    {
        _logger = logger;
    }

    public void LogWithContext()
    {
        _logger.LogWarning("This is a log within an ambient scope");

        using (_logger.BeginScope("this log is in an {Scope} scope", "internal"))
        {
            _logger.LogWarning("This is a log within an explicit scope");
        }

        using (_logger.BeginScope("this log is in a scope with another context info {Scope2} scope", "special"))
        {
            _logger.LogWarning("This is a log within a special explicit scope");
        }
    }
}
