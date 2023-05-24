using DarkRift;
using DarkRift.Client;
using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Абстрактный класс комнаты
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRoomController<T> where T : IDarkRiftSerializable, new()
    {
        protected abstract Tags Tag { get; }
        
        protected abstract void OnResponse(T response);

        protected void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            if (e.Tag != (ushort) Tag)
                return;
            NetworkMessageController.Instance.UnsubscribeMessageReceived(OnMessageReceived);
            OnResponse(NetworkMessageController.Instance.ReceiveMessage<T>(sender, e));
        }
    }
}