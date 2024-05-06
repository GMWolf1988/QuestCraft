using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Transform List;

    public void Refresh(string title, List<Quest> quests)
    {
        foreach (Transform child in List)
        {
            Destroy(child.gameObject);
        }

        Title.text = title;

        foreach (var quest in quests)
        {
            GameObject questObject = new GameObject();
            questObject.transform.parent = List;
            questObject.AddComponent<QuestListItem>();
            questObject.GetComponent<QuestListItem>().SetQuest(quest);
        }
    }
}
