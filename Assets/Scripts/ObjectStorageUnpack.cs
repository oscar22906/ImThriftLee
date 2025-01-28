using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VInspector;

public class ObjectStorageUnpack : MonoBehaviour, IInteractable
{
    public bool interactable = true;
    [SerializeField] private GameObject storage;

    IReceptacle container;
    private void OnValidate()
    {
        if (storage != null)
        {
            container = storage.GetComponent<IReceptacle>();
        }
    }
    public void Interact()
    {
        if (container != null)
        {
            // Cool effect
            RetrieveItem();
        }
    }
    [Button]
    void RetrieveItem()
    {
        if (container != null)
        {
            if (container.GetList().Count > 0)
            {
                // Retrieve the first item from storage
                GameObject item = container.GetList()[0];
                container.GetList().RemoveAt(0);

                // Reactivate
                item.SetActive(true);
            }
        }
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }
}
