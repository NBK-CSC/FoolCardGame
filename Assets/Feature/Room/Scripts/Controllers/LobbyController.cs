using FoolCardGame.Rooms.Views;
using UnityEngine;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер лобби
    /// </summary>
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private CreateRoomView createRoomView;
        [SerializeField] private RoomController roomController;
        [SerializeField] private LeaveRoomView leaveRoomView;

        private CreateRoomController _createRoomController;
        private JoinRoomController _joinRoomController;
        private LeaveRoomController _leaveRoomController;

        private void Start()
        {
            _joinRoomController = new JoinRoomController(roomController);
            _createRoomController = new CreateRoomController(createRoomView, _joinRoomController);
            _leaveRoomController = new LeaveRoomController(leaveRoomView, roomController);
        }
    }
}