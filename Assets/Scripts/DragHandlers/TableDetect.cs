using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragHandlers
{
    public class TableDetect : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<ILaying>(out var card))
                card.TryLay();
        }
    }
}