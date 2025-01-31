using UnityEngine;
using VInspector;

public class ShopManager : MonoBehaviour
{
    IUIAnimate[] iUIAnimates;
    Animator animator;
    void Start()
    {

    }

    private void OnEnable()
    {
        iUIAnimates = GetComponentsInChildren<IUIAnimate>();
        animator = GetComponentInChildren<Animator>();
        if (this == MapManager.Instance?.redShop)
        {
            if (!MapManager.Instance.IsRedShopEnabled)
                HideShop();
            else
                ShowShop();
        }
        if (this == MapManager.Instance?.orangeShop)
        {
            if (!MapManager.Instance.IsOrangeShopEnabled)
                HideShop();
            else
                ShowShop();
        }
        if (this == MapManager.Instance?.blueShop)
        {
            if (!MapManager.Instance.IsBlueShopEnabled)
                HideShop();
            else
                ShowShop();
        }
    }


    [Button] 
    public void ShowShop()
    {
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
    public void HideShop()
    {
        print("ahh"); //calling hide shop when its not meant to
        if (animator != null)
        {
            animator.SetTrigger("Down");
        }
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateDisable();
        }

    }
}
