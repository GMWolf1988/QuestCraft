using System.Collections.Generic;
// Define a public class named Location which inherits from BaseDataEntry.
public class Location : BaseDataEntry
{
    // Declare a public string field named TravelText to hold the text related to traveling to this location.
    public string TravelText;

    // Declare a public string field named ImagePath to store the file path of the image associated with this location.
    public string ImagePath;

    // Declare a readonly List of Action objects named _contextualActions to store contextual actions related to this location.
    readonly List<Action> _contextualActions = new List<Action>();

    // Constructor for Location class with parameters: name, travelText, imagePath, and tags.
    public Location(string name, string travelText, string imagePath, List<string> tags) : base(name, tags)
    {
        // Assign the travel text passed as parameter to the TravelText field.
        TravelText = travelText;
        // Assign the image path passed as parameter to the ImagePath field.
        ImagePath = imagePath;
    }

    // Constructor overload for Location class with an additional parameter for contextualActions.
    public Location(string name, string travelText, string imagePath, List<string> tags, List<Action> contextualActions) : base(name, tags)
    {
        // Assign the travel text passed as parameter to the TravelText field.
        TravelText = travelText;
        // Assign the image path passed as parameter to the ImagePath field.
        ImagePath = imagePath;
        // Add all contextual actions passed as parameter to the _contextualActions list.
        _contextualActions.AddRange(contextualActions);
    }

    // Method to retrieve the contextual actions associated with this location.
    public List<Action> GetContextualActions()
    {
        // Return the list of contextual actions.
        return _contextualActions;
    }
}
