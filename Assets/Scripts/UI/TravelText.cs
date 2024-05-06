using TMPro;
using UnityEngine;

public class TravelText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;

    public void SetText(string text)
    {
        _text.SetText(text);
    }
}
