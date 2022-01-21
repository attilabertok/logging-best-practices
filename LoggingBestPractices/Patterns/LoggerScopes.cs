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

        using (_logger.BeginScope("this log is in an {ExplicitScope} scope", "internal"))
        {
            _logger.LogWarning("This is a log within an explicit scope");
        }

        using (_logger.BeginScope("this log is in a scope with another context info {ExplicitScope} scope, with some {ExtraData} data", "special", "additional"))
        {
            _logger.LogWarning("This is a log within a special explicit scope");
        }

        using (_logger.BeginScope("this will be inserted to a property called Scope"))
        {
            _logger.LogWarning("No formatted string in scope");
        }

        var scope = new Dictionary<string, object>
        {
            ["Key1"] = "value1",
            ["Key2"] = "value2"
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogWarning("This is a log within a dictionary scope");
        }
    }
}
