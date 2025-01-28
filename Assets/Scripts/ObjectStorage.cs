using System.Collections.Generic;
using UnityEngine;

public class ObjectStorage : MonoBehaviour, IReceptacle
{
    [SerializeField] private bool acceptAll;
    [SerializeField] private GameObject button;

    public List<GameObject> objects = new List<GameObject>();

    IInteractable openButton;
    public bool CanAccept(IDraggable draggable)
    {
        return acceptAll;
    }
    private void Start()
    {
        if (button != null)
        {
            openButton = button.GetComponent<IInteractable>();
        }
        UpdateContainer();
    }

    public void OnItemRecieve(IDraggable draggable)
    {
        if (draggable is MonoBehaviour draggableObject)
        {
            draggableObject.gameObject.SetActive(false);
            objects.Add(draggableObject.gameObject);
        }
        UpdateContainer();
    }

    void UpdateContainer()
    {
        if (openButton is MonoBehaviour button)
        {
            if (objects.Count >= 1)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

    }

    public List<GameObject> GetList()
    {
        return objects;
    }
}
