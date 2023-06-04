using System;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    /// <summary>
    /// Интерфейс выбора
    /// </summary>
    public interface ISelected
    {
        /// <summary>
        /// Ивент выбрали ли
        /// </summary>
        public event Action<bool> OnSelected;
    }
}