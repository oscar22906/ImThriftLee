using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCursorTest : MonoBehaviour, IDraggable
{
    public bool debugMsg = false;
    public bool canDrag;
    [Tooltip("Toggles custom drag sprite on drag")]
    public bool dragSprite;
    public GameObject grasp;
    private void Start()
    {
        grasp.SetActive(false);
    }
    public bool isDraggable()
    {
        return canDrag;
    }

    public void OnDragBegin()
    {
        if (debugMsg)
        {
            Debug.Log("Started Dragging");
        }
        if (dragSprite)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
            CursorManager.Instance.HideCursor();
            grasp.SetActive(true);
        }
    }

    public void OnDragEnd()
    {
        if (debugMsg)
        {
            Debug.Log("Stopped Dragging");
        }
        if (dragSprite)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            CursorManager.Instance.ShowCursor();

            grasp.SetActive(false);
        }
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }
}
