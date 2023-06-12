using FoolCardGame.Rooms.Abstractions.Controllers;
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
        [SerializeField] private AbstractRoomView roomView;
        [SerializeField] private ListRoomsController listRoomsController;
        [SerializeField] private FindRoomsView findRoomsView;

        private CreateRoomController _createRoomController;
        private JoinRoomController _joinRoomController;
        private LeaveRoomController _leaveRoomController;
        private FindRoomsController _findRoomsController;
        private RoomController _roomController;

        private void Start()
        {
            _leaveRoomController = new LeaveRoomController();

            _roomController = new RoomController(roomView, _leaveRoomController);
            _joinRoomController = new JoinRoomController(_roomController);
            _createRoomController = new CreateRoomController(createRoomView, _joinRoomController);
            _findRoomsController = new FindRoomsController(findRoomsView, listRoomsController);
            
            listRoomsController.Init(_joinRoomController);
        }
    }
}