using FoolCardGame.Network;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс контроллер комнаты
    /// </summary>
    public interface IRoomController
    {
        /// <summary>
        /// Обновить информацию о комнате
        /// </summary>
        /// <param name="localId">Локальный id</param>
        /// <param name="roomData">Данные комнаты</param>
        public void UpdateRoomData(string localId, RoomData roomData);

        /// <summary>
        /// Ливнуть
        /// </summary>
        public void Leave();
    }
}