using System.Collections;
using UnityEngine;
using VInspector;

public class ShopManager : MonoBehaviour
{
    IUIAnimate[] iUIAnimates;
    Animator animator;
    public float disableDelay;

    public GameObject assets;

    public Shop shop;

    bool disabling = false;

    [SerializeField] private float minBlinkTime = 2f;
    [SerializeField] private float maxBlinkTime = 5f;
    private Coroutine blinkCoroutine;

    private void OnEnable()
    {
        iUIAnimates = GetComponentsInChildren<IUIAnimate>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(WaitToEnable());
        StartBlinking();
    }

    private void OnDisable()
    {
        StopBlinking();
    }

    [Button]
    public void ShowShop()
    {
        if (disabling)
        {
            print("Can't show, being disabled");
            return;
        }
        print("Showing shop");
        MapManager.Instance.DisableColliders();
        assets.SetActive(true);
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateEnable();
        }
        if (animator != null)
        {
            animator.SetTrigger("PopUp");
        }
        ItemDisplay.Instance.OpenShop(shop);
        StartBlinking();
    }

    [Button]
    public void HideShop()
    {
        if (disabling)
            return;
        MapManager.Instance.EnableColliders();
        if (animator != null)
        {
            animator.SetTrigger("Down");
        }
        foreach (IUIAnimate anim in iUIAnimates)
        {
            anim.AnimateDisable();
        }
        StartCoroutine(WaitToDisable());
        StopBlinking();
    }

    public void UpdateShop()
    {
        if (this == MapManager.Instance?.redShop)
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
    IEnumerator WaitToDisable()
    {
        disabling = true;
        yield return new WaitForSeconds(disableDelay);
        assets.SetActive(false);
        disabling = false;
    }

    private void StartBlinking()
    {
        if (blinkCoroutine == null)
        {
            blinkCoroutine = StartCoroutine(BlinkRoutine());
        }
    }

    private void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    IEnumerator BlinkRoutine()
    {
        while (assets.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minBlinkTime, maxBlinkTime));
            if (animator != null && assets.activeSelf)
            {
                animator.SetTrigger("Blink");
            }
        }
    }
}
