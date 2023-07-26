using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadioController : MonoBehaviour
{
    public SongData[] songs;    // An array to store your audio clips
    public float volume = 1f;


    [System.NonSerialized]
    public AudioSource audioSource;


    protected int currentSongIndex = 0;

    public RadioState currentState;
    private RadioState onState = new RadioOnState();
    private RadioState offState = new RadioOffState();
    private RadioState pausedState = new RadioPausedState();




    void Update()
    {
        currentState.UpdateState();
    }



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSongIndex = (int) Random.Range(0, songs.Length - 1);
        ChangeState(onState);
        //PlayNextSong();  // Play the first song when the scene starts
    }

    public void PlayNextSong()
    {
        // Check if there are any songs in the array
        if (songs.Length == 0)
        {
            Debug.LogWarning("No songs in the 'songs' array.");
            return;
        }

        // Move to the next song in the array, and loop back to the beginning if we've reached the end
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        SongData currentSong = songs[currentSongIndex];

        // Set the audio clip to the next song in the array
        audioSource.clip = currentSong.songClip;

        // Play the audio
        AudioManager.instance.PlayAudioSource(audioSource, volume);


        Debug.Log("Now Playing " + currentSong.name);

    }

    public void Pause()
    {
        ChangeState(pausedState);
    }

    public void UnPause()
    {
        if (currentState == pausedState)
            currentState.ExitState();
    }




    public void Button()
    {
        if (currentState == onState)
        {
            ChangeState(offState);
        }
        else if (currentState == offState)
        {
            ChangeState(onState);
        }


    }




    public void ChangeState(RadioState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState(this);
    }


}

public abstract class RadioState
{
    public abstract void EnterState(RadioController radioController);
    public abstract void UpdateState();
    public abstract void ExitState();
}



public class RadioOnState : RadioState
{
    public RadioController radioController;


    public override void EnterState(RadioController radioController)
    {
        this.radioController = radioController;
    }
    public override void ExitState()
    {

    }

    public override void UpdateState()
    {


        if (!radioController.audioSource.isPlaying)
        {
            radioController.PlayNextSong();
        }
    }
}


public class RadioOffState : RadioState
{
    public RadioController radioController;

    public override void EnterState(RadioController radioController)
    {
        this.radioController = radioController;
        radioController.audioSource.mute = true;
    }

    public override void ExitState()
    {
        radioController.audioSource.mute = false;
    }

    public override void UpdateState()
    {

        if (!radioController.audioSource.isPlaying)
        {
            radioController.PlayNextSong();
        }
    }
}

public class RadioPausedState : RadioState
{
    public RadioController radioController;
    public RadioState previousState;

    public override void EnterState(RadioController radioController)
    {
        this.radioController = radioController;
        radioController.audioSource.Pause();
        previousState = radioController.currentState;
    }
    public override void ExitState()
    {
        radioController.ChangeState(previousState);
        radioController.audioSource.UnPause();

    }

    public override void UpdateState()
    {
    }
}
