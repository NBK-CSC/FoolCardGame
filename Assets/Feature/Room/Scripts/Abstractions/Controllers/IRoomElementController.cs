using System;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Контроллер элемента комнаты
    /// </summary>
    public interface IRoomElementController
    {
        /// <summary>
        /// Id комнаты
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Ивент когда элемент выбрали
        /// </summary>
        public event Action<IRoomElementController> OnSelected;
    }
}