using LoggingBestPractices.Infrastructure.ThingService;
using Microsoft.Extensions.Logging;

namespace LoggingBestPractices.Patterns;

public class Things
{
    public const string Wallet = "wallet";
    public const string Keys = "keys";
}

public class DecoratorPattern
{
    private readonly IThingService _thingService;

    public DecoratorPattern(IThingService thingService)
    {
        _thingService = thingService;
    }

    public void ManipulateThings()
    {
        _thingService.CountThings();
        _thingService.AddThing(Things.Wallet);
        _thingService.AddThing(Things.Keys);
        _thingService.CountThings();
        _thingService.RemoveThing(Things.Wallet);
        _thingService.CountThings();
        _thingService.RemoveThing(Things.Wallet);
        _thingService.RemoveThing(Things.Keys);
        _thingService.RemoveThing(Things.Wallet);
    }
}
