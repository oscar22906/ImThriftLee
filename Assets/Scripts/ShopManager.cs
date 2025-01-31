using System.Collections;
using UnityEngine;
using VInspector;

public class ShopManager : MonoBehaviour
{
    IUIAnimate[] iUIAnimates;
    Animator animator;
    public float disableDelay;

    public GameObject assets;

    bool disabling = false; // stop multiple calls

    private void OnEnable()
    {
        iUIAnimates = GetComponentsInChildren<IUIAnimate>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(WaitToEnable());
        print(MapManager.Instance);
    }
    [Button] 
    public void ShowShop() // call to start all animations
    {
        if (disabling)
        {
            print("Can't show, being disabled");
            return;
        }
        print("Showing shop");
        assets.SetActive(true);
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateEnable();
        }
        if (animator != null)
        {
            animator.SetTrigger("PopUp");
        }
    }
    [Button]
    public void HideShop() // call to start hide animations
    {
        if (disabling)
            return;
        if (animator != null)
        {
            animator.SetTrigger("Down");
        }
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateDisable();
        }
        StartCoroutine(WaitToDisable());

    }

    public void UpdateShop()
    {
        if (this == MapManager.Instance?.redShop) // hide on start
        {
            if (!MapManager.Instance.IsRedShopEnabled)
                HideShop();
        }
        if (this == MapManager.Instance?.orangeShop)
        {
            if (!MapManager.Instance.IsOrangeShopEnabled)
                HideShop();
        }
        if (this == MapManager.Instance?.blueShop)
        {
            if (!MapManager.Instance.IsBlueShopEnabled)
                HideShop();
        }
    }
    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(1f);
        UpdateShop();
    }
    IEnumerator WaitToDisable() // wait until 
    {
        disabling = true;
        yield return new WaitForSeconds(disableDelay);
        assets.SetActive(false);
        disabling = false;
    }

}
