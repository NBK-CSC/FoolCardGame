using System;
using UnityEngine;
using UnityEngine.UI;
using FoolCardGame.Rooms.Abstractions.Controllers;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью покидания комнаты
    /// </summary>
    public class LeaveRoomView : MonoBehaviour, ILeaveRoomView
    {
        [SerializeField] private Button leaveButton;
        private Action _leaveAction;

        public void Init(Action leaveAction)
        {
            _leaveAction = leaveAction;
        }

        private void OnEnable()
        {
            leaveButton.onClick.AddListener(Leave);
        }

        private void OnDisable()
        {
            leaveButton.onClick.RemoveListener(Leave);
        }

        private void Leave()
        {
            _leaveAction.Invoke();
        }
    }
}