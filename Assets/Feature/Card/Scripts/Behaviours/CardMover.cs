using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FoolCardGame.Card.Behaviours
{
    /// <summary>
    /// Класс перемещения карты в руке
    /// </summary>
    public class CardMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Vector2 _startPosition;

        /// <summary>
        /// Ивент начало захвата карты
        /// </summary>
        public event Action DragHandlerBegun;
        
        /// <summary>
        /// Ивент конца захвата карты
        /// </summary>
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
            var newPosition = CanTransform() ? delta : new Vector2(0, delta.y);
            _rectTransform.anchoredPosition += newPosition;
        }

        private bool CanTransform()
        {
            var rectPosition = _rectTransform.anchoredPosition;
            return _startPosition.y < rectPosition.y || Math.Abs(_startPosition.x - rectPosition.x) > 0f;
        }
    }
}