using System;
using DarkRift;
using DarkRift.Client;
using FoolCardGame.Network.Enums;

namespace FoolCardGame.Network.Controllers
{
    /// <summary>
    /// Контроллер сообщений
    /// </summary>
    public class NetworkMessageController
    {
        private static NetworkMessageController _instance;
        
        public static NetworkMessageController Instance => _instance ??= new NetworkMessageController();

        public string LocalId => NetworkController.Instance.Client.ID.ToString();

        private NetworkMessageController() { }
        
        /// <summary>
        /// Подписаться на получение сообщения
        /// </summary>
        /// <param name="eventHandler">Обработчик события</param>
        public void SubscribeMessageReceived(EventHandler<MessageReceivedEventArgs> eventHandler)
        {
            NetworkController.Instance.Client.MessageReceived += eventHandler;
        }
        
        /// <summary>
        /// Отписаться на получение сообщения
        /// </summary>
        /// <param name="eventHandler">Обработчик события</param>
        public void UnsubscribeMessageReceived(EventHandler<MessageReceivedEventArgs> eventHandler)
        {
            NetworkController.Instance.Client.MessageReceived -= eventHandler;
        }

        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="tag">Тэг</param>
        /// <param name="request">Сообщение</param>
        /// <param name="sendMode">Мод</param>
        /// <typeparam name="T">Тип объекта</typeparam>
        public void SendMessage<T>(Tags tag, T request, SendMode sendMode = SendMode.Reliable) where T : IDarkRiftSerializable
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(request);
                using (Message message = Message.Create((ushort)tag, writer))
                {
                    NetworkController.Instance.Client.SendMessage(message, sendMode);
                }
            }
        }
        
        public void SendMessage(Tags tag, SendMode sendMode = SendMode.Reliable)
        {
            using (Message message = Message.CreateEmpty((ushort)tag))
            {
                NetworkController.Instance.Client.SendMessage(message, sendMode);
            }
        }
        
        /// <summary>
        /// Получения сообщения
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Сообщение</param>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns>Десериализованный сообщение</returns>
        public T ReceiveMessage<T>(object sender, MessageReceivedEventArgs e) where T : IDarkRiftSerializable, new()
        {
            return e.GetMessage().Deserialize<T>();
        }
    }
}