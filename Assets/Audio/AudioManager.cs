using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public float masterVolume = 1.0f;  // Master volume (0.0 to 1.0)
    public float musicVolume = 1.0f;   // Music volume (0.0 to 1.0)
    public float sfxVolume = 1.0f;     // Sound effects volume (0.0 to 1.0)

    public float maxPitch = 1.1f;
    public float minPitch = 0.9f;



    private HashSet<AudioSource> audioSourceSet = new HashSet<AudioSource>();



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

    #region sets
    public void AddToSet(AudioSource audioSource)
    {
        audioSourceSet.Add(audioSource);
    }

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
        // Apply sound settings to each AudioSource
        foreach (AudioSource audioSource in audioSourceSet)
        {
            if (audioSource == null)
            {
                audioSourceSet.Remove(audioSource);
                continue;
            }

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



    public void PlayAudioSource(AudioSource audioSource , float audioSourceVolume = 1f)
    {
        ApplySoundSettings(audioSource, audioSourceVolume);
        audioSourceSet.Add(audioSource);
        audioSource.PlayOneShot(audioSource.clip);


    }


    void ApplySoundSettings(AudioSource audioSource, float audioSourceVolume)
    {

        switch (audioSource.tag)
        {
            case "Music":
                audioSource.volume = (masterVolume * musicVolume) * audioSourceVolume;
                break;
            case "SFX":
                audioSource.volume = (masterVolume * sfxVolume) * audioSourceVolume;
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                break;
            default:
                Debug.LogWarning("AudioSource with tag '" + audioSource.tag + "' is not recognized.");
                audioSource.volume = (masterVolume * musicVolume) * audioSourceVolume;
                break;
        }

    }



    #region Save/Load
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
    #endregion
}