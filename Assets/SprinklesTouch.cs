using UnityEngine;
using UnityEngine.Audio;

public class SprinklesTouch : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    public void Touch()
    {
        if (audioClips.Length > 0 && audioSource != null)
        {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
    }
}
