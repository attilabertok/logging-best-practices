using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class Sampling
{
    private const int ItemCount = 1_000_000;
    private const int SamplingRate = 100_000;
    private readonly ILogger _logger;

    public Sampling(ILogger logger)
    {
        _logger = logger;
    }

    public void LogCriticalPath()
    {
        _logger.LogInformation("Processing items...");
        for (var i = 0; i < ItemCount; i++)
        {
            // do thing here
            if (ShouldSample(i))
            {
                _logger.LogInformation("Processed {ItemCount}/{TotalCount} items", i, ItemCount);
            }
        }
        _logger.LogInformation("Processed all items");
    }

    public void ProcessAndSample()
    {
        _logger.LogInformation("Processing items...");
        for (var i = 0; i < ItemCount; i++)
        {
            // do thing here
            if (ShouldSample(i))
            {
                using (_logger.BeginScope("item details {item}", new { Details = "this is an item", ItemId = Guid.NewGuid() }))
                {
                    _logger.LogInformation("Processing {ItemCount}/{TotalCount} item", i, ItemCount);
                }
            }
        }
    }

    private static bool ShouldSample(int i)
    {
        return i % SamplingRate == 1;
    }
}
