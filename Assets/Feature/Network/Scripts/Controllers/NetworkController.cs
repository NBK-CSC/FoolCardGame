using System;
using System.Net;
using DarkRift;
using DarkRift.Client;
using UnityEngine;

namespace FoolCardGame.Network.Controllers
{
    /// <summary>
    /// Network котроллер
    /// </summary>
    public class NetworkController
    {
        private static NetworkController _instance;
        private DarkRiftClient _client;
        
        /// <summary>
        /// Синглтон
        /// </summary>
        public static NetworkController Instance => _instance ??= new NetworkController();
        
        /// <summary>
        /// Присоединен ли
        /// </summary>
        public bool IsConnected => _client.ConnectionState == ConnectionState.Connected;

        /// <summary>
        /// Клиент
        /// </summary>
        public DarkRiftClient Client => _client;

        private NetworkController()
        {
            _client = new DarkRiftClient();
        }
        
        /// <summary>
        /// Присоединиться
        /// </summary>
        /// <returns></returns>
        public bool Connect(string address, int port)
        {
            if (_client.ConnectionState == ConnectionState.Connecting)
                return false;
            if (IsConnected)
                return true;

            try
            {
                _client.Connect(IPAddress.Parse(address), port, false);
                return true;
            }
            catch (Exception e)
            {
                //ignored
            }

            return false;
        }
        
        /// <summary>
        /// Отключиться
        /// </summary>
        public void Disconnect()
        {
            _client.Disconnect();
        }
    }
}