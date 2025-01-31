using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class FlipUp : MonoBehaviour, IUIAnimate
{
    public float tweenDuration = 1.0f;
    public float setValue;
    public float fadeDuration = 1.0f;

    public float overshoot = 1.0f;
    public float period = 2f;

    SpriteRenderer[] spriteRenderers;
    private List<Material> materials = new List<Material>();

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            materials.Add(spriteRenderer.material);
        }
    }
    private void Start()
    {

    }

    public void AnimateEnable()
    {
        AnimateFadeIn(fadeDuration);
        Animate();
    }
    public void AnimateDisable()
    {
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
            .SetEase(Ease.OutElastic, overshoot, period);
        }
    }

    [Button]
    public void SetValue()
    {
        foreach (Material material in materials)
        {
            material.SetFloat("_Strength", setValue);
        }
    }

    public void AnimateFadeIn(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            sr.color = new Color(color.r, color.g, color.b, 0);
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
            .SetEase(Ease.InQuad);
        }
    }
}
