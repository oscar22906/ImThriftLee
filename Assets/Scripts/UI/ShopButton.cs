using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ShopButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ButtonType type;

    public UnityEvent onButtonClick;

    [SerializeField] private float duration;
    [SerializeField] private float scaleFactor;

    [SerializeField] private bool animateScale = true;


    public enum ButtonType
    {
        Left,
        Right,
        Buy
    }


    private Transform buttonTransform;
    [SerializeField] private Vector3 originalScale;

    private void Start()
    {
        buttonTransform = GetComponent<Transform>();
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
        if (animateScale)
        {
            AnimateScale(duration, scaleFactor, buttonTransform);
            return;
        }
        else
        {
            onButtonClick?.Invoke();
        }
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
