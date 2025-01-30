using UnityEngine;
using VInspector;

public class ShopManager : MonoBehaviour
{
    IUIAnimate[] iUIAnimates;
    Animator animator;
    void Start()
    {
        iUIAnimates = GetComponentsInChildren<IUIAnimate>();
        animator = GetComponentInChildren<Animator>();

        HideShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button] 
    void ShowShop()
    {
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateEnable();
        }
        animator.SetTrigger("PopUp");
    }
    [Button]
    void HideShop()
    {
        animator.SetTrigger("Down");
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateDisable();
        }

    }
}
