using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using VInspector;

public class AnimateButton : MonoBehaviour, IInteractable
{
    public UnityEvent onButtonClick;

    [SerializeField] private float duration;
    [SerializeField] private float scaleFactor;
    [SerializeField] private bool isInteractable = true;
    private Transform buttonTransform;
    private Vector3 originalScale;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        buttonTransform = GetComponent<Transform>();
        originalScale = buttonTransform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AnimateScale(float duration, float scaleFactor, Transform tf)
    {
        tf.DOKill(); // Stop any ongoing animations
        tf.localScale = originalScale; // Reset scale

        Vector3 targetScale = originalScale * scaleFactor;

        tf.DOScale(targetScale, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                tf.DOScale(originalScale, duration)
                    .SetEase(Ease.OutElastic);
                AnimateComplete();
            });
    }

    public void Interact()
    {
        AnimateScale(duration, scaleFactor, buttonTransform);
    }

    void AnimateComplete()
    {
        onButtonClick?.Invoke();
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public void OnHoverEnter() { }

    public void OnHoverExit() { }
    [Button]
    public void DisableButton()
    {
        isInteractable = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }
    }
    [Button]
    public void EnableButton()
    {
        isInteractable = true;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
