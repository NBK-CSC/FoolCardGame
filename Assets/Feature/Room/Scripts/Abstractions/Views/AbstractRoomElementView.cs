using UnityEngine;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    /// <summary>
    /// Абстрактный вью элемента комнаты
    /// </summary>
    public abstract class AbstractRoomElementView : MonoBehaviour
    {
        /// <summary>
        /// Сделать активным
        /// </summary>
        /// <param name="state">Состояние активности</param>
        public abstract void SetActive(bool state);

        /// <summary>
        /// Обновить информацию о комнате
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="count">Количество</param>
        /// <param name="maxCount">Маскимальное количество</param>
        public abstract void UpdateRoomInfo(string name, int count, int maxCount);
    }
}