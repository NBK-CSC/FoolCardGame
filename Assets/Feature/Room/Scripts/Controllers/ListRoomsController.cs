using System;
using System.Collections.Generic;
using FoolCardGame.Behaviours;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using FoolCardGame.Rooms.Views;
using UnityEngine;

namespace FoolCardGame.Rooms.Controllers
{
    public class ListRoomsController : MonoBehaviour, IListRooms
    {
        [SerializeField] private Transform content;
        [SerializeField] private AbstractRoomElementView roomElementPrefab;
        [SerializeField] private int number = 20;
        private List<AbstractRoomElementView> _elements;

        private PoolMono<AbstractRoomElementView> _poolMono;
        //TODO вырезать
        private bool _isUpdated = true;
        private IEnumerable<RoomConfig> _rooms;

        private void Awake()
        {
            _poolMono = new PoolMono<AbstractRoomElementView>(number, roomElementPrefab, content);
            _elements = new List<AbstractRoomElementView>();
        }
        
        public void UpdateList(IEnumerable<RoomConfig> rooms)
        {
            _isUpdated = false;
            _rooms = rooms;
            return;
            Clear();
            
            foreach (var room in rooms)
            {
                var roomView = _poolMono.GetFreeObject();
                roomView.UpdateRoomInfo(room.Name, room.Slots, room.MaxSlots);
                _elements.Add(roomView);
            }
        }

        private void UpdateList()
        {
            Clear();
            
            foreach (var room in _rooms)
            {
                var roomView = _poolMono.GetFreeObject();
                roomView.UpdateRoomInfo(room.Name, room.Slots, room.MaxSlots);
                _elements.Add(roomView);
            }
        }

        private void Update()
        {
            if (_isUpdated == false)
            {
                UpdateList();
                _isUpdated = true;
            }
        }

        private void Clear()
        {
            _elements.ForEach(x => x.SetActive(false));
            _elements.Clear();
        }
    }
}