using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip[] audioClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.GetComponent<AudioSource>().clip = audioClips[Random.Range(0, 3)];
            this.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "OneMatching" && scene.name != "Lobby" && scene.name != "Title" && scene.name != "TwoMatching")
        {
            Destroy(gameObject);
        }
    }
}
