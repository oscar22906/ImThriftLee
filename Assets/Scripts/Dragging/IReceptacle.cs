using System.Collections.Generic;
using UnityEngine;

public interface IReceptacle
{
    List<GameObject> GetList();
    bool CanAccept(IDraggable draggable);

    void OnItemRecieve(IDraggable draggable);
}
