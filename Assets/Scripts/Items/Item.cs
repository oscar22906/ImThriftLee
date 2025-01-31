using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string itemName;            // Name of the item
    public string description;         // Description
    public Sprite icon;                // Icon
    public Sprite sprite;
    public ItemType itemType;          // Enum of item type
    public bool canPickUp;

    [Header("Food Properties")]
    public float foodSaturation;       // Amount of saturation the food provides (only for Food)

    [Header("Tool Properties")]
    public float durability;           // Durability

    public bool IsEdible()
    {
        return itemType == ItemType.Food;
    }

    public bool IsToolOrWeapon()
    {
        return itemType == ItemType.Tool;
    }

}
public enum ItemType
{
    Food,
    Tool,
    Generic
}
