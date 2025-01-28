using DG.Tweening;
using UnityEngine;

public class GenericSticker : MonoBehaviour, IDraggable
{
    [Tooltip("e.g 3.15f")]
    [SerializeField] float maxValue = 0f;
    [Tooltip("e.g 0f")]
    [SerializeField] float minValue = 3.15f;
    [SerializeField] private float tweenDuration;

    SpriteRenderer spriteRenderer;
    Material material;
    public IDraggable.DragType Type => IDraggable.DragType.Sticker;

    private float stickerMoveValue = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

    }
    public bool IsDraggable()
    {
        return true;
        
    }

    public void OnDragBegin()
    {
        Animate(minValue, maxValue);
    }

    public void OnDragEnd()
    {
        Animate(maxValue, minValue);
    }

    void Animate(float start, float end)
    {
        if (material == null) return;
        if (Random.Range(0,2) == 0)
        {
            end = -end;
            start = -start;
        }

        DOTween.To(
            () => stickerMoveValue,
            x =>
            {
                stickerMoveValue = x;
                material.SetFloat("_stickerMove", stickerMoveValue);
            },
            end,
            tweenDuration
        ).SetEase(Ease.InOutSine)
         .OnComplete(() =>
         {
             stickerMoveValue = end;
         });
    }
    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }
}
