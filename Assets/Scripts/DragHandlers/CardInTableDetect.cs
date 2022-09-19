using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragHandlers
{
    public class CardInTableDetect : MonoBehaviour, IDropHandler
    {
        private ICardData _data;
        
        public void Init(ICardData data)
        {
            _data = data;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<IBeating>(out var card))
                card.TryBeat(_data);
        }
    }
}