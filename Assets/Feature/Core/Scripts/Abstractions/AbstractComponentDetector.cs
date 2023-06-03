using System;
using UnityEngine;

namespace FoolCardGame.Core.Abstractions
{
    /// <summary>
    /// Абстрация детектора компонентов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractComponentDetector<T> : MonoBehaviour
    {
        /// <summary>
        /// Ивент обнаружения объекта с нужным компонентом
        /// </summary>
        public abstract event Action<T> OnComponentDetected;
    }
}