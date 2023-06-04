using FoolCardGame.Network;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс обновляющий комнату
    /// </summary>
    public interface IUpdating
    {
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="data">Инфа комнаты</param>
        public void Update(RoomData data);
    }
}