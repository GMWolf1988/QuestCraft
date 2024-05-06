using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicDoNotDestroy : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip inGameMusic;

    private AudioSource audioSource;

    static MusicDoNotDestroy Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the loaded scene and update the music track accordingly
        if (scene.name == "InGame")
        {
            // Change the music track to otherSceneMusic
            if (audioSource.clip != inGameMusic)
            {
                audioSource.clip = inGameMusic;
                audioSource.Play();
            }
        }
        else
        {
            // Change the music track to mainMenuMusic
            if (audioSource.clip != mainMenuMusic)
            {
                audioSource.clip = mainMenuMusic;
                audioSource.Play();
            }
        }
    }
}
