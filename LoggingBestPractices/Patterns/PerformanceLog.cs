using Serilog;
using SerilogTimings.Extensions;

namespace LoggingBestPractices.Patterns;

public class PerformanceLog
{
    private readonly ILogger logger;

    public PerformanceLog(ILogger logger)
    {
        this.logger = logger;
    }

    public void Log()
    {
        using (logger.TimeOperation("Performance timing this long-running operation."))
        {
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
        }
    }
}