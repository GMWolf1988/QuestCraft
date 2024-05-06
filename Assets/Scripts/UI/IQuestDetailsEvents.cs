using UnityEngine.EventSystems;

public interface IQuestDetailsEvents : IEventSystemHandler
{
    void OnShowQuest(Quest quest);
}