using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class DecorMenu : MonoBehaviour
{
    public float tweenDuration = 1.0f;
    public float fadeDuration = 1.0f;
    public float overshoot = 1.0f;
    public float period = 2f;

    private SpriteRenderer[] spriteRenderers;
    private Collider2D[] childColliders;
    private List<Material> materials = new List<Material>();
    private bool menuEnabled = false;
    private bool isAnimating = false;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        childColliders = GetComponentsInChildren<Collider2D>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            materials.Add(spriteRenderer.material);
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }
    private void Start()
    {
        DisableColliders();
    }
    [Button]
    public void ToggleMenu()
    {
        if (isAnimating) return;

        menuEnabled = !menuEnabled;

        if (menuEnabled)
            AnimateEnable();
        else
            AnimateDisable();
    }

    [Button]
    public void AnimateEnable()
    {
        if (isAnimating) return;
        isAnimating = true;

        foreach (Collider2D col in childColliders)
        {
            col.enabled = true;
        }

        AnimateFadeIn(fadeDuration);
        Animate();
    }

    [Button]
    public void AnimateDisable()
    {
        if (isAnimating) return;
        isAnimating = true;

        AnimateFadeOut(fadeDuration);
        AnimateShrinkAndDisable();
    }

    [Button]
    public void Animate()
    {
        foreach (Material material in materials)
        {
            float start = -15f;
            DOTween.To(
                () => start,
                x =>
                {
                    start = x;
                    material.SetFloat("_Strength", start);
                },
                0.0f,
                tweenDuration
            )
            .SetEase(Ease.OutElastic, overshoot, period)
            .OnComplete(() => {
                EnableColliders();
                isAnimating = false;
            });
        }
    }

    public void AnimateFadeIn(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            sr.color = new Color(1f, 1f, 1f, 0f);
            sr.DOFade(1, fadeDuration);
        }
    }

    public void AnimateFadeOut(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.DOFade(0, fadeDuration);
        }
    }
    public void AnimateShrinkAndDisable()
    {
        float totalDelay = materials.Count * 0.1f;
        int completedTweens = 0;

        for (int i = 0; i < materials.Count; i++)
        {
            float delay = i * 0.1f;
            DOTween.To(
                () => 1f,
                x => materials[i].SetFloat("_Strength", x),
                0f,
                tweenDuration
            )
            .SetDelay(delay)
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                completedTweens++;
                if (completedTweens >= materials.Count)
                {
                    DisableColliders();
                    isAnimating = false;
                }
            });
        }

        DOTween.To(
            () => 1f,
            x => materials[materials.Count - 1].SetFloat("_Strength", x),
            0f,
            tweenDuration
        )
        .SetDelay(materials.Count * 0.1f)
        .SetEase(Ease.InQuad)
        .OnComplete(() =>
        {
            isAnimating = false;
        });
    }


    private void EnableColliders()
    {
        foreach (Collider2D col in childColliders)
        {
            col.enabled = true;
        }
    }

    private void DisableColliders()
    {
        foreach (Collider2D col in childColliders)
        {
            col.enabled = false;
        }
    }
}
