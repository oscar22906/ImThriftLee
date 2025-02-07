using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class CreditsMenu : MonoBehaviour
{
    public float tweenDuration = 1.0f;
    public float fadeDuration = 1.0f;
    public float minWaveIntensity = 0.0f;
    public float maxWaveIntensity = 1.0f;
    public float overshoot = 1.0f;
    public float period = 2f;

    private SpriteRenderer[] spriteRenderers;
    private List<Material> materials = new List<Material>();
    private bool menuEnabled = false;
    private bool isAnimating = false;
    private int activeTweens = 0;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            materials.Add(spriteRenderer.material);
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    private void Start()
    {
        DisableMenu();
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

        activeTweens = 0;

        AnimateFadeIn(fadeDuration);
        AnimateWaveIntensity(minWaveIntensity, maxWaveIntensity, tweenDuration);
    }

    [Button]
    public void AnimateDisable()
    {
        if (isAnimating) return;
        isAnimating = true;

        activeTweens = 0;

        AnimateFadeOut(fadeDuration);
        AnimateWaveIntensity(maxWaveIntensity, minWaveIntensity, tweenDuration);
    }

    public void AnimateWaveIntensity(float fromValue, float toValue, float duration)
    {
        foreach (Material material in materials)
        {
            activeTweens++;

            DOTween.To(
                () => material.GetFloat("_WaveIntensity"),
                x => material.SetFloat("_WaveIntensity", x),
                toValue,
                duration
            )
            .SetEase(Ease.OutElastic, overshoot, period)
            .OnComplete(() =>
            {
                activeTweens--;
                CheckAllTweensCompleted();
            });
        }
    }

    public void AnimateFadeIn(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            activeTweens++;

            Color color = sr.color;
            sr.color = new Color(1f, 1f, 1f, 0f);
            sr.DOFade(1, fadeDuration)
                .OnComplete(() =>
                {
                    activeTweens--;
                    CheckAllTweensCompleted();
                });
        }
    }

    public void AnimateFadeOut(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            activeTweens++;

            sr.DOFade(0, fadeDuration)
                .OnComplete(() =>
                {
                    activeTweens--;
                    CheckAllTweensCompleted();
                });
        }
    }

    private void CheckAllTweensCompleted()
    {
        if (activeTweens <= 0)
        {
            isAnimating = false;
        }
    }

    private void DisableMenu()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
