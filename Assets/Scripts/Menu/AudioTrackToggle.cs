using UnityEngine;

public class AudioTrackToggle : MonoBehaviour
{
    public AudioSource audioTrack1;
    public AudioSource audioTrack2;
    public AudioSource audioTrack3;
    public ProximityVolumeControl proximityControl;

    public float activeVolume = 1f;
    public float inactiveVolume = 0f;

    private bool isTrack3Active = false;

    public void ToggleAudioTracks()
    {
        if (isTrack3Active)
        {
            // Activate tracks 1 and 2, deactivate track 3
            SetTrackVolume(audioTrack1, activeVolume);
            SetTrackVolume(audioTrack2, activeVolume);
            SetTrackVolume(audioTrack3, inactiveVolume);

            // Enable proximity control
            if (proximityControl != null)
            {
                proximityControl.ToggleOn(true);
            }
        }
        else
        {
            // Deactivate tracks 1 and 2, activate track 3
            SetTrackVolume(audioTrack1, inactiveVolume);
            SetTrackVolume(audioTrack2, inactiveVolume);
            SetTrackVolume(audioTrack3, activeVolume);

            // Disable proximity control
            if (proximityControl != null)
            {
                proximityControl.ToggleOn(false);
            }
        }

        isTrack3Active = !isTrack3Active;
    }

    private void SetTrackVolume(AudioSource track, float volume)
    {
        if (track != null)
        {
            track.volume = volume;
        }
        else
        {
            Debug.LogWarning("An AudioSource is not assigned in the AudioTrackToggle script.");
        }
    }
}