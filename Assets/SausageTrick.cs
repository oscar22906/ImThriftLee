using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageTrick : MonoBehaviour, IDraggable
{
    [SerializeField] private Animator trickAnimator;
    private SpriteRenderer spriteRenderer;
    private Color color;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null )
        {
            color = spriteRenderer.color;
        }
    }

    public bool isDraggable()
    {
        return true;
    }

    public void OnDragBegin()
    {
        CursorManager.Instance.HideCursor();
        if (color != null)
        {
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }
        trickAnimator.SetBool("Dragging", true);
    }

    public void OnDragEnd()
    {
        trickAnimator.SetBool("Dragging", false);
        if (color != null)
        {
            spriteRenderer.color = color;
        }
        CursorManager.Instance.ShowCursor();
    }

    public void OnHoverEnter()
    {
        return;
    }

    public void OnHoverExit()
    {
        return;
    }
}
