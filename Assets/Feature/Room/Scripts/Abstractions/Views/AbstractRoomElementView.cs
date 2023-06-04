using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    /// <summary>
    /// Абстрактный вью элемента комнаты
    /// </summary>
    public abstract class AbstractRoomElementView : MonoBehaviour, ISelected
    {
        /// <summary>
        /// Ивент когда элемент выбрали
        /// </summary>
        public abstract event Action<bool> OnSelected;

        /// <summary>
        /// Сделать активным
        /// </summary>
        /// <param name="state">Состояние активности</param>
        public abstract void SetActive(bool state);
        
        /// <summary>
        /// Устновить Toggle group
        /// </summary>
        /// <param name="state">Состояние активности</param>
        public abstract void SetToggleGroup(ToggleGroup group);

        /// <summary>
        /// Обновить информацию о комнате
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="count">Количество</param>
        /// <param name="maxCount">Маскимальное количество</param>
        public abstract void UpdateRoomInfo(string name, int count, int maxCount);
    }
}