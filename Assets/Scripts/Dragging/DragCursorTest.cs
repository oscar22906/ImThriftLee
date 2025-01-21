using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragCursorTest : MonoBehaviour, IDraggable
{
    [SerializeField] private bool debugMsg = false;
    [SerializeField] private bool canDrag;
    [Tooltip("Toggles custom drag sprite on drag")]
    [SerializeField] private bool dragSprite;
    [SerializeField] private GameObject grasp;

    [SerializeField] private Color colour;
    [SerializeField] private Color dragColour;

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
