using DG.Tweening;
using UnityEngine;

public class GenericSticker : MonoBehaviour, IDraggable
{
    [SerializeField] float maxValue = 1f;
    [SerializeField] private float tweenDuration = 0.1f;
    [SerializeField] private float shrinkScale = 0.1f;

    SpriteRenderer spriteRenderer;
    Material material;
    public IDraggable.DragType Type => IDraggable.DragType.Sticker;

    private float stickerMoveValue = 0f;
    private bool dragging;
    private Vector3 originalScale;
    private bool isOverBin;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        originalScale = transform.localScale;
    }

    public bool IsDraggable() => true;

    public void OnDragBegin()
    {
        dragging = true;
        StickerBin.Instance.DragBegin();
    }

    public void OnDragEnd()
    {
        dragging = false;
        StickerBin.Instance.DragEnd();
        ResetScale();
        Animate();
    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            CheckStickerBin();
            Animate();
        }
    }

    void Animate()
    {
        float motion = Input.GetAxis("Mouse Y");
        float targetValue = Mathf.Clamp(motion * maxValue, -maxValue, maxValue);
        DOTween.To(
            () => stickerMoveValue,
            x =>
            {
                stickerMoveValue = x;
                material.SetFloat("_stickerMove", stickerMoveValue);
            },
            targetValue,
            tweenDuration
        ).SetEase(Ease.InOutSine);
    }

    void CheckStickerBin()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);

        if (hit != null && hit.GetComponent<IReceptacle>() != null)
        {
            if (!isOverBin)
            {
                isOverBin = true;
                transform.DOScale(originalScale * shrinkScale, tweenDuration).SetEase(Ease.OutQuad);
            }
            return;
        }

        if (isOverBin)
        {
            isOverBin = false;
            transform.DOScale(originalScale, tweenDuration).SetEase(Ease.OutQuad);
        }
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }

    public void OnHoverEnter() { }
    public void OnHoverExit() { }
}
