using UnityEngine.EventSystems;

public interface IQuestStateEvents : IEventSystemHandler
{
    void OnAcceptQuest(Quest quest);
    void OnSetQuestActive(Quest quest);
    void OnCompleteQuest(Quest quest);
    void OnViewAvailableQuests();
    void OnViewAcceptedQuests();
    void OnViewCompletedQuests();
}