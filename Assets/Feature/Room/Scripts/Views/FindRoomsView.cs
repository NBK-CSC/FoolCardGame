using System;
using FoolCardGame.Rooms.Abstractions.Views;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    /// <summary>
    /// Вью поиска комнат
    /// </summary>
    public class FindRoomsView : MonoBehaviour, IFindRoomsView
    {
        [SerializeField] private Button updateListButton;
        private Action _updateListAction = delegate { };
        
        private void OnEnable()
        {
            updateListButton.onClick.AddListener(UpdateList);
            UpdateList();
        }

        private void OnDisable()
        {
            updateListButton.onClick.RemoveListener(UpdateList);
        }

        public void Init(Action updateListAction)
        {
            _updateListAction = updateListAction;
        }

        private void UpdateList() => _updateListAction.Invoke();
    }
}