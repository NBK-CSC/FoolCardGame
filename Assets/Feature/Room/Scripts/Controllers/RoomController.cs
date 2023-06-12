using System;
using System.Linq;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using UnityEngine;

namespace FoolCardGame.Rooms.Controllers
{
    /// <summary>
    /// Контролер комнаты
    /// </summary>
    public class RoomController : IRoomController
    {
        private AbstractRoomView _view;
        
        private ListPlayersController _listPlayersController;
        private IUpdating _updateRoomController;
        private ILeaving _leaveController;
        
        private string _localId;
        private RoomData _roomData;
        
        public RoomController(AbstractRoomView view, ILeaving leaveController)
        {
            _leaveController = leaveController;

            _view = view;
            _view.Init(Confirm, Leave);
        }
        
        public void UpdateRoomData(string localId, RoomData roomData)
        {
            _localId = localId;
            _roomData = roomData;

            _updateRoomController ??= new UpdateRoomController(this);

            _view.SetWindowsActive(true);
            
            _view.UpdateRoomInfo(roomData.Config, roomData.Clients.Where(c => c.Id != localId));
            CheckPlayers();
            
            if (roomData.IsStart)
            {
                Debug.Log("Start");
            }
        }

        private void CheckPlayers()
        {
            _view.SwitchConfirmState(_roomData.Config.Slots == _roomData.Config.MaxSlots);
            
            if (_roomData.Config.Slots != _roomData.Config.MaxSlots)
            {
                if (_roomData.Clients.Find(c => c.Id == _localId).State == false)
                    return;

                SetPlayerState(false);
                _updateRoomController.Update(_roomData);
            }
        }

        private void Leave()
        {
            _view.SetWindowsActive(false);
            _leaveController.Leave();

            if (_updateRoomController is IDisposable disposable)
                disposable.Dispose();
            _updateRoomController = null;
        }

        private void Confirm()
        {
            SetPlayerState(true);
            
            if (_roomData.Clients.All(c => c.State))
                _roomData.IsStart = true;
            
            _updateRoomController.Update(_roomData);
        }

        private void SetPlayerState(bool state)
        {
            for (var i = 0; i < _roomData.Clients.Count; i++)
            {
                if (!string.Equals(_roomData.Clients[i].Id, _localId))
                    continue;
                
                var client = _roomData.Clients[i];
                client.State = state;
                _roomData.Clients[i] = client;
                
                return;
            }
        }
    }
}