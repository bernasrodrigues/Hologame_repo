using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float masterVolume = 1.0f;  // Master volume (0.0 to 1.0)
    public float musicVolume = 1.0f;   // Music volume (0.0 to 1.0)
    public float sfxVolume = 1.0f;     // Sound effects volume (0.0 to 1.0)


    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadSoundSettings();
    }

    #region Sets
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        ApplySoundSettings();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        ApplySoundSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        ApplySoundSettings();
    }
    #endregion


    private void ApplySoundSettings()
    {
        // Apply sound settings to all audio sources in the scene

        // Find all AudioSources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Apply sound settings to each AudioSource
        foreach (AudioSource audioSource in audioSources)
        {
            switch (audioSource.tag)
            {
                case "Music":
                    audioSource.volume = masterVolume * musicVolume;
                    break;
                case "SFX":
                    audioSource.volume = masterVolume * sfxVolume;
                    break;
                default:
                    Debug.LogWarning("AudioSource with tag '" + audioSource.tag + "' is not recognized.");
                    break;
            }
        }

        // Save sound settings
        SaveSoundSettings();
    }

    private void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    private void LoadSoundSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");

        if (PlayerPrefs.HasKey("MusicVolume"))
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");

        if (PlayerPrefs.HasKey("SFXVolume"))
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");

        // Apply the loaded sound settings
        ApplySoundSettings();
    }
}