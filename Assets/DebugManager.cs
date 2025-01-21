using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject debug; // The debug menu GameObject
    public bool debugEnabled; // Tracks whether the debug menu is enabled

    void Start()
    {
        // Initialize the debug menu state
        if (debug != null)
        {
            debug.SetActive(debugEnabled);
        }
    }

    void Update()
    {
        // Toggle the debug menu with the F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (debug != null)
            {
                Animator animator = debug.GetComponent<Animator>();
                if (animator != null)
                {
                    if (!debugEnabled)
                    {
                        // Enable debug menu and play "debugOn" animation
                        debugEnabled = true;
                        debug.SetActive(true);
                        animator.Play("debugOn");
                    }
                    else
                    {
                        // Play "debugOff" animation, the animation event will handle disabling
                        debugEnabled = false;
                        animator.Play("debugOff");
                    }
                }
            }
        }
    }

    // Optional: Expose a public method to disable the debug menu
    public void DisableDebugMenu()
    {
        if (debug != null)
        {
            debug.SetActive(false);
        }
    }
}
