public interface IDraggable : IElement
{
    void OnDragBegin();
    void OnDragEnd();
    bool IsDraggable();

    public enum DragType
    {
        Generic,
        Sticker,
        Clothing
    }


    public DragType Type { get; }
}
