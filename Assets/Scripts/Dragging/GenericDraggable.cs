using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDraggable : MonoBehaviour, IDraggable
{
    public bool canDrag;
    public bool isDraggable()
    {
        return canDrag;
    }

    public void OnDragBegin()
    {
        Debug.Log("Started Dragging");
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void OnDragEnd()
    {
        Debug.Log("Stopped Dragging");
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnHoverEnter()
    {
        
    }

    public void OnHoverExit()
    {
        
    }
}
