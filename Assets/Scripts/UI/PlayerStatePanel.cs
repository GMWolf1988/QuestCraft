using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _currentLocation;

    [SerializeField]
    TextMeshProUGUI _activeQuest;

    [SerializeField]
    Image _image;

    void Start()
    {
        OnActiveQuestChanged(null);
    }

    public void OnLocationChanged()
    {
        var currentLocation = PlayerState.instance.CurrentLocation.Name;
        _currentLocation.text = currentLocation;
        _image.sprite = LocationSpriteManager.instance.Sprites[currentLocation];
    }

    public void OnActiveQuestChanged(Quest quest)
    {
        if (quest == null)
        {
            _activeQuest.text = "None";
            return;
        }

        _activeQuest.text = quest.Title;
    }
}
