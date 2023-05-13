using FoolCardGame.Room.Views;
using UnityEngine;

namespace FoolCardGame.Room.Controllers
{
    /// <summary>
    /// Контроллер лобби
    /// </summary>
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private CreateRoomView createRoomView;

        private CreateRoomController _createRoomController;
        private JoinRoomController _joinRoomController;

        private void Start()
        {
            _joinRoomController = new JoinRoomController();
            _createRoomController = new CreateRoomController(createRoomView, _joinRoomController);
        }
    }
}