using System;
using FoolCardGame.Player.Enums;

namespace FoolCardGame.Player.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс доющего статус игрока
    /// </summary>
    public interface IGivingState
    {
        /// <summary>
        /// Статус игрока
        /// </summary>
        public PlayerStatus PlayerStatus { get; }
        
        /// <summary>
        /// Ивент изменения статуса
        /// </summary>
        public event Action OnStatusChanged;
    }
}