using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "DressUp/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Clothing> allItems = new List<Clothing>();
    private HashSet<string> ownedItems = new HashSet<string>();

    private const string SaveKey = "OwnedItems";

    public void Initialize()
    {
        LoadItems();
    }

    public bool PurchaseItem(Clothing item)
    {
        if (item == null || ownedItems.Contains(item.itemID)) return false;

        ownedItems.Add(item.itemID);
        SaveItems();
        return true;
    }

    public bool OwnsItem(Clothing item)
    {
        return item != null && ownedItems.Contains(item.itemID);
    }

    public List<Clothing> GetOwnedItems()
    {
        return allItems.FindAll(item => item != null && ownedItems.Contains(item.itemID));
    }

    public List<Clothing> GetUnownedItems()
    {
        return allItems.FindAll(item => item != null && !ownedItems.Contains(item.itemID));
    }

    public List<Clothing> GetUnownedItemsByShop(Shop shop)
    {
        return allItems.FindAll(item => item != null && !ownedItems.Contains(item.itemID) && item.GetShop() == shop);
    }

    public List<Clothing> GetOwnedItemsByShop(Shop shop)
    {
        return allItems.FindAll(item => item != null && ownedItems.Contains(item.itemID) && item.GetShop() == shop);
    }

    private void SaveItems()
    {
        PlayerPrefs.SetString(SaveKey, string.Join(",", ownedItems));
        PlayerPrefs.Save();
    }

    private void LoadItems()
    {
        ownedItems.Clear();
        string savedData = PlayerPrefs.GetString(SaveKey, "");
        if (!string.IsNullOrEmpty(savedData))
        {
            string[] items = savedData.Split(',');
            foreach (string item in items)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    ownedItems.Add(item);
                }
            }
        }
    }
}

[System.Serializable]
public class Clothing
{
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public Sprite[] sprites; // all animation frames for the item in order

    [SerializeField] private BodyPart bodyPart;
    [SerializeField] private OutfitSet outfitSet;
    [SerializeField] private Shop shop;

    public BodyPart GetBodyPart() => bodyPart;
    public OutfitSet GetOutfitSet() => outfitSet;
    public Shop GetShop() => shop;
}

public enum OutfitSet
{
    Default
}

public enum BodyPart
{
    Accessories,
    Torso,
    Legs,
    Feet
}

public enum Shop
{
    RedShop,
    OrangeShop,
    BlueShop
}
