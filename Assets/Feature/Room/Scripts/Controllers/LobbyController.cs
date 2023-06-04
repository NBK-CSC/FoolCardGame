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
        [SerializeField] private ListRoomsController listRoomsController;
        [SerializeField] private FindRoomsView findRoomsView;

        private CreateRoomController _createRoomController;
        private JoinRoomController _joinRoomController;
        private LeaveRoomController _leaveRoomController;
        private FindRoomsController _findRoomsController;

        private void Start()
        {
            _joinRoomController = new JoinRoomController(roomController);
            _createRoomController = new CreateRoomController(createRoomView, _joinRoomController);
            _leaveRoomController = new LeaveRoomController(leaveRoomView, roomController);
            _findRoomsController = new FindRoomsController(findRoomsView, listRoomsController);
            
            listRoomsController.Init(_joinRoomController);
        }
    }
}