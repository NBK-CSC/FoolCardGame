using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Room.Abstractions.Controllers;
using FoolCardGamePlugin.Network;
using UnityEngine;

namespace FoolCardGame.Room.Controllers
{
    /// <summary>
    /// Контроллер присоединения к комнате
    /// </summary>
    public class JoinRoomController : BaseRoomController<RoomData>, IJoining
    {
        protected override Tags Tag => Tags.JoinRoom;

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
                return;
            }
            
            Debug.LogWarning("Не удалось присоединиться");
        }
    }
}