using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Rooms.Abstractions.Controllers;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер покидания комнаты
    /// </summary>
    public class LeaveRoomController : ILeaving
    {
        private Tags Tag => Tags.LeaveRoom;

        public void Leave()
        {
            NetworkMessageController.Instance.SendMessage(Tag);
        }
    }
}