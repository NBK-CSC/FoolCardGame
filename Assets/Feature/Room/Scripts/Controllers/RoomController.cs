using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using FoolCardGame.Windows.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контролер комнаты
    /// </summary>
    public class RoomController : MonoBehaviour, IRoomController
    {
        [SerializeField] private int maxCount;
        [SerializeField] private AbstractPlayerElementView playerElementView;
        [SerializeField] private Transform playersPanel;
        //TODO уброть во вью
        [SerializeField] private Text textNameRoom;
        [SerializeField] private Window roomWindow;
        [SerializeField] private Window lobbyWindow;
        
        private ListPlayersController _listPlayersController;
        
        private void Awake()
        {
            _listPlayersController = new ListPlayersController(maxCount, playerElementView, playersPanel);
        }
        
        public void UpdateRoomData(string localId, RoomData roomData)
        {
            roomWindow.Open();
            lobbyWindow.Close();
            
            var listWithoutClient = new List<ClientData>(roomData.Clients.Where(c => c.Id != localId));
            _listPlayersController.UpdateList(listWithoutClient);
            textNameRoom.text = roomData.Config.Name;
        }

        public void Leave()
        {
            roomWindow.Close();
        }
    }
}