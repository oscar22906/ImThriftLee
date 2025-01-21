using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        DetectHover();
        DetectClick();
    }

    void DetectHover()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Handle hover actions
                if (currentInteractable != interactable)
                {
                    // hovering over a new object
                    currentInteractable = interactable;
                    OnHoverStart(interactable);
                }
            }
            else if (currentInteractable != null)
            {
                // no longer hovering over any interactable
                OnHoverEnd(currentInteractable);
                currentInteractable = null;
            }
        }
        else if (currentInteractable != null)
        {
            // no object under the mouse
            OnHoverEnd(currentInteractable);
            currentInteractable = null;
        }
    }

    void DetectClick()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void OnHoverStart(IInteractable interactable)
    {
        interactable.OnHoverEnter();
    }

    void OnHoverEnd(IInteractable interactable)
    {
        interactable.OnHoverExit();
    }
}
