using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragHandlers
{
    public class MoverCardInHand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Vector2 _startPosition;

        public event Action DragHandlerBegun;
        public event Action DragHandlerEnded;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData touch)
        {
            _startPosition = _rectTransform.anchoredPosition;
            DragHandlerBegun?.Invoke();
        }

        public void OnDrag(PointerEventData touch)
        {
            Transform(touch.delta);
        }

        public void OnEndDrag(PointerEventData touch)
        {
            _rectTransform.anchoredPosition = _startPosition;
            DragHandlerEnded?.Invoke();
        }

        private void Transform(Vector2 delta)
        {
            Vector2 newPosition = CanTransform() ? delta : new Vector2(0, delta.y);
            _rectTransform.anchoredPosition += newPosition;
        }

        private bool CanTransform()
        {
            var rectPosition = _rectTransform.anchoredPosition;
            return _startPosition.y < rectPosition.y || Math.Abs(_startPosition.x - rectPosition.x) > 0f;
        }
    }
}