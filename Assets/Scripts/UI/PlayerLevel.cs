using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _currentLevel;

    [SerializeField]
    TextMeshProUGUI _toNextLevel;

    [SerializeField]
    Image _levelProgress;

    int _currentXP;

    // Start is called before the first frame update
    void Start()
    {
        _levelProgress.transform.localScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerState.instance.XP == _currentXP) return;

        _currentXP = PlayerState.instance.XP;

        double level = PlayerState.instance.GetLevel();
        int levelReached = (int)level;
        float progress = (float)level - levelReached;
        Vector3 progressScale = _levelProgress.transform.localScale;
        progressScale.x = progress;
        _levelProgress.transform.localScale = progressScale;

        int toNextLevel = PlayerState.instance.GetXPToNextLevel();
        _currentLevel.text = $"Level {levelReached}";
        _toNextLevel.text = $"(To next level: {toNextLevel} XP)";
    }
}
