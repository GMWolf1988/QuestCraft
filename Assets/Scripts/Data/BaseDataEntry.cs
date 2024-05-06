using System;
using System.Collections.Generic;

public class BaseDataEntry
{
    public string Name;

    public List<string> Tags;

    public BaseDataEntry(string name, List<string> tags)
    {
        Name = name;
        Tags = tags;
    }

    public string PickRandomTag()
    {
        Random random = new Random();
        int tagIndex = random.Next(Tags.Count);
        return Tags[tagIndex];
    }
}
