public interface IDraggable : IElement
{
    void OnDragBegin();
    void OnDragEnd();
    bool isDraggable();
}