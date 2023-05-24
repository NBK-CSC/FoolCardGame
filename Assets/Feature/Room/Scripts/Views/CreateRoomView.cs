using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Rooms.Views
{
    public class CreateRoomView : MonoBehaviour
    {
        [SerializeField] private InputField nameInputField;
        [SerializeField] private InputField countInputField;
        [SerializeField] private Button createRoomButton;
        private Action<string, byte> _createRoomAction;

        private void OnEnable()
        {
            createRoomButton.onClick.AddListener(CreateRoom);
        }

        private void OnDisable()
        {
            createRoomButton.onClick.RemoveListener(CreateRoom);
        }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="createRoomAction">Действие создание комнаты</param>
        public void Init(Action<string, byte> createRoomAction)
        {
            _createRoomAction = createRoomAction;
        }

        private void CreateRoom()
        {
            if (byte.TryParse(countInputField.text, out var count))
                _createRoomAction.Invoke(nameInputField.text, count);
        }
    }
}