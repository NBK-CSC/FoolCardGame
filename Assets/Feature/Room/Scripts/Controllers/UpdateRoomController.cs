using System;
using DarkRift.Client;
using FoolCardGame.Core;
using FoolCardGame.Network;
using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Rooms.Abstractions.Controllers;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер обновления комнаты
    /// </summary>
    public class UpdateRoomController : BaseRoomController<RoomData>, IUpdating, IDisposable
    {
        private IRoomController _roomController;

        protected override Tags Tag => Tags.UpdateRoom;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="roomController">Контроллер комнаты</param>
        public UpdateRoomController(IRoomController roomController)
        {
            _roomController = roomController;
            NetworkMessageController.Instance.SubscribeMessageReceived(OnMessageReceived);
        }
        
        protected override void OnResponse(RoomData response)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => _roomController.UpdateRoomData(NetworkMessageController.Instance.LocalId, response));
        }

        protected override void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            if (e.Tag != (ushort) Tag)
                return;
            OnResponse(NetworkMessageController.Instance.ReceiveMessage<RoomData>(sender, e));
        }

        /// <summary>
        /// Обновить инфу о комнате
        /// </summary>
        /// <param name="data">Инфа комнаты</param>
        public void Update(RoomData data)
        {
            NetworkMessageController.Instance.SendMessage(Tag, data);
        }

        public void Dispose()
        {
            NetworkMessageController.Instance.UnsubscribeMessageReceived(OnMessageReceived);
        }
    }
}