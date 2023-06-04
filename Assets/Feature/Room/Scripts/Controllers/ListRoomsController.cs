using System;
using System.Collections.Generic;
using FoolCardGame.Core;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Controllers
{
    public class ListRoomsController : MonoBehaviour, IListRooms
    {
        [SerializeField] private Transform content;
        [SerializeField] private AbstractRoomElementView roomElementPrefab;
        [SerializeField] private int number = 20;
        [SerializeField] private Button joinRoomButton;
        [SerializeField] private ToggleGroup toggleGroup;
        
        private PoolMono<AbstractRoomElementView> _poolMono;
        private List<IRoomElementController> _elements;

        private IJoining _joinController;
        private IRoomElementController _selectRoomElement;

        private void Awake()
        {
            _poolMono = new PoolMono<AbstractRoomElementView>(number, roomElementPrefab, content);
            _elements = new List<IRoomElementController>();
        }

        private void OnEnable()
        {
            joinRoomButton.onClick.AddListener(Join);
        }

        private void OnDisable()
        {
            joinRoomButton.onClick.RemoveListener(Join);
        }

        private void Join()
        {
            if (_selectRoomElement != null)
                _joinController.Join(_selectRoomElement.Id);
        }
        
        public void Init(IJoining joinController)
        {
            _joinController = joinController;
        }

        public void UpdateList(IEnumerable<RoomConfig> rooms)
        {
            Clear();
            
            foreach (var room in rooms)
            {
                var roomElementController = new RoomElementController(room, _poolMono.GetFreeObject(), toggleGroup);
                SubscribeSelect(roomElementController);
                _elements.Add(roomElementController);
            }
        }

        private void Clear()
        {
            _elements.ForEach(x =>
            {
                if (x is IDisposable disposable)
                    disposable.Dispose();
                UnsubscribeSelect(x);
            });
            
            _elements.Clear();
        }
        
        private void SubscribeSelect(IRoomElementController selected)
        {
            selected.OnSelected += Choose;
        }
        
        private void UnsubscribeSelect(IRoomElementController selected)
        {
            selected.OnSelected -= Choose;
        }
        
        private void Choose(IRoomElementController selected)
        {
            joinRoomButton.gameObject.SetActive(selected != null);
                
            _selectRoomElement = selected;
        }
    }
}