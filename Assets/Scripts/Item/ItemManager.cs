using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    private void Awake()
    {
        if (itemDatabase != null)
        {
            itemDatabase.Initialize();
        }
    }
}
