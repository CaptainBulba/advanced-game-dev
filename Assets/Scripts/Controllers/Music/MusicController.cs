using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource; 

    public float maxVolume;
    private float soundVolume = 1f;

    private bool soundToggle = true;

    public static MusicController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        audioSource.volume = maxVolume;
    }

    public void ChangeVolume()
    {
        if (soundToggle) soundVolume = 0f;
        else soundVolume = maxVolume;

        soundToggle = !soundToggle;

        audioSource.volume = soundVolume;
    }

    public float GetSoundVolume()
    {
        return soundVolume;
    }

    public void ChangeMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}