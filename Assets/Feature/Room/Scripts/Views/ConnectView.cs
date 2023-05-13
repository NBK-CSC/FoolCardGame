using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoolCardGame.Room.Views
{
    public class ConnectView : MonoBehaviour
    {
        [SerializeField] private Button connectButton;
        private Action _connectAction;

        private void OnEnable()
        {
            connectButton.onClick.AddListener(Connect);
        }

        private void OnDisable()
        {
            connectButton.onClick.RemoveListener(Connect);
        }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="connectAction">Действие коннекта</param>
        public void Init(Action connectAction)
        {
            _connectAction = connectAction;
        }

        private void Connect()
        {
            _connectAction.Invoke();
        }
    }
}