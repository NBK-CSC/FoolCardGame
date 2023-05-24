using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Network;
using FoolCardGame.Player.Abstractions.Views;
using FoolCardGame.Rooms.Abstractions.Controllers;
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
        [SerializeField] private AbstractPlayerView playerView;
        [SerializeField] private Transform playersPanel;
        [SerializeField] private Transform roomCanvas;
        [SerializeField] private Transform connectCanvas;
        //TODO уброть во вью
        [SerializeField] private Text textNameRoom;
        
        private PlayerListController _playerListController;
        private RoomData _roomData;
        private string _localId;
        private bool _isUpdated = true;
        private bool _isActive = false;
        
        private void Awake()
        {
            _playerListController = new PlayerListController(maxCount, playerView, playersPanel);
        }

        private void Update()
        {
            if (_isUpdated == false)
            {

                var listWithoutClient = new List<ClientData>(_roomData.Clients.Where(c => c.Id != _localId));
                _playerListController.UpdateList(listWithoutClient);
                textNameRoom.text = _roomData.Config.Name;
                _isUpdated = true;
            }
            
            roomCanvas.gameObject.SetActive(_isActive);
            connectCanvas.gameObject.SetActive(!_isActive);
        }
        
        public void UpdateRoomData(string localId, RoomData roomData)
        {
            _localId = localId;
            _roomData = roomData;
            _isUpdated = false;
            _isActive = true;
        }

        public void Leave()
        {
            _isActive = false;
        }
    }
}