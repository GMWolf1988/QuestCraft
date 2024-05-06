using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestDetails : MonoBehaviour, IQuestDetailsEvents
{
    [SerializeField]
    TextMeshProUGUI _titleText;
    [SerializeField]
    TextMeshProUGUI _descriptionText;
    [SerializeField]
    GameObject _objectiveList;
    [SerializeField]
    TextMeshProUGUI _rewardList;

    [SerializeField]
    GameObject _objectiveTextPrefab;

    [SerializeField]
    GameObject _buttonAccept;
    [SerializeField]
    GameObject _buttonSetActive;

    Quest _quest;

    public void OnShowQuest(Quest quest)
    {
        _quest = quest;

        _titleText.text = quest.Title;
        string the = quest.Objectives[0].Source.Tags.Contains("town") ? "the " : "";
        _descriptionText.text = quest.Description + $"\n\nYou should start by travelling to {the}{quest.Objectives[0].Source.Name}.";
        _rewardList.text = quest.GetRewardList();

        foreach (Transform child in _objectiveList.transform)
        {
            Destroy(child.gameObject);
        }

        quest.Objectives.ForEach(objective => {
            AddObjective(objective.ToString(), objective.IsCompleted);
        });

        AddObjective($"Return to {quest.QuestGiverLocation.Name}", quest.Status == QuestStatus.Completed);

        switch (quest.Status)
        {
            case QuestStatus.Available:
                _buttonAccept.SetActive(true);
                _buttonSetActive.SetActive(false);
            break;
            case QuestStatus.Completed:
                _buttonAccept.SetActive(false);
                _buttonSetActive.SetActive(false);
            break;
            case QuestStatus.Accepted:
                bool isActiveQuest = QuestState.instance.IsQuestActive(quest);
                _buttonSetActive.SetActive(!isActiveQuest);
                _buttonAccept.SetActive(false);
            break;
        }
    }

    void AddObjective(string text, bool completed = false)
    {
        var textObject = Instantiate(_objectiveTextPrefab, _objectiveList.transform);
        var tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = $"- {text}";
        if (completed)
        {
            tmp.text += " (Completed)";
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0.4f);
        }

        tmp.UpdateMeshPadding();
    }

    public void Accept()
    {
        ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnAcceptQuest(_quest));
    }

    public void SetActive()
    {
        ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnSetQuestActive(_quest));
    }
}
