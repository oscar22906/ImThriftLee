using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using VInspector;

public class ShopManager : MonoBehaviour
{
    public bool rol = false;

    IUIAnimate[] iUIAnimates;
    Animator animator;
    public float disableDelay;

    public GameObject assets;

    public Shop shop;

    bool disabling = false;

    public AudioSource audioSource;

    public AudioClip[] welcomeClips;
    public AudioClip[] purchaseClips;
    public AudioClip[] minigameCompleteClips;
    public AudioClip[] goodbyeClips;

    [SerializeField] private float minBlinkTime = 2f;
    [SerializeField] private float maxBlinkTime = 5f;

    [SerializeField] private float minRolTime = 2f;
    [SerializeField] private float maxRolTime = 5f;
    private Coroutine blinkCoroutine;
    private Coroutine rolCoroutine;

    private bool isEnabled = false;


    private void OnEnable()
    {
        ItemDisplay.OnItemPurchased += PlayPurchaseSound;
        RipMinigame.OnMinigameEnd += PlayMinigameCompleteSound;
        iUIAnimates = GetComponentsInChildren<IUIAnimate>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(WaitToEnable());
        StartBlinking();
        StartRolRoutine();
    }

    private void OnDisable()
    {
        ItemDisplay.OnItemPurchased -= PlayPurchaseSound;
        RipMinigame.OnMinigameEnd -= PlayMinigameCompleteSound;
        StopBlinking();
        if (rol)
            StopRolRoutine();
    }

    [Button]
    public void ShowShop()
    {
        if (disabling)
        {
            print("Can't show, being disabled");
            return;
        }
        MapManager.Instance.currentShop = this;
        isEnabled = true;
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
        if (rol)
            StartRolRoutine();
    }

    [Button]
    public void HideShop()
    {
        if (disabling)
            return;
        isEnabled = false;
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
        StopRolRoutine();
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
    public void EnableButtonColliders()
    {
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = true;
        }
        GetComponent<Collider2D>().enabled = false;
    }
    public void DisableButtonColliders()
    {
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = false;
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

    private void StartRolRoutine()
    {
        if (rolCoroutine == null)
        {
            rolCoroutine = StartCoroutine(RolRoutine());
        }
    }

    private void StopRolRoutine()
    {
        if (rolCoroutine != null)
        {
            StopCoroutine(rolCoroutine);
            rolCoroutine = null;
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

    IEnumerator RolRoutine()
    {
        while (assets.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minRolTime, maxRolTime));
            if (animator != null && assets.activeSelf)
            {
                animator.SetTrigger("Rol");
            }
        }
    }
    public void PlayWelcomeSound() => PlayRandomClip(welcomeClips);

    public void PlayPurchaseSound() => PlayRandomClip(purchaseClips);
    public void PlayMinigameCompleteSound() => PlayRandomClip(minigameCompleteClips);

    public void PlayGoodbyeSound() => PlayRandomClip(goodbyeClips);

    private void PlayRandomClip(AudioClip[] clips)
    {
        if (!isEnabled && clips != goodbyeClips)
            return;
        if (clips.Length > 0 && audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
