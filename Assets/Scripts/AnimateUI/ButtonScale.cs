using DG.Tweening;
using UnityEngine;
using VInspector;

public class ButtonScale : MonoBehaviour, IUIAnimate
{
    public float duration = 1f;
    public float scaleFactor = 1.2f;
    public float fadeDuration = 1.5f;
    public float popDelay = 0.1f;
    private Transform[] childTransforms;
    private SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        childTransforms = System.Array.FindAll(childTransforms, t => t != transform);

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    [Button]
    public void AnimateEnable()
    {
        AnimateFadeIn(fadeDuration);
        AnimateWithStagger();
    }

    [Button]
    public void AnimateDisable()
    {
        AnimateFadeOut(fadeDuration);
        AnimateShrinkAndDisable();
    }

    [Button]
    public void Animate()
    {
        foreach (Transform tf in childTransforms)
        {
            AnimateScale(duration, scaleFactor, tf);
        }
    }

    public void AnimateWithStagger()
    {
        for (int i = 0; i < childTransforms.Length; i++)
        {
            Transform tf = childTransforms[i];
            float delay = i * popDelay;

            tf.localScale = Vector3.zero;
            tf.DOScale(Vector3.one * scaleFactor, duration / 2)
                .SetEase(Ease.OutQuad)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    tf.DOScale(Vector3.one, duration)
                        .SetEase(Ease.OutElastic);
                });
        }
    }

    public void AnimateFadeIn(float fadeDuration)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
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
        float totalDelay = (childTransforms.Length - 1) * popDelay;

        for (int i = 0; i < childTransforms.Length; i++)
        {
            Transform tf = childTransforms[i];
            float delay = i * popDelay;

            Tween tween = tf.DOScale(Vector3.zero, duration)
                .SetEase(Ease.InQuad)
                .SetDelay(delay);

            if (i == childTransforms.Length - 1)
            {
                tween.OnComplete(() =>
                {
                    MapManager.Instance?.DeactivateAllShops();
                });
            }
        }
    }

    public void AnimateScale(float duration, float scaleFactor, Transform tf)
    {
        Vector3 originalScale = tf.localScale;
        Vector3 targetScale = originalScale * scaleFactor;

        tf.DOScale(targetScale, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                tf.DOScale(originalScale, duration)
                    .SetEase(Ease.OutElastic);
            });
    }
}
