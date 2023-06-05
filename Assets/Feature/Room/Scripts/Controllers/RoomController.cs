using System;
using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using FoolCardGame.Windows.Entities;
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
        private IUpdating _updateRoomController;
        
        private void Awake()
        {
            _listPlayersController = new ListPlayersController(maxCount, playerElementView, playersPanel);
        }
        
        public void UpdateRoomData(string localId, RoomData roomData)
        {
            SetWindow(true);

            _updateRoomController ??= new UpdateRoomController(this);
            
            var listWithoutClient = new List<ClientData>(roomData.Clients.Where(c => c.Id != localId));
            _listPlayersController.UpdateList(listWithoutClient);
            textNameRoom.text = roomData.Config.Name;
        }

        public void Leave()
        {
            SetWindow(false);

            if (_updateRoomController is IDisposable disposable)
                disposable.Dispose();
            _updateRoomController = null;
        }

        private void SetWindow(bool state)
        {
            roomWindow.SetActive(state);
            lobbyWindow.SetActive(!state);
        }
    }
}