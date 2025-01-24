using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDraggable : MonoBehaviour, IDraggable
{
    public bool canDrag;

    [SerializeField] private Color colour;
    [SerializeField] private Color dragColour;
    public bool isDraggable()
    {
        return canDrag;
    }

    public void OnDragBegin()
    {
        Debug.Log("Started Dragging");
        GetComponent<SpriteRenderer>().color = dragColour;
    }

    public void OnDragEnd()
    {
        Debug.Log("Stopped Dragging");
        GetComponent<SpriteRenderer>().color = colour;
    }

    public void OnHoverEnter()
    {
        
    }

    public void OnHoverExit()
    {
        
    }
}
