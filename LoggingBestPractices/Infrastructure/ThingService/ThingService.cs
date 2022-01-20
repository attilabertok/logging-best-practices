namespace LoggingBestPractices.Infrastructure.ThingService;

public class ThingService : IThingService
{
    private readonly List<string> _things = new List<string>();

    public int CountThings()
    {
        return _things.Count;
    }

    public void AddThing(string thingName)
    {
        _things.Add(thingName);
    }

    public void RemoveThing(string thingName)
    {
        if (_things.Count > 0)
        {
            if (_things.Contains(thingName))
            {
                _things.Remove(thingName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(thingName), thingName, "thing not found");
            }
        }
        else
        {
            throw new InvalidOperationException("Cannot remove a thing when there are no things.");
        }
    }
}
