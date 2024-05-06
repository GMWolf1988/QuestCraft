using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelListItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    TextMeshProUGUI _textMeshProUGUI;
    Location _location;

    Color _normalColor = Color.white;
    Color _highlightColor = new Color(1, 0.85f, 0.6f, 1);
    Color _disabledColor = Color.gray;

    Color _currentColor;

    bool _disabled = false;

    bool _needsUpdate = true;

    private void Awake()
    {
        _currentColor = _normalColor;
    }

    private void Start()
    {
        _textMeshProUGUI = gameObject.AddComponent<TextMeshProUGUI>();
        _textMeshProUGUI.fontSize = 18;
        _textMeshProUGUI.fontWeight = FontWeight.Bold;
        _textMeshProUGUI.text = _location.Name;
        _textMeshProUGUI.UpdateMeshPadding();

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0.5f, 0.5f);
    }

    void Update()
    {
        if (!_needsUpdate) return;
        
        _textMeshProUGUI.color = _currentColor;

        _needsUpdate = false;
    }

    void SetColor(Color color)
    {
        _currentColor = color;
        _needsUpdate = true;
    }

    public void SetLocation(Location location)
    {
        _location = location;
    }

    public Location GetLocation()
    {
        return _location;
    }

    public void Enable()
    {
        _disabled = false;
        SetColor(_normalColor);
    }

    public void Disable()
    {
        _disabled = true;
        SetColor(_disabledColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_disabled) return;
        SetColor(_highlightColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_disabled) return;
        SetColor(_normalColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_disabled) return;
        ExecuteEvents.Execute<IPlayerStateEvents>(GameObject.Find("PlayerState"), null, (x, y) => x.OnChangeLocation(_location));
    }

    void OnDisable()
    {
        if (_disabled) return;
        SetColor(_normalColor);
    }
}
