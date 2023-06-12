namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс покидания комнаты
    /// </summary>
    public interface ILeaving
    {
        /// <summary>
        /// Покинуть комнату
        /// </summary>
        public void Leave();
    }
}