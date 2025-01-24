using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject debug; // The debug menu GameObject
    public bool debugEnabled; // Tracks whether the debug menu is enabled

    Animator animator;

    void Start()
    {
        // Initialize the debug menu state
        if (debug != null)
        {
            animator = debug.GetComponent<Animator>();
            animator.SetBool("DebugEnabled", debugEnabled);
        }
    }

    void Update()
    {
        // Toggle the debug menu with the F key
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (debug != null)
            {
                if (animator != null)
                {
                    if (!debugEnabled)
                    {
                        animator.SetBool("DebugEnabled", debugEnabled);
                        debugEnabled = true;
                    }
                    else
                    {
                        // Play "debugOff" animation, the animation event will handle disabling
                        animator.SetBool("DebugEnabled", debugEnabled);
                        debugEnabled = false;
                    }
                }
            }
        }
    }

    public void DisableDebugMenu()
    {
        if (debug != null)
        {
            debug.SetActive(false);
        }
    }
}
