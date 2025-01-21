using UnityEditor;
using UnityEngine;

// Scriptable Object containing basic information about the clothing item.

[CreateAssetMenu(fileName = "NewClothing", menuName = "Character/Clothing")]
public class Clothing : ScriptableObject
{
    public string clothingName;
    public Sprite[] sprites; // all animation frames for the item in order
    public BodyPart bodyPart;
    public OutfitSet outfitSet;
}
public enum OutfitSet
{
    Default
}
public enum BodyPart
{
    Head,
    Torso,
    Legs,
    Feet
}

