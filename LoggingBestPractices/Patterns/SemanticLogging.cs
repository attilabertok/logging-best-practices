using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class SemanticLogging
{
    private readonly ILogger _logger;

    public SemanticLogging(ILogger logger)
    {
        _logger = logger;
    }

    public void LogInterpolated(string logMessage)
    {
        _logger.LogError($"this {logMessage} was written without semantic logging");
        _logger.LogError("this {LogMessage} was written with semantic logging", logMessage);
        _logger.LogError("logging complex data {Data}", new List<int> { 1, 2, 3, 4});
    }
}
