using UnityEngine;

public class ElementManager : MonoBehaviour
{
    private IElement currentElement;
    void Update()
    {
        DetectHover();
    }
    void DetectHover()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


        if (hit.collider != null)
        {
            IElement element = hit.collider.GetComponent<IElement>();

            if (element != null)
            {
                if (currentElement != element)
                {
                    currentElement = element;
                    OnHoverStart(element);
                }
            }
            else if (currentElement != null)
            {
                OnHoverEnd(currentElement);
                currentElement = null;
            }
        }
        else if (currentElement != null)
        {
            OnHoverEnd(currentElement);
            currentElement = null;
        }
    }

    void OnHoverStart(IElement element)
    {
        element.OnHoverEnter();
    }

    void OnHoverEnd(IElement element)
    {
        element.OnHoverExit();
    }
}
