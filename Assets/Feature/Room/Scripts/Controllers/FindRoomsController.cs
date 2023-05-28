using System.Collections.Generic;
using DarkRift.Client;
using FoolCardGame.Network;
using FoolCardGame.Network.Controllers;
using FoolCardGame.Network.Enums;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контроллер поиска комнат
    /// </summary>
    public class FindRoomsController
    {
        private IFindRoomsView _view;
        private IListRooms _listRoomsController;
        
        private Tags Tag => Tags.GetRooms;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="view">Вью</param>
        /// <param name="listRoomsController">Контроллер списка комнат</param>
        public FindRoomsController(IFindRoomsView view, IListRooms listRoomsController)
        {
            _view = view;
            _listRoomsController = listRoomsController;
            
            _view.Init(FindRooms);
        }

        private void OnResponse(IEnumerable<RoomConfig> rooms)
        {
            _listRoomsController.UpdateList(rooms);
        }
        
        private void OnMessageReceived(object? sender, MessageReceivedEventArgs e)
        {
            if (e.Tag != (ushort) Tag)
                return;
            NetworkMessageController.Instance.UnsubscribeMessageReceived(OnMessageReceived);
            OnResponse(NetworkMessageController.Instance.ReceiveMessages<RoomConfig>(sender, e));
        }

        private void FindRooms()
        {
            NetworkMessageController.Instance.SubscribeMessageReceived(OnMessageReceived);
            NetworkMessageController.Instance.SendMessage(Tag);
        }
    }
}