using UnityEngine;
using UnityEngine.Events;

public class ClothingAnimationSync : MonoBehaviour
{
    public Animator animator;
    public UnityEvent<int> onFrameChanged; // Event for notifying frame changes

    private int lastFrame = -1;

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        int currentFrame = Mathf.FloorToInt(stateInfo.normalizedTime * stateInfo.length * 24); // Assuming 24 FPS animation

        if (currentFrame != lastFrame)
        {
            lastFrame = currentFrame;
            onFrameChanged?.Invoke(currentFrame);
        }
    }
}