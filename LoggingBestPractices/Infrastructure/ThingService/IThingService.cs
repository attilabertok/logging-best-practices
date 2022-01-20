namespace LoggingBestPractices.Infrastructure.ThingService;

public interface IThingService
{
    int CountThings();

    void AddThing(string thingName);

    void RemoveThing(string thingName);
}
