using System.Collections.Generic;

public class Item : BaseDataEntry
{
    public int Price;

    public Item(string name, List<string> tags, int price) : base(name, tags)
    {
        Price = price;
    }
}
