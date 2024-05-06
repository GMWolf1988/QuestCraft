using TMPro;
using UnityEngine;

public class EventTitle : MonoBehaviour
{
    TextMeshProUGUI _text;
    AudioSource _audio;

    bool _isPlaying = false;

    readonly float _totalDuration = 2.5f;
    readonly float _fadeInDuration = 1f;
    readonly float _fadeOutDuration = .5f;
    float _totalTimer;
    float _fadeInTimer;
    float _fadeOutTimer = 1f;

    void Start()
    {
        _text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _audio = transform.Find("Sound").GetComponent<AudioSource>();
    }

    public void Run()
    {
        Reset();
        _isPlaying = true;
        _audio.Play();
    }

    void Update()
    {
        if (!_isPlaying) return;
        
        if (_fadeInTimer < _fadeInDuration)
        {
            _fadeInTimer += Time.deltaTime;
            _totalTimer += Time.deltaTime;
            _text.alpha = _fadeInTimer / _fadeInDuration;
            return;
        }

        if (_totalTimer + _fadeOutTimer < _totalDuration)
        {
            _totalTimer += Time.deltaTime;
            return;
        }

        if (_totalTimer >= _totalDuration)
        {
            Reset();
            return;
        }

        _fadeOutTimer -= Time.deltaTime;
        _totalTimer += Time.deltaTime;
        _text.alpha = _fadeOutTimer / _fadeOutDuration;
    }

    void Reset()
    {
        _text.alpha = 0;
        _fadeInTimer = 0f;
        _fadeOutTimer = _fadeOutDuration;
        _totalTimer = 0f;
        _isPlaying = false;
    }
}
