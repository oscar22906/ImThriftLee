using UnityEngine;

public class RipTarget : MonoBehaviour, IInteractable
{
    private RipMinigame minigameManager;

    public void Initialize(RipMinigame manager)
    {
        minigameManager = manager;
    }
    public void Interact()
    {
        if (minigameManager != null)
        {
            minigameManager.OnTargetClicked(transform.position);
            Destroy(gameObject);
        }
    }

    public bool IsInteractable()
    {
        return true;
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }

}
