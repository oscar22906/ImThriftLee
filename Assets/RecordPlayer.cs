using UnityEngine;
using DG.Tweening;

public class RecordPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip recordScratch;
    [SerializeField] private float pitchDuration = 2f;

    private Animator animator;
    private float previousTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleRecordPlayer()
    {
        if (audioSource.isPlaying)
        {
            previousTime = audioSource.time;  // Save current position of the track
            DOTween.To(() => audioSource.pitch, x => audioSource.pitch = x, 0f, pitchDuration)
                .OnKill(() => audioSource.Pause());  // Pause the track after pitch reaches 0
            audioSource.PlayOneShot(recordScratch);
            animator.SetBool("Stopped", true);
        }
        else
        {
            // Restore the track position and resume playing from where it left off
            audioSource.time = previousTime;

            DOTween.To(() => audioSource.pitch, x => audioSource.pitch = x, 1f, pitchDuration)
                .OnKill(() =>
                {
                    audioSource.UnPause();  // Unpause the track and resume playing
                });

            audioSource.PlayOneShot(recordScratch);
            animator.SetBool("Stopped", false);
        }
    }
}
