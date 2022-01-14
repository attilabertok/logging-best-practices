using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace LoggingBestPractices;

public class ExceptionLogging
{
    private readonly ILogger _logger;

    public ExceptionLogging(ILogger logger)
    {
        _logger = logger;
    }

    public void LogException()
    {
        try
        {
            throw new ArgumentOutOfRangeException("this is the parameter", "this is the actual value", "parameter should not take this value");
        }
        catch (Exception e)
        {
            _logger.LogError("An exception occurred, how helpful.");
            _logger.LogError($"An exception occurred, how helpful. {e.Message}");
            _logger.LogError("An exception occurred, how helpful. {Message}", e.Message);
            _logger.LogError(e, "Error when executing {Method}", nameof(LogException));
        }
    }
}
