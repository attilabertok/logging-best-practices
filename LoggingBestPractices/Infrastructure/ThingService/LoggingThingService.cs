using Serilog;
using Serilog.Context;

namespace LoggingBestPractices.Infrastructure.ThingService;

public class LoggingThingService : IThingService
{
    private readonly IThingService _thingServiceImplementation;
    private readonly ILogger _logger;

    public LoggingThingService(IThingService thingServiceImplementation, ILogger serilogLogger)
    {
        _thingServiceImplementation = thingServiceImplementation;
        _logger = serilogLogger;
    }

    public int CountThings()
    {
        _logger.Debug("Counting things");
        var result = _thingServiceImplementation.CountThings();

        _logger.ForContext("Count", result)
            .Debug("Counted things");

        return result;
    }

    public void AddThing(string thingName)
    {
        using (LogContext.PushProperty("ThingName", thingName))
        {
            _logger.Debug("Adding a new thing");
            _thingServiceImplementation.AddThing(thingName);
            _logger.Debug("Successfully added a new thing");
        }
    }

    public void RemoveThing(string thingName)
    {
        using (LogContext.PushProperty("ThingName", thingName))
        {
            _logger.Debug("Removing a thing");
            try
            {
                _thingServiceImplementation.RemoveThing(thingName);
                _logger.Debug("Successfully removed a thing");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to remove a thing");

                // notice that there is no throw here
            }
        }
    }
}
