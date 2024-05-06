using UnityEngine;

public class LeftPanel : MonoBehaviour, ILeftPanelEvents
{
    GameObject _currentObject;

    public void OnShowPanel(string panelName)
    {
        if(_currentObject != null)
        {
            _currentObject.SetActive(false);
        }
        if (panelName == "None")
        {
            _currentObject.SetActive(false);
            return;
        }

        _currentObject = transform.Find(panelName).gameObject;
        _currentObject.SetActive(true);
    }
}
