using UnityEngine;
using System.Collections.Generic;

public class AudioSyncManager : MonoBehaviour
{
    public List<AudioSource> audioSources = new List<AudioSource>();
    public float startDelay = 0.1f; // Short delay to ensure all audio sources are ready

    private bool isPlaying = false;

    void Start()
    {
        // Ensure all audio sources are stopped and ready to play
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.Stop();
                source.playOnAwake = false;
                source.time = 0; // Reset playback time to beginning
            }
            else
            {
                Debug.LogWarning("Null AudioSource found in AudioSyncManager.");
            }
        }
        PlayAllSources();
    }

    public void PlayAllSources()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            Invoke("PlaySources", startDelay);
        }
    }

    public void StopAllSources()
    {
        if (isPlaying)
        {
            isPlaying = false;
            foreach (AudioSource source in audioSources)
            {
                if (source != null)
                {
                    source.Stop();
                }
            }
        }
    }

    private void PlaySources()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.Play();
            }
        }
    }
}