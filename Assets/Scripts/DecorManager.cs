using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorManager : MonoBehaviour
{
    [SerializeField] private Sprite[] bedroomSprites;
    [SerializeField] private Sprite[] decorSprites;
    [SerializeField] private Sprite[] shelfSprites;

    private Image bedroomImage;
    private Image decorImage;
    private Image shelfImage;

    private const string BedroomKey = "BedroomIndex";
    private const string DecorKey = "DecorIndex";
    private const string ShelfKey = "ShelfIndex";

    private Dictionary<string, int> spriteIndexes = new Dictionary<string, int>();

    private void Awake()
    {
        bedroomImage = GameObject.FindGameObjectWithTag("Bedroom")?.GetComponent<Image>();
        decorImage = GameObject.FindGameObjectWithTag("Decor")?.GetComponent<Image>();
        shelfImage = GameObject.FindGameObjectWithTag("Shelf")?.GetComponent<Image>();

        LoadItems();
        ApplySprites();
    }

    private void NextSprite(string key, Sprite[] sprites, Image image)
    {
        if (sprites.Length == 0) return;

        int currentIndex = spriteIndexes.ContainsKey(key) ? spriteIndexes[key] : 0;
        currentIndex = (currentIndex + 1) % sprites.Length;

        image.sprite = sprites[currentIndex];
        spriteIndexes[key] = currentIndex;

        PlayerPrefs.SetInt(key, currentIndex);
        PlayerPrefs.Save();
    }

    private void ApplySprites()
    {
        bedroomImage.sprite = bedroomSprites[spriteIndexes[BedroomKey]];
        decorImage.sprite = decorSprites[spriteIndexes[DecorKey]];
        shelfImage.sprite = shelfSprites[spriteIndexes[ShelfKey]];
    }

    private void LoadItems()
    {
        spriteIndexes[BedroomKey] = PlayerPrefs.GetInt(BedroomKey, 0);
        spriteIndexes[DecorKey] = PlayerPrefs.GetInt(DecorKey, 0);
        spriteIndexes[ShelfKey] = PlayerPrefs.GetInt(ShelfKey, 0);
    }

    public void NextBedroomSprite() => NextSprite(BedroomKey, bedroomSprites, bedroomImage);
    public void NextDecorSprite() => NextSprite(DecorKey, decorSprites, decorImage);
    public void NextShelfSprite() => NextSprite(ShelfKey, shelfSprites, shelfImage);
}
