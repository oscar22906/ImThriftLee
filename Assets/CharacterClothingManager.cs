using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CharacterClothingManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer baseRenderer;
    [SerializeField] private SpriteRenderer hairRenderer;
    [SerializeField] private SpriteRenderer torsoRenderer;
    [SerializeField] private SpriteRenderer legsRenderer;
    [SerializeField] private SpriteRenderer accessoryRenderer;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Sprite[] baseSprites;
    [SerializeField] private Sprite[] hairSprites;

    private const string PoseKey = "PoseIndex";
    private const string HairEnabledKey = "HairEnabled";
    private const string TorsoKey = "TorsoEquipped";
    private const string LegsKey = "LegsEquipped";
    private const string AccessoriesKey = "AccessoriesEquipped";

    private int currentPose;
    private bool hairEnabled;

    private void Awake()
    {
        currentPose = PlayerPrefs.GetInt(PoseKey, 0);
        hairEnabled = PlayerPrefs.GetInt(HairEnabledKey, 1) == 1;

        UpdateBaseAndHairSprite();
        LoadClothing(BodyPart.Torso, torsoRenderer, TorsoKey);
        LoadClothing(BodyPart.Legs, legsRenderer, LegsKey);
        LoadClothing(BodyPart.Accessories, accessoryRenderer, AccessoriesKey);
    }

    [Button("Cycle Torso")] public void CycleTorso() => CycleClothing(BodyPart.Torso, torsoRenderer, TorsoKey);
    [Button("Cycle Legs")] public void CycleLegs() => CycleClothing(BodyPart.Legs, legsRenderer, LegsKey);
    [Button("Cycle Accessories")] public void CycleAccessories() => CycleClothing(BodyPart.Accessories, accessoryRenderer, AccessoriesKey);
    [Button("Next Pose")] public void NextPose() => ChangePose((currentPose + 1) % baseSprites.Length);
    [Button("Toggle Hair")] public void ToggleHair() => SetHairEnabled(!hairEnabled);
    [Button("Randomize Outfit")] public void RandomizeOutfit() => SetRandomClothing();

    private void CycleClothing(BodyPart part, SpriteRenderer renderer, string key)
    {
        List<Clothing> ownedClothes = itemDatabase.GetOwnedItems().FindAll(item => item.GetBodyPart() == part);
        if (ownedClothes.Count == 0) return;

        string currentItemID = PlayerPrefs.GetString(key, "");
        int currentIndex = ownedClothes.FindIndex(item => item.itemID == currentItemID);
        int newIndex = (currentIndex + 1) % ownedClothes.Count;

        Clothing newClothing = ownedClothes[newIndex];
        PlayerPrefs.SetString(key, newClothing.itemID);
        PlayerPrefs.Save();
        renderer.sprite = GetClothingSprite(newClothing);
    }

    private void ChangePose(int newPose)
    {
        currentPose = newPose;
        PlayerPrefs.SetInt(PoseKey, currentPose);
        PlayerPrefs.Save();
        UpdateBaseAndHairSprite();
        LoadClothing(BodyPart.Torso, torsoRenderer, TorsoKey);
        LoadClothing(BodyPart.Legs, legsRenderer, LegsKey);
        LoadClothing(BodyPart.Accessories, accessoryRenderer, AccessoriesKey);
    }

    private void SetHairEnabled(bool enabled)
    {
        hairEnabled = enabled;
        PlayerPrefs.SetInt(HairEnabledKey, hairEnabled ? 1 : 0);
        PlayerPrefs.Save();
        hairRenderer.enabled = hairEnabled;
    }

    private void SetRandomClothing()
    {
        AssignRandomClothing(BodyPart.Torso, torsoRenderer, TorsoKey);
        AssignRandomClothing(BodyPart.Legs, legsRenderer, LegsKey);
        AssignRandomClothing(BodyPart.Accessories, accessoryRenderer, AccessoriesKey);
    }

    private void AssignRandomClothing(BodyPart part, SpriteRenderer renderer, string key)
    {
        List<Clothing> ownedClothes = itemDatabase.GetOwnedItems().FindAll(item => item.GetBodyPart() == part);
        if (ownedClothes.Count == 0) return;

        Clothing randomItem = ownedClothes[Random.Range(0, ownedClothes.Count)];
        PlayerPrefs.SetString(key, randomItem.itemID);
        PlayerPrefs.Save();
        renderer.sprite = GetClothingSprite(randomItem);
    }

    private void UpdateBaseAndHairSprite()
    {
        baseRenderer.sprite = baseSprites.Length > currentPose ? baseSprites[currentPose] : null;
        hairRenderer.sprite = hairEnabled && hairSprites.Length > currentPose ? hairSprites[currentPose] : null;
    }

    private void LoadClothing(BodyPart part, SpriteRenderer renderer, string key)
    {
        string itemID = PlayerPrefs.GetString(key, "");
        Clothing clothing = itemDatabase.GetOwnedItems().Find(item => item.itemID == itemID);
        renderer.sprite = GetClothingSprite(clothing);
    }

    private Sprite GetClothingSprite(Clothing clothing)
    {
        return (clothing != null && clothing.sprites.Length > currentPose) ? clothing.sprites[currentPose] : null;
    }
}
