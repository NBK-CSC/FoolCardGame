using System;
using FoolCardGame.Player.Abstractions.Controllers;
using FoolCardGame.Player.Enums;

namespace FoolCardGame.State.Abstractions.Controllers
{
    /// <summary>
    /// Интерфейс выдающего статусы игроков
    /// </summary>
    public interface IGettingStatus
    {
        /// <summary>
        /// Ивент, что статусы поменялись
        /// </summary>
        public event Action OnStatusesChanged;
        
        /// <summary>
        /// Получить статус
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Статус игрока</returns>
        public PlayerStatus GetStatus(IGettingSmallestTrumpCard player);
    }
}