using System.Collections.Generic;

public class Location : BaseDataEntry
{
    public string TravelText;
    public string ImagePath;
    readonly List<Action> _contextualActions = new List<Action>();

    public Location(string name, string travelText, string imagePath, List<string> tags) : base(name, tags)
    {
        TravelText = travelText;
        ImagePath = imagePath;
    }

    public Location(string name, string travelText, string imagePath, List<string> tags, List<Action> contextualActions) : base(name, tags)
    {
        TravelText = travelText;
        ImagePath = imagePath;
        _contextualActions.AddRange(contextualActions);
    }

    public List<Action> GetContextualActions()
    {
        return _contextualActions;
    }
}
