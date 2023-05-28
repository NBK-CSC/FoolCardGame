using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер покидания комнаты
    /// </summary>
    public class LeaveRoomController
    {
        private ILeaveRoomView _leaveRoomView;
        private IRoomController _roomController;
        
        private Tags Tag => Tags.LeaveRoom;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="leaveRoomView">Вью</param>
        /// <param name="roomController">контроллер комнаты</param>
        public LeaveRoomController(ILeaveRoomView leaveRoomView, IRoomController roomController)
        {
            _leaveRoomView = leaveRoomView;
            _leaveRoomView.Init(Leave);

            _roomController = roomController;
        }

        private void Leave()
        {
            NetworkMessageController.Instance.SendMessage(Tag);
            _roomController.Leave();
        }
    }
}