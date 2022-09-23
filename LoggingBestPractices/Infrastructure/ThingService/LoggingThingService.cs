using Microsoft.Extensions.Logging;

using Serilog.Context;

namespace LoggingBestPractices.Infrastructure.ThingService;

public class LoggingThingService : IThingService
{
    private readonly IThingService _thingServiceImplementation;
    private readonly ILogger _logger;

    public LoggingThingService(IThingService thingServiceImplementation, ILogger logger)
    {
        _thingServiceImplementation = thingServiceImplementation;
        _logger = logger;
    }

    public int CountThings()
    {
        _logger.LogDebug("Counting things");
        var result = _thingServiceImplementation.CountThings();

        using (_logger.BeginScope(new Dictionary<string, object> { ["Count"] = result }))
        {
            _logger.LogDebug("Counted things");
        }

        return result;
    }

    public void AddThing(string thingName)
    {
        using (_logger.BeginScope(new Dictionary<string, object> { ["ThingName"] = thingName}))
        {
            _logger.LogDebug("Adding a new thing");
            _thingServiceImplementation.AddThing(thingName);
            _logger.LogDebug("Successfully added a new thing");
        }
    }

    public void RemoveThing(string thingName)
    {
        using (_logger.BeginScope(CreateContext(thingName)))
        {
            _logger.LogDebug("Removing a thing");
            try
            {
                _thingServiceImplementation.RemoveThing(thingName);
                _logger.LogDebug("Successfully removed a thing");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to remove a thing");

                // notice that there is no throw here
            }
        }
    }

    private static Dictionary<string, object> CreateContext(string thingName)
    {
        return new Dictionary<string, object>
        {
            ["CustomScopeSource"] = nameof(LoggingThingService),
            ["CommonData"] = "Some common data",
            ["ThingName"] = thingName
        };
    }
}
