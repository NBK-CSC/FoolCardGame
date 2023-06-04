using System;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Controllers
{
    public class RoomElementController : IRoomElementController, IDisposable
    {
        private RoomConfig _config;
        private AbstractRoomElementView _view;

        public string Id => _config.Id;
        
        public event Action<IRoomElementController> OnSelected;

        public RoomElementController(RoomConfig config, AbstractRoomElementView view, ToggleGroup toggleGroup)
        {
            _config = config;
            _view = view;

            _view.UpdateRoomInfo(_config.Name, _config.Slots, _config.MaxSlots);
            _view.SetToggleGroup(toggleGroup);
            _view.OnSelected += Select;
        }

        private void Select(bool state)
        {
            OnSelected?.Invoke(state ? this : null);
        }
        
        public void Dispose()
        {
            _view.OnSelected -= Select;
            _view.SetActive(false);
        }
    }
}