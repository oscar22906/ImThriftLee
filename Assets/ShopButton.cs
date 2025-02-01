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

    Transform buttonTransform;

    private void Start()
    {
        buttonTransform = GetComponent<Transform>();
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

    public void Interact()
    {
        AnimateScale(duration, scaleFactor, buttonTransform);
        if (onButtonClick != null)
        {
            print("interact");
            onButtonClick.Invoke();
        }
    }

    public bool IsInteractable()
    {
        return true;
    }

    public void OnHoverEnter()
    {
        
    }

    public void OnHoverExit()
    {
        
    }
}
