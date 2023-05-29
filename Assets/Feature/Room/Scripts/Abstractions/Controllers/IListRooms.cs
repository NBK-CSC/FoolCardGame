using System.Collections.Generic;
using FoolCardGame.Network;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс списка комнат
    /// </summary>
    public interface IListRooms
    {
        /// <summary>
        /// Обновить список
        /// </summary>
        /// <param name="rooms">Перечесления комнат</param>
        public void UpdateList(IEnumerable<RoomConfig> rooms);
    }
}