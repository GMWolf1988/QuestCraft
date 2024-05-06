using UnityEngine.EventSystems;

public interface ILeftPanelEvents : IEventSystemHandler
{
    void OnShowPanel(string panelName);
}