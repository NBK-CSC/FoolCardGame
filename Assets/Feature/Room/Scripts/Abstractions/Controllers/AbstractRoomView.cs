using System;
using System.Collections.Generic;
using FoolCardGame.Network;
using UnityEngine;

namespace FoolCardGame.Rooms.Abstractions.Controllers
{
    /// <summary>
    /// Абстрактная вью комнаты
    /// </summary>
    public abstract class AbstractRoomView : MonoBehaviour
    {
        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="confirmAction">Функция подтверждения</param>
        /// <param name="leaveAction">Функция покидания</param>
        public abstract void Init(Action confirmAction, Action leaveAction);

        /// <summary>
        /// Обновить инфу комнаты
        /// </summary>
        /// <param name="roomConfig">Конфиг комнаты</param>
        /// <param name="clientsData">Перечисления инфы клиентов</param>
        public abstract void UpdateRoomInfo(RoomConfig roomConfig, IEnumerable<ClientData> clientsData);

        /// <summary>
        /// Установить активность игрока
        /// </summary>
        /// <param name="state">Состояние</param>
        /// <param name="time">Время</param>
        public abstract void SwitchConfirmState(bool state);

        /// <summary>
        /// Установить активность окон
        /// </summary>
        /// <param name="state">Состояние активности</param>
        public abstract void SetWindowsActive(bool state);
    }
}