using System;
using UnityEngine.EventSystems;

namespace FoolCardGame.Behaviours.Abstractions
{
    /// <summary>
    /// Детектор объекта с нужным компонентом
    /// </summary>
    /// <typeparam name="T">Компонент который надо задетектить</typeparam>
    public abstract class BaseComponentDropHandlerDetector<T> : AbstractComponentDetector<T>, IDropHandler
    {
        public override event Action<T> OnComponentDetected;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent<T>(out var obj))
                OnComponentDetected?.Invoke(obj);
        }
    }
}