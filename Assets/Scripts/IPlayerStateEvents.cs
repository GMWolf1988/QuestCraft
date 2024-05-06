using UnityEngine.EventSystems;

public interface IPlayerStateEvents : IEventSystemHandler
{
    void OnChangeLocation(Location location);
}