using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ShopButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ButtonType type;

    public UnityEvent onButtonClick;

    [SerializeField] private float duration;
    [SerializeField] private float scaleFactor;


    public enum ButtonType
    {
        Left,
        Right,
        Buy
    }


    private Transform buttonTransform;
    private Vector3 originalScale;

    private void Start()
    {
        buttonTransform = GetComponent<Transform>();
        originalScale = buttonTransform.localScale;
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
        return true;
    }

    public void OnHoverEnter() { }

    public void OnHoverExit() { }
}
