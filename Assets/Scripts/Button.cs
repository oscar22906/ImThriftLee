using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using VInspector;

public class Button : MonoBehaviour, IInteractable
{
    [Header("Custom Click Event")]
    public UnityEvent onButtonClick;
    [SerializeField] private bool isInteractable;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Interact()
    {
        if (onButtonClick != null)
        {
            onButtonClick.Invoke();
        }
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public void OnHoverEnter()
    {
        return;
    }

    public void OnHoverExit()
    {
        return;
    }
    [Button]
    public void DisableButton()
    {
        isInteractable = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }
    }
    [Button]
    public void EnableButton()
    {
        isInteractable = true;
        if(spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
