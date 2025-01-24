using System.Collections.Generic;
using UnityEngine;

public class ObjectStorageUnpack : MonoBehaviour, IInteractable
{
    public bool interactable = true;
    public IReceptacle container; 
    public void Interact()
    {
        if (container != null)
        {
            foreach(GameObject item in container.GetList())
            {
                //Animation to shoot out all items
                Instantiate(item);
            }
        }
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    public void OnHoverEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }
}
