using System.Collections.Generic;
using UnityEngine;

public class StickerBin : MonoBehaviour, IReceptacle
{
    public bool CanAccept(IDraggable draggable)
    {
        if (draggable != null && draggable.Type == IDraggable.DragType.Sticker) // only accept stickers
        {
            return true;
        }
        else
        {
            print("NOT A STICKER !!!");
            return false;
        }
    }

    public List<GameObject> GetList()
    {
        return null;
    }

    public void OnItemRecieve(IDraggable draggable)
    {
        if (draggable is MonoBehaviour draggableObject)
        {
            Destroy(draggableObject.gameObject);
        }
    }

}
