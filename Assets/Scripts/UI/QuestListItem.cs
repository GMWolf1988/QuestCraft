using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestListItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    TextMeshProUGUI _textMeshProUGUI;
    Quest _quest;

    Color _normalColor = Color.white;
    Color _highlightColor = new Color(1, 0.85f, 0.6f, 1);

    private void Start()
    {
        _textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
        _textMeshProUGUI.color = _normalColor;
        _textMeshProUGUI.fontSize = 18;
        _textMeshProUGUI.fontWeight = FontWeight.Bold;
        _textMeshProUGUI.text = _quest.Title;
        _textMeshProUGUI.UpdateMeshPadding();

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0.5f, 0.5f);
    }

    public void SetQuest(Quest quest)
    {
        _quest = quest;
    }

    public void OnPointerEnter(PointerEventData eventData) => _textMeshProUGUI.color = _highlightColor;
    public void OnPointerExit(PointerEventData eventData) => _textMeshProUGUI.color = _normalColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        ExecuteEvents.Execute<ILeftPanelEvents>(GameObject.Find("LeftPanel"), null, (x, y) => x.OnShowPanel("QuestDetails"));
        ExecuteEvents.Execute<IQuestDetailsEvents>(GameObject.Find("QuestDetails"), null, (x, y) => x.OnShowQuest(_quest));
    }

    void OnDisable() => _textMeshProUGUI.color = _normalColor;
}
