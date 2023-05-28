using System;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    /// <summary>
    /// Интерфейс вью покидания комнаты
    /// </summary>
    public interface ILeaveRoomView
    {
        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="leaveAction">Действие покидания</param>
        public void Init(Action leaveAction);
    }
}