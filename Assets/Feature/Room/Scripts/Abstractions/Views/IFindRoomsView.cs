using System;

namespace FoolCardGame.Rooms.Abstractions.Views
{
    /// <summary>
    /// Вью поиск комнат
    /// </summary>
    public interface IFindRoomsView
    {
        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="updateListAction">Действие обновления комнаты</param>
        public void Init(Action updateListAction);
    }
}