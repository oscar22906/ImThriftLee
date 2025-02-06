using DG.Tweening;
using UnityEngine;

public class GenericSticker : MonoBehaviour, IDraggable
{
    [Tooltip("e.g 3.15f")]
    [SerializeField] float maxValue = 3.15f;
    [SerializeField] private float tweenDuration;

    SpriteRenderer spriteRenderer;
    Material material;
    public IDraggable.DragType Type => IDraggable.DragType.Sticker;

    private float stickerMoveValue = 0f;

    private bool dragging;

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
        dragging = true;
    }

    public void OnDragEnd()
    {
        dragging = false;
        Animate();
    }


    private void FixedUpdate()
    {
        if (dragging)
        {
            Animate();
        }
    }
    void Animate() // Called on FixedUpdate
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
        )
        .SetEase(Ease.InOutSine);
    }


    //void Animate(float start, float end)
    //{
    //    if (material == null) return;
    //    if (Random.Range(0,2) == 0)
    //    {
    //        end = -end;
    //        start = -start;
    //    }

    //    DOTween.To(
    //        () => stickerMoveValue,
    //        x =>
    //        {
    //            stickerMoveValue = x;
    //            material.SetFloat("_stickerMove", stickerMoveValue);
    //        },
    //        end,
    //        tweenDuration
    //    ).SetEase(Ease.InOutSine)
    //     .OnComplete(() =>
    //     {
    //         stickerMoveValue = end;
    //     });
    //}
    public void OnHoverEnter()
    {

    }

    public void OnHoverExit()
    {

    }
}
