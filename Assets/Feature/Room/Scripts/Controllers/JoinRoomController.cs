using System;
using FoolCardGame.Core;
using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Network;
using UnityEngine;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер присоединения к комнате
    /// </summary>
    public class JoinRoomController : BaseRoomController<RoomData>, IJoining
    {
        private IRoomController _roomController;
        
        protected override Tags Tag => Tags.JoinRoom;

        public JoinRoomController(IRoomController roomController)
        {
            _roomController = roomController;
        }

        public void Join(string id)
        {
            var room = new RoomConfig(id, "", 0, 0);
            NetworkMessageController.Instance.SubscribeMessageReceived(OnMessageReceived);
            NetworkMessageController.Instance.SendMessage(Tag, room);
        }
        
        protected override void OnResponse(RoomData response)
        {
            if (string.IsNullOrEmpty(response.Config.Id) == false)
            {
                Debug.LogWarning($"Присоединилось id={response.Config.Id} Name={response.Config.Name} Slots={response.Config.Slots}");
                UnityMainThreadDispatcher.Instance().Enqueue(() => _roomController.UpdateRoomData(NetworkMessageController.Instance.LocalId, response));
                return;
            }
            
            Debug.LogWarning("Не удалось присоединиться");
        }
    }
}