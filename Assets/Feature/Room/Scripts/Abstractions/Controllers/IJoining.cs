namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Интерфес присоединения к комнате
    /// </summary>
    public interface IJoining
    {
        /// <summary>
        /// Присоединиться
        /// </summary>
        /// <param name="id">id комнаты</param>
        public void Join(string id);
    }
}