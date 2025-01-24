using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectStorage : MonoBehaviour, IReceptacle
{
    [SerializeField] private bool acceptAll;

    IInteractable openButton;
    public Component component;

    public List<GameObject> objects = new List<GameObject>();
    public bool CanAccept(IDraggable draggable)
    {
        return acceptAll;
    }

    private void OnValidate()
    {
        if (component == IInteractable)
        {

        }
    }

    public void OnItemRecieve(IDraggable draggable)
    {
        if (draggable is MonoBehaviour draggableObject)
        {
            objects.Add(draggableObject.gameObject);
            Destroy(draggableObject.gameObject);
        }
        UpdateContainer();
    }

    void UpdateContainer()
    {
        if (openButton is MonoBehaviour button)
        {
            if (objects.Count <= 1)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

    }


    private void Start()
    {
        UpdateContainer();
    }

    public List<GameObject> GetList()
    {
        return objects;
    }
}
