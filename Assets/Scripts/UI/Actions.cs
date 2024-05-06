using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public struct Action
{
    public string Label;
    public UnityAction Callback;

    public Action(string label, UnityAction callback)
    {
        Label = label;
        Callback = callback;
    }
}

public class Actions : MonoBehaviour
{
    public GameObject itemPrefab;
    public AudioClip hoverSound; 

    readonly List<Action> BaseActions = new List<Action>();
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        BaseActions.Add(new Action() { Label = "Travel", Callback = () => OpenLeftPanel("Travel") });
        BaseActions.Add(new Action() { Label = "Quest Log", Callback = () => {
            ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnViewAcceptedQuests());
        }});
        BaseActions.Add(new Action() { Label = "Completed Quests", Callback = () => {
            ExecuteEvents.Execute<IQuestStateEvents>(GameObject.Find("QuestState"), null, (x, y) => x.OnViewCompletedQuests());
        }});

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnActionContextChanged()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var location = PlayerState.instance.CurrentLocation;
        var activeQuest = QuestState.instance.GetActiveQuest();

        BaseActions
            .Concat(location.GetContextualActions())
            .Concat(activeQuest != null ? activeQuest.GetContextualActions(location) : new List<Action>())
            .ToList()
            .ForEach(action => {
                GameObject item = Instantiate(itemPrefab, transform);
                item.GetComponent<ButtonLabel>().SetLabel(action.Label);
                var button = item.GetComponent<Button>();
                button.onClick.AddListener(action.Callback);

                //Pointer Enter event listener to play the hover sound
                EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => { OnPointerEnter(button); });
                trigger.triggers.Add(entry);
            });
    }

    void OpenLeftPanel(string panelName)
    {
        ExecuteEvents.Execute<ILeftPanelEvents>(GameObject.Find("LeftPanel"), null, (x, y) => x.OnShowPanel(panelName));
    }

    void OnPointerEnter(Button button)
    {
        // Play the hover sound when the pointer enters the button
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}