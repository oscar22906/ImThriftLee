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

    ShopManager currentShop;

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

        if (shop == redShop)
        {
            isRedShopEnabled = true;
            orangeShop.UpdateShop();
            blueShop.UpdateShop();
        }
        if (shop == orangeShop)
        {
            isOrangeShopEnabled = true;
            redShop.UpdateShop();
            blueShop.UpdateShop();
        }
        if (shop == blueShop)
        {
            isBlueShopEnabled = true;
            redShop.UpdateShop();
            orangeShop.UpdateShop();
        }
        shop.ShowShop();
    }
    [Button]
    public void CloseCurrent()
    {
        ItemDisplay.Instance.CloseShop();
        isRedShopEnabled = false;
        isOrangeShopEnabled = false;
        isBlueShopEnabled = false;
        redShop.UpdateShop();
        orangeShop.UpdateShop();
        blueShop.UpdateShop();
    }

    private void DisableAllShops()
    {
        isRedShopEnabled = false;
        isOrangeShopEnabled = false;
        isBlueShopEnabled = false;
        //redShop.UpdateShop();
        //orangeShop.UpdateShop();
        //blueShop.UpdateShop();
    }

    public void DisableColliders()
    {
        redShop.gameObject.GetComponent<Collider2D>().enabled = false;
        orangeShop.gameObject.GetComponent<Collider2D>().enabled = false;
        blueShop.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void EnableColliders()
    {
        redShop.gameObject.GetComponent<Collider2D>().enabled = true;
        orangeShop.gameObject.GetComponent<Collider2D>().enabled = true;
        blueShop.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    [Button]
    public void ActivateRedShop() => EnableShop(redShop);

    [Button]
    public void ActivateOrangeShop() => EnableShop(orangeShop);

    [Button]
    public void ActivateBlueShop() => EnableShop(blueShop);
}
