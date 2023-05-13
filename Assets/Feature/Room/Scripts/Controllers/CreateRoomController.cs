using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Room.Abstractions.Controllers;
using FoolCardGame.Room.Views;
using FoolCardGamePlugin.Network;
using UnityEngine;

namespace FoolCardGame.Room.Controllers
{
    /// <summary>
    /// Контроллер создание комнаты
    /// </summary>
    public class CreateRoomController : BaseRoomController<RoomConfig>
    {
        private CreateRoomView _view;
        private IJoining _joinRoomController;

        protected override Tags Tag => Tags.CreateRoom;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="view">Вью создания комнаты</param>
        /// <param name="joinRoomController">Контроллер присоединения к комнате</param>
        public CreateRoomController(CreateRoomView view, IJoining joinRoomController)
        {
            _view = view;
            _view.Init(Create);

            _joinRoomController = joinRoomController;
        }

        private void Create(string name, byte maxNumber)
        {
            var room = new RoomConfig("", name, 0, maxNumber);
            NetworkMessageController.Instance.SubscribeMessageReceived(OnMessageReceived);
            NetworkMessageController.Instance.SendMessage(Tag, room);
        }
        
        protected override void OnResponse(RoomConfig response)
        {
            if (string.IsNullOrEmpty(response.Id) == false)
            {
                Debug.LogWarning($"Создалась {response.Id}");
                _joinRoomController.Join(response.Id);
                return;
            }
            Debug.LogWarning("Не удалось создать");
        }
    }
}