using UnityEngine;
using VInspector;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public ShopManager redShop;
    public ShopManager orangeShop;
    public ShopManager blueShop;

    private bool isRedShopEnabled;
    private bool isOrangeShopEnabled;
    private bool isBlueShopEnabled;

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

    public bool IsRedShopEnabled => isRedShopEnabled;
    public bool IsOrangeShopEnabled => isOrangeShopEnabled;
    public bool IsBlueShopEnabled => isBlueShopEnabled;

    public void EnableShop(ShopManager shop)
    {
        DisableAllShops();

        if (shop == redShop) isRedShopEnabled = true;
        if (shop == orangeShop) isOrangeShopEnabled = true;
        if (shop == blueShop) isBlueShopEnabled = true;

        shop.gameObject.SetActive(true);
    }

    public void DisableShop(ShopManager shop)
    {
        if (shop == redShop) isRedShopEnabled = false;
        if (shop == orangeShop) isOrangeShopEnabled = false;
        if (shop == blueShop) isBlueShopEnabled = false;

        shop.HideShop();
    }

    [Button]
    private void DisableAllShops()
    {
        isRedShopEnabled = false;
        isOrangeShopEnabled = false;
        isBlueShopEnabled = false;

        redShop.HideShop();
        orangeShop.HideShop();
        blueShop.HideShop();
    }

    public void DeactivateAllShops()
    {
        redShop.gameObject.SetActive(false);
        orangeShop.gameObject.SetActive(false);
        blueShop.gameObject.SetActive(false);
    }

    [Button]
    public void ActivateRedShop() => EnableShop(redShop);

    [Button]
    public void ActivateOrangeShop() => EnableShop(orangeShop);

    [Button]
    public void ActivateBlueShop() => EnableShop(blueShop);
}
