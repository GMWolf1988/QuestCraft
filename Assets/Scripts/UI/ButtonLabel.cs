using TMPro;
using UnityEngine;

public class ButtonLabel : MonoBehaviour
{
    TextMeshProUGUI _label;

    string _labelText;

    bool _needsUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        _label = transform.Find("Label").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!_needsUpdate)
        {
            return;
        }

        _label.text = _labelText;
        _needsUpdate = false;
    }
    
    public void SetLabel(string text)
    {
        _labelText = text;
        _needsUpdate = true;
    }
}
