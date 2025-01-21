using UnityEngine;

public class DraggingManager : MonoBehaviour
{
    private IDraggable currentDraggable;
    private IDial currentDial;
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    private Camera mainCamera;

    [SerializeField] private float precisionFactor = 0.5f; // apply when shift is held

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        DetectDragOrDial();
    }

    void DetectDragOrDial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                IDraggable draggable = hit.collider.GetComponent<IDraggable>();
                if (draggable != null && draggable.isDraggable())
                {
                    currentDraggable = draggable;
                    currentDial = draggable as IDial; // Check if it's also a dial
                    if (currentDraggable is MonoBehaviour draggableObject)
                    {
                        draggableObject.GetComponent<Collider2D>().enabled = false;
                    }
                    currentDraggable.OnDragBegin();
                    isDragging = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }

        if (isDragging && currentDraggable != null)
        {
            if (currentDial != null)
            {
                HandleDialRotation();
            }
            else
            {
                DragObject();
            }

            if (Input.GetMouseButtonUp(0))
            {
                HandleDragEnd();
            }
        }
    }

    void HandleDialRotation()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 delta = currentMousePosition - lastMousePosition;

        // precision factor if shift is held
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            delta *= precisionFactor;
        }

        currentDial.OnDialDrag(delta.x, delta.y);
        lastMousePosition = currentMousePosition;
    }

    void DragObject()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // assuming 2D

        if (currentDraggable is MonoBehaviour draggableObject)
        {
            Vector3 currentPos = draggableObject.transform.position;
            Vector3 newPos = mousePos;

            // precision factor if shift is held
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                newPos = Vector3.Lerp(currentPos, newPos, precisionFactor); // doest actly work :(
            }

            draggableObject.transform.position = newPos;
        }
    }

    void HandleDragEnd()
    {
        if (currentDial == null) // handle drops for non-dial objects
        {
            HandleDrop();
        }

        if (currentDraggable is MonoBehaviour draggableObject)
        {
            draggableObject.GetComponent<Collider2D>().enabled = true;
        }
        currentDraggable.OnDragEnd();
        currentDraggable = null;
        currentDial = null;
        isDragging = false;
    }

    void HandleDrop()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            Debug.Log("Dropped on " + hit.collider.gameObject);
            IReceptacle receptacle = hit.collider.GetComponent<IReceptacle>();
            if (receptacle != null && receptacle.CanAccept(currentDraggable))
            {
                receptacle.OnItemRecieve(currentDraggable);
                Debug.Log("Item dropped on valid receptacle.");
            }
            else
            {
                Debug.Log("Item cannot be dropped here.");
            }
        }
    }
}