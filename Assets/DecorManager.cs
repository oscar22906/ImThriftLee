using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{
    [SerializeField] Sprite[] bedroomSprites;
    [SerializeField] Sprite[] decorSprites;
    [SerializeField] Sprite[] shelfSprites;

    private SpriteRenderer bedroom;
    private SpriteRenderer decor;
    private SpriteRenderer shelf;

    private const string BedroomKey = "BedroomIndex";
    private const string DecorKey = "DecorIndex";
    private const string ShelfKey = "ShelfIndex";

    private HashSet<string> ownedItems = new HashSet<string>();

    private void Awake()
    {
        bedroom = GameObject.FindGameObjectWithTag("Bedroom").GetComponent<SpriteRenderer>();
        decor = GameObject.FindGameObjectWithTag("Decor").GetComponent<SpriteRenderer>();
        shelf = GameObject.FindGameObjectWithTag("Shelf").GetComponent<SpriteRenderer>();
    }

    private void NextSprite(Sprite[] type)
    {
        
    }

    private void SaveItems()
    {
        PlayerPrefs.SetString(BedroomKey, string.Join(",", ownedItems));
        PlayerPrefs.Save();
    }

    private void LoadItems()
    {
        string savedData = PlayerPrefs.GetString(BedroomKey, "");
        if (!string.IsNullOrEmpty(savedData))
        {
            string[] items = savedData.Split(',');
            foreach (string item in items)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    
                }
            }
        }
    }

    public void NextBedroomSprite() => NextSprite(bedroomSprites);
    public void NextDecorSprite() => NextSprite(decorSprites);
    public void NextShelfSprite() => NextSprite(shelfSprites);
}
