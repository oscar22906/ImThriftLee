using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using VInspector;
using System;

public class ItemDisplay : MonoBehaviour
{
    public static ItemDisplay Instance { get; private set; }

    public ItemDatabase itemDatabase;
    public SpriteRenderer itemDisplay;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fadeDuration = 2f;
    private List<Clothing> availableItems;
    private int currentIndex = 0;
    private Vector3 originalPosition;
    [HideInInspector]
    public bool isTweening = false;

    [SerializeField] private float overshoot;
    [SerializeField] private float period;

    [SerializeField] private GameObject ripMinigameTarget;

    public static event Action OnItemPurchased;

    private SpriteRenderer newDisplay;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        originalPosition = itemDisplay.transform.position;
        itemDisplay.color = new Color(1, 1, 1, 0); // Set transparent by default
    }

    public void OpenShop(Shop shop)
    {
        availableItems = itemDatabase.GetUnownedItemsByShop(shop);
        if (availableItems.Count > 0)
        {
            currentIndex = 0;
            UpdateDisplay();
            itemDisplay.DOFade(1, fadeDuration); // Fade in
        }
    }

    public void CloseShop()
    {
        DOTween.Kill(itemDisplay.transform);
        if (newDisplay != null)
            DOTween.Kill(newDisplay.gameObject);
        isTweening = false;
        newDisplay.DOFade(0, 1f);
        itemDisplay.DOFade(0, 1f).OnComplete(() =>
        {
            availableItems.Clear();
        });
    }


    [Button]
    public void ScrollLeft()
    {
        if (availableItems.Count == 0 || isTweening) return;
        isTweening = true;

        SpriteRenderer newItem = Instantiate(itemDisplay, originalPosition + Vector3.left * 10, Quaternion.identity);
        newDisplay = newItem;
        newItem.sprite = availableItems[(currentIndex - 1 + availableItems.Count) % availableItems.Count].itemIcon;
        newItem.color = new Color(1, 1, 1, 1); // Ensure visibility

        itemDisplay.transform.DOMoveX(originalPosition.x + 10, duration);
        newItem.transform.DOMoveX(originalPosition.x, duration)
            .SetEase(Ease.OutElastic, overshoot, period)
            .OnComplete(() =>
            {
                Destroy(itemDisplay.gameObject);
                itemDisplay = newItem;
                currentIndex = (currentIndex - 1 + availableItems.Count) % availableItems.Count;
                isTweening = false;
            });
    }

    [Button]
    public void ScrollRight()
    {
        if (availableItems.Count == 0 || isTweening) return;
        isTweening = true;

        SpriteRenderer newItem = Instantiate(itemDisplay, originalPosition + Vector3.right * 10, Quaternion.identity);
        newDisplay = newItem;
        newItem.sprite = availableItems[(currentIndex + 1) % availableItems.Count].itemIcon;
        newItem.color = new Color(1, 1, 1, 1); // Ensure visibility

        itemDisplay.transform.DOMoveX(originalPosition.x - 10, duration);
        newItem.transform.DOMoveX(originalPosition.x, duration)
            .SetEase(Ease.OutElastic, overshoot, period)
            .OnComplete(() =>
            {
                Destroy(itemDisplay.gameObject);
                itemDisplay = newItem;
                currentIndex = (currentIndex + 1) % availableItems.Count;
                isTweening = false;
            });
    }

    private void UpdateDisplay()
    {
        if (availableItems.Count > 0)
        {
            itemDisplay.sprite = availableItems[currentIndex].itemIcon;
        }
    }

    public void BuyItem()
    {
        if (availableItems.Count == 0 || isTweening) return;

        Clothing itemToBuy = availableItems[currentIndex];
        bool success = itemDatabase.PurchaseItem(itemToBuy);

        if (success)
        {
            StartMinigame(itemToBuy);
        }
        else
        {
            Debug.Log("Item already owned or purchase failed.");
        }
    }
    private void StartMinigame(Clothing item)
    {
        GameObject minigameInstance = Instantiate(ripMinigameTarget, transform.position, Quaternion.identity);
        RipMinigame minigameScript = minigameInstance.GetComponent<RipMinigame>();

        if (minigameScript != null)
        {
            minigameScript.SetupMinigame(item);
        }
    }
    public void OnMinigameSuccess(Clothing item)
    {
        availableItems = itemDatabase.GetUnownedItemsByShop(item.GetShop());
        OnItemPurchased?.Invoke();

        UpdateDisplay();
    }
}
