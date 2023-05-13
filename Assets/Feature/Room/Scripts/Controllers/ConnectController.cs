using FoolCardGame.Network.Controllers;
using FoolCardGame.Room.Views;
using UnityEngine;

namespace FoolCardGame.Room.Controllers
{
    /// <summary>
    /// Контроллер коннекта
    /// </summary>
    public class ConnectController : MonoBehaviour
    {
        [SerializeField] private string address = "127.0.0.1";
        [SerializeField] private int port = 4296;
        [Space]
        [SerializeField] private ConnectView view;
        
        private void Awake()
        {
            view.Init(Connect);
        }

        private void OnDisable()
        {
            NetworkController.Instance.Disconnect();
        }

        private void Connect()
        {
            if (!NetworkController.Instance.Connect(address, port))
                Debug.LogError("Не удалось подключиться к адресу");
        }
    }
}