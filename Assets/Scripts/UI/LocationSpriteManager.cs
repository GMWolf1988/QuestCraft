using System.Collections.Generic;
using UnityEngine;

public class LocationSpriteManager : MonoBehaviour
{
    public static LocationSpriteManager instance;

    public readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        Database.Locations.ForEach(location => {
            Sprite sprite = Resources.Load<Sprite>($"Images/{location.ImagePath}");
            Sprites.Add(location.Name, sprite);
        });
    }
}
