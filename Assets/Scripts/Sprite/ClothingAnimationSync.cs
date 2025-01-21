using UnityEngine;

public class ClothingAnimationSync : MonoBehaviour
{
    private Animator clothingAnimator;

    private void Start()
    {
        clothingAnimator = GetComponent<Animator>();

        // Subscribe to frame change events
        AnimationManager.Instance.OnFrameChanged += SyncFrame;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (AnimationManager.Instance != null)
        {
            AnimationManager.Instance.OnFrameChanged -= SyncFrame;
        }
    }

    /// <summary>
    /// Syncs the clothing animation to the specified frame.
    /// </summary>
    /// <param name="frame">The frame to sync to.</param>
    private void SyncFrame(int frame)
    {
        clothingAnimator.Play(0, 0, frame / (float)clothingAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
}