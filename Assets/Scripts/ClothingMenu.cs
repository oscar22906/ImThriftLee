using UnityEngine;
using UnityEngine.UI;

public class ClothingMenu : MonoBehaviour
{
    private Animator animator;
    private bool menuOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OpenMenu()
    {
        menuOpen = true;
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
    }
    private void CloseMenu()
    {
        menuOpen = false;
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
    }

    public void ToggleMenu()
    {
        if (animator == null)
            return;
        if (!menuOpen)
            OpenMenu();
        else CloseMenu();
    }
}
