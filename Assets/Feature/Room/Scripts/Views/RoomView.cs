using System;
using System.Collections.Generic;
using System.Linq;
using FoolCardGame.Network;
using FoolCardGame.Rooms.Abstractions.Controllers;
using FoolCardGame.Rooms.Abstractions.Views;
using FoolCardGame.Rooms.Controllers;
using FoolCardGame.Windows.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью комнаты
    /// </summary>
    public class RoomView : AbstractRoomView
    {
        [SerializeField] private int maxCount = 6;
        [SerializeField] private AbstractPlayerElementView playerElementView;
        [SerializeField] private Transform playersPanel;
        [Space]
        [SerializeField] private Text textNameRoom;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button leaveButton;
        [Space]
        [SerializeField] private Window roomWindow;
        [SerializeField] private Window lobbyWindow;
        
        private Action _confirmAction;
        private Action _leaveAction;
        
        private ListPlayersController _listPlayersController;

        private void Awake()
        {
            _listPlayersController = new ListPlayersController(maxCount, playerElementView, playersPanel);
        }

        public override void Init(Action confirmAction, Action leaveAction)
        {
            _confirmAction = confirmAction;
            _leaveAction = leaveAction;
        }

        private void OnEnable()
        {
            confirmButton.onClick.AddListener(Confirm);
            leaveButton.onClick.AddListener(Leave);
        }

        private void OnDisable()
        {
            confirmButton.onClick.RemoveListener(Confirm);
            leaveButton.onClick.RemoveListener(Leave);
        }

        private void Confirm()
        {
            _confirmAction.Invoke();
        }
        
        private void Leave()
        {
            _leaveAction.Invoke();
        }

        public override void UpdateRoomInfo(RoomConfig roomConfig, IEnumerable<ClientData> clientsData)
        {
            _listPlayersController.UpdateList(clientsData.ToList());
            textNameRoom.text = roomConfig.Name;
        }

        public override void SwitchConfirmState(bool state)
        {
            confirmButton.gameObject.SetActive(state);
            // TODO таймер
        }

        public override void SetWindowsActive(bool state)
        {
            roomWindow.SetActive(state);
            lobbyWindow.SetActive(!state);
        }
    }
}