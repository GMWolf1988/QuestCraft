using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _currentGoldText;

    int _currentGold = 0;

    void Update()
    {
        if (PlayerState.instance.Gold == _currentGold) return;

        _currentGold = PlayerState.instance.Gold;

        _currentGoldText.text = $"{_currentGold} gold";
    }
}
